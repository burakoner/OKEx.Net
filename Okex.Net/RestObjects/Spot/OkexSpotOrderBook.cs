using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using Okex.Net.Converters;
using Okex.Net.Enums;
using System;
using System.Collections.Generic;

namespace Okex.Net.RestObjects
{
    public class OkexSpotOrderBook
    {
        [JsonProperty("instrument_id"), JsonOptionalProperty]
        public string Symbol { get; set; } = "";

        [JsonProperty("checksum"), JsonOptionalProperty]
        public long Checksum { get; set; }

        [JsonOptionalProperty, JsonConverter(typeof(OrderBookDataTypeConverter))]
        public OkexOrderBookDataType DataType { get; set; } = OkexOrderBookDataType.Api;

        /// <summary>
        /// Selling side
        /// </summary>
        [JsonProperty("asks")]
        public IEnumerable<OkexSpotOrderBookEntry> Asks { get; set; } = new List<OkexSpotOrderBookEntry>();

        /// <summary>
        /// Buying side
        /// </summary>
        [JsonProperty("bids")]
        public IEnumerable<OkexSpotOrderBookEntry> Bids { get; set; } = new List<OkexSpotOrderBookEntry>();

        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }
    }

    [JsonConverter(typeof(ArrayConverter))]
    public class OkexSpotOrderBookEntry
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

        /// <summary>
        /// Number of orders at the price
        /// </summary>
        [ArrayProperty(2)]
        public decimal OrdersCount { get; set; }
    }
}
