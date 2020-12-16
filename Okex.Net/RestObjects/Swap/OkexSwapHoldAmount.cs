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
    public class OkexSwapHoldAmount
    {
        /// <summary>
        /// Contract ID, e.g. BTC-USD-SWAP,BTC-USDT-SWAP
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
