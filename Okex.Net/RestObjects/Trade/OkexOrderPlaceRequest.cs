using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using Okex.Net.Converters;
using Okex.Net.Enums;
using System;

namespace Okex.Net.RestObjects.Trade
{
    public class OkexOrderPlaceRequest
    {
        [JsonProperty("instId")]
        public string InstrumentId { get; set; }

        [JsonProperty("tdMode"), JsonConverter(typeof(TradeModeConverter))]
        public OkexTradeMode TradeMode { get; set; }

        [JsonProperty("side"), JsonConverter(typeof(OrderSideConverter))]
        public OkexOrderSide OrderSide { get; set; }

        [JsonProperty("posSide"), JsonConverter(typeof(PositionSideConverter))]
        public OkexPositionSide PositionSide { get; set; }

        [JsonProperty("ordType"), JsonConverter(typeof(OrderTypeConverter))]
        public OkexOrderType OrderType { get; set; }

        [JsonProperty("sz")]
        public decimal Size { get; set; }

        [JsonProperty("px", NullValueHandling = NullValueHandling.Ignore), JsonOptionalProperty]
        public decimal? Price { get; set; }

        [JsonProperty("ccy")]
        public string Currency { get; set; }

        [JsonProperty("clOrdId")]
        public string ClientOrderId { get; set; }

        [JsonProperty("tag")]
        public string Tag { get; set; }

        [JsonProperty("reduceOnly", NullValueHandling = NullValueHandling.Ignore), JsonOptionalProperty]
        public bool? ReduceOnly { get; set; }

        [JsonProperty("tgtCcy", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(QuantityTypeConverter)), JsonOptionalProperty]
        public OkexQuantityType? QuantityType { get; set; }
    }
}