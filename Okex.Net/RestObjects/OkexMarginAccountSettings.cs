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
    public class OkexMarginAccountSettings
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        // TODO
        [JsonIgnore]
        public OkexMarginAccountSettingsCurrencyDetails BaseCurrency { get; set; }

        // TODO
        [JsonIgnore]
        public OkexMarginAccountSettingsCurrencyDetails QuoteCurrency { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        /// <summary>
        /// Trading pair
        /// </summary>
        [JsonProperty("instrument_id")]
        public string Symbol { get; set; } = "";

        [JsonProperty("product_id")]
        public string ProductId { get; set; } = "";
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