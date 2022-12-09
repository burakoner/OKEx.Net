namespace Okex.Net.Objects.Account;

public class OkexInterestAccrued
{
    [JsonProperty("instId")]
    public string Instrument { get; set; }

    [JsonProperty("ccy")]
    public string Currency { get; set; }

    [JsonProperty("mgnMode"), JsonConverter(typeof(MarginModeConverter))]
    public OkexMarginMode MarginMode { get; set; }

    [JsonProperty("interest")]
    public decimal? Interest { get; set; }

    [JsonProperty("interestRate")]
    public decimal? InterestRate { get; set; }

    [JsonProperty("liab")]
    public decimal? Liabilities { get; set; }

    [JsonProperty("ts"), JsonConverter(typeof(OkexTimestampConverter))]
    public DateTime Time { get; set; }
}
