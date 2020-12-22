using Newtonsoft.Json;
using Okex.Net.Converters;
using Okex.Net.Enums;
using System;

namespace Okex.Net.RestObjects
{
    public class OkexSwapTrade
    {
        /// <summary>
        /// Order fill time
        /// </summary>
        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Filled price
        /// </summary>
        [JsonProperty("price")]
        public decimal Price { get; set; }

        /// <summary>
        /// Filled size
        /// </summary>
        [JsonProperty("size")]
        public decimal Quantity { get; set; }

        /// <summary>
        /// Transaction ID
        /// </summary>
        [JsonProperty("trade_id")]
        public long TradeId { get; set; }

        /// <summary>
        /// Filled side
        /// </summary>
        [JsonProperty("side"), JsonConverter(typeof(SwapOrderSideConverter))]        
        public OkexSwapOrderSide Side { get; set; }
    }
}
