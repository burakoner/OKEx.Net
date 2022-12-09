namespace Okex.Net.Objects.Public;

public class OkexFundingRate
{
    [JsonProperty("instId")]
    public string Instrument { get; set; }

    [JsonProperty("instType"), JsonConverter(typeof(InstrumentTypeConverter))]
    public OkexInstrumentType InstrumentType { get; set; }

    [JsonProperty("fundingTime"), JsonConverter(typeof(OkexTimestampConverter))]
    public DateTime FundingTime { get; set; }

    [JsonProperty("fundingRate")]
    public decimal FundingRate { get; set; }

    [JsonProperty("nextFundingTime"), JsonConverter(typeof(OkexTimestampConverter))]
    public DateTime NextFundingTime { get; set; }

    [JsonProperty("nextFundingRate")]
    public decimal NextFundingRate { get; set; }
}
