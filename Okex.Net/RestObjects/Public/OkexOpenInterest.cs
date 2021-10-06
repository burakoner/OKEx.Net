using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using Okex.Net.Converters;
using Okex.Net.Enums;
using System;
using System.Collections.Generic;

namespace Okex.Net.RestObjects.Public
{
    public class OkexOpenInterest
    {
        [JsonProperty("instType"), JsonConverter(typeof(InstrumentTypeConverter))]
        public OkexInstrumentType InstrumentType { get; set; }

        [JsonProperty("instId")]
        public string Instrument { get; set; }
        
        [JsonProperty("oi")]
        public decimal? OpenInterestCont { get; set; }
        
        [JsonProperty("oiCcy")]
        public decimal? OpenInterestCoin { get; set; }

        [JsonProperty("ts"), JsonConverter(typeof(OkexTimestampConverter))]
        public DateTime Time { get; set; }
    }
}
