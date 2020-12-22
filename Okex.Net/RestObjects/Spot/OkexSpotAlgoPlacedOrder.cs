using Newtonsoft.Json;

namespace Okex.Net.RestObjects
{
    public class OkexSpotAlgoPlacedOrder
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
