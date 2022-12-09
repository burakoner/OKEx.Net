namespace Okex.Net.Converters;

internal class GreeksTypeConverter : BaseConverter<OkexGreeksType>
{
    public GreeksTypeConverter() : this(true) { }
    public GreeksTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkexGreeksType, string>> Mapping => new List<KeyValuePair<OkexGreeksType, string>>
    {
        new KeyValuePair<OkexGreeksType, string>(OkexGreeksType.GreeksInCoins, "PA"),
        new KeyValuePair<OkexGreeksType, string>(OkexGreeksType.BlackScholesGreeksInDollars, "BS"),
    };
}