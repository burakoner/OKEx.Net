using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using Okex.Net.Converters;
using Okex.Net.Enums;
using System;

namespace Okex.Net.RestObjects.SubAccount
{
    public class OkexSubAccountName
    {
        [JsonProperty("subAcct")]
        public string SubAccountName { get; set; }
    }
}
