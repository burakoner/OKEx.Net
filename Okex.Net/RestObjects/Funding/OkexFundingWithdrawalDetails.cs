using CryptoExchange.Net.Attributes;
using Newtonsoft.Json;
using Okex.Net.Converters;
using Okex.Net.Enums;
using System;

namespace Okex.Net.RestObjects
{
    public class OkexFundingWithdrawalDetails
    {
        /// <summary>
        /// Withdrawal ID
        /// </summary>
        [JsonProperty("withdrawal_id")]
        public long WithdrawalId { get; set; }

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
        /// Time the withdrawal request was submitted
        /// </summary>
        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Remitting address (User account ID will be shown for OKEx addresses)
        /// </summary>
        [JsonProperty("from")]
        public string FromAddress { get; set; } = "";

        /// <summary>
        /// Receiving address
        /// </summary>
        [JsonProperty("to")]
        public string ToAddress { get; set; } = "";

        /// <summary>
        /// Some tokens require a tag for withdrawals. This is not returned if not required
        /// </summary>
        [JsonProperty("tag"), JsonOptionalProperty]
        public string Tag { get; set; } = "";

        /// <summary>
        /// Some tokens require payment ID for withdrawals. This is not returned if not required
        /// </summary>
        [JsonProperty("payment_id"), JsonOptionalProperty]
        public string PaymentId { get; set; } = "";

        /// <summary>
        /// Some tokens require payment ID for withdrawals. This is not returned if not required
        /// </summary>
        [JsonProperty("memo"), JsonOptionalProperty]
        public string Memo { get; set; } = "";

        /// <summary>
        /// Hash record of the withdrawal. This parameter will not be returned for internal transfers
        /// </summary>
        [JsonProperty("txid")]
        public string TxId { get; set; } = "";

        /// <summary>
        /// Withdrawal fee
        /// </summary>
        [JsonProperty("fee")]
        public string Fee { get; set; } = "";

        /// <summary>
        /// Status of Withdrawal
        /// </summary>
        [JsonProperty("status"), JsonConverter(typeof(FundingWithdrawalStatusConverter))]
        public OkexFundingWithdrawalStatus Status { get; set; }
    }
}
