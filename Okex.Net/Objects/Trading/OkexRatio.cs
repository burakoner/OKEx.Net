namespace Okex.Net.Objects.Trading;

[JsonConverter(typeof(ArrayConverter))]
public class OkexRatio
{
    [ArrayProperty(0), JsonConverter(typeof(OkexTimestampConverter))]
    public DateTime Time { get; set; }

    [ArrayProperty(1)]
    public decimal Ratio { get; set; }
}
