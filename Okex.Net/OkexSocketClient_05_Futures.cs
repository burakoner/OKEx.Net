using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Okex.Net.Converters;
using Okex.Net.RestObjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Okex.Net.SocketObjects.Structure;
using Okex.Net.SocketObjects.Containers;
using Okex.Net.Helpers;
using Okex.Net.Enums;

namespace Okex.Net
{
    public partial class OkexSocketClient
    {
        #region Futures Trading WS-API

        #region Public Unsigned Feeds
        /// <summary>
        /// Complete List of Contracts
        /// When a new contract is available for trading after settlement, full contract data of all currency will be pushed in this channel ,no login required.
        /// </summary>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public CallResult<UpdateSubscription> Futures_SubscribeToContracts(Action<OkexFuturesContract> onData) => Futures_SubscribeToContracts_Async(onData).Result;
        /// <summary>
        /// Complete List of Contracts
        /// When a new contract is available for trading after settlement, full contract data of all currency will be pushed in this channel ,no login required.
        /// </summary>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public async Task<CallResult<UpdateSubscription>> Futures_SubscribeToContracts_Async(Action<OkexFuturesContract> onData)
        {
            var internalHandler = new Action<OkexSocketUpdateResponse<IEnumerable<IEnumerable<OkexFuturesContract>>>>(data =>
            {
                foreach (var d in data.Data)
                    foreach (var dd in d)
                        onData(dd);
            });

            var request = new OkexSocketRequest(OkexSocketOperation.Subscribe, $"futures/instruments");
            return await Subscribe(request, null, false, internalHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// To capture the latest traded price, best-bid price, best-ask price, 24-hour trading volume etc for the contract,data is pushed every 100ms.
        /// </summary>
        /// <param name="symbol">Trading contract symbol</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public CallResult<UpdateSubscription> Futures_SubscribeToTicker(string symbol, Action<OkexFuturesTicker> onData) => Futures_SubscribeToTicker_Async(symbol, onData).Result;
        /// <summary>
        /// To capture the latest traded price, best-bid price, best-ask price, 24-hour trading volume etc for the contract,data is pushed every 100ms.
        /// </summary>
        /// <param name="symbol">Trading contract symbol</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public async Task<CallResult<UpdateSubscription>> Futures_SubscribeToTicker_Async(string symbol, Action<OkexFuturesTicker> onData)
        {
            symbol = symbol.ValidateSymbol();

            var internalHandler = new Action<OkexSocketUpdateResponse<IEnumerable<OkexFuturesTicker>>>(data =>
            {
                foreach (var d in data.Data)
                    onData(d);
            });

            var request = new OkexSocketRequest(OkexSocketOperation.Subscribe, $"futures/ticker:{symbol}");
            return await Subscribe(request, null, false, internalHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// To capture the latest traded price, best-bid price, best-ask price, 24-hour trading volume etc for the contract,data is pushed every 100ms.
        /// </summary>
        /// <param name="symbols">Trading pair symbols Maximum Length: 100 symbols</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public CallResult<UpdateSubscription> Futures_SubscribeToTicker(IEnumerable<string> symbols, Action<OkexFuturesTicker> onData) => Futures_SubscribeToTicker_Async(symbols, onData).Result;
        /// <summary>
        /// To capture the latest traded price, best-bid price, best-ask price, 24-hour trading volume etc for the contract,data is pushed every 100ms.
        /// </summary>
        /// <param name="symbols">Trading pair symbols Maximum Length: 100 symbols</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public async Task<CallResult<UpdateSubscription>> Futures_SubscribeToTicker_Async(IEnumerable<string> symbols, Action<OkexFuturesTicker> onData)
        {
            // To List
            var symbolList = symbols.ToList();

            // Check Point
            if (symbolList.Count == 0)
                throw new ArgumentException("Symbols must contain 1 element at least");

            if (symbolList.Count > 100)
                throw new ArgumentException("Symbols can contain maximum 100 elements");

            for (int i = 0; i < symbolList.Count; i++)
                symbolList[i] = symbolList[i].ValidateSymbol();

            var internalHandler = new Action<OkexSocketUpdateResponse<IEnumerable<OkexFuturesTicker>>>(data =>
            {
                foreach (var d in data.Data)
                    onData(d);
            });

            var tickerList = new List<string>();
            for (int i = 0; i < symbolList.Count; i++)
                tickerList.Add($"futures/ticker:{symbolList[i]}");

            var request = new OkexSocketRequest(OkexSocketOperation.Subscribe, tickerList);
            return await Subscribe(request, null, false, internalHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve the candlestick data
        /// </summary>
		/// <param name="symbol">Trading pair symbol</param>
        /// <param name="period">The period of a single candlestick</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public CallResult<UpdateSubscription> Futures_SubscribeToCandlesticks(string symbol, OkexSpotPeriod period, Action<OkexFuturesCandle> onData) => Futures_SubscribeToCandlesticks_Async(symbol, period, onData).Result;
        /// <summary>
        /// Retrieve the candlestick data
        /// </summary>
		/// <param name="symbol">Trading pair symbol</param>
        /// <param name="period">The period of a single candlestick</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public async Task<CallResult<UpdateSubscription>> Futures_SubscribeToCandlesticks_Async(string symbol, OkexSpotPeriod period, Action<OkexFuturesCandle> onData)
        {
            symbol = symbol.ValidateSymbol();

            var internalHandler = new Action<OkexSocketUpdateResponse<IEnumerable<OkexFuturesCandleContainer>>>(data =>
            {
                foreach (var d in data.Data)
                {
                    d.Timestamp = DateTime.UtcNow;
                    d.Candle.Symbol = symbol.ToUpper(OkexGlobals.OkexCultureInfo);
                    onData(d.Candle);
                }
            });

            var period_s = JsonConvert.SerializeObject(period, new SpotPeriodConverter(false));
            var request = new OkexSocketRequest(OkexSocketOperation.Subscribe, $"futures/candle{period_s}s:{symbol}");
            return await Subscribe(request, null, false, internalHandler).ConfigureAwait(false);
        }










        /*
        /// <summary>
        /// Get the filled orders data
        /// </summary>
		/// <param name="symbol">Trading pair symbol</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public CallResult<UpdateSubscription> Spot_SubscribeToTrades(string symbol, Action<OkexSpotTrade> onData) => Spot_SubscribeToTrades_Async(symbol, onData).Result;
        /// <summary>
        /// Get the filled orders data
        /// </summary>
		/// <param name="symbol">Trading pair symbol</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public async Task<CallResult<UpdateSubscription>> Spot_SubscribeToTrades_Async(string symbol, Action<OkexSpotTrade> onData)
        {
            symbol = symbol.ValidateSymbol();

            var internalHandler = new Action<OkexSocketUpdateResponse<IEnumerable<OkexSpotTrade>>>(data =>
            {
                foreach (var d in data.Data)
                    onData(d);
            });

            var request = new OkexSocketRequest(OkexSocketOperation.Subscribe, $"spot/trade:{symbol}");
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
        public CallResult<UpdateSubscription> Spot_SubscribeToOrderBook(string symbol, OkexSpotOrderBookDepth depth, Action<OkexSpotOrderBook> onData) => Spot_SubscribeToTrades_Async(symbol, depth, onData).Result;
        /// <summary>
        /// Depth-Five: Back to the previous five entries of depth data,This data is snapshot data per 100 milliseconds.For every 100 milliseconds, we will snapshot and push 5 entries of market depth data of the current order book.
        /// Depth-All: After subscription, 400 entries of market depth data of the order book will first be pushed. Subsequently every 100 milliseconds we will snapshot and push entries that have changed during this time.
        /// </summary>
		/// <param name="symbol">Trading pair symbol</param>
        /// <param name="depth">Order Book Depth</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public async Task<CallResult<UpdateSubscription>> Spot_SubscribeToTrades_Async(string symbol, OkexSpotOrderBookDepth depth, Action<OkexSpotOrderBook> onData)
        {
            symbol = symbol.ValidateSymbol();

            var internalHandler = new Action<OkexSocketOrderBookUpdate>(data =>
            {
                foreach (var d in data.Data)
                {
                    d.Symbol = symbol.ToUpper(OkexGlobals.OkexCultureInfo);
                    d.DataType = depth == OkexSpotOrderBookDepth.Depth5 ? OkexSpotOrderBookDataType.DepthTop5 : data.DataType;
                    onData(d);
                }
            });

            var channel = "depth";
            if (depth == OkexSpotOrderBookDepth.Depth5) channel = "depth5";
            else if (depth == OkexSpotOrderBookDepth.Depth400) channel = "depth";
            else if (depth == OkexSpotOrderBookDepth.TickByTick) channel = "depth_l2_tbt";
            var request = new OkexSocketRequest(OkexSocketOperation.Subscribe, $"spot/{channel}:{symbol}");
            return await Subscribe(request, null, false, internalHandler).ConfigureAwait(false);
        }
        */

        #endregion

        #region Private Signed Feeds

        /*
        
        public CallResult<UpdateSubscription> Spot_SubscribeToBalance(string currency, Action<OkexSpotBalance> onData) => Spot_SubscribeToBalance_Async(currency, onData).Result;
        public async Task<CallResult<UpdateSubscription>> Spot_SubscribeToBalance_Async(string currency, Action<OkexSpotBalance> onData)
        {
            currency = currency.ValidateCurrency();

            var internalHandler = new Action<OkexSocketUpdateResponse<IEnumerable<OkexSpotBalance>>>(data =>
            {
                foreach (var d in data.Data)
                    onData(d);
            });

            var request = new OkexSocketRequest(OkexSocketOperation.Subscribe, $"spot/account:{currency}");
            return await Subscribe(request, null, true, internalHandler).ConfigureAwait(false);
        }

        public CallResult<UpdateSubscription> Spot_SubscribeToOrders(string symbol, Action<OkexSpotOrderDetails> onData) => Spot_SubscribeToOrders_Async(symbol, onData).Result;
        public async Task<CallResult<UpdateSubscription>> Spot_SubscribeToOrders_Async(string symbol, Action<OkexSpotOrderDetails> onData)
        {
            symbol = symbol.ValidateSymbol();

            var internalHandler = new Action<OkexSocketUpdateResponse<IEnumerable<OkexSpotOrderDetails>>>(data =>
            {
                foreach (var d in data.Data)
                    onData(d);
            });

            var request = new OkexSocketRequest(OkexSocketOperation.Subscribe, $"spot/order:{symbol}");
            return await Subscribe(request, null, true, internalHandler).ConfigureAwait(false);
        }

        public CallResult<UpdateSubscription> Spot_SubscribeToAlgoOrders(string symbol, Action<OkexSpotAlgoOrder> onData) => Spot_SubscribeToAlgoOrders_Async(symbol, onData).Result;
        public async Task<CallResult<UpdateSubscription>> Spot_SubscribeToAlgoOrders_Async(string symbol, Action<OkexSpotAlgoOrder> onData)
        {
            symbol = symbol.ValidateSymbol();

            var internalHandler = new Action<OkexSocketUpdateResponse<IEnumerable<OkexSpotAlgoOrder>>>(data =>
            {
                foreach (var d in data.Data)
                    onData(d);
            });

            var request = new OkexSocketRequest(OkexSocketOperation.Subscribe, $"spot/order_algo:{symbol}");
            return await Subscribe(request, null, true, internalHandler).ConfigureAwait(false);
        }

        */
        #endregion

        #endregion
    }
}
