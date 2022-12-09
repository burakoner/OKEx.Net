namespace Okex.Net.Converters;

internal class MarginTransferModeConverter : BaseConverter<OkexMarginTransferMode>
{
    public MarginTransferModeConverter() : this(true) { }
    public MarginTransferModeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkexMarginTransferMode, string>> Mapping => new List<KeyValuePair<OkexMarginTransferMode, string>>
    {
        new KeyValuePair<OkexMarginTransferMode, string>(OkexMarginTransferMode.AutoTransfer, "automatic"),
        new KeyValuePair<OkexMarginTransferMode, string>(OkexMarginTransferMode.ManualTransfer, "autonomy"),
    };
}