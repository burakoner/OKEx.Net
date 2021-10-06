using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using Okex.Net.Converters;
using Okex.Net.Enums;
using System;

namespace Okex.Net.RestObjects.Trade
{
    public class OkexOrderCancelRequest
    {
        [JsonProperty("instId")]
        public string InstrumentId { get; set; }

        [JsonProperty("ordId", NullValueHandling = NullValueHandling.Ignore), JsonOptionalProperty]
        public long? OrderId { get; set; }

        [JsonProperty("clOrdId", NullValueHandling = NullValueHandling.Ignore), JsonOptionalProperty]
        public string ClientOrderId { get; set; }
    }
}