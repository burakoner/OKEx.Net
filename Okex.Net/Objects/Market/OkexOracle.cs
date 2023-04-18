namespace Okex.Net.Objects.Market;

public class OkexOracle
{
    [JsonProperty("messages")]
    public IEnumerable<string> Messages { get; set; }

    [JsonProperty("signatures")]
    public IEnumerable<string> Signatures { get; set; }

    [JsonProperty("timestamp"), JsonConverter(typeof(OkexTimestampSecondsConverter))]
    public DateTime Time { get; set; }

    [JsonProperty("prices")]
    public Dictionary<string, decimal> Prices { get; set; } = new Dictionary<string, decimal>();
}
