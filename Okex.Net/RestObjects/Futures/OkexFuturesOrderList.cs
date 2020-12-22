using Newtonsoft.Json;
using Okex.Net.Converters;
using Okex.Net.Enums;
using System;
using System.Collections.Generic;

namespace Okex.Net.RestObjects
{
    public class OkexFuturesOrderList
    {
        [JsonProperty("result")]
        public bool Result { get; set; }

        [JsonProperty("order_info")]
        public IEnumerable<OkexFuturesOrder> Orders { get; set; } = new List<OkexFuturesOrder>();
    }

    public class OkexFuturesOrder
    {
        /// <summary>
        /// Contract ID, e.g. BTC-USD-180213,BTC-USDT-191227
        /// </summary>
        [JsonProperty("instrument_id")]
        public string Symbol { get; set; } = "";

        /// <summary>
        /// Order creation time
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
        public decimal Size { get; set; }

        /// <summary>
        /// Filled quantity
        /// </summary>
        [JsonProperty("filled_qty")]
        public decimal FilledQuantity { get; set; }

        /// <summary>
        /// Transaction Fees
        /// </summary>
        [JsonProperty("fee")]
        public decimal Fee { get; set; }

        /// <summary>
        /// Order price
        /// </summary>
        [JsonProperty("price")]
        public decimal Price { get; set; }

        /// <summary>
        /// Average filled price
        /// </summary>
        [JsonProperty("price_avg")]
        public decimal PriceAverage { get; set; }

        /// <summary>
        /// status is the older version of state and both can be used interchangeably in the short term. It is recommended to switch to state.
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; } = "";

        /// <summary>
        /// -2:Failed,-1:Canceled,0:Open ,1:Partially Filled, 2:Fully Filled,3:Submitting,4:Canceling
        /// </summary>
        [JsonProperty("state"), JsonConverter(typeof(FuturesOrderStateConverter))]
        public OkexFuturesOrderState State { get; set; }

        /// <summary>
        ///  Type (1: open long 2: open short 3: close long 4: close short)
        /// </summary>
        [JsonProperty("type"), JsonConverter(typeof(FuturesOrderTypeConverter))]
        public OkexFuturesOrderType Type { get; set; }

        /// <summary>
        /// 0: Normal limit order 1: Post only 2: Fill Or Kill 3: Immediatel Or Cancel 4：Market
        /// </summary>
        [JsonProperty("order_type"), JsonConverter(typeof(FuturesTimeInForceConverter))]
        public OkexFuturesTimeInForce TimeInForce { get; set; }

        /// <summary>
        /// Par value of the contract
        /// </summary>
        [JsonProperty("contract_val")]
        public decimal ContractValue { get; set; }

        /// <summary>
        /// Leverage , 1-100x
        /// </summary>
        [JsonProperty("leverage")]
        public decimal Leverage { get; set; }

        /// <summary>
        /// Closed position realized profit and loss
        /// </summary>
        [JsonProperty("pnl")]
        public decimal PNL { get; set; }
    }
}
