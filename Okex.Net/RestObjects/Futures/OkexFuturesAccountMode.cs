using CryptoExchange.Net.Attributes;
using Newtonsoft.Json;
using Okex.Net.Converters;
using Okex.Net.Enums;

namespace Okex.Net.RestObjects
{
    public class OkexFuturesAccountMode
    {
        /// <summary>
        /// Result of the request
        /// </summary>
        [JsonProperty("result")]
        public bool result { get; set; }

        /// <summary>
        /// Underlying index，eg：BTC-USD BTC-USDT
        /// </summary>
        [JsonProperty("underlying"), JsonOptionalProperty]
        public string Underlying { get; set; } = "";

        /// <summary>
        /// Margin mode: crossed / fixed
        /// </summary>
        [JsonProperty("margin_mode"), JsonConverter(typeof(FuturesMarginModeConverter))]
        public OkexFuturesMarginMode MarginMode { get; set; }
    }
}
