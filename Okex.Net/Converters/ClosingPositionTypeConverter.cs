namespace Okex.Net.Converters;

internal class ClosingPositionTypeConverter : BaseConverter<OkexClosingPositionType>
{
    public ClosingPositionTypeConverter() : this(true) { }
    public ClosingPositionTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkexClosingPositionType, string>> Mapping => new List<KeyValuePair<OkexClosingPositionType, string>>
        {
            new KeyValuePair<OkexClosingPositionType, string>(OkexClosingPositionType.ClosePartially, "1"),
            new KeyValuePair<OkexClosingPositionType, string>(OkexClosingPositionType.CloseAll, "2"),
            new KeyValuePair<OkexClosingPositionType, string>(OkexClosingPositionType.Liquidation, "3"),
            new KeyValuePair<OkexClosingPositionType, string>(OkexClosingPositionType.PartialLiquidation, "4"),
            new KeyValuePair<OkexClosingPositionType, string>(OkexClosingPositionType.ADL, "5"),
        };
}