using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using Okex.Net.Converters;
using Okex.Net.Enums;
using System;

namespace Okex.Net.RestObjects.Funding
{
    public class OkexFundingBalance
    {
        [JsonProperty("ccy")]
        public string Currency { get; set; }
                
        [JsonProperty("availBal")]
        public decimal Available { get; set; }
        
        [JsonProperty("bal")]
        public decimal Balance { get; set; }
        
        [JsonProperty("frozenBal")]
        public decimal Frozen { get; set; }
    }
}
