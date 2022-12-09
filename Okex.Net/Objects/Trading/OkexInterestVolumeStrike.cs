namespace Okex.Net.Objects.Trading;

[JsonConverter(typeof(ArrayConverter))]
public class OkexInterestVolumeStrike
{
    [ArrayProperty(0), JsonConverter(typeof(OkexTimestampConverter))]
    public DateTime Time { get; set; }

    [ArrayProperty(1)]
    public string Strike { get; set; }

    [ArrayProperty(2)]
    public decimal CallOpenInterest { get; set; }

    [ArrayProperty(3)]
    public decimal PutOpenInterest { get; set; }

    [ArrayProperty(4)]
    public decimal CallVolume { get; set; }

    [ArrayProperty(5)]
    public decimal PutVolume { get; set; }
}
