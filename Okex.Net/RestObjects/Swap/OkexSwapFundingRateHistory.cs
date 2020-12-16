using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Okex.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Okex.Net.Enums;

namespace Okex.Net.RestObjects
{
    public class OkexSwapFundingRateHistory
    {
        [JsonProperty("instrument_id")]
        public string Symbol { get; set; } = "";

        [JsonProperty("funding_rate")]
        public decimal FundingRate { get; set; }

        [JsonProperty("realized_rate")]
        public decimal RealizedRate { get; set; }

        [JsonProperty("interest_rate")]
        public decimal InterestRate { get; set; }

        [JsonProperty("funding_time")]
        public DateTime FundingTime { get; set; }
    }
}
