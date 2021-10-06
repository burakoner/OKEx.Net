using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using Okex.Net.Converters;
using Okex.Net.Enums;
using System;

namespace Okex.Net.RestObjects.Funding
{
    public class OkexPiggyBankBalance
    {
        [JsonProperty("ccy")]
        public string Currency { get; set; }
              
        [JsonProperty("amt")]
        public decimal Amount { get; set; }
        
        [JsonProperty("earnings")]
        public decimal Earnings { get; set; }
    }
}