using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Okex.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Okex.Net.RestObjects
{
    [JsonConverter(typeof(ArrayConverter))]
    public class OkexContractMargin
    {
        /// <summary>
        /// Return the time corresponding to the data
        /// </summary>
        [ArrayProperty(0)]
        public DateTime Time { get; set; }

        /// <summary>
        /// Long Position
        /// </summary>
        [ArrayProperty(1)]
        public decimal LongMarginRatio { get; set; }

        /// <summary>
        /// Short Position
        /// </summary>
        [ArrayProperty(2)]
        public decimal ShortMarginRatio { get; set; }
    }
}
