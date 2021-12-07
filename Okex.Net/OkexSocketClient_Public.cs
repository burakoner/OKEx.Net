using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Newtonsoft.Json;
using Okex.Net.Converters;
using Okex.Net.CoreObjects;
using Okex.Net.Enums;
using Okex.Net.RestObjects.Market;
using Okex.Net.RestObjects.Public;
using Okex.Net.RestObjects.System;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Okex.Net
{
    public partial class OkexSocketClient
    {
        #region Public Channels
        /// <summary>
        /// The full instrument list will be pushed for the first time after subscription. Subsequently, the instruments will be pushed if there's any change to the instrument’s state (such as delivery of FUTURES, exercise of OPTION, listing of new contracts / trading pairs, trading suspension, etc.).
        /// </summary>
        /// <param name="instrumentType">Instrument Type</param>
        /// <param name="onData">On Data Handler</param>
        /// <returns></returns>
        public virtual CallResult<UpdateSubscription> SubscribeToInstruments(OkexInstrumentType instrumentType, Action<OkexInstrument> onData) => SubscribeToInstruments_Async(instrumentType, onData).Result;
        /// <summary>
        /// The full instrument list will be pushed for the first time after subscription. Subsequently, the instruments will be pushed if there's any change to the instrument’s state (such as delivery of FUTURES, exercise of OPTION, listing of new contracts / trading pairs, trading suspension, etc.).
        /// </summary>
        /// <param name="instrumentType">Instrument Type</param>
        /// <param name="onData">On Data Handler</param>
        /// <returns></returns>
        public virtual async Task<CallResult<UpdateSubscription>> SubscribeToInstruments_Async(OkexInstrumentType instrumentType, Action<OkexInstrument> onData)
        {
            var internalHandler = new Action<DataEvent<OkexSocketUpdateResponse<IEnumerable<OkexInstrument>>>>(data =>
            {
                foreach (var d in data.Data.Data)
                    onData(d);
            });

            var request = new OkexSocketRequest(OkexSocketOperation.Subscribe, "instruments", instrumentType);
            return await SubscribeAsync(request, null, false, internalHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve the last traded price, bid price, ask price and 24-hour trading volume of instruments. Data will be pushed every 100 ms.
        /// </summary>
        /// <param name="intrumentId">Instrument ID</param>
        /// <param name="onData">On Data Handler</param>
        /// <returns></returns>
        public virtual CallResult<UpdateSubscription> SubscribeToTickers(string intrumentId, Action<OkexTicker> onData) => SubscribeToTickers_Async(intrumentId, onData).Result;
        /// Retrieve the last traded price, bid price, ask price and 24-hour trading volume of instruments. Data will be pushed every 100 ms.
        /// </summary>
        /// <param name="intrumentId">Instrument ID</param>
        /// <param name="onData">On Data Handler</param>
        /// <returns></returns>
        public virtual async Task<CallResult<UpdateSubscription>> SubscribeToTickers_Async(string intrumentId, Action<OkexTicker> onData)
        {
            var internalHandler = new Action<DataEvent<OkexSocketUpdateResponse<IEnumerable<OkexTicker>>>>(data =>
            {
                foreach (var d in data.Data.Data)
                    onData(d);
            });

            var request = new OkexSocketRequest(OkexSocketOperation.Subscribe, "tickers", intrumentId);
            return await SubscribeAsync(request, null, false, internalHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve the open interest. Data will by pushed every 3 seconds.
        /// </summary>
        /// <param name="intrumentId">Instrument ID</param>
        /// <param name="onData">On Data Handler</param>
        /// <returns></returns>
        public virtual CallResult<UpdateSubscription> SubscribeToInterests(string intrumentId, Action<OkexOpenInterest> onData) => SubscribeToInterests_Async(intrumentId, onData).Result;
        /// <summary>
        /// Retrieve the open interest. Data will by pushed every 3 seconds.
        /// </summary>
        /// <param name="intrumentId">Instrument ID</param>
        /// <param name="onData">On Data Handler</param>
        /// <returns></returns>
        public virtual async Task<CallResult<UpdateSubscription>> SubscribeToInterests_Async(string intrumentId, Action<OkexOpenInterest> onData)
        {
            var internalHandler = new Action<DataEvent<OkexSocketUpdateResponse<IEnumerable<OkexOpenInterest>>>>(data =>
            {
                foreach (var d in data.Data.Data)
                    onData(d);
            });

            var request = new OkexSocketRequest(OkexSocketOperation.Subscribe, "open-interest", intrumentId);
            return await SubscribeAsync(request, null, false, internalHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve the candlesticks data of an instrument. Data will be pushed every 500 ms.
        /// </summary>
        /// <param name="intrumentId">Instrument ID</param>
        /// <param name="period"></param>
        /// <param name="onData">On Data Handler</param>
        /// <returns></returns>
        public virtual CallResult<UpdateSubscription> SubscribeToCandlesticks(string intrumentId, OkexPeriod period, Action<OkexCandlestick> onData) => SubscribeToCandlesticks_Async(intrumentId, period, onData).Result;
        /// <summary>
        /// Retrieve the candlesticks data of an instrument. Data will be pushed every 500 ms.
        /// </summary>
        /// <param name="intrumentId">Instrument ID</param>
        /// <param name="period"></param>
        /// <param name="onData">On Data Handler</param>
        /// <returns></returns>
        public virtual async Task<CallResult<UpdateSubscription>> SubscribeToCandlesticks_Async(string intrumentId, OkexPeriod period, Action<OkexCandlestick> onData)
        {
            var internalHandler = new Action<DataEvent<OkexSocketUpdateResponse<IEnumerable<OkexCandlestick>>>>(data =>
            {
                foreach (var d in data.Data.Data)
                {
                    d.Instrument = intrumentId;
                    onData(d);
                }
            });

            var jc = JsonConvert.SerializeObject(period, new PeriodConverter(false));
            var request = new OkexSocketRequest(OkexSocketOperation.Subscribe, "candle" + jc, intrumentId);
            return await SubscribeAsync(request, null, false, internalHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve the recent trades data. Data will be pushed whenever there is a trade.
        /// </summary>
        /// <param name="intrumentId">Instrument ID</param>
        /// <param name="onData">On Data Handler</param>
        /// <returns></returns>
        public virtual CallResult<UpdateSubscription> SubscribeToTrades(string intrumentId, Action<OkexTrade> onData) => SubscribeToTrades_Async(intrumentId, onData).Result;
        /// <summary>
        /// Retrieve the recent trades data. Data will be pushed whenever there is a trade.
        /// </summary>
        /// <param name="intrumentId">Instrument ID</param>
        /// <param name="onData">On Data Handler</param>
        /// <returns></returns>
        public virtual async Task<CallResult<UpdateSubscription>> SubscribeToTrades_Async(string intrumentId, Action<OkexTrade> onData)
        {
            var internalHandler = new Action<DataEvent<OkexSocketUpdateResponse<IEnumerable<OkexTrade>>>>(data =>
            {
                foreach (var d in data.Data.Data)
                    onData(d);
            });

            var request = new OkexSocketRequest(OkexSocketOperation.Subscribe, "trades", intrumentId);
            return await SubscribeAsync(request, null, false, internalHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve the estimated delivery/exercise price of FUTURES contracts and OPTION.
        /// Only the estimated delivery/exercise price will be pushed an hour before delivery/exercise, and will be pushed if there is any price change.
        /// </summary>
        /// <param name="instrumentType">Instrument Type</param>
        /// <param name="underlying">Underlying</param>
        /// <param name="onData">On Data Handler</param>
        /// <returns></returns>
        public virtual CallResult<UpdateSubscription> SubscribeToEstimatedPrice(OkexInstrumentType instrumentType, string underlying, Action<OkexEstimatedPrice> onData) => SubscribeToEstimatedPrice_Async(instrumentType, underlying, onData).Result;
        /// <summary>
        /// Retrieve the estimated delivery/exercise price of FUTURES contracts and OPTION.
        /// Only the estimated delivery/exercise price will be pushed an hour before delivery/exercise, and will be pushed if there is any price change.
        /// </summary>
        /// <param name="instrumentType">Instrument Type</param>
        /// <param name="underlying">Underlying</param>
        /// <param name="onData">On Data Handler</param>
        /// <returns></returns>
        public virtual async Task<CallResult<UpdateSubscription>> SubscribeToEstimatedPrice_Async(OkexInstrumentType instrumentType, string underlying, Action<OkexEstimatedPrice> onData)
        {
            var internalHandler = new Action<DataEvent<OkexSocketUpdateResponse<IEnumerable<OkexEstimatedPrice>>>>(data =>
            {
                foreach (var d in data.Data.Data)
                    onData(d);
            });

            var request = new OkexSocketRequest(OkexSocketOperation.Subscribe, "estimated-price", instrumentType, underlying);
            return await SubscribeAsync(request, null, false, internalHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve the mark price. Data will be pushed every 200 ms when the mark price changes, and will be pushed every 10 seconds when the mark price does not change.
        /// </summary>
        /// <param name="intrumentId">Instrument ID</param>
        /// <param name="onData">On Data Handler</param>
        /// <returns></returns>
        public virtual CallResult<UpdateSubscription> SubscribeToMarkPrice(string intrumentId, Action<OkexMarkPrice> onData) => SubscribeToMarkPrice_Async(intrumentId, onData).Result;
        /// <summary>
        /// Retrieve the mark price. Data will be pushed every 200 ms when the mark price changes, and will be pushed every 10 seconds when the mark price does not change.
        /// </summary>
        /// <param name="intrumentId">Instrument ID</param>
        /// <param name="onData">On Data Handler</param>
        /// <returns></returns>
        public virtual async Task<CallResult<UpdateSubscription>> SubscribeToMarkPrice_Async(string intrumentId, Action<OkexMarkPrice> onData)
        {
            var internalHandler = new Action<DataEvent<OkexSocketUpdateResponse<IEnumerable<OkexMarkPrice>>>>(data =>
            {
                foreach (var d in data.Data.Data)
                    onData(d);
            });

            var request = new OkexSocketRequest(OkexSocketOperation.Subscribe, "mark-price", intrumentId);
            return await SubscribeAsync(request, null, false, internalHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve the candlesticks data of the mark price. Data will be pushed every 500 ms.
        /// </summary>
        /// <param name="intrumentId">Instrument ID</param>
        /// <param name="period">Period</param>
        /// <param name="onData">On Data Handler</param>
        /// <returns></returns>
        public virtual CallResult<UpdateSubscription> SubscribeToMarkPriceCandlesticks(string intrumentId, OkexPeriod period, Action<OkexCandlestick> onData) => SubscribeToMarkPriceCandlesticks_Async(intrumentId, period, onData).Result;
        /// <summary>
        /// Retrieve the candlesticks data of the mark price. Data will be pushed every 500 ms.
        /// </summary>
        /// <param name="intrumentId">Instrument ID</param>
        /// <param name="period">Period</param>
        /// <param name="onData">On Data Handler</param>
        /// <returns></returns>
        public virtual async Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceCandlesticks_Async(string intrumentId, OkexPeriod period, Action<OkexCandlestick> onData)
        {
            var internalHandler = new Action<DataEvent<OkexSocketUpdateResponse<IEnumerable<OkexCandlestick>>>>(data =>
            {
                foreach (var d in data.Data.Data)
                {
                    d.Instrument = intrumentId;
                    onData(d);
                }
            });

            var jc = JsonConvert.SerializeObject(period, new PeriodConverter(false));
            var request = new OkexSocketRequest(OkexSocketOperation.Subscribe, "mark-price-candle" + jc, intrumentId);
            return await SubscribeAsync(request, null, false, internalHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve the maximum buy price and minimum sell price of the instrument. Data will be pushed every 5 seconds when there are changes in limits, and will not be pushed when there is no changes on limit.
        /// </summary>
        /// <param name="intrumentId">Instrument ID</param>
        /// <param name="onData">On Data Handler</param>
        /// <returns></returns>
        public virtual CallResult<UpdateSubscription> SubscribeToPriceLimit(string intrumentId, Action<OkexLimitPrice> onData) => SubscribeToPriceLimit_Async(intrumentId, onData).Result;
        /// <summary>
        /// Retrieve the maximum buy price and minimum sell price of the instrument. Data will be pushed every 5 seconds when there are changes in limits, and will not be pushed when there is no changes on limit.
        /// </summary>
        /// <param name="intrumentId">Instrument ID</param>
        /// <param name="onData">On Data Handler</param>
        /// <returns></returns>
        public virtual async Task<CallResult<UpdateSubscription>> SubscribeToPriceLimit_Async(string intrumentId, Action<OkexLimitPrice> onData)
        {
            var internalHandler = new Action<DataEvent<OkexSocketUpdateResponse<IEnumerable<OkexLimitPrice>>>>(data =>
            {
                foreach (var d in data.Data.Data)
                    onData(d);
            });

            var request = new OkexSocketRequest(OkexSocketOperation.Subscribe, "price-limit", intrumentId);
            return await SubscribeAsync(request, null, false, internalHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve order book data.
        /// Use books for 400 depth levels, book5 for 5 depth levels, books50-l2-tbt tick-by-tick 50 depth levels, and books-l2-tbt for tick-by-tick 400 depth levels.
        /// books: 400 depth levels will be pushed in the initial full snapshot. Incremental data will be pushed every 100 ms when there is change in order book.
        /// books5: 5 depth levels will be pushed every time.Data will be pushed every 200 ms when there is change in order book.
        /// books50-l2-tbt: 50 depth levels will be pushed in the initial full snapshot. Incremental data will be pushed tick by tick, i.e.whenever there is change in order book.
        /// books-l2-tbt: 400 depth levels will be pushed in the initial full snapshot. Incremental data will be pushed tick by tick, i.e.whenever there is change in order book.
        /// </summary>
        /// <param name="intrumentId">Instrument ID</param>
        /// <param name="orderBookType">Order Book Type</param>
        /// <param name="onData">On Data Handler</param>
        /// <returns></returns>
        public virtual CallResult<UpdateSubscription> SubscribeToOrderBook(string intrumentId, OkexOrderBookType orderBookType, Action<OkexOrderBook> onData) => SubscribeToOrderBook_Async(intrumentId, orderBookType,onData).Result;
        /// <summary>
        /// Retrieve order book data.
        /// Use books for 400 depth levels, book5 for 5 depth levels, books50-l2-tbt tick-by-tick 50 depth levels, and books-l2-tbt for tick-by-tick 400 depth levels.
        /// books: 400 depth levels will be pushed in the initial full snapshot. Incremental data will be pushed every 100 ms when there is change in order book.
        /// books5: 5 depth levels will be pushed every time.Data will be pushed every 200 ms when there is change in order book.
        /// books50-l2-tbt: 50 depth levels will be pushed in the initial full snapshot. Incremental data will be pushed tick by tick, i.e.whenever there is change in order book.
        /// books-l2-tbt: 400 depth levels will be pushed in the initial full snapshot. Incremental data will be pushed tick by tick, i.e.whenever there is change in order book.
        /// </summary>
        /// <param name="intrumentId">Instrument ID</param>
        /// <param name="orderBookType">Order Book Type</param>
        /// <param name="onData">On Data Handler</param>
        /// <returns></returns>
        public virtual async Task<CallResult<UpdateSubscription>> SubscribeToOrderBook_Async(string intrumentId, OkexOrderBookType orderBookType, Action<OkexOrderBook> onData)
        {
            var internalHandler = new Action<DataEvent<OkexOrderBookUpdate>>(data =>
            {
                foreach (var d in data.Data.Data)
                {
                    d.Instrument = intrumentId;
                    d.Action = data.Data.Action;
                    onData(d);
                }
            });

            var jc = JsonConvert.SerializeObject(orderBookType, new OrderBookTypeConverter(false));
            var request = new OkexSocketRequest(OkexSocketOperation.Subscribe, jc, intrumentId);
            return await SubscribeAsync(request, null, false, internalHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve detailed pricing information of all OPTION contracts. Data will be pushed at once.
        /// </summary>
        /// <param name="underlying">Underlying</param>
        /// <param name="onData">On Data Handler</param>
        /// <returns></returns>
        public virtual CallResult<UpdateSubscription> SubscribeToOptionSummary(string underlying, Action<OkexOptionSummary> onData) => SubscribeToOptionSummary_Async(underlying, onData).Result;
        /// <summary>
        /// Retrieve detailed pricing information of all OPTION contracts. Data will be pushed at once.
        /// </summary>
        /// <param name="underlying">Underlying</param>
        /// <param name="onData">On Data Handler</param>
        /// <returns></returns>
        public virtual async Task<CallResult<UpdateSubscription>> SubscribeToOptionSummary_Async(string underlying, Action<OkexOptionSummary> onData)
        {
            var internalHandler = new Action<DataEvent<OkexSocketUpdateResponse<IEnumerable<OkexOptionSummary>>>>(data =>
            {
                foreach (var d in data.Data.Data)
                    onData(d);
            });

            var request = new OkexSocketRequest(OkexSocketOperation.Subscribe, "opt-summary", string.Empty, underlying);
            return await SubscribeAsync(request, null, false, internalHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve funding rate. Data will be pushed every minute.
        /// </summary>
        /// <param name="intrumentId">Instrument ID</param>
        /// <param name="onData">On Data Handler</param>
        /// <returns></returns>
        public virtual CallResult<UpdateSubscription> SubscribeToFundingRates(string intrumentId, Action<OkexFundingRate> onData) => SubscribeToFundingRates_Async(intrumentId, onData).Result;
        /// <summary>
        /// Retrieve funding rate. Data will be pushed every minute.
        /// </summary>
        /// <param name="intrumentId">Instrument ID</param>
        /// <param name="onData">On Data Handler</param>
        /// <returns></returns>
        public virtual async Task<CallResult<UpdateSubscription>> SubscribeToFundingRates_Async(string intrumentId, Action<OkexFundingRate> onData)
        {
            var internalHandler = new Action<DataEvent<OkexSocketUpdateResponse<IEnumerable<OkexFundingRate>>>>(data =>
            {
                foreach (var d in data.Data.Data)
                    onData(d);
            });

            var request = new OkexSocketRequest(OkexSocketOperation.Subscribe, "funding-rate", intrumentId);
            return await SubscribeAsync(request, null, false, internalHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve the candlesticks data of the index. Data will be pushed every 500 ms.
        /// </summary>
        /// <param name="intrumentId">Instrument ID</param>
        /// <param name="period">Period</param>
        /// <param name="onData">On Data Handler</param>
        /// <returns></returns>
        public virtual CallResult<UpdateSubscription> SubscribeToIndexCandlesticks(string intrumentId, OkexPeriod period, Action<OkexCandlestick> onData) => SubscribeToIndexCandlesticks_Async(intrumentId, period, onData).Result;
        /// <summary>
        /// Retrieve the candlesticks data of the index. Data will be pushed every 500 ms.
        /// </summary>
        /// <param name="intrumentId">Instrument ID</param>
        /// <param name="period">Period</param>
        /// <param name="onData">On Data Handler</param>
        /// <returns></returns>
        public virtual async Task<CallResult<UpdateSubscription>> SubscribeToIndexCandlesticks_Async(string intrumentId, OkexPeriod period, Action<OkexCandlestick> onData)
        {
            var internalHandler = new Action<DataEvent<OkexSocketUpdateResponse<IEnumerable<OkexCandlestick>>>>(data =>
            {
                foreach (var d in data.Data.Data)
                {
                    d.Instrument = intrumentId;
                    onData(d);
                }
            });

            var jc = JsonConvert.SerializeObject(period, new PeriodConverter(false));
            var request = new OkexSocketRequest(OkexSocketOperation.Subscribe, "index-candle" + jc, intrumentId);
            return await SubscribeAsync(request, null, false, internalHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve index tickers data
        /// </summary>
        /// <param name="intrumentId">Instrument ID</param>
        /// <param name="onData">On Data Handler</param>
        /// <returns></returns>
        public virtual CallResult<UpdateSubscription> SubscribeToIndexTickers(string intrumentId, Action<OkexIndexTicker> onData) => SubscribeToIndexTickers_Async(intrumentId, onData).Result;
        /// <summary>
        /// Retrieve index tickers data
        /// </summary>
        /// <param name="intrumentId">Instrument ID</param>
        /// <param name="onData">On Data Handler</param>
        /// <returns></returns>
        public virtual async Task<CallResult<UpdateSubscription>> SubscribeToIndexTickers_Async(string intrumentId, Action<OkexIndexTicker> onData)
        {
            var internalHandler = new Action<DataEvent<OkexSocketUpdateResponse<IEnumerable<OkexIndexTicker>>>>(data =>
            {
                foreach (var d in data.Data.Data)
                    onData(d);
            });

            var request = new OkexSocketRequest(OkexSocketOperation.Subscribe, "index-tickers", intrumentId);
            return await SubscribeAsync(request, null, false, internalHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Get the status of system maintenance and push when the system maintenance status changes. First subscription: "Push the latest change data"; every time there is a state change, push the changed content
        /// </summary>
        /// <param name="onData">On Data Handler</param>
        /// <returns></returns>
        public virtual CallResult<UpdateSubscription> SubscribeToSystemStatus(Action<OkexStatus> onData) => SubscribeToSystemStatus_Async(onData).Result;
        /// <summary>
        /// Get the status of system maintenance and push when the system maintenance status changes. First subscription: "Push the latest change data"; every time there is a state change, push the changed content
        /// </summary>
        /// <param name="onData">On Data Handler</param>
        /// <returns></returns>
        public virtual async Task<CallResult<UpdateSubscription>> SubscribeToSystemStatus_Async(Action<OkexStatus> onData)
        {
            var internalHandler = new Action<DataEvent<OkexSocketUpdateResponse<IEnumerable<OkexStatus>>>>(data =>
            {
                foreach (var d in data.Data.Data)
                    onData(d);
            });

            var request = new OkexSocketRequest(OkexSocketOperation.Subscribe, "status", string.Empty, string.Empty);
            return await SubscribeAsync(request, null, false, internalHandler).ConfigureAwait(false);
        }
        #endregion
    }
}
