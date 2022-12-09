namespace Okex.Net.Objects.Trade;

public class OkexAlgoOrderRequest
{
    [JsonProperty("algoId")]
    public long AlgoOrderId { get; set; }

    [JsonProperty("instId")]
    public string Instrument { get; set; }
}
