using CryptoExchange.Net;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Okex.Net.Converters;
using Okex.Net.RestObjects;
using Okex.Net.SocketObjects;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Okex.Net.Interfaces;
using Okex.Net.SocketObjects.Spot;

namespace Okex.Net
{
    /// <summary>
    /// Client for the Okex socket API
    /// </summary>
    public class OkexSocketClient : SocketClient, IOkexSocketClient
    {
        #region Client Options
        private static OkexSocketClientOptions defaultOptions = new OkexSocketClientOptions();
        private static OkexSocketClientOptions DefaultOptions => defaultOptions.Copy();
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
        public OkexSocketClient(OkexSocketClientOptions options) : base(options, options.ApiCredentials == null ? null : new OkexAuthenticationProvider(options.ApiCredentials, "", false, ArrayParametersSerialization.Array))
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
        /*
        public CallResult<PingPongContainer> Ping() => PingAsync().Result;
        public async Task<CallResult<PingPongContainer>> PingAsync()
        {
            var result = await Query<string>("ping", false).ConfigureAwait(false);
            return new CallResult<PingPongContainer>(new PingPongContainer {Timestamp=DateTime.UtcNow, Message=result.Data }, result.Error);
        }
        */
        #endregion

        #region Spot & Margin
        /// <summary>
        /// Retrieve the latest price, best bid & offer and 24-hours trading volume of a single contract.
        /// </summary>
		/// <param name="symbol">Trading pair symbol</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public CallResult<UpdateSubscription> Spot_SubscribeToTicker(string symbol, Action<RestObjects.Spot.Ticker> onData) => Spot_SubscribeToTicker_Async(symbol, onData).Result;
        /// <summary>
        /// Retrieve the latest price, best bid & offer and 24-hours trading volume of a single contract.
        /// </summary>
		/// <param name="symbol">Trading pair symbol</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public async Task<CallResult<UpdateSubscription>> Spot_SubscribeToTicker_Async(string symbol, Action<RestObjects.Spot.Ticker> onData)
        {
            symbol = symbol.ValidateSymbol();

            var internalHandler = new Action<SocketUpdateResponse<IEnumerable<RestObjects.Spot.Ticker>>>(data =>
            {
                foreach (var d in data.Data)
                {
                    onData(d);
                }
            });

            var request = new SocketRequest(SocketOperation.Subscribe, $"spot/ticker:{symbol}");
            return await Subscribe(request, null, false, internalHandler).ConfigureAwait(false);
        }


        /// <summary>
        /// Retrieve the candlestick data
        /// </summary>
		/// <param name="symbol">Trading pair symbol</param>
        /// <param name="period">The period of a single candlestick</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public CallResult<UpdateSubscription> Spot_SubscribeToCandlesticks(string symbol, SpotPeriod period, Action<RestObjects.Spot.Candle> onData) => Spot_SubscribeToCandlesticks_Async(symbol, period, onData).Result;
        /// <summary>
        /// Retrieve the candlestick data
        /// </summary>
		/// <param name="symbol">Trading pair symbol</param>
        /// <param name="period">The period of a single candlestick</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public async Task<CallResult<UpdateSubscription>> Spot_SubscribeToCandlesticks_Async(string symbol, SpotPeriod period, Action<RestObjects.Spot.Candle> onData)
        {
            symbol = symbol.ValidateSymbol();

            var internalHandler = new Action<SocketUpdateResponse<IEnumerable<CandleContainer>>>(data =>
            {
                foreach (var d in data.Data)
                {
                    d.Timestamp = DateTime.UtcNow;
                    d.Candle.Symbol = symbol.ToUpper(OkexHelpers.OkexCultureInfo);
                    onData(d.Candle);
                }
            });

            var request = new SocketRequest(SocketOperation.Subscribe, $"spot/candle{JsonConvert.SerializeObject(period, new SpotPeriodConverter(false))}s:{symbol}");
            return await Subscribe(request, null, false, internalHandler).ConfigureAwait(false);
        }


