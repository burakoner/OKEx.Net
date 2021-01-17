using CryptoExchange.Net.Attributes;
using Newtonsoft.Json;
using Okex.Net.Converters;
using Okex.Net.Enums;
using Okex.Net.RestObjects;
using System.Collections.Generic;

namespace Okex.Net.SocketObjects.Structure
{
    public class OkexSocketResponse
    {
        [JsonProperty("event")]
        public string Event { get; set; } = "";
    }

    public class OkexSocketSubscribeResponse : OkexSocketResponse
    {
        [JsonProperty("channel")]
        public string Channel { get; set; } = "";
    }

    public class OkexSocketErrorResponse : OkexSocketResponse
    {
        [JsonProperty("errorCode")]
        public string ErrorCode { get; set; } = "";

        [JsonProperty("message")]
        public string ErrorMessage { get; set; } = "";
    }

    public class OkexSocketLoginResponse : OkexSocketResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }
    }

    public class OkexSocketUpdateResponse<T>
    {
        [JsonProperty("table")]
        public string Table { get; set; } = "";

        /*
        [JsonOptionalProperty]
        public T Data { get; set; } = default!;
        [JsonOptionalProperty, JsonProperty("data")]
        private T Tick { set => Data = value; get => Data; }
        */

        [JsonProperty("data")]
        public T Data { get; set; } = default!;
    }

    public class OkexSpotOrderBookUpdate
    {
        [JsonProperty("table")]
        public string Table { get; set; } = "";

        [JsonProperty("action"), JsonOptionalProperty, JsonConverter(typeof(OrderBookDataTypeConverter))]
        public OkexOrderBookDataType DataType { get; set; }

        [JsonProperty("data")]
        public IEnumerable<OkexSpotOrderBook> Data { get; set; } = default!;
    }

    public class OkexFuturesOrderBookUpdate
    {
        [JsonProperty("table")]
        public string Table { get; set; } = "";

        [JsonProperty("action"), JsonOptionalProperty, JsonConverter(typeof(OrderBookDataTypeConverter))]
        public OkexOrderBookDataType DataType { get; set; }

        [JsonProperty("data")]
        public IEnumerable<OkexFuturesOrderBook> Data { get; set; } = default!;
    }

    public class OkexSwapOrderBookUpdate
    {
        [JsonProperty("table")]
        public string Table { get; set; } = "";

        [JsonProperty("action"), JsonOptionalProperty, JsonConverter(typeof(OrderBookDataTypeConverter))]
        public OkexOrderBookDataType DataType { get; set; }

        [JsonProperty("data")]
        public IEnumerable<OkexSwapOrderBook> Data { get; set; } = default!;
    }

    public class OkexOptionsOrderBookUpdate
    {
        [JsonProperty("table")]
        public string Table { get; set; } = "";

        [JsonProperty("action"), JsonOptionalProperty, JsonConverter(typeof(OrderBookDataTypeConverter))]
        public OkexOrderBookDataType DataType { get; set; }

        [JsonProperty("data")]
        public IEnumerable<OkexOptionsOrderBook> Data { get; set; } = default!;
    }
}
