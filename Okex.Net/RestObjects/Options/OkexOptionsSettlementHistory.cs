using Newtonsoft.Json;
using Okex.Net.Converters;
using Okex.Net.Enums;
using System;
using System.Collections.Generic;

namespace Okex.Net.RestObjects
{
    public class OkexOptionsSettlementHistory
    {

        [JsonProperty("clawback_loss")]
        public decimal ClawbackLoss { get; set; }

        [JsonProperty("clawback_rate")]
        public decimal ClawbackRate { get; set; }

        [JsonProperty("reserve")]
        public decimal Reserve { get; set; }

        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }

        [JsonProperty("info")]
        public IEnumerable<OkexOptionsSettlementHistoryInfo> Data { get; set; } = new List<OkexOptionsSettlementHistoryInfo>();
    }

    public class OkexOptionsSettlementHistoryInfo
    {

        [JsonProperty("instrument_id")]
        public string Instrument { get; set; } = "";

        [JsonProperty("underlying_fixing")]
        public decimal? UnderlyingPrice { get; set; }

        [JsonProperty("settlement_price")]
        public decimal? SettlementPrice { get; set; }

        [JsonProperty("type"), JsonConverter(typeof(OptionsSettlementTypeConverter))]
        public OkexOptionsSettlementType Type { get; set; }
    }
}
