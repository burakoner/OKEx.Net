using Newtonsoft.Json;
using System;
using Okex.Net.RestObjects;

namespace Okex.Net.SocketObjects.Containers
{
    public class OkexSpotCandleContainer
    {
        [JsonIgnore]
        public DateTime Timestamp { get; set; }

        [JsonProperty("instrument_id")]
        public string Symbol { get; set; } = "";

        [JsonProperty("candle")]
        public OkexSpotCandle Candle { get; set; } = default!;
    }
}
