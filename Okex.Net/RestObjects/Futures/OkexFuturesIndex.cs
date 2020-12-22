using Newtonsoft.Json;
using System;

namespace Okex.Net.RestObjects
{
    public class OkexFuturesIndex
    {
        [JsonProperty("instrument_id")]
        public string Symbol { get; set; } = "";
        
        [JsonProperty("index")]
        public decimal Index { get; set; }
        
        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}
