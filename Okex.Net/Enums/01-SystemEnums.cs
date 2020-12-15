namespace Okex.Net.Enums
{
    public enum OkexSystemMaintenanceStatus
    {
        Waiting,
        Processing,
        Completed
    }
    public enum OkexSystemMaintenanceProduct
    {
        WebSocket,
        SpotMargin,
        Futures,
        Perpetual,
        Options,
    }
}