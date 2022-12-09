namespace Okex.Net.Converters;

internal class TradeModeConverter : BaseConverter<OkexTradeMode>
{
    public TradeModeConverter() : this(true) { }
    public TradeModeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkexTradeMode, string>> Mapping => new List<KeyValuePair<OkexTradeMode, string>>
    {
        new KeyValuePair<OkexTradeMode, string>(OkexTradeMode.Cash, "cash"),
        new KeyValuePair<OkexTradeMode, string>(OkexTradeMode.Cross, "cross"),
        new KeyValuePair<OkexTradeMode, string>(OkexTradeMode.Isolated, "isolated"),
    };
}