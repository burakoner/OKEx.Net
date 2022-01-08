using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using Newtonsoft.Json;
using Okex.Net.Converters;
using Okex.Net.CoreObjects;
using Okex.Net.Enums;
using Okex.Net.Helpers;
using Okex.Net.RestObjects.Account;
using Okex.Net.RestObjects.SubAccount;
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
        #region Sub-Account API Endpoints
        /// <summary>
        /// applies to master accounts only
        /// </summary>
        /// <param name="enable">Sub-account status，true: Normal ; false: Frozen</param>
        /// <param name="subAccountName">Sub Account Name</param>
        /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
        /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
        /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<IEnumerable<OkexSubAccount>> GetSubAccounts(
            bool? enable = null,
            string subAccountName = null,
            long? after = null,
            long? before = null,
            int limit = 100,
            CancellationToken ct = default) => GetSubAccounts_Async(enable, subAccountName, after, before, limit, ct).Result;
        /// <summary>
        /// applies to master accounts only
        /// </summary>
        /// <param name="enable">Sub-account status，true: Normal ; false: Frozen</param>
        /// <param name="subAccountName">Sub Account Name</param>
        /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
        /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
        /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<IEnumerable<OkexSubAccount>>> GetSubAccounts_Async(
            bool? enable = null,
            string subAccountName = null,
            long? after = null,
            long? before = null,
            int limit = 100,
            CancellationToken ct = default)
        {
            if (limit < 1 || limit > 100)
                throw new ArgumentException("Limit can be between 1-100.");

            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("enable", enable);
            parameters.AddOptionalParameter("subAcct", subAccountName);
            parameters.AddOptionalParameter("after", after?.ToString(OkexGlobals.OkexCultureInfo));
            parameters.AddOptionalParameter("before", before?.ToString(OkexGlobals.OkexCultureInfo));
            parameters.AddOptionalParameter("limit", limit.ToString(OkexGlobals.OkexCultureInfo));

            var result = await SendRequestAsync<OkexRestApiResponse<IEnumerable<OkexSubAccount>>>(GetUrl(Endpoints_V5_SubAccount_List), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<IEnumerable<OkexSubAccount>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<IEnumerable<OkexSubAccount>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage, result.Data.Data));

            return new WebCallResult<IEnumerable<OkexSubAccount>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }

        /// <summary>
        /// applies to master accounts only
        /// </summary>
        /// <param name="password">Funding password of the master account</param>
        /// <param name="subAccountName">Sub Account Name</param>
        /// <param name="apiLabel">APIKey note</param>
        /// <param name="apiPassphrase">APIKey password， supports 6 to 32 characters that include numbers and letters (case sensitive, space symbol is not supported).</param>
        /// <param name="permission">APIKey access</param>
        /// <param name="ipAddresses">Link IP addresses, separate with commas if more than one. Support up to 5 addresses.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<OkexSubAccountApiKey> CreateSubAccountApiKey(
            string password,
            string subAccountName,
            string apiLabel,
            string apiPassphrase,
            OkexApiPermission permission,
            string ipAddresses = null,
            CancellationToken ct = default) => CreateSubAccountApiKey_Async(password, subAccountName, apiLabel, apiPassphrase, permission, ipAddresses, ct).Result;
        /// <summary>
        /// applies to master accounts only
        /// </summary>
        /// <param name="password">Funding password of the master account</param>
        /// <param name="subAccountName">Sub Account Name</param>
        /// <param name="apiLabel">APIKey note</param>
        /// <param name="apiPassphrase">APIKey password， supports 6 to 32 characters that include numbers and letters (case sensitive, space symbol is not supported).</param>
        /// <param name="permission">APIKey access</param>
        /// <param name="ipAddresses">Link IP addresses, separate with commas if more than one. Support up to 5 addresses.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<OkexSubAccountApiKey>> CreateSubAccountApiKey_Async(
            string password,
            string subAccountName,
            string apiLabel,
            string apiPassphrase,
            OkexApiPermission permission,
            string ipAddresses = null,
            CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                {"pwd", password },
                {"subAcct", subAccountName },
                {"label", apiLabel },
                { "Passphrase", apiPassphrase},
                { "perm", JsonConvert.SerializeObject(permission, new ApiPermissionConverter(false))},
            };
            parameters.AddOptionalParameter("ip", ipAddresses);

            var result = await SendRequestAsync<OkexRestApiResponse<IEnumerable<OkexSubAccountApiKey>>>(GetUrl(Endpoints_V5_SubAccount_ApiKey), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<OkexSubAccountApiKey>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<OkexSubAccountApiKey>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage, result.Data.Data));

            return new WebCallResult<OkexSubAccountApiKey>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data.FirstOrDefault(), null);
        }

        /// <summary>
        /// applies to master accounts only
        /// </summary>
        /// <param name="subAccountName">Sub Account Name</param>
        /// <param name="apiKey">API public key</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<OkexSubAccountApiKey> GetSubAccountApiKey(
            string subAccountName,
            string apiKey,
            CancellationToken ct = default) => GetSubAccountApiKey_Async(subAccountName, apiKey, ct).Result;
        /// <summary>
        /// applies to master accounts only
        /// </summary>
        /// <param name="subAccountName">Sub Account Name</param>
        /// <param name="apiKey">API public key</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<OkexSubAccountApiKey>> GetSubAccountApiKey_Async(
            string subAccountName,
            string apiKey,
            CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                {"subAcct", subAccountName },
                {"apiKey", apiKey },
            };

            var result = await SendRequestAsync<OkexRestApiResponse<IEnumerable<OkexSubAccountApiKey>>>(GetUrl(Endpoints_V5_SubAccount_ApiKey), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<OkexSubAccountApiKey>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<OkexSubAccountApiKey>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage, result.Data.Data));

            return new WebCallResult<OkexSubAccountApiKey>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data.FirstOrDefault(), null);
        }

        /// <summary>
        /// applies to master accounts only
        /// </summary>
        /// <param name="password">Funds password of the master account</param>
        /// <param name="subAccountName">Sub Account Name</param>
        /// <param name="apiKey">APIKey note</param>
        /// <param name="apiLabel">APIKey note</param>
        /// <param name="permission">APIKey access</param>
        /// <param name="ipAddresses">Link IP addresses, separate with commas if more than one. Support up to 20 IP addresses.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<OkexSubAccountApiKey> ModifySubAccountApiKey(
            string password,
            string subAccountName,
            string apiKey,
            string apiLabel,
            OkexApiPermission permission,
            string ipAddresses = null,
            CancellationToken ct = default) => ModifySubAccountApiKey_Async(password, subAccountName, apiKey, apiLabel, permission, ipAddresses, ct).Result;
        /// <summary>
        /// applies to master accounts only
        /// </summary>
        /// <param name="password">Funds password of the master account</param>
        /// <param name="subAccountName">Sub Account Name</param>
        /// <param name="apiKey">APIKey note</param>
        /// <param name="apiLabel">APIKey note</param>
        /// <param name="permission">APIKey access</param>
        /// <param name="ipAddresses">Link IP addresses, separate with commas if more than one. Support up to 20 IP addresses.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<OkexSubAccountApiKey>> ModifySubAccountApiKey_Async(
            string password,
            string subAccountName,
            string apiKey,
            string apiLabel,
            OkexApiPermission permission,
            string ipAddresses = null,
            CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                {"pwd", password },
                {"subAcct", subAccountName },
                {"label", apiLabel },
                { "apiKey", apiKey},
                { "perm", JsonConvert.SerializeObject(permission, new ApiPermissionConverter(false))},
            };
            parameters.AddOptionalParameter("ip", ipAddresses);

            var result = await SendRequestAsync<OkexRestApiResponse<IEnumerable<OkexSubAccountApiKey>>>(GetUrl(Endpoints_V5_SubAccount_ModifyApiKey), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<OkexSubAccountApiKey>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<OkexSubAccountApiKey>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage, result.Data.Data));

            return new WebCallResult<OkexSubAccountApiKey>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data.FirstOrDefault(), null);
        }

        /// <summary>
        /// applies to master accounts only
        /// </summary>
        /// <param name="password">Funds password of the master account</param>
        /// <param name="subAccountName">Sub Account Name</param>
        /// <param name="apiKey">API public key</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<OkexSubAccountName> DeleteSubAccountApiKey(
            string password,
            string subAccountName,
            string apiKey,
            CancellationToken ct = default) => DeleteSubAccountApiKey_Async(password, subAccountName, apiKey, ct).Result;
        /// <summary>
        /// applies to master accounts only
        /// </summary>
        /// <param name="password">Funds password of the master account</param>
        /// <param name="subAccountName">Sub Account Name</param>
        /// <param name="apiKey">API public key</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<OkexSubAccountName>> DeleteSubAccountApiKey_Async(
            string password,
            string subAccountName,
            string apiKey,
            CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                {"pwd", password },
                {"subAcct", subAccountName },
                { "apiKey", apiKey},
            };

            var result = await SendRequestAsync<OkexRestApiResponse<IEnumerable<OkexSubAccountName>>>(GetUrl(Endpoints_V5_SubAccount_DeleteApiKey), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<OkexSubAccountName>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<OkexSubAccountName>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage, result.Data.Data));

            return new WebCallResult<OkexSubAccountName>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data.FirstOrDefault(), null);
        }

        /// <summary>
        /// Query detailed balance info of Trading Account of a sub-account via the master account (applies to master accounts only)
        /// </summary>
        /// <param name="subAccountName">Sub Account Name</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<OkexAccountBalance> GetSubAccountBalance(
            string subAccountName,
            CancellationToken ct = default) => GetSubAccountBalance_Async(subAccountName, ct).Result;
        /// <summary>
        /// Query detailed balance info of Trading Account of a sub-account via the master account (applies to master accounts only)
        /// </summary>
        /// <param name="subAccountName">Sub Account Name</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<OkexAccountBalance>> GetSubAccountBalance_Async(
            string subAccountName,
            CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                {"subAcct", subAccountName },
            };

            var result = await SendRequestAsync<OkexRestApiResponse<IEnumerable<OkexAccountBalance>>>(GetUrl(Endpoints_V5_SubAccount_Balances), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<OkexAccountBalance>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<OkexAccountBalance>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage, result.Data.Data));

            return new WebCallResult<OkexAccountBalance>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data.FirstOrDefault(), null);
        }

        /// <summary>
        /// applies to master accounts only
        /// </summary>
        /// <param name="subAccountName">Sub Account Name</param>
        /// <param name="currency">Currency</param>
        /// <param name="type">0: Transfers from master account to sub-account ;1 : Transfers from sub-account to master account.</param>
        /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
        /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
        /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<IEnumerable<OkexSubAccountBill>> GetSubAccountBills(
            string subAccountName = null,
            string currency = null,
            OkexSubAccountTransferType? type = null,
            long? after = null,
            long? before = null,
            int limit = 100,
            CancellationToken ct = default) => GetSubAccountBills_Async(subAccountName, currency, type, after, before, limit, ct).Result;
        /// <summary>
        /// applies to master accounts only
        /// </summary>
        /// <param name="subAccountName">Sub Account Name</param>
        /// <param name="currency">Currency</param>
        /// <param name="type">0: Transfers from master account to sub-account ;1 : Transfers from sub-account to master account.</param>
        /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
        /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
        /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<IEnumerable<OkexSubAccountBill>>> GetSubAccountBills_Async(
            string subAccountName = null,
            string currency = null,
            OkexSubAccountTransferType? type = null,
            long? after = null,
            long? before = null,
            int limit = 100,
            CancellationToken ct = default)
        {
            if (limit < 1 || limit > 100)
                throw new ArgumentException("Limit can be between 1-100.");

            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("subAcct", subAccountName);
            parameters.AddOptionalParameter("ccy", currency);
            parameters.AddOptionalParameter("type", JsonConvert.SerializeObject(type, new SubAccountTransferTypeConverter(false)));
            parameters.AddOptionalParameter("after", after?.ToString(OkexGlobals.OkexCultureInfo));
            parameters.AddOptionalParameter("before", before?.ToString(OkexGlobals.OkexCultureInfo));
            parameters.AddOptionalParameter("limit", limit.ToString(OkexGlobals.OkexCultureInfo));


            var result = await SendRequestAsync<OkexRestApiResponse<IEnumerable<OkexSubAccountBill>>>(GetUrl(Endpoints_V5_SubAccount_Bills), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<IEnumerable<OkexSubAccountBill>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<IEnumerable<OkexSubAccountBill>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage, result.Data.Data));

            return new WebCallResult<IEnumerable<OkexSubAccountBill>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }

        /// <summary>
        /// applies to master accounts only
        /// </summary>
        /// <param name="currency">Currency</param>
        /// <param name="amount">Amount</param>
        /// <param name="fromAccount">6:Funding Account 18:Unified Account</param>
        /// <param name="toAccount">6:Funding Account 18:Unified Account</param>
        /// <param name="fromSubAccountName">Sub-account name of the account that transfers funds out.</param>
        /// <param name="toSubAccountName">Sub-account name of the account that transfers funds in.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<OkexSubAccountTransfer> TransferBetweenSubAccounts(
            string currency,
            decimal amount,
            OkexAccount fromAccount,
            OkexAccount toAccount,
            string fromSubAccountName,
            string toSubAccountName,
            CancellationToken ct = default) => TransferBetweenSubAccounts_Async(currency, amount, fromAccount, toAccount, fromSubAccountName, toSubAccountName, ct).Result;
        /// <summary>
        /// applies to master accounts only
        /// </summary>
        /// <param name="currency">Currency</param>
        /// <param name="amount">Amount</param>
        /// <param name="fromAccount">6:Funding Account 18:Unified Account</param>
        /// <param name="toAccount">6:Funding Account 18:Unified Account</param>
        /// <param name="fromSubAccountName">Sub-account name of the account that transfers funds out.</param>
        /// <param name="toSubAccountName">Sub-account name of the account that transfers funds in.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<OkexSubAccountTransfer>> TransferBetweenSubAccounts_Async(
            string currency,
            decimal amount,
            OkexAccount fromAccount,
            OkexAccount toAccount,
            string fromSubAccountName,
            string toSubAccountName,
            CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                {"ccy", currency },
                {"amt", amount.ToString(OkexGlobals.OkexCultureInfo) },
                {"from", JsonConvert.SerializeObject(fromAccount, new AccountConverter(false)) },
                {"to", JsonConvert.SerializeObject(toAccount, new AccountConverter(false)) },
                {"fromSubAccount", fromSubAccountName },
                {"toSubAccount", toSubAccountName },
            };

            var result = await SendRequestAsync<OkexRestApiResponse<IEnumerable<OkexSubAccountTransfer>>>(GetUrl(Endpoints_V5_SubAccount_Transfer), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<OkexSubAccountTransfer>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<OkexSubAccountTransfer>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage, result.Data.Data));

            return new WebCallResult<OkexSubAccountTransfer>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data.FirstOrDefault(), null);
        }
        #endregion
    }
}