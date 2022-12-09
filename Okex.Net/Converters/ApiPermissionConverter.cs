namespace Okex.Net.Converters;

internal class ApiPermissionConverter : BaseConverter<OkexApiPermission>
{
    public ApiPermissionConverter() : this(true) { }
    public ApiPermissionConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkexApiPermission, string>> Mapping => new List<KeyValuePair<OkexApiPermission, string>>
    {
        new KeyValuePair<OkexApiPermission, string>(OkexApiPermission.ReadOnly, "read_only"),
        new KeyValuePair<OkexApiPermission, string>(OkexApiPermission.Trade, "trade"),
    };
}