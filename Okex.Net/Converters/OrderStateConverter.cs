namespace Okex.Net.Converters;

internal class OrderStateConverter : BaseConverter<OkexOrderState>
{
    public OrderStateConverter() : this(true) { }
    public OrderStateConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkexOrderState, string>> Mapping => new List<KeyValuePair<OkexOrderState, string>>
    {
        new KeyValuePair<OkexOrderState, string>(OkexOrderState.Live, "live"),
        new KeyValuePair<OkexOrderState, string>(OkexOrderState.Canceled, "canceled"),
        new KeyValuePair<OkexOrderState, string>(OkexOrderState.PartiallyFilled, "partially_filled"),
        new KeyValuePair<OkexOrderState, string>(OkexOrderState.Filled, "filled"),
    };
}