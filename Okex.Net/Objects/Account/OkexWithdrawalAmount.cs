namespace Okex.Net.Objects.Account;

public class OkexWithdrawalAmount
{
    [JsonProperty("ccy")]
    public string Currency { get; set; }

    [JsonProperty("maxWd")]
    public decimal? MaximumWithdrawal { get; set; }
}
