using CryptoExchange.Net.Converters;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    internal class FundinWithdrawalDestinationConverter : BaseConverter<OkexFundinWithdrawalDestination>
    {
        public FundinWithdrawalDestinationConverter() : this(true) { }
        public FundinWithdrawalDestinationConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OkexFundinWithdrawalDestination, string>> Mapping => new List<KeyValuePair<OkexFundinWithdrawalDestination, string>>
        {
            new KeyValuePair<OkexFundinWithdrawalDestination, string>(OkexFundinWithdrawalDestination.OKEx, "3"),
            new KeyValuePair<OkexFundinWithdrawalDestination, string>(OkexFundinWithdrawalDestination.CoinAll, "68"),
            new KeyValuePair<OkexFundinWithdrawalDestination, string>(OkexFundinWithdrawalDestination.Others, "4"),
        };
    }
}