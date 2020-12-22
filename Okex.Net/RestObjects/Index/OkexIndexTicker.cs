using Newtonsoft.Json;
using System;

namespace Okex.Net.RestObjects
{
    public class OkexIndexTicker
    {
        [JsonProperty("instrument_id")]
        public string Symbol { get; set; } = "";

        [JsonProperty("last")]
        public decimal Last { get; set; }

        [JsonProperty("high_24h")]
        public decimal High24H { get; set; }

        [JsonProperty("low_24h")]
        public decimal Low24H { get; set; }

        [JsonProperty("open_24h")]
        public decimal Open24H { get; set; }

        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}
