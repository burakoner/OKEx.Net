namespace Okex.Net.Objects.Trade;

public class OkexOrder
{
    [JsonProperty("cTime"), JsonConverter(typeof(OkexTimestampConverter))]
    public DateTime CreateTime { get; set; }

    [JsonProperty("uTime"), JsonConverter(typeof(OkexTimestampConverter))]
    public DateTime UpdateTime { get; set; }

    [JsonProperty("fillTime"), JsonConverter(typeof(OkexTimestampConverter))]
    public DateTime? FillTime { get; set; }

    [JsonProperty("ccy")]
    public string Currency { get; set; }

    [JsonProperty("instId")]
    public string Instrument { get; set; }

    [JsonProperty("tradeId")]
    public long? TradeId { get; set; }

    [JsonProperty("ordId")]
    public long? OrderId { get; set; }

    [JsonProperty("clOrdId")]
    public string ClientOrderId { get; set; }

    [JsonProperty("instType"), JsonConverter(typeof(InstrumentTypeConverter))]
    public OkexInstrumentType InstrumentType { get; set; }

    [JsonProperty("posSide"), JsonConverter(typeof(PositionSideConverter))]
    public OkexPositionSide? PositionSide { get; set; }

    [JsonProperty("ordType"), JsonConverter(typeof(OrderTypeConverter))]
    public OkexOrderType OrderType { get; set; }

    [JsonProperty("side"), JsonConverter(typeof(OrderSideConverter))]
    public OkexOrderSide OrderSide { get; set; }

    [JsonProperty("tdMode"), JsonConverter(typeof(TradeModeConverter))]
    public OkexTradeMode TradeMode { get; set; }

    [JsonProperty("state"), JsonConverter(typeof(OrderStateConverter))]
    public OkexOrderState OrderState { get; set; }

    [JsonProperty("tgtCcy"), JsonConverter(typeof(QuantityTypeConverter))]
    public OkexQuantityType? QuantityType { get; set; }

    [JsonProperty("avgPx")]
    public decimal? AveragePrice { get; set; }

    [JsonProperty("px")]
    public decimal? Price { get; set; }

    [JsonProperty("sz")]
    public decimal? Quantity { get; set; }

    [JsonProperty("pnl")]
    public decimal? ProfitAndLoss { get; set; }

    [JsonProperty("tag")]
    public string Tag { get; set; }

    [JsonProperty("category")]
    public string Category { get; set; }

    [JsonProperty("accFillSz")]
    public decimal? AccumulatedFillQuantity { get; set; }

    [JsonProperty("fillPx")]
    public decimal? FillPrice { get; set; }

    [JsonProperty("fillSz")]
    public decimal? FillQuantity { get; set; }

    [JsonProperty("tpTriggerPx")]
    public decimal? TakeProfitTriggerPrice { get; set; }

    [JsonProperty("tpOrdPx")]
    public decimal? TakeProfitOrderPrice { get; set; }

    [JsonProperty("slTriggerPx")]
    public decimal? StopLossTriggerPrice { get; set; }

    [JsonProperty("slOrdPx")]
    public decimal? StopLossOrderPrice { get; set; }

    [JsonProperty("feeCcy")]
    public string FeeCurrency { get; set; }

    [JsonProperty("fee")]
    public decimal? Fee { get; set; }

    [JsonProperty("rebateCcy")]
    public string RebateCurrency { get; set; }

    [JsonProperty("rebate")]
    public decimal? Rebate { get; set; }
}