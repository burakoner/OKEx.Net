namespace Okex.Net.Enums
{
    public enum OkexMarginBillType
    {
        TokensBorrowed,
        TokensRepaid,
        InterestAccrued,
        Buy,
        Sell,
        FromFunding,
        FromC2C,
        FromSpot,
        ToFunding,
        ToC2C,
        ToSpot,
        AutoInterestPayment,
        LiquidationFees,
        RepayCandy,
        ToMargin,
        FromMargin,
    }

    public enum OkexMarginLoanStatus
    {
        Outstanding,
        Repaid,
    }

    public enum OkexMarginLoanState
    {
        Failed,
        Cancelled,
        Open,
        PartiallyFilled,
        FullyFilled,
        Submitting,
        Cancelling,

        /// <summary>
        /// (open+partially filled
        /// </summary>
        Incomplete,

        /// <summary>
        /// (cancelled+fully filled）
        /// </summary>
        Complete,
    }

}