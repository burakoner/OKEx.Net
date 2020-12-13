using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using Okex.Net.Enums;
using Okex.Net.RestObjects;

namespace Okex.Net.Interfaces
{
	public interface IOkexClientFunding
	{
		/// <summary>
		/// This retrieves information on the balances of all the assets, and the amount that is available or on hold.
		/// Limit: 20 requests per 2 seconds
		/// </summary>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		WebCallResult<IEnumerable<OkexFundingAssetBalance>> Funding_GetAllBalances(CancellationToken ct = default);
		/// <summary>
		/// This retrieves information on the balances of all the assets, and the amount that is available or on hold.
		/// Limit: 20 requests per 2 seconds
		/// </summary>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		Task<WebCallResult<IEnumerable<OkexFundingAssetBalance>>> Funding_GetAllBalances_Async(CancellationToken ct = default);


		/// <summary>
		/// This retrieves information for a single token in your account, including the remaining balance, and the amount available or on hold.
		/// </summary>
		/// <param name="currency">Token symbol, e.g. 'BTC'</param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		WebCallResult<IEnumerable<OkexFundingAssetBalance>> Funding_GetCurrencyBalance(string currency, CancellationToken ct = default);
		/// <summary>
		/// This retrieves information for a single token in your account, including the remaining balance, and the amount available or on hold.
		/// </summary>
		/// <param name="currency">Token symbol, e.g. 'BTC'</param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		Task<WebCallResult<IEnumerable<OkexFundingAssetBalance>>> Funding_GetBalance_Async(string currency, CancellationToken ct = default);


		/// <summary>
		/// Get the valuation of the total assets of the account in btc or fiat currency.
		/// Limit: 1 requests per 20 seconds
		/// </summary>
		/// <param name="accountType">Line of Business Type。0.Total account assets 1.spot 3.futures 4.C2C 5.margin 6.Funding Account 8. PiggyBank 9.swap 12：option 14.mining account Query total assets by default</param>
		/// <param name="valuationCurrency">The valuation according to a certain fiat currency can only be one of the following "BTC USD CNY JPY KRW RUB" The default unit is BTC</param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		WebCallResult<OkexFundingAssetValuation> Funding_GetAssetValuation(OkexFundingAccountType accountType = OkexFundingAccountType.TotalAccountAssets, string valuationCurrency = "BTC", CancellationToken ct = default);
		/// <summary>
		/// Get the valuation of the total assets of the account in btc or fiat currency.
		/// Limit: 1 requests per 20 seconds
		/// </summary>
		/// <param name="accountType">Line of Business Type。0.Total account assets 1.spot 3.futures 4.C2C 5.margin 6.Funding Account 8. PiggyBank 9.swap 12：option 14.mining account Query total assets by default</param>
		/// <param name="valuationCurrency">The valuation according to a certain fiat currency can only be one of the following "BTC USD CNY JPY KRW RUB" The default unit is BTC</param>
		/// <param name="ct">Cancellation Token</param>
		Task<WebCallResult<OkexFundingAssetValuation>> Funding_GetAssetValuation_Async(OkexFundingAccountType accountType = OkexFundingAccountType.TotalAccountAssets, string valuationCurrency = "BTC", CancellationToken ct = default);


		/// <summary>
		/// The account obtains the fund balance information in each account of the sub account
		/// Limit: 1 requests per 20 seconds
		/// </summary>
		/// <param name="subAccountName">Sub Account Name</param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		WebCallResult<OkexFundingSubAccount> Funding_GetSubAccount(string subAccountName, CancellationToken ct = default);
		/// <summary>
		/// The account obtains the fund balance information in each account of the sub account
		/// Limit: 1 requests per 20 seconds
		/// </summary>
		/// <param name="subAccountName">Sub Account Name</param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		Task<WebCallResult<OkexFundingSubAccount>> Funding_GetSubAccount_Async(string subAccountName, CancellationToken ct = default);


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
		WebCallResult<OkexFundingAssetTransfer> Funding_Transfer(string currency, decimal amount, OkexFundingTransferAccountType fromAccount, OkexFundingTransferAccountType toAccount, string? subAccountName = null, string? fromSymbol = null, string? toSymbol = null, CancellationToken ct = default);
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
		Task<WebCallResult<OkexFundingAssetTransfer>> Funding_Transfer_Async(string currency, decimal amount, OkexFundingTransferAccountType fromAccount, OkexFundingTransferAccountType toAccount, string? subAccountName = null, string? fromSymbol = null, string? toSymbol = null, CancellationToken ct = default);


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
		WebCallResult<IEnumerable<OkexFundingBill>> Funding_GetBills(string? currency = null, OkexFundingBillType? type = null, int limit = 100, int? before = null, int? after = null, CancellationToken ct = default);
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
		Task<WebCallResult<IEnumerable<OkexFundingBill>>> Funding_GetBills_Async(string? currency = null, OkexFundingBillType? type = null, int limit = 100, int? before = null, int? after = null, CancellationToken ct = default);


