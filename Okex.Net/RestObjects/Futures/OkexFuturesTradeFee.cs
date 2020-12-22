using Newtonsoft.Json;
using System;

namespace Okex.Net.RestObjects
{
    public class OkexFuturesTradeFee
    {
        /// <summary>
        /// Fee Schedule Tie 1：Tier 1，2：Tier 2 ；
        /// </summary>
        [JsonProperty("category")]
        public int Category { get; set; }

        /// <summary>
        /// Delivery fee rate
        /// </summary>
        [JsonProperty("delivery")]
        public decimal Delivery { get; set; }

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
