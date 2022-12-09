namespace Okex.Net.Objects.Funding;

public class OkexSavingBalance
{
    [JsonProperty("earnings")]
    public decimal? Earnings { get; set; }

    [JsonProperty("redemptAmt")]
    public decimal? RedemptingAmount { get; set; }

    [JsonProperty("rate")]
    public decimal? LendingRate { get; set; }

    [JsonProperty("ccy")]
    public string Currency { get; set; }

    [JsonProperty("amt")]
    public decimal? Amount { get; set; }

    [JsonProperty("loanAmt")]
    public decimal? LoanAmount { get; set; }

    [JsonProperty("pendingAmt")]
    public decimal? PendingAmount { get; set; }

}