using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Okex.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Okex.Net.Enums;
using Okex.Net.Attributes;

namespace Okex.Net.RestObjects
{
    public class OkexOptionsBalance
    {
        [JsonProperty("error_code")]
        public string ErrorCode { get; set; } = "";

        [JsonProperty("error_message")]
        public string ErrorMessage { get; set; } = "";

        /// <summary>
        /// The underlying index that the contract is based upon, e.g. BTC-USD.
        /// </summary>
        [JsonProperty("underlying")]
        public string Underlying { get; set; } = "";

        /// <summary>
        /// Token symbol, eg. BTC
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; } = "";

        /// <summary>
        /// "0": Normal，"1": Delayed deleveraging，"2": Deleveraging
        /// </summary>
        [JsonProperty("account_status"), JsonConverter(typeof(OptionsAccountStatusConverter))]
        public OkexOptionsAccountStatus AccountStatus { get; set; }

        /// <summary>
        /// Available Margin
        /// </summary>
        [JsonProperty("avail_margin")]
        public decimal AvailableMargin { get; set; }

        /// <summary>
        /// delta
        /// </summary>
        [JsonProperty("delta")]
        public decimal Delta { get; set; }

        /// <summary>
        /// Equity of the account
        /// </summary>
        [JsonProperty("equity")]
        public decimal Equity { get; set; }

        /// <summary>
        /// gamma
        /// </summary>
        [JsonProperty("gamma")]
        public decimal Gamma { get; set; }

        /// <summary>
        /// theta
        /// </summary>
        [JsonProperty("theta")]
        public decimal Theta { get; set; }

        /// <summary>
        /// vega
        /// </summary>
        [JsonProperty("vega")]
        public decimal Vega { get; set; }

        /// <summary>
        /// Maintenance margin ratio
        /// </summary>
        [JsonProperty("maintenance_margin")]
        public decimal MaintenanceMargin { get; set; }

        /// <summary>
        /// Margin balance
        /// </summary>
        [JsonProperty("margin_balance")]
        public decimal MarginBalance { get; set; }

        /// <summary>
        /// Margin frozen for open orders
        /// </summary>
        [JsonProperty("margin_for_unfilled")]
        public decimal MarginForUnfilled { get; set; }

        /// <summary>
        /// Margin frozen (for open orders + open interests)
        /// </summary>
        [JsonProperty("margin_frozen")]
        public decimal MarginFrozen { get; set; }

        /// <summary>
        /// Margin multiplier （Not currently used）
        /// </summary>
        [JsonProperty("margin_multiplier")]
        public decimal MarginMultiplier { get; set; }
        
        [JsonProperty("max_withdraw")]
        public decimal MaxWithdraw { get; set; }

        /// <summary>
        /// Option value
        /// </summary>
        [JsonProperty("option_value")]
        public decimal OptionValue { get; set; }

        /// <summary>
        /// Realized profits and losses
        /// </summary>
        [JsonProperty("realized_pnl")]
        public decimal RealizedPnl { get; set; }

        /// <summary>
        /// Risk factor （Not currently used）
        /// </summary>
        [JsonProperty("risk_factor")]
        public decimal RiskFactor { get; set; }

        /// <summary>
        /// Balance of the account
        /// </summary>
        [JsonProperty("total_avail_balance")]
        public decimal TotalAvailBalance { get; set; }

        /// <summary>
        /// Unrealized profits and losses
        /// </summary>
        [JsonProperty("unrealized_pnl")]
        public decimal UnrealizedPnl { get; set; }
    }
}
