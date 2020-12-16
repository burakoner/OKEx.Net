namespace Okex.Net.Enums
{
    public enum OkexSwapOrderSide
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

    public enum OkexSwapDirection
    {
        /// <summary>
        /// Long
        /// </summary>
        Long,

        /// <summary>
        /// Short
        /// </summary>
        Short
    }

    public enum OkexSwapOrderType
    {
        OpenLong,
        OpenShort,
        CloseLong,
        CloseShort,
    }
    public enum OkexSwapLiquidationStatus
    {
        UnfilledInTheRecent7Days,
        FilledOrdersInTheRecent7Days,
    }
    public enum OkexSwapMarginMode
    {
        Crossed,
        Fixed
    }
    public enum OkexSwapLeverageSide
    {
        FixedMarginForLongPosition,
        FixedMarginForShortPosition,
        CrossedMargin,
    }

    public enum OkexSwapBillType
    {
        OpenLong,
        OpenShort,
        CloseLong,
        CloseShort,
        TransferIn,
        TransferOut,
        SettledUPL,
        Clawback,
        InsuranceFund,
        FullLiquidationOfLong,
        FullLiquidationOfShort,
        FundingFee,
        ManuallyAddMargin,
        ManuallyReduceMargin,
        AutoMargin,
        SwitchMarginMode,
        PartialLiquidationOfLong,
        PartialLiquidationOfShort,
        MarginAddedWithLoweredLeverage,
        SettledRP
    }
    public enum OkexSwapTimeInForce
    {
        NormalOrder,
        PostOnly,
        FillOrKil,
        ImmediateOrCancel,
        Market
    }

    public enum OkexSwapOrderState
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