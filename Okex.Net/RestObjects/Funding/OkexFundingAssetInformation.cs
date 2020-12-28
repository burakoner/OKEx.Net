using Newtonsoft.Json;

namespace Okex.Net.RestObjects
{
    public class OkexFundingAssetInformation
    {
        /// <summary>
        /// Token name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; } = "";

        /// <summary>
        /// Token symbol, e.g. 'BTC'
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; } = "";


        [JsonProperty("can_deposit")]
        private int can_deposit { get; set; }

        /// <summary>
        /// Availability to deposit, 0 = not available，1 = available
        /// </summary>
        [JsonIgnore]
        public bool CanDeposit => can_deposit == 1;

        [JsonProperty("can_withdraw")]
        private int can_withdraw { get; set; }

        /// <summary>
        /// Availability to withdraw, 0 = not available，1 = available
        /// </summary>
        [JsonIgnore]
        public bool CanWithdraw => can_withdraw == 1;

        /// <summary>
        /// Minimum withdrawal threshold
        /// </summary>
        [JsonProperty("min_withdrawal")]
        public decimal? MinWithdrawalAmount { get; set; }
    }
}
