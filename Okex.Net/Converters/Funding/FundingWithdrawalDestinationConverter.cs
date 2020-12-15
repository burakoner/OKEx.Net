using CryptoExchange.Net.Converters;
using Okex.Net.Enums;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    internal class FundingWithdrawalDestinationConverter : BaseConverter<OkexFundingWithdrawalDestination>
    {
        public FundingWithdrawalDestinationConverter() : this(true) { }
        public FundingWithdrawalDestinationConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OkexFundingWithdrawalDestination, string>> Mapping => new List<KeyValuePair<OkexFundingWithdrawalDestination, string>>
        {
            new KeyValuePair<OkexFundingWithdrawalDestination, string>(OkexFundingWithdrawalDestination.OKEx, "3"),
            new KeyValuePair<OkexFundingWithdrawalDestination, string>(OkexFundingWithdrawalDestination.CoinAll, "68"),
            new KeyValuePair<OkexFundingWithdrawalDestination, string>(OkexFundingWithdrawalDestination.Others, "4"),
        };
    }
}