using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Objects;
using Newtonsoft.Json;

namespace Okex.Net.CoreObjects
{
    public class OkexApiResponse<T>
    {
        [JsonProperty("code", NullValueHandling = NullValueHandling.Ignore), JsonOptionalProperty]
        public int ErrorCode { get; set; }

        [JsonProperty("msg", NullValueHandling = NullValueHandling.Ignore), JsonOptionalProperty]
        public string ErrorMessage { get; set; }

        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore), JsonOptionalProperty]
        public T Data { get; set; }
    }
}