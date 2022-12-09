namespace Okex.Net.Objects.Public;

public class OkexLimitPrice
{
    [JsonProperty("instId")]
    public string Instrument { get; set; }

    [JsonProperty("instType"), JsonConverter(typeof(InstrumentTypeConverter))]
    public OkexInstrumentType InstrumentType { get; set; }

    [JsonProperty("buyLmt")]
    public decimal BuyLimit { get; set; }

    [JsonProperty("sellLmt")]
    public decimal SellLimit { get; set; }

    [JsonProperty("ts"), JsonConverter(typeof(OkexTimestampConverter))]
    public DateTime Time { get; set; }
}
