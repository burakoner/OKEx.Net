namespace Okex.Net.Objects.Market;

public class OkexIndexTicker
{
    [JsonProperty("instId")]
    public string Instrument { get; set; }

    [JsonProperty("idxPx")]
    public decimal IndexPrice { get; set; }

    [JsonProperty("high24h")]
    public decimal High { get; set; }

    [JsonProperty("low24h")]
    public decimal Low { get; set; }

    [JsonProperty("open24h")]
    public decimal Open { get; set; }

    [JsonProperty("sodUtc0")]
    public decimal OpenPriceUtc0 { get; set; }

    [JsonProperty("sodUtc8")]
    public decimal OpenPriceUtc8 { get; set; }

    [JsonProperty("ts"), JsonConverter(typeof(OkexTimestampConverter))]
    public DateTime Time { get; set; }
}
