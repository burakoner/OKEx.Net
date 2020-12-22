using Newtonsoft.Json;
using System;

namespace Okex.Net.RestObjects
{
    public class OkexFuturesHoldAmount
    {
        /// <summary>
        /// Contract ID, e.g. BTC-USD-190329,BTC-USDT-191227
        /// </summary>
        [JsonProperty("instrument_id")]
        public string Symbol { get; set; } = "";

        /// <summary>
        /// Hold amount
        /// </summary>
        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}
