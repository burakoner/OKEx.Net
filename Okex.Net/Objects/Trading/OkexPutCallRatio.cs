namespace Okex.Net.Objects.Trading;

[JsonConverter(typeof(ArrayConverter))]
public class OkexPutCallRatio
{
    [ArrayProperty(0), JsonConverter(typeof(OkexTimestampConverter))]
    public DateTime Time { get; set; }

    [ArrayProperty(1)]
    public decimal OpenInterestRatio { get; set; }

    [ArrayProperty(2)]
    public decimal VolumeRatio { get; set; }
}
