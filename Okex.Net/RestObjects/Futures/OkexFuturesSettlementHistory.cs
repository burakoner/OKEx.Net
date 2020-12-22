using Newtonsoft.Json;
using Okex.Net.Converters;
using Okex.Net.Enums;
using System;

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
