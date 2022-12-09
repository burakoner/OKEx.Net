namespace Okex.Net.Converters;

internal class OrderTypeConverter : BaseConverter<OkexOrderType>
{
    public OrderTypeConverter() : this(true) { }
    public OrderTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkexOrderType, string>> Mapping => new List<KeyValuePair<OkexOrderType, string>>
    {
        new KeyValuePair<OkexOrderType, string>(OkexOrderType.MarketOrder, "market"),
        new KeyValuePair<OkexOrderType, string>(OkexOrderType.LimitOrder, "limit"),
        new KeyValuePair<OkexOrderType, string>(OkexOrderType.PostOnly, "post_only"),
        new KeyValuePair<OkexOrderType, string>(OkexOrderType.FillOrKill, "fok"),
        new KeyValuePair<OkexOrderType, string>(OkexOrderType.ImmediateOrCancel, "ioc"),
        new KeyValuePair<OkexOrderType, string>(OkexOrderType.OptimalLimitOrder, "optimal_limit_ioc"),
    };
}