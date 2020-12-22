using Newtonsoft.Json;
using Okex.Net.Attributes;
using System.Collections.Generic;

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
            Currencies = new Dictionary<string, OkexMarginAccountSettingsCurrencyDetails>();
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