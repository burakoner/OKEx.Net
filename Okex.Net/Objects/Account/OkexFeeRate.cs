namespace Okex.Net.Objects.Account;

public class OkexFeeRate
{
    [JsonProperty("category"), JsonConverter(typeof(FeeRateCategoryConverter))]
    public OkexFeeRateCategory? Category { get; set; }

    [JsonProperty("ts"), JsonConverter(typeof(OkexTimestampConverter))]
    public DateTime Time { get; set; }

    [JsonProperty("level")]
    public string Level { get; set; }

    [JsonProperty("maker")]
    public decimal? Maker { get; set; }

    [JsonProperty("taker")]
    public decimal? Taker { get; set; }

    [JsonProperty("instType"), JsonConverter(typeof(InstrumentTypeConverter))]
    public OkexInstrumentType InstrumentType { get; set; }

    [JsonProperty("delivery")]
    public decimal? Delivery { get; set; }

    [JsonProperty("exercise")]
    public decimal? Exercise { get; set; }
}
