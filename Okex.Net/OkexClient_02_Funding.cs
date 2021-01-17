using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using Newtonsoft.Json;
using Okex.Net.Converters;
using Okex.Net.Enums;
using Okex.Net.Helpers;
using Okex.Net.Interfaces;
using Okex.Net.RestObjects;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Okex.Net
{
    public partial class OkexClient : IOkexClientFunding
    {
        #region Funding Account API

        /// <summary>
        /// This retrieves information on the balances of all the assets, and the amount that is available or on hold.
        /// Limit: 20 requests per 2 seconds
        /// </summary>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<IEnumerable<OkexFundingAssetBalance>> Funding_GetAllBalances(CancellationToken ct = default) => Funding_GetAllBalances_Async(ct).Result;
        /// <summary>
        /// This retrieves information on the balances of all the assets, and the amount that is available or on hold.
        /// Limit: 20 requests per 2 seconds
        /// </summary>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<IEnumerable<OkexFundingAssetBalance>>> Funding_GetAllBalances_Async(CancellationToken ct = default)
        {
            return await SendRequest<IEnumerable<OkexFundingAssetBalance>>(GetUrl(Endpoints_Funding_GetAllBalances), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// The account obtains the fund balance information in each account of the sub account
        /// Limit: 1 requests per 20 seconds
        /// </summary>
        /// <param name="subAccountName">Sub Account Name</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<OkexFundingSubAccount> Funding_GetSubAccount(string subAccountName, CancellationToken ct = default) => Funding_GetSubAccount_Async(subAccountName, ct).Result;
        /// <summary>
        /// The account obtains the fund balance information in each account of the sub account
        /// Limit: 1 requests per 20 seconds
        /// </summary>
        /// <param name="subAccountName">Sub Account Name</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<OkexFundingSubAccount>> Funding_GetSubAccount_Async(string subAccountName, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "sub-account", subAccountName },
            };

            return await SendRequest<OkexFundingSubAccount>(GetUrl(Endpoints_Funding_GetSubAccount), HttpMethod.Get, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Get the valuation of the total assets of the account in btc or fiat currency.
        /// Limit: 1 requests per 20 seconds
        /// </summary>
        /// <param name="accountType">Line of Business Type。0.Total account assets 1.spot 3.futures 4.C2C 5.margin 6.Funding Account 8. PiggyBank 9.swap 12：option 14.mining account Query total assets by default</param>
        /// <param name="valuationCurrency">The valuation according to a certain fiat currency can only be one of the following "BTC USD CNY JPY KRW RUB" The default unit is BTC</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<OkexFundingAssetValuation> Funding_GetAssetValuation(OkexFundingAccountType accountType = OkexFundingAccountType.TotalAccountAssets, string valuationCurrency = "BTC", CancellationToken ct = default) => Funding_GetAssetValuation_Async(accountType, valuationCurrency, ct).Result;
        /// <summary>
        /// Get the valuation of the total assets of the account in btc or fiat currency.
        /// Limit: 1 requests per 20 seconds
        /// </summary>
        /// <param name="accountType">Line of Business Type。0.Total account assets 1.spot 3.futures 4.C2C 5.margin 6.Funding Account 8. PiggyBank 9.swap 12：option 14.mining account Query total assets by default</param>
        /// <param name="valuationCurrency">The valuation according to a certain fiat currency can only be one of the following "BTC USD CNY JPY KRW RUB" The default unit is BTC</param>
        /// <param name="ct">Cancellation Token</param>
        public virtual async Task<WebCallResult<OkexFundingAssetValuation>> Funding_GetAssetValuation_Async(OkexFundingAccountType accountType = OkexFundingAccountType.TotalAccountAssets, string valuationCurrency = "BTC", CancellationToken ct = default)
        {
            valuationCurrency = valuationCurrency.ValidateCurrency();

            if (string.IsNullOrEmpty(valuationCurrency) || valuationCurrency.IsNotOneOf("BTC", "USD", "CNY", "JPY", "KRW", "RUB"))
                throw new ArgumentException("The valuation according to a certain fiat currency can only be one of the following BTC, USD, CNY, JPY, KRW, RUB");

            var parameters = new Dictionary<string, object>
            {
                { "account_type", JsonConvert.SerializeObject(accountType, new FundingAccountTypeConverter(false)) },
                { "valuation_currency", valuationCurrency },
            };

            return await SendRequest<OkexFundingAssetValuation>(GetUrl(Endpoints_Funding_GetAssetValuation), HttpMethod.Get, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// This retrieves information for a single token in your account, including the remaining balance, and the amount available or on hold.
        /// </summary>
        /// <param name="currency">Token symbol, e.g. 'BTC'</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<IEnumerable<OkexFundingAssetBalance>> Funding_GetCurrencyBalance(string currency, CancellationToken ct = default) => Funding_GetBalance_Async(currency, ct).Result;
        /// <summary>
        /// This retrieves information for a single token in your account, including the remaining balance, and the amount available or on hold.
        /// </summary>
        /// <param name="currency">Token symbol, e.g. 'BTC'</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<IEnumerable<OkexFundingAssetBalance>>> Funding_GetBalance_Async(string currency, CancellationToken ct = default)
        {
            currency = currency.ValidateCurrency();
            return await SendRequest<IEnumerable<OkexFundingAssetBalance>>(GetUrl(Endpoints_Funding_GetCurrencyBalance, currency), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// This endpoint supports the transfer of funds among your funding account, trading accounts, main account, and sub accounts.
        /// Limit: 1 request per 2 seconds (per currency)
        /// </summary>
        /// <param name="currency">Token symbol, e.g., 'EOS'</param>
        /// <param name="amount">Amount to be transferred</param>
        /// <param name="fromAccount">Remitting account</param>
        /// <param name="toAccount">Receiving account</param>
        /// <param name="subAccountName">Name of the sub account</param>
        /// <param name="fromSymbol">Margin trading pair of token or underlying of USDT-margined futures transferred out, such as: btc-usdt. Limited to trading pairs available for margin trading or underlying of enabled futures trading.</param>
        /// <param name="toSymbol">Margin trading pair of token or underlying of USDT-margined futures transferred in, such as: btc-usdt. Limited to trading pairs available for margin trading or underlying of enabled futures trading.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<OkexFundingAssetTransfer> Funding_Transfer(string currency, decimal amount, OkexFundingTransferAccountType fromAccount, OkexFundingTransferAccountType toAccount, string? subAccountName = null, string? fromSymbol = null, string? toSymbol = null, CancellationToken ct = default) => Funding_Transfer_Async(currency, amount, fromAccount, toAccount, subAccountName, fromSymbol, toSymbol, ct).Result;
        /// <summary>
        /// This endpoint supports the transfer of funds among your funding account, trading accounts, main account, and sub accounts.
        /// Limit: 1 request per 2 seconds (per currency)
        /// </summary>
        /// <param name="currency">Token symbol, e.g., 'EOS'</param>
        /// <param name="amount">Amount to be transferred</param>
        /// <param name="fromAccount">Remitting account</param>
        /// <param name="toAccount">Receiving account</param>
        /// <param name="subAccountName">Name of the sub account</param>
        /// <param name="fromSymbol">Margin trading pair of token or underlying of USDT-margined futures transferred out, such as: btc-usdt. Limited to trading pairs available for margin trading or underlying of enabled futures trading.</param>
        /// <param name="toSymbol">Margin trading pair of token or underlying of USDT-margined futures transferred in, such as: btc-usdt. Limited to trading pairs available for margin trading or underlying of enabled futures trading.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<OkexFundingAssetTransfer>> Funding_Transfer_Async(string currency, decimal amount, OkexFundingTransferAccountType fromAccount, OkexFundingTransferAccountType toAccount, string? subAccountName = null, string? fromSymbol = null, string? toSymbol = null, CancellationToken ct = default)
        {
            currency = currency.ValidateCurrency();

            if (fromAccount == toAccount)
                throw new ArgumentException("'fromAccount' and 'toAccount' must be different.");

            if ((fromAccount == OkexFundingTransferAccountType.SubAccount || toAccount == OkexFundingTransferAccountType.SubAccount) && string.IsNullOrEmpty(subAccountName))
                throw new ArgumentException("When 'fromAccount' or 'toAccount' is SubAccount, subAccountName parameter is required.");

            if (fromAccount == OkexFundingTransferAccountType.SubAccount && toAccount != OkexFundingTransferAccountType.FundingAccount)
                throw new ArgumentException("When 'fromAccount' is SubAccount, 'toAccount' can only be FundingAccount as the sub account can only be transferred to the main account.");

            if ((fromAccount == OkexFundingTransferAccountType.Margin || toAccount == OkexFundingTransferAccountType.Margin) && string.IsNullOrEmpty(fromSymbol))
                throw new ArgumentException("When 'fromAccount' or 'toAccount' is Margin, fromSymbol parameter is required.");

            var parameters = new Dictionary<string, object>
            {
                { "currency", currency },
                { "amount", amount.ToString(ci) },
                { "from", JsonConvert.SerializeObject(fromAccount, new FundingTransferAccountTypeConverter(false)) },
                { "to", JsonConvert.SerializeObject(toAccount, new FundingTransferAccountTypeConverter(false)) },
            };
            parameters.AddOptionalParameter("sub_account", subAccountName);
            parameters.AddOptionalParameter("instrument_id", fromSymbol);
            parameters.AddOptionalParameter("to_instrument_id", toSymbol);

            return await SendRequest<OkexFundingAssetTransfer>(GetUrl(Endpoints_Funding_Transfer, currency), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// This endpoint supports the withdrawal of tokens
        /// Limit: 20 requests per 2 seconds
        /// </summary>
        /// <param name="currency">Token symbol, e.g., 'BTC'</param>
        /// <param name="amount">Withdrawal amount</param>
        /// <param name="destination">withdrawal address(3:OKEx 4:others 68.CoinAll )</param>
        /// <param name="toAddress">Verified digital currency address, email or mobile number. Some digital currency addresses are formatted as 'address+tag', e.g. 'ARDOR-7JF3-8F2E-QUWZ-CAN7F：123456'</param>
        /// <param name="fundPassword">Fund password</param>
        /// <param name="fee">Network transaction fee. Please refer to the withdrawal fees section below for recommended fee amount</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<IEnumerable<OkexFundingWithdrawalRequest>> Funding_Withdrawal(string currency, decimal amount, OkexFundingWithdrawalDestination destination, string toAddress, string fundPassword, decimal fee, CancellationToken ct = default) => Funding_Withdrawal_Async(currency, amount, destination, toAddress, fundPassword, fee, ct).Result;
        /// <summary>
        /// This endpoint supports the withdrawal of tokens
        /// Limit: 20 requests per 2 seconds
        /// </summary>
        /// <param name="currency">Token symbol, e.g., 'BTC'</param>
        /// <param name="amount">Withdrawal amount</param>
        /// <param name="destination">withdrawal address(3:OKEx 4:others 68.CoinAll )</param>
        /// <param name="toAddress">Verified digital currency address, email or mobile number. Some digital currency addresses are formatted as 'address+tag', e.g. 'ARDOR-7JF3-8F2E-QUWZ-CAN7F：123456'</param>
        /// <param name="fundPassword">Fund password</param>
        /// <param name="fee">Network transaction fee. Please refer to the withdrawal fees section below for recommended fee amount</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<IEnumerable<OkexFundingWithdrawalRequest>>> Funding_Withdrawal_Async(string currency, decimal amount, OkexFundingWithdrawalDestination destination, string toAddress, string fundPassword, decimal fee, CancellationToken ct = default)
        {
            currency = currency.ValidateCurrency();

            var parameters = new Dictionary<string, object>
            {
                { "currency", currency },
                { "amount", amount.ToString(ci) },
                { "destination", JsonConvert.SerializeObject(destination, new FundingWithdrawalDestinationConverter(false)) },
                { "to_address", toAddress },
                { "trade_pwd", fundPassword },
                { "fee", fee.ToString(ci) },
            };

            return await SendRequest<IEnumerable<OkexFundingWithdrawalRequest>>(GetUrl(Endpoints_Funding_Withdrawal), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// This retrieves up to 100 recent withdrawal records.
        /// Limit: 20 requests per 2 seconds
        /// </summary>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<IEnumerable<OkexFundingWithdrawalDetails>> Funding_GetWithdrawalHistory(CancellationToken ct = default) => Funding_GetWithdrawalHistory_Async(ct).Result;
        /// <summary>
        /// This retrieves up to 100 recent withdrawal records.
        /// Limit: 20 requests per 2 seconds
        /// </summary>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<IEnumerable<OkexFundingWithdrawalDetails>>> Funding_GetWithdrawalHistory_Async(CancellationToken ct = default)
        {
            return await SendRequest<IEnumerable<OkexFundingWithdrawalDetails>>(GetUrl(Endpoints_Funding_WithdrawalHistory), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// This retrieves the withdrawal records of a specific currency.
        /// Limit: 20 requests per 2 seconds
        /// </summary>
        /// <param name="currency">Token symbol</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<IEnumerable<OkexFundingWithdrawalDetails>> Funding_GetWithdrawalHistoryByCurrency(string currency, CancellationToken ct = default) => Funding_GetWithdrawalHistoryByCurrency_Async(currency, ct).Result;
        /// <summary>
        /// This retrieves the withdrawal records of a specific currency.
        /// Limit: 20 requests per 2 seconds
        /// </summary>
        /// <param name="currency">Token symbol</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<IEnumerable<OkexFundingWithdrawalDetails>>> Funding_GetWithdrawalHistoryByCurrency_Async(string currency, CancellationToken ct = default)
        {
            currency = currency.ValidateCurrency();
            return await SendRequest<IEnumerable<OkexFundingWithdrawalDetails>>(GetUrl(Endpoints_Funding_WithdrawalHistoryOfCurrency, currency), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// This retrieves the account bills dating back the past month. Pagination is supported and the response is sorted with most recent first in reverse chronological order. Latest 1 month records will be returned at maximum.
        /// Rate Limit: 20 requests per 2 seconds
        /// </summary>
        /// <param name="currency">The token symbol, e.g. 'BTC'. Complete account statement for will be returned if the field is left blank</param>
        /// <param name="type">Type of Bill</param>
        /// <param name="limit">Number of results per request. The maximum is 100; the default is 100</param>
        /// <param name="before">Pagination of data to return records newer than the requested ledger_id</param>
        /// <param name="after">Pagination of data to return records earlier than the requested ledger_id</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<IEnumerable<OkexFundingBill>> Funding_GetBills(string? currency = null, OkexFundingBillType? type = null, int limit = 100, int? before = null, int? after = null, CancellationToken ct = default) => Funding_GetBills_Async(currency, type, limit, before, after, ct).Result;
        /// <summary>
        /// This retrieves the account bills dating back the past month. Pagination is supported and the response is sorted with most recent first in reverse chronological order. Latest 1 month records will be returned at maximum.
        /// Rate Limit: 20 requests per 2 seconds
        /// </summary>
        /// <param name="currency">The token symbol, e.g. 'BTC'. Complete account statement for will be returned if the field is left blank</param>
        /// <param name="type">Type of Bill</param>
        /// <param name="limit">Number of results per request. The maximum is 100; the default is 100</param>
        /// <param name="before">Pagination of data to return records newer than the requested ledger_id</param>
        /// <param name="after">Pagination of data to return records earlier than the requested ledger_id</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<IEnumerable<OkexFundingBill>>> Funding_GetBills_Async(string? currency = null, OkexFundingBillType? type = null, int limit = 100, int? before = null, int? after = null, CancellationToken ct = default)
        {
            if (!string.IsNullOrEmpty(currency)) currency = currency?.ValidateCurrency();
            limit.ValidateIntBetween(nameof(limit), 1, 100);

            var parameters = new Dictionary<string, object>
            {
                { "limit", limit.ToString(ci) },
            };
            parameters.AddOptionalParameter("currency", currency);
            if (type != null) parameters.AddOptionalParameter("type", JsonConvert.SerializeObject(type, new FundingBillTypeConverter(false)));
            parameters.AddOptionalParameter("before", before?.ToString(ci));
            parameters.AddOptionalParameter("after", after?.ToString(ci));

            return await SendRequest<IEnumerable<OkexFundingBill>>(GetUrl(Endpoints_Funding_Bills), HttpMethod.Get, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// This retrieves the deposit addresses of currencies, including previously used addresses.
        /// Limit: 20 requests per 2 seconds
        /// </summary>
        /// <param name="currency">Token symbol</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<IEnumerable<OkexFundingDepositAddress>> Funding_GetDepositAddress(string currency, CancellationToken ct = default) => Funding_GetDepositAddress_Async(currency, ct).Result;
        /// <summary>
        /// This retrieves the deposit addresses of currencies, including previously used addresses.
        /// Limit: 20 requests per 2 seconds
        /// </summary>
        /// <param name="currency">Token symbol</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<IEnumerable<OkexFundingDepositAddress>>> Funding_GetDepositAddress_Async(string currency, CancellationToken ct = default)
        {
            currency = currency.ValidateCurrency();

            var parameters = new Dictionary<string, object>
            {
                { "currency", currency },
            };

            return await SendRequest<IEnumerable<OkexFundingDepositAddress>>(GetUrl(Endpoints_Funding_DepositAddress), HttpMethod.Get, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// This retrieves the deposit history of all currencies, up to 100 recent records.
        /// Limit: 20 requests per 2 seconds
        /// </summary>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<IEnumerable<OkexFundingDepositDetails>> Funding_GetDepositHistory(CancellationToken ct = default) => Funding_GetDepositHistory_Async(ct).Result;
        /// <summary>
        /// This retrieves the deposit history of all currencies, up to 100 recent records.
        /// Limit: 20 requests per 2 seconds
        /// </summary>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<IEnumerable<OkexFundingDepositDetails>>> Funding_GetDepositHistory_Async(CancellationToken ct = default)
        {
            return await SendRequest<IEnumerable<OkexFundingDepositDetails>>(GetUrl(Endpoints_Funding_DepositHistory), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// This retrieves the deposit history of a currency, up to 100 recent records returned.
        /// Limit: 20 requests per 2 seconds
        /// </summary>
        /// <param name="currency">Token Symbol</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<IEnumerable<OkexFundingDepositDetails>> Funding_GetDepositHistoryByCurrency(string currency, CancellationToken ct = default) => Funding_GetDepositHistoryByCurrency_Async(currency, ct).Result;
        /// <summary>
        /// This retrieves the deposit history of a currency, up to 100 recent records returned.
        /// Limit: 20 requests per 2 seconds
        /// </summary>
        /// <param name="currency">Token Symbol</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<IEnumerable<OkexFundingDepositDetails>>> Funding_GetDepositHistoryByCurrency_Async(string currency, CancellationToken ct = default)
        {
            currency = currency.ValidateCurrency();
            return await SendRequest<IEnumerable<OkexFundingDepositDetails>>(GetUrl(Endpoints_Funding_DepositHistoryOfCurrency, currency), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// This retrieves a list of all currencies. Not all currencies can be traded. Currencies that have not been defined in ISO 4217 may use a custom symbol.
        /// Rate Limit: 20 requests per 2 seconds
        /// </summary>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<IEnumerable<OkexFundingAssetInformation>> Funding_GetAllCurrencies(CancellationToken ct = default) => Funding_GetAllCurrencies_Async(ct).Result;
        /// <summary>
        /// This retrieves a list of all currencies. Not all currencies can be traded. Currencies that have not been defined in ISO 4217 may use a custom symbol.
        /// Rate Limit: 20 requests per 2 seconds
        /// </summary>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<IEnumerable<OkexFundingAssetInformation>>> Funding_GetAllCurrencies_Async(CancellationToken ct = default)
        {
            return await SendRequest<IEnumerable<OkexFundingAssetInformation>>(GetUrl(Endpoints_Funding_GetCurrencies), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Get the unique UID of the account。
        /// Rate Limit: 1 requests per second
        /// </summary>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<IEnumerable<OkexFundingUserId>> Funding_GetUserID(CancellationToken ct = default) => Funding_GetUserID_Async(ct).Result;
        /// <summary>
        /// Get the unique UID of the account。
        /// Rate Limit: 1 requests per second
        /// </summary>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<IEnumerable<OkexFundingUserId>>> Funding_GetUserID_Async(CancellationToken ct = default)
        {
            return await SendRequest<IEnumerable<OkexFundingUserId>>(GetUrl(Endpoints_Funding_GetUserID), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// This retrieves the information about the recommended network transaction fee for withdrawals to digital currency addresses. The higher the fees are set, the faster the confirmations.
        /// <param name="currency">Token symbol, e.g. 'BTC', if left blank, information for all tokens will be returned</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<IEnumerable<OkexFundingWithdrawalFee>> Funding_GetWithdrawalFees(string? currency = null, CancellationToken ct = default) => Funding_GetWithdrawalFees_Async(currency, ct).Result;
        /// <summary>
        /// This retrieves the information about the recommended network transaction fee for withdrawals to digital currency addresses. The higher the fees are set, the faster the confirmations.
        /// <param name="currency">Token symbol, e.g. 'BTC', if left blank, information for all tokens will be returned</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<IEnumerable<OkexFundingWithdrawalFee>>> Funding_GetWithdrawalFees_Async(string? currency = null, CancellationToken ct = default)
        {
            if (!string.IsNullOrEmpty(currency)) currency = currency?.ValidateCurrency();

            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("currency", currency);

            return await SendRequest<IEnumerable<OkexFundingWithdrawalFee>>(GetUrl(Endpoints_Funding_WithdrawalFees), HttpMethod.Get, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// This endpoint supports the purchase and redemption of PiggyBank.
        /// Limit: 6 requests per second
        /// </summary>
        /// <param name="currency">Token symbol, e.g., 'BTC'</param>
        /// <param name="amount">purchase/redempt amount</param>
        /// <param name="side">action type. Purchase: purchase shares of PiggyBank, Redempt: redempt shares of PiggyBank</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<OkexFundingPiggyBank> Funding_PiggyBank(string currency, decimal amount, OkexFundingPiggyBankActionSide side, CancellationToken ct = default) => Funding_PiggyBank_Async(currency, amount, side, ct).Result;
        /// <summary>
        /// This endpoint supports the purchase and redemption of PiggyBank.
        /// </summary>
        /// <param name="currency">Token symbol, e.g., 'BTC'</param>
        /// <param name="amount">purchase/redempt amount</param>
        /// <param name="side">action type. Purchase: purchase shares of PiggyBank, Redempt: redempt shares of PiggyBank</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<OkexFundingPiggyBank>> Funding_PiggyBank_Async(string currency, decimal amount, OkexFundingPiggyBankActionSide side, CancellationToken ct = default)
        {
            currency = currency.ValidateCurrency();
            var parameters = new Dictionary<string, object>
            {
                { "currency", currency },
                { "amount", amount.ToString(ci) },
                { "side", JsonConvert.SerializeObject(side, new FundingPiggyBankActionSideConverter(false)) },
            };

            return await SendRequest<OkexFundingPiggyBank>(GetUrl(Endpoints_Funding_PiggyBank, currency), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
        }

        #endregion
    }
}
