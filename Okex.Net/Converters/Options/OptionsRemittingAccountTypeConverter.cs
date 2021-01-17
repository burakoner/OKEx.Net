using CryptoExchange.Net.Converters;
using Okex.Net.Enums;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    public class OptionsRemittingAccountTypeConverter : BaseConverter<OkexOptionsRemittingAccountType>
    {
        public OptionsRemittingAccountTypeConverter() : this(true) { }
        public OptionsRemittingAccountTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OkexOptionsRemittingAccountType, string>> Mapping => new List<KeyValuePair<OkexOptionsRemittingAccountType, string>>
        {
            new KeyValuePair<OkexOptionsRemittingAccountType, string>(OkexOptionsRemittingAccountType.Spot, "1"),
            new KeyValuePair<OkexOptionsRemittingAccountType, string>(OkexOptionsRemittingAccountType.Futures, "3"),
            new KeyValuePair<OkexOptionsRemittingAccountType, string>(OkexOptionsRemittingAccountType.C2C, "4"),
            new KeyValuePair<OkexOptionsRemittingAccountType, string>(OkexOptionsRemittingAccountType.Margin, "5"),
            new KeyValuePair<OkexOptionsRemittingAccountType, string>(OkexOptionsRemittingAccountType.FundingAccount, "6"),
            new KeyValuePair<OkexOptionsRemittingAccountType, string>(OkexOptionsRemittingAccountType.PiggyBank, "8"),
            new KeyValuePair<OkexOptionsRemittingAccountType, string>(OkexOptionsRemittingAccountType.Swap, "9"),
            new KeyValuePair<OkexOptionsRemittingAccountType, string>(OkexOptionsRemittingAccountType.Options, "12"),
            new KeyValuePair<OkexOptionsRemittingAccountType, string>(OkexOptionsRemittingAccountType.MiningAccount, "14"),
            new KeyValuePair<OkexOptionsRemittingAccountType, string>(OkexOptionsRemittingAccountType.LoansAccount, "17"),
        };
    }
}