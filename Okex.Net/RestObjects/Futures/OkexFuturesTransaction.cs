using Newtonsoft.Json;
using Okex.Net.Converters;
using Okex.Net.Enums;
using System;

namespace Okex.Net.RestObjects
{
    public class OkexFuturesTransaction
    {
        /// <summary>
        /// Trading pair symbol
        /// </summary>
        [JsonProperty("instrument_id")]
        public string Symbol { get; set; } = "";

        /// <summary>
        /// Trade ID
        /// </summary>
        [JsonProperty("trade_id")]
        public long TradeId { get; set; }

        /// <summary>
        /// Price
        /// </summary>
        [JsonProperty("price")]
        public decimal Price { get; set; }

        /// <summary>
        /// Size of order
        /// </summary>
        [JsonProperty("order_qty")]
        public decimal Size { get; set; }

        /// <summary>
        /// Order ID
        /// </summary>
        [JsonProperty("order_id")]
        public long OrderId { get; set; }

        /// <summary>
        /// Time of order transaction
        /// </summary>
        [JsonProperty("created_at")]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Client-supplied order ID
        /// </summary>
        [JsonProperty("client_oid")]
        public string ClientOrderId { get; set; } = "";

        /// <summary>
        /// Taker or maker (T or M)
        /// </summary>
        [JsonProperty("exec_type"), JsonConverter(typeof(TraderRoleConverter))]
        public OkexTraderRole TraderRole { get; set; }

        /// <summary>
        /// Transaction fee
        /// </summary>
        [JsonProperty("fee")]
        public decimal Fee { get; set; }

        /// <summary>
        /// 1. open long; 2. open short; 3. close long; 4. close short
        /// </summary>
        [JsonProperty("type"), JsonConverter(typeof(FuturesOrderTypeConverter))]
        public OkexFuturesOrderType Type { get; set; }

        /// <summary>
        /// The side that pays the trading fees, such as buy, sell
        /// </summary>
        [JsonProperty("side"), JsonConverter(typeof(FuturesOrderSideConverter))]
        public OkexFuturesOrderSide Side { get; set; }
    }
}
