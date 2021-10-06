using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using Okex.Net.Converters;
using Okex.Net.Enums;
using System;
using System.Collections.Generic;

namespace Okex.Net.RestObjects.Account
{
    public class OkexInterestRate
    {
        [JsonProperty("ccy")]
        public string Currency { get; set; }

        [JsonProperty("interestRate")]
        public decimal? InterestRate { get; set; }
    }
}
