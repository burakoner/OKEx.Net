namespace Okex.Net;

public partial class OkexClient
{
    #region Funding API Endpoints
    /// <summary>
    /// Retrieve a list of all currencies. Not all currencies can be traded. Currencies that have not been defined in ISO 4217 may use a custom symbol.
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual WebCallResult<IEnumerable<OkexCurrency>> GetCurrencies(CancellationToken ct = default)
        => GetCurrenciesAsync(ct).Result;
    /// <summary>
    /// Retrieve a list of all currencies. Not all currencies can be traded. Currencies that have not been defined in ISO 4217 may use a custom symbol.
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<WebCallResult<IEnumerable<OkexCurrency>>> GetCurrenciesAsync(CancellationToken ct = default)
    {
        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexCurrency>>>(UnifiedApi.GetUri(Endpoints_V5_Asset_Currencies), HttpMethod.Get, ct, null, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OkexCurrency>>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OkexCurrency>>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data);
    }

    /// <summary>
    /// Retrieve the balances of all the assets, and the amount that is available or on hold.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual WebCallResult<IEnumerable<OkexFundingBalance>> GetFundingBalance(string currency = null, CancellationToken ct = default)
        => GetFundingBalanceAsync(currency, ct).Result;
    /// <summary>
    /// Retrieve the balances of all the assets, and the amount that is available or on hold.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<WebCallResult<IEnumerable<OkexFundingBalance>>> GetFundingBalanceAsync(string currency = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("ccy", currency);

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexFundingBalance>>>(UnifiedApi.GetUri(Endpoints_V5_Asset_Balances), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OkexFundingBalance>>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OkexFundingBalance>>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data);
    }

    // TODO: Get account asset valuation

    /// <summary>
    /// This endpoint supports the transfer of funds between your funding account and trading account, and from the master account to sub-accounts. Direct transfers between sub-accounts are not allowed.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="amount">Amount</param>
    /// <param name="type">Transfer type</param>
    /// <param name="fromAccount">The remitting account</param>
    /// <param name="toAccount">The beneficiary account</param>
    /// <param name="subAccountName">Sub Account Name</param>
    /// <param name="fromInstrumentId">MARGIN trading pair (e.g. BTC-USDT) or contract underlying (e.g. BTC-USD) to be transferred out.</param>
    /// <param name="toInstrumentId">MARGIN trading pair (e.g. BTC-USDT) or contract underlying (e.g. BTC-USD) to be transferred in.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual WebCallResult<OkexTransferResponse> FundTransfer(
        string currency,
        decimal amount,
        OkexTransferType type,
        OkexAccount fromAccount,
        OkexAccount toAccount,
        string subAccountName = null,
        string fromInstrumentId = null,
        string toInstrumentId = null,
        CancellationToken ct = default)
        => FundTransferAsync(
        currency,
        amount,
        type,
        fromAccount,
        toAccount,
        subAccountName,
        fromInstrumentId,
        toInstrumentId,
        ct).Result;
    /// <summary>
    /// This endpoint supports the transfer of funds between your funding account and trading account, and from the master account to sub-accounts. Direct transfers between sub-accounts are not allowed.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="amount">Amount</param>
    /// <param name="type">Transfer type</param>
    /// <param name="fromAccount">The remitting account</param>
    /// <param name="toAccount">The beneficiary account</param>
    /// <param name="subAccountName">Sub Account Name</param>
    /// <param name="fromInstrumentId">MARGIN trading pair (e.g. BTC-USDT) or contract underlying (e.g. BTC-USD) to be transferred out.</param>
    /// <param name="toInstrumentId">MARGIN trading pair (e.g. BTC-USDT) or contract underlying (e.g. BTC-USD) to be transferred in.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<WebCallResult<OkexTransferResponse>> FundTransferAsync(
        string currency,
        decimal amount,
        OkexTransferType type,
        OkexAccount fromAccount,
        OkexAccount toAccount,
        string subAccountName = null,
        string fromInstrumentId = null,
        string toInstrumentId = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "ccy",currency},
            { "amt",amount.ToString(OkexGlobals.OkexCultureInfo)},
            { "type", JsonConvert.SerializeObject(type, new TransferTypeConverter(false)) },
            { "from", JsonConvert.SerializeObject(fromAccount, new AccountConverter(false)) },
            { "to", JsonConvert.SerializeObject(toAccount, new AccountConverter(false)) },
        };
        parameters.AddOptionalParameter("subAcct", subAccountName);
        parameters.AddOptionalParameter("instId", fromInstrumentId);
        parameters.AddOptionalParameter("toInstId", toInstrumentId);

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexTransferResponse>>>(UnifiedApi.GetUri(Endpoints_V5_Asset_Transfer), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<OkexTransferResponse>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<OkexTransferResponse>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data.FirstOrDefault());
    }

    // TODO: Get funds transfer state

    /// <summary>
    /// Query the billing record, you can get the latest 1 month historical data
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="type">Bill type</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual WebCallResult<IEnumerable<OkexFundingBill>> GetFundingBillDetails(
        string currency = null,
        OkexFundingBillType? type = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
        => GetFundingBillDetailsAsync(
        currency,
        type,
        after,
        before,
        limit,
        ct).Result;
    /// <summary>
    /// Query the billing record, you can get the latest 1 month historical data
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="type">Bill type</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<WebCallResult<IEnumerable<OkexFundingBill>>> GetFundingBillDetailsAsync(
        string currency = null,
        OkexFundingBillType? type = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        if (limit < 1 || limit > 100)
            throw new ArgumentException("Limit can be between 1-100.");

        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("ccy", currency);
        if (type.HasValue)
            parameters.AddOptionalParameter("type", JsonConvert.SerializeObject(type, new FundingBillTypeConverter(false)));
        parameters.AddOptionalParameter("after", after?.ToString(OkexGlobals.OkexCultureInfo));
        parameters.AddOptionalParameter("before", before?.ToString(OkexGlobals.OkexCultureInfo));
        parameters.AddOptionalParameter("limit", limit.ToString(OkexGlobals.OkexCultureInfo));

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexFundingBill>>>(UnifiedApi.GetUri(Endpoints_V5_Asset_Bills), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OkexFundingBill>>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OkexFundingBill>>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data);
    }

    /// <summary>
    /// Users can create up to 10,000 different invoices within 24 hours.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="amount">deposit amount between 0.000001 - 0.1</param>
    /// <param name="account">Receiving account</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual WebCallResult<IEnumerable<OkexLightningDeposit>> GetLightningDeposits(
        string currency,
        decimal amount,
        OkexLightningDepositAccount? account = null,
        CancellationToken ct = default)
        => GetLightningDepositsAsync(currency, amount, account, ct).Result;
    /// <summary>
    /// Users can create up to 10,000 different invoices within 24 hours.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="amount">deposit amount between 0.000001 - 0.1</param>
    /// <param name="account">Receiving account</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<WebCallResult<IEnumerable<OkexLightningDeposit>>> GetLightningDepositsAsync(
        string currency,
        decimal amount,
        OkexLightningDepositAccount? account = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "ccy", currency },
            { "amt", amount.ToString(OkexGlobals.OkexCultureInfo) },
        };
        if (account.HasValue)
            parameters.AddOptionalParameter("to", JsonConvert.SerializeObject(account, new LightningDepositAccountConverter(false)));

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexLightningDeposit>>>(UnifiedApi.GetUri(Endpoints_V5_Asset_DepositLightning), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OkexLightningDeposit>>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OkexLightningDeposit>>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data);
    }

    /// <summary>
    /// Retrieve the deposit addresses of currencies, including previously-used addresses.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual WebCallResult<IEnumerable<OkexDepositAddress>> GetDepositAddress(string currency = null, CancellationToken ct = default)
        => GetDepositAddressAsync(currency, ct).Result;
    /// <summary>
    /// Retrieve the deposit addresses of currencies, including previously-used addresses.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<WebCallResult<IEnumerable<OkexDepositAddress>>> GetDepositAddressAsync(string currency = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "ccy", currency },
        };

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexDepositAddress>>>(UnifiedApi.GetUri(Endpoints_V5_Asset_DepositAddress), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OkexDepositAddress>>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OkexDepositAddress>>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data);
    }

    /// <summary>
    /// Retrieve the deposit history of all currencies, up to 100 recent records in a year.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="transactionId">Transaction ID</param>
    /// <param name="state">State</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual WebCallResult<IEnumerable<OkexDepositHistory>> GetDepositHistory(
        string currency = null,
        string transactionId = null,
        OkexDepositState? state = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
        => GetDepositHistoryAsync(currency, transactionId, state, after, before, limit, ct).Result;
    /// <summary>
    /// Retrieve the deposit history of all currencies, up to 100 recent records in a year.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="transactionId">Transaction ID</param>
    /// <param name="state">State</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<WebCallResult<IEnumerable<OkexDepositHistory>>> GetDepositHistoryAsync(
        string currency = null,
        string transactionId = null,
        OkexDepositState? state = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        if (limit < 1 || limit > 100)
            throw new ArgumentException("Limit can be between 1-100.");

        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("ccy", currency);
        parameters.AddOptionalParameter("txId", transactionId);
        if (state.HasValue)
            parameters.AddOptionalParameter("state", JsonConvert.SerializeObject(state, new DepositStateConverter(false)));
        parameters.AddOptionalParameter("after", after?.ToString(OkexGlobals.OkexCultureInfo));
        parameters.AddOptionalParameter("before", before?.ToString(OkexGlobals.OkexCultureInfo));
        parameters.AddOptionalParameter("limit", limit.ToString(OkexGlobals.OkexCultureInfo));

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexDepositHistory>>>(UnifiedApi.GetUri(Endpoints_V5_Asset_DepositHistory), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OkexDepositHistory>>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OkexDepositHistory>>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data);
    }

    /// <summary>
    /// Withdrawal of tokens.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="amount">Amount</param>
    /// <param name="destination">Withdrawal destination address</param>
    /// <param name="toAddress">Verified digital currency address, email or mobile number. Some digital currency addresses are formatted as 'address+tag', e.g. 'ARDOR-7JF3-8F2E-QUWZ-CAN7F:123456'</param>
    /// <param name="password">Trade password</param>
    /// <param name="fee">Transaction fee</param>
    /// <param name="chain">Chain name. There are multiple chains under some currencies, such as USDT has USDT-ERC20, USDT-TRC20, and USDT-Omni. If this parameter is not filled in because it is not available, it will default to the main chain.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual WebCallResult<OkexWithdrawalResponse> Withdraw(
        string currency,
        decimal amount,
        OkexWithdrawalDestination destination,
        string toAddress,
        string password,
        decimal fee,
        string chain = null,
        CancellationToken ct = default)
        => WithdrawAsync(
        currency,
        amount,
        destination,
        toAddress,
        password,
        fee,
        chain,
        ct).Result;
    /// <summary>
    /// Withdrawal of tokens.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="amount">Amount</param>
    /// <param name="destination">Withdrawal destination address</param>
    /// <param name="toAddress">Verified digital currency address, email or mobile number. Some digital currency addresses are formatted as 'address+tag', e.g. 'ARDOR-7JF3-8F2E-QUWZ-CAN7F:123456'</param>
    /// <param name="password">Trade password</param>
    /// <param name="fee">Transaction fee</param>
    /// <param name="chain">Chain name. There are multiple chains under some currencies, such as USDT has USDT-ERC20, USDT-TRC20, and USDT-Omni. If this parameter is not filled in because it is not available, it will default to the main chain.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<WebCallResult<OkexWithdrawalResponse>> WithdrawAsync(
        string currency,
        decimal amount,
        OkexWithdrawalDestination destination,
        string toAddress,
        string password,
        decimal fee,
        string chain = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "ccy",currency},
            { "amt",amount.ToString(OkexGlobals.OkexCultureInfo)},
            { "dest", JsonConvert.SerializeObject(destination, new WithdrawalDestinationConverter(false)) },
            { "toAddr",toAddress},
            { "pwd",password},
            { "fee",fee   .ToString(OkexGlobals.OkexCultureInfo)},
        };
        parameters.AddOptionalParameter("chain", chain);

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexWithdrawalResponse>>>(UnifiedApi.GetUri(Endpoints_V5_Asset_Withdrawal), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<OkexWithdrawalResponse>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<OkexWithdrawalResponse>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data.FirstOrDefault());
    }

    /// <summary>
    /// The maximum withdrawal amount is 0.1 BTC per request, and 1 BTC in 24 hours. The minimum withdrawal amount is approximately 0.000001 BTC. Sub-account does not support withdrawal.
    /// </summary>
    /// <param name="currency">Token symbol. Currently only BTC is supported.</param>
    /// <param name="invoice">Invoice text</param>
    /// <param name="memo">Lightning withdrawal memo</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual WebCallResult<OkexLightningWithdrawal> GetLightningWithdrawals(
        string currency,
        string invoice,
        string memo = null,
        CancellationToken ct = default)
        => GetLightningWithdrawalsAsync(currency, invoice, memo, ct).Result;
    /// <summary>
    /// The maximum withdrawal amount is 0.1 BTC per request, and 1 BTC in 24 hours. The minimum withdrawal amount is approximately 0.000001 BTC. Sub-account does not support withdrawal.
    /// </summary>
    /// <param name="currency">Token symbol. Currently only BTC is supported.</param>
    /// <param name="invoice">Invoice text</param>
    /// <param name="memo">Lightning withdrawal memo</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<WebCallResult<OkexLightningWithdrawal>> GetLightningWithdrawalsAsync(
        string currency,
        string invoice,
        string memo = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "ccy", currency },
            { "invoice", invoice },
        };
        if (!string.IsNullOrEmpty(memo))
            parameters.AddOptionalParameter("memo", memo);

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexLightningWithdrawal>>>(UnifiedApi.GetUri(Endpoints_V5_Asset_WithdrawalLightning), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<OkexLightningWithdrawal>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<OkexLightningWithdrawal>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data.FirstOrDefault());
    }

    /// <summary>
    /// Cancel withdrawal
    /// You can cancel normal withdrawal requests, but you cannot cancel withdrawal requests on Lightning.
    /// Rate Limit: 6 requests per second
    /// </summary>
    /// <param name="withdrawalId">Withdrawal ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual WebCallResult<OkexWithdrawalId> CancelWithdrawal(string withdrawalId, CancellationToken ct = default)
        => CancelWithdrawalAsync(withdrawalId, ct).Result;

    /// <summary>
    /// Cancel withdrawal
    /// You can cancel normal withdrawal requests, but you cannot cancel withdrawal requests on Lightning.
    /// Rate Limit: 6 requests per second
    /// </summary>
    /// <param name="withdrawalId">Withdrawal ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<WebCallResult<OkexWithdrawalId>> CancelWithdrawalAsync(string withdrawalId, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "wdId",withdrawalId},
        };

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexWithdrawalId>>>(UnifiedApi.GetUri(Endpoints_V5_Asset_WithdrawalCancel), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<OkexWithdrawalId>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<OkexWithdrawalId>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data.FirstOrDefault());
    }

    /// <summary>
    /// Retrieve the withdrawal records according to the currency, withdrawal status, and time range in reverse chronological order. The 100 most recent records are returned by default.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="transactionId">Transaction ID</param>
    /// <param name="state">State</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual WebCallResult<IEnumerable<OkexWithdrawalHistory>> GetWithdrawalHistory(
        string currency = null,
        string transactionId = null,
        OkexWithdrawalState? state = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
        => GetWithdrawalHistoryAsync(currency, transactionId, state, after, before, limit, ct).Result;
    /// <summary>
    /// Retrieve the withdrawal records according to the currency, withdrawal status, and time range in reverse chronological order. The 100 most recent records are returned by default.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="transactionId">Transaction ID</param>
    /// <param name="state">State</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<WebCallResult<IEnumerable<OkexWithdrawalHistory>>> GetWithdrawalHistoryAsync(
        string currency = null,
        string transactionId = null,
        OkexWithdrawalState? state = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        if (limit < 1 || limit > 100)
            throw new ArgumentException("Limit can be between 1-100.");

        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("ccy", currency);
        parameters.AddOptionalParameter("txId", transactionId);
        if (state.HasValue)
            parameters.AddOptionalParameter("state", JsonConvert.SerializeObject(state, new WithdrawalStateConverter(false)));
        parameters.AddOptionalParameter("after", after?.ToString(OkexGlobals.OkexCultureInfo));
        parameters.AddOptionalParameter("before", before?.ToString(OkexGlobals.OkexCultureInfo));
        parameters.AddOptionalParameter("limit", limit.ToString(OkexGlobals.OkexCultureInfo));

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexWithdrawalHistory>>>(UnifiedApi.GetUri(Endpoints_V5_Asset_WithdrawalHistory), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OkexWithdrawalHistory>>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OkexWithdrawalHistory>>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data);
    }

    // TODO: Small assets convert

    /// <summary>
    /// Get saving balance
    /// Only the assets in the funding account can be used for saving.
    /// Rate Limit: 6 requests per second
    /// </summary>
    /// <param name="currency">Currency, e.g. BTC</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual WebCallResult<IEnumerable<OkexSavingBalance>> GetSavingBalances(string currency = null, CancellationToken ct = default)
        => GetSavingBalancesAsync(currency, ct).Result;
    /// <summary>
    /// Get saving balance
    /// Only the assets in the funding account can be used for saving.
    /// Rate Limit: 6 requests per second
    /// </summary>
    /// <param name="currency">Currency, e.g. BTC</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<WebCallResult<IEnumerable<OkexSavingBalance>>> GetSavingBalancesAsync(string currency = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("ccy", currency);

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexSavingBalance>>>(UnifiedApi.GetUri(Endpoints_V5_Asset_SavingBalance), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OkexSavingBalance>>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OkexSavingBalance>>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data);
    }

    public virtual WebCallResult<OkexSavingActionResponse> SavingPurchaseRedemption(
        string currency,
        decimal amount,
        OkexSavingActionSide side,
        decimal? rate = null,
        CancellationToken ct = default)
        => SavingPurchaseRedemptionAsync(
        currency,
        amount,
        side,
        rate,
        ct).Result;
    public virtual async Task<WebCallResult<OkexSavingActionResponse>> SavingPurchaseRedemptionAsync(
        string currency,
        decimal amount,
        OkexSavingActionSide side,
        decimal? rate = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "ccy",currency},
            { "amt",amount.ToString(OkexGlobals.OkexCultureInfo)},
            { "side", JsonConvert.SerializeObject(side, new SavingActionSideConverter(false)) },
        };
        parameters.AddOptionalParameter("rate", rate?.ToString(OkexGlobals.OkexCultureInfo));

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexSavingActionResponse>>>(UnifiedApi.GetUri(Endpoints_V5_Asset_SavingPurchaseRedempt), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<OkexSavingActionResponse>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<OkexSavingActionResponse>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data.FirstOrDefault());
    }

    // TODO: Set lending rate
    // TODO: Get lending history
    // TODO: Get public borrow info (public)
    // TODO: Get public borrow history (public)
    #endregion
}