namespace Okex.Net.Converters;

internal class PositionModeConverter : BaseConverter<OkexPositionMode>
{
    public PositionModeConverter() : this(true) { }
    public PositionModeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkexPositionMode, string>> Mapping => new List<KeyValuePair<OkexPositionMode, string>>
    {
        new KeyValuePair<OkexPositionMode, string>(OkexPositionMode.LongShortMode, "long_short_mode"),
        new KeyValuePair<OkexPositionMode, string>(OkexPositionMode.NetMode, "net_mode"),
    };
}