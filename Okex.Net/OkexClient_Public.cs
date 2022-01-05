using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using Newtonsoft.Json;
using Okex.Net.Converters;
using Okex.Net.CoreObjects;
using Okex.Net.Enums;
using Okex.Net.Helpers;
using Okex.Net.RestObjects.Public;
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
        #region Public API Endpoints
        /// <summary>
        /// Retrieve a list of instruments with open contracts.
        /// </summary>
        /// <param name="instrumentType">Instrument Type</param>
        /// <param name="underlying">Underlying</param>
        /// <param name="instrumentId">Instrument ID</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<IEnumerable<OkexInstrument>> GetInstruments(OkexInstrumentType instrumentType, string underlying = null, string instrumentId = null, CancellationToken ct = default) => GetInstruments_Async(instrumentType, underlying, instrumentId, ct).Result;
        /// <summary>
        /// Retrieve a list of instruments with open contracts.
        /// </summary>
        /// <param name="instrumentType">Instrument Type</param>
        /// <param name="underlying">Underlying</param>
        /// <param name="instrumentId">Instrument ID</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<IEnumerable<OkexInstrument>>> GetInstruments_Async(OkexInstrumentType instrumentType, string underlying = null, string instrumentId = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)) },
            };
            parameters.AddOptionalParameter("uly", underlying);
            parameters.AddOptionalParameter("instId", instrumentId);

            var result = await SendRequestAsync<OkexRestApiResponse<IEnumerable<OkexInstrument>>>(GetUrl(Endpoints_V5_Public_Instruments), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<IEnumerable<OkexInstrument>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<IEnumerable<OkexInstrument>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage, result.Data.Data));

            return new WebCallResult<IEnumerable<OkexInstrument>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }

        /// <summary>
        /// Retrieve the estimated delivery price, which will only have a return value one hour before the delivery/exercise.
        /// </summary>
        /// <param name="instrumentType">Instrument Type</param>
        /// <param name="underlying">Underlying</param>
        /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
        /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
        /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<IEnumerable<OkexDeliveryExerciseHistory>> GetDeliveryExerciseHistory(OkexInstrumentType instrumentType, string underlying, long? after = null, long? before = null, int limit = 100, CancellationToken ct = default) => GetDeliveryExerciseHistory_Async(instrumentType, underlying, after, before, limit, ct).Result;
        /// <summary>
        /// Retrieve the estimated delivery price, which will only have a return value one hour before the delivery/exercise.
        /// </summary>
        /// <param name="instrumentType">Instrument Type</param>
        /// <param name="underlying">Underlying</param>
        /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
        /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
        /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<IEnumerable<OkexDeliveryExerciseHistory>>> GetDeliveryExerciseHistory_Async(OkexInstrumentType instrumentType, string underlying, long? after = null, long? before = null, int limit = 100, CancellationToken ct = default)
        {
            if (instrumentType.IsNotIn(OkexInstrumentType.Futures, OkexInstrumentType.Option))
                throw new ArgumentException("Instrument Type can be only Futures or Option.");

            if (limit < 1 || limit > 100)
                throw new ArgumentException("Limit can be between 1-100.");

            var parameters = new Dictionary<string, object>
            {
                { "instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)) },
                { "uly", underlying },
            };
            parameters.AddOptionalParameter("after", after?.ToString());
            parameters.AddOptionalParameter("before", before?.ToString());
            parameters.AddOptionalParameter("limit", limit.ToString());

            var result = await SendRequestAsync<OkexRestApiResponse<IEnumerable<OkexDeliveryExerciseHistory>>>(GetUrl(Endpoints_V5_Public_DeliveryExerciseHistory), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<IEnumerable<OkexDeliveryExerciseHistory>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<IEnumerable<OkexDeliveryExerciseHistory>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage, result.Data.Data));

            return new WebCallResult<IEnumerable<OkexDeliveryExerciseHistory>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }

        /// <summary>
        /// Retrieve the total open interest for contracts on OKEx.
        /// </summary>
        /// <param name="instrumentType">Instrument Type</param>
        /// <param name="underlying">Underlying</param>
        /// <param name="instrumentId">Instrument ID</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<IEnumerable<OkexOpenInterest>> GetOpenInterests(OkexInstrumentType instrumentType, string underlying = null, string instrumentId = null, CancellationToken ct = default) => GetOpenInterests_Async(instrumentType, underlying, instrumentId, ct).Result;
        /// <summary>
        /// Retrieve the total open interest for contracts on OKEx.
        /// </summary>
        /// <param name="instrumentType">Instrument Type</param>
        /// <param name="underlying">Underlying</param>
        /// <param name="instrumentId">Instrument ID</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<IEnumerable<OkexOpenInterest>>> GetOpenInterests_Async(OkexInstrumentType instrumentType, string underlying = null, string instrumentId = null, CancellationToken ct = default)
        {
            if (instrumentType.IsNotIn(OkexInstrumentType.Futures, OkexInstrumentType.Option, OkexInstrumentType.Swap))
                throw new ArgumentException("Instrument Type can be only Futures, Option or Swap.");

            if (instrumentType == OkexInstrumentType.Swap && string.IsNullOrEmpty(underlying))
                throw new ArgumentException("Underlying is required for Option.");

            var parameters = new Dictionary<string, object>
            {
                { "instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)) },
            };
            parameters.AddOptionalParameter("uly", underlying);
            parameters.AddOptionalParameter("instId", instrumentId);

            var result = await SendRequestAsync<OkexRestApiResponse<IEnumerable<OkexOpenInterest>>>(GetUrl(Endpoints_V5_Public_OpenInterest), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<IEnumerable<OkexOpenInterest>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<IEnumerable<OkexOpenInterest>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage, result.Data.Data));

            return new WebCallResult<IEnumerable<OkexOpenInterest>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }

        /// <summary>
        /// Retrieve funding rate.
        /// </summary>
        /// <param name="instrumentId">Instrument ID</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<IEnumerable<OkexFundingRate>> GetFundingRates(string instrumentId, CancellationToken ct = default) => GetFundingRates_Async(instrumentId, ct).Result;
        /// <summary>
        /// Retrieve funding rate.
        /// </summary>
        /// <param name="instrumentId">Instrument ID</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<IEnumerable<OkexFundingRate>>> GetFundingRates_Async(string instrumentId, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "instId", instrumentId },
            };

            var result = await SendRequestAsync<OkexRestApiResponse<IEnumerable<OkexFundingRate>>>(GetUrl(Endpoints_V5_Public_FundingRate), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<IEnumerable<OkexFundingRate>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<IEnumerable<OkexFundingRate>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage, result.Data.Data));

            return new WebCallResult<IEnumerable<OkexFundingRate>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }

        /// <summary>
        /// Retrieve funding rate history. This endpoint can retrieve data from the last 3 months.
        /// </summary>
        /// <param name="instrumentId">Instrument ID</param>
        /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
        /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
        /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<IEnumerable<OkexFundingRateHistory>> GetFundingRateHistory(string instrumentId, long? after = null, long? before = null, int limit = 100, CancellationToken ct = default) => GetFundingRateHistory_Async(instrumentId, after, before, limit, ct).Result;
        /// <summary>
        /// Retrieve funding rate history. This endpoint can retrieve data from the last 3 months.
        /// </summary>
        /// <param name="instrumentId">Instrument ID</param>
        /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
        /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
        /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<IEnumerable<OkexFundingRateHistory>>> GetFundingRateHistory_Async(string instrumentId, long? after = null, long? before = null, int limit = 100, CancellationToken ct = default)
        {
            if (limit < 1 || limit > 100)
                throw new ArgumentException("Limit can be between 1-100.");

            var parameters = new Dictionary<string, object>
            {
                { "instId", instrumentId },
            };
            parameters.AddOptionalParameter("after", after?.ToString());
            parameters.AddOptionalParameter("before", before?.ToString());
            parameters.AddOptionalParameter("limit", limit.ToString());

            var result = await SendRequestAsync<OkexRestApiResponse<IEnumerable<OkexFundingRateHistory>>>(GetUrl(Endpoints_V5_Public_FundingRateHistory), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<IEnumerable<OkexFundingRateHistory>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<IEnumerable<OkexFundingRateHistory>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage, result.Data.Data));

            return new WebCallResult<IEnumerable<OkexFundingRateHistory>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }

        /// <summary>
        /// Retrieve the highest buy limit and lowest sell limit of the instrument.
        /// </summary>
        /// <param name="instrumentId">Instrument ID</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<OkexLimitPrice> GetLimitPrice(string instrumentId, CancellationToken ct = default) => GetLimitPrice_Async(instrumentId, ct).Result;
        /// <summary>
        /// Retrieve the highest buy limit and lowest sell limit of the instrument.
        /// </summary>
        /// <param name="instrumentId">Instrument ID</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<OkexLimitPrice>> GetLimitPrice_Async(string instrumentId, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "instId", instrumentId },
            };

            var result = await SendRequestAsync<OkexRestApiResponse<IEnumerable<OkexLimitPrice>>>(GetUrl(Endpoints_V5_Public_PriceLimit), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<OkexLimitPrice>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<OkexLimitPrice>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage, result.Data.Data));

            return new WebCallResult<OkexLimitPrice>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data.FirstOrDefault(), null);
        }

        /// <summary>
        /// Retrieve option market data.
        /// </summary>
        /// <param name="underlying">Underlying</param>
        /// <param name="expiryDate">Contract expiry date, the format is "YYMMDD", e.g. "200527"</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<IEnumerable<OkexOptionSummary>> GetOptionMarketData(string underlying, DateTime? expiryDate = null, CancellationToken ct = default) => GetOptionMarketData_Async(underlying, expiryDate, ct).Result;
        /// <summary>
        /// Retrieve option market data.
        /// </summary>
        /// <param name="underlying">Underlying</param>
        /// <param name="expiryDate">Contract expiry date, the format is "YYMMDD", e.g. "200527"</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<IEnumerable<OkexOptionSummary>>> GetOptionMarketData_Async(string underlying, DateTime? expiryDate = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "uly", underlying },
            };
            parameters.AddOptionalParameter("expTime", expiryDate?.ToString("yyMMdd"));

            var result = await SendRequestAsync<OkexRestApiResponse<IEnumerable<OkexOptionSummary>>>(GetUrl(Endpoints_V5_Public_OptionSummary), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<IEnumerable<OkexOptionSummary>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<IEnumerable<OkexOptionSummary>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage, result.Data.Data));

            return new WebCallResult<IEnumerable<OkexOptionSummary>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }

        /// <summary>
        /// Retrieve the estimated delivery price which will only have a return value one hour before the delivery/exercise.
        /// </summary>
        /// <param name="instrumentId">Instrument ID</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<OkexEstimatedPrice> GetEstimatedPrice(string instrumentId, CancellationToken ct = default) => GetEstimatedPrice_Async(instrumentId, ct).Result;
        /// <summary>
        /// Retrieve the estimated delivery price which will only have a return value one hour before the delivery/exercise.
        /// </summary>
        /// <param name="instrumentId">Instrument ID</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<OkexEstimatedPrice>> GetEstimatedPrice_Async(string instrumentId, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "instId", instrumentId },
            };

            var result = await SendRequestAsync<OkexRestApiResponse<IEnumerable<OkexEstimatedPrice>>>(GetUrl(Endpoints_V5_Public_EstimatedPrice), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<OkexEstimatedPrice>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<OkexEstimatedPrice>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage, result.Data.Data));

            return new WebCallResult<OkexEstimatedPrice>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data.FirstOrDefault(), null);
        }

        /// <summary>
        /// Retrieve discount rate level and interest-free quota.
        /// </summary>
        /// <param name="discountLevel">Discount level</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<IEnumerable<OkexDiscountInfo>> GetDiscountInfo(int? discountLevel = null, CancellationToken ct = default) => GetDiscountInfo_Async(discountLevel, ct).Result;
        /// <summary>
        /// Retrieve discount rate level and interest-free quota.
        /// </summary>
        /// <param name="discountLevel">Discount level</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<IEnumerable<OkexDiscountInfo>>> GetDiscountInfo_Async(int? discountLevel = null, CancellationToken ct = default)
        {

            if (discountLevel.HasValue && (discountLevel < 1 || discountLevel > 5))
                throw new ArgumentException("Limit can be between 1-5.");

            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("discountLv", discountLevel?.ToString());

            var result = await SendRequestAsync<OkexRestApiResponse<IEnumerable<OkexDiscountInfo>>>(GetUrl(Endpoints_V5_Public_DiscountRateInterestFreeQuota), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<IEnumerable<OkexDiscountInfo>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<IEnumerable<OkexDiscountInfo>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage, result.Data.Data));

            return new WebCallResult<IEnumerable<OkexDiscountInfo>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }

        /// <summary>
        /// Retrieve API server time.
        /// </summary>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<DateTime> GetSystemTime(CancellationToken ct = default) => GetSystemTime_Async(ct).Result;
        /// <summary>
        /// Retrieve API server time.
        /// </summary>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<DateTime>> GetSystemTime_Async(CancellationToken ct = default)
        {
            var result = await SendRequestAsync<OkexRestApiResponse<IEnumerable<OkexTime>>>(GetUrl(Endpoints_V5_Public_Time), HttpMethod.Get, ct).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<DateTime>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<DateTime>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage, result.Data.Data));

            return new WebCallResult<DateTime>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data.FirstOrDefault().Time, null);
        }

        /// <summary>
        /// Retrieve information on liquidation orders in the last 1 days.
        /// </summary>
        /// <param name="instrumentType">Instrument Type</param>
        /// <param name="marginMode">Margin Mode</param>
        /// <param name="instrumentId">Instrument ID</param>
        /// <param name="currency">Currency</param>
        /// <param name="underlying">Underlying</param>
        /// <param name="alias">Alias</param>
        /// <param name="state">State</param>
        /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
        /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
        /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<IEnumerable<OkexLiquidationInfo>> GetLiquidationOrders(
            OkexInstrumentType instrumentType,
            OkexMarginMode? marginMode = null,
            string instrumentId = null,
            string currency = null,
            string underlying = null,
            OkexInstrumentAlias? alias = null,
            OkexLiquidationState? state = null,
            long? after = null, long? before = null, int limit = 100,
            CancellationToken ct = default) => GetLiquidationOrders_Async(
            instrumentType,
            marginMode,
            instrumentId,
            currency,
            underlying,
            alias,
            state,
            after, before, limit,
            ct).Result;
        /// <summary>
        /// Retrieve information on liquidation orders in the last 1 days.
        /// </summary>
        /// <param name="instrumentType">Instrument Type</param>
        /// <param name="marginMode">Margin Mode</param>
        /// <param name="instrumentId">Instrument ID</param>
        /// <param name="currency">Currency</param>
        /// <param name="underlying">Underlying</param>
        /// <param name="alias">Alias</param>
        /// <param name="state">State</param>
        /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
        /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
        /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<IEnumerable<OkexLiquidationInfo>>> GetLiquidationOrders_Async(
            OkexInstrumentType instrumentType,
            OkexMarginMode? marginMode = null,
            string instrumentId = null,
            string currency = null,
            string underlying = null,
            OkexInstrumentAlias? alias = null,
            OkexLiquidationState? state = null,
            long? after = null, long? before = null, int limit = 100,
            CancellationToken ct = default)
        {
            if (instrumentType.IsIn(OkexInstrumentType.Futures, OkexInstrumentType.Swap, OkexInstrumentType.Option) && string.IsNullOrEmpty(underlying))
                throw new ArgumentException("Underlying is required.");

            if (instrumentType.IsIn(OkexInstrumentType.Futures, OkexInstrumentType.Swap) && state == null)
                throw new ArgumentException("State is required.");

            if (instrumentType.IsIn(OkexInstrumentType.Futures) && alias == null)
                throw new ArgumentException("Alias is required.");

            if (limit < 1 || limit > 100)
                throw new ArgumentException("Limit can be between 1-100.");

            var parameters = new Dictionary<string, object>
            {
                { "instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)) },
            };
            if (marginMode != null)
                parameters.AddOptionalParameter("mgnMode", JsonConvert.SerializeObject(marginMode, new MarginModeConverter(false)));
            parameters.AddOptionalParameter("instId", instrumentId);
            parameters.AddOptionalParameter("ccy", currency);
            parameters.AddOptionalParameter("uly", underlying);
            if (alias != null)
                parameters.AddOptionalParameter("alias", JsonConvert.SerializeObject(alias, new InstrumentAliasConverter(false)));
            if (state != null)
                parameters.AddOptionalParameter("state", JsonConvert.SerializeObject(state, new LiquidationStateConverter(false)));
            parameters.AddOptionalParameter("after", after?.ToString());
            parameters.AddOptionalParameter("before", before?.ToString());
            parameters.AddOptionalParameter("limit", limit.ToString());

            var result = await SendRequestAsync<OkexRestApiResponse<IEnumerable<OkexLiquidationInfo>>>(GetUrl(Endpoints_V5_Public_LiquidationOrders), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<IEnumerable<OkexLiquidationInfo>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<IEnumerable<OkexLiquidationInfo>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage, result.Data.Data));

            return new WebCallResult<IEnumerable<OkexLiquidationInfo>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }

        /// <summary>
        /// Retrieve mark price.
        /// We set the mark price based on the SPOT index and at a reasonable basis to prevent individual users from manipulating the market and causing the contract price to fluctuate.
        /// </summary>
        /// <param name="instrumentType">Instrument Type</param>
        /// <param name="underlying">Underlying</param>
        /// <param name="instrumentId">Instrument ID</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<IEnumerable<OkexMarkPrice>> GetMarkPrices(OkexInstrumentType instrumentType, string underlying = null, string instrumentId = null, CancellationToken ct = default) => GetMarkPrices_Async(instrumentType, underlying, instrumentId, ct).Result;
        /// <summary>
        /// Retrieve mark price.
        /// We set the mark price based on the SPOT index and at a reasonable basis to prevent individual users from manipulating the market and causing the contract price to fluctuate.
        /// </summary>
        /// <param name="instrumentType">Instrument Type</param>
        /// <param name="underlying">Underlying</param>
        /// <param name="instrumentId">Instrument ID</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<IEnumerable<OkexMarkPrice>>> GetMarkPrices_Async(OkexInstrumentType instrumentType, string underlying = null, string instrumentId = null, CancellationToken ct = default)
        {
            if (instrumentType.IsNotIn(OkexInstrumentType.Margin, OkexInstrumentType.Futures, OkexInstrumentType.Option, OkexInstrumentType.Swap))
                throw new ArgumentException("Instrument Type can be only Margin, Futures, Option or Swap.");

            var parameters = new Dictionary<string, object>
            {
                { "instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)) },
            };
            parameters.AddOptionalParameter("uly", underlying);
            parameters.AddOptionalParameter("instId", instrumentId);

            var result = await SendRequestAsync<OkexRestApiResponse<IEnumerable<OkexMarkPrice>>>(GetUrl(Endpoints_V5_Public_MarkPrice), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<IEnumerable<OkexMarkPrice>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<IEnumerable<OkexMarkPrice>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage, result.Data.Data));

            return new WebCallResult<IEnumerable<OkexMarkPrice>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }

        /// <summary>
        /// Position information，Maximum leverage depends on your borrowings and margin ratio.
        /// </summary>
        /// <param name="instrumentType">Instrument Type</param>
        /// <param name="marginMode">Margin Mode</param>
        /// <param name="underlying">Underlying</param>
        /// <param name="instrumentId">Instrument ID</param>
        /// <param name="tier"></param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<IEnumerable<OkexPositionTier>> GetPositionTiers(
            OkexInstrumentType instrumentType,
            OkexMarginMode marginMode,
            string underlying,
            string instrumentId = null,
            string tier = null,
            CancellationToken ct = default) => GetPositionTiers_Async(instrumentType, marginMode, underlying, instrumentId, tier, ct).Result;
        /// <summary>
        /// Position information，Maximum leverage depends on your borrowings and margin ratio.
        /// </summary>
        /// <param name="instrumentType">Instrument Type</param>
        /// <param name="marginMode">Margin Mode</param>
        /// <param name="underlying">Underlying</param>
        /// <param name="instrumentId">Instrument ID</param>
        /// <param name="tier"></param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<IEnumerable<OkexPositionTier>>> GetPositionTiers_Async(
            OkexInstrumentType instrumentType,
            OkexMarginMode marginMode,
            string underlying,
            string instrumentId = null,
            string tier = null,
            CancellationToken ct = default)
        {
            if (instrumentType.IsNotIn(OkexInstrumentType.Margin, OkexInstrumentType.Futures, OkexInstrumentType.Option, OkexInstrumentType.Swap))
                throw new ArgumentException("Instrument Type can be only Margin, Futures, Option or Swap.");

            var parameters = new Dictionary<string, object>
            {
                { "instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)) },
                { "tdMode", JsonConvert.SerializeObject(marginMode, new MarginModeConverter(false)) },
            };
            parameters.AddOptionalParameter("uly", underlying);
            parameters.AddOptionalParameter("instId", instrumentId);
            parameters.AddOptionalParameter("tier", tier);

            var result = await SendRequestAsync<OkexRestApiResponse<IEnumerable<OkexPositionTier>>>(GetUrl(Endpoints_V5_Public_PositionTiers), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<IEnumerable<OkexPositionTier>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<IEnumerable<OkexPositionTier>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage, result.Data.Data));

            return new WebCallResult<IEnumerable<OkexPositionTier>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }

        /// <summary>
        /// Get margin interest rate
        /// </summary>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<OkexInterestRate> GetInterestRates(CancellationToken ct = default) => GetInterestRates_Async(ct).Result;
        /// <summary>
        /// Get margin interest rate
        /// </summary>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<OkexInterestRate>> GetInterestRates_Async(CancellationToken ct = default)
        {
            var result = await SendRequestAsync<OkexRestApiResponse<IEnumerable<OkexInterestRate>>>(GetUrl(Endpoints_V5_Public_InterestRateLoanQuota), HttpMethod.Get, ct).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<OkexInterestRate>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<OkexInterestRate>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage, result.Data.Data));

            return new WebCallResult<OkexInterestRate>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data.FirstOrDefault(), null);
        }

        /// <summary>
        /// Get Underlying
        /// </summary>
        /// <param name="instrumentType">Instrument Type</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<IEnumerable<string>> GetUnderlying(OkexInstrumentType instrumentType, CancellationToken ct = default) => GetUnderlying_Async(instrumentType, ct).Result;
        /// <summary>
        /// Get Underlying
        /// </summary>
        /// <param name="instrumentType">Instrument Type</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<IEnumerable<string>>> GetUnderlying_Async(OkexInstrumentType instrumentType, CancellationToken ct = default)
        {
            if (instrumentType.IsNotIn(OkexInstrumentType.Futures, OkexInstrumentType.Option, OkexInstrumentType.Swap))
                throw new ArgumentException("Instrument Type can be only Futures, Option or Swap.");

            var parameters = new Dictionary<string, object>
            {
                { "instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)) },
            };
            var result = await SendRequestAsync<OkexRestApiResponse<IEnumerable<IEnumerable<string>>>>(GetUrl(Endpoints_V5_Public_Underlying), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<IEnumerable<string>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<IEnumerable<string>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage, result.Data.Data));

            return new WebCallResult<IEnumerable<string>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data.FirstOrDefault(), null);
        }

        #endregion
    }
}