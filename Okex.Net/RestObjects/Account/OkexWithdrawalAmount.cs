using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using Okex.Net.Converters;
using Okex.Net.Enums;
using System;
using System.Collections.Generic;

namespace Okex.Net.RestObjects.Account
{
    public class OkexWithdrawalAmount
    {
        [JsonProperty("ccy")]
        public string Currency { get; set; }

        [JsonProperty("maxWd")]
        public decimal? MaximumWithdrawal { get; set; }
    }
}
