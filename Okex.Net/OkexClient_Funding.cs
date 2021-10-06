using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using Newtonsoft.Json;
using Okex.Net.Converters;
using Okex.Net.CoreObjects;
using Okex.Net.Enums;
using Okex.Net.Helpers;
using Okex.Net.RestObjects.Funding;
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
        #region Funding API Endpoints
        public virtual WebCallResult<IEnumerable<OkexCurrency>> GetCurrencies(CancellationToken ct = default) => GetCurrencies_Async(ct).Result;
        public virtual async Task<WebCallResult<IEnumerable<OkexCurrency>>> GetCurrencies_Async(CancellationToken ct = default)
        {
            var result = await SendRequestAsync<OkexApiResponse<IEnumerable<OkexCurrency>>>(GetUrl(Endpoints_V5_Asset_Currencies), HttpMethod.Get, ct, null, true).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<IEnumerable<OkexCurrency>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<IEnumerable<OkexCurrency>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage));

            return new WebCallResult<IEnumerable<OkexCurrency>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }


        public virtual WebCallResult<IEnumerable<OkexFundingBalance>> GetFundingBalance(string currency = null, CancellationToken ct = default) => GetFundingBalance_Async(currency, ct).Result;
        public virtual async Task<WebCallResult<IEnumerable<OkexFundingBalance>>> GetFundingBalance_Async(string currency = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("ccy", currency);

            var result = await SendRequestAsync<OkexApiResponse<IEnumerable<OkexFundingBalance>>>(GetUrl(Endpoints_V5_Asset_Balances), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<IEnumerable<OkexFundingBalance>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<IEnumerable<OkexFundingBalance>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage));

            return new WebCallResult<IEnumerable<OkexFundingBalance>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }


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

            var result = await SendRequestAsync<OkexApiResponse<IEnumerable<OkexTransferResponse>>>(GetUrl(Endpoints_V5_Asset_Transfer), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<OkexTransferResponse>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<OkexTransferResponse>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage));

            return new WebCallResult<OkexTransferResponse>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data.FirstOrDefault(), null);
        }



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

            var result = await SendRequestAsync<OkexApiResponse<IEnumerable<OkexFundingBill>>>(GetUrl(Endpoints_V5_Asset_Bills), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<IEnumerable<OkexFundingBill>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<IEnumerable<OkexFundingBill>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage));

            return new WebCallResult<IEnumerable<OkexFundingBill>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }


        public virtual WebCallResult<IEnumerable<OkexDepositAddress>> GetDepositAddress(string currency = null, CancellationToken ct = default) => GetDepositAddress_Async(currency, ct).Result;
        public virtual async Task<WebCallResult<IEnumerable<OkexDepositAddress>>> GetDepositAddress_Async(string currency = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> {
                { "ccy", currency },
            };

            var result = await SendRequestAsync<OkexApiResponse<IEnumerable<OkexDepositAddress>>>(GetUrl(Endpoints_V5_Asset_DepositAddress), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<IEnumerable<OkexDepositAddress>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<IEnumerable<OkexDepositAddress>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage));

            return new WebCallResult<IEnumerable<OkexDepositAddress>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }


        public virtual WebCallResult<IEnumerable<OkexDepositHistory>> GetDepositHistory(
            string currency = null,
            string transactionId = null,
            OkexDepositState? state = null,
            long? after = null,
            long? before = null,
            int limit = 100,
            CancellationToken ct = default) => GetDepositHistory_Async(currency, transactionId, state, after, before, limit, ct).Result;
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

            var result = await SendRequestAsync<OkexApiResponse<IEnumerable<OkexDepositHistory>>>(GetUrl(Endpoints_V5_Asset_DepositHistory), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<IEnumerable<OkexDepositHistory>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<IEnumerable<OkexDepositHistory>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage));

            return new WebCallResult<IEnumerable<OkexDepositHistory>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }


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

            var result = await SendRequestAsync<OkexApiResponse<IEnumerable<OkexWithdrawalResponse>>>(GetUrl(Endpoints_V5_Asset_Withdrawal), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<OkexWithdrawalResponse>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<OkexWithdrawalResponse>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage));

            return new WebCallResult<OkexWithdrawalResponse>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data.FirstOrDefault(), null);
        }


        public virtual WebCallResult<IEnumerable<OkexWithdrawalHistory>> GetWithdrawalHistory(
            string currency = null,
            string transactionId = null,
            OkexWithdrawalState? state = null,
            long? after = null,
            long? before = null,
            int limit = 100,
            CancellationToken ct = default) => GetWithdrawalHistory_Async(currency, transactionId, state, after, before, limit, ct).Result;
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

            var result = await SendRequestAsync<OkexApiResponse<IEnumerable<OkexWithdrawalHistory>>>(GetUrl(Endpoints_V5_Asset_WithdrawalHistory), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<IEnumerable<OkexWithdrawalHistory>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<IEnumerable<OkexWithdrawalHistory>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage));

            return new WebCallResult<IEnumerable<OkexWithdrawalHistory>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }


        public virtual WebCallResult<OkexPiggyBankActionResponse> PiggyBankAction(
            string currency,
            decimal amount,
            OkexPiggyBankActionSide side,
            CancellationToken ct = default) => PiggyBankAction_Async(
            currency,
            amount,
            side,
            ct).Result;
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

            var result = await SendRequestAsync<OkexApiResponse<IEnumerable<OkexPiggyBankActionResponse>>>(GetUrl(Endpoints_V5_Asset_PurchaseRedempt), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<OkexPiggyBankActionResponse>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<OkexPiggyBankActionResponse>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage));

            return new WebCallResult<OkexPiggyBankActionResponse>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data.FirstOrDefault(), null);
        }


        public virtual WebCallResult<IEnumerable<OkexPiggyBankBalance>> PiggyBankBalance(string currency = null, CancellationToken ct = default) => PiggyBankBalance_Async(currency, ct).Result;
        public virtual async Task<WebCallResult<IEnumerable<OkexPiggyBankBalance>>> PiggyBankBalance_Async(string currency = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("ccy", currency);

            var result = await SendRequestAsync<OkexApiResponse<IEnumerable<OkexPiggyBankBalance>>>(GetUrl(Endpoints_V5_Asset_PiggyBalance), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<IEnumerable<OkexPiggyBankBalance>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<IEnumerable<OkexPiggyBankBalance>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage));

            return new WebCallResult<IEnumerable<OkexPiggyBankBalance>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }


        public virtual WebCallResult<IEnumerable<OkexLightningDeposit>> GetLightningDeposits(
            string currency,
            decimal amount,
            OkexLightningDepositAccount? account = null,
            CancellationToken ct = default) => GetLightningDeposits_Async(currency, amount, account, ct).Result;
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

            var result = await SendRequestAsync<OkexApiResponse<IEnumerable<OkexLightningDeposit>>>(GetUrl(Endpoints_V5_Asset_DepositLightning), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<IEnumerable<OkexLightningDeposit>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<IEnumerable<OkexLightningDeposit>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage));

            return new WebCallResult<IEnumerable<OkexLightningDeposit>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }


        public virtual WebCallResult<OkexLightningWithdrawal> GetLightningWithdrawals(
            string currency,
            string invoice,
            string password,
            CancellationToken ct = default) => GetLightningWithdrawals_Async(currency, invoice, password, ct).Result;
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

            var result = await SendRequestAsync<OkexApiResponse<IEnumerable<OkexLightningWithdrawal>>>(GetUrl(Endpoints_V5_Asset_WithdrawalLightning), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<OkexLightningWithdrawal>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<OkexLightningWithdrawal>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage));

            return new WebCallResult<OkexLightningWithdrawal>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data.FirstOrDefault(), null);
        }


        #endregion
    }
}