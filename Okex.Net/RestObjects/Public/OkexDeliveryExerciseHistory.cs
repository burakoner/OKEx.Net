using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using Okex.Net.Converters;
using Okex.Net.Enums;
using System;
using System.Collections.Generic;

namespace Okex.Net.RestObjects.Public
{
    public class OkexDeliveryExerciseHistory
    {
        [JsonProperty("ts"), JsonConverter(typeof(OkexTimestampConverter))]
        public DateTime Time { get; set; }

        [JsonProperty("details")]
        public IEnumerable<OkexPublicDeliveryExerciseHistoryDetail> Details { get; set; }
    }

    public class OkexPublicDeliveryExerciseHistoryDetail
    {
        [JsonProperty("type"), JsonConverter(typeof(DeliveryExerciseHistoryTypeConverter))]
        public OkexDeliveryExerciseHistoryType Type { get; set; }

        [JsonProperty("insId")]
        public string Instrument { get; set; }

        [JsonProperty("px")]
        public decimal Price { get; set; }
    }
}
