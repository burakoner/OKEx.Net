using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using Okex.Net.Converters;
using Okex.Net.RestObjects.Spot;
using System;
using System.Collections.Generic;

namespace Okex.Net.SocketObjects
{
    internal class SocketResponse
    {
        [JsonProperty("event")]
        internal string Event { get; set; } = "";
    }

    internal class SocketSubscribeResponse : SocketResponse
    {
        [JsonProperty("channel")]
        internal string Channel { get; set; } = "";
    }

    internal class SocketErrorResponse : SocketResponse
    {
        [JsonProperty("errorCode")]
        internal string ErrorCode { get; set; } = "";

        [JsonProperty("message")]
        internal string ErrorMessage { get; set; } = "";
    }

    internal class SocketLoginResponse : SocketResponse
    {
        [JsonProperty("success")]
        internal bool Success { get; set; }
    }

    internal class SocketUpdateResponse<T>
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

    internal class SocketOrderBookUpdate
    {
        [JsonProperty("table")]
        internal string Table { get; set; } = "";

        [JsonProperty("action"), JsonOptionalProperty, JsonConverter(typeof(SpotOrderBookDataTypeConverter))]
        internal SpotOrderBookDataType DataType { get; set; }

        [JsonProperty("data")]
        public IEnumerable<OrderBook> Data { get; set; } = default!;
    }
}
