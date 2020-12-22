using Newtonsoft.Json;
using Okex.Net.Converters;

namespace Okex.Net.RestObjects
{
    public class OkexFuturesModifyOrder
    {
        /// <summary>
        /// When the order amendment fails, whether to cancell the order automatically: 0: Don't cancel the order automatically 1: Automatically cancel the order. The default value is 0.
        /// </summary>
        [JsonProperty("cancel_on_fail", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(BooleanConverter))]
        public bool? CancelOnFail { get; set; }

        /// <summary>
        /// Either client_oid or order_id must be present. Order ID。
        /// </summary>
        [JsonProperty("order_id", NullValueHandling = NullValueHandling.Ignore)]
        public long? OrderId { get; set; }

        /// <summary>
        /// Either client_oid or order_id must be present. client_oid should be the same Client-supplied order ID when submitting the order. It should be comprised of alpha-numeric characters with length 1 to 32. Both uppercase and lowercase are supported.
        /// </summary>
        [JsonProperty("client_oid", NullValueHandling = NullValueHandling.Ignore)]
        public string? ClientOrderId { get; set; }

        /// <summary>
        /// You can provide the request_id. If provided, the response will include the corresponding request_id to help you identify the request. It should be comprised of alpha-numeric characters with length 1 to 32. Both uppercase and lowercase are supported.
        /// </summary>
        [JsonProperty("request_id", NullValueHandling = NullValueHandling.Ignore)]
        public string? RequestId { get; set; }

        /// <summary>
        /// Must provide at least one of new_size or new_price. When modifying a partially filled order, the new_size should include the amount that has been filled.
        /// </summary>
        [JsonProperty("new_size", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? NewSize { get; set; }

        /// <summary>
        /// Must provide at least one of new_size or new_price. Modifies the price.
        /// </summary>
        [JsonProperty("new_price", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? NewPrice { get; set; }
    }
}