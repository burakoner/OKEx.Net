using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Okex.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Okex.Net.RestObjects.General
{
    public class ServerTime
    {
        [JsonProperty("iso")]
        public DateTime IsoTime { get; set; }

        [JsonProperty("epoch")]
        public decimal EpochTime { get; set; }
    }
}
