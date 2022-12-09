namespace Okex.Net.Objects.Trade;

public class OkexTransaction
{
    [JsonProperty("instType"), JsonConverter(typeof(InstrumentTypeConverter))]
    public OkexInstrumentType InstrumentType { get; set; }

    [JsonProperty("instId")]
    public string Instrument { get; set; }

    [JsonProperty("tradeId")]
    public long? TradeId { get; set; }

    [JsonProperty("ordId")]
    public long? OrderId { get; set; }

    [JsonProperty("clOrdId")]
    public string ClientOrderId { get; set; }

    [JsonProperty("billId")]
    public long? BillId { get; set; }

    [JsonProperty("tag")]
    public string Tag { get; set; }

    [JsonProperty("fillPx")]
    public decimal? FillPrice { get; set; }

    [JsonProperty("fillSz")]
    public decimal? FillQuantity { get; set; }

    [JsonProperty("side"), JsonConverter(typeof(OrderSideConverter))]
    public OkexOrderSide OrderSide { get; set; }

    [JsonProperty("posSide"), JsonConverter(typeof(PositionSideConverter))]
    public OkexPositionSide PositionSide { get; set; }

    [JsonProperty("execType"), JsonConverter(typeof(OrderFlowTypeConverter))]
    public OkexOrderFlowType OrderFlowType { get; set; }

    [JsonProperty("feeCcy")]
    public string FeeCurrency { get; set; }

    [JsonProperty("fee")]
    public decimal? Fee { get; set; }

    [JsonProperty("ts"), JsonConverter(typeof(OkexTimestampConverter))]
    public DateTime Time { get; set; }
}