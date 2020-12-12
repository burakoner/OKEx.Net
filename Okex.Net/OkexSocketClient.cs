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
        private HMACSHA256? hmacEncryptor;
        private bool socketAuthendicated;


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
        public OkexSocketClient(OkexSocketClientOptions options) : base("OKEx", options, options.ApiCredentials == null ? null : new OkexAuthenticationProvider(options.ApiCredentials, "", false, ArrayParametersSerialization.Array))
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

        #region Private Methods & Subscriptions
        public CallResult<bool> User_Login(string apiKey, string apiSecret, string passPhrase) => User_Login_Async(apiKey,  apiSecret,  passPhrase).Result;
        public async Task<CallResult<bool>> User_Login_Async(string apiKey, string apiSecret, string passPhrase)
        {
            if (string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(apiSecret) || string.IsNullOrEmpty(passPhrase))
                return new CallResult<bool>(false, new NoApiCredentialsError());

            this.Key = apiKey.ToSecureString();
            this.Secret = apiSecret.ToSecureString();
            this.PassPhrase = passPhrase.ToSecureString();

            var time = (DateTime.UtcNow.ToUnixTimeMilliSeconds() / 1000.0m).ToString(CultureInfo.InvariantCulture);
            var signtext = time + "GET" + "/users/self/verify";
            hmacEncryptor = new HMACSHA256(Encoding.ASCII.GetBytes(this.Secret.GetString()));
            var signature = OkexAuthenticationProvider.Base64Encode(hmacEncryptor.ComputeHash(Encoding.UTF8.GetBytes(signtext)));

            var result = await Query<OkexSocketLoginResponse>(new OkexSocketRequest(OkexSocketOperation.Login, this.Key.GetString(), this.PassPhrase.GetString(), time, signature), false).ConfigureAwait(false);
            this.socketAuthendicated = result.Success;
            return new CallResult<bool>(result.Success, result.Error);
        }

        public CallResult<UpdateSubscription> User_Spot_SubscribeToBalance(string currency, Action<OkexSpotAccount> onData) => User_Spot_SubscribeToBalance_Async(currency, onData).Result;
        public async Task<CallResult<UpdateSubscription>> User_Spot_SubscribeToBalance_Async(string currency, Action<OkexSpotAccount> onData)
        {
            currency = currency.ValidateCurrency();

            var internalHandler = new Action<OkexSocketUpdateResponse<IEnumerable<OkexSpotAccount>>>(data =>
            {
                foreach (var d in data.Data)
                {
                    onData(d);
                }
            });

            var request = new OkexSocketRequest(OkexSocketOperation.Subscribe, $"spot/account:{currency}");
            return await Subscribe(request, null, false, internalHandler).ConfigureAwait(false);
        }

        // TODO: User Margin Account

        public CallResult<UpdateSubscription> User_Spot_SubscribeToOrders(string symbol, Action<OkexSpotOrderDetails> onData) => User_Spot_SubscribeToOrders_Async(symbol, onData).Result;
        public async Task<CallResult<UpdateSubscription>> User_Spot_SubscribeToOrders_Async(string symbol, Action<OkexSpotOrderDetails> onData)
        {
            symbol = symbol.ValidateSymbol();

            var internalHandler = new Action<OkexSocketUpdateResponse<IEnumerable<OkexSpotOrderDetails>>>(data =>
            {
                foreach (var d in data.Data)
                {
                    onData(d);
                }
            });

            var request = new OkexSocketRequest(OkexSocketOperation.Subscribe, $"spot/order:{symbol}");
            return await Subscribe(request, null, false, internalHandler).ConfigureAwait(false);
        }

        // TODO: User Algo Orders

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
                    /**/
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

            return true;
        }

        protected override bool HandleSubscriptionResponse(SocketConnection s, SocketSubscription subscription, object request, JToken message, out CallResult<object>? callResult)
        {
            callResult = null;

            // Check for Error
            // 30040: {0} Channel : {1} doesn't exist
            if (message["event"] != null && (string)message["event"] == "error" && message["errorCode"] != null && (string)message["errorCode"] == "30040")
            {
                log.Write(LogVerbosity.Warning, "Subscription failed: " + (string)message["message"]);
                callResult = new CallResult<object>(null, new ServerError($"{(string)message["errorCode"]}, {(string)message["message"]}"));
                return true;
            }

            // Check for Success
            if (message["event"] != null && (string)message["event"] == "subscribe" && message["channel"] != null)
            {
                if (request is OkexSocketRequest socRequest)
                {
                    if (socRequest.Arguments.Contains((string)message["channel"]))
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
            if (request is OkexSocketRequest hRequest)
            {
                // Tickers Update
                if (hRequest.Operation == OkexSocketOperation.Subscribe && message["table"] != null && ((string)message["table"]).StartsWith("spot/ticker"))
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

                // Candlesticks Update
                if (hRequest.Operation == OkexSocketOperation.Subscribe && message["table"] != null && ((string)message["table"]).StartsWith("spot/candle"))
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

                // Trades Update
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

                // Depth5 Update
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

                // Depth400 Update
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

                // User Orders (Private)
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
            }

            return false;
        }

        protected override bool MessageMatchesHandler(JToken message, string identifier)
        {
            return true;
        }

        protected override async Task<CallResult<bool>> AuthenticateSocket(SocketConnection s)
        {
            if (authProvider == null)
                return new CallResult<bool>(false, new NoApiCredentialsError());
            /** /
            var authParams = authProvider.AddAuthenticationToParameters(BaseAddress, HttpMethod.Get, new Dictionary<string, object>(), true);
            var authObjects = new OkexSocketRequest("auth", 
                authProvider.Credentials.Key!.GetString(),
                (string)authParams["SignatureMethod"],
                authParams["SignatureVersion"].ToString(),
                (string)authParams["Timestamp"],
                (string)authParams["Signature"]);
            */
            var result = new CallResult<bool>(false, new ServerError("No response from server"));
            /*
            await s.SendAndWait(authObjects, ResponseTimeout, data =>
            {
                if ((string)data["op"] != "auth")
                    return false;

                var authResponse = Deserialize<OkexSocketAuthDataResponse<object>>(data, false);
                if (!authResponse)
                {
                    log.Write(LogVerbosity.Warning, "Authorization failed: " + authResponse.Error);
                    result = new CallResult<bool>(false, authResponse.Error);
                    return true;
                }
                if (!authResponse.Data.IsSuccessful)
                {
                    log.Write(LogVerbosity.Warning, "Authorization failed: " + authResponse.Data.ErrorMessage);
                    result = new CallResult<bool>(false, new ServerError(authResponse.Data.ErrorCode, authResponse.Data.ErrorMessage ?? "-"));
                    return true;
                }

                log.Write(LogVerbosity.Debug, "Authorization completed");
                result = new CallResult<bool>(true, null);
                return true;
            });
            */

            return result;
        }

        protected override async Task<bool> Unsubscribe(SocketConnection connection, SocketSubscription s)
        {
            string topic;
            object? unsub = null;
            string? unsubId = null;
            var idField = "id";
            /*
            if (s.Request is OkexSocketRequest hRequest)
            {
                topic = hRequest.Topic;
                unsub = new OkexSocketRequest(unsubId, topic);
            }

            if (s.Request is OkexSocketRequest haRequest)
            {
                topic = haRequest.Topic;
                unsubId = NextId().ToString();
                unsub = new OkexSocketRequest(unsubId, topic);
                idField = "cid";
            }
            */

            var result = false;
            await connection.SendAndWait(unsub, ResponseTimeout, data =>
            {
                if (data.Type != JTokenType.Object)
                    return false;

                var id = (string)data[idField];
                if (id == unsubId)
                {
                    result = (string)data["status"] == "ok";
                    return true;
                }

                return false;
            });
            return result;
        }

        #endregion

    }
}
