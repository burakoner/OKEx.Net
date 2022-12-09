namespace Okex.Net.Objects.Public;

public class OkexLiquidationInfo
{
    [JsonProperty("instId")]
    public string Instrument { get; set; }

    [JsonProperty("instType"), JsonConverter(typeof(InstrumentTypeConverter))]
    public OkexInstrumentType InstrumentType { get; set; }

    [JsonProperty("totalLoss")]
    public decimal? TotalLoss { get; set; }

    [JsonProperty("uly")]
    public string Underlying { get; set; }

    [JsonProperty("details")]
    public IEnumerable<OkexPublicLiquidationInfoDetail> Details { get; set; }
}

public class OkexPublicLiquidationInfoDetail
{
    [JsonProperty("side"), JsonConverter(typeof(OrderSideConverter))]
    public OkexOrderSide OrderSide { get; set; }

    [JsonProperty("posSide"), JsonConverter(typeof(PositionSideConverter))]
    public OkexPositionSide PositionSide { get; set; }

    [JsonProperty("bkPx")]
    public decimal? BankruptcyPrice { get; set; }

    [JsonProperty("sz")]
    public decimal? NumberOfLiquidations { get; set; }

    [JsonProperty("bkLoss")]
    public decimal? NumberOfLosses { get; set; }

    [JsonProperty("ccy")]
    public string Currency { get; set; }

    [JsonProperty("ts"), JsonConverter(typeof(OkexTimestampConverter))]
    public DateTime Time { get; set; }
}