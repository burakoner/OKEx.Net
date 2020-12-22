using Newtonsoft.Json;

namespace Okex.Net.RestObjects
{
    public class OkexMarginLeverageResponse
    {
        /// <summary>
        /// Result of request, true or false
        /// </summary>
        [JsonProperty("result")]
        public bool Result { get; set; }

        /// <summary>
        /// Currency pair name, such as: BTC-USDT
        /// </summary>
        [JsonProperty("instrument_id")]
        public string Symbol { get; set; } = "";

        /// <summary>
        /// Leverage ratio, 2-10x
        /// </summary>
        [JsonProperty("leverage")]
        public int Leverage { get; set; }

        /// <summary>
        /// error code
        /// </summary>
        [JsonProperty("error_code")]
        public string ErrorCode { get; set; } = "";

        /// <summary>
        /// error message
        /// </summary>
        [JsonProperty("error_message")]
        public string ErrorMessage { get; set; } = "";

    }
}
