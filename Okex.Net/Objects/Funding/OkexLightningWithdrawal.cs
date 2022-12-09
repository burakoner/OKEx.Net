namespace Okex.Net.Objects.Funding;

public class OkexLightningWithdrawal
{
    [JsonProperty("cTime"), JsonConverter(typeof(OkexTimestampConverter))]
    public DateTime Time { get; set; }

    [JsonProperty("wdId")]
    public string WithdrawalId { get; set; }
}