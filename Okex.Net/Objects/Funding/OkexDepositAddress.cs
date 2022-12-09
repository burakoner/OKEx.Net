namespace Okex.Net.Objects.Funding;

public class OkexDepositAddress
{
    [JsonProperty("addr")]
    public string Address { get; set; }

    [JsonProperty("tag")]
    public string Tag { get; set; }

    [JsonProperty("memo")]
    public string Memo { get; set; }

    [JsonProperty("pmtId")]
    public string DepositPaymentId { get; set; }

    [JsonProperty("ccy")]
    public string Currency { get; set; }

    [JsonProperty("chain")]
    public string Chain { get; set; }

    [JsonProperty("selected")]
    public bool Selected { get; set; }

    [JsonProperty("ctAddr")]
    public string ContractAddr { get; set; }

    [JsonProperty("to"), JsonConverter(typeof(AccountConverter))]
    public OkexAccount? Account { get; set; }
}