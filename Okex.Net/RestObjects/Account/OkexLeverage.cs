using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using Okex.Net.Converters;
using Okex.Net.Enums;
using System;
using System.Collections.Generic;

namespace Okex.Net.RestObjects.Account
{
    public class OkexLeverage
    {
        [JsonProperty("instId")]
        public string Instrument { get; set; }

        [JsonProperty("mgnMode"), JsonConverter(typeof(MarginModeConverter))]
        public OkexMarginMode MarginMode { get; set; }

        [JsonProperty("posSide"), JsonConverter(typeof(PositionSideConverter))]
        public OkexPositionSide PositionSide { get; set; }

        [JsonProperty("lever")]
        public decimal? Leverage { get; set; }
    }
}
