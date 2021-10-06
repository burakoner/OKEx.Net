using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using Okex.Net.Converters;
using Okex.Net.Enums;
using System;
using System.Collections.Generic;

namespace Okex.Net.RestObjects.Account
{
    public class OkexMaximumAmount
    {
        [JsonProperty("instId")]
        public string Instrument { get; set; }
        
        [JsonProperty("ccy")]
        public string Currency { get; set; }

        [JsonProperty("maxBuy")]
        public decimal? MaximumBuy { get; set; }

        [JsonProperty("maxSell")]
        public decimal? MaximumSell { get; set; }
    }
}
