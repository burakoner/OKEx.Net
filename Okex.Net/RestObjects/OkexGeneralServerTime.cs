using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Okex.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Okex.Net.RestObjects
{
    public class OkexGeneralServerTime
    {
        [JsonProperty("iso")]
        public DateTime IsoTime { get; set; }

        [JsonProperty("epoch")]
        public decimal EpochTime { get; set; }
    }
}
