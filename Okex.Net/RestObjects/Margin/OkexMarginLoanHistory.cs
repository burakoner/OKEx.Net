using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Okex.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace Okex.Net.RestObjects
{
    public class OkexMarginLoanHistory
    {
        /// <summary>
        /// borrow ID
        /// </summary>
        [JsonProperty("borrow_id")]
        public long BorrowId { get; set; }

        /// <summary>
        /// token
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; } = "";

        /// <summary>
        /// trading pair
        /// </summary>
        [JsonProperty("instrument_id")]
        public string Symbol { get; set; } = "";

        [JsonProperty("product_id")]
        public string ProductId { get; set; } = "";

        /// <summary>
        /// borrow time
        /// </summary>
        [JsonProperty("Timestamp")]
        public DateTime Timestamp { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// force repay time
        /// </summary>
        [JsonProperty("force_repay_time")]
        public DateTime? ForceRepayTime { get; set; }

        /// <summary>
        /// last interest accrual time
        /// </summary>
        [JsonProperty("last_interest_time")]
        public DateTime? LastInterestTime { get; set; }

        /// <summary>
        /// borrow amount
        /// </summary>
        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        /// <summary>
        /// interest
        /// </summary>
        [JsonProperty("interest")]
        public decimal Interest { get; set; }

        /// <summary>
        /// interest repaid
        /// </summary>
        [JsonProperty("paid_interest")]
        public decimal PaidInterest { get; set; }

        /// <summary>
        /// rate
        /// </summary>
        [JsonProperty("rate")]
        public decimal Rate { get; set; }

        [JsonProperty("repay_amount")]
        public decimal? RepayAmount { get; set; }

        [JsonProperty("repay_interest")]
        public decimal? RepayInterest { get; set; }

        /// <summary>
        /// amount repaid
        /// </summary>
        [JsonProperty("returned_amount")]
        public decimal? ReturnedAmount { get; set; }
    }

}