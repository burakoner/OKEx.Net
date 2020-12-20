using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Okex.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Okex.Net.RestObjects
{
    [JsonConverter(typeof(ArrayConverter))]
    public class OkexContractTakerVolume
    {
        /// <summary>
        /// Return the time corresponding to the data
        /// </summary>
        [ArrayProperty(0)]
        public DateTime Time { get; set; }

        /// <summary>
        /// Buy Taker Volume（USD）
        /// </summary>
        [ArrayProperty(1)]
        public decimal BuyVolume { get; set; }

        /// <summary>
        /// Sell Taker Volume（USD）
        /// </summary>
        [ArrayProperty(2)]
        public decimal SellVolume { get; set; }
    }
}
