namespace Okex.Net.Objects.Public;

public class OkexDiscountInfo
{
    [JsonProperty("ccy")]
    public string Currency { get; set; }

    [JsonProperty("amt")]
    public decimal? Amount { get; set; }

    [JsonProperty("discountLv")]
    public int DiscountLevel { get; set; }

    [JsonProperty("discountInfo")]
    public IEnumerable<OkexPublicDiscountInfoDetail> Details { get; set; }
}

public class OkexPublicDiscountInfoDetail
{
    [JsonProperty("discountRate")]
    public decimal? DiscountRate { get; set; }

    [JsonProperty("maxAmt")]
    public decimal? MaximumAmount { get; set; }

    [JsonProperty("minAmt")]
    public decimal? MinimumAmount { get; set; }
}
