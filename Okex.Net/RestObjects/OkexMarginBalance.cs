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
    public class OkexMarginBalance
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        // TODO
        [JsonIgnore]
        public OkexMarginBalanceCurrencyDetails BaseCurrency { get; set; }

        // TODO
        [JsonIgnore]
        public OkexMarginBalanceCurrencyDetails QuoteCurrency { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        /// <summary>
        /// Trading pair
        /// </summary>
        [JsonProperty("instrument_id")]
        public string Symbol { get; set; } = "";

        /// <summary>
        /// Liquidation price
        /// </summary>
        [JsonProperty("liquidation_price")]
        public decimal LiquidationPrice { get; set; } 

        /// <summary>
        /// Maintenance margin ratio
        /// </summary>
        [JsonProperty("maint_margin_ratio")]
        public decimal MaintMarginRatio { get; set; } 

        /// <summary>
        /// Margin ratio
        /// </summary>
        [JsonProperty("margin_ratio")]
        public decimal? MarginRatio { get; set; } 
        
        [JsonProperty("product_id")]
        public string ProductId { get; set; } = "";

        /// <summary>
        /// Risk rate
        /// </summary>
        [JsonProperty("risk_rate")]
        public decimal? RiskRate { get; set; } 

        /// <summary>
        /// Margin borrowing Position Tiers
        /// </summary>
        [JsonProperty("tiers")]
        public string Tiers { get; set; } = "";
    }

    public class OkexMarginBalanceCurrencyDetails
    {
        /// <summary>
        /// Available amount
        /// </summary>
        [JsonProperty("available")]
        public decimal Available { get; set; }

        /// <summary>
        /// Remaining balance
        /// </summary>
        [JsonProperty("balance")]
        public decimal Balance { get; set; }

        /// <summary>
        /// Borrowed tokens (unpaid)
        /// </summary>
        [JsonProperty("borrowed")]
        public decimal borrowed { get; set; }

        /// <summary>
        /// Available transfer amount
        /// </summary>
        [JsonProperty("can_withdraw")]
        public decimal CanWithdraw { get; set; }

        /// <summary>
        /// Frozen Amount
        /// </summary>
        [JsonProperty("frozen")]
        public decimal Frozen { get; set; }

        [JsonProperty("hold")]
        public decimal Hold { get; set; }

        [JsonProperty("holds")]
        public decimal Holds { get; set; }

        /// <summary>
        /// Interest (unpaid)
        /// </summary>
        [JsonProperty("lending_fee")]
        public decimal LendingFee { get; set; }
    }

}