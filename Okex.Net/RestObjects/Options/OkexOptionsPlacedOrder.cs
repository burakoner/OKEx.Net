using CryptoExchange.Net.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Okex.Net.RestObjects
{
    public class OkexOptionsBatchOrders
    {
        [JsonProperty("result")]
        public bool Result { get; set; }

        [JsonProperty("order_info")]
        public IEnumerable<OkexOptionsPlacedOrder> Orders { get; set; } = new List<OkexOptionsPlacedOrder>();
    }
    public class OkexOptionsPlacedOrder
    {
        /// <summary>
        /// The result of the request (true/false)
        /// </summary>
        [JsonProperty("result")]
        public bool Result { get; set; }

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

        /// <summary>
        /// Client-supplied order ID
        /// </summary>
        [JsonProperty("client_oid")]
        public string ClientOrderId { get; set; } = "";

        /// <summary>
        /// Order ID. If order placement fails, this value will be -1
        /// </summary>
        [JsonProperty("order_id")]
        public long OrderId { get; set; }

        /// <summary>
        /// If request_id is provided in the request, then the response will return the corresponding request_ID
        /// </summary>
        [JsonProperty("request_id"), JsonOptionalProperty]
        public long RequestId { get; set; }
    }
}
