using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Okex.Net.RestObjects
{
    public class OkexIndexConstituents
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("msg")]
        public string Message { get; set; } = "";
        
        [JsonProperty("detailMsg")]
        public string DetailMessage { get; set; } = "";

        [JsonProperty("error_code")]
        public string ErrorCode { get; set; } = "";

        [JsonProperty("error_message")]
        public string ErrorMessage { get; set; } = "";

        [JsonProperty("data")]
        public OkexIndexConstituentsData Data { get; set; } = new OkexIndexConstituentsData();
    }

    public class OkexIndexConstituentsData
    {
        [JsonProperty("last")]
        public decimal Last { get; set; }

        [JsonProperty("instrument_id")]
        public string Symbol { get; set; } = "";

        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }

        [JsonProperty("constituents")]
        public IEnumerable<OkexIndexConstituentsDataConstituent> Constituents { get; set; } = new List<OkexIndexConstituentsDataConstituent>();
    }

    public class OkexIndexConstituentsDataConstituent
    {
        [JsonProperty("exchange")]
        public string Exchange { get; set; } = "";
        
        [JsonProperty("symbol")]
        public string Symbol { get; set; } = "";

        [JsonProperty("original_price")]
        public decimal OriginalPrice { get; set; }
        
        [JsonProperty("usd_price")]
        public decimal UsdPrice { get; set; }
        
        [JsonProperty("weight")]
        public decimal Weight { get; set; }
    }
}
