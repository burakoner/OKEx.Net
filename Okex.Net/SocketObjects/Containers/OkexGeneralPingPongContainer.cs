using System;

namespace Okex.Net.SocketObjects.Containers
{
    public class OkexGeneralPingPongContainer
    {
        public DateTime PingTime { get; set; }
        public DateTime PongTime { get; set; }
        public string PongMessage { get; set; }
        public TimeSpan Latency { get; set; }
    }
}
