using Newtonsoft.Json;
using Okex.Net.Converters;
using Okex.Net.Enums;

namespace Okex.Net.RestObjects
{
    public class OkexOptionsPlaceOrder
    {
        [JsonProperty("instrument_id", NullValueHandling = NullValueHandling.Ignore)]
        public string Instrument { get; set; } = "";

        [JsonProperty("size", NullValueHandling = NullValueHandling.Ignore)]
        public decimal Size { get; set; }

        [JsonProperty("price", NullValueHandling = NullValueHandling.Ignore)]
        public decimal Price { get; set; }

        [JsonProperty("side"), JsonConverter(typeof(OptionsOrderSideConverter))]
        public OkexOptionsOrderSide Side { get; set; }

        [JsonProperty("order_type"), JsonConverter(typeof(OptionsTimeInForceConverter))]
        public OkexOptionsTimeInForce TimeInForce { get; set; } = OkexOptionsTimeInForce.NormalOrder;

        [JsonProperty("match_price")]
        public bool MatchPrice { get; set; }

        [JsonProperty("client_oid")]
        public string ClientOrderId { get; set; } = "";
    }
}
