namespace Okex.Net.Objects.Account;

public class OkexAccountBill
{
    [JsonProperty("ccy")]
    public string Currency { get; set; }

    [JsonProperty("ts"), JsonConverter(typeof(OkexTimestampConverter))]
    public DateTime Time { get; set; }

    [JsonProperty("instId")]
    public string Instrument { get; set; }

    [JsonProperty("instType"), JsonConverter(typeof(InstrumentTypeConverter))]
    public OkexInstrumentType? InstrumentType { get; set; }

    [JsonProperty("mgnMode"), JsonConverter(typeof(MarginModeConverter))]
    public OkexMarginMode? MarginMode { get; set; }

    [JsonProperty("billId")]
    public long? BillId { get; set; }

    [JsonProperty("ordId")]
    public long? OrderId { get; set; }

    [JsonProperty("bal")]
    public decimal? Balance { get; set; }

    [JsonProperty("balChg")]
    public decimal? BalanceChange { get; set; }

    [JsonProperty("sz")]
    public decimal? Quantity { get; set; }

    [JsonProperty("fee")]
    public decimal? Fee { get; set; }

    [JsonProperty("from"), JsonConverter(typeof(AccountConverter))]
    public OkexAccount? FromAccount { get; set; }

    [JsonProperty("to"), JsonConverter(typeof(AccountConverter))]
    public OkexAccount? ToAccount { get; set; }

    [JsonProperty("notes")]
    public string Notes { get; set; }
}
