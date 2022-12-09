namespace Okex.Net.Objects.Account;

public class OkexInterestRate
{
    [JsonProperty("ccy")]
    public string Currency { get; set; }

    [JsonProperty("interestRate")]
    public decimal? InterestRate { get; set; }
}
