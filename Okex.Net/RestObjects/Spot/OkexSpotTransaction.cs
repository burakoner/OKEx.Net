using Newtonsoft.Json;
using Okex.Net.Converters;
using Okex.Net.Enums;
using System;

namespace Okex.Net.RestObjects
{
    public class OkexSpotTransaction
    {
        /// <summary>
        /// Trading pair symbol
        /// </summary>
        [JsonProperty("instrument_id")]
        public string Symbol { get; set; } = "";

        /// <summary>
        /// Bill record ID
        /// </summary>
        [JsonProperty("ledger_id")]
        public long LedgerId { get; set; }

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
        [JsonProperty("size")]
        public decimal Size { get; set; }

        /// <summary>
        /// Order ID
        /// </summary>
        [JsonProperty("order_id")]
        public long OrderId { get; set; }

        /// <summary>
        /// Time of order transaction
        /// </summary>
        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }

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
        /// The side that pays the trading fees, such as buy, sell
        /// </summary>
        [JsonProperty("side"), JsonConverter(typeof(SpotOrderSideConverter))]
        public OkexSpotOrderSide Side { get; set; }

        /// <summary>
        /// The token symbol, e.g. 'BTC'
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; } = "";

    }
}
