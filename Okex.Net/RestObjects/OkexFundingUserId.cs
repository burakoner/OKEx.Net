using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Okex.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Okex.Net.RestObjects
{
    public class OkexFundingUserId
    {
        /// <summary>
        /// User ID
        /// </summary>
        [JsonProperty("uid")]
        public string UserId { get; set; } = "";
    }

}
