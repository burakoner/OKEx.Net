using Newtonsoft.Json;

namespace Okex.Net.RestObjects.Funding
{
    public class OkexFundingBalance
    {
        [JsonProperty("ccy")]
        public string Currency { get; set; }

        [JsonProperty("availBal")]
        public decimal Available { get; set; }

        [JsonProperty("bal")]
        public decimal Balance { get; set; }

        [JsonProperty("frozenBal")]
        public decimal Frozen { get; set; }
    }
}
