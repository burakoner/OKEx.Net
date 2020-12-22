using Newtonsoft.Json;

namespace Okex.Net.RestObjects
{
    public class OkexSpotPlacedOrder
    {
        /// <summary>
        /// Order ID
        /// </summary>
        [JsonProperty("order_id")]
        public long OrderId { get; set; }

        /// <summary>
        /// Client-supplied order ID
        /// </summary>
        [JsonProperty("client_oid")]
        public string ClientOrderId { get; set; } = "";

        /// <summary>
        /// Result of the order. Error message will be returned if the order failed.
        /// </summary>
        [JsonProperty("result")]
        public bool Result { get; set; }

        /// <summary>
        /// Error code; blank if order placed successfully
        /// </summary>
        [JsonProperty("error_code")]
        public string ErrorCode { get; set; } = "";

        /// <summary>
        /// Error message; blank if order placed successfully
        /// </summary>
        [JsonProperty("error_message")]
        public string ErrorMessage { get; set; } = "";

    }
}
