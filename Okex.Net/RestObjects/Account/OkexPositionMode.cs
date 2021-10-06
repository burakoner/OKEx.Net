using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using Okex.Net.Converters;
using Okex.Net.Enums;
using System;
using System.Collections.Generic;

namespace Okex.Net.RestObjects.Account
{
    public class OkexPositionMode
    {
        [JsonProperty("posMode"), JsonConverter(typeof(PositionModeConverter))]
        public OkexPositionMode PositionMode { get; set; }
    }
}
