namespace Okex.Net.Converters;

internal class ConvertTypeConverter : BaseConverter<OkexConvertType>
{
    public ConvertTypeConverter() : this(true) { }
    public ConvertTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkexConvertType, string>> Mapping => new List<KeyValuePair<OkexConvertType, string>>
    {
        new KeyValuePair<OkexConvertType, string>(OkexConvertType.CurrencyToContract, "1"),
        new KeyValuePair<OkexConvertType, string>(OkexConvertType.ContractToCurrency, "2"),
    };
}