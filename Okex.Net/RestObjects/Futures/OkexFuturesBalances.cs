using CryptoExchange.Net.Attributes;
using Newtonsoft.Json;
using Okex.Net.Attributes;
using Okex.Net.Converters;
using Okex.Net.Enums;
using System.Collections.Generic;

namespace Okex.Net.RestObjects
{
    [JsonConverter(typeof(TypedDataConverter<OkexFuturesBalances>))]
    public class OkexFuturesBalances
    {
        [TypedData]
        [JsonProperty("info")]
        public Dictionary<string, OkexFuturesBalance> Balances { get; set; } = new Dictionary<string, OkexFuturesBalance>();
    }   
    
    public class OkexFuturesBalance
    {
        /// <summary>
        /// Account Type
        /// </summary>
        [JsonProperty("margin_mode"), JsonConverter(typeof(FuturesMarginModeConverter))]
        public OkexFuturesMarginMode MarginMode { get; set; }

        /// <summary>
        /// Account balance currency eg.BTC
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; } = "";

        /// <summary>
        /// Underlying index e.g:BTC-USD，BTC-USDT
        /// !!! CROSS-MARGIN ONLY !!!
        /// </summary>
        [JsonProperty("underlying"), JsonOptionalProperty]
        public string Underlying { get; set; } = "";

        /// <summary>
        /// Equity of the account
        /// </summary>
        [JsonProperty("equity")]
        public decimal Equity { get; set; }

        /// <summary>
        /// Transferable amount
        /// </summary>
        [JsonProperty("can_withdraw")]
        public decimal CanWithdraw { get; set; }

        /// <summary>
        /// Liquidation mode: tier（partial)
        /// </summary>
        [JsonProperty("liqui_mode")]
        public string LiquidationMode { get; set; } = "";

        /// <summary>
        /// liquidation fee
        /// !!! CROSS-MARGIN ONLY !!!
        /// </summary>
        [JsonProperty("liqui_fee_rate"), JsonOptionalProperty]
        public decimal LiquidationFeeRate { get; set; }

        /// <summary>
        /// Margin (frozen for open orders + open interests)
        /// !!! CROSS-MARGIN ONLY !!!
        /// </summary>
        [JsonProperty("margin"), JsonOptionalProperty]
        public decimal Margin { get; set; }

        /// <summary>
        /// Margin frozen for open interests
        /// !!! CROSS-MARGIN ONLY !!!
        /// </summary>
        [JsonProperty("margin_frozen"), JsonOptionalProperty]
        public decimal MarginFrozen { get; set; }

        /// <summary>
        /// Margin frozen for open orders
        /// !!! CROSS-MARGIN ONLY !!!
        /// </summary>
        [JsonProperty("margin_for_unfilled"), JsonOptionalProperty]
        public decimal MarginForUnfilled { get; set; }

        /// <summary>
        /// Margin ratio
        /// !!! CROSS-MARGIN ONLY !!!
        /// </summary>
        [JsonProperty("margin_ratio"), JsonOptionalProperty]
        public decimal MarginRatio { get; set; }

        /// <summary>
        /// Maintenance Margin Ratio
        /// !!! CROSS-MARGIN ONLY !!!
        /// </summary>
        [JsonProperty("maint_margin_ratio"), JsonOptionalProperty]
        public decimal MaintenanceMarginRatio { get; set; }

        /// <summary>
        /// Balance of the account
        /// </summary>
        [JsonProperty("total_avail_balance")]
        public decimal TotalAvailableBalance { get; set; }

        /// <summary>
        /// Realized profit and loss
        /// !!! CROSS-MARGIN ONLY !!!
        /// </summary>
        [JsonProperty("realized_pnl"), JsonOptionalProperty]
        public decimal RealizedPnl { get; set; }

        /// <summary>
        /// Unrealized profit and loss
        /// !!! CROSS-MARGIN ONLY !!!
        /// </summary>
        [JsonProperty("unrealized_pnl"), JsonOptionalProperty]
        public decimal UnrealizedPnl { get; set; }

        /// <summary>
        /// Auto Margin Status
        /// !!! FIXED-MARGIN ONLY !!!
        /// </summary>
        [JsonProperty("auto_margin"), JsonOptionalProperty]
        public bool AutoMargin { get; set; }

        /// <summary>
        /// Balances of Contracts
        /// !!! FIXED-MARGIN ONLY !!!
        /// </summary>
        [JsonProperty("contracts"), JsonOptionalProperty]
        public List<OkexFuturesFixedBalance> Contracts { get; set; } = new List<OkexFuturesFixedBalance>();
    }

    /// <summary>
    /// !!! FIXED-MARGIN ONLY !!!
    /// </summary>
    public class OkexFuturesFixedBalance
    {
        [JsonProperty("instrument_id")]
        public string Symbol { get; set; } = "";

        [JsonProperty("available_qty")]
        public decimal AvailableQuantity { get; set; }

        [JsonProperty("fixed_balance")]
        public decimal FixedBalance { get; set; }

        /// <summary>
        /// Margin frozen for open interests
        /// </summary>
        [JsonProperty("margin_frozen")]
        public decimal MarginFrozen { get; set; }

        /// <summary>
        /// Margin frozen for open orders
        /// </summary>
        [JsonProperty("margin_for_unfilled")]
        public decimal MarginForUnfilled { get; set; }

        /// <summary>
        /// Realized profit and loss
        /// </summary>
        [JsonProperty("realized_pnl")]
        public decimal RealizedPnl { get; set; }

        /// <summary>
        /// Unrealized profit and loss
        /// </summary>
        [JsonProperty("unrealized_pnl")]
        public decimal UnrealizedPnl { get; set; }
    }
}
