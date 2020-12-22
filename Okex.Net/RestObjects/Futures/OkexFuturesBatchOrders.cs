using CryptoExchange.Net.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Okex.Net.RestObjects
{
    public class OkexFuturesBatchOrders
    {
        /// <summary>
        /// The result of canceling the order
        /// </summary>
        [JsonProperty("result")]
        public bool result { get; set; }

        /// <summary>
        /// Error code; returns 0 when request is successful, otherwise corresponding error code will be returned
        /// </summary>
        [JsonProperty("error_code")]
        public string error_code { get; set; } = "";

        /// <summary>
        /// Error message; blank if request is successful, otherwise corresponding error message will be returned
        /// </summary>
        [JsonProperty("error_message")]
        public string error_message { get; set; } = "";

        /// <summary>
        /// The orders of the contract to be canceled e.g BTC-USD-180309,BTC-USDT-191227
        /// </summary>
        [JsonProperty("instrument_id")]
        public string Symbol { get; set; } = "";

        /// <summary>
        /// ID of the orders to be canceled
        /// </summary>
        [JsonProperty("order_ids"), JsonOptionalProperty]
        public IEnumerable<long> OrderIds { get; set; } = new List<long>();

        /// <summary>
        /// Client-supplied order ID
        /// </summary>
        [JsonProperty("client_oids"), JsonOptionalProperty]
        public IEnumerable<string> ClientOrderIds { get; set; } = new List<string>();
    }
}