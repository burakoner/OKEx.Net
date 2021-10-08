using Newtonsoft.Json;
using Okex.Net.Converters;

namespace Okex.Net.RestObjects.Account
{
    public class OkexGreeksType
    {
        [JsonProperty("greeksType"), JsonConverter(typeof(GreeksTypeConverter))]
        public OkexGreeksType GreeksType { get; set; }
    }
}
