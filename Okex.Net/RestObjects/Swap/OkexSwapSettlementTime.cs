using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Okex.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Okex.Net.Enums;

namespace Okex.Net.RestObjects
{
    public class OkexSwapSettlementTime
    {
        /// <summary>
        /// Contract ID, e.g. BTC-USD-SWAP,BTC-USDT-SWAP
        /// </summary>
        [JsonProperty("instrument_id")]
        public string Symbol { get; set; } = "";

        /// <summary>
        /// time of current funding
        /// </summary>
        [JsonProperty("funding_time")]
        public DateTime FundingTime { get; set; }

        /// <summary>
        /// next estimated funding rate
        /// </summary>
        [JsonProperty("settlement_time")]
        public DateTime SettlementTime { get; set; }

        /// <summary>
        /// current funding rate
        /// </summary>
        [JsonProperty("funding_rate")]
        public decimal FundingRate { get; set; }

        /// <summary>
        /// settlement rate
        /// </summary>
        [JsonProperty("estimated_rate")]
        public decimal EstimatedRate { get; set; }

    }
}
