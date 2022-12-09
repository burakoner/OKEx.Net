namespace Okex.Net.Objects.SubAccount;

public class OkexSubAccountApiKey
{
    [JsonProperty("subAcct")]
    public string SubAccountName { get; set; }

    [JsonProperty("label")]
    public string Label { get; set; }

    [JsonProperty("apiKey")]
    public string apiKey { get; set; }

    /*
    [JsonProperty("secretKey")]
    public string secretKey { get; set; }

    [JsonProperty("Passphrase")]
    public string Passphrase { get; set; }

    [JsonProperty("perm"), JsonConverter(typeof(ApiPermissionConverter))]
    public OkexApiPermission Permission { get; set; }
    */

    [JsonProperty("perm")]
    public string Permissions { get; set; }

    [JsonProperty("ip")]
    public string IpAddresses { get; set; }

    [JsonProperty("ts"), JsonConverter(typeof(OkexTimestampConverter))]
    public DateTime Time { get; set; }
}
