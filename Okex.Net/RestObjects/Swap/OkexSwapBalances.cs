using Newtonsoft.Json;
using Okex.Net.Converters;
using Okex.Net.Enums;
using System;
using System.Collections.Generic;

namespace Okex.Net.RestObjects
{
    public class OkexSwapBalances
    {
        [JsonProperty("info")]
        public IEnumerable<OkexSwapBalance> Balances { get; set; } = new List<OkexSwapBalance>();
    }

    public class OkexSwapBalanceOfSymbol
    {
        [JsonProperty("info")]
        public OkexSwapBalance Balance { get; set; } = new OkexSwapBalance();
    }

    public class OkexSwapBalanceExt : OkexSwapBalance
    {
        /// <summary>
        /// Available quantity
        /// </summary>
        [JsonProperty("available_qty")]
        public decimal AvailableSize { get; set; }

        /// <summary>
        /// Maximum Number of Long Positions Available
        /// </summary>
        [JsonProperty("long_open_max")]
        public int LongOpenMax { get; set; }

        /// <summary>
        /// Maximum Number of Short Positions Available
        /// </summary>
        [JsonProperty("short_open_max")]
        public int ShortOpenMax { get; set; }
    }

    public class OkexSwapBalance
    {
        /// <summary>
        /// Margin Mode: crossed / fixed
        /// </summary>
        [JsonProperty("margin_mode"), JsonConverter(typeof(SwapMarginModeConverter))]
        public OkexSwapMarginMode MarginMode { get; set; }

        /// <summary>
        /// Contract ID
        /// </summary>
        [JsonProperty("instrument_id")]
        public string Symbol { get; set; } = "";

        /// <summary>
        /// Creation time
        /// </summary>
        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Equity of the account
        /// </summary>
        [JsonProperty("equity")]
        public decimal Equity { get; set; }

        /// <summary>
        /// Balance of the account
        /// </summary>
        [JsonProperty("total_avail_balance")]
        public decimal TotalAvailableBalance { get; set; }

        /// <summary>
        /// Balance of fixed margin account
        /// </summary>
        [JsonProperty("fixed_balance")]
        public decimal FixedBalance { get; set; }

        /// <summary>
        /// Margin for open positions
        /// </summary>
        [JsonProperty("margin")]
        public decimal Margin { get; set; }

        /// <summary>
        /// Realized profits and losses
        /// </summary>
        [JsonProperty("realized_pnl")]
        public decimal RealizedPnl { get; set; }

        /// <summary>
        /// Unrealized profits and losses
        /// </summary>
        [JsonProperty("unrealized_pnl")]
        public decimal UnrealizedPnl { get; set; }

        /// <summary>
        /// Margin Ratio
        /// </summary>
        [JsonProperty("margin_ratio")]
        public decimal MarginRatio { get; set; }

        /// <summary>
        /// Initial margin on hold
        /// </summary>
        [JsonProperty("margin_frozen")]
        public decimal MarginFrozen { get; set; }

        /// <summary>
        /// Maintenance Margin Ratio
        /// </summary>
        [JsonProperty("maint_margin_ratio")]
        public decimal? MaintenanceMarginRatio { get; set; }

        /// <summary>
        /// Transferable amount
        /// </summary>
        [JsonProperty("max_withdraw")]
        public decimal MaxWithdraw { get; set; }
    }
}
