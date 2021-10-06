using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using Okex.Net.Converters;
using Okex.Net.Enums;
using System;
using System.Collections.Generic;

namespace Okex.Net.RestObjects.SubAccount
{
    public class OkexSubAccountBill
    {
        [JsonProperty("billId")]
        public long BillId { get; set; }

        [JsonProperty("ts"), JsonConverter(typeof(OkexTimestampConverter))]
        public DateTime Time { get; set; }
        
        [JsonProperty("type"), JsonConverter(typeof(SubAccountTransferTypeConverter))]
        public OkexSubAccountTransferType Type { get; set; }

        [JsonProperty("ccy")]
        public string Currency { get; set; }
        
        [JsonProperty("subAcct")]
        public string SubAccountName { get; set; }

        [JsonProperty("amt")]
        public decimal? Amount { get; set; }
    }
}
