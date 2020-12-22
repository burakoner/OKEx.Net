using Newtonsoft.Json;
using Okex.Net.Converters;
using Okex.Net.Enums;
using System;

namespace Okex.Net.RestObjects
{
    public class OkexSwapTransaction
    {
        /// <summary>
        /// Contract ID, e.g. BTC-USD-SWAP
        /// </summary>
        [JsonProperty("instrument_id")]
        public string Symbol { get; set; } = "";

        /// <summary>
        /// ID of the bill record
        /// </summary>
        [JsonProperty("trade_id")]
        public long TradeId { get; set; }

        /// <summary>
        /// Order ID
        /// </summary>
        [JsonProperty("order_id")]
        public long OrderId { get; set; }

        [JsonProperty("fill_id")]
        public long FillId { get; set; }

        /// <summary>
        /// Price
        /// </summary>
        [JsonProperty("price")]
        public decimal Price { get; set; }

        /// <summary>
        /// Quantity
        /// </summary>
        [JsonProperty("order_qty")]
        public decimal Size { get; set; }

        /// <summary>
        /// Transaction Fee
        /// </summary>
        [JsonProperty("fee")]
        public decimal Fee { get; set; }

        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonProperty("created_at")]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Taker or Maker (T or M)
        /// </summary>
        [JsonProperty("exec_type"), JsonConverter(typeof(TraderRoleConverter))]
        public OkexTraderRole TraderRole { get; set; }

        /// <summary>
        /// Side of the order (buy or sell)
        /// </summary>
        [JsonProperty("order_side"), JsonConverter(typeof(SwapOrderSideConverter))]
        public OkexSwapOrderSide Side { get; set; }

        /// <summary>
        /// Side of the position (long or short )
        /// </summary>
        [JsonProperty("side"), JsonConverter(typeof(SwapDirectionConverter))]
        public OkexSwapDirection Direction { get; set; }

        /// <summary>
        /// 1. open long; 2. open short; 3. close long; 4. close short
        /// </summary>
        [JsonProperty("type"), JsonConverter(typeof(SwapOrderTypeConverter))]
        public OkexSwapOrderType Type { get; set; }

    }
}
