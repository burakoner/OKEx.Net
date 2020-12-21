using CryptoExchange.Net;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Okex.Net.RestObjects;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using Okex.Net.Interfaces;
using System.Diagnostics;
using System.Text;
using System.Globalization;
using System.Security;
using System.Security.Cryptography;
using Okex.Net.SocketObjects.Structure;
using Okex.Net.SocketObjects.Containers;
using Okex.Net.Helpers;
using CryptoExchange.Net.Authentication;

namespace Okex.Net
{
    /// <summary>
    /// Client for the Okex socket API
    /// </summary>
    public partial class OkexSocketClient : SocketClient, IOkexSocketClient
    {
        #region Client Options
        private static OkexSocketClientOptions defaultOptions = new OkexSocketClientOptions();
        private static OkexSocketClientOptions DefaultOptions => defaultOptions.Copy();
        #endregion

        private SecureString? Key;
        private SecureString? Secret;
        private SecureString? PassPhrase;
        private HMACSHA256? _hmacEncryptor;
        public bool Authendicated { get; private set; }

        #region Constructor/Destructor
        /// <summary>
        /// Create a new instance of OkexSocketClient with default options
        /// </summary>
        public OkexSocketClient() : this(DefaultOptions)
        {
        }

        /// <summary>
        /// Create a new instance of OkexSocketClient using provided options
        /// </summary>
        /// <param name="options">The options to use for this client</param>
        public OkexSocketClient(OkexSocketClientOptions options) : base("Okex", options, options.ApiCredentials == null ? null : new OkexAuthenticationProvider(options.ApiCredentials, "", false, ArrayParametersSerialization.Array))
        {
            SetDataInterpreter(DecompressData, null);
        }
        #endregion

        #region Common Methods
        /// <summary>
        /// Set the default options to be used when creating new socket clients
        /// </summary>
        /// <param name="options">The options to use for new clients</param>
        public static void SetDefaultOptions(OkexSocketClientOptions options)
        {
            defaultOptions = options;
        }

        /// <summary>
        /// Set the API key and secret
        /// </summary>
        /// <param name="apiKey">The api key</param>
        /// <param name="apiSecret">The api secret</param>
        /// <param name="passPhrase">The api pass phrase</param>
        public void SetApiCredentials(string apiKey, string apiSecret, string passPhrase)
        {
            this.Key = apiKey.ToSecureString();
            this.Secret = apiSecret.ToSecureString();
            this.PassPhrase = passPhrase.ToSecureString();
        }
        #endregion

        #region General
        public CallResult<OkexGeneralPingPongContainer> Ping() => PingAsync().Result;
        public async Task<CallResult<OkexGeneralPingPongContainer>> PingAsync()
        {
            var pit = DateTime.UtcNow;
            var sw = Stopwatch.StartNew();
            var result = await Query<string>("ping", false).ConfigureAwait(false);
            var pot = DateTime.UtcNow;
            sw.Stop();

            return new CallResult<OkexGeneralPingPongContainer>(new OkexGeneralPingPongContainer {PingTime=pit, PongTime=pot, Latency=sw.Elapsed, PongMessage=result.Data }, result.Error);
        }
        #endregion

        #region Private & Protected Methods
        private static string DecompressData(byte[] byteData)
        {
            using (var decompressedStream = new MemoryStream())
            using (var compressedStream = new MemoryStream(byteData))
            using (var deflateStream = new DeflateStream(compressedStream, CompressionMode.Decompress))
            {
                deflateStream.CopyTo(decompressedStream);
                decompressedStream.Position = 0;

                using (var streamReader = new StreamReader(decompressedStream))
                {
                    /** /
                    var response = streamReader.ReadToEnd();
                    return response;
                    /**/

                    return streamReader.ReadToEnd();
                }
            }
        }

        protected override bool HandleQueryResponse<T>(SocketConnection s, object request, JToken data, out CallResult<T> callResult)
        {
            callResult = new CallResult<T>(default, null);

            // Check for Error
            // 30040: {0} Channel : {1} doesn't exist
            if (data["event"] != null && (string)data["event"]! == "error" && data["errorCode"] != null)
            {
                log.Write(LogVerbosity.Warning, "Query failed: " + (string)data["message"]!);
                callResult = new CallResult<T>(default, new ServerError($"{(string)data["errorCode"]!}, {(string)data["message"]!}"));
                return true;
            }

            // Ping Request
            if (data.ToString() == "pong")
            {
                var desResult = Deserialize<T>(data, false);
                if (!desResult)
                {
                    log.Write(LogVerbosity.Warning, $"Failed to deserialize data: {desResult.Error}. Data: {data}");
                    return false;
                }

                callResult = new CallResult<T>(desResult.Data, null);
                return true;
            }

            // Login Request
            if (data["event"] != null && (string)data["event"]! == "login")
            {
                var desResult = Deserialize<T>(data, false);
                if (!desResult)
                {
                    log.Write(LogVerbosity.Warning, $"Failed to deserialize data: {desResult.Error}. Data: {data}");
                    return false;
                }

                callResult = new CallResult<T>(desResult.Data, null);
                return true;
            }

            return true;
        }

