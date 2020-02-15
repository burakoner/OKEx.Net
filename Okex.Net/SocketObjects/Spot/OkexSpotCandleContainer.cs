using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Okex.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Okex.Net.RestObjects;

namespace Okex.Net.SocketObjects.Spot
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
