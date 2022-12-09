namespace Okex.Net.Objects.Account;

public class OkexMaximumAmount
{
    [JsonProperty("instId")]
    public string Instrument { get; set; }

    [JsonProperty("ccy")]
    public string Currency { get; set; }

    [JsonProperty("maxBuy")]
    public decimal? MaximumBuy { get; set; }

    [JsonProperty("maxSell")]
    public decimal? MaximumSell { get; set; }
}
