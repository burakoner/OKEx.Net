namespace Okex.Net.Objects.Public;

public class OkexDeliveryExerciseHistory
{
    [JsonProperty("ts"), JsonConverter(typeof(OkexTimestampConverter))]
    public DateTime Time { get; set; }

    [JsonProperty("details")]
    public IEnumerable<OkexPublicDeliveryExerciseHistoryDetail> Details { get; set; }
}

public class OkexPublicDeliveryExerciseHistoryDetail
{
    [JsonProperty("type"), JsonConverter(typeof(DeliveryExerciseHistoryTypeConverter))]
    public OkexDeliveryExerciseHistoryType Type { get; set; }

    [JsonProperty("insId")]
    public string Instrument { get; set; }

    [JsonProperty("px")]
    public decimal Price { get; set; }
}
