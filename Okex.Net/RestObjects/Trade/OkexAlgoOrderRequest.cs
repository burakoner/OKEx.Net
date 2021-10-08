using Newtonsoft.Json;

namespace Okex.Net.RestObjects.Trade
{
    public class OkexAlgoOrderRequest
    {
        [JsonProperty("algoId")]
        public long AlgoOrderId { get; set; }

        [JsonProperty("instId")]
        public string Instrument { get; set; }
    }
}
