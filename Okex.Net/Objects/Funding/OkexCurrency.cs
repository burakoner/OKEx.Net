namespace Okex.Net.Objects.Funding;

public class OkexCurrency
{
    [JsonProperty("ccy")]
    public string Currency { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("chain")]
    public string Chain { get; set; }

    [JsonProperty("canDep")]
    public bool AllowDeposit { get; set; }

    [JsonProperty("canWd")]
    public bool AllowWithdrawal { get; set; }

    [JsonProperty("canInternal")]
    public bool AllowInternalTransfer { get; set; }

    [JsonProperty("minWd")]
    public decimal MinimumWithdrawalAmount { get; set; }

    [JsonProperty("minFee")]
    public decimal MinimumWithdrawealFee { get; set; }

    [JsonProperty("maxFee")]
    public decimal MaximumWithdrawealFee { get; set; }
}
