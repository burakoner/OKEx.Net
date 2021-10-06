using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using Okex.Net.Converters;
using Okex.Net.Enums;
using System;
using System.Collections.Generic;

namespace Okex.Net.RestObjects.Account
{
    public class OkexMaximumAvailableAmount
    {
        [JsonProperty("instId")]
        public string Instrument { get; set; }
        
        [JsonProperty("availBuy")]
        public decimal? AvailableBuy { get; set; }

        [JsonProperty("availSell")]
        public decimal? AvailableSell { get; set; }
    }
}
