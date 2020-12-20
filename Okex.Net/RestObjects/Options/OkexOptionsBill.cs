using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Okex.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Okex.Net.Enums;

namespace Okex.Net.RestObjects
{
    public class OkexOptionsBill
    {
        [JsonProperty("ledger_id")]
        public long LedgerId { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; } = "";
        
        [JsonProperty("amount")]
        public decimal Amount { get; set; }
        
        [JsonProperty("realized_pnl")]
        public decimal RealizedPnl { get; set; }

        /// <summary>
        /// Transaction type: transfer, match, fee, settlement, liquidation
        /// </summary>
        [JsonProperty("type"), JsonConverter(typeof(OptionsBillTypeConverter))]
        public OkexOptionsBillType Type { get; set; }

        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }

        [JsonProperty("balance")]
        public decimal Balance { get; set; }

        [JsonProperty("order_id")]
        public long OrderId { get; set; }

        [JsonProperty("instrument_id")]
        public string Instrument { get; set; } = "";

        [JsonProperty("details")]
        public OkexOptionsBillDetails Details { get; set; } = new OkexOptionsBillDetails();

        [JsonProperty("from"), JsonConverter(typeof(OptionsRemittingAccountTypeConverter))]
        public OkexOptionsRemittingAccountType RemittingAccount { get; set; }

        [JsonProperty("to"), JsonConverter(typeof(OptionsReceivingAccountTypeConverter))]
        public OkexOptionsReceivingAccountType ReceivingAccount { get; set; } 
    }

    public class OkexOptionsBillDetails
    {
        [JsonProperty("order_id")]
        public long? OrderId { get; set; }

        [JsonProperty("instrument_id")]
        public string Instrument { get; set; } = "";

    }
}
