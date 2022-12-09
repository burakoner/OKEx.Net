namespace Okex.Net.Converters;

internal class SubAccountTypeConverter : BaseConverter<OkexSubAccountType>
{
    public SubAccountTypeConverter() : this(true) { }
    public SubAccountTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkexSubAccountType, string>> Mapping => new List<KeyValuePair<OkexSubAccountType, string>>
    {
        new KeyValuePair<OkexSubAccountType, string>(OkexSubAccountType.Standard, "1"),
        new KeyValuePair<OkexSubAccountType, string>(OkexSubAccountType.Custody, "2"),
    };
}