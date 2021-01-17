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
    public partial class OkexSocketClient : IOkexSocketClientOptions
    {
        #region Options Trading WS-API

        #region Public Unsigned Feeds

        /// <summary>
        /// Retrieve list of instruments with open contracts for options trading，Does not require login.
        /// When a new contract is available or the sate of a contract is updated, full contract data of the underlying will be pushed.
        /// </summary>
        /// <param name="underlying">The underlying index that the contract is based upon, e.g. BTC-USD.</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public virtual CallResult<UpdateSubscription> Options_SubscribeToContracts(string underlying, Action<OkexOptionsInstrument> onData) => Options_SubscribeToContracts_Async(underlying, onData).Result;
        /// <summary>
        /// Retrieve list of instruments with open contracts for options trading，Does not require login.
        /// When a new contract is available or the sate of a contract is updated, full contract data of the underlying will be pushed.
        /// </summary>
        /// <param name="underlying">The underlying index that the contract is based upon, e.g. BTC-USD.</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public virtual async Task<CallResult<UpdateSubscription>> Options_SubscribeToContracts_Async(string underlying, Action<OkexOptionsInstrument> onData)
        {
            underlying = underlying.ValidateSymbol();

            var internalHandler = new Action<OkexSocketUpdateResponse<IEnumerable<OkexOptionsInstrument>>>(data =>
            {
                foreach (var d in data.Data)
                    onData(d);
            });

            var request = new OkexSocketRequest(OkexSocketOperation.Subscribe, $"option/instruments:{underlying}");
            return await Subscribe(request, null, false, internalHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve market data summary of all contracts of an underlying index for options trading. Does not require login.
        /// It will push multiple messages for multiple contracts, and only one contract per message.
        /// </summary>
        /// <param name="underlying">The underlying index that the contract is based upon, e.g. BTC-USD.</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public virtual CallResult<UpdateSubscription> Options_SubscribeToMarketData(string underlying, Action<OkexOptionsMarketData> onData) => Options_SubscribeToMarketData_Async(underlying, onData).Result;
        /// <summary>
        /// Retrieve market data summary of all contracts of an underlying index for options trading. Does not require login.
        /// It will push multiple messages for multiple contracts, and only one contract per message.
        /// </summary>
        /// <param name="underlying">The underlying index that the contract is based upon, e.g. BTC-USD.</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public virtual async Task<CallResult<UpdateSubscription>> Options_SubscribeToMarketData_Async(string underlying, Action<OkexOptionsMarketData> onData)
        {
            underlying = underlying.ValidateSymbol();

            var internalHandler = new Action<OkexSocketUpdateResponse<IEnumerable<OkexOptionsMarketData>>>(data =>
            {
                foreach (var d in data.Data)
                    onData(d);
            });

            var request = new OkexSocketRequest(OkexSocketOperation.Subscribe, $"option/summary:{underlying}");
            return await Subscribe(request, null, false, internalHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve market data summary of all contracts of an underlying index for options trading. Does not require login.
        /// It will push multiple messages for multiple contracts, and only one contract per message.
        /// </summary>
        /// <param name="underlyings">The underlying symbols Maximum Length: 100 symbols</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public virtual CallResult<UpdateSubscription> Options_SubscribeToMarketData(IEnumerable<string> underlyings, Action<OkexOptionsMarketData> onData) => Options_SubscribeToMarketData_Async(underlyings, onData).Result;
        /// <summary>
        /// Retrieve market data summary of all contracts of an underlying index for options trading. Does not require login.
        /// It will push multiple messages for multiple contracts, and only one contract per message.
        /// </summary>
        /// <param name="underlyings">The underlying symbols Maximum Length: 100 symbols</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public virtual async Task<CallResult<UpdateSubscription>> Options_SubscribeToMarketData_Async(IEnumerable<string> underlyings, Action<OkexOptionsMarketData> onData)
        {
            // To List
            var symbolList = underlyings.ToList();

            // Check Point
            if (symbolList.Count == 0)
                throw new ArgumentException("Symbols must contain 1 element at least");

            if (symbolList.Count > 100)
                throw new ArgumentException("Symbols can contain maximum 100 elements");

            for (int i = 0; i < symbolList.Count; i++)
                symbolList[i] = symbolList[i].ValidateSymbol();

            var internalHandler = new Action<OkexSocketUpdateResponse<IEnumerable<OkexOptionsMarketData>>>(data =>
            {
                foreach (var d in data.Data)
                    onData(d);
            });

            var tickerList = new List<string>();
            for (int i = 0; i < symbolList.Count; i++)
                tickerList.Add($"option/summary:{symbolList[i]}");

            var request = new OkexSocketRequest(OkexSocketOperation.Subscribe, tickerList);
            return await Subscribe(request, null, false, internalHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve the candlestick charts of option contracts. Does not require login,data is pushed every 500ms.
        /// Retrieve the candlestick data
        /// </summary>
		/// <param name="instrument">Trading contract symbol</param>
        /// <param name="period">The period of a single candlestick</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public virtual CallResult<UpdateSubscription> Options_SubscribeToCandlesticks(string instrument, OkexSpotPeriod period, Action<OkexOptionsCandle> onData) => Options_SubscribeToCandlesticks_Async(instrument, period, onData).Result;
        /// <summary>
        /// Retrieve the candlestick charts of option contracts. Does not require login,data is pushed every 500ms.
        /// </summary>
		/// <param name="instrument">Trading contract symbol</param>
        /// <param name="period">The period of a single candlestick</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public virtual async Task<CallResult<UpdateSubscription>> Options_SubscribeToCandlesticks_Async(string instrument, OkexSpotPeriod period, Action<OkexOptionsCandle> onData)
        {
            var internalHandler = new Action<OkexSocketUpdateResponse<IEnumerable<OkexOptionsCandleContainer>>>(data =>
            {
                foreach (var d in data.Data)
                {
                    d.Timestamp = DateTime.UtcNow;
                    d.Candle.Symbol = instrument.ToUpper(OkexGlobals.OkexCultureInfo);
                    onData(d.Candle);
                }
            });

            var period_s = JsonConvert.SerializeObject(period, new SpotPeriodConverter(false));
            var request = new OkexSocketRequest(OkexSocketOperation.Subscribe, $"option/candle{period_s}s:{instrument}");
            return await Subscribe(request, null, false, internalHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Get the filled orders data,it will be pushed when there is transaction data.
        /// </summary>
        /// <param name="instrument">Trading pair symbol</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public virtual CallResult<UpdateSubscription> Options_SubscribeToTrades(string instrument, Action<OkexOptionsTrade> onData) => Options_SubscribeToTrades_Async(instrument, onData).Result;
        /// <summary>
        /// Get the filled orders data,it will be pushed when there is transaction data.
        /// </summary>
		/// <param name="instrument">Trading pair symbol</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public virtual async Task<CallResult<UpdateSubscription>> Options_SubscribeToTrades_Async(string instrument, Action<OkexOptionsTrade> onData)
        {
            var internalHandler = new Action<OkexSocketUpdateResponse<IEnumerable<OkexOptionsTrade>>>(data =>
            {
                foreach (var d in data.Data)
                    onData(d);
            });

            var request = new OkexSocketRequest(OkexSocketOperation.Subscribe, $"option/trade:{instrument}");
            return await Subscribe(request, null, false, internalHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// This retrieves the latest traded price, best-bid price, best-ask price etc,data is pushed every 100ms.
        /// </summary>
        /// <param name="instrument">Instrument ID, e.g. BTC-USD-190830-9000-C</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public virtual CallResult<UpdateSubscription> Options_SubscribeToTicker(string instrument, Action<OkexOptionsTicker> onData) => Options_SubscribeToTicker_Async(instrument, onData).Result;
        /// <summary>
        /// This retrieves the latest traded price, best-bid price, best-ask price etc,data is pushed every 100ms.
        /// </summary>
        /// <param name="instrument">Instrument ID, e.g. BTC-USD-190830-9000-C</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public virtual async Task<CallResult<UpdateSubscription>> Options_SubscribeToTicker_Async(string instrument, Action<OkexOptionsTicker> onData)
        {
            var internalHandler = new Action<OkexSocketUpdateResponse<IEnumerable<OkexOptionsTicker>>>(data =>
            {
                foreach (var d in data.Data)
                    onData(d);
            });

            var request = new OkexSocketRequest(OkexSocketOperation.Subscribe, $"option/ticker:{instrument}");
            return await Subscribe(request, null, false, internalHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// This retrieves the latest traded price, best-bid price, best-ask price etc,data is pushed every 100ms.
        /// </summary>
        /// <param name="underlyings">The underlying symbols Maximum Length: 100 symbols</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public virtual CallResult<UpdateSubscription> Options_SubscribeToTicker(IEnumerable<string> underlyings, Action<OkexOptionsTicker> onData) => Options_SubscribeToTicker_Async(underlyings, onData).Result;
        /// <summary>
        /// This retrieves the latest traded price, best-bid price, best-ask price etc,data is pushed every 100ms.
        /// </summary>
        /// <param name="underlyings">The underlying symbols Maximum Length: 100 symbols</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public virtual async Task<CallResult<UpdateSubscription>> Options_SubscribeToTicker_Async(IEnumerable<string> underlyings, Action<OkexOptionsTicker> onData)
        {
            // To List
            var symbolList = underlyings.ToList();

            // Check Point
            if (symbolList.Count == 0)
                throw new ArgumentException("Symbols must contain 1 element at least");

            if (symbolList.Count > 100)
                throw new ArgumentException("Symbols can contain maximum 100 elements");

            for (int i = 0; i < symbolList.Count; i++)
                symbolList[i] = symbolList[i].ValidateSymbol();

            var internalHandler = new Action<OkexSocketUpdateResponse<IEnumerable<OkexOptionsTicker>>>(data =>
            {
                foreach (var d in data.Data)
                    onData(d);
            });

            var tickerList = new List<string>();
            for (int i = 0; i < symbolList.Count; i++)
                tickerList.Add($"option/ticker:{symbolList[i]}");

            var request = new OkexSocketRequest(OkexSocketOperation.Subscribe, tickerList);
            return await Subscribe(request, null, false, internalHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Depth-Five: The latest 5 entries of the market depth data is snapshooted and pushed every 100 milliseconds.
        /// Depth-All: After subscription, 400 entries of market depth data of the order book will first be pushed. Subsequently every 100 milliseconds we will snapshot and push entries that have changed during this time.
        /// Depth-TickByTick: The 400 entries of market depth data of the order book that return for the first time after subscription will be pushed; subsequently as long as there's any change of market depth data of the order book, the changes will be pushed tick by tick. Subsequently as long as there's any change of market depth data of the order book, the changes will be pushed tick by tick.
        /// </summary>
		/// <param name="instrument">Trading pair symbol</param>
        /// <param name="depth">Order Book Depth</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public virtual CallResult<UpdateSubscription> Options_SubscribeToOrderBook(string instrument, OkexOrderBookDepth depth, Action<OkexOptionsOrderBook> onData) => Options_SubscribeToOrderBook_Async(instrument, depth, onData).Result;
        /// <summary>
        /// Depth-Five: The latest 5 entries of the market depth data is snapshooted and pushed every 100 milliseconds.
        /// Depth-All: After subscription, 400 entries of market depth data of the order book will first be pushed. Subsequently every 100 milliseconds we will snapshot and push entries that have changed during this time.
        /// Depth-TickByTick: The 400 entries of market depth data of the order book that return for the first time after subscription will be pushed; subsequently as long as there's any change of market depth data of the order book, the changes will be pushed tick by tick. Subsequently as long as there's any change of market depth data of the order book, the changes will be pushed tick by tick.
        /// </summary>
		/// <param name="instrument">Trading pair symbol</param>
        /// <param name="depth">Order Book Depth</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public virtual async Task<CallResult<UpdateSubscription>> Options_SubscribeToOrderBook_Async(string instrument, OkexOrderBookDepth depth, Action<OkexOptionsOrderBook> onData)
        {
            var internalHandler = new Action<OkexOptionsOrderBookUpdate>(data =>
            {
                foreach (var d in data.Data)
                {
                    d.Symbol = instrument.ToUpper(OkexGlobals.OkexCultureInfo);
                    d.DataType = depth == OkexOrderBookDepth.Depth5 ? OkexOrderBookDataType.DepthTop5 : data.DataType;
                    onData(d);
                }
            });

            var channel = "depth";
            if (depth == OkexOrderBookDepth.Depth5) channel = "depth5";
            else if (depth == OkexOrderBookDepth.Depth400) channel = "depth";
            else if (depth == OkexOrderBookDepth.TickByTick) channel = "depth_l2_tbt";
            var request = new OkexSocketRequest(OkexSocketOperation.Subscribe, $"option/{channel}:{instrument}");
            return await Subscribe(request, null, false, internalHandler).ConfigureAwait(false);
        }

        #endregion

        #region Private Signed Feeds

        /// <summary>
        /// Retrieve the information on your option positions. Requires login.
        /// It will also push data due to change of fields such as mark price, option value etc.
        /// Multiple messages will be pushed for multiple (contracts) positions, and only one (contract) position per message.
        /// User can retrieve the full positions firstly via REST Option Positions endpoint, and then retrieve the individual positions updates via this channel.
        /// </summary>
        /// <param name="underlying">Underlying Symbol</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public virtual CallResult<UpdateSubscription> Options_SubscribeToPositions(string underlying, Action<OkexOptionsPosition> onData) => Options_SubscribeToPositions_Async(underlying, onData).Result;
        /// <summary>
        /// Retrieve the information on your option positions. Requires login.
        /// It will also push data due to change of fields such as mark price, option value etc.
        /// Multiple messages will be pushed for multiple (contracts) positions, and only one (contract) position per message.
        /// User can retrieve the full positions firstly via REST Option Positions endpoint, and then retrieve the individual positions updates via this channel.
        /// </summary>
        /// <param name="underlying">Underlying Symbol</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public virtual async Task<CallResult<UpdateSubscription>> Options_SubscribeToPositions_Async(string underlying, Action<OkexOptionsPosition> onData)
        {
            var internalHandler = new Action<OkexSocketUpdateResponse<IEnumerable<OkexOptionsPosition>>>(data =>
            {
                foreach (var d in data.Data)
                    onData(d);
            });

            var request = new OkexSocketRequest(OkexSocketOperation.Subscribe, $"option/position:{underlying}");
            return await Subscribe(request, null, true, internalHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve account information. Requires login.
        /// </summary>
        /// <param name="underlying">Instrument Id</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public virtual CallResult<UpdateSubscription> Options_SubscribeToBalance(string underlying, Action<OkexOptionsBalance> onData) => Options_SubscribeToBalance_Async(underlying, onData).Result;
        /// <summary>
        /// Retrieve account information. Requires login.
        /// </summary>
        /// <param name="underlying">Instrument Id</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public virtual async Task<CallResult<UpdateSubscription>> Options_SubscribeToBalance_Async(string underlying, Action<OkexOptionsBalance> onData)
        {
            var internalHandler = new Action<OkexSocketUpdateResponse<IEnumerable<OkexOptionsBalance>>>(data =>
            {
                foreach (var d in data.Data)
                    onData(d);
            });

            var request = new OkexSocketRequest(OkexSocketOperation.Subscribe, $"option/account:{underlying}");
            return await Subscribe(request, null, true, internalHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieves the order details of the account. Requires login.
        /// </summary>
        /// <param name="underlying">Underlying Symbol</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public virtual CallResult<UpdateSubscription> Options_SubscribeToOrders(string underlying, Action<OkexOptionsOrder> onData) => Options_SubscribeToOrders_Async(underlying, onData).Result;
        /// <summary>
        /// Retrieves the order details of the account. Requires login.
        /// </summary>
        /// <param name="underlying">Underlying Symbol</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public virtual async Task<CallResult<UpdateSubscription>> Options_SubscribeToOrders_Async(string underlying, Action<OkexOptionsOrder> onData)
        {
            var internalHandler = new Action<OkexSocketUpdateResponse<IEnumerable<OkexOptionsOrder>>>(data =>
            {
                foreach (var d in data.Data)
                    onData(d);
            });

            var request = new OkexSocketRequest(OkexSocketOperation.Subscribe, $"option/order:{underlying}");
            return await Subscribe(request, null, true, internalHandler).ConfigureAwait(false);
        }

        #endregion

        #endregion
    }
}
