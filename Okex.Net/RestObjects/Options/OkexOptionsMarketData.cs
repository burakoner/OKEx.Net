using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Okex.Net.Converters;
using Newtonsoft.Json;
using System;
using Okex.Net.Enums;

namespace Okex.Net.RestObjects
{
    public class OkexOptionsMarketData
    {
        /// <summary>
        /// Instrument ID, e.g. BTC-USD-190830-9000-C
        /// </summary>
        [JsonProperty("instrument_id")]
        public string Instrument { get; set; } = "";

        /// <summary>
        /// System timestamp, in ISO 8061 format
        /// </summary>
        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Best bid price
        /// </summary>
        [JsonProperty("best_bid")]
        public decimal? BestBid { get; set; }

        /// <summary>
        /// Best ask price
        /// </summary>
        [JsonProperty("best_ask")]
        public decimal? BestAsk { get; set; }

        /// <summary>
        /// Last traded price
        /// </summary>
        [JsonProperty("last")]
        public decimal Last { get; set; }

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
        /// Trading volume of past 24 hours, in contracts size
        /// </summary>
        [JsonProperty("volume_24h")]
        public decimal Volume24H { get; set; }

        /// <summary>
        /// Open interest
        /// </summary>
        [JsonProperty("open_interest")]
        public decimal OpenInterest { get; set; }

        /// <summary>
        /// Mark price
        /// </summary>
        [JsonProperty("mark_price")]
        public decimal MarkPrice { get; set; }

        /// <summary>
        /// Highest buy price
        /// </summary>
        [JsonProperty("highest_buy")]
        public decimal HighestBuy { get; set; }

        /// <summary>
        /// Lowest sell price
        /// </summary>
        [JsonProperty("lowest_sell")]
        public decimal LowestSell { get; set; }

        /// <summary>
        /// delta
        /// </summary>
        [JsonProperty("delta")]
        public decimal Delta { get; set; }

        /// <summary>
        /// gamma
        /// </summary>
        [JsonProperty("gamma")]
        public decimal Gamma { get; set; }

        /// <summary>
        /// vega
        /// </summary>
        [JsonProperty("vega")]
        public decimal Vega { get; set; }

        /// <summary>
        /// theta
        /// </summary>
        [JsonProperty("theta")]
        public decimal Theta { get; set; }

        /// <summary>
        /// Leverage ratio
        /// </summary>
        [JsonProperty("leverage")]
        public decimal Leverage { get; set; }

        /// <summary>
        /// Markup volatility
        /// </summary>
        [JsonProperty("mark_vol")]
        public decimal MarkVolume { get; set; }

        /// <summary>
        /// Bid volatility
        /// </summary>
        [JsonProperty("bid_vol")]
        public decimal? BidVolume { get; set; }

        /// <summary>
        /// Ask volatility
        /// </summary>
        [JsonProperty("ask_vol")]
        public decimal? AskVolume { get; set; }

        /// <summary>
        /// Realized volatility （Not currently used）
        /// </summary>
        [JsonProperty("realized_vol")]
        public decimal RealizedVolume { get; set; }

        /// <summary>
        /// Estimated price at delivery
        /// </summary>
        [JsonProperty("estimated_price")]
        public decimal EstimatedPrice { get; set; } 
    }
}
