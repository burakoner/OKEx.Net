using CryptoExchange.Net.Converters;
using Okex.Net.Enums;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    internal class OptionsReceivingAccountTypeConverter : BaseConverter<OkexOptionsReceivingAccountType>
    {
        public OptionsReceivingAccountTypeConverter() : this(true) { }
        public OptionsReceivingAccountTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OkexOptionsReceivingAccountType, string>> Mapping => new List<KeyValuePair<OkexOptionsReceivingAccountType, string>>
        {
            new KeyValuePair<OkexOptionsReceivingAccountType, string>(OkexOptionsReceivingAccountType.Spot, "1"),
            new KeyValuePair<OkexOptionsReceivingAccountType, string>(OkexOptionsReceivingAccountType.Futures, "3"),
            new KeyValuePair<OkexOptionsReceivingAccountType, string>(OkexOptionsReceivingAccountType.C2C, "4"),
            new KeyValuePair<OkexOptionsReceivingAccountType, string>(OkexOptionsReceivingAccountType.Margin, "5"),
            new KeyValuePair<OkexOptionsReceivingAccountType, string>(OkexOptionsReceivingAccountType.FundingAccount, "6"),
            new KeyValuePair<OkexOptionsReceivingAccountType, string>(OkexOptionsReceivingAccountType.PiggyBank, "8"),
            new KeyValuePair<OkexOptionsReceivingAccountType, string>(OkexOptionsReceivingAccountType.Swap, "9"),
            new KeyValuePair<OkexOptionsReceivingAccountType, string>(OkexOptionsReceivingAccountType.Options, "12"),
            new KeyValuePair<OkexOptionsReceivingAccountType, string>(OkexOptionsReceivingAccountType.MiningAccount, "14"),
            new KeyValuePair<OkexOptionsReceivingAccountType, string>(OkexOptionsReceivingAccountType.LoansAccount, "17"),
        };
    }
}