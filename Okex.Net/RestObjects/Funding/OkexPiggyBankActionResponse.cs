using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using Okex.Net.Converters;
using Okex.Net.Enums;
using System;

namespace Okex.Net.RestObjects.Funding
{
    public class OkexPiggyBankActionResponse
    {
        [JsonProperty("ccy")]
        public string Currency { get; set; }
              
        [JsonProperty("amt")]
        public decimal Amount { get; set; }

        [JsonProperty("side"), JsonConverter(typeof(PiggyBankActionSideConverter))]
        public OkexPiggyBankActionSide Side { get; set; }
    }
}