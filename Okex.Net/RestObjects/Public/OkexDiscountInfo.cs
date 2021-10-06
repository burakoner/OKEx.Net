using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using Okex.Net.Converters;
using Okex.Net.Enums;
using System;
using System.Collections.Generic;

namespace Okex.Net.RestObjects.Public
{
    public class OkexDiscountInfo
    {
        [JsonProperty("ccy")]
        public string Currency { get; set; }

        [JsonProperty("amt")]
        public decimal? Amount { get; set; }
        
        [JsonProperty("discountLv")]
        public int DiscountLevel { get; set; }
        
        [JsonProperty("discountInfo")]
        public IEnumerable<OkexPublicDiscountInfoDetail> Details { get; set; }
    }   
    
    public class OkexPublicDiscountInfoDetail
    {
        [JsonProperty("discountRate")]
        public decimal? DiscountRate { get; set; }
        
        [JsonProperty("maxAmt")]
        public decimal? MaximumAmount { get; set; }

        [JsonProperty("minAmt")]
        public decimal? MinimumAmount { get; set; }
    }
}
