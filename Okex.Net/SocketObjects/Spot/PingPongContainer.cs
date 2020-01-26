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
        [JsonIgnore]
        public DateTime Timestamp { get; set; }

        [JsonIgnore]
        public string Message { get; set; } = "";
    }
}
