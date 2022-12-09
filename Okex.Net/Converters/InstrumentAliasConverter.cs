namespace Okex.Net.Converters;

internal class InstrumentAliasConverter : BaseConverter<OkexInstrumentAlias>
{
    public InstrumentAliasConverter() : this(true) { }
    public InstrumentAliasConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkexInstrumentAlias, string>> Mapping => new List<KeyValuePair<OkexInstrumentAlias, string>>
    {
        new KeyValuePair<OkexInstrumentAlias, string>(OkexInstrumentAlias.ThisWeek, "this_week"),
        new KeyValuePair<OkexInstrumentAlias, string>(OkexInstrumentAlias.NextWeek, "next_week"),
        new KeyValuePair<OkexInstrumentAlias, string>(OkexInstrumentAlias.Quarter, "quarter"),
        new KeyValuePair<OkexInstrumentAlias, string>(OkexInstrumentAlias.NextQuarter, "next_quarter"),
    };
}