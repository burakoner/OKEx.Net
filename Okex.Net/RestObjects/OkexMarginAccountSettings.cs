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
    [JsonConverter(typeof(TypedDataConverter<OkexMarginAccountSettings>))]
    public class OkexMarginAccountSettings
    {
        [TypedData]
        public Dictionary<string, OkexMarginAccountSettingsCurrencyDetails> Currencies { get; set; }

        /// <summary>
        /// Trading pair
        /// </summary>
        [JsonProperty("instrument_id")]
        public string Symbol { get; set; } = "";

        [JsonProperty("product_id")]
        public string ProductId { get; set; } = "";

        public OkexMarginAccountSettings()
        {
            this.Currencies = new Dictionary<string, OkexMarginAccountSettingsCurrencyDetails>();
        }
    }

    public class OkexMarginAccountSettingsCurrencyDetails
    {
        /// <summary>
        /// Maximum loan amount
        /// </summary>
        [JsonProperty("available")]
        public decimal Available { get; set; }

        /// <summary>
        /// Maximum leverage
        /// </summary>
        [JsonProperty("leverage")]
        public decimal Leverage { get; set; }

        [JsonProperty("leverage_ratio")]
        public decimal LeverageRatio { get; set; }

        /// <summary>
        /// Interest rate
        /// </summary>
        [JsonProperty("rate")]
        public decimal Rate { get; set; }
    }

}