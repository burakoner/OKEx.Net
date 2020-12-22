using Newtonsoft.Json;
using Okex.Net.Converters;
using Okex.Net.Enums;

namespace Okex.Net.RestObjects
{
    public class OkexFuturesAutoMarginResponse
    {
        /// <summary>
        /// The result of the request
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
        /// Underlying index，eg：BTC-USD BTC-USDT
        /// </summary>
        [JsonProperty("underlying")]
        public string Symbol { get; set; } = "";

        /// <summary>
        /// On / off automatically increases margin: 1, on 2, off
        /// </summary>
        [JsonProperty("type"), JsonConverter(typeof(FuturesAutoMarginConverter))]
        public OkexFuturesAutoMargin AutoMarginStatus { get; set; }
    }
}
