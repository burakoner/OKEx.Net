using Newtonsoft.Json;

namespace Okex.Net.RestObjects
{
    public class OkexFuturesContract
    {
        /// <summary>
        /// Contract ID, e.g. BTC-USD-180213,BTC-USDT-191227
        /// </summary>
        [JsonProperty("instrument_id")]
        public string Symbol { get; set; } = "";

        /// <summary>
        /// Underlying index，eg：BTC-USD
        /// </summary>
        [JsonProperty("underlying")]
        public string Underlying { get; set; } = "";

        /// <summary>
        /// currency of Underlying index, eg：BTC
        /// </summary>
        [JsonProperty("underlying_index")]
        public string UnderlyingIndex { get; set; } = "";

        /// <summary>
        /// Transaction currency，eg:BTC in BTC-USD,BTC in BTC-USDT
        /// </summary>
        [JsonProperty("base_currency")]
        public string BaseCurrency { get; set; } = "";

        /// <summary>
        /// Quote currency，such as USD in BTC-USD
        /// </summary>
        [JsonProperty("quote_currency")]
        public string QuoteCurrency { get; set; } = "";

        /// <summary>
        /// Settlement currency，eg:BTC
        /// </summary>
        [JsonProperty("settlement_currency")]
        public string SettlementCurrency { get; set; } = "";

        /// <summary>
        /// Settlement currency，eg:BTC
        /// </summary>
        [JsonProperty("contract_val")]
        public decimal ContractVal { get; set; }

        /// <summary>
        /// Contract denomination currency eg: USD，BTC，LTC，ETC , XRP, EOS
        /// </summary>
        [JsonProperty("contract_val_currency")]
        public string ContractValCurrency { get; set; } = "";

        /// <summary>
        /// Online date
        /// </summary>
        [JsonProperty("listing")]
        public string Listing { get; set; } = "";

        /// <summary>
        /// Delivery date
        /// </summary>
        [JsonProperty("delivery")]
        public string Delivery { get; set; } = "";

        /// <summary>
        /// this_week next_week quarter bi_quarter
        /// </summary>
        [JsonProperty("alias")]
        public string Alias { get; set; } = "";

        /// <summary>
        /// Order price accuracy
        /// </summary>
        [JsonProperty("tick_size")]
        public decimal TickSize { get; set; }

        /// <summary>
        /// Order quantity accuracy
        /// </summary>
        [JsonProperty("trade_increment")]
        public decimal TradeIncrement { get; set; }

        /// <summary>
        /// Fee Schedule Tie 1：Tier 1，2：Tier 2 ；
        /// </summary>
        [JsonProperty("category")]
        public int Category { get; set; }

        /// <summary>
        /// (true or false) ,inverse contract or not
        /// </summary>
        [JsonProperty("is_inverse")]
        public bool IsInverse { get; set; } 
    }
}
