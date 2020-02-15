using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Okex.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Okex.Net.RestObjects
{
    public class OkexFundingDepositDetails
    {
        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        /// <summary>
        /// Token symbol
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; } = "";

        /// <summary>
        /// Hash record of the deposit
        /// </summary>
        [JsonProperty("txid")]
        public string TxId { get; set; } = "";

        /// <summary>
        /// Only internal OKEx account is returned，not the address on the blockchain
        /// </summary>
        [JsonProperty("from")]
        public string FromAddress { get; set; } = "";

        /// <summary>
        /// Deposit address
        /// </summary>
        [JsonProperty("to")]
        public string ToAddress { get; set; } = "";

        /// <summary>
        /// Time that the deposit is credited
        /// </summary>
        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Status of deposit
        /// </summary>
        [JsonProperty("status"), JsonConverter(typeof(FundingDepositStatusConverter))]
        public FundingDepositStatus Status { get; set; }
    }
}
