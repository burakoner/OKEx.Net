using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Okex.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Okex.Net.RestObjects.Funding
{
    public class SubAccount
    {
        [JsonProperty("data")]
        public SubAccountData? Data { get; set; }
    }

    public class SubAccountData
    {
        [JsonProperty("sub_account")]
        public string SubAccountName { get; set; } = "";

        [JsonProperty("asset_valuation")]
        public decimal AssetValuation { get; set; }

        [JsonProperty("account_type:funding")]
        public IEnumerable<SubAccountFundingAsset> FundingAssets { get; set; } = new List<SubAccountFundingAsset>();

        [JsonProperty("account_type:spot")]
        public IEnumerable<SubAccountSpotAsset> SpotAssets { get; set; } = new List<SubAccountSpotAsset>();

        [JsonProperty("account_type:futures")]
        public IEnumerable<SubAccountFuturesAsset> FuturesAssets { get; set; } = new List<SubAccountFuturesAsset>();

        // TODO: Margin
        // [JsonProperty("account_type:margin")]

        // TODO: Swap
        // [JsonProperty("account_type:swap")]
    }

    public class SubAccountFundingAsset
    {
        [JsonProperty("currency")]
        public string Currency { get; set; } = "";

        [JsonProperty("balance")]
        public decimal Balance { get; set; }

        [JsonProperty("max_withdraw")]
        public decimal MaxWithdraw { get; set; }

        [JsonProperty("available")]
        public decimal Available { get; set; }

        [JsonProperty("hold")]
        public decimal Hold { get; set; }
    }

    public class SubAccountSpotAsset
    {
        [JsonProperty("currency")]
        public string Currency { get; set; } = "";

        [JsonProperty("balance")]
        public decimal Balance { get; set; }

        [JsonProperty("max_withdraw")]
        public decimal MaxWithdraw { get; set; }

        [JsonProperty("available")]
        public decimal Available { get; set; }

        [JsonProperty("hold")]
        public decimal Hold { get; set; }
    }

    public class SubAccountFuturesAsset
    {
        [JsonProperty("currency")]
        public string Currency { get; set; } = "";

        [JsonProperty("underlying")]
        public string Underlying { get; set; } = "";

        [JsonProperty("balance")]
        public decimal Balance { get; set; }

        [JsonProperty("max_withdraw")]
        public decimal MaxWithdraw { get; set; }

        [JsonProperty("equity")]
        public decimal Equity { get; set; }
    }
}
