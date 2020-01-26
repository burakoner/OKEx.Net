using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Okex.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Okex.Net.RestObjects.Funding
{
    public class Bill
    {
        /// <summary>
        /// Bill ID
        /// </summary>
        [JsonProperty("ledger_id")]
        public long LedgerId { get; set; }

        /// <summary>
        /// Creation time
        /// </summary>
        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Token symbol
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; } = "";

        /// <summary>
        /// Remaining balance
        /// </summary>
        [JsonProperty("balance")]
        public decimal Balance { get; set; }

        /// <summary>
        /// Amount changed
        /// </summary>
        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        /// <summary>
        /// Service fees
        /// </summary>
        [JsonProperty("fee")]
        public decimal Fee { get; set; }

        /// <summary>
        /// Type of bills
        /// </summary>
        [JsonProperty("typename")]
        public string TypeName { get; set; } = "";
    }

}
