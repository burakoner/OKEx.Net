using CryptoExchange.Net.Objects;
using Okex.Net.Enums;
using Okex.Net.RestObjects;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Okex.Net.Interfaces
{
    public interface IOkexClientFunding
    {
        WebCallResult<IEnumerable<OkexFundingAssetBalance>> Funding_GetAllBalances(CancellationToken ct = default);
        Task<WebCallResult<IEnumerable<OkexFundingAssetBalance>>> Funding_GetAllBalances_Async(CancellationToken ct = default);
        WebCallResult<IEnumerable<OkexFundingAssetInformation>> Funding_GetAllCurrencies(CancellationToken ct = default);
        Task<WebCallResult<IEnumerable<OkexFundingAssetInformation>>> Funding_GetAllCurrencies_Async(CancellationToken ct = default);
        WebCallResult<OkexFundingAssetValuation> Funding_GetAssetValuation(OkexFundingAccountType accountType = OkexFundingAccountType.TotalAccountAssets, string valuationCurrency = "BTC", CancellationToken ct = default);
        Task<WebCallResult<OkexFundingAssetValuation>> Funding_GetAssetValuation_Async(OkexFundingAccountType accountType = OkexFundingAccountType.TotalAccountAssets, string valuationCurrency = "BTC", CancellationToken ct = default);
        Task<WebCallResult<IEnumerable<OkexFundingAssetBalance>>> Funding_GetBalance_Async(string currency, CancellationToken ct = default);
        WebCallResult<IEnumerable<OkexFundingBill>> Funding_GetBills(string? currency = null, OkexFundingBillType? type = null, int limit = 100, int? before = null, int? after = null, CancellationToken ct = default);
        Task<WebCallResult<IEnumerable<OkexFundingBill>>> Funding_GetBills_Async(string? currency = null, OkexFundingBillType? type = null, int limit = 100, int? before = null, int? after = null, CancellationToken ct = default);
        WebCallResult<IEnumerable<OkexFundingAssetBalance>> Funding_GetCurrencyBalance(string currency, CancellationToken ct = default);
        WebCallResult<IEnumerable<OkexFundingDepositAddress>> Funding_GetDepositAddress(string currency, CancellationToken ct = default);
        Task<WebCallResult<IEnumerable<OkexFundingDepositAddress>>> Funding_GetDepositAddress_Async(string currency, CancellationToken ct = default);
        WebCallResult<IEnumerable<OkexFundingDepositDetails>> Funding_GetDepositHistory(CancellationToken ct = default);
        WebCallResult<IEnumerable<OkexFundingDepositDetails>> Funding_GetDepositHistoryByCurrency(string currency, CancellationToken ct = default);
        Task<WebCallResult<IEnumerable<OkexFundingDepositDetails>>> Funding_GetDepositHistoryByCurrency_Async(string currency, CancellationToken ct = default);
        Task<WebCallResult<IEnumerable<OkexFundingDepositDetails>>> Funding_GetDepositHistory_Async(CancellationToken ct = default);
        WebCallResult<OkexFundingSubAccount> Funding_GetSubAccount(string subAccountName, CancellationToken ct = default);
        Task<WebCallResult<OkexFundingSubAccount>> Funding_GetSubAccount_Async(string subAccountName, CancellationToken ct = default);
        WebCallResult<IEnumerable<OkexFundingUserId>> Funding_GetUserID(CancellationToken ct = default);
        Task<WebCallResult<IEnumerable<OkexFundingUserId>>> Funding_GetUserID_Async(CancellationToken ct = default);
        WebCallResult<IEnumerable<OkexFundingWithdrawalFee>> Funding_GetWithdrawalFees(string? currency = null, CancellationToken ct = default);
        Task<WebCallResult<IEnumerable<OkexFundingWithdrawalFee>>> Funding_GetWithdrawalFees_Async(string? currency = null, CancellationToken ct = default);
        WebCallResult<IEnumerable<OkexFundingWithdrawalDetails>> Funding_GetWithdrawalHistory(CancellationToken ct = default);
        WebCallResult<IEnumerable<OkexFundingWithdrawalDetails>> Funding_GetWithdrawalHistoryByCurrency(string currency, CancellationToken ct = default);
        Task<WebCallResult<IEnumerable<OkexFundingWithdrawalDetails>>> Funding_GetWithdrawalHistoryByCurrency_Async(string currency, CancellationToken ct = default);
        Task<WebCallResult<IEnumerable<OkexFundingWithdrawalDetails>>> Funding_GetWithdrawalHistory_Async(CancellationToken ct = default);
        WebCallResult<OkexFundingPiggyBank> Funding_PiggyBank(string currency, decimal amount, OkexFundingPiggyBankActionSide side, CancellationToken ct = default);
        Task<WebCallResult<OkexFundingPiggyBank>> Funding_PiggyBank_Async(string currency, decimal amount, OkexFundingPiggyBankActionSide side, CancellationToken ct = default);
        WebCallResult<OkexFundingAssetTransfer> Funding_Transfer(string currency, decimal amount, OkexFundingTransferAccountType fromAccount, OkexFundingTransferAccountType toAccount, string? subAccountName = null, string? fromSymbol = null, string? toSymbol = null, CancellationToken ct = default);
        Task<WebCallResult<OkexFundingAssetTransfer>> Funding_Transfer_Async(string currency, decimal amount, OkexFundingTransferAccountType fromAccount, OkexFundingTransferAccountType toAccount, string? subAccountName = null, string? fromSymbol = null, string? toSymbol = null, CancellationToken ct = default);
        WebCallResult<IEnumerable<OkexFundingWithdrawalRequest>> Funding_Withdrawal(string currency, decimal amount, OkexFundingWithdrawalDestination destination, string toAddress, string fundPassword, decimal fee, CancellationToken ct = default);
        Task<WebCallResult<IEnumerable<OkexFundingWithdrawalRequest>>> Funding_Withdrawal_Async(string currency, decimal amount, OkexFundingWithdrawalDestination destination, string toAddress, string fundPassword, decimal fee, CancellationToken ct = default);
    }
}