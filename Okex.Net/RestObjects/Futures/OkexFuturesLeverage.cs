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
    [JsonConverter(typeof(TypedDataConverter<OkexFuturesLeverage>))]
    public class OkexFuturesLeverage
    {
        /// <summary>
        /// Account Type
        /// </summary>
        [JsonProperty("margin_mode"), JsonConverter(typeof(FuturesMarginModeConverter))]
        public OkexFuturesMarginMode MarginMode { get; set; }

        /// <summary>
        /// Underlying index e.g:BTC-USD，BTC-USDT
        /// !!! CROSS-MARGIN ONLY !!!
        /// </summary>
        [JsonProperty("underlying"), JsonOptionalProperty]
        public string Underlying { get; set; } = "";

        /// <summary>
        /// Leverage ratio, 1-100x
        /// !!! CROSS-MARGIN ONLY !!!
        /// </summary>
        [JsonProperty("leverage"), JsonOptionalProperty]
        public decimal Leverage { get; set; }

        /// <summary>
        /// Contract ID, e.g.BTC-USD-180213,BTC-USDT-180213
        /// !!! FIXED-MARGIN ONLY !!!
        /// </summary>
        [JsonProperty("instrument_id"), JsonOptionalProperty]
        public string InstrumentId { get; set; } = "";

        /// <summary>
        /// !!! FIXED-MARGIN ONLY !!!
        /// </summary>
        [TypedData]
        [JsonOptionalProperty]
        public Dictionary<string, OkexFuturesFixedLeverage> FixedLeverages { get; set; } = new Dictionary<string, OkexFuturesFixedLeverage>();

    }

    /// <summary>
    /// !!! FIXED-MARGIN ONLY !!!
    /// </summary>
    public class OkexFuturesFixedLeverage
    {
        /// <summary>
        /// Leverage ratio for long positions, 1-100x
        /// </summary>
        [JsonProperty("long_leverage"), JsonOptionalProperty]
        public decimal LongLeverage { get; set; }

        /// <summary>
        /// Leverage ratio for short positions, 1-100x
        /// </summary>
        [JsonProperty("short_leverage"), JsonOptionalProperty]
        public decimal ShortLeverage { get; set; }
    }

}