using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Interfaces;
using Newtonsoft.Json;
using Okex.Net.Converters;
using System;
using System.Collections.Generic;

namespace Okex.Net.RestObjects.Market
{
    public class OkexOrderBook
    {
        [JsonProperty("asks")]
        public IEnumerable<OkexOrderBookRow> Asks { get; set; } = new List<OkexOrderBookRow>();

        [JsonProperty("bids")]
        public IEnumerable<OkexOrderBookRow> Bids { get; set; } = new List<OkexOrderBookRow>();

        [JsonProperty("ts"), JsonConverter(typeof(OkexTimestampConverter))]
        public DateTime Time { get; set; }

        [JsonProperty("action")]
        public string Action { get; set; }

        [JsonProperty("checksum")]
        public long? Checksum { get; set; }
    }

    [JsonConverter(typeof(ArrayConverter))]
    public class OkexOrderBookRow : ISymbolOrderBookEntry
    {
        /// <summary>
        /// The price for this row
        /// </summary>
        [ArrayProperty(0)]
        public decimal Price { get; set; }

        /// <summary>
        /// The quantity for this row
        /// </summary>
        [ArrayProperty(1)]
        public decimal Quantity { get; set; }

        /// <summary>
        /// The number of liquidated orders at the price
        /// </summary>
        [ArrayProperty(2)]
        public decimal LiquidatedOrders { get; set; }

        /// <summary>
        /// The number of orders at the price
        /// </summary>
        [ArrayProperty(3)]
        public decimal OrdersCount { get; set; }
    }
}
