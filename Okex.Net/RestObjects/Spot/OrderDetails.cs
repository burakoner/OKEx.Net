using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Okex.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Okex.Net.RestObjects.Spot
{
    public class OrderDetails
    {
        /// <summary>
        /// Trading pair symbol
        /// </summary>
        [JsonProperty("instrument_id")]
        public string Symbol { get; set; } = "";

        //[JsonProperty("product_id")]
        //public string ProductId { get; set; } = "";

        /// <summary>
        /// Order ID
        /// </summary>
        [JsonProperty("order_id")]
        public long OrderId { get; set; }

        /// <summary>
        /// Client-supplied order ID
        /// </summary>
        [JsonProperty("client_oid")]
        public string ClientOrderId { get; set; } = "";

        /// <summary>
        /// Time of order creation
        /// </summary>
        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Time of order creation
        /// </summary>
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Price of order
        /// </summary>
        [JsonProperty("price")]
        public decimal? Price { get; set; }

        /// <summary>
        /// Size of order in unit of quote currency
        /// </summary>
        [JsonProperty("size")]
        public decimal? Size { get; set; }

        /// <summary>
        /// Allocated amount to buy (for market orders)
        /// </summary>
        [JsonProperty("notional")]
        public decimal? Notional { get; set; }

        /// <summary>
        /// Order type: limit or market (default: limit)
        /// </summary>
        [JsonProperty("type"), JsonConverter(typeof(SpotOrderTypeConverter))]
        public SpotOrderType Type { get; set; }

        /// <summary>
        /// Buy or sell
        /// </summary>
        [JsonProperty("side"), JsonConverter(typeof(SpotOrderSideConverter))]
        public SpotOrderSide Side { get; set; }

        /// <summary>
        /// Quantity filled
        /// </summary>
        [JsonProperty("filled_size")]
        public decimal FilledSize { get; set; }

        /// <summary>
        /// Amount filled
        /// </summary>
        [JsonProperty("filled_notional")]
        public decimal FilledNotional { get; set; }

        /// <summary>
        /// Specify 0: Normal order (Unfilled and 0 imply normal limit order) 1: Post only 2: Fill or Kill 3: Immediate Or Cancel
        /// </summary>
        [JsonProperty("order_type"), JsonConverter(typeof(SpotTimeInForceConverter))]
        public SpotTimeInForce TimeInForce { get; set; }

        /// <summary>
        /// Order Status: -2 = Failed -1 = Canceled 0 = Open 1 = Partially Filled 2 = Fully Filled 3 = Submitting 4 = Canceling
        /// </summary>
        [JsonProperty("state"), JsonConverter(typeof(SpotOrderStateConverter))]
        public SpotOrderState State { get; set; }

        /// <summary>
        /// status is the older version of state and both can be used interchangeably in the short term. It is recommended to switch to state.
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; } = "";

        /// <summary>
        /// Average filled price
        /// </summary>
        [JsonProperty("price_avg")]
        public decimal? PriceAverage { get; set; }

        [JsonProperty("funds")]
        public decimal? Funds { get; set; }
    }
}
