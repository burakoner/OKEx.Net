using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Okex.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Okex.Net.RestObjects
{
    public class OkexFundingAssetTransfer
    {
        /// <summary>
        /// Transfer result. An error code will be displayed if it failed.
        /// </summary>
        [JsonProperty("result")]
        public bool Result { get; set; }

        /// <summary>
        /// Transfer amount
        /// </summary>
        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        /// <summary>
        /// Token to be transferred
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; } = "";

        /// <summary>
        /// The remitting account
        /// </summary>
        [JsonProperty("from"), JsonConverter(typeof(FundingTransferAccountTypeConverter))]
        public FundingTransferAccountType FromAccount { get; set; }

        /// <summary>
        /// The beneficiary account
        /// </summary>
        [JsonProperty("to"), JsonConverter(typeof(FundingTransferAccountTypeConverter))]
        public FundingTransferAccountType ToAccount { get; set; }

        /// <summary>
        /// Transfer ID
        /// </summary>
        [JsonProperty("transfer_id")]
        public long TransferId { get; set; }
    }
}
