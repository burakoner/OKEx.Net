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
    public partial class OkexSocketClient : IOkexSocketClientSwap
    {
        #region Perpetual Swap Trading WS-API

        #region Public Unsigned Feeds

        /// <summary>
        /// To capture the latest traded price, best-bid price, best-ask price, and 24-hour trading volume of all perpetual swap contracts on the platform,it will be pushed when there is transaction data.
        /// </summary>
        /// <param name="symbol">Trading contract symbol</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public virtual CallResult<UpdateSubscription> Swap_SubscribeToTicker(string symbol, Action<OkexSwapTicker> onData) => Swap_SubscribeToTicker_Async(symbol, onData).Result;
        /// <summary>
        /// To capture the latest traded price, best-bid price, best-ask price, and 24-hour trading volume of all perpetual swap contracts on the platform,it will be pushed when there is transaction data.
        /// </summary>
        /// <param name="symbol">Trading contract symbol</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public virtual async Task<CallResult<UpdateSubscription>> Swap_SubscribeToTicker_Async(string symbol, Action<OkexSwapTicker> onData)
        {
            symbol = symbol.ValidateSymbol();

            var internalHandler = new Action<OkexSocketUpdateResponse<IEnumerable<OkexSwapTicker>>>(data =>
            {
                foreach (var d in data.Data)
                    onData(d);
            });

            var request = new OkexSocketRequest(OkexSocketOperation.Subscribe, $"swap/ticker:{symbol}");
            return await Subscribe(request, null, false, internalHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// To capture the latest traded price, best-bid price, best-ask price, and 24-hour trading volume of all perpetual swap contracts on the platform,it will be pushed when there is transaction data.
        /// </summary>
        /// <param name="symbols">Trading pair symbols Maximum Length: 100 symbols</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public virtual CallResult<UpdateSubscription> Swap_SubscribeToTicker(IEnumerable<string> symbols, Action<OkexSwapTicker> onData) => Swap_SubscribeToTicker_Async(symbols, onData).Result;
        /// <summary>
        /// To capture the latest traded price, best-bid price, best-ask price, and 24-hour trading volume of all perpetual swap contracts on the platform,it will be pushed when there is transaction data.
        /// </summary>
        /// <param name="symbols">Trading pair symbols Maximum Length: 100 symbols</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public virtual async Task<CallResult<UpdateSubscription>> Swap_SubscribeToTicker_Async(IEnumerable<string> symbols, Action<OkexSwapTicker> onData)
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

            var internalHandler = new Action<OkexSocketUpdateResponse<IEnumerable<OkexSwapTicker>>>(data =>
            {
                foreach (var d in data.Data)
                    onData(d);
            });

            var tickerList = new List<string>();
            for (int i = 0; i < symbolList.Count; i++)
                tickerList.Add($"swap/ticker:{symbolList[i]}");

            var request = new OkexSocketRequest(OkexSocketOperation.Subscribe, tickerList);
            return await Subscribe(request, null, false, internalHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Kline information for swap business,data is pushed every 500ms.
        /// </summary>
		/// <param name="symbol">Trading pair symbol</param>
        /// <param name="period">The period of a single candlestick</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public virtual CallResult<UpdateSubscription> Swap_SubscribeToCandlesticks(string symbol, OkexSpotPeriod period, Action<OkexSwapCandle> onData) => Swap_SubscribeToCandlesticks_Async(symbol, period, onData).Result;
        /// <summary>
        /// Kline information for swap business,data is pushed every 500ms.
        /// </summary>
		/// <param name="symbol">Trading pair symbol</param>
        /// <param name="period">The period of a single candlestick</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public virtual async Task<CallResult<UpdateSubscription>> Swap_SubscribeToCandlesticks_Async(string symbol, OkexSpotPeriod period, Action<OkexSwapCandle> onData)
        {
            symbol = symbol.ValidateSymbol();

            var internalHandler = new Action<OkexSocketUpdateResponse<IEnumerable<OkexSwapCandleContainer>>>(data =>
            {
                foreach (var d in data.Data)
                {
                    d.Timestamp = DateTime.UtcNow;
                    d.Candle.Symbol = symbol.ToUpper(OkexGlobals.OkexCultureInfo);
                    onData(d.Candle);
                }
            });

            var period_s = JsonConvert.SerializeObject(period, new SpotPeriodConverter(false));
            var request = new OkexSocketRequest(OkexSocketOperation.Subscribe, $"swap/candle{period_s}s:{symbol}");
            return await Subscribe(request, null, false, internalHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Get the filled orders data
        /// </summary>
		/// <param name="symbol">Trading pair symbol</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public virtual CallResult<UpdateSubscription> Swap_SubscribeToTrades(string symbol, Action<OkexSwapTrade> onData) => Swap_SubscribeToTrades_Async(symbol, onData).Result;
        /// <summary>
        /// Get the filled orders data
        /// </summary>
		/// <param name="symbol">Trading pair symbol</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public virtual async Task<CallResult<UpdateSubscription>> Swap_SubscribeToTrades_Async(string symbol, Action<OkexSwapTrade> onData)
        {
            symbol = symbol.ValidateSymbol();

            var internalHandler = new Action<OkexSocketUpdateResponse<IEnumerable<OkexSwapTrade>>>(data =>
            {
                foreach (var d in data.Data)
                    onData(d);
            });

            var request = new OkexSocketRequest(OkexSocketOperation.Subscribe, $"swap/trade:{symbol}");
            return await Subscribe(request, null, false, internalHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Get the funding rate information，push data once a minute.
        /// </summary>
        /// <param name="symbol">Trading pair symbol</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public virtual CallResult<UpdateSubscription> Swap_SubscribeToFundingRate(string symbol, Action<OkexSwapFundingRate> onData) => Swap_SubscribeToFundingRate_Async(symbol, onData).Result;
        /// <summary>
        /// Get the funding rate information，push data once a minute.
        /// </summary>
		/// <param name="symbol">Trading pair symbol</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public virtual async Task<CallResult<UpdateSubscription>> Swap_SubscribeToFundingRate_Async(string symbol, Action<OkexSwapFundingRate> onData)
        {
            symbol = symbol.ValidateSymbol();

            var internalHandler = new Action<OkexSocketUpdateResponse<IEnumerable<OkexSwapFundingRate>>>(data =>
            {
                foreach (var d in data.Data)
                    onData(d);
            });

            var request = new OkexSocketRequest(OkexSocketOperation.Subscribe, $"swap/funding_rate:{symbol}");
            return await Subscribe(request, null, false, internalHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// The maximum buying price and the minimum selling price of the contract.Push data once every five seconds.
        /// </summary>
		/// <param name="symbol">Trading pair symbol</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public virtual CallResult<UpdateSubscription> Swap_SubscribeToPriceRange(string symbol, Action<OkexSwapPriceRange> onData) => Swap_SubscribeToPriceRange_Async(symbol, onData).Result;
        /// <summary>
        /// The maximum buying price and the minimum selling price of the contract.Push data once every five seconds.
        /// </summary>
		/// <param name="symbol">Trading pair symbol</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public virtual async Task<CallResult<UpdateSubscription>> Swap_SubscribeToPriceRange_Async(string symbol, Action<OkexSwapPriceRange> onData)
        {
            symbol = symbol.ValidateSymbol();

            var internalHandler = new Action<OkexSocketUpdateResponse<IEnumerable<OkexSwapPriceRange>>>(data =>
            {
                foreach (var d in data.Data)
                    onData(d);
            });

            var request = new OkexSocketRequest(OkexSocketOperation.Subscribe, $"swap/price_range:{symbol}");
            return await Subscribe(request, null, false, internalHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Depth-Five: The latest 5 entries of the market depth data is snapshooted and pushed every 100 milliseconds.
        /// Depth-All: After subscription, 400 entries of market depth data of the order book will first be pushed. Subsequently every 100 milliseconds we will snapshot and push entries that have changed during this time.
        /// Depth-TickByTick: The 400 entries of market depth data of the order book that return for the first time after subscription will be pushed; subsequently as long as there's any change of market depth data of the order book, the changes will be pushed tick by tick. Subsequently as long as there's any change of market depth data of the order book, the changes will be pushed tick by tick.
        /// </summary>
		/// <param name="symbol">Trading pair symbol</param>
        /// <param name="depth">Order Book Depth</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public virtual CallResult<UpdateSubscription> Swap_SubscribeToOrderBook(string symbol, OkexOrderBookDepth depth, Action<OkexSwapOrderBook> onData) => Swap_SubscribeToOrderBook_Async(symbol, depth, onData).Result;
        /// <summary>
        /// Depth-Five: The latest 5 entries of the market depth data is snapshooted and pushed every 100 milliseconds.
        /// Depth-All: After subscription, 400 entries of market depth data of the order book will first be pushed. Subsequently every 100 milliseconds we will snapshot and push entries that have changed during this time.
        /// Depth-TickByTick: The 400 entries of market depth data of the order book that return for the first time after subscription will be pushed; subsequently as long as there's any change of market depth data of the order book, the changes will be pushed tick by tick. Subsequently as long as there's any change of market depth data of the order book, the changes will be pushed tick by tick.
        /// </summary>
        /// <param name="symbol">Trading pair symbol</param>
        /// <param name="depth">Order Book Depth</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public virtual async Task<CallResult<UpdateSubscription>> Swap_SubscribeToOrderBook_Async(string symbol, OkexOrderBookDepth depth, Action<OkexSwapOrderBook> onData)
        {
            symbol = symbol.ValidateSymbol();

            var internalHandler = new Action<OkexSwapOrderBookUpdate>(data =>
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
            var request = new OkexSocketRequest(OkexSocketOperation.Subscribe, $"swap/{channel}:{symbol}");
            return await Subscribe(request, null, false, internalHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// The maximum buying price and the minimum selling price of the contract.Push data once every five seconds.
        /// </summary>
		/// <param name="symbol">Trading pair symbol</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public virtual CallResult<UpdateSubscription> Swap_SubscribeToMarkPrice(string symbol, Action<OkexSwapMarkPrice> onData) => Swap_SubscribeToMarkPrice_Async(symbol, onData).Result;
        /// <summary>
        /// The maximum buying price and the minimum selling price of the contract.Push data once every five seconds.
        /// </summary>
		/// <param name="symbol">Trading pair symbol</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public virtual async Task<CallResult<UpdateSubscription>> Swap_SubscribeToMarkPrice_Async(string symbol, Action<OkexSwapMarkPrice> onData)
        {
            symbol = symbol.ValidateSymbol();

            var internalHandler = new Action<OkexSocketUpdateResponse<IEnumerable<OkexSwapMarkPrice>>>(data =>
            {
                foreach (var d in data.Data)
                    onData(d);
            });

            var request = new OkexSocketRequest(OkexSocketOperation.Subscribe, $"swap/mark_price:{symbol}");
            return await Subscribe(request, null, false, internalHandler).ConfigureAwait(false);
        }

        #endregion

        #region Private Signed Feeds

        /// <summary>
        /// Get the information of holding positions of a contract. require login
        /// </summary>
        /// <param name="symbol">Instrument Id</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public virtual CallResult<UpdateSubscription> Swap_SubscribeToPositions(string symbol, Action<OkexSwapPosition> onData) => Swap_SubscribeToPositions_Async(symbol, onData).Result;
        /// <summary>
        /// Get the information of holding positions of a contract. require login
        /// </summary>
        /// <param name="symbol">Instrument Id</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public virtual async Task<CallResult<UpdateSubscription>> Swap_SubscribeToPositions_Async(string symbol, Action<OkexSwapPosition> onData)
        {
            var internalHandler = new Action<OkexSocketUpdateResponse<IEnumerable<OkexSwapPosition>>>(data =>
            {
                foreach (var d in data.Data)
                    onData(d);
            });

            var request = new OkexSocketRequest(OkexSocketOperation.Subscribe, $"swap/position:{symbol}");
            return await Subscribe(request, null, true, internalHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Get the user's account information , require login.
        /// </summary>
        /// <param name="symbol">Instrument Id</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public virtual CallResult<UpdateSubscription> Swap_SubscribeToBalance(string symbol, Action<OkexSwapBalanceExt> onData) => Swap_SubscribeToBalance_Async(symbol, onData).Result;
        /// <summary>
        /// Get the user's account information , require login.
        /// </summary>
        /// <param name="symbol">Instrument Id</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public virtual async Task<CallResult<UpdateSubscription>> Swap_SubscribeToBalance_Async(string symbol, Action<OkexSwapBalanceExt> onData)
        {
            var internalHandler = new Action<OkexSocketUpdateResponse<IEnumerable<OkexSwapBalanceExt>>>(data =>
            {
                foreach (var d in data.Data)
                    onData(d);
            });

            var request = new OkexSocketRequest(OkexSocketOperation.Subscribe, $"swap/account:{symbol}");
            return await Subscribe(request, null, true, internalHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Get user's order information , require login .
        /// </summary>
        /// <param name="symbol">Instrument Id</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public virtual CallResult<UpdateSubscription> Swap_SubscribeToOrders(string symbol, Action<OkexSwapOrder> onData) => Swap_SubscribeToOrders_Async(symbol, onData).Result;
        /// <summary>
        /// Get user's order information , require login .
        /// </summary>
        /// <param name="symbol">Instrument Id</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public virtual async Task<CallResult<UpdateSubscription>> Swap_SubscribeToOrders_Async(string symbol, Action<OkexSwapOrder> onData)
        {
            symbol = symbol.ValidateSymbol();

            var internalHandler = new Action<OkexSocketUpdateResponse<IEnumerable<OkexSwapOrder>>>(data =>
            {
                foreach (var d in data.Data)
                    onData(d);
            });

            var request = new OkexSocketRequest(OkexSocketOperation.Subscribe, $"swap/order:{symbol}");
            return await Subscribe(request, null, true, internalHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Users must login to obtain trading data.
        /// </summary>
        /// <param name="symbol">Instrument Id</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public virtual CallResult<UpdateSubscription> Swap_SubscribeToAlgoOrders(string symbol, Action<OkexSwapAlgoOrder> onData) => Swap_SubscribeToAlgoOrders_Async(symbol, onData).Result;
        /// <summary>
        /// Users must login to obtain trading data.
        /// </summary>
        /// <param name="symbol">Instrument Id</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public virtual async Task<CallResult<UpdateSubscription>> Swap_SubscribeToAlgoOrders_Async(string symbol, Action<OkexSwapAlgoOrder> onData)
        {
            symbol = symbol.ValidateSymbol();

            var internalHandler = new Action<OkexSocketUpdateResponse<IEnumerable<OkexSwapAlgoOrder>>>(data =>
            {
                foreach (var d in data.Data)
                    onData(d);
            });

            var request = new OkexSocketRequest(OkexSocketOperation.Subscribe, $"swap/order_algo:{symbol}");
            return await Subscribe(request, null, true, internalHandler).ConfigureAwait(false);
        }

        #endregion

        #endregion
    }
}
