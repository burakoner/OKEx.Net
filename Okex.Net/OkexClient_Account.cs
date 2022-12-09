namespace Okex.Net;

public partial class OkexClient
{
    #region Account API Endpoints
    /// <summary>
    /// Retrieve a list of assets (with non-zero balance), remaining balance, and available amount in the account.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual WebCallResult<OkexAccountBalance> GetAccountBalance(string currency = null, CancellationToken ct = default)
        => GetAccountBalance_Async(currency, ct).Result;
    /// <summary>
    /// Retrieve a list of assets (with non-zero balance), remaining balance, and available amount in the account.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<WebCallResult<OkexAccountBalance>> GetAccountBalance_Async(string currency = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("ccy", currency);

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexAccountBalance>>>(UnifiedApi.GetUri(Endpoints_V5_Account_Balance), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<OkexAccountBalance>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<OkexAccountBalance>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data.FirstOrDefault());
    }

    /// <summary>
    /// Retrieve information on your positions. When the account is in net mode, net positions will be displayed, and when the account is in long/short mode, long or short positions will be displayed.
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="positionId">Position ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual WebCallResult<IEnumerable<OkexPosition>> GetAccountPositions(
        OkexInstrumentType? instrumentType = null,
        string instrumentId = null,
        string positionId = null,
        CancellationToken ct = default)
        => GetAccountPositions_Async(instrumentType, instrumentId, positionId, ct).Result;
    /// <summary>
    /// Retrieve information on your positions. When the account is in net mode, net positions will be displayed, and when the account is in long/short mode, long or short positions will be displayed.
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="positionId">Position ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<WebCallResult<IEnumerable<OkexPosition>>> GetAccountPositions_Async(
        OkexInstrumentType? instrumentType = null,
        string instrumentId = null,
        string positionId = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        if (instrumentType.HasValue)
            parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)));
        parameters.AddOptionalParameter("instId", instrumentId);
        parameters.AddOptionalParameter("posId", positionId);

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexPosition>>>(UnifiedApi.GetUri(Endpoints_V5_Account_Positions), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OkexPosition>>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OkexPosition>>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data);
    }

    /// <summary>
    /// Retrieve the updated position data for the last 3 months. Return in reverse chronological order using utime.
    /// </summary>
    /// <param name="instrumentType">Instrument type</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="marginMode">Margin mode</param>
    /// <param name="type">The type of closing position. It is the latest type if there are several types for the same position.</param>
    /// <param name="positionId">Position ID</param>
    /// <param name="after">Pagination of data to return records earlier than the requested uTime, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records earlier than the requested uTime, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100. The default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public virtual WebCallResult<IEnumerable<OkexClosingPosition>> GetAccountPositionsHistory(
        OkexInstrumentType? instrumentType = null,
        string instrumentId = null,
        OkexMarginMode? marginMode = null,
        OkexClosingPositionType? type = null,
        string positionId = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
        => GetAccountPositionsHistory_Async(instrumentType, instrumentId, marginMode, type, positionId, after, before, limit, ct).Result;

    /// <summary>
    /// Retrieve the updated position data for the last 3 months. Return in reverse chronological order using utime.
    /// </summary>
    /// <param name="instrumentType">Instrument type</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="marginMode">Margin mode</param>
    /// <param name="type">The type of closing position. It is the latest type if there are several types for the same position.</param>
    /// <param name="positionId">Position ID</param>
    /// <param name="after">Pagination of data to return records earlier than the requested uTime, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records earlier than the requested uTime, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100. The default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public virtual async Task<WebCallResult<IEnumerable<OkexClosingPosition>>> GetAccountPositionsHistory_Async(
        OkexInstrumentType? instrumentType = null,
        string instrumentId = null,
        OkexMarginMode? marginMode = null,
        OkexClosingPositionType? type = null,
        string positionId = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        if (instrumentType.HasValue)
            parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)));
        parameters.AddOptionalParameter("instId", instrumentId);
        if (marginMode != null)
            parameters.AddOptionalParameter("mgnMode", JsonConvert.SerializeObject(marginMode, new MarginModeConverter(false)));
        if (type != null)
            parameters.AddOptionalParameter("type", JsonConvert.SerializeObject(type, new ClosingPositionTypeConverter(false)));
        parameters.AddOptionalParameter("posId", positionId);
        parameters.AddOptionalParameter("after", after?.ToString());
        parameters.AddOptionalParameter("before", before?.ToString());
        parameters.AddOptionalParameter("limit", limit.ToString());

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexClosingPosition>>>(UnifiedApi.GetUri(Endpoints_V5_Account_PositionsHistory), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OkexClosingPosition>>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OkexClosingPosition>>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));


        return result.As(result.Data.Data);
    }

    /// <summary>
    /// Get account and position risk
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual WebCallResult<IEnumerable<OkexPositionRisk>> GetAccountPositionRisk(OkexInstrumentType? instrumentType = null, CancellationToken ct = default)
        => GetAccountPositionRisk_Async(instrumentType, ct).Result;
    /// <summary>
    /// Get account and position risk
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<WebCallResult<IEnumerable<OkexPositionRisk>>> GetAccountPositionRisk_Async(OkexInstrumentType? instrumentType = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        if (instrumentType.HasValue)
            parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)));

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexPositionRisk>>>(UnifiedApi.GetUri(Endpoints_V5_Account_PositionRisk), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OkexPositionRisk>>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OkexPositionRisk>>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data);
    }

    /// <summary>
    /// Retrieve the bills of the account. The bill refers to all transaction records that result in changing the balance of an account. Pagination is supported, and the response is sorted with the most recent first. This endpoint can retrieve data from the last 7 days.
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="currency">Currency</param>
    /// <param name="marginMode">Margin Mode</param>
    /// <param name="contractType">Contract Type</param>
    /// <param name="billType">Bill Type</param>
    /// <param name="billSubType">Bill Sub Type</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual WebCallResult<IEnumerable<OkexAccountBill>> GetBillHistory(
        OkexInstrumentType? instrumentType = null,
        string currency = null,
        OkexMarginMode? marginMode = null,
        OkexContractType? contractType = null,
        OkexAccountBillType? billType = null,
        OkexAccountBillSubType? billSubType = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
        => GetBillHistory_Async(
        instrumentType,
        currency,
        marginMode,
        contractType,
        billType,
        billSubType,
        after,
        before,
        limit,
        ct).Result;
    /// <summary>
    /// Retrieve the bills of the account. The bill refers to all transaction records that result in changing the balance of an account. Pagination is supported, and the response is sorted with the most recent first. This endpoint can retrieve data from the last 7 days.
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="currency">Currency</param>
    /// <param name="marginMode">Margin Mode</param>
    /// <param name="contractType">Contract Type</param>
    /// <param name="billType">Bill Type</param>
    /// <param name="billSubType">Bill Sub Type</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<WebCallResult<IEnumerable<OkexAccountBill>>> GetBillHistory_Async(
        OkexInstrumentType? instrumentType = null,
        string currency = null,
        OkexMarginMode? marginMode = null,
        OkexContractType? contractType = null,
        OkexAccountBillType? billType = null,
        OkexAccountBillSubType? billSubType = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        if (limit < 1 || limit > 100)
            throw new ArgumentException("Limit can be between 1-100.");

        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("ccy", currency);
        parameters.AddOptionalParameter("after", after?.ToString());
        parameters.AddOptionalParameter("before", before?.ToString());
        parameters.AddOptionalParameter("limit", limit.ToString());

        if (instrumentType.HasValue)
            parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)));
        if (marginMode.HasValue)
            parameters.AddOptionalParameter("mgnMode", JsonConvert.SerializeObject(marginMode, new MarginModeConverter(false)));
        if (contractType.HasValue)
            parameters.AddOptionalParameter("ctType", JsonConvert.SerializeObject(contractType, new ContractTypeConverter(false)));
        if (billType.HasValue)
            parameters.AddOptionalParameter("type", JsonConvert.SerializeObject(billType, new AccountBillTypeConverter(false)));
        if (billSubType.HasValue)
            parameters.AddOptionalParameter("subType", JsonConvert.SerializeObject(billSubType, new AccountBillSubTypeConverter(false)));

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexAccountBill>>>(UnifiedApi.GetUri(Endpoints_V5_Account_Bills), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OkexAccountBill>>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OkexAccountBill>>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data);
    }

    /// <summary>
    /// Retrieve the account’s bills. The bill refers to all transaction records that result in changing the balance of an account. Pagination is supported, and the response is sorted with most recent first. This endpoint can retrieve data from the last 3 months.
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="currency">Currency</param>
    /// <param name="marginMode">Margin Mode</param>
    /// <param name="contractType">Contract Type</param>
    /// <param name="billType">Bill Type</param>
    /// <param name="billSubType">Bill Sub Type</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual WebCallResult<IEnumerable<OkexAccountBill>> GetBillArchive(
        OkexInstrumentType? instrumentType = null,
        string currency = null,
        OkexMarginMode? marginMode = null,
        OkexContractType? contractType = null,
        OkexAccountBillType? billType = null,
        OkexAccountBillSubType? billSubType = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
        => GetBillArchive_Async(
        instrumentType,
        currency,
        marginMode,
        contractType,
        billType,
        billSubType,
        after,
        before,
        limit,
        ct).Result;
    /// <summary>
    /// Retrieve the account’s bills. The bill refers to all transaction records that result in changing the balance of an account. Pagination is supported, and the response is sorted with most recent first. This endpoint can retrieve data from the last 3 months.
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="currency">Currency</param>
    /// <param name="marginMode">Margin Mode</param>
    /// <param name="contractType">Contract Type</param>
    /// <param name="billType">Bill Type</param>
    /// <param name="billSubType">Bill Sub Type</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<WebCallResult<IEnumerable<OkexAccountBill>>> GetBillArchive_Async(
        OkexInstrumentType? instrumentType = null,
        string currency = null,
        OkexMarginMode? marginMode = null,
        OkexContractType? contractType = null,
        OkexAccountBillType? billType = null,
        OkexAccountBillSubType? billSubType = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        if (limit < 1 || limit > 100)
            throw new ArgumentException("Limit can be between 1-100.");

        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("ccy", currency);
        parameters.AddOptionalParameter("after", after?.ToString());
        parameters.AddOptionalParameter("before", before?.ToString());
        parameters.AddOptionalParameter("limit", limit.ToString());

        if (instrumentType.HasValue)
            parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)));
        if (marginMode.HasValue)
            parameters.AddOptionalParameter("mgnMode", JsonConvert.SerializeObject(marginMode, new MarginModeConverter(false)));
        if (contractType.HasValue)
            parameters.AddOptionalParameter("ctType", JsonConvert.SerializeObject(contractType, new ContractTypeConverter(false)));
        if (billType.HasValue)
            parameters.AddOptionalParameter("type", JsonConvert.SerializeObject(billType, new AccountBillTypeConverter(false)));
        if (billSubType.HasValue)
            parameters.AddOptionalParameter("subType", JsonConvert.SerializeObject(billSubType, new AccountBillSubTypeConverter(false)));

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexAccountBill>>>(UnifiedApi.GetUri(Endpoints_V5_Account_BillsArchive), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OkexAccountBill>>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OkexAccountBill>>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data);
    }

    /// <summary>
    /// Retrieve current account configuration.
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual WebCallResult<OkexAccountConfiguration> GetAccountConfiguration(CancellationToken ct = default)
        => GetAccountConfiguration_Async(ct).Result;
    /// <summary>
    /// Retrieve current account configuration.
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<WebCallResult<OkexAccountConfiguration>> GetAccountConfiguration_Async(CancellationToken ct = default)
    {
        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexAccountConfiguration>>>(UnifiedApi.GetUri(Endpoints_V5_Account_Config), HttpMethod.Get, ct, null, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<OkexAccountConfiguration>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<OkexAccountConfiguration>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data.FirstOrDefault());
    }

    /// <summary>
    /// FUTURES and SWAP support both long/short mode and net mode. In net mode, users can only have positions in one direction; In long/short mode, users can hold positions in long and short directions.
    /// </summary>
    /// <param name="positionMode"></param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual WebCallResult<OkexAccountPositionMode> SetAccountPositionMode(Enums.OkexPositionMode positionMode, CancellationToken ct = default)
        => SetAccountPositionMode_Async(positionMode, ct).Result;
    /// <summary>
    /// FUTURES and SWAP support both long/short mode and net mode. In net mode, users can only have positions in one direction; In long/short mode, users can hold positions in long and short directions.
    /// </summary>
    /// <param name="positionMode"></param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<WebCallResult<OkexAccountPositionMode>> SetAccountPositionMode_Async(Enums.OkexPositionMode positionMode, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            {"posMode", JsonConvert.SerializeObject(positionMode, new PositionModeConverter(false)) },
        };

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexAccountPositionMode>>>(UnifiedApi.GetUri(Endpoints_V5_Account_SetPositionMode), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<OkexAccountPositionMode>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<OkexAccountPositionMode>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data.FirstOrDefault());
    }

    /// <summary>
    /// Get Leverage
    /// </summary>
    /// <param name="instrumentIds">Single instrument ID or multiple instrument IDs (no more than 20) separated with comma</param>
    /// <param name="marginMode">Margin Mode</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual WebCallResult<IEnumerable<OkexLeverage>> GetAccountLeverage(
        string instrumentIds,
        OkexMarginMode marginMode,
        CancellationToken ct = default)
        => GetAccountLeverage_Async(instrumentIds, marginMode, ct).Result;
    /// <summary>
    /// Get Leverage
    /// </summary>
    /// <param name="instrumentIds">Single instrument ID or multiple instrument IDs (no more than 20) separated with comma</param>
    /// <param name="marginMode">Margin Mode</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<WebCallResult<IEnumerable<OkexLeverage>>> GetAccountLeverage_Async(
        string instrumentIds,
        OkexMarginMode marginMode,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            {"instId", instrumentIds },
            {"mgnMode", JsonConvert.SerializeObject(marginMode, new MarginModeConverter(false)) },
        };

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexLeverage>>>(UnifiedApi.GetUri(Endpoints_V5_Account_LeverageInfo), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OkexLeverage>>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OkexLeverage>>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data);
    }

    /// <summary>
    /// The following are the setting leverage cases for an instrument:
    /// Set leverage for isolated MARGIN at pairs level.
    /// Set leverage for cross MARGIN in Single-currency margin at pairs level.
    /// Set leverage for cross MARGIN in Multi-currency margin at currency level.
    /// Set leverage for cross/isolated FUTURES/SWAP at underlying/contract level.
    /// </summary>
    /// <param name="leverage">Leverage</param>
    /// <param name="currency">Currency</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="marginMode">Margin Mode</param>
    /// <param name="positionSide">Position Side</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual WebCallResult<IEnumerable<OkexLeverage>> SetAccountLeverage(
        int leverage,
        string currency = null,
        string instrumentId = null,
        OkexMarginMode? marginMode = null,
        OkexPositionSide? positionSide = null,
        CancellationToken ct = default)
        => SetAccountLeverage_Async(leverage, currency, instrumentId, marginMode, positionSide, ct).Result;
    /// <summary>
    /// The following are the setting leverage cases for an instrument:
    /// Set leverage for isolated MARGIN at pairs level.
    /// Set leverage for cross MARGIN in Single-currency margin at pairs level.
    /// Set leverage for cross MARGIN in Multi-currency margin at currency level.
    /// Set leverage for cross/isolated FUTURES/SWAP at underlying/contract level.
    /// </summary>
    /// <param name="leverage">Leverage</param>
    /// <param name="currency">Currency</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="marginMode">Margin Mode</param>
    /// <param name="positionSide">Position Side</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<WebCallResult<IEnumerable<OkexLeverage>>> SetAccountLeverage_Async(
        int leverage,
        string currency = null,
        string instrumentId = null,
        OkexMarginMode? marginMode = null,
        OkexPositionSide? positionSide = null,
        CancellationToken ct = default)
    {
        if (leverage < 1)
            throw new ArgumentException("Invalid Leverage");

        if (string.IsNullOrEmpty(currency) && string.IsNullOrEmpty(instrumentId))
            throw new ArgumentException("Either instId or ccy is required; if both are passed, instId will be used by default.");

        if (marginMode == null)
            throw new ArgumentException("marginMode is required");

        var parameters = new Dictionary<string, object> {
            {"lever", leverage.ToString() },
            {"mgnMode", JsonConvert.SerializeObject(marginMode, new MarginModeConverter(false)) },
        };
        parameters.AddOptionalParameter("ccy", currency);
        parameters.AddOptionalParameter("instId", instrumentId);
        if (positionSide.HasValue)
            parameters.AddOptionalParameter("posSide", JsonConvert.SerializeObject(positionSide, new PositionSideConverter(false)));

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexLeverage>>>(UnifiedApi.GetUri(Endpoints_V5_Account_SetLeverage), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OkexLeverage>>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OkexLeverage>>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data);
    }

    /// <summary>
    /// Get maximum buy/sell amount or open amount
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="tradeMode">Trade Mode</param>
    /// <param name="currency">Currency</param>
    /// <param name="price">Price</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual WebCallResult<IEnumerable<OkexMaximumAmount>> GetMaximumAmount(
        string instrumentId,
        OkexTradeMode tradeMode,
        string currency = null,
        decimal? price = null,
        CancellationToken ct = default)
        => GetMaximumAmount_Async(instrumentId, tradeMode, currency, price, ct).Result;
    /// <summary>
    /// Get maximum buy/sell amount or open amount
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="tradeMode">Trade Mode</param>
    /// <param name="currency">Currency</param>
    /// <param name="price">Price</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<WebCallResult<IEnumerable<OkexMaximumAmount>>> GetMaximumAmount_Async(
        string instrumentId,
        OkexTradeMode tradeMode,
        string currency = null,
        decimal? price = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            {"instId", instrumentId },
            {"tdMode", JsonConvert.SerializeObject(tradeMode, new TradeModeConverter(false)) },
        };
        parameters.AddOptionalParameter("ccy", currency);
        parameters.AddOptionalParameter("px", price?.ToString(OkexGlobals.OkexCultureInfo));

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexMaximumAmount>>>(UnifiedApi.GetUri(Endpoints_V5_Account_MaxSize), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OkexMaximumAmount>>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OkexMaximumAmount>>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data);
    }

    /// <summary>
    /// Get Maximum Available Tradable Amount
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="tradeMode">Trade Mode</param>
    /// <param name="currency">Currency</param>
    /// <param name="reduceOnly">Reduce Only</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual WebCallResult<IEnumerable<OkexMaximumAvailableAmount>> GetMaximumAvailableAmount(
        string instrumentId,
        OkexTradeMode tradeMode,
        string currency = null,
        bool? reduceOnly = null,
        CancellationToken ct = default)
        => GetMaximumAvailableAmount_Async(instrumentId, tradeMode, currency, reduceOnly, ct).Result;
    /// <summary>
    /// Get Maximum Available Tradable Amount
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="tradeMode">Trade Mode</param>
    /// <param name="currency">Currency</param>
    /// <param name="reduceOnly">Reduce Only</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<WebCallResult<IEnumerable<OkexMaximumAvailableAmount>>> GetMaximumAvailableAmount_Async(
        string instrumentId,
        OkexTradeMode tradeMode,
        string currency = null,
        bool? reduceOnly = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            {"instId", instrumentId },
            {"tdMode", JsonConvert.SerializeObject(tradeMode, new TradeModeConverter(false)) },
        };
        parameters.AddOptionalParameter("ccy", currency);
        parameters.AddOptionalParameter("reduceOnly", reduceOnly);

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexMaximumAvailableAmount>>>(UnifiedApi.GetUri(Endpoints_V5_Account_MaxAvailSize), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OkexMaximumAvailableAmount>>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OkexMaximumAvailableAmount>>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data);
    }

    /// <summary>
    /// Increase or decrease the margin of the isolated position.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="positionSide">Position Side</param>
    /// <param name="marginAddReduce">Type</param>
    /// <param name="amount">Amount</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual WebCallResult<IEnumerable<OkexMarginAmount>> SetMarginAmount(
        string instrumentId,
        OkexPositionSide positionSide,
        OkexMarginAddReduce marginAddReduce,
        decimal amount,
        CancellationToken ct = default)
        => SetMarginAmount_Async(instrumentId, positionSide, marginAddReduce, amount, ct).Result;
    /// <summary>
    /// Increase or decrease the margin of the isolated position.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="positionSide">Position Side</param>
    /// <param name="marginAddReduce">Type</param>
    /// <param name="amount">Amount</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<WebCallResult<IEnumerable<OkexMarginAmount>>> SetMarginAmount_Async(
        string instrumentId,
        OkexPositionSide positionSide,
        OkexMarginAddReduce marginAddReduce,
        decimal amount,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            {"instId", instrumentId },
            {"posSide", JsonConvert.SerializeObject(positionSide, new PositionSideConverter(false)) },
            {"type", JsonConvert.SerializeObject(marginAddReduce, new MarginAddReduceConverter(false)) },
            {"amt", amount.ToString(OkexGlobals.OkexCultureInfo) },
        };

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexMarginAmount>>>(UnifiedApi.GetUri(Endpoints_V5_Account_PositionMarginBalance), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OkexMarginAmount>>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OkexMarginAmount>>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data);
    }

    /// <summary>
    /// Get the maximum loan of instrument
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="marginMode">Margin Mode</param>
    /// <param name="marginCurrency">Margin Currency</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual WebCallResult<IEnumerable<OkexMaximumLoanAmount>> GetMaximumLoanAmount(
        string instrumentId,
        OkexMarginMode marginMode,
        string marginCurrency = null,
        CancellationToken ct = default)
        => GetMaximumLoanAmount_Async(instrumentId, marginMode, marginCurrency, ct).Result;
    /// <summary>
    /// Get the maximum loan of instrument
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="marginMode">Margin Mode</param>
    /// <param name="marginCurrency">Margin Currency</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<WebCallResult<IEnumerable<OkexMaximumLoanAmount>>> GetMaximumLoanAmount_Async(
        string instrumentId,
        OkexMarginMode marginMode,
        string marginCurrency = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            {"instId", instrumentId },
            {"mgnMode", JsonConvert.SerializeObject(marginMode, new MarginModeConverter(false)) },
        };
        parameters.AddOptionalParameter("mgnCcy", marginCurrency);

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexMaximumLoanAmount>>>(UnifiedApi.GetUri(Endpoints_V5_Account_MaxLoan), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OkexMaximumLoanAmount>>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OkexMaximumLoanAmount>>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data);
    }

    /// <summary>
    /// Get Fee Rates
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="underlying">Underlying</param>
    /// <param name="category">Category</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual WebCallResult<OkexFeeRate> GetFeeRates(
        OkexInstrumentType instrumentType,
        string instrumentId = null,
        string underlying = null,
        OkexFeeRateCategory? category = null,
        CancellationToken ct = default)
        => GetFeeRates_Async(instrumentType, instrumentId, underlying, category, ct).Result;
    /// <summary>
    /// Get Fee Rates
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="underlying">Underlying</param>
    /// <param name="category">Category</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<WebCallResult<OkexFeeRate>> GetFeeRates_Async(
        OkexInstrumentType instrumentType,
        string instrumentId = null,
        string underlying = null,
        OkexFeeRateCategory? category = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            {"instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)) },
        };
        parameters.AddOptionalParameter("instId", instrumentId);
        parameters.AddOptionalParameter("uly", underlying);
        parameters.AddOptionalParameter("category", JsonConvert.SerializeObject(category, new FeeRateCategoryConverter(false)));

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexFeeRate>>>(UnifiedApi.GetUri(Endpoints_V5_Account_TradeFee), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<OkexFeeRate>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<OkexFeeRate>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data.FirstOrDefault());
    }

    /// <summary>
    /// Get interest-accrued
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="currency">Currency</param>
    /// <param name="marginMode">Margin Mode</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual WebCallResult<IEnumerable<OkexInterestAccrued>> GetInterestAccrued(
        string instrumentId = null,
        string currency = null,
        OkexMarginMode? marginMode = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
        => GetInterestAccrued_Async(
        instrumentId,
        currency,
        marginMode,
        after,
        before,
        limit,
        ct).Result;
    /// <summary>
    /// Get interest-accrued
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="currency">Currency</param>
    /// <param name="marginMode">Margin Mode</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<WebCallResult<IEnumerable<OkexInterestAccrued>>> GetInterestAccrued_Async(
        string instrumentId = null,
        string currency = null,
        OkexMarginMode? marginMode = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        if (limit < 1 || limit > 100)
            throw new ArgumentException("Limit can be between 1-100.");

        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("instId", instrumentId);
        parameters.AddOptionalParameter("ccy", currency);
        parameters.AddOptionalParameter("after", after?.ToString());
        parameters.AddOptionalParameter("before", before?.ToString());
        parameters.AddOptionalParameter("limit", limit.ToString());

        if (marginMode.HasValue)
            parameters.AddOptionalParameter("mgnMode", JsonConvert.SerializeObject(marginMode, new MarginModeConverter(false)));

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexInterestAccrued>>>(UnifiedApi.GetUri(Endpoints_V5_Account_InterestAccrued), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OkexInterestAccrued>>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OkexInterestAccrued>>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data);
    }

    /// <summary>
    /// Get the user's current leveraged currency borrowing interest rate
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual WebCallResult<IEnumerable<Objects.Account.OkexInterestRate>> GetInterestRate(
        string currency = null,
        CancellationToken ct = default)
        => GetInterestRate_Async(
        currency,
        ct).Result;
    /// <summary>
    /// Get the user's current leveraged currency borrowing interest rate
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<WebCallResult<IEnumerable<Objects.Account.OkexInterestRate>>> GetInterestRate_Async(
        string currency = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("ccy", currency);

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<Objects.Account.OkexInterestRate>>>(UnifiedApi.GetUri(Endpoints_V5_Account_InterestRate), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<Objects.Account.OkexInterestRate>>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<Objects.Account.OkexInterestRate>>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data);
    }

    // TODO: Get Greeks

    /// <summary>
    /// Set the display type of Greeks.
    /// </summary>
    /// <param name="greeksType">Display type of Greeks.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual WebCallResult<OkexAccountGreeksType> SetGreeks(Enums.OkexGreeksType greeksType, CancellationToken ct = default)
        => SetGreeks_Async(greeksType, ct).Result;
    /// <summary>
    /// Set the display type of Greeks.
    /// </summary>
    /// <param name="greeksType">Display type of Greeks.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<WebCallResult<OkexAccountGreeksType>> SetGreeks_Async(Enums.OkexGreeksType greeksType, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            {"greeksType", JsonConvert.SerializeObject(greeksType, new GreeksTypeConverter(false)) },
        };

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexAccountGreeksType>>>(UnifiedApi.GetUri(Endpoints_V5_Account_SetGreeks), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<OkexAccountGreeksType>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<OkexAccountGreeksType>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data.FirstOrDefault());
    }

    // TODO: Isolated margin trading settings

    /// <summary>
    /// Retrieve the maximum transferable amount.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual WebCallResult<IEnumerable<OkexWithdrawalAmount>> GetMaximumWithdrawals(
        string currency = null,
        CancellationToken ct = default)
        => GetMaximumWithdrawals_Async(
        currency,
        ct).Result;
    /// <summary>
    /// Retrieve the maximum transferable amount.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<WebCallResult<IEnumerable<OkexWithdrawalAmount>>> GetMaximumWithdrawals_Async(
        string currency = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("ccy", currency);

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexWithdrawalAmount>>>(UnifiedApi.GetUri(Endpoints_V5_Account_MaxWithdrawal), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OkexWithdrawalAmount>>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OkexWithdrawalAmount>>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data);
    }


    // TODO: Get account risk state
    // TODO: VIP loans borrow and repay
    // TODO: Get borrow and repay history for VIP loans
    // TODO: Get borrow interest and limit
    // TODO: Position builder
    // TODO: Get PM limitation


    #endregion
}


