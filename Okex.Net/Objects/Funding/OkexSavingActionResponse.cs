namespace Okex.Net.Objects.Funding;

public class OkexSavingActionResponse
{
    [JsonProperty("ccy")]
    public string Currency { get; set; }

    [JsonProperty("amt")]
    public decimal? Amount { get; set; }

    [JsonProperty("rate")]
    public decimal? PurchaseRate { get; set; }

    [JsonProperty("side"), JsonConverter(typeof(SavingActionSideConverter))]
    public OkexSavingActionSide Side { get; set; }
}