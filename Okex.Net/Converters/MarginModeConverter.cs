namespace Okex.Net.Converters;

internal class MarginModeConverter : BaseConverter<OkexMarginMode>
{
    public MarginModeConverter() : this(true) { }
    public MarginModeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkexMarginMode, string>> Mapping => new List<KeyValuePair<OkexMarginMode, string>>
    {
        new KeyValuePair<OkexMarginMode, string>(OkexMarginMode.Isolated, "isolated"),
        new KeyValuePair<OkexMarginMode, string>(OkexMarginMode.Cross, "cross"),
    };
}