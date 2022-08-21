using Newtonsoft.Json;
using Okex.Net.Converters;
using Okex.Net.Enums;

namespace Okex.Net.Objects.Account
{
    public class OkexAccountGreeksType
    {
        [JsonProperty("greeksType"), JsonConverter(typeof(GreeksTypeConverter))]
        public OkexGreeksType GreeksType { get; set; }
    }
}
