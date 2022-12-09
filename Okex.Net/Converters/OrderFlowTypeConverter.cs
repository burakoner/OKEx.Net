namespace Okex.Net.Converters;

internal class OrderFlowTypeConverter : BaseConverter<OkexOrderFlowType>
{
    public OrderFlowTypeConverter() : this(true) { }
    public OrderFlowTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkexOrderFlowType, string>> Mapping => new List<KeyValuePair<OkexOrderFlowType, string>>
    {
        new KeyValuePair<OkexOrderFlowType, string>(OkexOrderFlowType.Taker, "T"),
        new KeyValuePair<OkexOrderFlowType, string>(OkexOrderFlowType.Maker, "M"),
    };
}