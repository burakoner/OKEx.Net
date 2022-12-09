namespace Okex.Net.Objects.Trading;

[JsonConverter(typeof(ArrayConverter))]
public class OkexInterestVolume
{
    [ArrayProperty(0), JsonConverter(typeof(OkexTimestampConverter))]
    public DateTime Time { get; set; }

    [ArrayProperty(1)]
    public decimal OpenInterest { get; set; }

    [ArrayProperty(2)]
    public decimal Volume { get; set; }
}
