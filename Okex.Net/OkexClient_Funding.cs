using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using Newtonsoft.Json;
using Okex.Net.Converters;
using Okex.Net.CoreObjects;
using Okex.Net.Enums;
using Okex.Net.Helpers;
using Okex.Net.RestObjects.Funding;
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
        #region Funding API Endpoints
        /// <summary>
        /// Retrieve a list of all currencies. Not all currencies can be traded. Currencies that have not been defined in ISO 4217 may use a custom symbol.
        /// </summary>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<IEnumerable<OkexCurrency>> GetCurrencies(CancellationToken ct = default) => GetCurrencies_Async(ct).Result;
        /// <summary>
        /// Retrieve a list of all currencies. Not all currencies can be traded. Currencies that have not been defined in ISO 4217 may use a custom symbol.
        /// </summary>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<IEnumerable<OkexCurrency>>> GetCurrencies_Async(CancellationToken ct = default)
        {
            var result = await SendRequestAsync<OkexRestApiResponse<IEnumerable<OkexCurrency>>>(GetUrl(Endpoints_V5_Asset_Currencies), HttpMethod.Get, ct, null, true).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<IEnumerable<OkexCurrency>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<IEnumerable<OkexCurrency>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage, result.Data.Data));

            return new WebCallResult<IEnumerable<OkexCurrency>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }

        /// <summary>
        /// Retrieve the balances of all the assets, and the amount that is available or on hold.
        /// </summary>
        /// <param name="currency">Currency</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<IEnumerable<OkexFundingBalance>> GetFundingBalance(string currency = null, CancellationToken ct = default) => GetFundingBalance_Async(currency, ct).Result;
        /// <summary>
        /// Retrieve the balances of all the assets, and the amount that is available or on hold.
        /// </summary>
        /// <param name="currency">Currency</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<IEnumerable<OkexFundingBalance>>> GetFundingBalance_Async(string currency = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("ccy", currency);

            var result = await SendRequestAsync<OkexRestApiResponse<IEnumerable<OkexFundingBalance>>>(GetUrl(Endpoints_V5_Asset_Balances), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<IEnumerable<OkexFundingBalance>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<IEnumerable<OkexFundingBalance>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage, result.Data.Data));

            return new WebCallResult<IEnumerable<OkexFundingBalance>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }

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
            CancellationToken ct = default) => FundTransfer_Async(
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
        public virtual async Task<WebCallResult<OkexTransferResponse>> FundTransfer_Async(
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

            var result = await SendRequestAsync<OkexRestApiResponse<IEnumerable<OkexTransferResponse>>>(GetUrl(Endpoints_V5_Asset_Transfer), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<OkexTransferResponse>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<OkexTransferResponse>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage, result.Data.Data));

            return new WebCallResult<OkexTransferResponse>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data.FirstOrDefault(), null);
        }

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
            CancellationToken ct = default) => GetFundingBillDetails_Async(
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
        public virtual async Task<WebCallResult<IEnumerable<OkexFundingBill>>> GetFundingBillDetails_Async(
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

            var result = await SendRequestAsync<OkexRestApiResponse<IEnumerable<OkexFundingBill>>>(GetUrl(Endpoints_V5_Asset_Bills), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<IEnumerable<OkexFundingBill>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<IEnumerable<OkexFundingBill>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage, result.Data.Data));

            return new WebCallResult<IEnumerable<OkexFundingBill>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }

        /// <summary>
        /// Retrieve the deposit addresses of currencies, including previously-used addresses.
        /// </summary>
        /// <param name="currency">Currency</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<IEnumerable<OkexDepositAddress>> GetDepositAddress(string currency = null, CancellationToken ct = default) => GetDepositAddress_Async(currency, ct).Result;
        /// <summary>
        /// Retrieve the deposit addresses of currencies, including previously-used addresses.
        /// </summary>
        /// <param name="currency">Currency</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<IEnumerable<OkexDepositAddress>>> GetDepositAddress_Async(string currency = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> {
                { "ccy", currency },
            };

            var result = await SendRequestAsync<OkexRestApiResponse<IEnumerable<OkexDepositAddress>>>(GetUrl(Endpoints_V5_Asset_DepositAddress), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<IEnumerable<OkexDepositAddress>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<IEnumerable<OkexDepositAddress>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage, result.Data.Data));

            return new WebCallResult<IEnumerable<OkexDepositAddress>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
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
            CancellationToken ct = default) => GetDepositHistory_Async(currency, transactionId, state, after, before, limit, ct).Result;
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
        public virtual async Task<WebCallResult<IEnumerable<OkexDepositHistory>>> GetDepositHistory_Async(
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

            var result = await SendRequestAsync<OkexRestApiResponse<IEnumerable<OkexDepositHistory>>>(GetUrl(Endpoints_V5_Asset_DepositHistory), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<IEnumerable<OkexDepositHistory>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<IEnumerable<OkexDepositHistory>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage, result.Data.Data));

            return new WebCallResult<IEnumerable<OkexDepositHistory>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
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
            CancellationToken ct = default) => Withdraw_Async(
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
        public virtual async Task<WebCallResult<OkexWithdrawalResponse>> Withdraw_Async(
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

            var result = await SendRequestAsync<OkexRestApiResponse<IEnumerable<OkexWithdrawalResponse>>>(GetUrl(Endpoints_V5_Asset_Withdrawal), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<OkexWithdrawalResponse>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<OkexWithdrawalResponse>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage, result.Data.Data));

            return new WebCallResult<OkexWithdrawalResponse>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data.FirstOrDefault(), null);
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
            CancellationToken ct = default) => GetWithdrawalHistory_Async(currency, transactionId, state, after, before, limit, ct).Result;
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
        public virtual async Task<WebCallResult<IEnumerable<OkexWithdrawalHistory>>> GetWithdrawalHistory_Async(
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

            var result = await SendRequestAsync<OkexRestApiResponse<IEnumerable<OkexWithdrawalHistory>>>(GetUrl(Endpoints_V5_Asset_WithdrawalHistory), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<IEnumerable<OkexWithdrawalHistory>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<IEnumerable<OkexWithdrawalHistory>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage, result.Data.Data));

            return new WebCallResult<IEnumerable<OkexWithdrawalHistory>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }

        /// <summary>
        /// PiggyBank Purchase/Redemption
        /// </summary>
        /// <param name="currency">Currency</param>
        /// <param name="amount">Amount</param>
        /// <param name="side">Side</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<OkexPiggyBankActionResponse> PiggyBankAction(
            string currency,
            decimal amount,
            OkexPiggyBankActionSide side,
            CancellationToken ct = default) => PiggyBankAction_Async(
            currency,
            amount,
            side,
            ct).Result;
        /// <summary>
        /// PiggyBank Purchase/Redemption
        /// </summary>
        /// <param name="currency">Currency</param>
        /// <param name="amount">Amount</param>
        /// <param name="side">Side</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<OkexPiggyBankActionResponse>> PiggyBankAction_Async(
            string currency,
            decimal amount,
            OkexPiggyBankActionSide side,
            CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> {
                { "ccy",currency},
                { "amt",amount.ToString(OkexGlobals.OkexCultureInfo)},
                { "side", JsonConvert.SerializeObject(side, new PiggyBankActionSideConverter(false)) },
            };

            var result = await SendRequestAsync<OkexRestApiResponse<IEnumerable<OkexPiggyBankActionResponse>>>(GetUrl(Endpoints_V5_Asset_PurchaseRedempt), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<OkexPiggyBankActionResponse>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<OkexPiggyBankActionResponse>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage, result.Data.Data));

            return new WebCallResult<OkexPiggyBankActionResponse>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data.FirstOrDefault(), null);
        }

        /// <summary>
        /// Get PiggyBank Balance
        /// </summary>
        /// <param name="currency">Currency</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<IEnumerable<OkexPiggyBankBalance>> PiggyBankBalance(string currency = null, CancellationToken ct = default) => PiggyBankBalance_Async(currency, ct).Result;
        /// <summary>
        /// Get PiggyBank Balance
        /// </summary>
        /// <param name="currency">Currency</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<IEnumerable<OkexPiggyBankBalance>>> PiggyBankBalance_Async(string currency = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("ccy", currency);

            var result = await SendRequestAsync<OkexRestApiResponse<IEnumerable<OkexPiggyBankBalance>>>(GetUrl(Endpoints_V5_Asset_PiggyBalance), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<IEnumerable<OkexPiggyBankBalance>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<IEnumerable<OkexPiggyBankBalance>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage, result.Data.Data));

            return new WebCallResult<IEnumerable<OkexPiggyBankBalance>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }

        /// <summary>
        /// only 100 invoices can be generated within a 24 hour period.
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
            CancellationToken ct = default) => GetLightningDeposits_Async(currency, amount, account, ct).Result;
        /// <summary>
        /// only 100 invoices can be generated within a 24 hour period.
        /// </summary>
        /// <param name="currency">Currency</param>
        /// <param name="amount">deposit amount between 0.000001 - 0.1</param>
        /// <param name="account">Receiving account</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<IEnumerable<OkexLightningDeposit>>> GetLightningDeposits_Async(
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

            var result = await SendRequestAsync<OkexRestApiResponse<IEnumerable<OkexLightningDeposit>>>(GetUrl(Endpoints_V5_Asset_DepositLightning), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<IEnumerable<OkexLightningDeposit>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<IEnumerable<OkexLightningDeposit>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage, result.Data.Data));

            return new WebCallResult<IEnumerable<OkexLightningDeposit>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }

        /// <summary>
        /// Used to make a withdrawal via Lightning. Maximum withdrawal amount is 0.1 BTC, minimum amount is 0.000001 BTC. Rolling 24 hour limit of 1 BTC.
        /// </summary>
        /// <param name="currency">Currency</param>
        /// <param name="invoice">Invoice text</param>
        /// <param name="password">fund password</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<OkexLightningWithdrawal> GetLightningWithdrawals(
            string currency,
            string invoice,
            string password,
            CancellationToken ct = default) => GetLightningWithdrawals_Async(currency, invoice, password, ct).Result;
        /// <summary>
        /// Used to make a withdrawal via Lightning. Maximum withdrawal amount is 0.1 BTC, minimum amount is 0.000001 BTC. Rolling 24 hour limit of 1 BTC.
        /// </summary>
        /// <param name="currency">Currency</param>
        /// <param name="invoice">Invoice text</param>
        /// <param name="password">fund password</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<OkexLightningWithdrawal>> GetLightningWithdrawals_Async(
            string currency,
            string invoice,
            string password,
            CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "ccy", currency },
                { "invoice", invoice },
                { "pwd", password },
            };

            var result = await SendRequestAsync<OkexRestApiResponse<IEnumerable<OkexLightningWithdrawal>>>(GetUrl(Endpoints_V5_Asset_WithdrawalLightning), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<OkexLightningWithdrawal>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<OkexLightningWithdrawal>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage, result.Data.Data));

            return new WebCallResult<OkexLightningWithdrawal>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data.FirstOrDefault(), null);
        }


        #endregion
    }
}