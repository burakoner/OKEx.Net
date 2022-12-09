namespace Okex.Net.Converters;

internal class TransferTypeConverter : BaseConverter<OkexTransferType>
{
    public TransferTypeConverter() : this(true) { }
    public TransferTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkexTransferType, string>> Mapping => new List<KeyValuePair<OkexTransferType, string>>
    {
        new KeyValuePair<OkexTransferType, string>(OkexTransferType.TransferWithinAccount, "0"),
        new KeyValuePair<OkexTransferType, string>(OkexTransferType.MasterAccountToSubAccount, "1"),
        new KeyValuePair<OkexTransferType, string>(OkexTransferType.SubAccountToMasterAccount, "2"),
    };
}