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
    public partial class OkexSocketClient : IOkexSocketClientFutures
    {
        #region Futures Trading WS-API

        #region Public Unsigned Feeds

        /// <summary>
        /// Complete List of Contracts
        /// When a new contract is available for trading after settlement, full contract data of all currency will be pushed in this channel ,no login required.
        /// </summary>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public virtual CallResult<UpdateSubscription> Futures_SubscribeToContracts(Action<OkexFuturesContract> onData) => Futures_SubscribeToContracts_Async(onData).Result;
        /// <summary>
        /// Complete List of Contracts
        /// When a new contract is available for trading after settlement, full contract data of all currency will be pushed in this channel ,no login required.
        /// </summary>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public virtual async Task<CallResult<UpdateSubscription>> Futures_SubscribeToContracts_Async(Action<OkexFuturesContract> onData)
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
        public virtual CallResult<UpdateSubscription> Futures_SubscribeToTicker(string symbol, Action<OkexFuturesTicker> onData) => Futures_SubscribeToTicker_Async(symbol, onData).Result;
        /// <summary>
        /// To capture the latest traded price, best-bid price, best-ask price, 24-hour trading volume etc for the contract,data is pushed every 100ms.
        /// </summary>
        /// <param name="symbol">Trading contract symbol</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public virtual async Task<CallResult<UpdateSubscription>> Futures_SubscribeToTicker_Async(string symbol, Action<OkexFuturesTicker> onData)
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
        public virtual CallResult<UpdateSubscription> Futures_SubscribeToTicker(IEnumerable<string> symbols, Action<OkexFuturesTicker> onData) => Futures_SubscribeToTicker_Async(symbols, onData).Result;
        /// <summary>
        /// To capture the latest traded price, best-bid price, best-ask price, 24-hour trading volume etc for the contract,data is pushed every 100ms.
        /// </summary>
        /// <param name="symbols">Trading pair symbols Maximum Length: 100 symbols</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public virtual async Task<CallResult<UpdateSubscription>> Futures_SubscribeToTicker_Async(IEnumerable<string> symbols, Action<OkexFuturesTicker> onData)
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
        public virtual CallResult<UpdateSubscription> Futures_SubscribeToCandlesticks(string symbol, OkexSpotPeriod period, Action<OkexFuturesCandle> onData) => Futures_SubscribeToCandlesticks_Async(symbol, period, onData).Result;
        /// <summary>
        /// Retrieve the candlestick data
        /// </summary>
		/// <param name="symbol">Trading pair symbol</param>
        /// <param name="period">The period of a single candlestick</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public virtual async Task<CallResult<UpdateSubscription>> Futures_SubscribeToCandlesticks_Async(string symbol, OkexSpotPeriod period, Action<OkexFuturesCandle> onData)
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

        /// <summary>
        /// Get the filled orders data
        /// </summary>
		/// <param name="symbol">Trading pair symbol</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public virtual CallResult<UpdateSubscription> Futures_SubscribeToTrades(string symbol, Action<OkexFuturesTrade> onData) => Futures_SubscribeToTrades_Async(symbol, onData).Result;
        /// <summary>
        /// Get the filled orders data
        /// </summary>
		/// <param name="symbol">Trading pair symbol</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public virtual async Task<CallResult<UpdateSubscription>> Futures_SubscribeToTrades_Async(string symbol, Action<OkexFuturesTrade> onData)
        {
            symbol = symbol.ValidateSymbol();

            var internalHandler = new Action<OkexSocketUpdateResponse<IEnumerable<OkexFuturesTrade>>>(data =>
            {
                foreach (var d in data.Data)
                    onData(d);
            });

            var request = new OkexSocketRequest(OkexSocketOperation.Subscribe, $"futures/trade:{symbol}");
            return await Subscribe(request, null, false, internalHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// The maximum buying price and the minimum selling price of the contract.When the limit price is changed, data is pushed once every 5 seconds, and when the limit price is not changed, data is not pushed.
        /// </summary>
		/// <param name="symbol">Trading pair symbol</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public virtual CallResult<UpdateSubscription> Futures_SubscribeToPriceRange(string symbol, Action<OkexFuturesPriceRange> onData) => Futures_SubscribeToPriceRange_Async(symbol, onData).Result;
        /// <summary>
        /// The maximum buying price and the minimum selling price of the contract.When the limit price is changed, data is pushed once every 5 seconds, and when the limit price is not changed, data is not pushed.
        /// </summary>
		/// <param name="symbol">Trading pair symbol</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public virtual async Task<CallResult<UpdateSubscription>> Futures_SubscribeToPriceRange_Async(string symbol, Action<OkexFuturesPriceRange> onData)
        {
            symbol = symbol.ValidateSymbol();

            var internalHandler = new Action<OkexSocketUpdateResponse<IEnumerable<OkexFuturesPriceRange>>>(data =>
            {
                foreach (var d in data.Data)
                    onData(d);
            });

            var request = new OkexSocketRequest(OkexSocketOperation.Subscribe, $"futures/price_range:{symbol}");
            return await Subscribe(request, null, false, internalHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Get estimated price，the estimated delivery price will be pushed one hour before the delivery, and it will be pushed if there is a change.
        /// </summary>
		/// <param name="symbol">Trading pair symbol</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public virtual CallResult<UpdateSubscription> Futures_SubscribeToEstimatedPrice(string symbol, Action<OkexFuturesEstimatedPrice> onData) => Futures_SubscribeToEstimatedPrice_Async(symbol, onData).Result;
        /// <summary>
        /// Get estimated price，the estimated delivery price will be pushed one hour before the delivery, and it will be pushed if there is a change.
        /// </summary>
		/// <param name="symbol">Trading pair symbol</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public virtual async Task<CallResult<UpdateSubscription>> Futures_SubscribeToEstimatedPrice_Async(string symbol, Action<OkexFuturesEstimatedPrice> onData)
        {
            symbol = symbol.ValidateSymbol();

            var internalHandler = new Action<OkexSocketUpdateResponse<IEnumerable<OkexFuturesEstimatedPrice>>>(data =>
            {
                foreach (var d in data.Data)
                    onData(d);
            });

            var request = new OkexSocketRequest(OkexSocketOperation.Subscribe, $"futures/estimated_price:{symbol}");
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
        public virtual CallResult<UpdateSubscription> Futures_SubscribeToOrderBook(string symbol, OkexOrderBookDepth depth, Action<OkexFuturesOrderBook> onData) => Futures_SubscribeToOrderBook_Async(symbol, depth, onData).Result;
        /// <summary>
        /// Depth-Five: The latest 5 entries of the market depth data is snapshooted and pushed every 100 milliseconds.
        /// Depth-All: After subscription, 400 entries of market depth data of the order book will first be pushed. Subsequently every 100 milliseconds we will snapshot and push entries that have changed during this time.
        /// Depth-TickByTick: The 400 entries of market depth data of the order book that return for the first time after subscription will be pushed; subsequently as long as there's any change of market depth data of the order book, the changes will be pushed tick by tick. Subsequently as long as there's any change of market depth data of the order book, the changes will be pushed tick by tick.
        /// </summary>
		/// <param name="symbol">Trading pair symbol</param>
        /// <param name="depth">Order Book Depth</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public virtual async Task<CallResult<UpdateSubscription>> Futures_SubscribeToOrderBook_Async(string symbol, OkexOrderBookDepth depth, Action<OkexFuturesOrderBook> onData)
        {
            symbol = symbol.ValidateSymbol();

            var internalHandler = new Action<OkexFuturesOrderBookUpdate>(data =>
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
            var request = new OkexSocketRequest(OkexSocketOperation.Subscribe, $"futures/{channel}:{symbol}");
            return await Subscribe(request, null, false, internalHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Get the mark price，when the mark price changes, data is pushed once every 200ms, and when the mark price is not changed, data is pushed once every 10s.
        /// </summary>
		/// <param name="symbol">Trading pair symbol</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public virtual CallResult<UpdateSubscription> Futures_SubscribeToMarkPrice(string symbol, Action<OkexFuturesMarkPrice> onData) => Futures_SubscribeToMarkPrice_Async(symbol, onData).Result;
        /// <summary>
        /// Get the mark price，when the mark price changes, data is pushed once every 200ms, and when the mark price is not changed, data is pushed once every 10s.
        /// </summary>
		/// <param name="symbol">Trading pair symbol</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public virtual async Task<CallResult<UpdateSubscription>> Futures_SubscribeToMarkPrice_Async(string symbol, Action<OkexFuturesMarkPrice> onData)
        {
            symbol = symbol.ValidateSymbol();

            var internalHandler = new Action<OkexSocketUpdateResponse<IEnumerable<OkexFuturesMarkPrice>>>(data =>
            {
                foreach (var d in data.Data)
                    onData(d);
            });

            var request = new OkexSocketRequest(OkexSocketOperation.Subscribe, $"futures/mark_price:{symbol}");
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
        public virtual CallResult<UpdateSubscription> Futures_SubscribeToPositions(string symbol, Action<OkexFuturesPosition> onData) => Futures_SubscribeToPositions_Async(symbol, onData).Result;
        /// <summary>
        /// Get the information of holding positions of a contract. require login
        /// </summary>
        /// <param name="symbol">Instrument Id</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public virtual async Task<CallResult<UpdateSubscription>> Futures_SubscribeToPositions_Async(string symbol, Action<OkexFuturesPosition> onData)
        {
            var internalHandler = new Action<OkexSocketUpdateResponse<IEnumerable<OkexFuturesPosition>>>(data =>
            {
                foreach (var d in data.Data)
                    onData(d);
            });

            var request = new OkexSocketRequest(OkexSocketOperation.Subscribe, $"futures/position:{symbol}");
            return await Subscribe(request, null, true, internalHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Get the user's account information , require login.
        /// </summary>
        /// <param name="symbol">Instrument Id</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public virtual CallResult<UpdateSubscription> Futures_SubscribeToBalance(string symbol, Action<OkexFuturesBalance> onData) => Futures_SubscribeToBalance_Async(symbol, onData).Result;
        /// <summary>
        /// Get the user's account information , require login.
        /// </summary>
        /// <param name="symbol">Instrument Id</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public virtual async Task<CallResult<UpdateSubscription>> Futures_SubscribeToBalance_Async(string symbol, Action<OkexFuturesBalance> onData)
        {
            var internalHandler = new Action<OkexSocketUpdateResponse<IEnumerable<OkexFuturesBalances>>>(data =>
            {
                foreach (var d in data.Data)
                    foreach (var dd in d.Balances.Values)
                        onData(dd);
            });

            var request = new OkexSocketRequest(OkexSocketOperation.Subscribe, $"futures/account:{symbol}");
            return await Subscribe(request, null, true, internalHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Get user's order information , require login .
        /// </summary>
        /// <param name="symbol">Instrument Id</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public virtual CallResult<UpdateSubscription> Futures_SubscribeToOrders(string symbol, Action<OkexFuturesOrder> onData) => Futures_SubscribeToOrders_Async(symbol, onData).Result;
        /// <summary>
        /// Get user's order information , require login .
        /// </summary>
        /// <param name="symbol">Instrument Id</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public virtual async Task<CallResult<UpdateSubscription>> Futures_SubscribeToOrders_Async(string symbol, Action<OkexFuturesOrder> onData)
        {
            symbol = symbol.ValidateSymbol();

            var internalHandler = new Action<OkexSocketUpdateResponse<IEnumerable<OkexFuturesOrder>>>(data =>
            {
                foreach (var d in data.Data)
                    onData(d);
            });

            var request = new OkexSocketRequest(OkexSocketOperation.Subscribe, $"futures/order:{symbol}");
            return await Subscribe(request, null, true, internalHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Users must login to obtain trading data.
        /// </summary>
        /// <param name="symbol">Instrument Id</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public virtual CallResult<UpdateSubscription> Futures_SubscribeToAlgoOrders(string symbol, Action<OkexFuturesAlgoOrder> onData) => Futures_SubscribeToAlgoOrders_Async(symbol, onData).Result;
        /// <summary>
        /// Users must login to obtain trading data.
        /// </summary>
        /// <param name="symbol">Instrument Id</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public virtual async Task<CallResult<UpdateSubscription>> Futures_SubscribeToAlgoOrders_Async(string symbol, Action<OkexFuturesAlgoOrder> onData)
        {
            symbol = symbol.ValidateSymbol();

            var internalHandler = new Action<OkexSocketUpdateResponse<IEnumerable<OkexFuturesAlgoOrder>>>(data =>
            {
                foreach (var d in data.Data)
                    onData(d);
            });

            var request = new OkexSocketRequest(OkexSocketOperation.Subscribe, $"futures/order_algo:{symbol}");
            return await Subscribe(request, null, true, internalHandler).ConfigureAwait(false);
        }

        #endregion

        #endregion
    }
}