		/// <summary>
		/// This retrieves a list of all currencies. Not all currencies can be traded. Currencies that have not been defined in ISO 4217 may use a custom symbol.
		/// Rate Limit: 20 requests per 2 seconds
		/// </summary>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		WebCallResult<IEnumerable<OkexFundingAssetInformation>> Funding_GetAllCurrencies(CancellationToken ct = default);
		/// <summary>
		/// This retrieves a list of all currencies. Not all currencies can be traded. Currencies that have not been defined in ISO 4217 may use a custom symbol.
		/// Rate Limit: 20 requests per 2 seconds
		/// </summary>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		Task<WebCallResult<IEnumerable<OkexFundingAssetInformation>>> Funding_GetAllCurrencies_Async(CancellationToken ct = default);


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
		WebCallResult<IEnumerable<OkexFundingWithdrawalRequest>> Funding_Withdrawal(string currency, decimal amount, OkexFundinWithdrawalDestination destination, string toAddress, string fundPassword, decimal fee, CancellationToken ct = default);
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
		Task<WebCallResult<IEnumerable<OkexFundingWithdrawalRequest>>> Funding_Withdrawal_Async(string currency, decimal amount, OkexFundinWithdrawalDestination destination, string toAddress, string fundPassword, decimal fee, CancellationToken ct = default);


		/// <summary>
		/// This retrieves the information about the recommended network transaction fee for withdrawals to digital currency addresses. The higher the fees are set, the faster the confirmations.
		/// <param name="currency">Token symbol, e.g. 'BTC', if left blank, information for all tokens will be returned</param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		WebCallResult<IEnumerable<OkexFundingWithdrawalFee>> Funding_GetWithdrawalFees(string? currency = null, CancellationToken ct = default);
		/// <summary>
		/// This retrieves the information about the recommended network transaction fee for withdrawals to digital currency addresses. The higher the fees are set, the faster the confirmations.
		/// <param name="currency">Token symbol, e.g. 'BTC', if left blank, information for all tokens will be returned</param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		Task<WebCallResult<IEnumerable<OkexFundingWithdrawalFee>>> Funding_GetWithdrawalFees_Async(string? currency = null, CancellationToken ct = default);


		/// <summary>
		/// This retrieves up to 100 recent withdrawal records.
		/// Limit: 20 requests per 2 seconds
		/// </summary>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		WebCallResult<IEnumerable<OkexFundingWithdrawalDetails>> Funding_GetWithdrawalHistory(CancellationToken ct = default);
		/// <summary>
		/// This retrieves up to 100 recent withdrawal records.
		/// Limit: 20 requests per 2 seconds
		/// </summary>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		Task<WebCallResult<IEnumerable<OkexFundingWithdrawalDetails>>> Funding_GetWithdrawalHistory_Async(CancellationToken ct = default);


		/// <summary>
		/// This retrieves the withdrawal records of a specific currency.
		/// Limit: 20 requests per 2 seconds
		/// </summary>
		/// <param name="currency">Token symbol</param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		WebCallResult<IEnumerable<OkexFundingWithdrawalDetails>> Funding_GetWithdrawalHistoryByCurrency(string currency, CancellationToken ct = default);
		/// <summary>
		/// This retrieves the withdrawal records of a specific currency.
		/// Limit: 20 requests per 2 seconds
		/// </summary>
		/// <param name="currency">Token symbol</param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		Task<WebCallResult<IEnumerable<OkexFundingWithdrawalDetails>>> Funding_GetWithdrawalHistoryByCurrency_Async(string currency, CancellationToken ct = default);


		/// <summary>
		/// This retrieves the deposit addresses of currencies, including previously used addresses.
		/// Limit: 20 requests per 2 seconds
		/// </summary>
		/// <param name="currency">Token symbol</param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		WebCallResult<IEnumerable<OkexFundingDepositAddress>> Funding_GetDepositAddress(string currency, CancellationToken ct = default);
		/// <summary>
		/// This retrieves the deposit addresses of currencies, including previously used addresses.
		/// Limit: 20 requests per 2 seconds
		/// </summary>
		/// <param name="currency">Token symbol</param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		Task<WebCallResult<IEnumerable<OkexFundingDepositAddress>>> Funding_GetDepositAddress_Async(string currency, CancellationToken ct = default);


		/// <summary>
		/// This retrieves the deposit history of all currencies, up to 100 recent records.
		/// Limit: 20 requests per 2 seconds
		/// </summary>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		WebCallResult<IEnumerable<OkexFundingDepositDetails>> Funding_GetDepositHistory(CancellationToken ct = default);
		/// <summary>
		/// This retrieves the deposit history of all currencies, up to 100 recent records.
		/// Limit: 20 requests per 2 seconds
		/// </summary>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		Task<WebCallResult<IEnumerable<OkexFundingDepositDetails>>> Funding_GetDepositHistory_Async(CancellationToken ct = default);


		/// <summary>
		/// This retrieves the deposit history of a currency, up to 100 recent records returned.
		/// Limit: 20 requests per 2 seconds
		/// </summary>
		/// <param name="currency">Token Symbol</param>
		/// <param name="ct"></param>
		/// <returns></returns>
		WebCallResult<IEnumerable<OkexFundingDepositDetails>> Funding_GetDepositHistoryByCurrency(string currency, CancellationToken ct = default);
		/// <summary>
		/// This retrieves the deposit history of a currency, up to 100 recent records returned.
		/// Limit: 20 requests per 2 seconds
		/// </summary>
		/// <param name="currency">Token Symbol</param>
		/// <param name="ct"></param>
		/// <returns></returns>
		Task<WebCallResult<IEnumerable<OkexFundingDepositDetails>>> Funding_GetDepositHistoryByCurrency_Async(string currency, CancellationToken ct = default);
	}
}