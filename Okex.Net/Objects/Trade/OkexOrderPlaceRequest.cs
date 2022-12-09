namespace Okex.Net.Objects.Trade;

public class OkexOrderPlaceRequest
{
    [JsonProperty("instId")]
    public string InstrumentId { get; set; }

    [JsonProperty("tdMode"), JsonConverter(typeof(TradeModeConverter))]
    public OkexTradeMode TradeMode { get; set; }

    [JsonProperty("side"), JsonConverter(typeof(OrderSideConverter))]
    public OkexOrderSide OrderSide { get; set; }

    [JsonProperty("posSide"), JsonConverter(typeof(PositionSideConverter))]
    public OkexPositionSide PositionSide { get; set; }

    [JsonProperty("ordType"), JsonConverter(typeof(OrderTypeConverter))]
    public OkexOrderType OrderType { get; set; }

    [JsonProperty("sz")]
    public string Size { get; set; }

    [JsonProperty("px", NullValueHandling = NullValueHandling.Ignore)]
    public string Price { get; set; }

    [JsonProperty("ccy")]
    public string Currency { get; set; }

    [JsonProperty("clOrdId")]
    public string ClientOrderId { get; set; }

    [JsonProperty("tag")]
    public string Tag { get; set; }

    [JsonProperty("reduceOnly", NullValueHandling = NullValueHandling.Ignore)]
    public bool? ReduceOnly { get; set; }

    [JsonProperty("tgtCcy", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(QuantityTypeConverter))]
    public OkexQuantityType? QuantityType { get; set; }
}