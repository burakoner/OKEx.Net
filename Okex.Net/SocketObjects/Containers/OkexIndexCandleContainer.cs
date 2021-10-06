using Newtonsoft.Json;
using Okex.Net.RestObjects;
using System;

namespace Okex.Net.SocketObjects.Containers
{
    public class OkexIndexCandleContainer
    {
        [JsonIgnore]
        public DateTime Timestamp { get; set; }

        [JsonProperty("instrument_id")]
        public string Symbol { get; set; }

        //[JsonProperty("candle")]
        //public OkexIndexCandle Candle { get; set; } = default!;
    }
}
