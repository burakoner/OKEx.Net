using Newtonsoft.Json;
using System;

namespace Okex.Net.RestObjects
{
    public class OkexFuturesEstimatedPrice
    {
        /// <summary>
        /// Contract ID, e.g. BTC-USD-180213,BTC-USDT-191227
        /// </summary>
        [JsonProperty("instrument_id")]
        public string Symbol { get; set; } = "";

        /// <summary>
        /// Estimated delivery price
        /// </summary>
        [JsonProperty("settlement_price")]
        public decimal Rate { get; set; }

        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}
