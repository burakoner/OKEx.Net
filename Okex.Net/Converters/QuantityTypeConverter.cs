namespace Okex.Net.Converters;

internal class QuantityTypeConverter : BaseConverter<OkexQuantityType>
{
    public QuantityTypeConverter() : this(true) { }
    public QuantityTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkexQuantityType, string>> Mapping => new List<KeyValuePair<OkexQuantityType, string>>
    {
        new KeyValuePair<OkexQuantityType, string>(OkexQuantityType.BaseCurrency, "base_ccy"),
        new KeyValuePair<OkexQuantityType, string>(OkexQuantityType.QuoteCurrency, "quote_ccy"),
    };
}