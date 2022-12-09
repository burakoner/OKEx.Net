namespace Okex.Net.Converters;

internal class PriceVarianceConverter : BaseConverter<OkexPriceVariance>
{
    public PriceVarianceConverter() : this(true) { }
    public PriceVarianceConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkexPriceVariance, string>> Mapping => new List<KeyValuePair<OkexPriceVariance, string>>
    {
        new KeyValuePair<OkexPriceVariance, string>(OkexPriceVariance.Spread, "pxSpread"),
        new KeyValuePair<OkexPriceVariance, string>(OkexPriceVariance.Variance, "pxVar"),
    };
}