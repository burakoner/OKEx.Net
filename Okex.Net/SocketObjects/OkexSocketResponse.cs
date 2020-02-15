using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using Okex.Net.Converters;
using Okex.Net.RestObjects;
using System;
using System.Collections.Generic;

namespace Okex.Net.SocketObjects
{
    internal class OkexSocketResponse
    {
        [JsonProperty("event")]
        internal string Event { get; set; } = "";
    }

    internal class OkexSocketSubscribeResponse : OkexSocketResponse
    {
        [JsonProperty("channel")]
        internal string Channel { get; set; } = "";
    }

    internal class OkexSocketErrorResponse : OkexSocketResponse
    {
        [JsonProperty("errorCode")]
        internal string ErrorCode { get; set; } = "";

        [JsonProperty("message")]
        internal string ErrorMessage { get; set; } = "";
    }

    internal class OkexSocketLoginResponse : OkexSocketResponse
    {
        [JsonProperty("success")]
        internal bool Success { get; set; }
    }

    internal class OkexSocketUpdateResponse<T>
    {
        [JsonProperty("table")]
        internal string Table { get; set; } = "";

        /*
        [JsonOptionalProperty]
        public T Data { get; set; } = default!;
        [JsonOptionalProperty, JsonProperty("data")]
        private T Tick { set => Data = value; get => Data; }
        */

        [JsonProperty("data")]
        public T Data { get; set; } = default!;
    }

    internal class OkexSocketOrderBookUpdate
    {
        [JsonProperty("table")]
        internal string Table { get; set; } = "";

        [JsonProperty("action"), JsonOptionalProperty, JsonConverter(typeof(SpotOrderBookDataTypeConverter))]
        internal SpotOrderBookDataType DataType { get; set; }

        [JsonProperty("data")]
        public IEnumerable<OkexSpotOrderBook> Data { get; set; } = default!;
    }
}
