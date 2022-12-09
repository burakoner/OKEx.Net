namespace Okex.Net.Objects.Public;

public class OkexInterestRate
{
    [JsonProperty("basic")]
    public IEnumerable<OkexPublicInterestRateBasic> Basic { get; set; }

    [JsonProperty("vip")]
    public IEnumerable<OkexPublicInterestRateVip> Vip { get; set; }

    [JsonProperty("regular")]
    public IEnumerable<OkexPublicInterestRateRegular> regular { get; set; }

}

public class OkexPublicInterestRateBasic
{
    [JsonProperty("ccy")]
    public string Currency { get; set; }

    [JsonProperty("quota")]
    public decimal? Quota { get; set; }

    [JsonProperty("rate")]
    public decimal? Rate { get; set; }
}

public class OkexPublicInterestRateVip
{
    [JsonProperty("irDiscount")]
    public decimal? InterestRateDiscount { get; set; }

    [JsonProperty("loanQuotaCoef")]
    public decimal? LoanQuotaCoef { get; set; }

    [JsonProperty("level")]
    public string Level { get; set; }
}

public class OkexPublicInterestRateRegular
{
    [JsonProperty("irDiscount")]
    public decimal? InterestRateDiscount { get; set; }

    [JsonProperty("loanQuotaCoef")]
    public decimal? LoanQuotaCoef { get; set; }

    [JsonProperty("level")]
    public string Level { get; set; }
}
