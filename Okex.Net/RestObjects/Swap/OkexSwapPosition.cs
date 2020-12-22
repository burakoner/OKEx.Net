using Newtonsoft.Json;
using Okex.Net.Converters;
using Okex.Net.Enums;
using System;
using System.Collections.Generic;

namespace Okex.Net.RestObjects
{
    public class OkexSwapPosition
    {
        /// <summary>
        /// Margin-mode
        /// </summary>
        [JsonProperty("margin_mode"), JsonConverter(typeof(SwapMarginModeConverter))]
        public OkexSwapMarginMode MarginMode { get; set; }

        /// <summary>
        /// Creation time
        /// </summary>
        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }

        [JsonProperty("holding")]
        public List<OkexSwapPositionHolding> Holding { get; set; } = new List<OkexSwapPositionHolding>();
    }

    public class OkexSwapPositionHolding
    {
        #region Cross-Margin All Fields
        /// <summary>
        /// Contract ID
        /// </summary>
        [JsonProperty("instrument_id")]
        public string Symbol { get; set; } = "";

        /// <summary>
        /// Leverage level
        /// </summary>
        [JsonProperty("leverage")]
        public decimal Leverage { get; set; }

        /// <summary>
        /// Creation time
        /// </summary>
        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Last traded price
        /// </summary>
        [JsonProperty("last")]
        public decimal LastPrice { get; set; }

        /// <summary>
        /// Estimated liquidation price
        /// </summary>
        [JsonProperty("liquidation_price")]
        public decimal LiquidationPrice { get; set; }

        /// <summary>
        /// Positions open
        /// </summary>
        [JsonProperty("position")]
        public int PositionCount { get; set; }

        /// <summary>
        /// Positions available to be closed
        /// </summary>
        [JsonProperty("avail_position")]
        public int AvailablePositionCount { get; set; }

        /// <summary>
        /// Margin
        /// </summary>
        [JsonProperty("margin")]
        public decimal Margin { get; set; }

        /// <summary>
        /// Avg. position price
        /// </summary>
        [JsonProperty("avg_cost")]
        public decimal AverageCost { get; set; }

        /// <summary>
        /// Maintenance Margin Ratio
        /// </summary>
        [JsonProperty("maint_margin_ratio")]
        public decimal MaintenanceMarginRatio { get; set; }

        /// <summary>
        /// Realized Profit of Positions
        /// </summary>
        [JsonProperty("settled_pnl")]
        public decimal RealizedProfitOfPositions { get; set; }

        /// <summary>
        /// Settlement price
        /// </summary>
        [JsonProperty("settlement_price")]
        public decimal SettlementPrice { get; set; }

        /// <summary>
        /// Side
        /// </summary>
        [JsonProperty("side"), JsonConverter(typeof(SwapOrderSideConverter))]
        public OkexSwapOrderSide Side { get; set; }

        /// <summary>
        /// Realized Profit and loss
        /// </summary>
        [JsonProperty("realized_pnl")]
        public decimal RealizedPnl { get; set; }

        /// <summary>
        /// unrealized profit and loss
        /// </summary>
        [JsonProperty("unrealized_pnl")]
        public decimal UnrealizedPnl { get; set; }
        #endregion

        #region Fixed-Margin Extra Fields
        // None
        #endregion
    }
}
