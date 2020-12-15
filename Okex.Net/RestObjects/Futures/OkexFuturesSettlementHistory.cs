using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Okex.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Okex.Net.Enums;

namespace Okex.Net.RestObjects
{
    public class OkexFuturesSettlementHistory
    {
        [JsonProperty("type"), JsonConverter(typeof(FuturesDeliverySettlementConverter))]
        public OkexFuturesDeliverySettlement Type { get; set; }

        [JsonProperty("instrument_id")]
        public string instrument_id { get; set; } = "";

        [JsonProperty("settlement_price")]
        public decimal settlement_price { get; set; }

        [JsonProperty("clawback_loss")]
        public decimal clawback_loss { get; set; }

        [JsonProperty("clawback_rate")]
        public decimal clawback_rate { get; set; }

        [JsonProperty("reserve")]
        public decimal reserve { get; set; }

        [JsonProperty("timestamp")]
        public DateTime timestamp { get; set; }
    }
}
