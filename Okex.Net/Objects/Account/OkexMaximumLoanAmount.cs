namespace Okex.Net.Objects.Account;

public class OkexMaximumLoanAmount
{
    [JsonProperty("instId")]
    public string Instrument { get; set; }

    [JsonProperty("mgnMode"), JsonConverter(typeof(MarginModeConverter))]
    public OkexMarginMode? MarginMode { get; set; }

    [JsonProperty("mgnCcy")]
    public string MarginCurrency { get; set; }

    [JsonProperty("maxLoan")]
    public decimal? MaximumLoan { get; set; }

    [JsonProperty("ccy")]
    public string Currency { get; set; }

    [JsonProperty("side"), JsonConverter(typeof(OrderSideConverter))]
    public OkexOrderSide? OrderSide { get; set; }
}
