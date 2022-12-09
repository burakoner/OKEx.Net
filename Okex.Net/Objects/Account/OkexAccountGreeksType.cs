namespace Okex.Net.Objects.Account;

public class OkexAccountGreeksType
{
    [JsonProperty("greeksType"), JsonConverter(typeof(GreeksTypeConverter))]
    public OkexGreeksType GreeksType { get; set; }
}
