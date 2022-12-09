namespace Okex.Net.Objects.Funding;

public class OkexWithdrawalHistory
{
    [JsonProperty("ts"), JsonConverter(typeof(OkexTimestampConverter))]
    public DateTime Time { get; set; }

    [JsonProperty("wdId")]
    public long WithdrawalId { get; set; }

    [JsonProperty("state"), JsonConverter(typeof(WithdrawalStateConverter))]
    public OkexWithdrawalState State { get; set; }

    [JsonProperty("ccy")]
    public string Currency { get; set; }

    [JsonProperty("chain")]
    public string Chain { get; set; }

    [JsonProperty("txId")]
    public string TransactionId { get; set; }

    [JsonProperty("from")]
    public string From { get; set; }

    [JsonProperty("to")]
    public string To { get; set; }

    [JsonProperty("fee")]
    public decimal Fee { get; set; }

    [JsonProperty("amt")]
    public decimal Amount { get; set; }
}