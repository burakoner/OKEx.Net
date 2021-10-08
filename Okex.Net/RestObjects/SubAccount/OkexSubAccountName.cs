using Newtonsoft.Json;

namespace Okex.Net.RestObjects.SubAccount
{
    public class OkexSubAccountName
    {
        [JsonProperty("subAcct")]
        public string SubAccountName { get; set; }
    }
}
