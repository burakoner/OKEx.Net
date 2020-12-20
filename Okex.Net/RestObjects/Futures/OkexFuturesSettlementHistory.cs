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
        [JsonProperty("type"), JsonConverter(typeof(OptionsDeliverySettlementConverter))]
        public OkexOptionsDeliverySettlement Type { get; set; }

        [JsonProperty("instrument_id")]
        public string InstrumentId { get; set; } = "";

        [JsonProperty("settlement_price")]
        public decimal SettlementPrice { get; set; }

        [JsonProperty("clawback_loss")]
        public decimal ClawbackLoss { get; set; }

        [JsonProperty("clawback_rate")]
        public decimal ClawbackRate { get; set; }

        [JsonProperty("reserve")]
        public decimal Reserve { get; set; }

        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}
