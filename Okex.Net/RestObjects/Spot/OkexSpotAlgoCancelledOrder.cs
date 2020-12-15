using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Okex.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Okex.Net.Enums;

namespace Okex.Net.RestObjects
{
    public class OkexSpotAlgoCancelledOrder
    {
        /// <summary>
        /// Trading pair symbol
        /// </summary>
        [JsonProperty("instrument_id")]
        public string Symbol { get; set; } = "";

        /// <summary>
        /// 1. trigger order; 2. trail order; 3. iceberg order; 4. time-weighted average price; 5. stop order
        /// </summary>
        [JsonProperty("order_type"), JsonConverter(typeof(AlgoOrderTypeConverter))]
        public OkexAlgoOrderType Type { get; set; }

        /// <summary>
        /// Cancel specific order ID
        /// </summary>
        [JsonProperty("algo_ids")]
        public string AlgoIds { get; set; } = "";

        /// <summary>
        /// Parameter return result
        /// </summary>
        [JsonProperty("result")]
        public bool Result { get; set; }

        /// <summary>
        /// Error code; returns 0 when request is successful, otherwise corresponding error code will be returned
        /// </summary>
        [JsonProperty("error_code")]
        public string ErrorCode { get; set; } = "";

        /// <summary>
        /// Error message; blank if request is successful, otherwise corresponding error message will be returned
        /// </summary>
        [JsonProperty("error_message")]
        public string ErrorMessage { get; set; } = "";
    }
}
