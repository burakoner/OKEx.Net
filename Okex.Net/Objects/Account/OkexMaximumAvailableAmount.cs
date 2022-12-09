namespace Okex.Net.Objects.Account;

public class OkexMaximumAvailableAmount
{
    [JsonProperty("instId")]
    public string Instrument { get; set; }

    [JsonProperty("availBuy")]
    public decimal? AvailableBuy { get; set; }

    [JsonProperty("availSell")]
    public decimal? AvailableSell { get; set; }
}
