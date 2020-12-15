namespace Okex.Net.Enums
{
    public enum OkexFuturesMarginMode
    {
        Crossed,
        Fixed
    }

    public enum OkexFuturesDirection
    {
        Long,
        Short
    }

    public enum OkexFuturesBillType
    {
        OpenLong,
        OpenShort,
        CloseLong,
        CloseShort,
        TransactionFee,
        TransferIn,
        TransferOut,
        SettledRPL,
        FullLiquidationOfLong,
        FullLiquidationOfShort,
        DeliveryLong,
        DeliveryShort,
        SettledUPLLong,
        SettledUPLShort,
        PartialLiquidationOfShort,
        PartialLiquidationOfLong
    }

    public enum OkexFuturesRemittingAccountType
    {
        Spot,
        Futures,
        C2C,
        Margin,
        FundingAccount,
        PiggyBank,
        Swap,
        Futures12,
        MiningAccount,
        LoansAccount
    }

    public enum OkexFuturesReceivingAccountType
    {
        Spot,
        Futures,
        C2C,
        Margin,
        FundingAccount,
        PiggyBank,
        Swap,
        Option,
        MiningAccount,
        LoansAccount
    }

    public enum OkexFuturesOrderSide
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

    public enum OkexFuturesOrderType
    {
        OpenLong,
        OpenShort,
        CloseLong,
        CloseShort,
    }

    public enum OkexFuturesTimeInForce
    {
        NormalOrder,
        PostOnly,
        FillOrKil,
        ImmediateOrCancel,
        Market
    }

    public enum OkexFuturesOrderState
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

    public enum OkexFuturesMarginAction
    {
        Increase,
        Decrease,
    }

    public enum OkexFuturesAutoMargin
    {
        On,
        Off,
    }
    
    public enum OkexFuturesLiquidationStatus
    {
        UnfilledInTheRecent7Days,
        FilledOrdersInTheRecent7Days,
    }

    public enum OkexFuturesDeliverySettlement
    {
        Delivery,
        Settlement,
    }

}