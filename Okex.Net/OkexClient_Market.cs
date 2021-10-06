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
        public virtual WebCallResult<IEnumerable<OkexTicker>> GetTickers(OkexInstrumentType instrumentType, string underlying = null, CancellationToken ct = default) 
            => GetTickers_Async(instrumentType, underlying, ct).Result;
        public virtual async Task<WebCallResult<IEnumerable<OkexTicker>>> GetTickers_Async(OkexInstrumentType instrumentType, string underlying = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)) },
            };
            parameters.AddOptionalParameter("uly", underlying);

            var result = await SendRequestAsync<OkexApiResponse<IEnumerable<OkexTicker>>>(GetUrl(Endpoints_V5_Market_Tickers), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<IEnumerable<OkexTicker>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<IEnumerable<OkexTicker>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage));

            return new WebCallResult<IEnumerable<OkexTicker>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }

        public virtual WebCallResult<OkexTicker> GetTicker(string instrumentId, CancellationToken ct = default) 
            => GetTicker_Async(instrumentId, ct).Result;
        public virtual async Task<WebCallResult<OkexTicker>> GetTicker_Async(string instrumentId, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "instId", instrumentId },
            };

            var result = await SendRequestAsync<OkexApiResponse<IEnumerable<OkexTicker>>>(GetUrl(Endpoints_V5_Market_Ticker), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<OkexTicker>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<OkexTicker>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage));

            return new WebCallResult<OkexTicker>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data.FirstOrDefault(), null);
        }

        public virtual WebCallResult<IEnumerable<OkexIndexTicker>> GetIndexTickers(string quoteCcurrency = null, string instrumentId = null, CancellationToken ct = default) 
            => GetIndexTickers_Async(quoteCcurrency, instrumentId, ct).Result;
        public virtual async Task<WebCallResult<IEnumerable<OkexIndexTicker>>> GetIndexTickers_Async(string quoteCcurrency = null, string instrumentId = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("quoteCcy", quoteCcurrency);
            parameters.AddOptionalParameter("instId", instrumentId);

            var result = await SendRequestAsync<OkexApiResponse<IEnumerable<OkexIndexTicker>>>(GetUrl(Endpoints_V5_Market_IndexTickers), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<IEnumerable<OkexIndexTicker>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<IEnumerable<OkexIndexTicker>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage));

            return new WebCallResult<IEnumerable<OkexIndexTicker>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }

        public virtual WebCallResult<OkexOrderBook> GetOrderBook(string instrumentId, int depth=1, CancellationToken ct = default) 
            => GetOrderBook_Async(instrumentId, depth, ct).Result;
        public virtual async Task<WebCallResult<OkexOrderBook>> GetOrderBook_Async(string instrumentId, int depth = 1, CancellationToken ct = default)
        {
            if (depth < 1 || depth > 400)
                throw new ArgumentException("Depth can be between 1-400.");

            var parameters = new Dictionary<string, object>
            {
                {"instId", instrumentId},
                {"sz", depth},
            };

            var result = await SendRequestAsync<OkexApiResponse<IEnumerable<OkexOrderBook>>>(GetUrl(Endpoints_V5_Market_Books), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<OkexOrderBook>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<OkexOrderBook>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage));

            return new WebCallResult<OkexOrderBook>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data.FirstOrDefault(), null);
        }

        public virtual WebCallResult<IEnumerable<OkexCandlestick>> GetCandlesticks(string instrumentId , OkexPeriod period, long? after = null, long? before = null, int limit = 100, CancellationToken ct = default) 
            => GetCandlesticks_Async(instrumentId, period, after, before,limit, ct).Result;
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

            var result = await SendRequestAsync<OkexApiResponse<IEnumerable<OkexCandlestick>>>(GetUrl(Endpoints_V5_Market_Candles), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<IEnumerable<OkexCandlestick>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<IEnumerable<OkexCandlestick>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage));

            foreach (var candle in result.Data.Data) candle.Instrument = instrumentId;
            return new WebCallResult<IEnumerable<OkexCandlestick>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }
        
        public virtual WebCallResult<IEnumerable<OkexCandlestick>> GetCandlesticksHistory(string instrumentId , OkexPeriod period, long? after = null, long? before = null, int limit = 100, CancellationToken ct = default) 
            => GetCandlesticksHistory_Async(instrumentId, period, after, before,limit, ct).Result;
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

            var result = await SendRequestAsync<OkexApiResponse<IEnumerable<OkexCandlestick>>>(GetUrl(Endpoints_V5_Market_HistoryCandles), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<IEnumerable<OkexCandlestick>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<IEnumerable<OkexCandlestick>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage));

            foreach (var candle in result.Data.Data) candle.Instrument = instrumentId;
            return new WebCallResult<IEnumerable<OkexCandlestick>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }

        public virtual WebCallResult<IEnumerable<OkexCandlestick>> GetIndexCandlesticks(string instrumentId, OkexPeriod period, long? after = null, long? before = null, int limit = 100, CancellationToken ct = default) 
            => GetIndexCandlesticks_Async(instrumentId, period, after, before, limit, ct).Result;
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

            var result = await SendRequestAsync<OkexApiResponse<IEnumerable<OkexCandlestick>>>(GetUrl(Endpoints_V5_Market_IndexCandles), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<IEnumerable<OkexCandlestick>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<IEnumerable<OkexCandlestick>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage));

            foreach (var candle in result.Data.Data) candle.Instrument = instrumentId;
            return new WebCallResult<IEnumerable<OkexCandlestick>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }
        
        public virtual WebCallResult<IEnumerable<OkexCandlestick>> GetMarkPriceCandlesticks(string instrumentId, OkexPeriod period, long? after = null, long? before = null, int limit = 100, CancellationToken ct = default) 
            => GetMarkPriceCandlesticks_Async(instrumentId, period, after, before, limit, ct).Result;
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

            var result = await SendRequestAsync<OkexApiResponse<IEnumerable<OkexCandlestick>>>(GetUrl(Endpoints_V5_Market_MarkPriceCandles), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<IEnumerable<OkexCandlestick>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<IEnumerable<OkexCandlestick>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage));

            foreach (var candle in result.Data.Data) candle.Instrument = instrumentId;
            return new WebCallResult<IEnumerable<OkexCandlestick>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }
        
        public virtual WebCallResult<IEnumerable<OkexTrade>> GetTrades(string instrumentId, int limit = 100, CancellationToken ct = default) 
            => GetTrades_Async(instrumentId,  limit, ct).Result;
        public virtual async Task<WebCallResult<IEnumerable<OkexTrade>>> GetTrades_Async(string instrumentId, int limit = 100, CancellationToken ct = default)
        {
            if (limit < 1 || limit > 100)
                throw new ArgumentException("Limit can be between 1-500.");

            var parameters = new Dictionary<string, object>
            {
                { "instId", instrumentId },
            };
            parameters.AddOptionalParameter("limit", limit.ToString());

            var result = await SendRequestAsync<OkexApiResponse<IEnumerable<OkexTrade>>>(GetUrl(Endpoints_V5_Market_Trades), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<IEnumerable<OkexTrade>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<IEnumerable<OkexTrade>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage));

            return new WebCallResult<IEnumerable<OkexTrade>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }
        
        public virtual WebCallResult<Okex24HourVolume> Get24HourVolume(CancellationToken ct = default) 
            => Get24HourVolume_Async(ct).Result;
        public virtual async Task<WebCallResult<Okex24HourVolume>> Get24HourVolume_Async(CancellationToken ct = default)
        {
            var result = await SendRequestAsync<OkexApiResponse<IEnumerable<Okex24HourVolume>>>(GetUrl(Endpoints_V5_Market_Platform24Volume), HttpMethod.Get, ct).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<Okex24HourVolume>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<Okex24HourVolume>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage));

            return new WebCallResult<Okex24HourVolume>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data.FirstOrDefault(), null);
        }
        
        public virtual WebCallResult<OkexOracle> GetOracle(CancellationToken ct = default) 
            => GetOracle_Async(ct).Result;
        public virtual async Task<WebCallResult<OkexOracle>> GetOracle_Async(CancellationToken ct = default)
        {
            var result = await SendRequestAsync<OkexApiResponse<IEnumerable<OkexOracle>>>(GetUrl(Endpoints_V5_Market_OpenOracle), HttpMethod.Get, ct).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<OkexOracle>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<OkexOracle>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage));

            return new WebCallResult<OkexOracle>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data.FirstOrDefault(), null);
        }

        public virtual WebCallResult<OkexIndexComponents> GetIndexComponents(string index, CancellationToken ct = default)
            => GetIndexComponents_Async(index, ct).Result;
        public virtual async Task<WebCallResult<OkexIndexComponents>> GetIndexComponents_Async(string index, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "index", index },
            };

            var result = await SendRequestAsync<OkexApiResponse<OkexIndexComponents>>(GetUrl(Endpoints_V5_Market_IndexComponents), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<OkexIndexComponents>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<OkexIndexComponents>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage));

            return new WebCallResult<OkexIndexComponents>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }

        #endregion
    }
}