using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Okex.Net.Converters;
using Newtonsoft.Json;
using System;
using Okex.Net.Enums;

namespace Okex.Net.RestObjects
{
    public class OkexOptionsTransaction
    {
        /// <summary>
        /// Instrument ID, e.g. BTC-USD-190927-12500-C
        /// </summary>
        [JsonProperty("instrument_id")]
        public string Instrument { get; set; } = "";

        /// <summary>
        /// Trade ID, consistent with last_fill_id, matching id
        /// </summary>
        [JsonProperty("trade_id")]
        public long TradeId { get; set; }

        /// <summary>
        /// Bill ID, consistent with ledger_id in bill
        /// </summary>
        [JsonProperty("ledger_id")]
        public long LedgerId { get; set; }

        /// <summary>
        /// Order ID
        /// </summary>
        [JsonProperty("order_id")]
        public long OrderId { get; set; }

        /// <summary>
        /// Time that the order is filled, in ISO8601 format
        /// </summary>
        [JsonProperty("created_at")]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Filled Price
        /// </summary>
        [JsonProperty("fill_price")]
        public decimal FillPrice { get; set; }

        /// <summary>
        /// Filled Quantity
        /// </summary>
        [JsonProperty("fill_qty")]
        public decimal FillQuantity { get; set; }

        /// <summary>
        /// Transaction fee
        /// </summary>
        [JsonProperty("fee")]
        public decimal Fee { get; set; }

        /// <summary>
        /// Side of the order (buy or sell)
        /// </summary>
        [JsonProperty("side"), JsonConverter(typeof(OptionsOrderSideConverter))]
        public OkexOptionsOrderSide Side { get; set; }

        /// <summary>
        /// Taker or Maker (T or M)
        /// </summary>
        [JsonProperty("exec_type"), JsonConverter(typeof(TraderRoleConverter))]
        public OkexTraderRole TraderRole { get; set; }
    }
}
