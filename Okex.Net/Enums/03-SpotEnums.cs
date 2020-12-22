namespace Okex.Net.Enums
{
    public enum OkexSpotPeriod
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

    public enum OkexSpotOrderType
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
    
    public enum OkexSpotOrderSide
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

    public enum OkexSpotBillType
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

    public enum OkexSpotTimeInForce
    {
        NormalOrder,
        PostOnly,
        FillOrKil,
        ImmediateOrCancel
    }

    public enum OkexSpotOrderState
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



}