using Newtonsoft.Json;
using Okex.Net.Converters;
using System;

namespace Okex.Net.RestObjects.Public
{
    public class OkexTime
    {
        /// <summary>
        /// System time, Unix timestamp format in milliseconds, e.g. 1597026383085
        /// </summary>
        [JsonProperty("ts"), JsonConverter(typeof(OkexTimestampConverter))]
        public DateTime Time { get; set; }
    }
}
