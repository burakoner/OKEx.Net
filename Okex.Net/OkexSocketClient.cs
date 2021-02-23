using CryptoExchange.Net;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Newtonsoft.Json.Linq;
using Okex.Net.CoreObjects;
using Okex.Net.Helpers;
using Okex.Net.Interfaces;
using Okex.Net.SocketObjects.Structure;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Okex.Net
{
    /// <summary>
    /// Client for the Okex Websocket API
    /// </summary>
    public partial class OkexSocketClient : SocketClient, IOkexSocketClient
    {
        #region Client Options
        protected static OkexSocketClientOptions defaultOptions = new OkexSocketClientOptions();
        protected static OkexSocketClientOptions DefaultOptions => defaultOptions.Copy();
        #endregion

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
        #endregion

        protected static string DecompressData(byte[] byteData)
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

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8629 // Nullable value type may be null.
        protected override bool HandleQueryResponse<T>(SocketConnection s, object request, JToken data, out CallResult<T> callResult)
        {
            return this.OkexHandleQueryResponse<T>(s, request, data, out callResult);
        }
        protected virtual bool OkexHandleQueryResponse<T>(SocketConnection s, object request, JToken data, out CallResult<T> callResult)
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

            // Check for Error
            // 30040: {0} Channel : {1} doesn't exist
            if (data is JObject && data["event"] != null && (string)data["event"]! == "error" && data["errorCode"] != null)
            {
                log.Write(LogVerbosity.Warning, "Query failed: " + (string)data["message"]!);
                callResult = new CallResult<T>(default, new ServerError($"{(string)data["errorCode"]!}, {(string)data["message"]!}"));
                return true;
            }

            // Login Request
            if (data is JObject && data["event"] != null && (string)data["event"]! == "login")
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

            return false;
        }

        protected override bool HandleSubscriptionResponse(SocketConnection s, SocketSubscription subscription, object request, JToken message, out CallResult<object>? callResult)
        {
            return this.OkexHandleSubscriptionResponse(s, subscription, request, message, out callResult);
        }
        protected virtual bool OkexHandleSubscriptionResponse(SocketConnection s, SocketSubscription subscription, object request, JToken message, out CallResult<object>? callResult)
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
            return this.OkexMessageMatchesHandler(message, request);
        }
        protected virtual bool OkexMessageMatchesHandler(JToken message, object request)
        {
            if (request is OkexSocketRequest hRequest)
            {
                if (hRequest.Operation != OkexSocketOperation.Subscribe || message["table"] == null)
                    return false;

                // Table Name
                var table = (string)message["table"]!;

                // Match Lists
                var m_dict = new Dictionary<string, List<string>>
                {
                    {"instrument_id", new List<string>{
                        "spot/ticker", // Spot Tickers Update
                        "spot/candle", // Spot Candlesticks Update
                        "spot/trade", // Spot Trades Update
                        "spot/depth_l2_tbt", // Spot Depth-TBT Update
                        "spot/depth5", // Spot Depth-5 Update
                        "spot/depth", // Spot Depth-400 Update
                        "spot/order", // Spot Orders Update (Private)
                        "spot/order_algo", // Spot Algo Orders Update (Private)

                        "spot/margin_account", // Margin Balance Update (Private)

                        "futures/ticker", // Futures Tickers Update
                        "futures/candle", // Futures Candlesticks Update
                        "futures/trade", // Futures Trades Update
                        "futures/price_range", // Futures Price Range Update
                        "futures/estimated_price", // Futures Estimated Price Update
                        "futures/depth_l2_tbt", // Futures Depth-TBT Update
                        "futures/depth5", // Futures Depth-5 Update
                        "futures/depth", // Futures Depth-400 Update
                        "futures/mark_price", // Futures Mark Price Update
                        "futures/position", // Futures Positions Update (Private)
                        "futures/order", // Futures Orders Update (Private)
                        "futures/order_algo", // Futures Algo Orders Update (Private)

                        "swap/ticker", // Perpetual Swap Tickers Update
                        "swap/candle", // Perpetual Swap Candlesticks Update
                        "swap/trade", // Perpetual Swap Trades Update
                        "swap/funding_rate", // Perpetual Swap Funding Rate Update
                        "swap/price_range", // Perpetual Swap Price Range Update
                        "swap/depth_l2_tbt", // Perpetual Swap Depth-TBT Update
                        "swap/depth5", // Perpetual Swap Depth-5 Update
                        "swap/depth", // Perpetual Swap Depth-400 Update
                        "swap/mark_price", // Perpetual Swap Mark Price Update
                        "swap/position", // Perpetual Swap Positions Update (Private)
                        "swap/order", // Perpetual Swap Orders Update (Private)
                        "swap/order_algo", // Perpetual Swap Algo Orders Update (Private)

                        "option/candle", // Options Candlesticks Update
                        "option/trade", // Options Trades Update
                        "option/ticker", // Options Tickers Update
                        "option/depth_l2_tbt", // Options Depth-TBT Update
                        "option/depth5", // Options Depth-5 Update
                        "option/depth", // Options Depth-400 Update
                        "option/position", // Options Positions Update (Private)
                        "option/order", // Options Orders Update (Private)

                        "index/ticker", // Index Tickers Update
                        "index/candle", // Index Candlesticks Update
                    } },
                    {"currency", new List<string>{
                        "spot/account", // Spot Balance Update (Private)
                    } },
                    {"underlying", new List<string>{
                        "option/instruments", // Options Contracts Update
                        "option/summary", // Options Market Data Update
                        "option/account", // Options Balance Update (Private)
                    } },
                };

                foreach (var kvp in m_dict)
                {
                    foreach (var m_url in kvp.Value)
                    {
                        if (table.StartsWith(m_url))
                        {
                            if (message["data"] != null && message["data"].HasValues && message["data"][0][kvp.Key] != null)
                            {
                                var channel = (string)message["table"]! + ":" + (string)message["data"][0][kvp.Key];
                                if (hRequest.Arguments.Contains(channel))
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }

                #region Exceptional Matchings
                // Futures Contracts Update
                if (table.StartsWith("futures/instruments"))
                {
                    foreach (var arg in hRequest.Arguments)
                    {
                        if (arg == table)
                            return true;
                    }
                }

                // Futures Balance (Private)
                if (table.StartsWith("futures/account"))
                {
                    foreach (var arg in hRequest.Arguments)
                    {
                        if (arg.StartsWith("futures/account"))
                            return true;
                    }
                }

                // Swap Balance (Private)
                if (table.StartsWith("swap/account"))
                {
                    foreach (var arg in hRequest.Arguments)
                    {
                        if (arg.StartsWith("swap/account"))
                            return true;
                    }
                }
                #endregion

            }

            return false;
        }

        protected override bool MessageMatchesHandler(JToken message, string identifier)
        {
            return this.OkexMessageMatchesHandler(message, identifier);
        }
        protected virtual bool OkexMessageMatchesHandler(JToken message, string identifier)
        {
            return true;
        }

        protected override async Task<CallResult<bool>> AuthenticateSocket(SocketConnection s)
        {
            return await this.OkexAuthenticateSocket(s);
        }
        protected virtual async Task<CallResult<bool>> OkexAuthenticateSocket(SocketConnection s)
        {
            if (Key == null || Secret == null || PassPhrase == null)
                return new CallResult<bool>(false, new NoApiCredentialsError());

            var key = Key.GetString();
            var secret = Secret.GetString();
            var passphrase = PassPhrase.GetString();
            if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(secret) || string.IsNullOrEmpty(passphrase))
                return new CallResult<bool>(false, new NoApiCredentialsError());

            var time = (DateTime.UtcNow.ToUnixTimeMilliSeconds() / 1000.0m).ToString(CultureInfo.InvariantCulture);
            var signtext = time + "GET" + "/users/self/verify";
            _hmacEncryptor = new HMACSHA256(Encoding.ASCII.GetBytes(Secret.GetString()));
            var signature = OkexAuthenticationProvider.Base64Encode(_hmacEncryptor.ComputeHash(Encoding.UTF8.GetBytes(signtext)));
            var request = new OkexSocketRequest(OkexSocketOperation.Login, Key.GetString(), PassPhrase.GetString(), time, signature);

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
            return await this.OkexUnsubscribe(connection, s);
        }
        protected virtual async Task<bool> OkexUnsubscribe(SocketConnection connection, SocketSubscription s)
        {
            if (s == null || s.Request == null)
                return false;

            var request = new OkexSocketRequest(OkexSocketOperation.Unsubscribe, ((OkexSocketRequest)s.Request).Arguments);
            await connection.SendAndWait(request, ResponseTimeout, data =>
            {
                if (data.Type != JTokenType.Object)
                    return false;

                if ((string)data["event"] == "unsubscribe")
                {
                    return (string)data["channel"] == request.Arguments.FirstOrDefault();
                }

                return false;
            });
            return false;
        }
#pragma warning restore CS8629 // Nullable value type may be null.
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
    }
}