        protected override bool HandleSubscriptionResponse(SocketConnection s, SocketSubscription subscription, object request, JToken message, out CallResult<object>? callResult)
        {
            callResult = null;

            // Check for Error
            // 30040: {0} Channel : {1} doesn't exist
            if (message["event"] != null && (string)message["event"]! == "error" && message["errorCode"] != null && (string)message["errorCode"]! == "30040")
            {
                log.Write(LogVerbosity.Warning, "Subscription failed: " + (string)message["message"]!);
                callResult = new CallResult<object>(null, new ServerError($"{(string)message["errorCode"]!}, {(string)message["message"]!}"));
                return true;
            }

            // Check for Success
            if (message["event"] != null && (string)message["event"]! == "subscribe" && message["channel"] != null)
            {
                if (request is OkexSocketRequest socRequest)
                {
                    if (socRequest.Arguments.Contains((string)message["channel"]!))
                    {
                        log.Write(LogVerbosity.Debug, "Subscription completed");
                        callResult = new CallResult<object>(true, null);
                        return true;
                    }
                }
            }

            return false;
        }

        protected override bool MessageMatchesHandler(JToken message, object request)
        {

#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            if (request is OkexSocketRequest hRequest)
            {
                // Spot Tickers Update
                if (hRequest.Operation == OkexSocketOperation.Subscribe && message["table"] != null && ((string)message["table"]!).StartsWith("spot/ticker"))
                {
                    if (message["data"] != null && message["data"].HasValues && message["data"][0]["instrument_id"] != null)
                    {
                        var channel = (string)message["table"] + ":" + (string)message["data"][0]["instrument_id"];
                        if (hRequest.Arguments.Contains(channel))
                        {
                            return true;
                        }
                    }
                }

                // Spot Candlesticks Update
                if (hRequest.Operation == OkexSocketOperation.Subscribe && message["table"] != null && ((string)message["table"]).StartsWith("spot/candle"))
                {
                    if (message["data"] != null && message["data"].HasValues && message["data"][0]["instrument_id"] != null)
                    {
                        var channel = (string)message["table"]! + ":" + (string)message["data"][0]["instrument_id"];
                        if (hRequest.Arguments.Contains(channel))
                        {
                            return true;
                        }
                    }
                }

                // Spot Trades Update
                if (hRequest.Operation == OkexSocketOperation.Subscribe && message["table"] != null && ((string)message["table"]).StartsWith("spot/trade"))
                {
                    if (message["data"] != null && message["data"].HasValues && message["data"][0]["instrument_id"] != null)
                    {
                        var channel = (string)message["table"] + ":" + (string)message["data"][0]["instrument_id"];
                        if (hRequest.Arguments.Contains(channel))
                        {
                            return true;
                        }
                    }
                }

                // Spot Depth5 Update
                if (hRequest.Operation == OkexSocketOperation.Subscribe && message["table"] != null && ((string)message["table"]).StartsWith("spot/depth5"))
                {
                    if (message["data"] != null && message["data"].HasValues && message["data"][0]["instrument_id"] != null)
                    {
                        var channel = (string)message["table"] + ":" + (string)message["data"][0]["instrument_id"];
                        if (hRequest.Arguments.Contains(channel))
                        {
                            return true;
                        }
                    }
                }

                // Spot Depth400 Update
                if (hRequest.Operation == OkexSocketOperation.Subscribe && message["table"] != null && ((string)message["table"]).StartsWith("spot/depth"))
                {
                    if (message["data"] != null && message["data"].HasValues && message["data"][0]["instrument_id"] != null)
                    {
                        var channel = (string)message["table"] + ":" + (string)message["data"][0]["instrument_id"];
                        if (hRequest.Arguments.Contains(channel))
                        {
                            return true;
                        }
                    }
                }

                // User Spot Account (Private)
                if (hRequest.Operation == OkexSocketOperation.Subscribe && message["table"] != null && ((string)message["table"]).StartsWith("spot/account"))
                {
                    if (message["data"] != null && message["data"].HasValues && message["data"][0]["currency"] != null)
                    {
                        var channel = (string)message["table"] + ":" + (string)message["data"][0]["currency"];
                        if (hRequest.Arguments.Contains(channel))
                        {
                            return true;
                        }
                    }
                }
                
                // User Margin Account (Private)
                if (hRequest.Operation == OkexSocketOperation.Subscribe && message["table"] != null && ((string)message["table"]).StartsWith("spot/margin_account"))
                {
                    if (message["data"] != null && message["data"].HasValues && message["data"][0]["instrument_id"] != null)
                    {
                        var channel = (string)message["table"] + ":" + (string)message["data"][0]["instrument_id"];
                        if (hRequest.Arguments.Contains(channel))
                        {
                            return true;
                        }
                    }
                }

                // Spot Orders (Private)
                if (hRequest.Operation == OkexSocketOperation.Subscribe && message["table"] != null && ((string)message["table"]).StartsWith("spot/order"))
                {
                    if (message["data"] != null && message["data"].HasValues && message["data"][0]["instrument_id"] != null)
                    {
                        var channel = (string)message["table"] + ":" + (string)message["data"][0]["instrument_id"];
                        if (hRequest.Arguments.Contains(channel))
                        {
                            return true;
                        }
                    }
                }

                // Spot Algo Orders (Private)
                if (hRequest.Operation == OkexSocketOperation.Subscribe && message["table"] != null && ((string)message["table"]).StartsWith("spot/order_algo"))
                {
                    if (message["data"] != null && message["data"].HasValues && message["data"][0]["instrument_id"] != null)
                    {
                        var channel = (string)message["table"] + ":" + (string)message["data"][0]["instrument_id"];
                        if (hRequest.Arguments.Contains(channel))
                        {
                            return true;
                        }
                    }
                }

                // -->>

                // Futures Contracts
                if (hRequest.Operation == OkexSocketOperation.Subscribe && message["table"] != null && ((string)message["table"]!).StartsWith("futures/instruments"))
                {
                    var channel = (string)message["table"];
                    if (hRequest.Arguments.Contains(channel))
                    {
                        return true;
                    }
                }

                // Futures Tickers Update
                if (hRequest.Operation == OkexSocketOperation.Subscribe && message["table"] != null && ((string)message["table"]!).StartsWith("futures/ticker"))
                {
                    if (message["data"] != null && message["data"].HasValues && message["data"][0]["instrument_id"] != null)
                    {
                        var channel = (string)message["table"] + ":" + (string)message["data"][0]["instrument_id"];
                        if (hRequest.Arguments.Contains(channel))
                        {
                            return true;
                        }
                    }
                }

                // Futures Candlesticks Update
                if (hRequest.Operation == OkexSocketOperation.Subscribe && message["table"] != null && ((string)message["table"]).StartsWith("futures/candle"))
                {
                    if (message["data"] != null && message["data"].HasValues && message["data"][0]["instrument_id"] != null)
                    {
                        var channel = (string)message["table"]! + ":" + (string)message["data"][0]["instrument_id"];
                        if (hRequest.Arguments.Contains(channel))
                        {
                            return true;
                        }
                    }
                }


            }

            return false;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        protected override bool MessageMatchesHandler(JToken message, string identifier)
        {
            return true;
        }

        protected override async Task<CallResult<bool>> AuthenticateSocket(SocketConnection s)
        {
            if (this.Key == null || this.Secret == null || this.PassPhrase == null)
                return new CallResult<bool>(false, new NoApiCredentialsError());

            var key = this.Key.GetString();
            var secret = this.Secret.GetString();
            var passphrase = this.PassPhrase.GetString();
            if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(secret) || string.IsNullOrEmpty(passphrase))
                return new CallResult<bool>(false, new NoApiCredentialsError());

            var time = (DateTime.UtcNow.ToUnixTimeMilliSeconds() / 1000.0m).ToString(CultureInfo.InvariantCulture);
            var signtext = time + "GET" + "/users/self/verify";
            this._hmacEncryptor = new HMACSHA256(Encoding.ASCII.GetBytes(this.Secret.GetString()));
            var signature = OkexAuthenticationProvider.Base64Encode(_hmacEncryptor.ComputeHash(Encoding.UTF8.GetBytes(signtext)));
            var request = new OkexSocketRequest(OkexSocketOperation.Login, this.Key.GetString(), this.PassPhrase.GetString(), time, signature);

            var result = new CallResult<bool>(false, new ServerError("No response from server"));
            await s.SendAndWait(request, ResponseTimeout, data =>
            {
                if ((string)data["event"] != "login")
                    return false;

                var authResponse = Deserialize<OkexSocketLoginResponse>(data, false);
                if (!authResponse)
                {
                    log.Write(LogVerbosity.Warning, "Authorization failed: " + authResponse.Error);
                    result = new CallResult<bool>(false, authResponse.Error);
                    return true;
                }
                if (!authResponse.Data.Success)
                {
                    log.Write(LogVerbosity.Warning, "Authorization failed: " + authResponse.Error.Message);
                    result = new CallResult<bool>(false, new ServerError(authResponse.Error.Code.Value, authResponse.Error.Message));
                    return true;
                }

                log.Write(LogVerbosity.Debug, "Authorization completed");
                result = new CallResult<bool>(true, null);
                return true;
            });

            return result;
        }

        protected override async Task<bool> Unsubscribe(SocketConnection connection, SocketSubscription s)
        {
            if (s == null || s.Request == null)
                return false;

            var request = new OkexSocketRequest(OkexSocketOperation.Unsubscribe, ((OkexSocketRequest)s.Request).Arguments);
            await connection.SendAndWait(request, ResponseTimeout, data =>
            {
                if (data.Type != JTokenType.Object)
                    return false;

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                if ((string)data["event"] == "unsubscribe")
                {
                    return (string)data["channel"] == request.Arguments.FirstOrDefault();
                }
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

                return false;
            });
            return false;
        }

        #endregion

    }
}
