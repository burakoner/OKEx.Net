using CryptoExchange.Net.Attributes;
using Newtonsoft.Json;
using Okex.Net.Converters;
using Okex.Net.Enums;
using System;
using System.Collections.Generic;

namespace Okex.Net.RestObjects
{
    public class OkexSwapOrderList
    {
        [JsonProperty("order_info")]
        public IEnumerable<OkexSwapOrder> Orders { get; set; } = new List<OkexSwapOrder>();
    }

    public class OkexSwapOrder
    {
        /// <summary>
        /// Contract ID, e.g. BTC-USD-SWAP,BTC-USDT-SWAP
        /// </summary>
        [JsonProperty("instrument_id")]
        public string Symbol { get; set; } = "";

        /// <summary>
        /// Creation time
        /// </summary>
        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Order ID
        /// </summary>
        [JsonProperty("order_id")]
        public long OrderId { get; set; }

        /// <summary>
        /// the order ID customised by yourself
        /// </summary>
        [JsonProperty("client_oid")]
        public string ClientOrderId { get; set; } = "";

        /// <summary>
        /// Quantity
        /// </summary>
        [JsonProperty("size")]
        public decimal Size { get; set; }

        /// <summary>
        /// Transaction fees
        /// </summary>
        [JsonProperty("fee")]
        public decimal Fee { get; set; }

        /// <summary>
        /// Filled quantity
        /// </summary>
        [JsonProperty("filled_qty")]
        public decimal FilledSize { get; set; }

        /// <summary>
        /// Order price
        /// </summary>
        [JsonProperty("price")]
        public decimal Price { get; set; }

        /// <summary>
        /// Average price
        /// </summary>
        [JsonProperty("price_avg")]
        public decimal PriceAverage { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; } = "";

        /// <summary>
        /// -2:Failed,-1:Canceled,0:Open ,1:Partially Filled, 2:Fully Filled,3:Submitting,4:Canceling
        /// </summary>
        [JsonProperty("state"), JsonConverter(typeof(SwapOrderStateConverter))]
        public OkexSwapOrderState State { get; set; }

        /// <summary>
        /// Type (1: open long 2: open short 3: close long 4: close short)
        /// </summary>
        [JsonProperty("type"), JsonConverter(typeof(SwapOrderTypeConverter))]
        public OkexSwapOrderType Type { get; set; }

        /// <summary>
        /// 0: Normal limit order 1: Post only 2: Fill Or Kill 3: Immediatel Or Cancel 4：Market
        /// </summary>
        [JsonProperty("order_type"), JsonConverter(typeof(SwapTimeInForceConverter))]
        public OkexSwapTimeInForce TimeInForce { get; set; }

        /// <summary>
        /// Contract value
        /// </summary>
        [JsonProperty("contract_val")]
        public decimal ContractValue { get; set; }

        /// <summary>
        /// Leverage , 1-100x
        /// </summary>
        [JsonProperty("leverage")]
        public decimal Leverage { get; set; }

        /// <summary>
        /// Only forced-liquidation orders will return actual forced-liquidation trigger price
        /// </summary>
        [JsonProperty("trigger_price"), JsonOptionalProperty]
        public decimal TriggerPrice { get; set; }

    }
}
