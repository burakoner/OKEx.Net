using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Okex.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Okex.Net.Enums;

namespace Okex.Net.RestObjects
{
    public class OkexFuturesLiquidatedOrder
    {
        /// <summary>
        /// Order creation time
        /// </summary>
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Contract ID, e.g. BTC-USD-180309,BTC-USDT-191227
        /// </summary>
        [JsonProperty("instrument_id")]
        public string Symbol { get; set; } = "";

        /// <summary>
        /// 3:Close long 4:Close short
        /// </summary>
        [JsonProperty("type"), JsonConverter(typeof(FuturesOrderTypeConverter))]
        public OkexFuturesOrderType Type { get; set; }

        /// <summary>
        /// Loss resulted by liquidation
        /// </summary>
        [JsonProperty("loss")]
        public decimal loss { get; set; }

        /// <summary>
        /// Order price
        /// </summary>
        [JsonProperty("price")]
        public decimal price { get; set; }

        /// <summary>
        /// Quantity
        /// </summary>
        [JsonProperty("size")]
        public decimal size { get; set; }
    }
}
