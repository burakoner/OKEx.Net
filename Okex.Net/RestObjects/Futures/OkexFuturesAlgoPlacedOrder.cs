using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Okex.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Okex.Net.Enums;

namespace Okex.Net.RestObjects
{
    public class OkexFuturesAlgoPlacedOrder
    {
        /// <summary>
        /// Parameters return result
        /// </summary>
        [JsonProperty("result")]
        public bool Result { get; set; }

        /// <summary>
        /// Order ID: when fail to place order, the value is -1
        /// </summary>
        [JsonProperty("algo_id")]
        public long AlgoId { get; set; }

        /// <summary>
        /// Contract ID, e.g. EOS-USD-191227
        /// </summary>
        [JsonProperty("instrument_id")]
        public string Symbol { get; set; } = "";

        /// <summary>
        /// 1. trigger order; 2. trail order; 3. iceberg order; 4. time-weighted average price (TWAP); 5. stop order
        /// </summary>
        [JsonProperty("order_type"), JsonConverter(typeof(AlgoOrderTypeConverter))]        
        public OkexAlgoOrderType AlgoOrderType { get; set; }

        /// <summary>
        /// Error code for order placement. Success = 0
        /// </summary>
        [JsonProperty("error_code")]
        public string ErrorCode { get; set; } = "";

        /// <summary>
        /// Error message will be returned if order placement fails, otherwise it will be blank
        /// </summary>
        [JsonProperty("error_message")]
        public string ErrorMessage { get; set; } = "";
    }
}
