using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Newtonsoft.Json;
using Okex.Net.Converters;
using Okex.Net.Enums;
using Okex.Net.Helpers;
using Okex.Net.Interfaces;
using Okex.Net.RestObjects;
using Okex.Net.SocketObjects.Containers;
using Okex.Net.SocketObjects.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Okex.Net
{
    public partial class OkexSocketClient: IOkexSocketClientSpot
    {
        #region Spot Trading WS-API

        #region Public Unsigned Feeds
        /// <summary>
        /// Retrieve the latest price, best bid & offer and 24-hours trading volume of a single contract.
        /// </summary>
        /// <param name="symbol">Trading pair symbol</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public CallResult<UpdateSubscription> Spot_SubscribeToTicker(string symbol, Action<OkexSpotTicker> onData) => Spot_SubscribeToTicker_Async(symbol, onData).Result;
        /// <summary>
        /// Retrieve the latest price, best bid & offer and 24-hours trading volume of a single contract.
        /// </summary>
		/// <param name="symbol">Trading pair symbol</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public async Task<CallResult<UpdateSubscription>> Spot_SubscribeToTicker_Async(string symbol, Action<OkexSpotTicker> onData)
        {
            symbol = symbol.ValidateSymbol();

            var internalHandler = new Action<OkexSocketUpdateResponse<IEnumerable<OkexSpotTicker>>>(data =>
            {
                foreach (var d in data.Data)
                    onData(d);
            });

            var request = new OkexSocketRequest(OkexSocketOperation.Subscribe, $"spot/ticker:{symbol}");
            return await Subscribe(request, null, false, internalHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Subscribes tickers for multiple symbols
        /// </summary>
        /// <param name="symbols">Trading pair symbols Maximum Length: 100 symbols</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public CallResult<UpdateSubscription> Spot_SubscribeToTicker(IEnumerable<string> symbols, Action<OkexSpotTicker> onData) => Spot_SubscribeToTicker_Async(symbols, onData).Result;
        /// <summary>
        /// Subscribes tickers for multiple symbols
        /// </summary>
        /// <param name="symbols">Trading pair symbols. Maximum Length: 100 symbols</param>
        /// <param name="onData">The handler for updates</param>
        public async Task<CallResult<UpdateSubscription>> Spot_SubscribeToTicker_Async(IEnumerable<string> symbols, Action<OkexSpotTicker> onData)
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

            var internalHandler = new Action<OkexSocketUpdateResponse<IEnumerable<OkexSpotTicker>>>(data =>
            {
                foreach (var d in data.Data)
                    onData(d);
            });

            var tickerList = new List<string>();
            for (int i = 0; i < symbolList.Count; i++)
                tickerList.Add($"spot/ticker:{symbolList[i]}");

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
        public CallResult<UpdateSubscription> Spot_SubscribeToCandlesticks(string symbol, OkexSpotPeriod period, Action<OkexSpotCandle> onData) => Spot_SubscribeToCandlesticks_Async(symbol, period, onData).Result;
        /// <summary>
        /// Retrieve the candlestick data
        /// </summary>
		/// <param name="symbol">Trading pair symbol</param>
        /// <param name="period">The period of a single candlestick</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public async Task<CallResult<UpdateSubscription>> Spot_SubscribeToCandlesticks_Async(string symbol, OkexSpotPeriod period, Action<OkexSpotCandle> onData)
        {
            symbol = symbol.ValidateSymbol();

            var internalHandler = new Action<OkexSocketUpdateResponse<IEnumerable<OkexSpotCandleContainer>>>(data =>
            {
                foreach (var d in data.Data)
                {
                    d.Timestamp = DateTime.UtcNow;
                    d.Candle.Symbol = symbol.ToUpper(OkexGlobals.OkexCultureInfo);
                    onData(d.Candle);
                }
            });

            var period_s = JsonConvert.SerializeObject(period, new SpotPeriodConverter(false));
            var request = new OkexSocketRequest(OkexSocketOperation.Subscribe, $"spot/candle{period_s}s:{symbol}");
            return await Subscribe(request, null, false, internalHandler).ConfigureAwait(false);
        }

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
        /// Depth-TickByTick: The 400 entries of market depth data of the order book that return for the first time after subscription will be pushed; subsequently as long as there's any change of market depth data of the order book, the changes will be pushed tick by tick. Subsequently as long as there's any change of market depth data of the order book, the changes will be pushed tick by tick.
        /// </summary>
		/// <param name="symbol">Trading pair symbol</param>
        /// <param name="depth">Order Book Depth</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public CallResult<UpdateSubscription> Spot_SubscribeToOrderBook(string symbol, OkexOrderBookDepth depth, Action<OkexSpotOrderBook> onData) => Spot_SubscribeToTrades_Async(symbol, depth, onData).Result;
        /// <summary>
        /// Depth-Five: Back to the previous five entries of depth data,This data is snapshot data per 100 milliseconds.For every 100 milliseconds, we will snapshot and push 5 entries of market depth data of the current order book.
        /// Depth-All: After subscription, 400 entries of market depth data of the order book will first be pushed. Subsequently every 100 milliseconds we will snapshot and push entries that have changed during this time.
        /// Depth-TickByTick: The 400 entries of market depth data of the order book that return for the first time after subscription will be pushed; subsequently as long as there's any change of market depth data of the order book, the changes will be pushed tick by tick. Subsequently as long as there's any change of market depth data of the order book, the changes will be pushed tick by tick.
        /// </summary>
		/// <param name="symbol">Trading pair symbol</param>
        /// <param name="depth">Order Book Depth</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public async Task<CallResult<UpdateSubscription>> Spot_SubscribeToTrades_Async(string symbol, OkexOrderBookDepth depth, Action<OkexSpotOrderBook> onData)
        {
            symbol = symbol.ValidateSymbol();

            var internalHandler = new Action<OkexSpotOrderBookUpdate>(data =>
            {
                foreach (var d in data.Data)
                {
                    d.Symbol = symbol.ToUpper(OkexGlobals.OkexCultureInfo);
                    d.DataType = depth == OkexOrderBookDepth.Depth5 ? OkexOrderBookDataType.DepthTop5 : data.DataType;
                    onData(d);
                }
            });

            var channel = "depth";
            if (depth == OkexOrderBookDepth.Depth5) channel = "depth5";
            else if (depth == OkexOrderBookDepth.Depth400) channel = "depth";
            else if (depth == OkexOrderBookDepth.TickByTick) channel = "depth_l2_tbt";
            var request = new OkexSocketRequest(OkexSocketOperation.Subscribe, $"spot/{channel}:{symbol}");
            return await Subscribe(request, null, false, internalHandler).ConfigureAwait(false);
        }
        #endregion

        #region Private Signed Feeds

        /// <summary>
        /// Retrieve the user's spot account information (login authentication required).
        /// </summary>
        /// <param name="currency">Instrument Id</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public CallResult<UpdateSubscription> Spot_SubscribeToBalance(string currency, Action<OkexSpotBalance> onData) => Spot_SubscribeToBalance_Async(currency, onData).Result;
        /// <summary>
        /// Retrieve the user's spot account information (login authentication required).
        /// </summary>
        /// <param name="currency">Instrument Id</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
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

        /// <summary>
        /// Retrieve the user's transaction information (login authentication required).
        /// </summary>
        /// <param name="symbol">Instrument Id</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public CallResult<UpdateSubscription> Spot_SubscribeToOrders(string symbol, Action<OkexSpotOrderDetails> onData) => Spot_SubscribeToOrders_Async(symbol, onData).Result;
        /// <summary>
        /// Retrieve the user's transaction information (login authentication required).
        /// </summary>
        /// <param name="symbol">Instrument Id</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
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

        /// <summary>
        /// Users must login to obtain trading data.
        /// </summary>
        /// <param name="symbol">Instrument Id</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public CallResult<UpdateSubscription> Spot_SubscribeToAlgoOrders(string symbol, Action<OkexSpotAlgoOrder> onData) => Spot_SubscribeToAlgoOrders_Async(symbol, onData).Result;
        /// <summary>
        /// Users must login to obtain trading data.
        /// </summary>
        /// <param name="symbol">Instrument Id</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
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

        #endregion

        #endregion
    }
}
