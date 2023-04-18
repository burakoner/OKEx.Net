namespace Okex.Net.Converters;

internal class QuickMarginTypeConverter : BaseConverter<OkexQuickMarginType>
{
    public QuickMarginTypeConverter() : this(true) { }
    public QuickMarginTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkexQuickMarginType, string>> Mapping => new()
    {
        new KeyValuePair<OkexQuickMarginType, string>(OkexQuickMarginType.Manual, "manual"),
        new KeyValuePair<OkexQuickMarginType, string>(OkexQuickMarginType.AutoBorrow, "auto_borrow"),
        new KeyValuePair<OkexQuickMarginType, string>(OkexQuickMarginType.AutoRepay, "auto_repay"),
    };
}