namespace Okex.Net.Objects.Funding;

public class OkexLightningDeposit
{
    [JsonProperty("cTime"), JsonConverter(typeof(OkexTimestampConverter))]
    public DateTime Time { get; set; }

    [JsonProperty("invoice")]
    public string Invoice { get; set; }
}