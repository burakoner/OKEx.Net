using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Okex.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Okex.Net.SocketObjects.Spot
{
    public class OkexGeneralPingPongContainer
    {
        public DateTime PingTime { get; set; }
        public DateTime PongTime { get; set; }
        public string PongMessage { get; set; } = "";
        public TimeSpan Latency { get; set; }
    }
}
