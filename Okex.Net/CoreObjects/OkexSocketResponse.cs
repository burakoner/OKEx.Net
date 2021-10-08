using CryptoExchange.Net.Attributes;
using Newtonsoft.Json;
using Okex.Net.RestObjects.Market;
using System.Collections.Generic;

namespace Okex.Net.CoreObjects
{
    public class OkexSocketResponse
    {
        public bool Success
        {
            get
            {
                return
                    string.IsNullOrEmpty(ErrorCode)
                    || ErrorCode.Trim() == "0";
            }
        }

        [JsonProperty("event")]
        public string Event { get; set; }

        [JsonProperty("code")]
        public string ErrorCode { get; set; }

        [JsonProperty("msg")]
        public string ErrorMessage { get; set; }
    }

    public class OkexSocketUpdateResponse<T> : OkexSocketResponse
    {
        /*
        [JsonOptionalProperty]
        public T Data { get; set; } = default!;
        [JsonOptionalProperty, JsonProperty("data")]
        private T Tick { set => Data = value; get => Data; }
        */

        [JsonProperty("data")]
        public T Data { get; set; } = default!;
    }

    public class OkexOrderBookUpdate
    {
        [JsonProperty("action"), JsonOptionalProperty]
        public string Action { get; set; }

        [JsonProperty("data")]
        public IEnumerable<OkexOrderBook> Data { get; set; } = default!;
    }
}
