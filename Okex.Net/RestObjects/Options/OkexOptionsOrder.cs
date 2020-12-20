using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Okex.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Okex.Net.Enums;

namespace Okex.Net.RestObjects
{
    public class OkexOptionsOrderList
    {
        [JsonProperty("result")]
        public bool Result { get; set; }

        [JsonProperty("order_info")]
        public IEnumerable<OkexOptionsOrder> Orders { get; set; } = new List<OkexOptionsOrder>();
    }

    public class OkexOptionsOrder
    {
        /// <summary>
        /// Instrument ID, e.g. BTC-USD-190927-12500-C
        /// </summary>
        [JsonProperty("instrument_id")]
        public string Instrument { get; set; } = "";

        /// <summary>
        /// Time when the order is last updated; ISO 8601 format
        /// </summary>
        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }

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
        /// Quantity
        /// </summary>
        [JsonProperty("size")]
        public decimal? Size { get; set; }

        /// <summary>
        /// Order price
        /// </summary>
        [JsonProperty("price")]
        public decimal? Price { get; set; }

        /// <summary>
        /// Filled quantity
        /// </summary>
        [JsonProperty("filled_qty")]
        public decimal FilledQuantity { get; set; }

        /// <summary>
        /// Transaction Fee
        /// </summary>
        [JsonProperty("fee")]
        public decimal Fee { get; set; }

        /// <summary>
        /// Average filled price. If none is filled, returns 0
        /// </summary>
        [JsonProperty("price_avg")]
        public decimal PriceAverage { get; set; }

        /// <summary>
        /// Contract value
        /// </summary>
        [JsonProperty("contract_val")]
        public decimal ContractValue { get; set; }

        /// <summary>
        /// Last filled price. If non is filled, returns 0
        /// </summary>
        [JsonProperty("last_fill_px")]
        public decimal last_fill_px { get; set; }

        /// <summary>
        /// Last filled volume. If non is filled, returns 0
        /// </summary>
        [JsonProperty("last_fill_qty")]
        public decimal last_fill_qty { get; set; }

        /// <summary>
        /// Buy or sell
        /// </summary>
        [JsonProperty("side"), JsonConverter(typeof(OptionsOrderSideConverter))]
        public OkexOptionsOrderSide Side { get; set; }

        /// <summary>
        /// Order state (-2:Failed, -1:Canceled,0:Open ,1:Partially Filled, 2:Fully Filled, 3:Submitting, 4:Canceling, 5:Pending Amend）
        /// </summary>
        [JsonProperty("state"), JsonConverter(typeof(OptionsOrderStateConverter))]
        public OkexOptionsOrderState State { get; set; }

        /// <summary>
        /// Differentiate the attributes of user orders
        /// 1. Buy,2. Sell,11. Liquidation Sell,12. Liquidation Buy,13. Partial Liquidation Sell,14. Partial Liquidation Buy
        /// </summary>
        [JsonProperty("type"), JsonConverter(typeof(OptionsOrderTypeConverter))]
        public OkexOptionsOrderType Type { get; set; }

        /// <summary>
        /// 0: Normal limit order
        /// </summary>
        [JsonProperty("order_type"), JsonConverter(typeof(OptionsTimeInForceConverter))]
        public OkexOptionsTimeInForce TimeInForce { get; set; }

    }
}
