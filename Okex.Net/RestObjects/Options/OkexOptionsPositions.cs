using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Okex.Net.RestObjects
{
    public class OkexOptionsPositions
    {
        [JsonProperty("error_code")]
        public string ErrorCode { get; set; } = "";

        [JsonProperty("error_message")]
        public string ErrorMessage { get; set; } = "";

        [JsonProperty("holding")]
        public List<OkexOptionsPosition> Holding { get; set; } = new List<OkexOptionsPosition>();
    }

    public class OkexOptionsPosition
    {
        /// <summary>
        /// Instrument ID, e.g. BTC-USD-190927-12500-C
        /// </summary>
        [JsonProperty("instrument_id")]
        public string Instrument { get; set; } = "";

        /// <summary>
        /// Quantity
        /// </summary>
        [JsonProperty("position")]
        public decimal Position { get; set; }

        /// <summary>
        /// Avaialble positions that can be closed
        /// </summary>
        [JsonProperty("avail_position")]
        public decimal AvailablePosition { get; set; }

        /// <summary>
        /// Average price for opening positions
        /// </summary>
        [JsonProperty("avg_cost")]
        public decimal AverageCost { get; set; }

        /// <summary>
        /// Settlement price
        /// </summary>
        [JsonProperty("settlement_price")]
        public decimal SettlementPrice { get; set; }

        /// <summary>
        /// Total profit and loss excluding fees, (markPx - avgCost) Pos multiplier
        /// </summary>
        [JsonProperty("total_pnl")]
        public decimal TotalPnl { get; set; }

        /// <summary>
        /// Profit ratio = sideFactor * (markPx/avgCost - 1)
        /// </summary>
        [JsonProperty("pnl_ratio")]
        public decimal PnlRatio { get; set; }

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
        /// Position margin（initial margin)
        /// </summary>
        [JsonProperty("pos_margin")]
        public decimal PositionMargin { get; set; }

        /// <summary>
        /// Option value
        /// </summary>
        [JsonProperty("option_value")]
        public decimal OptionValue { get; set; }

        /// <summary>
        /// Creation time
        /// </summary>
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Latest time position was adjusted
        /// </summary>
        [JsonProperty("updated_at")]
        public DateTime? UpdatedAt { get; set; }
    }
}
