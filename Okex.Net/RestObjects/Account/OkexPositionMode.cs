using Newtonsoft.Json;
using Okex.Net.Converters;

namespace Okex.Net.RestObjects.Account
{
    public class OkexPositionMode
    {
        [JsonProperty("posMode"), JsonConverter(typeof(PositionModeConverter))]
        public OkexPositionMode PositionMode { get; set; }
    }
}
