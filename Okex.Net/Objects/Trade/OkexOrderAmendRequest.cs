namespace Okex.Net.Objects.Trade;

public class OkexOrderAmendRequest
{
    [JsonProperty("ordId", NullValueHandling = NullValueHandling.Ignore)]
    public long? OrderId { get; set; }

    [JsonProperty("clOrdId")]
    public string ClientOrderId { get; set; }

    [JsonProperty("reqId")]
    public string RequestId { get; set; }

    [JsonProperty("instId")]
    public string InstrumentId { get; set; }

    [JsonProperty("cxlOnFail", NullValueHandling = NullValueHandling.Ignore)]
    public bool? CancelOnFail { get; set; }

    [JsonProperty("newSz", NullValueHandling = NullValueHandling.Ignore)]
    public string NewQuantity { get; set; }

    [JsonProperty("newPx", NullValueHandling = NullValueHandling.Ignore)]
    public decimal? NewPrice { get; set; }
}