namespace Okex.Net.Objects.Trade;

public class OkexClosePositionResponse
{
    [JsonProperty("instId")]
    public string Instrument { get; set; }

    [JsonProperty("posSide"), JsonConverter(typeof(PositionSideConverter))]
    public OkexPositionSide PositionSide { get; set; }
}
