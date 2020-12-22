using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using Okex.Net.Converters;
using Okex.Net.Enums;
using System;
using System.Collections.Generic;

namespace Okex.Net.RestObjects
{
    public class OkexFuturesOrderBook
    {
        [JsonProperty("asks")]
        public IEnumerable<OkexFuturesOrderBookEntry> Asks { get; set; } = new List<OkexFuturesOrderBookEntry>();

        [JsonProperty("bids")]
        public IEnumerable<OkexFuturesOrderBookEntry> Bids { get; set; } = new List<OkexFuturesOrderBookEntry>();

        [JsonOptionalProperty, JsonConverter(typeof(OrderBookDataTypeConverter))]
        public OkexOrderBookDataType DataType { get; set; } = OkexOrderBookDataType.Api;

        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }

        [JsonProperty("instrument_id"), JsonOptionalProperty]
        public string Symbol { get; set; } = "";

        [JsonProperty("checksum"), JsonOptionalProperty]
        public long Checksum { get; set; }

    }

    [JsonConverter(typeof(ArrayConverter))]
    public class OkexFuturesOrderBookEntry
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
