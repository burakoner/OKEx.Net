namespace Okex.Net.Objects.Market;

public class OkexOrderBook
{
    public string Instrument { get; set; }

    [JsonProperty("asks")]
    public IEnumerable<OkexOrderBookRow> Asks { get; set; } = new List<OkexOrderBookRow>();

    [JsonProperty("bids")]
    public IEnumerable<OkexOrderBookRow> Bids { get; set; } = new List<OkexOrderBookRow>();

    [JsonProperty("ts"), JsonConverter(typeof(OkexTimestampConverter))]
    public DateTime Time { get; set; }

    [JsonProperty("action")]
    public string Action { get; set; }

    [JsonProperty("checksum")]
    public long? Checksum { get; set; }
}
