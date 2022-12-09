namespace Okex.Net.Objects.Funding;

public class OkexTransferResponse
{
    [JsonProperty("ccy")]
    public string Currency { get; set; }

    [JsonProperty("transId")]
    public long? TransferId { get; set; }

    [JsonProperty("amt")]
    public decimal Amount { get; set; }

    [JsonProperty("from"), JsonConverter(typeof(AccountConverter))]
    public OkexAccount? From { get; set; }

    [JsonProperty("to"), JsonConverter(typeof(AccountConverter))]
    public OkexAccount? To { get; set; }
}