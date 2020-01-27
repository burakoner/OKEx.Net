using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Okex.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Okex.Net.RestObjects.Spot;

namespace Okex.Net.SocketObjects.Spot
{
    public class PingPongContainer
    {
        public DateTime PingTime { get; set; }
        public DateTime PongTime { get; set; }
        public string PongMessage { get; set; } = "";
        public TimeSpan Latency { get; set; }
    }
}
