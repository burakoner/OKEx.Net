namespace Okex.Net.Objects.Account;

public class OkexAccountPositionMode
{
    [JsonProperty("posMode"), JsonConverter(typeof(PositionModeConverter))]
    public OkexPositionMode PositionMode { get; set; }
}
