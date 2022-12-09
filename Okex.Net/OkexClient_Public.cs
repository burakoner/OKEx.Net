namespace Okex.Net;

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
    public virtual WebCallResult<IEnumerable<OkexInstrument>> GetInstruments(OkexInstrumentType instrumentType, string underlying = null, string instrumentId = null, CancellationToken ct = default)
        => GetInstrumentsAsync(instrumentType, underlying, instrumentId, ct).Result;
    /// <summary>
    /// Retrieve a list of instruments with open contracts.
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="underlying">Underlying</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<WebCallResult<IEnumerable<OkexInstrument>>> GetInstrumentsAsync(OkexInstrumentType instrumentType, string underlying = null, string instrumentId = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)) },
        };
        if (!string.IsNullOrEmpty(underlying)) parameters.AddOptionalParameter("uly", underlying);
        if (!string.IsNullOrEmpty(instrumentId)) parameters.AddOptionalParameter("instId", instrumentId);

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexInstrument>>>(UnifiedApi.GetUri(Endpoints_V5_Public_Instruments), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OkexInstrument>>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OkexInstrument>>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data);
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
    public virtual WebCallResult<IEnumerable<OkexDeliveryExerciseHistory>> GetDeliveryExerciseHistory(OkexInstrumentType instrumentType, string underlying, long? after = null, long? before = null, int limit = 100, CancellationToken ct = default)
        => GetDeliveryExerciseHistoryAsync(instrumentType, underlying, after, before, limit, ct).Result;
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
    public virtual async Task<WebCallResult<IEnumerable<OkexDeliveryExerciseHistory>>> GetDeliveryExerciseHistoryAsync(OkexInstrumentType instrumentType, string underlying, long? after = null, long? before = null, int limit = 100, CancellationToken ct = default)
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

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexDeliveryExerciseHistory>>>(UnifiedApi.GetUri(Endpoints_V5_Public_DeliveryExerciseHistory), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OkexDeliveryExerciseHistory>>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OkexDeliveryExerciseHistory>>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data);
    }

    /// <summary>
    /// Retrieve the total open interest for contracts on OKEx.
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="underlying">Underlying</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual WebCallResult<IEnumerable<OkexOpenInterest>> GetOpenInterests(OkexInstrumentType instrumentType, string underlying = null, string instrumentId = null, CancellationToken ct = default)
        => GetOpenInterestsAsync(instrumentType, underlying, instrumentId, ct).Result;
    /// <summary>
    /// Retrieve the total open interest for contracts on OKEx.
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="underlying">Underlying</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<WebCallResult<IEnumerable<OkexOpenInterest>>> GetOpenInterestsAsync(OkexInstrumentType instrumentType, string underlying = null, string instrumentId = null, CancellationToken ct = default)
    {
        if (instrumentType.IsNotIn(OkexInstrumentType.Futures, OkexInstrumentType.Option, OkexInstrumentType.Swap))
            throw new ArgumentException("Instrument Type can be only Futures, Option or Swap.");

        if (instrumentType == OkexInstrumentType.Swap && string.IsNullOrEmpty(underlying))
            throw new ArgumentException("Underlying is required for Option.");

        var parameters = new Dictionary<string, object>
        {
            { "instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)) },
        };
        if (!string.IsNullOrEmpty(underlying)) parameters.AddOptionalParameter("uly", underlying);
        if (!string.IsNullOrEmpty(instrumentId)) parameters.AddOptionalParameter("instId", instrumentId);

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexOpenInterest>>>(UnifiedApi.GetUri(Endpoints_V5_Public_OpenInterest), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OkexOpenInterest>>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OkexOpenInterest>>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data);
    }

    /// <summary>
    /// Retrieve funding rate.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual WebCallResult<IEnumerable<OkexFundingRate>> GetFundingRates(string instrumentId, CancellationToken ct = default)
        => GetFundingRatesAsync(instrumentId, ct).Result;
    /// <summary>
    /// Retrieve funding rate.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<WebCallResult<IEnumerable<OkexFundingRate>>> GetFundingRatesAsync(string instrumentId, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "instId", instrumentId },
        };

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexFundingRate>>>(UnifiedApi.GetUri(Endpoints_V5_Public_FundingRate), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OkexFundingRate>>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OkexFundingRate>>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data);
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
    public virtual WebCallResult<IEnumerable<OkexFundingRateHistory>> GetFundingRateHistory(string instrumentId, long? after = null, long? before = null, int limit = 100, CancellationToken ct = default)
        => GetFundingRateHistoryAsync(instrumentId, after, before, limit, ct).Result;
    /// <summary>
    /// Retrieve funding rate history. This endpoint can retrieve data from the last 3 months.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<WebCallResult<IEnumerable<OkexFundingRateHistory>>> GetFundingRateHistoryAsync(string instrumentId, long? after = null, long? before = null, int limit = 100, CancellationToken ct = default)
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

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexFundingRateHistory>>>(UnifiedApi.GetUri(Endpoints_V5_Public_FundingRateHistory), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OkexFundingRateHistory>>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OkexFundingRateHistory>>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data);
    }

    /// <summary>
    /// Retrieve the highest buy limit and lowest sell limit of the instrument.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual WebCallResult<OkexLimitPrice> GetLimitPrice(string instrumentId, CancellationToken ct = default)
        => GetLimitPriceAsync(instrumentId, ct).Result;
    /// <summary>
    /// Retrieve the highest buy limit and lowest sell limit of the instrument.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<WebCallResult<OkexLimitPrice>> GetLimitPriceAsync(string instrumentId, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "instId", instrumentId },
        };

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexLimitPrice>>>(UnifiedApi.GetUri(Endpoints_V5_Public_PriceLimit), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<OkexLimitPrice>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<OkexLimitPrice>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data.FirstOrDefault());
    }

    /// <summary>
    /// Retrieve option market data.
    /// </summary>
    /// <param name="underlying">Underlying</param>
    /// <param name="expiryDate">Contract expiry date, the format is "YYMMDD", e.g. "200527"</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual WebCallResult<IEnumerable<OkexOptionSummary>> GetOptionMarketData(string underlying, DateTime? expiryDate = null, CancellationToken ct = default)
        => GetOptionMarketDataAsync(underlying, expiryDate, ct).Result;
    /// <summary>
    /// Retrieve option market data.
    /// </summary>
    /// <param name="underlying">Underlying</param>
    /// <param name="expiryDate">Contract expiry date, the format is "YYMMDD", e.g. "200527"</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<WebCallResult<IEnumerable<OkexOptionSummary>>> GetOptionMarketDataAsync(string underlying, DateTime? expiryDate = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "uly", underlying },
        };
        parameters.AddOptionalParameter("expTime", expiryDate?.ToString("yyMMdd"));

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexOptionSummary>>>(UnifiedApi.GetUri(Endpoints_V5_Public_OptionSummary), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OkexOptionSummary>>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OkexOptionSummary>>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data);
    }

    /// <summary>
    /// Retrieve the estimated delivery price which will only have a return value one hour before the delivery/exercise.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual WebCallResult<OkexEstimatedPrice> GetEstimatedPrice(string instrumentId, CancellationToken ct = default)
        => GetEstimatedPriceAsync(instrumentId, ct).Result;
    /// <summary>
    /// Retrieve the estimated delivery price which will only have a return value one hour before the delivery/exercise.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<WebCallResult<OkexEstimatedPrice>> GetEstimatedPriceAsync(string instrumentId, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "instId", instrumentId },
        };

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexEstimatedPrice>>>(UnifiedApi.GetUri(Endpoints_V5_Public_EstimatedPrice), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<OkexEstimatedPrice>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<OkexEstimatedPrice>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data.FirstOrDefault());
    }

    /// <summary>
    /// Retrieve discount rate level and interest-free quota.
    /// </summary>
    /// <param name="discountLevel">Discount level</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual WebCallResult<IEnumerable<OkexDiscountInfo>> GetDiscountInfo(int? discountLevel = null, CancellationToken ct = default)
        => GetDiscountInfoAsync(discountLevel, ct).Result;
    /// <summary>
    /// Retrieve discount rate level and interest-free quota.
    /// </summary>
    /// <param name="discountLevel">Discount level</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<WebCallResult<IEnumerable<OkexDiscountInfo>>> GetDiscountInfoAsync(int? discountLevel = null, CancellationToken ct = default)
    {

        if (discountLevel.HasValue && (discountLevel < 1 || discountLevel > 5))
            throw new ArgumentException("Limit can be between 1-5.");

        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("discountLv", discountLevel?.ToString());

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexDiscountInfo>>>(UnifiedApi.GetUri(Endpoints_V5_Public_DiscountRateInterestFreeQuota), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OkexDiscountInfo>>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OkexDiscountInfo>>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data);
    }

    /// <summary>
    /// Retrieve API server time.
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual WebCallResult<DateTime> GetSystemTime(CancellationToken ct = default)
        => GetSystemTimeAsync(ct).Result;
    /// <summary>
    /// Retrieve API server time.
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<WebCallResult<DateTime>> GetSystemTimeAsync(CancellationToken ct = default)
    {
        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexTime>>>(UnifiedApi.GetUri(Endpoints_V5_Public_Time), HttpMethod.Get, ct).ConfigureAwait(false);
        if (!result.Success) return result.AsError<DateTime>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<DateTime>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data.FirstOrDefault().Time);
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
        CancellationToken ct = default)
        => GetLiquidationOrdersAsync(
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
    public virtual async Task<WebCallResult<IEnumerable<OkexLiquidationInfo>>> GetLiquidationOrdersAsync(
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
        if (!string.IsNullOrEmpty(instrumentId))
            parameters.AddOptionalParameter("instId", instrumentId);
        if (!string.IsNullOrEmpty(currency))
            parameters.AddOptionalParameter("ccy", currency);
        if (!string.IsNullOrEmpty(underlying))
            parameters.AddOptionalParameter("uly", underlying);
        if (alias != null)
            parameters.AddOptionalParameter("alias", JsonConvert.SerializeObject(alias, new InstrumentAliasConverter(false)));
        if (state != null)
            parameters.AddOptionalParameter("state", JsonConvert.SerializeObject(state, new LiquidationStateConverter(false)));

        parameters.AddOptionalParameter("after", after?.ToString());
        parameters.AddOptionalParameter("before", before?.ToString());
        parameters.AddOptionalParameter("limit", limit.ToString());

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexLiquidationInfo>>>(UnifiedApi.GetUri(Endpoints_V5_Public_LiquidationOrders), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OkexLiquidationInfo>>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OkexLiquidationInfo>>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data);
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
    public virtual WebCallResult<IEnumerable<OkexMarkPrice>> GetMarkPrices(OkexInstrumentType instrumentType, string underlying = null, string instrumentId = null, CancellationToken ct = default)
        => GetMarkPricesAsync(instrumentType, underlying, instrumentId, ct).Result;
    /// <summary>
    /// Retrieve mark price.
    /// We set the mark price based on the SPOT index and at a reasonable basis to prevent individual users from manipulating the market and causing the contract price to fluctuate.
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="underlying">Underlying</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<WebCallResult<IEnumerable<OkexMarkPrice>>> GetMarkPricesAsync(OkexInstrumentType instrumentType, string underlying = null, string instrumentId = null, CancellationToken ct = default)
    {
        if (instrumentType.IsNotIn(OkexInstrumentType.Margin, OkexInstrumentType.Futures, OkexInstrumentType.Option, OkexInstrumentType.Swap))
            throw new ArgumentException("Instrument Type can be only Margin, Futures, Option or Swap.");

        var parameters = new Dictionary<string, object>
        {
            { "instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)) },
        };
        if (!string.IsNullOrEmpty(underlying))
            parameters.AddOptionalParameter("uly", underlying);
        if (!string.IsNullOrEmpty(instrumentId))
            parameters.AddOptionalParameter("instId", instrumentId);

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexMarkPrice>>>(UnifiedApi.GetUri(Endpoints_V5_Public_MarkPrice), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OkexMarkPrice>>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OkexMarkPrice>>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data);
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
        CancellationToken ct = default)
        => GetPositionTiersAsync(instrumentType, marginMode, underlying, instrumentId, tier, ct).Result;
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
    public virtual async Task<WebCallResult<IEnumerable<OkexPositionTier>>> GetPositionTiersAsync(
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
        if (!string.IsNullOrEmpty(underlying))
            parameters.AddOptionalParameter("uly", underlying);
        if (!string.IsNullOrEmpty(instrumentId))
            parameters.AddOptionalParameter("instId", instrumentId);
        if (!string.IsNullOrEmpty(tier))
            parameters.AddOptionalParameter("tier", tier);

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexPositionTier>>>(UnifiedApi.GetUri(Endpoints_V5_Public_PositionTiers), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OkexPositionTier>>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OkexPositionTier>>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data);
    }

    /// <summary>
    /// Get margin interest rate
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual WebCallResult<Objects.Public.OkexInterestRate> GetInterestRates(CancellationToken ct = default)
        => GetInterestRatesAsync(ct).Result;
    /// <summary>
    /// Get margin interest rate
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<WebCallResult<Objects.Public.OkexInterestRate>> GetInterestRatesAsync(CancellationToken ct = default)
    {
        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<Objects.Public.OkexInterestRate>>>(UnifiedApi.GetUri(Endpoints_V5_Public_InterestRateLoanQuota), HttpMethod.Get, ct).ConfigureAwait(false);
        if (!result.Success) return result.AsError<Objects.Public.OkexInterestRate>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<Objects.Public.OkexInterestRate>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data.FirstOrDefault());
    }

    /// <summary>
    /// Get interest rate and loan quota for VIP loans
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual WebCallResult<IEnumerable<OkexVipInterestRate>> GetVIPInterestRates(CancellationToken ct = default)
        => GetVIPInterestRatesAsync(ct).Result;
    /// <summary>
    /// Get interest rate and loan quota for VIP loans
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<WebCallResult<IEnumerable<OkexVipInterestRate>>> GetVIPInterestRatesAsync(CancellationToken ct = default)
    {
        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexVipInterestRate>>>(UnifiedApi.GetUri(Endpoints_V5_Public_VIPInterestRateLoanQuota), HttpMethod.Get, ct).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OkexVipInterestRate>>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OkexVipInterestRate>>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data);
    }

    /// <summary>
    /// Get Underlying
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual WebCallResult<IEnumerable<string>> GetUnderlying(OkexInstrumentType instrumentType, CancellationToken ct = default)
        => GetUnderlyingAsync(instrumentType, ct).Result;
    /// <summary>
    /// Get Underlying
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<WebCallResult<IEnumerable<string>>> GetUnderlyingAsync(OkexInstrumentType instrumentType, CancellationToken ct = default)
    {
        if (instrumentType.IsNotIn(OkexInstrumentType.Futures, OkexInstrumentType.Option, OkexInstrumentType.Swap))
            throw new ArgumentException("Instrument Type can be only Futures, Option or Swap.");

        var parameters = new Dictionary<string, object>
        {
            { "instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)) },
        };
        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<IEnumerable<string>>>>(UnifiedApi.GetUri(Endpoints_V5_Public_Underlying), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<string>>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<string>>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data.FirstOrDefault());
    }

    /// <summary>
    /// Get insurance fund
    /// Get insurance fund balance information
    /// Rate Limit: 10 requests per 2 seconds
    /// </summary>
    /// <param name="instrumentType"></param>
    /// <param name="type"></param>
    /// <param name="underlying"></param>
    /// <param name="currency"></param>
    /// <param name="after"></param>
    /// <param name="before"></param>
    /// <param name="limit"></param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual WebCallResult<OkexInsuranceFund> GetInsuranceFund(
        OkexInstrumentType instrumentType,
        OkexInsuranceType type = OkexInsuranceType.All,
        string underlying = "",
        string currency = "",
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
        => GetInsuranceFundAsync(instrumentType, type, underlying, currency, after, before, limit, ct).Result;

    /// <summary>
    /// Get insurance fund
    /// Get insurance fund balance information
    /// Rate Limit: 10 requests per 2 seconds
    /// </summary>
    /// <param name="instrumentType"></param>
    /// <param name="type"></param>
    /// <param name="underlying"></param>
    /// <param name="currency"></param>
    /// <param name="after"></param>
    /// <param name="before"></param>
    /// <param name="limit"></param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<WebCallResult<OkexInsuranceFund>> GetInsuranceFundAsync(
        OkexInstrumentType instrumentType,
        OkexInsuranceType type = OkexInsuranceType.All,
        string underlying = "",
        string currency = "",
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        if (instrumentType.IsNotIn(OkexInstrumentType.Margin, OkexInstrumentType.Swap, OkexInstrumentType.Futures, OkexInstrumentType.Option))
            throw new ArgumentException("Instrument Type can be only Margin, Swap, Futures or Option.");

        var parameters = new Dictionary<string, object>
        {
            { "instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)) },
        };

        if (type != OkexInsuranceType.All)
            parameters.AddOptionalParameter("type", JsonConvert.SerializeObject(type, new InsuranceTypeConverter(false)));

        if (!string.IsNullOrEmpty(underlying))
            parameters.AddOptionalParameter("uly", underlying);
        if (!string.IsNullOrEmpty(currency))
            parameters.AddOptionalParameter("ccy", currency);

        parameters.AddOptionalParameter("after", after?.ToString());
        parameters.AddOptionalParameter("before", before?.ToString());
        parameters.AddOptionalParameter("limit", limit.ToString());

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexInsuranceFund>>>(UnifiedApi.GetUri(Endpoints_V5_Public_InsuranceFund), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<OkexInsuranceFund>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<OkexInsuranceFund>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data.FirstOrDefault());
    }

    /// <summary>
    /// Unit Convert
    /// Convert currency to contract, or contract to currency.
    /// Rate Limit: 10 requests per 2 seconds
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual WebCallResult<OkexUnitConvert> UnitConvert(
        OkexConvertType? type = null,
        OkexConvertUnit? unit = null,
        string instrumentId = "",
        decimal? price = null,
        decimal? size = null,
        CancellationToken ct = default)
        => UnitConvertAsync(type, unit, instrumentId, price, size, ct).Result;
    /// <summary>
    /// Get Underlying
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<WebCallResult<OkexUnitConvert>> UnitConvertAsync(
        OkexConvertType? type = OkexConvertType.CurrencyToContract,
        OkexConvertUnit? unit = null,
        string instrumentId = "",
        decimal? price = null,
        decimal? size = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        if (type != null) parameters.AddOptionalParameter("type", JsonConvert.SerializeObject(type, new ConvertTypeConverter(false)));
        if (unit != null) parameters.AddOptionalParameter("unit", JsonConvert.SerializeObject(type, new ConvertUnitConverter(false)));
        if (!string.IsNullOrEmpty(instrumentId)) parameters.AddOptionalParameter("instId", instrumentId);
        parameters.AddOptionalParameter("px", price?.ToString(OkexGlobals.OkexCultureInfo));
        parameters.AddOptionalParameter("sz", size?.ToString(OkexGlobals.OkexCultureInfo));

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexUnitConvert>>>(UnifiedApi.GetUri(Endpoints_V5_Public_ConvertContractCoin), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<OkexUnitConvert>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<OkexUnitConvert>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data.FirstOrDefault());
    }
    #endregion
}