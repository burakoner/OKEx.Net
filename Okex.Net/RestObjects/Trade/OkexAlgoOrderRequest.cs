using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using Okex.Net.Converters;
using Okex.Net.Enums;
using System;

namespace Okex.Net.RestObjects.Trade
{
    public class OkexAlgoOrderRequest
    {
        [JsonProperty("algoId")]
        public long AlgoOrderId { get; set; }

        [JsonProperty("instId")]
        public string Instrument { get; set; }
    }
}
