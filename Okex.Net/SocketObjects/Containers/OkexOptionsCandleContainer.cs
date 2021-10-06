using Newtonsoft.Json;
using Okex.Net.RestObjects;
using System;

namespace Okex.Net.SocketObjects.Containers
{
    public class OkexOptionsCandleContainer
    {
        [JsonIgnore]
        public DateTime Timestamp { get; set; }

        [JsonProperty("instrument_id")]
        public string Symbol { get; set; }

        //[JsonProperty("candle")]
        //public OkexOptionsCandle Candle { get; set; } = default!;
    }
}
