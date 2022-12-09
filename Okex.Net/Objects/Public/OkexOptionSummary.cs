namespace Okex.Net.Objects.Public;

public class OkexOptionSummary
{
    [JsonProperty("instId")]
    public string Instrument { get; set; }

    [JsonProperty("instType"), JsonConverter(typeof(InstrumentTypeConverter))]
    public OkexInstrumentType InstrumentType { get; set; }

    [JsonProperty("uly")]
    public string Underlying { get; set; }

    [JsonProperty("ts"), JsonConverter(typeof(OkexTimestampConverter))]
    public DateTime Time { get; set; }

    [JsonProperty("delta")]
    public decimal Delta { get; set; }

    [JsonProperty("gamma")]
    public decimal Gamma { get; set; }

    [JsonProperty("vega")]
    public decimal Vega { get; set; }

    [JsonProperty("theta")]
    public decimal Theta { get; set; }

    [JsonProperty("deltaBS")]
    public decimal DeltaBS { get; set; }

    [JsonProperty("gammaBS")]
    public decimal GammaBS { get; set; }

    [JsonProperty("vegaBS")]
    public decimal VegaBS { get; set; }

    [JsonProperty("thetaBS")]
    public decimal ThetaBS { get; set; }

    [JsonProperty("lever")]
    public decimal Leverage { get; set; }

    [JsonProperty("markVol")]
    public decimal? MarkVolatility { get; set; }

    [JsonProperty("bidVol")]
    public decimal? BidVolatility { get; set; }

    [JsonProperty("askVol")]
    public decimal? AskVolatility { get; set; }

    [JsonProperty("realVol")]
    public decimal? RealVolatility { get; set; }
}
