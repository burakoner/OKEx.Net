using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Okex.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Okex.Net.Enums;
using Okex.Net.Attributes;

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
