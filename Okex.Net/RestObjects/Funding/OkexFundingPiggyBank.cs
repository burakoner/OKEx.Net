using Newtonsoft.Json;
using Okex.Net.Converters;
using Okex.Net.Enums;

namespace Okex.Net.RestObjects
{
    public class OkexFundingPiggyBank
    {
        /// <summary>
        /// Request result. An error code will be displayed if it failed.
        /// </summary>
        [JsonProperty("result")]
        public bool Result { get; set; }

        /// <summary>
        /// Token symbol, e.g., 'BTC'
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; } = "";

        /// <summary>
        /// Purchase/Redempt amount
        /// </summary>
        [JsonProperty("amount")]
        public string Amount { get; set; } = "";

        /// <summary>
        /// Action Type
        /// </summary>
        [JsonProperty("side"), JsonConverter(typeof(FundingPiggyBankActionSideConverter))]
        public OkexFundingPiggyBankActionSide Side { get; set; }
    }
}
