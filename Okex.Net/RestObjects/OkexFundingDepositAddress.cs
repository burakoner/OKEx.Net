using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Okex.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Okex.Net.RestObjects
{
    public class OkexFundingDepositAddress
    {
        /// <summary>
        /// Token symbol
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; } = "";

        /// <summary>
        /// Deposit address
        /// </summary>
        [JsonProperty("address")]
        public string Address { get; set; } = "";

        /// <summary>
        /// The beneficiary account
        /// </summary>
        [JsonProperty("to"), JsonConverter(typeof(FundingTransferAccountTypeConverter))]
        public OkexFundingTransferAccountType BeneficiaryAccount { get; set; }

        /// <summary>
        /// Deposit memo (This will not be returned if the token does not require a payment_id for deposit)
        /// </summary>
        [JsonProperty("memo"), JsonOptionalProperty]
        public string Memo { get; set; } = "";

        /// <summary>
        /// Deposit tag (This will not be returned if the token does not require a tag for deposit)
        /// </summary>
        [JsonProperty("tag"), JsonOptionalProperty]
        public string Tag { get; set; } = "";

        /// <summary>
        /// Deposit payment ID (This will not be returned if the token does not require a payment_id for deposit)
        /// </summary>
        [JsonProperty("payment_id"), JsonOptionalProperty]
        public string PaymentId { get; set; } = "";
    }
}
