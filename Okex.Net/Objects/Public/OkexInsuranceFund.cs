namespace Okex.Net.Objects.Public;

public class OkexInsuranceFund
{
    [JsonProperty("total")]
    public decimal Total { get; set; }

    [JsonProperty("details")]
    public IEnumerable<OkexInsuranceFundDetail> Details { get; set; }
}

public class OkexInsuranceFundDetail
{
    [JsonProperty("amt")]
    public decimal Amount { get; set; }

    [JsonProperty("balance")]
    public decimal Balance { get; set; }

    [JsonProperty("ccy")]
    public string Currency { get; set; }

    [JsonProperty("type"), JsonConverter(typeof(InsuranceTypeConverter))]
    public OkexInsuranceType Type { get; set; }

    [JsonProperty("ts"), JsonConverter(typeof(OkexTimestampConverter))]
    public DateTime Timestamp { get; set; }
}
