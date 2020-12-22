using Newtonsoft.Json;

namespace Okex.Net.RestObjects
{
    public class OkexSpotBalance
    {
        /// <summary>
        /// Account ID
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; } = "";

        /// <summary>
        /// Token symbol
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; } = "";

        /// <summary>
        /// Available amount
        /// </summary>
        [JsonProperty("available")]
        public decimal Available { get; set; }

        /// <summary>
        /// Remaining balance
        /// </summary>
        [JsonProperty("balance")]
        public decimal Balance { get; set; }

        //<summary>
        //Available frozen
        //</summary>
        //[JsonProperty("frozen")]
        //public decimal Frozen { get; set; }

        /// <summary>
        /// Websockets uses "hold" name for Frozen amount
        /// </summary>
        [JsonProperty("hold")]
        public decimal Frozen { get; set; }

        // <summary>
        // Amount on hold (not available)
        // </summary>
        //[JsonProperty("holds")]
        //public decimal Holds { get; set; }
    }
}
