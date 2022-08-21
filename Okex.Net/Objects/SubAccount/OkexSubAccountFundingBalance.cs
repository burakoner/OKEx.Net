using Newtonsoft.Json;
using Okex.Net.Converters;
using System;
using System.Collections.Generic;

namespace Okex.Net.Objects.SubAccount
{
    public class OkexSubAccountFundingBalance
    {
        [JsonProperty("availBal")]
        public decimal? AvailableBalance { get; set; }

        [JsonProperty("bal")]
        public decimal? Balance { get; set; }

        [JsonProperty("ccy")]
        public string Currency { get; set; }

        [JsonProperty("frozenBal")]
        public decimal? FrozenBalance { get; set; }
    }
}
