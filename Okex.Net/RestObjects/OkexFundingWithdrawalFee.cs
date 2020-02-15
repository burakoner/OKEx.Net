using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Okex.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Okex.Net.RestObjects
{
    public class OkexFundingWithdrawalFee
    {
        /// <summary>
        /// Token symbol
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; } = "";

        /// <summary>
        /// Minimum withdrawal fee
        /// </summary>
        [JsonProperty("min_fee")]
        public decimal MinimumFee { get; set; }

        /// <summary>
        /// Maximum withdrawal fee
        /// </summary>
        [JsonProperty("max_fee")]
        public decimal MaximumFee { get; set; }
    }
}
