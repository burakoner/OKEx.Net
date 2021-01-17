using CryptoExchange.Net.Converters;
using Okex.Net.Enums;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    public class FuturesReceivingAccountTypeConverter : BaseConverter<OkexFuturesReceivingAccountType>
    {
        public FuturesReceivingAccountTypeConverter() : this(true) { }
        public FuturesReceivingAccountTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OkexFuturesReceivingAccountType, string>> Mapping => new List<KeyValuePair<OkexFuturesReceivingAccountType, string>>
        {
            new KeyValuePair<OkexFuturesReceivingAccountType, string>(OkexFuturesReceivingAccountType.Spot, "1"),
            new KeyValuePair<OkexFuturesReceivingAccountType, string>(OkexFuturesReceivingAccountType.Futures, "3"),
            new KeyValuePair<OkexFuturesReceivingAccountType, string>(OkexFuturesReceivingAccountType.C2C, "4"),
            new KeyValuePair<OkexFuturesReceivingAccountType, string>(OkexFuturesReceivingAccountType.Margin, "5"),
            new KeyValuePair<OkexFuturesReceivingAccountType, string>(OkexFuturesReceivingAccountType.FundingAccount, "6"),
            new KeyValuePair<OkexFuturesReceivingAccountType, string>(OkexFuturesReceivingAccountType.PiggyBank, "8"),
            new KeyValuePair<OkexFuturesReceivingAccountType, string>(OkexFuturesReceivingAccountType.Swap, "9"),
            new KeyValuePair<OkexFuturesReceivingAccountType, string>(OkexFuturesReceivingAccountType.Option, "12"),
            new KeyValuePair<OkexFuturesReceivingAccountType, string>(OkexFuturesReceivingAccountType.MiningAccount, "14"),
            new KeyValuePair<OkexFuturesReceivingAccountType, string>(OkexFuturesReceivingAccountType.LoansAccount, "17"),
        };
    }
}