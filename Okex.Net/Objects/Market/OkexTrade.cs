namespace Okex.Net.Objects.Market;

public class OkexTrade
{
    [JsonProperty("instId")]
    public string Instrument { get; set; }

    [JsonProperty("tradeId")]
    public long TradeId { get; set; }

    [JsonProperty("px")]
    public decimal Price { get; set; }

    [JsonProperty("sz")]
    public decimal Quantity { get; set; }

    [JsonProperty("side"), JsonConverter(typeof(TradeSideConverter))]
    public OkexTradeSide Side { get; set; }

    [JsonProperty("ts"), JsonConverter(typeof(OkexTimestampConverter))]
    public DateTime Time { get; set; }
}
