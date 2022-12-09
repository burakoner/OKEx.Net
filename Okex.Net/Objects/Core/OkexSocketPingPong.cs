namespace Okex.Net.Objects.Core;

public class OkexSocketPingPong
{
    public DateTime PingTime { get; set; }
    public DateTime PongTime { get; set; }
    public string PongMessage { get; set; }
    public TimeSpan Latency { get; set; }
}
