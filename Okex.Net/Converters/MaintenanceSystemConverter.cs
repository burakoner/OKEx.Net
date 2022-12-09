namespace Okex.Net.Converters;

internal class MaintenanceSystemConverter : BaseConverter<OkexMaintenanceSystem>
{
    public MaintenanceSystemConverter() : this(true) { }
    public MaintenanceSystemConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkexMaintenanceSystem, string>> Mapping => new List<KeyValuePair<OkexMaintenanceSystem, string>>
    {
        new KeyValuePair<OkexMaintenanceSystem, string>(OkexMaintenanceSystem.Classic, "classic"),
        new KeyValuePair<OkexMaintenanceSystem, string>(OkexMaintenanceSystem.Unified, "unified"),
    };
}