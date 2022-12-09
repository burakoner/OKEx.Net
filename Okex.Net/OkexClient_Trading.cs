namespace Okex.Net;

public partial class OkexClient
{
    #region Trading API Endpoints
    /// <summary>
    /// Get the currency supported by the transaction big data interface
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual WebCallResult<OkexSupportCoins> GetRubikSupportCoin(CancellationToken ct = default)
        => GetRubikSupportCoinAsync(ct).Result;
    /// <summary>
    /// Get the currency supported by the transaction big data interface
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<WebCallResult<OkexSupportCoins>> GetRubikSupportCoinAsync(CancellationToken ct = default)
    {
        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<OkexSupportCoins>>(UnifiedApi.GetUri(Endpoints_V5_RubikStat_TradingDataSupportCoin), HttpMethod.Get, ct).ConfigureAwait(false);
        if (!result.Success) return result.AsError<OkexSupportCoins>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<OkexSupportCoins>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data);
    }

    /// <summary>
    /// This is the taker volume for both buyers and sellers. This shows the influx and exit of funds in and out of {coin}.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="period">period, the default is 5m, e.g. [5m/1H/1D]</param>
    /// <param name="begin">begin, e.g. 1597026383085</param>
    /// <param name="end">end, e.g. 1597026383011</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual WebCallResult<IEnumerable<OkexTakerVolume>> GetRubikTakerVolume(
        string currency,
        OkexInstrumentType instrumentType,
        OkexPeriod period = OkexPeriod.FiveMinutes,
        long? begin = null,
        long? end = null,
        CancellationToken ct = default)
        => GetRubikTakerVolumeAsync(currency, instrumentType, period, begin, end, ct).Result;
    /// <summary>
    /// This is the taker volume for both buyers and sellers. This shows the influx and exit of funds in and out of {coin}.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="period">period, the default is 5m, e.g. [5m/1H/1D]</param>
    /// <param name="begin">begin, e.g. 1597026383085</param>
    /// <param name="end">end, e.g. 1597026383011</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<WebCallResult<IEnumerable<OkexTakerVolume>>> GetRubikTakerVolumeAsync(
        string currency,
        OkexInstrumentType instrumentType,
        OkexPeriod period = OkexPeriod.FiveMinutes,
        long? begin = null,
        long? end = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "ccy", currency},
            { "instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)) },
            { "period", JsonConvert.SerializeObject(period, new PeriodConverter(false)) },
        };
        parameters.AddOptionalParameter("begin", begin?.ToString(OkexGlobals.OkexCultureInfo));
        parameters.AddOptionalParameter("end", end?.ToString(OkexGlobals.OkexCultureInfo));

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexTakerVolume>>>(UnifiedApi.GetUri(Endpoints_V5_RubikStat_TakerVolume), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OkexTakerVolume>>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OkexTakerVolume>>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data);
    }

    /// <summary>
    /// This indicator shows the ratio of cumulative data value between currency pair leverage quote currency and underlying asset over a given period of time.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="period">period, the default is 5m, e.g. [5m/1H/1D]</param>
    /// <param name="begin">begin, e.g. 1597026383085</param>
    /// <param name="end">end, e.g. 1597026383085</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual WebCallResult<IEnumerable<OkexRatio>> GetRubikMarginLendingRatio(
        string currency,
        OkexPeriod period = OkexPeriod.FiveMinutes,
        long? begin = null,
        long? end = null,
        CancellationToken ct = default)
        => GetRubikMarginLendingRatioAsync(currency, period, begin, end, ct).Result;
    /// <summary>
    /// This indicator shows the ratio of cumulative data value between currency pair leverage quote currency and underlying asset over a given period of time.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="period">period, the default is 5m, e.g. [5m/1H/1D]</param>
    /// <param name="begin">begin, e.g. 1597026383085</param>
    /// <param name="end">end, e.g. 1597026383085</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<WebCallResult<IEnumerable<OkexRatio>>> GetRubikMarginLendingRatioAsync(
        string currency,
        OkexPeriod period = OkexPeriod.FiveMinutes,
        long? begin = null,
        long? end = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "ccy", currency},
            { "period", JsonConvert.SerializeObject(period, new PeriodConverter(false)) },
        };
        parameters.AddOptionalParameter("begin", begin?.ToString(OkexGlobals.OkexCultureInfo));
        parameters.AddOptionalParameter("end", end?.ToString(OkexGlobals.OkexCultureInfo));

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexRatio>>>(UnifiedApi.GetUri(Endpoints_V5_RubikStat_MarginLoanRatio), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OkexRatio>>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OkexRatio>>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data);
    }

    /// <summary>
    /// This is the ratio of users with net long vs short positions. It includes data from futures and perpetual swaps.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="period">period, the default is 5m, e.g. [5m/1H/1D]</param>
    /// <param name="begin">begin, e.g. 1597026383085</param>
    /// <param name="end">end, e.g. 1597026383011</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual WebCallResult<IEnumerable<OkexRatio>> GetRubikLongShortRatio(
        string currency,
        OkexPeriod period = OkexPeriod.FiveMinutes,
        long? begin = null,
        long? end = null,
        CancellationToken ct = default)
        => GetRubikLongShortRatioAsync(currency, period, begin, end, ct).Result;
    /// <summary>
    /// This is the ratio of users with net long vs short positions. It includes data from futures and perpetual swaps.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="period">period, the default is 5m, e.g. [5m/1H/1D]</param>
    /// <param name="begin">begin, e.g. 1597026383085</param>
    /// <param name="end">end, e.g. 1597026383011</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<WebCallResult<IEnumerable<OkexRatio>>> GetRubikLongShortRatioAsync(
        string currency,
        OkexPeriod period = OkexPeriod.FiveMinutes,
        long? begin = null,
        long? end = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "ccy", currency},
            { "period", JsonConvert.SerializeObject(period, new PeriodConverter(false)) },
        };
        parameters.AddOptionalParameter("begin", begin?.ToString(OkexGlobals.OkexCultureInfo));
        parameters.AddOptionalParameter("end", end?.ToString(OkexGlobals.OkexCultureInfo));

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexRatio>>>(UnifiedApi.GetUri(Endpoints_V5_RubikStat_ContractsLongShortAccountRatio), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OkexRatio>>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OkexRatio>>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data);
    }

    /// <summary>
    /// Open interest is the sum of all long and short futures and perpetual swap positions.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="period">period, the default is 5m, e.g. [5m/1H/1D]</param>
    /// <param name="begin">begin, e.g. 1597026383085</param>
    /// <param name="end">end, e.g. 1597026383011</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual WebCallResult<IEnumerable<OkexInterestVolume>> GetRubikContractSummary(
        string currency,
        OkexPeriod period = OkexPeriod.FiveMinutes,
        long? begin = null,
        long? end = null,
        CancellationToken ct = default)
        => GetRubikContractSummaryAsync(currency, period, begin, end, ct).Result;
    /// <summary>
    /// Open interest is the sum of all long and short futures and perpetual swap positions.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="period">period, the default is 5m, e.g. [5m/1H/1D]</param>
    /// <param name="begin">begin, e.g. 1597026383085</param>
    /// <param name="end">end, e.g. 1597026383011</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<WebCallResult<IEnumerable<OkexInterestVolume>>> GetRubikContractSummaryAsync(
        string currency,
        OkexPeriod period = OkexPeriod.FiveMinutes,
        long? begin = null,
        long? end = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "ccy", currency},
            { "period", JsonConvert.SerializeObject(period, new PeriodConverter(false)) },
        };
        parameters.AddOptionalParameter("begin", begin?.ToString(OkexGlobals.OkexCultureInfo));
        parameters.AddOptionalParameter("end", end?.ToString(OkexGlobals.OkexCultureInfo));

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexInterestVolume>>>(UnifiedApi.GetUri(Endpoints_V5_RubikStat_ContractsOpenInterestVolume), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OkexInterestVolume>>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OkexInterestVolume>>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data);
    }

    /// <summary>
    /// This shows the sum of all open positions and how much total trading volume has taken place.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="period">period, the default is 8H. e.g. [8H/1D]</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual WebCallResult<IEnumerable<OkexInterestVolume>> GetRubikOptionsSummary(
        string currency,
        OkexPeriod period = OkexPeriod.FiveMinutes,
        CancellationToken ct = default)
        => GetRubikOptionsSummaryAsync(currency, period, ct).Result;
    /// <summary>
    /// This shows the sum of all open positions and how much total trading volume has taken place.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="period">period, the default is 8H. e.g. [8H/1D]</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<WebCallResult<IEnumerable<OkexInterestVolume>>> GetRubikOptionsSummaryAsync(
        string currency,
        OkexPeriod period = OkexPeriod.FiveMinutes,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "ccy", currency},
            { "period", JsonConvert.SerializeObject(period, new PeriodConverter(false)) },
        };

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexInterestVolume>>>(UnifiedApi.GetUri(Endpoints_V5_RubikStat_OptionOpenInterestVolume), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OkexInterestVolume>>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OkexInterestVolume>>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data);
    }

    /// <summary>
    /// This shows the relative buy/sell volume for calls and puts. It shows whether traders are bullish or bearish on price and volatility.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="period">period, the default is 8H. e.g. [8H/1D]</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual WebCallResult<IEnumerable<OkexPutCallRatio>> GetRubikPutCallRatio(
        string currency,
        OkexPeriod period = OkexPeriod.FiveMinutes,
        CancellationToken ct = default)
        => GetRubikPutCallRatioAsync(currency, period, ct).Result;
    /// <summary>
    /// This shows the relative buy/sell volume for calls and puts. It shows whether traders are bullish or bearish on price and volatility.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="period">period, the default is 8H. e.g. [8H/1D]</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<WebCallResult<IEnumerable<OkexPutCallRatio>>> GetRubikPutCallRatioAsync(
        string currency,
        OkexPeriod period = OkexPeriod.FiveMinutes,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "ccy", currency},
            { "period", JsonConvert.SerializeObject(period, new PeriodConverter(false)) },
        };

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexPutCallRatio>>>(UnifiedApi.GetUri(Endpoints_V5_RubikStat_OptionOpenInterestVolumeRatio), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OkexPutCallRatio>>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OkexPutCallRatio>>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data);
    }

    /// <summary>
    /// This shows the volume and open interest for each upcoming expiration. You can use this to see which expirations are currently the most popular to trade.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="period">period, the default is 8H. e.g. [8H/1D]</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual WebCallResult<IEnumerable<OkexInterestVolumeExpiry>> GetRubikInterestVolumeExpiry(
        string currency,
        OkexPeriod period = OkexPeriod.FiveMinutes,
        CancellationToken ct = default)
        => GetRubikInterestVolumeExpiryAsync(currency, period, ct).Result;
    /// <summary>
    /// This shows the volume and open interest for each upcoming expiration. You can use this to see which expirations are currently the most popular to trade.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="period">period, the default is 8H. e.g. [8H/1D]</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<WebCallResult<IEnumerable<OkexInterestVolumeExpiry>>> GetRubikInterestVolumeExpiryAsync(
        string currency,
        OkexPeriod period = OkexPeriod.FiveMinutes,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "ccy", currency},
            { "period", JsonConvert.SerializeObject(period, new PeriodConverter(false)) },
        };

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexInterestVolumeExpiry>>>(UnifiedApi.GetUri(Endpoints_V5_RubikStat_OptionOpenInterestVolumeExpiry), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OkexInterestVolumeExpiry>>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OkexInterestVolumeExpiry>>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data);
    }

    /// <summary>
    /// This shows what option strikes are the most popular for each expiration.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="expiryTime">expiry time (Format: YYYYMMdd, for example: "20210623")</param>
    /// <param name="period">period, the default is 8H. e.g. [8H/1D]</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual WebCallResult<IEnumerable<OkexInterestVolumeStrike>> GetRubikInterestVolumeStrike(
        string currency,
        string expiryTime,
        OkexPeriod period = OkexPeriod.FiveMinutes,
        CancellationToken ct = default)
        => GetRubikInterestVolumeStrikeAsync(currency, expiryTime, period, ct).Result;
    /// <summary>
    /// This shows what option strikes are the most popular for each expiration.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="expiryTime">expiry time (Format: YYYYMMdd, for example: "20210623")</param>
    /// <param name="period">period, the default is 8H. e.g. [8H/1D]</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<WebCallResult<IEnumerable<OkexInterestVolumeStrike>>> GetRubikInterestVolumeStrikeAsync(
        string currency,
        string expiryTime,
        OkexPeriod period = OkexPeriod.FiveMinutes,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "ccy", currency},
            { "expTime", expiryTime},
            { "period", JsonConvert.SerializeObject(period, new PeriodConverter(false)) },
        };

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexInterestVolumeStrike>>>(UnifiedApi.GetUri(Endpoints_V5_RubikStat_OptionOpenInterestVolumeStrike), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OkexInterestVolumeStrike>>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OkexInterestVolumeStrike>>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data);
    }

    /// <summary>
    /// This shows the relative buy/sell volume for calls and puts. It shows whether traders are bullish or bearish on price and volatility.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="period">period, the default is 8H. e.g. [8H/1D]</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual WebCallResult<OkexTakerFlow> GetRubikTakerFlow(
        string currency,
        OkexPeriod period = OkexPeriod.FiveMinutes,
        CancellationToken ct = default)
        => GetRubikTakerFlowAsync(currency, period, ct).Result;
    /// <summary>
    /// This shows the relative buy/sell volume for calls and puts. It shows whether traders are bullish or bearish on price and volatility.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="period">period, the default is 8H. e.g. [8H/1D]</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<WebCallResult<OkexTakerFlow>> GetRubikTakerFlowAsync(
        string currency,
        OkexPeriod period = OkexPeriod.FiveMinutes,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "ccy", currency},
            { "period", JsonConvert.SerializeObject(period, new PeriodConverter(false)) },
        };

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<OkexTakerFlow>>(UnifiedApi.GetUri(Endpoints_V5_RubikStat_OptionTakerBlockVolume), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<OkexTakerFlow>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<OkexTakerFlow>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data);
    }
    #endregion
}