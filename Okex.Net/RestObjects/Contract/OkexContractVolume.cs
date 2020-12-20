using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Okex.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Okex.Net.RestObjects
{
    [JsonConverter(typeof(ArrayConverter))]
    public class OkexContractVolume
    {
        /// <summary>
        /// Return the time corresponding to the data
        /// </summary>
        [ArrayProperty(0)]
        public DateTime Time { get; set; }

        /// <summary>
        /// Open Interest（USD）
        /// </summary>
        [ArrayProperty(1)]
        public decimal Interest { get; set; }

        /// <summary>
        /// Trading Volume（USD）
        /// </summary>
        [ArrayProperty(2)]
        public decimal Volume { get; set; }
    }
}
