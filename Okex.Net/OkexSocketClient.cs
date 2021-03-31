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
        private const string InstrumentIdChannelKey = "instrument_id";
        private const string UnderlyingChannelKey = "underlying";

        // Match Lists
        Dictionary<string, string>? channelKeyByTable = new Dictionary<string, string>
        {
            {"spot/ticker", InstrumentIdChannelKey}, // Spot Tickers Update
            {"spot/candle", InstrumentIdChannelKey}, // Spot Candlesticks Update
            {"spot/trade", InstrumentIdChannelKey}, // Spot Trades Update
            {"spot/depth_l2_tbt", InstrumentIdChannelKey}, // Spot Depth-TBT Update
            {"spot/depth5", InstrumentIdChannelKey}, // Spot Depth-5 Update
            {"spot/depth", InstrumentIdChannelKey}, // Spot Depth-400 Update
            {"spot/order", InstrumentIdChannelKey}, // Spot Orders Update (Private)
            {"spot/order_algo", InstrumentIdChannelKey}, // Spot Algo Orders Update (Private)

            {"spot/margin_account", InstrumentIdChannelKey}, // Margin Balance Update (Private)

            {"futures/ticker", InstrumentIdChannelKey}, // Futures Tickers Update
            {"futures/candle", InstrumentIdChannelKey}, // Futures Candlesticks Update
            {"futures/trade", InstrumentIdChannelKey}, // Futures Trades Update
            {"futures/price_range", InstrumentIdChannelKey}, // Futures Price Range Update
            {"futures/estimated_price", InstrumentIdChannelKey}, // Futures Estimated Price Update
            {"futures/depth_l2_tbt", InstrumentIdChannelKey}, // Futures Depth-TBT Update
            {"futures/depth5", InstrumentIdChannelKey}, // Futures Depth-5 Update
            {"futures/depth", InstrumentIdChannelKey}, // Futures Depth-400 Update
            {"futures/mark_price", InstrumentIdChannelKey}, // Futures Mark Price Update
            {"futures/position", InstrumentIdChannelKey}, // Futures Positions Update (Private)
            {"futures/order", InstrumentIdChannelKey}, // Futures Orders Update (Private)
            {"futures/order_algo", InstrumentIdChannelKey}, // Futures Algo Orders Update (Private)

            {"swap/ticker", InstrumentIdChannelKey}, // Perpetual Swap Tickers Update
            {"swap/candle", InstrumentIdChannelKey}, // Perpetual Swap Candlesticks Update
            {"swap/trade", InstrumentIdChannelKey}, // Perpetual Swap Trades Update
            {"swap/funding_rate", InstrumentIdChannelKey}, // Perpetual Swap Funding Rate Update
            {"swap/price_range", InstrumentIdChannelKey}, // Perpetual Swap Price Range Update
            {"swap/depth_l2_tbt", InstrumentIdChannelKey}, // Perpetual Swap Depth-TBT Update
            {"swap/depth5", InstrumentIdChannelKey}, // Perpetual Swap Depth-5 Update
            {"swap/depth", InstrumentIdChannelKey}, // Perpetual Swap Depth-400 Update
            {"swap/mark_price", InstrumentIdChannelKey}, // Perpetual Swap Mark Price Update
            {"swap/position", InstrumentIdChannelKey}, // Perpetual Swap Positions Update (Private)
            {"swap/order", InstrumentIdChannelKey}, // Perpetual Swap Orders Update (Private)
            {"swap/order_algo", InstrumentIdChannelKey}, // Perpetual Swap Algo Orders Update (Private)

            {"option/candle", InstrumentIdChannelKey}, // Options Candlesticks Update
            {"option/trade", InstrumentIdChannelKey}, // Options Trades Update
            {"option/ticker", InstrumentIdChannelKey}, // Options Tickers Update
            {"option/depth_l2_tbt", InstrumentIdChannelKey}, // Options Depth-TBT Update
            {"option/depth5", InstrumentIdChannelKey}, // Options Depth-5 Update
            {"option/depth", InstrumentIdChannelKey}, // Options Depth-400 Update
            {"option/position", InstrumentIdChannelKey}, // Options Positions Update (Private)
            {"option/order", InstrumentIdChannelKey}, // Options Orders Update (Private)

            {"index/ticker", InstrumentIdChannelKey}, // Index Tickers Update
            {"index/candle", InstrumentIdChannelKey}, // Index Candlesticks Update

            {"spot/account", "currency"}, // Spot Balance Update (Private)

            {"option/instruments", UnderlyingChannelKey}, // Options Contracts Update
            {"option/summary", UnderlyingChannelKey}, // Options Market Data Update
            {"option/account", UnderlyingChannelKey}, // Options Balance Update (Private)
        };

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

                if (channelKeyByTable.TryGetValue(table, out var channelKey))
                {
                    var data = message["data"];
                    if (data?.HasValues ?? false)
                    {
                        var channelSuffix = data[0][channelKey];

                        if (channelSuffix != null && hRequest.Arguments.Contains(table + ":" + channelSuffix))
                        {
                            return true;
                        }
                    }
                }
                else
                {
                    return table == "futures/instruments" && hRequest.Arguments.Contains(table) ||
                           (table == "futures/account" || table == "swap/account") && hRequest.Arguments.Any(a => a.StartsWith(table));
                }
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
