using Newtonsoft.Json;
using Okex.Net.Converters;
using Okex.Net.Enums;

namespace Okex.Net.Objects.Account
{
    public class OkexAccountPositionMode
    {
        [JsonProperty("posMode"), JsonConverter(typeof(PositionModeConverter))]
        public OkexPositionMode PositionMode { get; set; }
    }
}
