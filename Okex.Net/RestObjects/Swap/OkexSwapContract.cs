using Newtonsoft.Json;

namespace Okex.Net.RestObjects
{
    public class OkexSwapContract
    {
        /// <summary>
        /// Contract ID, e.g. BTC-USD-SWAP,,BTC-USDT-SWAP
        /// </summary>
        [JsonProperty("instrument_id")]
        public string Symbol { get; set; } = "";

        /// <summary>
        /// Underlying index，eg：BTC-USD ,BTC-USDT
        /// </summary>
        [JsonProperty("underlying")]
        public string Underlying { get; set; } = "";

        /// <summary>
        /// currency of Underlying index, eg：BTC
        /// </summary>
        [JsonProperty("underlying_index")]
        public string UnderlyingIndex { get; set; } = "";

        /// <summary>
        /// Transaction currency，eg:BTC
        /// </summary>
        [JsonProperty("base_currency")]
        public string BaseCurrency { get; set; } = "";

        /// <summary>
        /// Quote currency, such as the USD in BTC-USD-SWAP
        /// </summary>
        [JsonProperty("quote_currency")]
        public string QuoteCurrency { get; set; } = "";

        /// <summary>
        /// Settlement currency，eg:BTC
        /// </summary>
        [JsonProperty("settlement_currency")]
        public string SettlementCurrency { get; set; } = "";

        /// <summary>
        /// Contract value
        /// </summary>
        [JsonProperty("contract_val")]
        public decimal ContractVal { get; set; }

        /// <summary>
        /// Contract denomination currency eg: USD，BTC，LTC
        /// </summary>
        [JsonProperty("contract_val_currency")]
        public string ContractValCurrency { get; set; } = "";

        /// <summary>
        /// Online date
        /// </summary>
        [JsonProperty("listing")]
        public string Listing { get; set; } = "";

        /// <summary>
        /// Settlement time
        /// </summary>
        [JsonProperty("delivery")]
        public string Delivery { get; set; } = "";

        /// <summary>
        /// Order price accuracy
        /// </summary>
        [JsonProperty("tick_size")]
        public decimal TickSize { get; set; }

        /// <summary>
        /// Order quantity accuracy
        /// </summary>
        [JsonProperty("size_increment")]
        public decimal SizeIncrement { get; set; }

        /// <summary>
        /// Fee Schedule Tie 1：Tier 1，2：Tier 2
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
