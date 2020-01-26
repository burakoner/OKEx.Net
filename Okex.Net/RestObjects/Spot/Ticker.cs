using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Okex.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Okex.Net.RestObjects.Spot
{
    public class Ticker
    {
        /// <summary>
        /// Trading pair symbol
        /// </summary>
        [JsonProperty("instrument_id")]
        public string Symbol { get; set; } = "";

        //[JsonProperty("product_id")]
        //public string ProductId { get; set; } = "";

        /// <summary>
        /// Last traded price
        /// </summary>
        [JsonProperty("last")]
        public decimal LastPrice { get; set; }

        /// <summary>
        /// Last traded price amount
        /// </summary>
        [JsonProperty("last_qty")]
        public decimal LastQuantity { get; set; }

        //[JsonProperty("bid")]
        //public decimal Bid { get; set; }

        /// <summary>
        /// Best bid price
        /// </summary>
        [JsonProperty("best_bid")]
        public decimal BestBidPrice { get; set; }

        /// <summary>
        /// Best bid price amount
        /// </summary>
        [JsonProperty("best_bid_size")]
        public decimal BestBidSize { get; set; }

        //[JsonProperty("ask")]
        //public decimal Ask { get; set; }

        /// <summary>
        /// Best ask price
        /// </summary>
        [JsonProperty("best_ask")]
        public decimal BestAskPrice { get; set; }

        /// <summary>
        /// Best bid price amount
        /// </summary>
        [JsonProperty("best_ask_size")]
        public decimal BestAskSize { get; set; }

        /// <summary>
        /// Opening price 24 hours ago
        /// </summary>
        [JsonProperty("open_24h")]
        public decimal Open24H { get; set; }

        /// <summary>
        /// Highest price in the past 24 hours
        /// </summary>
        [JsonProperty("high_24h")]
        public decimal High24H { get; set; }

        /// <summary>
        /// Lowest price in the past 24 hours
        /// </summary>
        [JsonProperty("low_24h")]
        public decimal Low24H { get; set; }

        /// <summary>
        /// Trading volume of past 24 hours in the base currency
        /// </summary>
        [JsonProperty("base_volume_24h")]
        public decimal BaseVolume24H { get; set; }

        /// <summary>
        /// Trading volume of past 24 hours in the quote currency
        /// </summary>
        [JsonProperty("quote_volume_24h")]
        public decimal QuoteVolume24H { get; set; }

        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}
