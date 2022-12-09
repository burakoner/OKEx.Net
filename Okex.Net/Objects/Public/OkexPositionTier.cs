namespace Okex.Net.Objects.Public;

public class OkexPositionTier
{
    [JsonProperty("instId")]
    public string Instrument { get; set; }

    [JsonProperty("uly")]
    public string Underlying { get; set; }

    [JsonProperty("tier")]
    public int Tier { get; set; }

    [JsonProperty("minSz")]
    public decimal? MinimumSize { get; set; }

    [JsonProperty("maxSz")]
    public decimal? MaximumSize { get; set; }

    [JsonProperty("mmr")]
    public decimal? MaintenanceMarginRequirement { get; set; }

    [JsonProperty("imr")]
    public decimal? InitialMarginRequirement { get; set; }

    [JsonProperty("maxLever")]
    public decimal? MaximumLeverage { get; set; }

    [JsonProperty("optMgnFactor")]
    public decimal? OptionMarginCoefficient { get; set; }

    [JsonProperty("quoteMaxLoan")]
    public decimal? MaximumQuoteLoan { get; set; }

    [JsonProperty("baseMaxLoan")]
    public decimal? MaximumBaseLoan { get; set; }
}