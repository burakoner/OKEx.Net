using Newtonsoft.Json;
using System.Collections.Generic;

namespace Okex.Net.RestObjects
{
    public class OkexSpotCancelOrder
    {
        /// <summary>
        /// by providing this parameter, the corresponding order of a designated trading pair will be cancelled. If not providing this parameter, it will be back to error code.
        /// </summary>
        [JsonProperty("instrument_id")]
        public string Symbol { get; set; } = "";

        /// <summary>
        /// Either client_oids or order_ids must be present. Order ID
        /// </summary>
        [JsonProperty("order_ids", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<long> OrderIds { get; set; } = new List<long>();

        /// <summary>
        /// Either client_oids or order_ids must be present. Client-supplied order ID that you can customize. It should be comprised of alpha-numeric characters with length 1 to 32. Both uppercase and lowercase are supported
        /// </summary>
        [JsonProperty("client_oids", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<string> ClientOrderIds { get; set; } = new List<string>();
    }
}
