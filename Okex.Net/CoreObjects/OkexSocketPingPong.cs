using System;

namespace Okex.Net.CoreObjects
{
    public class OkexSocketPingPong
    {
        public DateTime PingTime { get; set; }
        public DateTime PongTime { get; set; }
        public string PongMessage { get; set; }
        public TimeSpan Latency { get; set; }
    }
}
