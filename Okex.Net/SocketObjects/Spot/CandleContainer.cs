using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Okex.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Okex.Net.RestObjects.Spot;

namespace Okex.Net.SocketObjects.Spot
{
    public class CandleContainer
    {
        [JsonIgnore]
        public DateTime Timestamp { get; set; }

        [JsonProperty("instrument_id")]
        public string Symbol { get; set; } = "";

        [JsonProperty("candle")]
        public Candle Candle { get; set; } = default!;
    }
}
