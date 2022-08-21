using Newtonsoft.Json;
using Okex.Net.Converters;
using Okex.Net.Enums;

namespace Okex.Net.Objects.Trade
{
    public class OkexClosePositionResponse
    {
        [JsonProperty("instId")]
        public string Instrument { get; set; }

        [JsonProperty("posSide"), JsonConverter(typeof(PositionSideConverter))]
        public OkexPositionSide PositionSide { get; set; }
    }
}
