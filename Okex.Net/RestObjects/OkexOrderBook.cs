using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Interfaces;
using Newtonsoft.Json;

namespace Okex.Net.RestObjects
{
    /// <summary>
    /// Order book
    /// </summary>
    public class OkexOrderBook
    {
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonIgnore]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// List of bids
        /// </summary>
        public IEnumerable<OkexOrderBookEntry> Bids { get; set; } = new List<OkexOrderBookEntry>();

        /// <summary>
        /// List of asks
        /// </summary>
        public IEnumerable<OkexOrderBookEntry> Asks { get; set; } = new List<OkexOrderBookEntry>();
    }

    [JsonConverter(typeof(ArrayConverter))]
    public class OkexOrderBookEntry: ISymbolOrderBookEntry
    {
        /// <summary>
        /// The price for this entry
        /// </summary>
        [ArrayProperty(0)]
        public decimal Price { get; set; }

        /// <summary>
        /// The quantity for this entry
        /// </summary>
        [ArrayProperty(1)]
        public decimal Quantity { get; set; }

        /*
        /// <summary>
        /// I dont know What this is. This may be "Order Count" or "Version"
        /// </summary>
        [ArrayProperty(2)]
        public decimal DummyInteger { get; set; }
        */
    }
}
