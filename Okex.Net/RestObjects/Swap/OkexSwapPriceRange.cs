﻿using Newtonsoft.Json;
using System;

namespace Okex.Net.RestObjects
{
    public class OkexSwapPriceRange
    {
        /// <summary>
        /// Contract ID, e.g. BTC-USD-SWAP,BTC-USDT-SWAP
        /// </summary>
        [JsonProperty("instrument_id")]
        public string Symbol { get; set; } = "";

        /// <summary>
        /// Ceiling of buying price
        /// </summary>
        [JsonProperty("highest")]
        public decimal Highest { get; set; }

        /// <summary>
        /// Floor of selling price
        /// </summary>
        [JsonProperty("lowest")]
        public decimal Lowest { get; set; }

        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}
