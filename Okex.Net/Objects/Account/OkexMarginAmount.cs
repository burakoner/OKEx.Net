namespace Okex.Net.Objects.Account;

public class OkexMarginAmount
{
    [JsonProperty("instId")]
    public string Instrument { get; set; }

    [JsonProperty("posSide"), JsonConverter(typeof(PositionSideConverter))]
    public OkexPositionSide? PositionSide { get; set; }

    [JsonProperty("amt")]
    public decimal? amt { get; set; }

    [JsonProperty("type"), JsonConverter(typeof(MarginAddReduceConverter))]
    public OkexMarginAddReduce? MarginAddReduce { get; set; }
}
