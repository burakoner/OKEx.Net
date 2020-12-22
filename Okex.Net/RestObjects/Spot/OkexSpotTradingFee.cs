using Newtonsoft.Json;
using System;

namespace Okex.Net.RestObjects
{
    public class OkexSpotTradingFee
    {
        /// <summary>
        /// Taker Fee Rate
        /// </summary>
        [JsonProperty("taker")]
        public decimal Taker { get; set; }

        /// <summary>
        /// Maker Fee Rate
        /// </summary>
        [JsonProperty("maker")]
        public decimal Maker { get; set; }

        /// <summary>
        /// Data Return Time
        /// </summary>
        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }

    }
}
