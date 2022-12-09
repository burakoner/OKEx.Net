namespace Okex.Net.Converters;

internal class MaintenanceStateConverter : BaseConverter<OkexMaintenanceState>
{
    public MaintenanceStateConverter() : this(true) { }
    public MaintenanceStateConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkexMaintenanceState, string>> Mapping => new List<KeyValuePair<OkexMaintenanceState, string>>
    {
        new KeyValuePair<OkexMaintenanceState, string>(OkexMaintenanceState.Scheduled, "scheduled"),
        new KeyValuePair<OkexMaintenanceState, string>(OkexMaintenanceState.Ongoing, "ongoing"),
        new KeyValuePair<OkexMaintenanceState, string>(OkexMaintenanceState.Completed, "completed"),
        new KeyValuePair<OkexMaintenanceState, string>(OkexMaintenanceState.Canceled, "canceled"),
    };
}