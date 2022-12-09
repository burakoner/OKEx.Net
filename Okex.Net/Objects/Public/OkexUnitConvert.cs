namespace Okex.Net.Objects.Public;

public class OkexUnitConvert
{
    [JsonProperty("instId")]
    public string Instrument { get; set; }

    [JsonProperty("px")]
    public decimal Price { get; set; }

    [JsonProperty("sz")]
    public decimal Size { get; set; }

    [JsonProperty("type"), JsonConverter(typeof(ConvertTypeConverter))]
    public OkexConvertType Type { get; set; }

    [JsonProperty("unit"), JsonConverter(typeof(ConvertUnitConverter))]
    public OkexConvertUnit Unit { get; set; }
}
