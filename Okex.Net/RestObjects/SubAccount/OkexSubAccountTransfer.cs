using Newtonsoft.Json;

namespace Okex.Net.RestObjects.SubAccount
{
    public class OkexSubAccountTransfer
    {
        [JsonProperty("transId")]
        public long? TransferId { get; set; }
    }
}
