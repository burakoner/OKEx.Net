using Newtonsoft.Json;
using Okex.Net.Converters;
using Okex.Net.Enums;

namespace Okex.Net.RestObjects
{
    public class OkexFuturesPlaceOrder
    {
        [JsonProperty("type"), JsonConverter(typeof(FuturesOrderTypeConverter))]
        public OkexFuturesOrderType Type { get; set; }

        [JsonProperty("size", NullValueHandling = NullValueHandling.Ignore)]
        public decimal Size { get; set; }

        [JsonProperty("order_type"), JsonConverter(typeof(FuturesTimeInForceConverter))]
        public OkexFuturesTimeInForce TimeInForce { get; set; } = OkexFuturesTimeInForce.NormalOrder;

        [JsonProperty("price", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Price { get; set; }
        
        [JsonProperty("match_price")]
        public bool MatchPrice { get; set; }

        [JsonProperty("client_oid")]
        public string ClientOrderId { get; set; } = "";
    }
}
