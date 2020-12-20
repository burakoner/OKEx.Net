using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Okex.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Okex.Net.RestObjects
{
    public class OkexOptionsTicker
    {
        [JsonProperty("error_code")]
        public string ErrorCode { get; set; } = "";

        [JsonProperty("error_message")]
        public string ErrorMessage { get; set; } = "";

        /// <summary>
        /// Instrument ID, e.g. BTC-USD-190830-9000-C
        /// </summary>
        [JsonProperty("instrument_id")]
        public string Symbol { get; set; } = "";

        /// <summary>
        /// Best bid price
        /// </summary>
        [JsonProperty("best_bid")]
        public decimal? BestBidPrice { get; set; }

        /// <summary>
        /// Best ask price
        /// </summary>
        [JsonProperty("best_ask")]
        public decimal? BestAskPrice { get; set; }

        /// <summary>
        /// Last traded price
        /// </summary>
        [JsonProperty("last")]
        public decimal? LastPrice { get; set; }

        /// <summary>
        /// Size of best bid price
        /// </summary>
        [JsonProperty("best_bid_size")]
        public decimal? BestBidSize { get; set; }

        /// <summary>
        /// Size of best ask price
        /// </summary>
        [JsonProperty("best_ask_size")]
        public decimal? BestAskSize { get; set; }

        /// <summary>
        /// Last traded quantity
        /// </summary>
        [JsonProperty("last_qty")]
        public decimal? LastQuantity { get; set; }

        /// <summary>
        /// Open Interest
        /// </summary>
        [JsonProperty("open_interest")]
        public decimal OpenInterest { get; set; }

        /// <summary>
        /// Opening price in the past 24 hours
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
        /// Trading volume in the last 24 hours (in lots)
        /// </summary>
        [JsonProperty("volume_24h")]
        public decimal Volume24H { get; set; }

        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}