        /// <summary>
        /// Get the filled orders data
        /// </summary>
		/// <param name="symbol">Trading pair symbol</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public CallResult<UpdateSubscription> Spot_SubscribeToTrades(string symbol, Action<RestObjects.Spot.Trade> onData) => Spot_SubscribeToTrades_Async(symbol, onData).Result;
        /// <summary>
        /// Get the filled orders data
        /// </summary>
		/// <param name="symbol">Trading pair symbol</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public async Task<CallResult<UpdateSubscription>> Spot_SubscribeToTrades_Async(string symbol, Action<RestObjects.Spot.Trade> onData)
        {
            symbol = symbol.ValidateSymbol();

            var internalHandler = new Action<SocketUpdateResponse<IEnumerable<RestObjects.Spot.Trade>>>(data =>
            {
                foreach (var d in data.Data)
                {
                    onData(d);
                }
            });

            var request = new SocketRequest(SocketOperation.Subscribe, $"spot/trade:{symbol}");
            return await Subscribe(request, null, false, internalHandler).ConfigureAwait(false);
        }


        /// <summary>
        /// Depth-Five: Back to the previous five entries of depth data,This data is snapshot data per 100 milliseconds.For every 100 milliseconds, we will snapshot and push 5 entries of market depth data of the current order book.
        /// Depth-All: After subscription, 400 entries of market depth data of the order book will first be pushed. Subsequently every 100 milliseconds we will snapshot and push entries that have changed during this time.
        /// </summary>
		/// <param name="symbol">Trading pair symbol</param>
        /// <param name="depth">Order Book Depth</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public CallResult<UpdateSubscription> Spot_SubscribeToOrderBook(string symbol, SpotOrderBookDepth depth, Action<RestObjects.Spot.OrderBook> onData) => Spot_SubscribeToTrades_Async(symbol, depth, onData).Result;
        /// <summary>
        /// Depth-Five: Back to the previous five entries of depth data,This data is snapshot data per 100 milliseconds.For every 100 milliseconds, we will snapshot and push 5 entries of market depth data of the current order book.
        /// Depth-All: After subscription, 400 entries of market depth data of the order book will first be pushed. Subsequently every 100 milliseconds we will snapshot and push entries that have changed during this time.
        /// </summary>
		/// <param name="symbol">Trading pair symbol</param>
        /// <param name="depth">Order Book Depth</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public async Task<CallResult<UpdateSubscription>> Spot_SubscribeToTrades_Async(string symbol, SpotOrderBookDepth depth, Action<RestObjects.Spot.OrderBook> onData)
        {
            symbol = symbol.ValidateSymbol();

            var internalHandler = new Action<SocketOrderBookUpdate>(data =>
            {
                foreach (var d in data.Data)
                {
                    d.Symbol = symbol.ToUpper(OkexHelpers.OkexCultureInfo);
                    d.DataType = depth == SpotOrderBookDepth.Five ? SpotOrderBookDataType.DepthTop5 : data.DataType;
                    onData(d);
                }
            });

            var request = new SocketRequest(SocketOperation.Subscribe, $"spot/depth{(depth == SpotOrderBookDepth.Five ? "5" : "")}:{symbol}");
            return await Subscribe(request, null, false, internalHandler).ConfigureAwait(false);
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

            var isV2Response = (string)data["op"] == "req";
            if (isV2Response)
            {
                var hRequest = (SocketRequest)request;
                if ((string)data["cid"] != "hRequest.Id")
                    return false;

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
                if (request is SocketRequest socRequest)
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
            if (request is SocketRequest hRequest)
            {
                // Tickers Update
                if (hRequest.Operation == SocketOperation.Subscribe && message["table"] != null && ((string)message["table"]).StartsWith("spot/ticker"))
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
                if (hRequest.Operation == SocketOperation.Subscribe && message["table"] != null && ((string)message["table"]).StartsWith("spot/candle"))
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
                if (hRequest.Operation == SocketOperation.Subscribe && message["table"] != null && ((string)message["table"]).StartsWith("spot/trade"))
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
                if (hRequest.Operation == SocketOperation.Subscribe && message["table"] != null && ((string)message["table"]).StartsWith("spot/depth5"))
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
                if (hRequest.Operation == SocketOperation.Subscribe && message["table"] != null && ((string)message["table"]).StartsWith("spot/depth"))
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
            /*
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
