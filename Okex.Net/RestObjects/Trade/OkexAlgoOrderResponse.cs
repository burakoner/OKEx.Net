using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using Okex.Net.Converters;
using Okex.Net.Enums;
using System;

namespace Okex.Net.RestObjects.Trade
{
    public class OkexAlgoOrderResponse
    {
        [JsonProperty("algoId")]
        public long? AlgoOrderId { get; set; }

        [JsonProperty("sCode")]
        public string Code { get; set; }
        
        [JsonProperty("sMsg")]
        public string Message { get; set; }
    }
}
