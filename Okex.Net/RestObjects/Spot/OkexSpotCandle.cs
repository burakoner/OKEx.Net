using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Okex.Net.RestObjects
{
    [JsonConverter(typeof(ArrayConverter))]
    public class OkexSpotCandle
    {
        [JsonOptionalProperty]
        public string Symbol { get; set; } = "";

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
        public decimal Volume { get; set; }
    }
}
