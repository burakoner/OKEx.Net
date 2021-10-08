using Newtonsoft.Json;
using Okex.Net.Converters;
using Okex.Net.Enums;
using System;

namespace Okex.Net.RestObjects.Public
{
    public class OkexFundingRateHistory
    {
        [JsonProperty("instId")]
        public string Instrument { get; set; }

        [JsonProperty("instType"), JsonConverter(typeof(InstrumentTypeConverter))]
        public OkexInstrumentType InstrumentType { get; set; }

        [JsonProperty("fundingTime"), JsonConverter(typeof(OkexTimestampConverter))]
        public DateTime FundingTime { get; set; }

        [JsonProperty("fundingRate")]
        public decimal FundingRate { get; set; }

        [JsonProperty("realizedRate")]
        public decimal RealizedRate { get; set; }
    }
}
