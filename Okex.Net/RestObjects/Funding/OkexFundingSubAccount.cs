using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Okex.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Okex.Net.RestObjects
{
    public class OkexFundingSubAccount
    {
        [JsonProperty("data")]
        public OkexFundingSubAccountData? Data { get; set; }
    }

    public class OkexFundingSubAccountData
    {
        [JsonProperty("sub_account")]
        public string SubAccountName { get; set; } = "";

        [JsonProperty("asset_valuation")]
        public decimal AssetValuation { get; set; }

        [JsonProperty("account_type:funding")]
        public IEnumerable<OkexFundingSubAccountFundingAsset> FundingAssets { get; set; } = new List<OkexFundingSubAccountFundingAsset>();

        [JsonProperty("account_type:spot")]
        public IEnumerable<OkexFundingSubAccountSpotAsset> SpotAssets { get; set; } = new List<OkexFundingSubAccountSpotAsset>();

        [JsonProperty("account_type:futures")]
        public IEnumerable<OkexFundingSubAccountFuturesAsset> FuturesAssets { get; set; } = new List<OkexFundingSubAccountFuturesAsset>();

        // TODO: Margin
        // [JsonProperty("account_type:margin")]

        // TODO: Swap
        // [JsonProperty("account_type:swap")]
    }

    public class OkexFundingSubAccountFundingAsset
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

    public class OkexFundingSubAccountSpotAsset
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

    public class OkexFundingSubAccountFuturesAsset
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
