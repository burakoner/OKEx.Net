using Newtonsoft.Json;

namespace Okex.Net.RestObjects
{
    public class OkexOptionsResponse
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
        public string error_code { get; set; } = "";

        /// <summary>
        /// Error message will be returned if order placement fails, otherwise it will be blank
        /// </summary>
        [JsonProperty("error_message")]
        public string error_message { get; set; } = "";
    }
}
