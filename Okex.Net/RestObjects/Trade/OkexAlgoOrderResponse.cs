using Newtonsoft.Json;

namespace Okex.Net.RestObjects.Trade
{
    public class OkexAlgoOrderResponse
    {
        [JsonProperty("algoId")]
        public long? AlgoOrderId { get; set; }

        [JsonProperty("sCode")]
        public string Code { get; set; }

        [JsonProperty("sMsg")]
        public string Message { get; set; }
    }
}
