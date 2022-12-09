namespace Okex.Net.Converters;

internal class SubAccountTransferTypeConverter : BaseConverter<OkexSubAccountTransferType>
{
    public SubAccountTransferTypeConverter() : this(true) { }
    public SubAccountTransferTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkexSubAccountTransferType, string>> Mapping => new List<KeyValuePair<OkexSubAccountTransferType, string>>
    {
        new KeyValuePair<OkexSubAccountTransferType, string>(OkexSubAccountTransferType.FromMasterAccountToSubAccout, "0s"),
        new KeyValuePair<OkexSubAccountTransferType, string>(OkexSubAccountTransferType.FromSubAccountToMasterAccout, "1"),
    };
}