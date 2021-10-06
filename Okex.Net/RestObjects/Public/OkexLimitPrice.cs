using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using Okex.Net.Converters;
using Okex.Net.Enums;
using System;
using System.Collections.Generic;

namespace Okex.Net.RestObjects.Public
{
    public class OkexLimitPrice
    {
        [JsonProperty("instId")]
        public string Instrument { get; set; }

        [JsonProperty("instType"), JsonConverter(typeof(InstrumentTypeConverter))]
        public OkexInstrumentType InstrumentType { get; set; }

        [JsonProperty("buyLmt")]
        public decimal BuyLimit { get; set; }
        
        [JsonProperty("sellLmt")]
        public decimal SellLimit { get; set; }

        [JsonProperty("ts"), JsonConverter(typeof(OkexTimestampConverter))]
        public DateTime Time { get; set; }
    }
}
