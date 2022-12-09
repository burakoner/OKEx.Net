namespace Okex.Net.Converters;

internal class InstrumentTypeConverter : BaseConverter<OkexInstrumentType>
{
    public InstrumentTypeConverter() : this(true) { }
    public InstrumentTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkexInstrumentType, string>> Mapping => new List<KeyValuePair<OkexInstrumentType, string>>
    {
        new KeyValuePair<OkexInstrumentType, string>(OkexInstrumentType.Any, "ANY"),
        new KeyValuePair<OkexInstrumentType, string>(OkexInstrumentType.Spot, "SPOT"),
        new KeyValuePair<OkexInstrumentType, string>(OkexInstrumentType.Margin, "MARGIN"),
        new KeyValuePair<OkexInstrumentType, string>(OkexInstrumentType.Swap, "SWAP"),
        new KeyValuePair<OkexInstrumentType, string>(OkexInstrumentType.Futures, "FUTURES"),
        new KeyValuePair<OkexInstrumentType, string>(OkexInstrumentType.Option, "OPTION"),
        new KeyValuePair<OkexInstrumentType, string>(OkexInstrumentType.Contracts, "CONTRACTS"),
    };
}