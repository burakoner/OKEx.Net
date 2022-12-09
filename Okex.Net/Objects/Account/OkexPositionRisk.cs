namespace Okex.Net.Objects.Account;

public class OkexPositionRisk
{
    [JsonProperty("adjEq")]
    public decimal? AdjustedEquity { get; set; }

    [JsonProperty("ts"), JsonConverter(typeof(OkexTimestampConverter))]
    public DateTime Time { get; set; }

    [JsonProperty("balData")]
    public IEnumerable<OkexAccountPositionRiskBalanceData> BalanceData { get; set; }

    [JsonProperty("posData")]
    public IEnumerable<OkexAccountPositionRiskPositionData> PositionData { get; set; }
}

public class OkexAccountPositionRiskBalanceData
{
    [JsonProperty("ccy")]
    public string Currency { get; set; }

    [JsonProperty("disEq")]
    public decimal? DiscountEquity { get; set; }

    [JsonProperty("eq")]
    public decimal? Equity { get; set; }
}

public class OkexAccountPositionRiskPositionData
{
    [JsonProperty("ccy")]
    public string Currency { get; set; }

    [JsonProperty("instId")]
    public string Instrument { get; set; }

    [JsonProperty("instType"), JsonConverter(typeof(InstrumentTypeConverter))]
    public OkexInstrumentType InstrumentType { get; set; }

    [JsonProperty("mgnMode"), JsonConverter(typeof(MarginModeConverter))]
    public OkexMarginMode MarginMode { get; set; }

    [JsonProperty("notionalCcy")]
    public decimal? NotionalCcy { get; set; }

    [JsonProperty("notionalUsd")]
    public decimal? NotionalUsd { get; set; }

    [JsonProperty("pos")]
    public decimal? Quantity { get; set; }

    [JsonProperty("posCcy")]
    public string PositionCurrency { get; set; }

    [JsonProperty("posId")]
    public long PositionId { get; set; }

    [JsonProperty("posSide"), JsonConverter(typeof(PositionSideConverter))]
    public OkexPositionSide PositionSide { get; set; }
}
