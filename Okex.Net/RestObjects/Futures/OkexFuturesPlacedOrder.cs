using CryptoExchange.Net.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Okex.Net.RestObjects
{
    public class OkexFuturesBatchPlacedOrder
    {
        [JsonProperty("result")]
        public bool Result { get; set; }

        [JsonProperty("order_info")]
        public IEnumerable<OkexFuturesPlacedOrder> Orders { get; set; } = new List<OkexFuturesPlacedOrder>();
    }

    public class OkexFuturesPlacedOrder
    {
        [JsonProperty("instrument_id"), JsonOptionalProperty]
        public string Symbol { get; set; } = "";

        /// <summary>
        /// The result of the request
        /// </summary>
        [JsonProperty("result"), JsonOptionalProperty]
        public bool Result { get; set; }

        /// <summary>
        /// Order ID. If order placement fails, this value will be -1
        /// </summary>
        [JsonProperty("order_id")]
        public long OrderId { get; set; }

        /// <summary>
        /// Client-supplied order ID
        /// </summary>
        [JsonProperty("client_oid")]
        public string ClientOrderId { get; set; } = "";

        /// <summary>
        /// If request_id is provided in the request, then the response will return the corresponding request_ID
        /// </summary>
        [JsonProperty("request_id"), JsonOptionalProperty]
        public string RequestId { get; set; } = "";

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
