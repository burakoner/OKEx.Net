using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Okex.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Okex.Net.RestObjects
{
    [JsonConverter(typeof(ArrayConverter))]
    public class OkexOptionsCandle
    {
        /// <summary>
        /// Start time
        /// </summary>
        [ArrayProperty(0)]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Open price
        /// </summary>
        [ArrayProperty(1)]
        public decimal Open { get; set; }

        /// <summary>
        /// Highest price
        /// </summary>
        [ArrayProperty(2)]
        public decimal High { get; set; }

        /// <summary>
        /// Lowest price
        /// </summary>
        [ArrayProperty(3)]
        public decimal Low { get; set; }

        /// <summary>
        /// Close price
        /// </summary>
        [ArrayProperty(4)]
        public decimal Close { get; set; }

        /// <summary>
        /// Trading volume
        /// </summary>
        [ArrayProperty(5)]
        public decimal QuoteVolume { get; set; }

        /// <summary>
        /// Volume of a specific token
        /// </summary>
        [ArrayProperty(6)]
        public decimal BaseVolume { get; set; }
    }
}
