namespace Okex.Net.Converters;

internal class ConvertUnitConverter : BaseConverter<OkexConvertUnit>
{
    public ConvertUnitConverter() : this(true) { }
    public ConvertUnitConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkexConvertUnit, string>> Mapping => new List<KeyValuePair<OkexConvertUnit, string>>
    {
        new KeyValuePair<OkexConvertUnit, string>(OkexConvertUnit.Coin, "coin"),
        new KeyValuePair<OkexConvertUnit, string>(OkexConvertUnit.Usdt, "usdt"),
    };
}