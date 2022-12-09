namespace Okex.Net.Converters;

internal class MarginAddReduceConverter : BaseConverter<OkexMarginAddReduce>
{
    public MarginAddReduceConverter() : this(true) { }
    public MarginAddReduceConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkexMarginAddReduce, string>> Mapping => new List<KeyValuePair<OkexMarginAddReduce, string>>
    {
        new KeyValuePair<OkexMarginAddReduce, string>(OkexMarginAddReduce.Add, "add"),
        new KeyValuePair<OkexMarginAddReduce, string>(OkexMarginAddReduce.Reduce, "reduce"),
    };
}