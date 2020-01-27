using CryptoExchange.Net.Converters;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    internal class FundinWithdrawalDestinationConverter : BaseConverter<FundinWithdrawalDestination>
    {
        public FundinWithdrawalDestinationConverter() : this(true) { }
        public FundinWithdrawalDestinationConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<FundinWithdrawalDestination, string>> Mapping => new List<KeyValuePair<FundinWithdrawalDestination, string>>
        {
            new KeyValuePair<FundinWithdrawalDestination, string>(FundinWithdrawalDestination.OKEx, "3"),
            new KeyValuePair<FundinWithdrawalDestination, string>(FundinWithdrawalDestination.CoinAll, "68"),
            new KeyValuePair<FundinWithdrawalDestination, string>(FundinWithdrawalDestination.Others, "4"),
        };
    }
}