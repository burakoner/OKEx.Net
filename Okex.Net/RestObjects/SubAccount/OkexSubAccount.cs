using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using Okex.Net.Converters;
using Okex.Net.Enums;
using System;

namespace Okex.Net.RestObjects.SubAccount
{
    public class OkexSubAccount
    {
        [JsonProperty("enable")]
        public bool Enable { get; set; }
        
        [JsonProperty("gAuth")]
        public bool GoogleAuth { get; set; }
        
        [JsonProperty("subAcct")]
        public string SubAccountName { get; set; }
        
        [JsonProperty("label")]
        public string Label { get; set; }
        
        [JsonProperty("mobile")]
        public string Mobile { get; set; }

        [JsonProperty("ts"), JsonConverter(typeof(OkexTimestampConverter))]
        public DateTime Time { get; set; }
    }
}
