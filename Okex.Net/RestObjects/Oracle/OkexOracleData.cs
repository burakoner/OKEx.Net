using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using Okex.Net.Attributes;
using System;
using System.Collections.Generic;

namespace Okex.Net.RestObjects
{
    [JsonConverter(typeof(TypedDataConverter<OkexOracleData>))]
    public class OkexOracleData
    {
        /// <summary>
        /// time of latest datapoint
        /// </summary>
        [JsonProperty("timestamp"), JsonConverter(typeof(TimestampSecondsConverter))]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// abi-encoded values [kind, timestamp, key, value], where kind equals 'prices', timestamp is the time when price was obtained, key is the asset ticker (e.g. btc) and value is the asset price.
        /// </summary>
        [JsonProperty("messages")]
        public IEnumerable<string> Messages { get; set; } = new List<string>();

        /// <summary>
        /// readable asset prices
        /// </summary>
        [TypedData]
        [JsonProperty("prices")]
        public Dictionary<string, decimal> Prices { get; set; } = new Dictionary<string, decimal>();

        /// <summary>
        /// Ethereum-compatible ECDSA signatures for each message
        /// </summary>
        [JsonProperty("signatures")]
        public IEnumerable<string> Signatures { get; set; } = new List<string>();
    }
}
