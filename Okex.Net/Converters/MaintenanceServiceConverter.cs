namespace Okex.Net.Converters;

internal class MaintenanceServiceConverter : BaseConverter<OkexMaintenanceService>
{
    public MaintenanceServiceConverter() : this(true) { }
    public MaintenanceServiceConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkexMaintenanceService, string>> Mapping => new List<KeyValuePair<OkexMaintenanceService, string>>
    {
        new KeyValuePair<OkexMaintenanceService, string>(OkexMaintenanceService.WebSocket, "0"),
        new KeyValuePair<OkexMaintenanceService, string>(OkexMaintenanceService.SpotMargin, "1"),
        new KeyValuePair<OkexMaintenanceService, string>(OkexMaintenanceService.Futures, "2"),
        new KeyValuePair<OkexMaintenanceService, string>(OkexMaintenanceService.Perpetual, "3"),
        new KeyValuePair<OkexMaintenanceService, string>(OkexMaintenanceService.Options, "4"),
        new KeyValuePair<OkexMaintenanceService, string>(OkexMaintenanceService.Trading, "5"),
    };
}