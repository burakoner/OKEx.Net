using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Okex.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Okex.Net.RestObjects.Funding
{
    public class WithdrawalRequest
    {
        /// <summary>
        /// Token symbol
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; } = "";

        /// <summary>
        /// Withdrawal amount
        /// </summary>
        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        /// <summary>
        /// Wthdrawal result. An error code will be displayed if it failed.
        /// </summary>
        [JsonProperty("withdrawal_id")]
        public long WithdrawalId { get; set; }

        /// <summary>
        /// Hash record of the deposit
        /// </summary>
        [JsonProperty("result")]
        public bool Result { get; set; }
    }
}
