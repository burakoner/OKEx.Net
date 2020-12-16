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
    public class OkexSwapTradeFee
    {
        /// <summary>
        /// Fee Schedule Tie 1：Tier 1，2：Tier 2 ；
        /// </summary>
        [JsonProperty("category")]
        public int Category { get; set; }

        /// <summary>
        /// maker fee
        /// </summary>
        [JsonProperty("maker")]
        public decimal Maker { get; set; }

        /// <summary>
        /// taker fee
        /// </summary>
        [JsonProperty("taker")]
        public decimal Taker { get; set; }

        /// <summary>
        /// Data return time
        /// </summary>
        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}
