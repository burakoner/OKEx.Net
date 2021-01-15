namespace Okex.Net.Enums
{
    public enum OkexFundingAccountType
    {
        TotalAccountAssets,
        Spot,
        Futures,
        C2C,
        Margin,
        FundingAccount,
        PiggyBank,
        Swap,
        Option,
        MiningAccount
    }

    public enum OkexFundingTransferAccountType
    {
        SubAccount,
        Spot,
        Futures,
        C2C,
        Margin,
        FundingAccount,
        PiggyBank,
        Swap,
        Option,
    }

    public enum OkexFundingPiggyBankActionSide
    {
        Purchase,
        Redempt,
    }

    public enum OkexFundingBillType
    {
        Deposit,
        Withdrawal,
        CanceledWithdrawal,
        TransferIntoFutures,
        TransferFromFutures,
        TransferIntoSubAccount,
        TransferFromSubAccount,
        Claim,
        TransferIntoETT,
        TransferFromETT,
        TransferIntoC2C,
        TransferFromC2C,
        TransferIntoMargin,
        TransferFromMargin,
        TransferIntoSpotAccount,
        TransferFromSpotAccount,
    }

    public enum OkexFundingDepositStatus
    {
        WaitingForConfirmation,
        DepositCredited,
        DepositSuccessful,
    }

    public enum OkexFundingWithdrawalStatus
    {
        PendingCancel,
        Cancelled,
        Failed,
        Pending,
        Sending,
        Sent,
        AwaitingEmailVerification,
        AwaitingManualVerification,
        AwaitingIdentityVerification
    }

    public enum OkexFundingWithdrawalDestination
    {
        OKEx,
        CoinAll,
        Others,
    }

}