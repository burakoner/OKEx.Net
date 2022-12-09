namespace Okex.Net.Objects.Market;

public class OkexIndexComponents
{
    [JsonProperty("last")]
    public decimal LastPrice { get; set; }

    [JsonProperty("index")]
    public string Index { get; set; }

    [JsonProperty("ts"), JsonConverter(typeof(OkexTimestampConverter))]
    public DateTime Time { get; set; }

    [JsonProperty("components")]
    public IEnumerable<OkexIndexComponent> Components { get; set; }
}

public class OkexIndexComponent
{
    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    [JsonProperty("symPx")]
    public decimal Price { get; set; }

    [JsonProperty("wgt")]
    public decimal Weight { get; set; }

    [JsonProperty("cnvPx")]
    public decimal ConvertPrice { get; set; }

    [JsonProperty("exch")]
    public string Exchange { get; set; }
}
