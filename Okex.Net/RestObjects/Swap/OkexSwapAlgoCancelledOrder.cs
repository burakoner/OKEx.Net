using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Okex.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Okex.Net.Enums;

namespace Okex.Net.RestObjects
{
    public class OkexSwapAlgoCancelledOrder
    {
        [JsonProperty("error_code")]
        public string ErrorCode { get; set; } = "";

        [JsonProperty("error_message")]
        public string ErrorMessage { get; set; } = "";

        [JsonProperty("detailMsg")]
        public string DetailMessage { get; set; } = "";

        [JsonProperty("data")]
        public OkexSwapAlgoCancelledOrderData Data { get; set; } = new OkexSwapAlgoCancelledOrderData();
    }

    public class OkexSwapAlgoCancelledOrderData
    {
        [JsonProperty("result")]
        public bool Result { get; set; }

        [JsonProperty("instrument_id")]
        public string Symbol { get; set; } = "";

        [JsonProperty("algo_ids")]
        public string AlgoIds { get; set; } = "";

        [JsonProperty("order_type"), JsonConverter(typeof(AlgoOrderTypeConverter))]
        public OkexAlgoOrderType Type { get; set; }
    }
}
