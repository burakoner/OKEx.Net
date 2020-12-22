using Newtonsoft.Json;
using Okex.Net.Converters;
using Okex.Net.Enums;

namespace Okex.Net.RestObjects
{
    public class OkexFuturesMarginActionResponse
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
        /// Contract ID, e.g. BTC-USD-180309,BTC-USDT-191227
        /// </summary>
        [JsonProperty("instrument_id")]
        public string Symbol { get; set; } = "";

        /// <summary>
        /// opening side (long or short)
        /// </summary>
        [JsonProperty("direction"), JsonConverter(typeof(FuturesDirectionConverter))]
        public OkexFuturesDirection Direction { get; set; }

        /// <summary>
        /// 1：increase 2：decrease
        /// </summary>
        [JsonProperty("type"), JsonConverter(typeof(FuturesMarginActionConverter))]
        public OkexFuturesMarginAction MarginAction { get; set; }

        /// <summary>
        /// Amount to be increase or decrease
        /// </summary>
        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        /// <summary>
        /// Liquidation price after increase or decrease
        /// </summary>
        [JsonProperty("liquidation_price")]
        public string LiquidationPrice { get; set; } = "";
    }
}
