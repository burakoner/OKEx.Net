using CryptoExchange.Net.Converters;
using Okex.Net.Enums;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    public class FuturesRemittingAccountTypeConverter : BaseConverter<OkexFuturesRemittingAccountType>
    {
        public FuturesRemittingAccountTypeConverter() : this(true) { }
        public FuturesRemittingAccountTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OkexFuturesRemittingAccountType, string>> Mapping => new List<KeyValuePair<OkexFuturesRemittingAccountType, string>>
        {
            new KeyValuePair<OkexFuturesRemittingAccountType, string>(OkexFuturesRemittingAccountType.Spot, "1"),
            new KeyValuePair<OkexFuturesRemittingAccountType, string>(OkexFuturesRemittingAccountType.Futures, "3"),
            new KeyValuePair<OkexFuturesRemittingAccountType, string>(OkexFuturesRemittingAccountType.C2C, "4"),
            new KeyValuePair<OkexFuturesRemittingAccountType, string>(OkexFuturesRemittingAccountType.Margin, "5"),
            new KeyValuePair<OkexFuturesRemittingAccountType, string>(OkexFuturesRemittingAccountType.FundingAccount, "6"),
            new KeyValuePair<OkexFuturesRemittingAccountType, string>(OkexFuturesRemittingAccountType.PiggyBank, "8"),
            new KeyValuePair<OkexFuturesRemittingAccountType, string>(OkexFuturesRemittingAccountType.Swap, "9"),
            new KeyValuePair<OkexFuturesRemittingAccountType, string>(OkexFuturesRemittingAccountType.Futures12, "12"),
            new KeyValuePair<OkexFuturesRemittingAccountType, string>(OkexFuturesRemittingAccountType.MiningAccount, "14"),
            new KeyValuePair<OkexFuturesRemittingAccountType, string>(OkexFuturesRemittingAccountType.LoansAccount, "17"),
        };
    }
}