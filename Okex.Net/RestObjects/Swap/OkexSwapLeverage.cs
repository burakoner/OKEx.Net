using Newtonsoft.Json;
using Okex.Net.Converters;
using Okex.Net.Enums;

namespace Okex.Net.RestObjects
{
    public class OkexSwapLeverage
    {
        [JsonProperty("margin_mode"), JsonConverter(typeof(SwapMarginModeConverter))]
        public OkexSwapMarginMode MarginMode { get; set; }

        [JsonProperty("instrument_id")]
        public string Symbol { get; set; } = "";

        [JsonProperty("long_leverage")]
        public decimal LongLeverage { get; set; }

        [JsonProperty("short_leverage")]
        public decimal ShortLeverage { get; set; }

    }
}