using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Okex.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Okex.Net.Enums;

namespace Okex.Net.RestObjects
{
    public class OkexFuturesPositions
    {
        [JsonProperty("result")]
        public bool Result { get; set; }

        [JsonProperty("holding")]
        public List<List<OkexFuturesPosition>> Holding { get; set; } = new List<List<OkexFuturesPosition>>();
    }

    public class OkexFuturesPositionsOfContract
    {
        [JsonProperty("result")]
        public bool Result { get; set; }

        [JsonProperty("holding")]
        public List<OkexFuturesPosition> Holding { get; set; } = new List<OkexFuturesPosition>();
    }

    public class OkexFuturesPosition
    {
        /// <summary>
        /// Account Type
        /// </summary>
        [JsonProperty("margin_mode"), JsonConverter(typeof(FuturesMarginModeConverter))]
        public OkexFuturesMarginMode MarginMode { get; set; }

        /// <summary>
        /// Contract ID, e.g. BTC-USD-180213,BTC-USD-191227
        /// </summary>
        [JsonProperty("instrument_id")]
        public string Symbol { get; set; } = "";

        /// <summary>
        /// Leverage Ratio.
        /// !!! CROSS-MARGIN ONLY !!!
        /// </summary>
        [JsonProperty("leverage"), JsonOptionalProperty]
        public decimal Leverage { get; set; }

        /// <summary>
        /// Creation time
        /// </summary>
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Latest time margin was adjusted
        /// </summary>
        [JsonProperty("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Last traded price
        /// </summary>
        [JsonProperty("last")]
        public decimal LastPrice { get; set; }

        /// <summary>
        /// Estimated liquidation price.
        /// !!! CROSS-MARGIN ONLY !!!
        /// </summary>
        [JsonProperty("liquidation_price"), JsonOptionalProperty]
        public decimal LiquidationPrice { get; set; }

        /// <summary>
        /// Realized profits of long positions
        /// </summary>
        [JsonProperty("realised_pnl")]
        public decimal RealisedPnl { get; set; }

        /// <summary>
        /// Quantity of long positions
        /// </summary>
        [JsonProperty("long_qty")]
        public int LongQuantity { get; set; }

        /// <summary>
        /// Available long positions that can be closed
        /// </summary>
        [JsonProperty("long_avail_qty")]
        public decimal LongAvailableQuantity { get; set; }

        /// <summary>
        /// Average price for opening long positions
        /// </summary>
        [JsonProperty("long_avg_cost")]
        public decimal LongAverageCost { get; set; }

        /// <summary>
        /// Standard settlement price for long positions
        /// </summary>
        [JsonProperty("long_settlement_price")]
        public decimal LongSettlementPrice { get; set; }

        /// <summary>
        /// Margin for long positions
        /// </summary>
        [JsonProperty("long_margin")]
        public decimal LongMargin { get; set; }

        /// <summary>
        /// Profit of long positions
        /// </summary>
        [JsonProperty("long_pnl")]
        public decimal LongPnl { get; set; }

        /// <summary>
        /// Profit rate of long positions
        /// </summary>
        [JsonProperty("long_pnl_ratio")]
        public decimal LongPnlRatio { get; set; }

        /// <summary>
        /// Unrealized profits and losses of long positions
        /// </summary>
        [JsonProperty("long_unrealised_pnl")]
        public decimal LongUnrealisedPnl { get; set; }

        /// <summary>
        /// Realized Profit of Long Positions
        /// </summary>
        [JsonProperty("long_settled_pnl")]
        public decimal LongSettledPnl { get; set; }

        /// <summary>
        /// Leverage ratio for long pos.
        /// !!! FIXED-MARGIN ONLY !!!
        /// </summary>
        [JsonProperty("long_leverage"), JsonOptionalProperty]
        public decimal LongLeverage { get; set; }

        /// <summary>
        /// Liquidation price for long positions.
        /// !!! FIXED-MARGIN ONLY !!!
        /// </summary>
        [JsonProperty("long_liqui_price"), JsonOptionalProperty]
        public decimal LongLiquidationPrice { get; set; }

        /// <summary>
        /// Margin ratio of long positions.
        /// !!! FIXED-MARGIN ONLY !!!
        /// </summary>
        [JsonProperty("long_margin_ratio"), JsonOptionalProperty]
        public decimal LongMarginRatio { get; set; }

        /// <summary>
        /// Maintenance margin ratio of long positions.
        /// !!! FIXED-MARGIN ONLY !!!
        /// </summary>
        [JsonProperty("long_maint_margin_ratio"), JsonOptionalProperty]
        public decimal LongMaintenanceMarginRatio { get; set; }

        /// <summary>
        /// Quantity of short positions
        /// </summary>
        [JsonProperty("short_qty")]
        public int ShortQuantity { get; set; }

        /// <summary>
        /// Available short positions that can be closed
        /// </summary>
        [JsonProperty("short_avail_qty")]
        public decimal ShortAvailableQuantity { get; set; }

        /// <summary>
        /// Average price for opening short positions
        /// </summary>
        [JsonProperty("short_avg_cost")]
        public decimal ShortAverageCost { get; set; }

        /// <summary>
        /// Standard settlement price for short positions
        /// </summary>
        [JsonProperty("short_settlement_price")]
        public decimal ShortSettlementPrice { get; set; }

        /// <summary>
        /// Margin for short positions
        /// </summary>
        [JsonProperty("short_margin")]
        public decimal ShortMargin { get; set; }

        /// <summary>
        /// Profit of short positions
        /// </summary>
        [JsonProperty("short_pnl")]
        public decimal ShortPnl { get; set; }

        /// <summary>
        /// Profit rate of short positions
        /// </summary>
        [JsonProperty("short_pnl_ratio")]
        public decimal ShortPnlRatio { get; set; }

        /// <summary>
        /// Unrealized profits and losses of short positions
        /// </summary>
        [JsonProperty("short_unrealised_pnl")]
        public decimal ShortUnrealisedPnl { get; set; }

        /// <summary>
        /// Realized Profit of Short Positions
        /// </summary>
        [JsonProperty("short_settled_pnl")]
        public decimal ShortSettledPnl { get; set; }

        /// <summary>
        /// Leverage ratio for short pos.
        /// !!! FIXED-MARGIN ONLY !!!
        /// </summary>
        [JsonProperty("short_leverage"), JsonOptionalProperty]
        public decimal ShortLeverage { get; set; }

        /// <summary>
        /// Liquidation price for short positions.
        /// !!! FIXED-MARGIN ONLY !!!
        /// </summary>
        [JsonProperty("short_liqui_price"), JsonOptionalProperty]
        public decimal ShortLiquidationPrice { get; set; }

        /// <summary>
        /// Margin ratio of short positions.
        /// !!! FIXED-MARGIN ONLY !!!
        /// </summary>
        [JsonProperty("short_margin_ratio"), JsonOptionalProperty]
        public decimal ShortMarginRatio { get; set; }

        /// <summary>
        /// Maintenance margin ratio of short positions.
        /// !!! FIXED-MARGIN ONLY !!!
        /// </summary>
        [JsonProperty("short_maint_margin_ratio"), JsonOptionalProperty]
        public decimal ShortMaintenanceMarginRatio { get; set; }

    }
}
