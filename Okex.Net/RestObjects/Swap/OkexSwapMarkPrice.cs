using Newtonsoft.Json;
using System;

namespace Okex.Net.RestObjects
{
    public class OkexSwapMarkPrice
    {
        /// <summary>
        /// Contract ID, e.g. BTC-USD-SWAP,BTC-USDT-SWAP
        /// </summary>
        [JsonProperty("instrument_id")]
        public string Symbol { get; set; } = "";

        /// <summary>
        /// Mark Price
        /// </summary>
        [JsonProperty("mark_price")]
        public decimal MarkPrice { get; set; }

        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}
