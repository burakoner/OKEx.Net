using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using Okex.Net.Converters;
using Okex.Net.Enums;
using System;

namespace Okex.Net.RestObjects.Funding
{
    public class OkexWithdrawalResponse
    {
        [JsonProperty("ccy")]
        public string Currency { get; set; }

        [JsonProperty("chain")]
        public string Chain { get; set; }
        
        [JsonProperty("amt")]
        public decimal Amount { get; set; }

        [JsonProperty("wdId")]
        public string WithdrawalId { get; set; }
    }
}