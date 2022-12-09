namespace Okex.Net.Objects.Funding;

public class OkexFundingBill
{
    [JsonProperty("billId")]
    public long? BillId { get; set; }

    [JsonProperty("ccy")]
    public string Currency { get; set; }

    [JsonProperty("bal")]
    public decimal? Balance { get; set; }

    [JsonProperty("balChg")]
    public decimal? BalanceChange { get; set; }

    [JsonProperty("type"), JsonConverter(typeof(FundingBillTypeConverter))]
    public OkexFundingBillType Type { get; set; }

    [JsonProperty("ts"), JsonConverter(typeof(OkexTimestampConverter))]
    public DateTime Time { get; set; }
}