using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Okex.Net.Converters;
using Newtonsoft.Json;
using System;
using Okex.Net.Enums;

namespace Okex.Net.RestObjects
{
    public class OkexOptionsInstrument
    {
        /// <summary>
        /// Instrument ID, e.g. BTC-USD-190830-9000-C
        /// </summary>
        [JsonProperty("instrument_id")]
        public string Instrument { get; set; } = "";

        /// <summary>
        /// The underlying index that the contract is based upon, e.g. BTC-USD.
        /// </summary>
        [JsonProperty("underlying")]
        public string Underlying { get; set; } = "";

        /// <summary>
        /// Settlement and margin currency, e.g. BTC
        /// </summary>
        [JsonProperty("settlement_currency")]
        public string SettlementCurrency { get; set; } = "";

        /// <summary>
        /// Fee Schedule Tie 1：Tier 1
        /// </summary>
        [JsonProperty("category")]
        public int Category { get; set; }

        /// <summary>
        /// Contract multiplier. (It is 0.1 for BTC option contract)
        /// </summary>
        [JsonProperty("contract_val")]
        public decimal ContractValue { get; set; }

        /// <summary>
        /// Expiry time, in ISO 8601 format
        /// </summary>
        [JsonProperty("delivery")]
        public DateTime Delivery { get; set; }

        /// <summary>
        /// Listing time, in ISO 8601 format
        /// </summary>
        [JsonProperty("listing")]
        public DateTime Listing { get; set; }

        /// <summary>
        /// Lot size
        /// </summary>
        [JsonProperty("lot_size")]
        public decimal LotSize { get; set; }

        /// <summary>
        /// Option type: C or P
        /// </summary>
        [JsonProperty("option_type"), JsonConverter(typeof(OptionsTypeConverter))]
        public OkexOptionsType Type { get; set; }

        /// <summary>
        /// Contract status: 1:PreOpen, 2:Live, 3:Suspended, 4:Settlement
        /// </summary>
        [JsonProperty("state"), JsonConverter(typeof(OptionsStateConverter))]
        public OkexOptionsState State { get; set; }

        /// <summary>
        /// Strike price
        /// </summary>
        [JsonProperty("strike")]
        public decimal Strike { get; set; }

        /// <summary>
        /// Tick size（e.g. 0.0005）
        /// </summary>
        [JsonProperty("tick_size")]
        public decimal TickSize { get; set; }

        /// <summary>
        /// Latest time the contract state was updated, in ISO 8601 format
        /// </summary>
        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Time when trading opened, in ISO 8601 format
        /// </summary>
        [JsonProperty("trading_start_time")]
        public DateTime TradingStartTime { get; set; }

    }
}
