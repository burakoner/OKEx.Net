namespace Okex.Net.Enums
{
    public enum OkexOptionsType
    {
        C,
        P,
    }

    public enum OkexOptionsState
    {
        PreOpen,
        Live,
        Suspended,
        Settlement,
    }

    public enum OkexOptionsOrderSide
    {
        Buy,
        Sell,
    }
    
    public enum OkexOptionsOrderType
    {
        Buy,
        Sell,
        LiquidationBuy,
        LiquidationSell,
        PartialLiquidationBuy,
        PartialLiquidationSell,
    }

    public enum OkexOptionsDeliverySettlement
    {
        Delivery,
        Settlement,
    }
    
    public enum OkexOptionsSettlementType
    {
        Settled,
        Exercised,
        ExpiredOTM,
    }

    public enum OkexOptionsAccountStatus
    {
        Normal,
        DelayedDeleveraging,
        Deleveraging,
    }

    public enum OkexOptionsTimeInForce
    {
        NormalOrder,
        //PostOnly,
        //FillOrKil,
        //ImmediateOrCancel,
        //Market
    }

    public enum OkexOptionsOrderState
    {
        Failed,
        Canceled,
        Open,
        PartiallyFilled,
        FullyFilled,
        Submitting,
        Canceling,
        PendingAmend,

        /// <summary>
        /// (Open + Partially Filled)
        /// </summary>
        Incomplete,

        /// <summary>
        /// (Canceled + Fully Filled)
        /// </summary>
        Complete,
    }

    public enum OkexOptionsBillType
    {
        /// <summary>
        /// Transfer in / out
        /// </summary>
        Transfer,

        /// <summary>
        /// Trade
        /// </summary>
        Match,

        /// <summary>
        /// Transaction fee
        /// </summary>
        Fee,

        /// <summary>
        /// Delivery / Settle
        /// </summary>
        Settlement,

        /// <summary>
        /// Forced close / Forced reduce
        /// </summary>
        Liquidation
    }

    public enum OkexOptionsRemittingAccountType
    {
        Spot,
        Futures,
        C2C,
        Margin,
        FundingAccount,
        PiggyBank,
        Swap,
        Options,
        MiningAccount,
        LoansAccount
    }

    public enum OkexOptionsReceivingAccountType
    {
        Spot,
        Futures,
        C2C,
        Margin,
        FundingAccount,
        PiggyBank,
        Swap,
        Options,
        MiningAccount,
        LoansAccount
    }


}