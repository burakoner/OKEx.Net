namespace Okex.Net.Objects.Trading;

[JsonConverter(typeof(ArrayConverter))]
public class OkexTakerVolume
{
    [JsonIgnore]
    public string Currency { get; set; }

    [ArrayProperty(0), JsonConverter(typeof(OkexTimestampConverter))]
    public DateTime Time { get; set; }

    [ArrayProperty(1)]
    public decimal SellVolume { get; set; }

    [ArrayProperty(2)]
    public decimal BuyVolume { get; set; }
}
