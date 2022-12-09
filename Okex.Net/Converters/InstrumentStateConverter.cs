namespace Okex.Net.Converters;

internal class InstrumentStateConverter : BaseConverter<OkexInstrumentState>
{
    public InstrumentStateConverter() : this(true) { }
    public InstrumentStateConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkexInstrumentState, string>> Mapping => new List<KeyValuePair<OkexInstrumentState, string>>
    {
        new KeyValuePair<OkexInstrumentState, string>(OkexInstrumentState.Live, "live"),
        new KeyValuePair<OkexInstrumentState, string>(OkexInstrumentState.Suspend, "suspend"),
        new KeyValuePair<OkexInstrumentState, string>(OkexInstrumentState.PreOpen, "preopen"),
    };
}