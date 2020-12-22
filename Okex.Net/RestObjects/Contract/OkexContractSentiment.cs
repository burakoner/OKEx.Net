using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Okex.Net.RestObjects
{
    [JsonConverter(typeof(ArrayConverter))]
    public class OkexContractSentiment
    {
        /// <summary>
        /// Return the time corresponding to the data
        /// </summary>
        [ArrayProperty(0)]
        public DateTime Time { get; set; }

        /// <summary>
        /// Percentage of Long Position
        /// </summary>
        [ArrayProperty(1)]
        public decimal LongSentiment { get; set; }

        /// <summary>
        /// Percentage of Short Position
        /// </summary>
        [ArrayProperty(2)]
        public decimal ShortSentiment { get; set; }
    }
}
