using Newtonsoft.Json;

namespace Okex.Net.RestObjects.Account
{
    public class OkexMaximumAvailableAmount
    {
        [JsonProperty("instId")]
        public string Instrument { get; set; }

        [JsonProperty("availBuy")]
        public decimal? AvailableBuy { get; set; }

        [JsonProperty("availSell")]
        public decimal? AvailableSell { get; set; }
    }
}
