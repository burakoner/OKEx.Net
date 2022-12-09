namespace Okex.Net.Objects.Market;

public class Okex24HourVolume
{
    [JsonProperty("volUsd")]
    public decimal VolumeUsd { get; set; }

    [JsonProperty("volCny")]
    public decimal VolumeCny { get; set; }

    [JsonProperty("ts"), JsonConverter(typeof(OkexTimestampConverter))]
    public DateTime Time { get; set; }
}
