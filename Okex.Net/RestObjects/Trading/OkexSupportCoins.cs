using Newtonsoft.Json;
using System.Collections.Generic;

namespace Okex.Net.RestObjects.Trading
{
    public class OkexSupportCoins
    {
        [JsonProperty("contract")]
        public IEnumerable<string> Contract { get; set; }

        [JsonProperty("option")]
        public IEnumerable<string> Option { get; set; }

        [JsonProperty("spot")]
        public IEnumerable<string> Spot { get; set; }
    }
}
