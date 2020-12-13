using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Okex.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Okex.Net.Enums;

namespace Okex.Net.RestObjects
{
    public class OkexSpotPlaceOrder
    {
        /// <summary>
        /// Trading pair symbol
        /// </summary>
        [JsonProperty("instrument_id")]
        public string Symbol { get; set; } = "";

        /// <summary>
        /// You can customize order IDs to identify your orders. The system supports alphabets + numbers(case-sensitive，e.g:A123、a123), or alphabets (case-sensitive，e.g:Abc、abc) only, between 1-32 characters.
        /// </summary>
        [JsonProperty("client_oid")]
        public string ClientOrderId { get; set; } = "";

        /// <summary>
        /// Supports types limit or market (default: limit). When placing market orders, order_type must be 0 (normal order)
        /// </summary>
        [JsonProperty("type"), JsonConverter(typeof(SpotOrderTypeConverter))]
        public OkexSpotOrderType Type { get; set; }

        /// <summary>
        /// Specify buy or sell
        /// </summary>
        [JsonProperty("side"), JsonConverter(typeof(SpotOrderSideConverter))]
        public OkexSpotOrderSide Side { get; set; }

        /// <summary>
        /// Specify 0: Normal order (Unfilled and 0 imply normal limit order) 1: Post only 2: Fill or Kill 3: Immediate Or Cancel
        /// </summary>
        [JsonProperty("order_type"), JsonConverter(typeof(SpotTimeInForceConverter))]
        public OkexSpotTimeInForce TimeInForce { get; set; }

        /// <summary>
        /// Price. Required for limit orders
        /// </summary>
        [JsonProperty("price",NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Price { get; set; }

        /// <summary>
        /// Quantity to be sold. Required for limit-buy, limit-sell and market sells
        /// </summary>
        [JsonProperty("size", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Size { get; set; }

        /// <summary>
        /// Amount to spend. Required for market buys
        /// </summary>
        [JsonProperty("notional", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Notional { get; set; }

        /// <summary>
        /// Flag for Margin Trading
        /// </summary>
        [JsonProperty("margin_trading", NullValueHandling = NullValueHandling.Ignore)]
        public string? MarginTrading { get; set; } = null;
    }
}
