using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using Okex.Net.Converters;
using Okex.Net.Enums;
using System;
using System.Collections.Generic;

namespace Okex.Net.RestObjects.Account
{
    public class OkexConfiguration
    {
        [JsonProperty("uid")]
        public long UserId { get; set; }
        
        [JsonProperty("acctLv"), JsonConverter(typeof(AccountLevelConverter))]
        public OkexAccountLevel AccountLevel { get; set; }
        
        [JsonProperty("posMode"), JsonConverter(typeof(PositionModeConverter))]
        public OkexPositionMode PositionMode { get; set; }

        [JsonProperty("autoLoan"), JsonConverter(typeof(OkexBooleanConverter))]
        public bool AutoLoan { get; set; }

        [JsonProperty("greeksType"), JsonConverter(typeof(GreeksTypeConverter))]
        public OkexGreeksType GreeksType { get; set; }

        [JsonProperty("level")]
        public string Level { get; set; }
        
        [JsonProperty("levelTmp")]
        public string LevelTemporary { get; set; }
    }
}
