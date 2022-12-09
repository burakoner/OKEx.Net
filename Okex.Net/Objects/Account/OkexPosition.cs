namespace Okex.Net.Objects.Account;

public class OkexPosition
{
    [JsonProperty("cTime"), JsonConverter(typeof(OkexTimestampConverter))]
    public DateTime CreateTime { get; set; }

    [JsonProperty("uTime"), JsonConverter(typeof(OkexTimestampConverter))]
    public DateTime UpdateTime { get; set; }

    [JsonProperty("pTime"), JsonConverter(typeof(OkexTimestampConverter))]
    public DateTime pTime { get; set; }

    [JsonProperty("ccy")]
    public string Currency { get; set; }

    [JsonProperty("instId")]
    public string Instrument { get; set; }

    [JsonProperty("posCcy")]
    public string PositionCurrency { get; set; }

    [JsonProperty("posId")]
    public long? PositionId { get; set; }

    [JsonProperty("tradeId")]
    public long? TradeId { get; set; }

    [JsonProperty("instType"), JsonConverter(typeof(InstrumentTypeConverter))]
    public OkexInstrumentType InstrumentType { get; set; }

    [JsonProperty("posSide"), JsonConverter(typeof(PositionSideConverter))]
    public OkexPositionSide PositionSide { get; set; }

    [JsonProperty("mgnMode"), JsonConverter(typeof(MarginModeConverter))]
    public OkexMarginMode MarginMode { get; set; }

    [JsonProperty("liab")]
    public decimal? Liabilities { get; set; }

    [JsonProperty("liabCcy")]
    public string LiabilitiesCurrency { get; set; }

    [JsonProperty("imr")]
    public decimal? InitialMarginRequirement { get; set; }

    [JsonProperty("optVal")]
    public decimal? OptionValue { get; set; }

    [JsonProperty("upl")]
    public decimal? UnrealizedProfitAndLoss { get; set; }

    [JsonProperty("adl")]
    public decimal? AutoDeleveragingIndicator { get; set; }

    [JsonProperty("availPos")]
    public decimal? AvailablePositions { get; set; }

    [JsonProperty("interest")]
    public decimal? Interest { get; set; }

    [JsonProperty("lever")]
    public decimal? Leverage { get; set; }

    [JsonProperty("pos")]
    public decimal? PositionsQuantity { get; set; }

    [JsonProperty("uplRatio")]
    public decimal? UnrealizedProfitAndLossRatio { get; set; }

    [JsonProperty("notionalUsd")]
    public decimal? NotionalUsd { get; set; }

    [JsonProperty("mmr")]
    public decimal? MaintenanceMarginRequirement { get; set; }

    [JsonProperty("mgnRatio")]
    public decimal? MarginRatio { get; set; }

    [JsonProperty("margin")]
    public decimal? Margin { get; set; }

    [JsonProperty("last")]
    public decimal? LastPrice { get; set; }

    [JsonProperty("avgPx")]
    public decimal? AveragePrice { get; set; }

    [JsonProperty("liqPx")]
    public decimal? LiquidationPrice { get; set; }

    [JsonProperty("deltaBS")]
    public decimal? DeltaBS { get; set; }

    [JsonProperty("deltaPA")]
    public decimal? DeltaPA { get; set; }

    [JsonProperty("gammaBS")]
    public decimal? GammaBS { get; set; }

    [JsonProperty("gammaPA")]
    public decimal? GammaPA { get; set; }

    [JsonProperty("thetaBS")]
    public decimal? ThetaBS { get; set; }

    [JsonProperty("thetaPA")]
    public decimal? ThetaPA { get; set; }

    [JsonProperty("vegaBS")]
    public decimal? VegaBS { get; set; }

    [JsonProperty("vegaPA")]
    public decimal? VegaPA { get; set; }
}
