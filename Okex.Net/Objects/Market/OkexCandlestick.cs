namespace Okex.Net.Objects.Market;

[JsonConverter(typeof(ArrayConverter))]
public class OkexCandlestick
{
    [JsonIgnore]
    public string Instrument { get; set; }

    [ArrayProperty(0), JsonConverter(typeof(OkexTimestampConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// Open price
    /// </summary>
    [ArrayProperty(1)]
    public decimal Open { get; set; }

    /// <summary>
    /// Highest price
    /// </summary>
    [ArrayProperty(2)]
    public decimal High { get; set; }

    /// <summary>
    /// Lowest price
    /// </summary>
    [ArrayProperty(3)]
    public decimal Low { get; set; }

    /// <summary>
    /// Close price
    /// </summary>
    [ArrayProperty(4)]
    public decimal Close { get; set; }

    /// <summary>
    /// Trading volume
    /// </summary>
    [ArrayProperty(5)]
    public decimal Volume { get; set; }

    [ArrayProperty(6)]
    public decimal VolumeCurrency { get; set; }

    public override bool Equals(object obj)
    {
        // Check for null and compare run-time types.
        if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            return false;

        // Equal Check
        var stick = (OkexCandlestick)obj;
        return (this.Time == stick.Time)
            && (this.Instrument == stick.Instrument)
            && (this.Open == stick.Open)
            && (this.High == stick.High)
            && (this.Low == stick.Low)
            && (this.Close == stick.Close)
            && (this.Volume == stick.Volume)
            && (this.VolumeCurrency == stick.VolumeCurrency);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
