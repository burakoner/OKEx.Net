using Newtonsoft.Json;
using System;

namespace Okex.Net.RestObjects
{
    public class OkexSwapInterest
    {
        /// <summary>
        /// Contract ID, e.g. BTC-USD-180213,BTC-USDT-191227
        /// </summary>
        [JsonProperty("instrument_id")]
        public string Symbol { get; set; } = "";

        /// <summary>
        /// Total open interest
        /// </summary>
        [JsonProperty("amount")]
        public decimal Interest { get; set; }

        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}
