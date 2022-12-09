namespace Okex.Net.Objects.SubAccount;

public class OkexSubAccount
{
    [JsonProperty("enable")]
    public bool Enable { get; set; }

    [JsonProperty("gAuth")]
    public bool GoogleAuth { get; set; }

    [JsonProperty("subAcct")]
    public string SubAccountName { get; set; }

    [JsonProperty("label")]
    public string Label { get; set; }

    [JsonProperty("mobile")]
    public string Mobile { get; set; }

    [JsonProperty("canTransOut")]
    public bool CanTransOut { get; set; }

    [JsonProperty("ts"), JsonConverter(typeof(OkexTimestampConverter))]
    public DateTime Time { get; set; }

    [JsonProperty("type"), JsonConverter(typeof(SubAccountTypeConverter))]
    public OkexSubAccountType Type { get; set; }
}
