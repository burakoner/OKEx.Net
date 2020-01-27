namespace Okex.Net
{
    public enum SpotPeriod
    {
        /// <summary>
        /// 1m
        /// </summary>
        OneMinute,

        /// <summary>
        /// 3m
        /// </summary>
        ThreeMinutes,

        /// <summary>
        /// 5m
        /// </summary>
        FiveMinutes,

        /// <summary>
        /// 15m
        /// </summary>
        FifteenMinutes,

        /// <summary>
        /// 30m
        /// </summary>
        ThirtyMinutes,

        /// <summary>
        /// 1h
        /// </summary>
        OneHour,

        /// <summary>
        /// 2h
        /// </summary>
        TwoHours,

        /// <summary>
        /// 4h
        /// </summary>
        FourHours,

        /// <summary>
        /// 6h
        /// </summary>
        SixHours,

        /// <summary>
        /// 12h
        /// </summary>
        TwelveHours,

        /// <summary>
        /// 1d
        /// </summary>
        OneDay,

        /// <summary>
        /// 1w
        /// </summary>
        OneWeek
    }

    public enum SpotOrderType
    {
        /// <summary>
        /// Limit Order
        /// </summary>
        Limit,

        /// <summary>
        /// Market Order
        /// </summary>
        Market,
    }

    public enum SpotOrderSide
    {
        /// <summary>
        /// Buy
        /// </summary>
        Buy,

        /// <summary>
        /// Sell
        /// </summary>
        Sell
    }

    public enum SpotBillType
    {
        /// <summary>
        /// Funds transferred in/out
        /// </summary>
        Transfer,

        /// <summary>
        /// Funds changed from trades
        /// </summary>
        Trade,

        /// <summary>
        /// Fee rebate as per fee schedule
        /// </summary>
        Rebate,

        /// <summary>
        /// Fee rebate as per fee schedule
        /// </summary>
        Fee,

        /*
        Deposit,
        Withdraw,
        Buy,
        Sell,
        BeginnersTask,
        InviteFriendsToCompleteBeginnersTask,
        DeductionOfTaskReward,
        InvitationBonus,
        CanceledWithdrawal,
        DeductedForEvents,
        ReceivedFromEvents,
        TransferFromFutures,
        TransferToFutures,
        TransactionFeeRebate,
        ReceiveRedPacket,
        SendRedPacket,
        C2CBuy,
        C2CSell,
        Deduct,
        Convert,
        TransferToAssetsAccount,
        TransferFromAssetsAccount,
        TransferToC2CAccount,
        TransferFromC2CAccount,
        TransferToMarginAccount,
        TransferFromMarginAccount,
        Borrow,
        Repay,
        MarketMakerBonus,
        MarketMakerRebate,
        FeeSettledWithLP,
        PurchaseLoyaltyPoints,
        TransferLoyaltyPoints,
        MMProgramBonus,
        MMProgramRebate,
        TransferFromSpotAccount,
        TransferToSpotAccount,
        TransferToETT,
        TransferFromETT,
        DeductedForMining,
        GainFromMining,
        ExtraYield,
        IncentiveBonusDistribution,
        TransferFromOKPiggyBank,
        TransferToOKPiggyBank,
        TransferFromSwapAccount,
        TransferToSwapAccount,
        RepayBonus,
        MarginFeeSettledWithLP
        */
    }

    public enum SpotTimeInForce
    {
        NormalOrder,
        PostOnly,
        FillOrKil,
        ImmediateOrCancel
    }

    public enum SpotOrderState
    {
        Failed,
        Canceled,
        Open,
        PartiallyFilled,
        FullyFilled,
        Submitting,
        Canceling,

        /// <summary>
        /// Open + PartiallyFilled
        /// </summary>
        Incomplete,

        /// <summary>
        /// Canceled + fully Filled
        /// </summary>
        Complete
    }

    public enum SpotOrderBookDepth
    {
        All,
        Five,
    }

    public enum SpotOrderBookDataType
    {
        /// <summary>
        /// This does not exists normally. Used for Rest Api response
        /// </summary>
        Api,

        /// <summary>
        /// This does not exists normally. Used for Web Socket Depth5 response
        /// </summary>
        DepthTop5,

        /// <summary>
        /// Web Socket Order Book Partial Data
        /// </summary>
        DepthPartial,

        /// <summary>
        /// Web Socket Order Book Update Data
        /// </summary>
        DepthUpdate,
    }

    public enum SpotMarginOrderSourceType
    {
        Spot,
        Margin,
    }

    public enum FundingAccountType
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

    public enum FundingTransferAccountType
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

    public enum FundingBillType
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

    public enum FundingDepositStatus
    {
        WaitingForConfirmation,
        DepositCredited,
        DepositSuccessful,
    }

    public enum FundingWithdrawalStatus
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

    public enum FundinWithdrawalDestination
    {
        OKEx,
        CoinAll,
        Others,
    }

}