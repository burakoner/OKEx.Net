using Newtonsoft.Json;
using Okex.Net.Converters;
using Okex.Net.Enums;
using System;

namespace Okex.Net.RestObjects
{
    public class OkexSpotAlgoOrder
    {
        #region Common Fields
        /// <summary>
        /// Trading pair symbol
        /// </summary>
        [JsonProperty("instrument_id")]
        public string Symbol { get; set; } = "";

        /// <summary>
        /// Order ID
        /// </summary>
        [JsonProperty("algo_id")]
        public long AlgoId { get; set; }

        /// <summary>
        /// Order ID
        /// </summary>
        [JsonProperty("order_id")]
        public long? OrderId { get; set; }

        /// <summary>
        /// Order time
        /// </summary>
        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Order expiration time
        /// </summary>
        [JsonProperty("rejected_at")]
        public DateTime? RejectedAt { get; set; }

        /// <summary>
        /// 1：Spot; 2：Margin
        /// </summary>
        [JsonProperty("mode"), JsonConverter(typeof(MarketConverter))]
        public OkexMarket Mode { get; set; }

        /// <summary>
        /// Buy or Sell
        /// </summary>
        [JsonProperty("side"), JsonConverter(typeof(SpotOrderSideConverter))]
        public OkexSpotOrderSide Side { get; set; }

        /// <summary>
        /// Order status: 1. Pending; 2. Effective; 3. Canceled (4. Partially effective; 5. Paused)
        /// </summary>
        [JsonProperty("status"), JsonConverter(typeof(AlgoStatusConverter))]
        public OkexAlgoStatus Status { get; set; }

        /// <summary>
        /// 1. trigger order; 2. trail order; 3. iceberg order; 4. time-weighted average price (TWAP); 5. stop order
        /// </summary>
        [JsonProperty("order_type"), JsonConverter(typeof(AlgoOrderTypeConverter))]
        public OkexAlgoOrderType Type { get; set; }

        /// <summary>
        /// Order amount must be an integer between 1 and 1,000,000, incl. 1,000,000
        /// </summary>
        [JsonProperty("size")]
        public decimal? Size { get; set; }

        /// <summary>
        /// Algo order price
        /// </summary>
        [JsonProperty("algo_price")]
        public decimal? AlgoPrice { get; set; }

        /// <summary>
        /// Algo order trigger price
        /// </summary>
        [JsonProperty("trigger_price")]
        public decimal? TriggerPrice { get; set; }

        #endregion

        #region Trigger Orders Fields
        /*
        /// <summary>
        /// Trigger price must be greater than 0
        /// </summary>
        [JsonProperty("trigger_price")]
        public decimal? TriggerPrice { get; set; }

        /// <summary>
        /// Algo price must be between 0 and 1,000,000 incl. 1,000,000
        /// </summary>
        [JsonProperty("algo_price")]
        public decimal? AlgoPrice { get; set; }
        */

        /// <summary>
        /// 1: Limit 2: Market ; trigger price type, default is limit price; when it is the market price, the commission price need not be filled;
        /// </summary>
        [JsonProperty("algo_type"), JsonConverter(typeof(AlgoPriceTypeConverter))]
        public OkexAlgoPriceType AlgoPriceType { get; set; }
        #endregion

        #region Trail Orders Fields
        /// <summary>
        /// Callback rate must be between 0.001 (0.1%) and 0.05 (5%), incl. both numbers
        /// </summary>
        [JsonProperty("callback_rate")]
        public decimal? CallbackRate { get; set; }

        /*
        /// <summary>
        /// Trigger price must be between 0 and 1,000,000, incl. 1,000,000
        /// </summary>
        [JsonProperty("trigger_price")]
        public decimal? TriggerPrice { get; set; }
        */
        #endregion

        #region Iceberg Orders Fields
        /// <summary>
        /// Order depth must be between 0 and 0.01 (1%), incl. 0.01
        /// </summary>
        [JsonProperty("algo_variance")]
        public decimal? AlgoVariance { get; set; }

        /// <summary>
        /// Average amount must be an integer between 2 and 500 (same for perpetual swap), incl. both numbers
        /// </summary>
        [JsonProperty("avg_amount")]
        public decimal? AvgAmount { get; set; }

        /// <summary>
        /// Price limit must be between 0 and 1,000,000, incl. 1,000,000
        /// </summary>
        [JsonProperty("limit_price")]
        public decimal? LimitPrice { get; set; }

        /// <summary>
        /// Transacted volume
        /// </summary>
        [JsonProperty("deal_value")]
        public decimal? DealValue { get; set; }
        #endregion

        #region Time-weighted Average Price (TWAP) Orders Fields
        /// <summary>
        /// Auto-cancelling order range must be between 0.005 (0.5%) and 0.01 (1%), incl. both numbers
        /// </summary>
        [JsonProperty("sweep_range")]
        public decimal? sweep_range { get; set; }

        /// <summary>
        /// Auto-cancelling order rate must be between 0.01 and 1, incl. both numbers
        /// </summary>
        [JsonProperty("sweep_ratio")]
        public decimal? sweep_ratio { get; set; }

        /// <summary>
        /// Single order limit must be between 10 and 2,000 (perpetual swap orders: integer between 2 and 500), incl. both numbers
        /// </summary>
        [JsonProperty("single_limit")]
        public int? single_limit { get; set; }

        /*
        /// <summary>
        /// Price limit must be between 0 and 1,000,000, incl. 1,000,000
        /// </summary>
        [JsonProperty("limit_price")]
        public decimal? LimitPrice { get; set; }
        */

        /// <summary>
        /// Order time interval must be between 5 and 120, incl. both numbers
        /// </summary>
        [JsonProperty("time_interval")]
        public int? time_interval { get; set; }

        /*
        /// <summary>
        /// Order volume
        /// </summary>
        [JsonProperty("deal_value")]
        public decimal? DealValue { get; set; }
        */
        #endregion

    }
}
