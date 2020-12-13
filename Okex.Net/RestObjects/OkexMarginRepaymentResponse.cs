using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Okex.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace Okex.Net.RestObjects
{
    public class OkexMarginRepaymentResponse
    {
        /// <summary>
        /// Result of the request
        /// </summary>
        [JsonProperty("result")]
        public bool Result { get; set; }

        /// <summary>
        /// Borrow ID
        /// </summary>
        [JsonProperty("repayment_id")]
        public long? repayment_id { get; set; }

        /// <summary>
        /// client-supplied order ID
        /// </summary>
        [JsonProperty("client_oid")]
        public string ClientOrderId { get; set; } = "";
    }

}