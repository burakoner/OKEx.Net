using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using Okex.Net.Converters;
using Okex.Net.Enums;
using System;
using System.Collections.Generic;

namespace Okex.Net.RestObjects.Account
{
    public class OkexGreeksType
    {
        [JsonProperty("greeksType"), JsonConverter(typeof(GreeksTypeConverter))]
        public OkexGreeksType GreeksType { get; set; }
    }
}
