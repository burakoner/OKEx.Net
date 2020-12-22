using Newtonsoft.Json;
using Okex.Net.Converters;
using Okex.Net.Enums;
using System;

namespace Okex.Net.RestObjects
{
    public class OkexFundingAssetValuation
    {
        /// <summary>
        /// The valuation according to a certain fiat currency can only be one of the following "BTC USD CNY JPY KRW RUB" The default unit is BTC
        /// </summary>
        [JsonProperty("valuation_currency")]
        public string ValuationCurrency { get; set; } = "";

        /// <summary>
        /// Estimated assets
        /// </summary>
        [JsonProperty("balance")]
        public decimal Balance { get; set; }

        /// <summary>
        /// Data return time
        /// </summary>
        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Line of Business Type。
        /// 0.Total account assets 
        /// 1.spot
        /// 3.futures
        /// 4.C2C
        /// 5.margin
        /// 6.Funding Account
        /// 8. PiggyBank
        /// 9.swap
        /// 12：option
        /// 14.mining account
        /// Query total assets by default
        /// </summary>
        [JsonProperty("account_type"), JsonConverter(typeof(FundingAccountTypeConverter))]
        public OkexFundingAccountType AccountType { get; set; }
    }
}
