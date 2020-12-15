namespace Okex.Net.Enums
{
    public enum OkexTraderRole
    {
        Taker,
        Maker
    }

    public enum OkexMarket
    {
        Spot,
        Margin,
    }

    public enum OkexAlgoOrderType
    {
        /// <summary>
        /// Trigger Order
        /// </summary>
        TriggerOrder,

        /// <summary>
        /// Trail Order
        /// </summary>
        TrailOrder,

        /// <summary>
        /// Iceberg Order
        /// </summary>
        IcebergOrder,

        /// <summary>
        /// Time-Weighted Average Price
        /// </summary>
        TWAP,

        /// <summary>
        /// Take Profit and Stop Loss
        /// </summary>
        StopOrder
    }

    public enum OkexAlgoPriceType
    {
        /// <summary>
        /// Limit Price
        /// </summary>
        Limit,

        /// <summary>
        /// Market Price
        /// </summary>
        Market,
    }

    public enum OkexAlgoStatus
    {
        Pending,
        Effective,
        Cancelled,

        /// <summary>
        /// only applies to iceberg and TWAP orders
        /// </summary>
        PartiallyEffective,

        /// <summary>
        /// only applies to iceberg and TWAP orders
        /// </summary>
        Paused,

        OrderFailed,
    }

}