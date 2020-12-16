using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Okex.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Okex.Net.Enums;

namespace Okex.Net.RestObjects
{
    public class OkexSwapIndex
    {
        [JsonProperty("instrument_id")]
        public string Symbol { get; set; } = "";
        
        [JsonProperty("index")]
        public decimal Index { get; set; }
        
        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}
