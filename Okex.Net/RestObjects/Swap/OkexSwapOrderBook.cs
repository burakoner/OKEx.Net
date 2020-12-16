using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Okex.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Okex.Net.Enums;

namespace Okex.Net.RestObjects
{
    public class OkexSwapOrderBook
    {
        [JsonProperty("asks")]
        public IEnumerable<OkexSwapOrderBookEntry> Asks { get; set; } = new List<OkexSwapOrderBookEntry>();

        [JsonProperty("bids")]
        public IEnumerable<OkexSwapOrderBookEntry> Bids { get; set; } = new List<OkexSwapOrderBookEntry>();

        [JsonProperty("time")]
        public DateTime Timestamp { get; set; }
    }

    [JsonConverter(typeof(ArrayConverter))]
    public class OkexSwapOrderBookEntry
    {
        /// <summary>
        /// The price for this entry
        /// </summary>
        [ArrayProperty(0)]
        public decimal Price { get; set; }

        /// <summary>
        /// The contract size at the price
        /// </summary>
        [ArrayProperty(1)]
        public decimal Quantity { get; set; }

        /// <summary>
        /// The number of the liquidated orders at the price
        /// </summary>
        [ArrayProperty(2)]
        public decimal LiquidatedOrdersCount { get; set; }

        /// <summary>
        /// The number of orders at the price
        /// </summary>
        [ArrayProperty(3)]
        public decimal OrdersCount { get; set; }
    }
}
