namespace Okex.Net.Objects.Public;

public class OkexVipInterestRate
{
    [JsonProperty("ccy")]
    public string Currency { get; set; }

    [JsonProperty("quota")]
    public decimal? Quota { get; set; }

    [JsonProperty("rate")]
    public decimal? Rate { get; set; }

    [JsonProperty("levelList")]
    public IEnumerable<OkexVipInterestRateLevel> LevelList { get; set; }
}

public class OkexVipInterestRateLevel
{
    [JsonProperty("level")]
    public string Level { get; set; }

    [JsonProperty("loanQuota")]
    public decimal? LoanQuota { get; set; }
}
