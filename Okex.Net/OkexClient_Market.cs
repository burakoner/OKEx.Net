using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using Newtonsoft.Json;
using Okex.Net.Converters;
using Okex.Net.CoreObjects;
using Okex.Net.Enums;
using Okex.Net.RestObjects.Market;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Okex.Net
{
    public partial class OkexClient
    {
        #region Market API Endpoints
        /// <summary>
        /// Retrieve the latest price snapshot, best bid/ask price, and trading volume in the last 24 hours.
        /// </summary>
        /// <param name="instrumentType">Instrument Type</param>
        /// <param name="underlying">Underlying</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<IEnumerable<OkexTicker>> GetTickers(OkexInstrumentType instrumentType, string underlying = null, CancellationToken ct = default)
            => GetTickers_Async(instrumentType, underlying, ct).Result;
        /// <summary>
        /// Retrieve the latest price snapshot, best bid/ask price, and trading volume in the last 24 hours.
        /// </summary>
        /// <param name="instrumentType">Instrument Type</param>
        /// <param name="underlying">Underlying</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<IEnumerable<OkexTicker>>> GetTickers_Async(OkexInstrumentType instrumentType, string underlying = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)) },
            };
            parameters.AddOptionalParameter("uly", underlying);

            var result = await SendRequestAsync<OkexRestApiResponse<IEnumerable<OkexTicker>>>(GetUrl(Endpoints_V5_Market_Tickers), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<IEnumerable<OkexTicker>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<IEnumerable<OkexTicker>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage));

            return new WebCallResult<IEnumerable<OkexTicker>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }

        /// <summary>
        /// Retrieve the latest price snapshot, best bid/ask price, and trading volume in the last 24 hours.
        /// </summary>
        /// <param name="instrumentId">Instrument ID</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<OkexTicker> GetTicker(string instrumentId, CancellationToken ct = default)
            => GetTicker_Async(instrumentId, ct).Result;
        /// <summary>
        /// Retrieve the latest price snapshot, best bid/ask price, and trading volume in the last 24 hours.
        /// </summary>
        /// <param name="instrumentId">Instrument ID</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<OkexTicker>> GetTicker_Async(string instrumentId, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "instId", instrumentId },
            };

            var result = await SendRequestAsync<OkexRestApiResponse<IEnumerable<OkexTicker>>>(GetUrl(Endpoints_V5_Market_Ticker), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<OkexTicker>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<OkexTicker>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage));

            return new WebCallResult<OkexTicker>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data.FirstOrDefault(), null);
        }

        /// <summary>
        /// Retrieve index tickers.
        /// </summary>
        /// <param name="quoteCurrency">Quote currency. Currently there is only an index with USD/USDT/BTC as the quote currency.</param>
        /// <param name="instrumentId">Instrument ID</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<IEnumerable<OkexIndexTicker>> GetIndexTickers(string quoteCurrency = null, string instrumentId = null, CancellationToken ct = default)
            => GetIndexTickers_Async(quoteCurrency, instrumentId, ct).Result;
        /// <summary>
        /// Retrieve index tickers.
        /// </summary>
        /// <param name="quoteCurrency">Quote currency. Currently there is only an index with USD/USDT/BTC as the quote currency.</param>
        /// <param name="instrumentId">Instrument ID</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<IEnumerable<OkexIndexTicker>>> GetIndexTickers_Async(string quoteCurrency = null, string instrumentId = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("quoteCcy", quoteCurrency);
            parameters.AddOptionalParameter("instId", instrumentId);

            var result = await SendRequestAsync<OkexRestApiResponse<IEnumerable<OkexIndexTicker>>>(GetUrl(Endpoints_V5_Market_IndexTickers), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<IEnumerable<OkexIndexTicker>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<IEnumerable<OkexIndexTicker>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage));

            return new WebCallResult<IEnumerable<OkexIndexTicker>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }

        /// <summary>
        /// Retrieve a instrument is order book.
        /// </summary>
        /// <param name="instrumentId">Instrument ID</param>
        /// <param name="depth">Order book depth per side. Maximum 400, e.g. 400 bids + 400 asks. Default returns to 1 depth data</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<OkexOrderBook> GetOrderBook(string instrumentId, int depth = 1, CancellationToken ct = default)
            => GetOrderBook_Async(instrumentId, depth, ct).Result;
        /// <summary>
        /// Retrieve a instrument is order book.
        /// </summary>
        /// <param name="instrumentId">Instrument ID</param>
        /// <param name="depth">Order book depth per side. Maximum 400, e.g. 400 bids + 400 asks. Default returns to 1 depth data</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<OkexOrderBook>> GetOrderBook_Async(string instrumentId, int depth = 1, CancellationToken ct = default)
        {
            if (depth < 1 || depth > 400)
                throw new ArgumentException("Depth can be between 1-400.");

            var parameters = new Dictionary<string, object>
            {
                {"instId", instrumentId},
                {"sz", depth},
            };

            var result = await SendRequestAsync<OkexRestApiResponse<IEnumerable<OkexOrderBook>>>(GetUrl(Endpoints_V5_Market_Books), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
            if (!result.Success || result.Data.Data.Count()==0) return WebCallResult<OkexOrderBook>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<OkexOrderBook>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage));

            var orderbook = result.Data.Data.FirstOrDefault();
            orderbook.Instrument = instrumentId;
            return new WebCallResult<OkexOrderBook>(result.ResponseStatusCode, result.ResponseHeaders, orderbook, null);
        }

        /// <summary>
        /// Retrieve the candlestick charts. This endpoint can retrieve the latest 1,440 data entries. Charts are returned in groups based on the requested bar.
        /// </summary>
        /// <param name="instrumentId">Instrument ID</param>
        /// <param name="period">Bar size, the default is 1m</param>
        /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
        /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
        /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<IEnumerable<OkexCandlestick>> GetCandlesticks(string instrumentId, OkexPeriod period, long? after = null, long? before = null, int limit = 100, CancellationToken ct = default)
            => GetCandlesticks_Async(instrumentId, period, after, before, limit, ct).Result;
        /// <summary>
        /// Retrieve the candlestick charts. This endpoint can retrieve the latest 1,440 data entries. Charts are returned in groups based on the requested bar.
        /// </summary>
        /// <param name="instrumentId">Instrument ID</param>
        /// <param name="period">Bar size, the default is 1m</param>
        /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
        /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
        /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<IEnumerable<OkexCandlestick>>> GetCandlesticks_Async(string instrumentId, OkexPeriod period, long? after = null, long? before = null, int limit = 100, CancellationToken ct = default)
        {
            if (limit < 1 || limit > 100)
                throw new ArgumentException("Limit can be between 1-100.");

            var parameters = new Dictionary<string, object>
            {
                { "instId", instrumentId },
                { "bar", JsonConvert.SerializeObject(period, new PeriodConverter(false)) },
            };
            parameters.AddOptionalParameter("after", after?.ToString());
            parameters.AddOptionalParameter("before", before?.ToString());
            parameters.AddOptionalParameter("limit", limit.ToString());

            var result = await SendRequestAsync<OkexRestApiResponse<IEnumerable<OkexCandlestick>>>(GetUrl(Endpoints_V5_Market_Candles), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<IEnumerable<OkexCandlestick>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<IEnumerable<OkexCandlestick>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage));

            foreach (var candle in result.Data.Data) candle.Instrument = instrumentId;
            return new WebCallResult<IEnumerable<OkexCandlestick>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }

        /// <summary>
        /// Retrieve history candlestick charts from recent years.
        /// </summary>
        /// <param name="instrumentId">Instrument ID</param>
        /// <param name="period">Bar size, the default is 1m</param>
        /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
        /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
        /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<IEnumerable<OkexCandlestick>> GetCandlesticksHistory(string instrumentId, OkexPeriod period, long? after = null, long? before = null, int limit = 100, CancellationToken ct = default)
            => GetCandlesticksHistory_Async(instrumentId, period, after, before, limit, ct).Result;
        /// <summary>
        /// Retrieve history candlestick charts from recent years.
        /// </summary>
        /// <param name="instrumentId">Instrument ID</param>
        /// <param name="period">Bar size, the default is 1m</param>
        /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
        /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
        /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<IEnumerable<OkexCandlestick>>> GetCandlesticksHistory_Async(string instrumentId, OkexPeriod period, long? after = null, long? before = null, int limit = 100, CancellationToken ct = default)
        {
            if (limit < 1 || limit > 100)
                throw new ArgumentException("Limit can be between 1-100.");

            var parameters = new Dictionary<string, object>
            {
                { "instId", instrumentId },
                { "bar", JsonConvert.SerializeObject(period, new PeriodConverter(false)) },
            };
            parameters.AddOptionalParameter("after", after?.ToString());
            parameters.AddOptionalParameter("before", before?.ToString());
            parameters.AddOptionalParameter("limit", limit.ToString());

            var result = await SendRequestAsync<OkexRestApiResponse<IEnumerable<OkexCandlestick>>>(GetUrl(Endpoints_V5_Market_HistoryCandles), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<IEnumerable<OkexCandlestick>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<IEnumerable<OkexCandlestick>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage));

            foreach (var candle in result.Data.Data) candle.Instrument = instrumentId;
            return new WebCallResult<IEnumerable<OkexCandlestick>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }

        /// <summary>
        /// Retrieve the candlestick charts of the index. This endpoint can retrieve the latest 1,440 data entries. Charts are returned in groups based on the requested bar.
        /// </summary>
        /// <param name="instrumentId">Instrument ID</param>
        /// <param name="period">Bar size, the default is 1m</param>
        /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
        /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
        /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<IEnumerable<OkexCandlestick>> GetIndexCandlesticks(string instrumentId, OkexPeriod period, long? after = null, long? before = null, int limit = 100, CancellationToken ct = default)
            => GetIndexCandlesticks_Async(instrumentId, period, after, before, limit, ct).Result;
        /// <summary>
        /// Retrieve the candlestick charts of the index. This endpoint can retrieve the latest 1,440 data entries. Charts are returned in groups based on the requested bar.
        /// </summary>
        /// <param name="instrumentId">Instrument ID</param>
        /// <param name="period">Bar size, the default is 1m</param>
        /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
        /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
        /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<IEnumerable<OkexCandlestick>>> GetIndexCandlesticks_Async(string instrumentId, OkexPeriod period, long? after = null, long? before = null, int limit = 100, CancellationToken ct = default)
        {
            if (limit < 1 || limit > 100)
                throw new ArgumentException("Limit can be between 1-100.");

            var parameters = new Dictionary<string, object>
            {
                { "instId", instrumentId },
                { "bar", JsonConvert.SerializeObject(period, new PeriodConverter(false)) },
            };
            parameters.AddOptionalParameter("after", after?.ToString());
            parameters.AddOptionalParameter("before", before?.ToString());
            parameters.AddOptionalParameter("limit", limit.ToString());

            var result = await SendRequestAsync<OkexRestApiResponse<IEnumerable<OkexCandlestick>>>(GetUrl(Endpoints_V5_Market_IndexCandles), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<IEnumerable<OkexCandlestick>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<IEnumerable<OkexCandlestick>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage));

            foreach (var candle in result.Data.Data) candle.Instrument = instrumentId;
            return new WebCallResult<IEnumerable<OkexCandlestick>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }

        /// <summary>
        /// Retrieve the candlestick charts of mark price. This endpoint can retrieve the latest 1,440 data entries. Charts are returned in groups based on the requested bar.
        /// </summary>
        /// <param name="instrumentId">Instrument ID</param>
        /// <param name="period">Bar size, the default is 1m</param>
        /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
        /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
        /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<IEnumerable<OkexCandlestick>> GetMarkPriceCandlesticks(string instrumentId, OkexPeriod period, long? after = null, long? before = null, int limit = 100, CancellationToken ct = default)
            => GetMarkPriceCandlesticks_Async(instrumentId, period, after, before, limit, ct).Result;
        /// <summary>
        /// Retrieve the candlestick charts of mark price. This endpoint can retrieve the latest 1,440 data entries. Charts are returned in groups based on the requested bar.
        /// </summary>
        /// <param name="instrumentId">Instrument ID</param>
        /// <param name="period">Bar size, the default is 1m</param>
        /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
        /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
        /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<IEnumerable<OkexCandlestick>>> GetMarkPriceCandlesticks_Async(string instrumentId, OkexPeriod period, long? after = null, long? before = null, int limit = 100, CancellationToken ct = default)
        {
            if (limit < 1 || limit > 100)
                throw new ArgumentException("Limit can be between 1-100.");

            var parameters = new Dictionary<string, object>
            {
                { "instId", instrumentId },
                { "bar", JsonConvert.SerializeObject(period, new PeriodConverter(false)) },
            };
            parameters.AddOptionalParameter("after", after?.ToString());
            parameters.AddOptionalParameter("before", before?.ToString());
            parameters.AddOptionalParameter("limit", limit.ToString());

            var result = await SendRequestAsync<OkexRestApiResponse<IEnumerable<OkexCandlestick>>>(GetUrl(Endpoints_V5_Market_MarkPriceCandles), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<IEnumerable<OkexCandlestick>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<IEnumerable<OkexCandlestick>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage));

            foreach (var candle in result.Data.Data) candle.Instrument = instrumentId;
            return new WebCallResult<IEnumerable<OkexCandlestick>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }

        /// <summary>
        /// Retrieve the recent transactions of an instrument.
        /// </summary>
        /// <param name="instrumentId">Instrument ID</param>
        /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<IEnumerable<OkexTrade>> GetTrades(string instrumentId, int limit = 100, CancellationToken ct = default)
            => GetTrades_Async(instrumentId, limit, ct).Result;
        /// <summary>
        /// Retrieve the recent transactions of an instrument.
        /// </summary>
        /// <param name="instrumentId">Instrument ID</param>
        /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<IEnumerable<OkexTrade>>> GetTrades_Async(string instrumentId, int limit = 100, CancellationToken ct = default)
        {
            if (limit < 1 || limit > 500)
                throw new ArgumentException("Limit can be between 1-500.");

            var parameters = new Dictionary<string, object>
            {
                { "instId", instrumentId },
            };
            parameters.AddOptionalParameter("limit", limit.ToString());

            var result = await SendRequestAsync<OkexRestApiResponse<IEnumerable<OkexTrade>>>(GetUrl(Endpoints_V5_Market_Trades), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<IEnumerable<OkexTrade>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<IEnumerable<OkexTrade>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage));

            return new WebCallResult<IEnumerable<OkexTrade>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }

        /// <summary>
        /// The 24-hour trading volume is calculated on a rolling basis, using USD as the pricing unit.
        /// </summary>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<Okex24HourVolume> Get24HourVolume(CancellationToken ct = default)
            => Get24HourVolume_Async(ct).Result;
        /// <summary>
        /// The 24-hour trading volume is calculated on a rolling basis, using USD as the pricing unit.
        /// </summary>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<Okex24HourVolume>> Get24HourVolume_Async(CancellationToken ct = default)
        {
            var result = await SendRequestAsync<OkexRestApiResponse<IEnumerable<Okex24HourVolume>>>(GetUrl(Endpoints_V5_Market_Platform24Volume), HttpMethod.Get, ct).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<Okex24HourVolume>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<Okex24HourVolume>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage));

            return new WebCallResult<Okex24HourVolume>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data.FirstOrDefault(), null);
        }

        /// <summary>
        /// Get the crypto price of signing using Open Oracle smart contract.
        /// </summary>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<OkexOracle> GetOracle(CancellationToken ct = default)
            => GetOracle_Async(ct).Result;
        /// <summary>
        /// Get the crypto price of signing using Open Oracle smart contract.
        /// </summary>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<OkexOracle>> GetOracle_Async(CancellationToken ct = default)
        {
            var result = await SendRequestAsync<OkexRestApiResponse<IEnumerable<OkexOracle>>>(GetUrl(Endpoints_V5_Market_OpenOracle), HttpMethod.Get, ct).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<OkexOracle>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<OkexOracle>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage));

            return new WebCallResult<OkexOracle>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data.FirstOrDefault(), null);
        }

        /// <summary>
        /// Get the index component information data on the market
        /// </summary>
        /// <param name="index"></param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<OkexIndexComponents> GetIndexComponents(string index, CancellationToken ct = default)
            => GetIndexComponents_Async(index, ct).Result;
        /// <summary>
        /// Get the index component information data on the market
        /// </summary>
        /// <param name="index"></param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<OkexIndexComponents>> GetIndexComponents_Async(string index, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "index", index },
            };

            var result = await SendRequestAsync<OkexRestApiResponse<OkexIndexComponents>>(GetUrl(Endpoints_V5_Market_IndexComponents), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<OkexIndexComponents>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<OkexIndexComponents>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage));

            return new WebCallResult<OkexIndexComponents>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }

        #endregion
    }
}
