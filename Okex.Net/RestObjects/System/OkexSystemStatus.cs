using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Okex.Net.Converters;
using Okex.Net.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Okex.Net.RestObjects
{
    public class OkexSystemStatus
    {
        /// <summary>
        /// The title of system maintenance instructions
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; } = "";

        /// <summary>
        /// Hyperlink for system maintenance details, if there is no return value, the default value will be empty. e.g. href: “”
        /// </summary>
        [JsonProperty("href")]
        public string Link { get; set; } = "";

        /// <summary>
        /// Start time of system maintenance in ISO 8601 standard format e.g. 2020-04-03T16:30:00.000Z
        /// </summary>
        [JsonProperty("start_time")]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// End time of system maintenance in ISO 8601 standard format e.g. 2020-04-03T17:40:00.000Z
        /// </summary>
        [JsonProperty("end_time")]
        public DateTime EndTime { get; set; }

        /// <summary>
        /// Product type 0 :WebSocket ; 1:Spot/Margin ; 2:Futures ; 3:Perpetual ; 4:Options
        /// </summary>
        [JsonProperty("product_type"), JsonConverter(typeof(SystemMaintenanceProductConverter))]
        public OkexSystemMaintenanceProduct Product { get; set; }

        /// <summary>
        /// System maintenance status 0: waiting; 1: processing; 2: completed
        /// </summary>
        [JsonProperty("status"), JsonConverter(typeof(SystemMaintenanceStatusConverter))]
        public OkexSystemMaintenanceStatus Status { get; set; }

    }
}
