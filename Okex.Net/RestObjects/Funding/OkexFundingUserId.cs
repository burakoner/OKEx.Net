using Newtonsoft.Json;

namespace Okex.Net.RestObjects
{
    public class OkexFundingUserId
    {
        /// <summary>
        /// User ID
        /// </summary>
        [JsonProperty("uid")]
        public string UserId { get; set; } = "";
    }

}
