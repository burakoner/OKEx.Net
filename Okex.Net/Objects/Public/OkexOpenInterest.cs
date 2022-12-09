namespace Okex.Net.Objects.Public;

public class OkexOpenInterest
{
    [JsonProperty("instType"), JsonConverter(typeof(InstrumentTypeConverter))]
    public OkexInstrumentType InstrumentType { get; set; }

    [JsonProperty("instId")]
    public string Instrument { get; set; }

    [JsonProperty("oi")]
    public decimal? OpenInterestCont { get; set; }

    [JsonProperty("oiCcy")]
    public decimal? OpenInterestCoin { get; set; }

    [JsonProperty("ts"), JsonConverter(typeof(OkexTimestampConverter))]
    public DateTime Time { get; set; }
}
