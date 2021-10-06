using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using Okex.Net.Converters;
using Okex.Net.Enums;
using System;

namespace Okex.Net.RestObjects.Trade
{
    public class OkexClosePositionResponse
    {
        [JsonProperty("instId")]
        public string Instrument { get; set; }
        
        [JsonProperty("posSide"), JsonConverter(typeof(PositionSideConverter))]
        public OkexPositionSide PositionSide { get; set; }
    }
}
