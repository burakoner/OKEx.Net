using CryptoExchange.Net.Converters;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    internal class FundingAccountTypeConverter : BaseConverter<FundingAccountType>
    {
        public FundingAccountTypeConverter() : this(true) { }
        public FundingAccountTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<FundingAccountType, string>> Mapping => new List<KeyValuePair<FundingAccountType, string>>
        {
            new KeyValuePair<FundingAccountType, string>(FundingAccountType.TotalAccountAssets, "0"),
            new KeyValuePair<FundingAccountType, string>(FundingAccountType.Spot, "1"),
            new KeyValuePair<FundingAccountType, string>(FundingAccountType.Futures, "3"),
            new KeyValuePair<FundingAccountType, string>(FundingAccountType.C2C, "4"),
            new KeyValuePair<FundingAccountType, string>(FundingAccountType.Margin, "5"),
            new KeyValuePair<FundingAccountType, string>(FundingAccountType.FundingAccount, "6"),
            new KeyValuePair<FundingAccountType, string>(FundingAccountType.PiggyBank, "8"),
            new KeyValuePair<FundingAccountType, string>(FundingAccountType.Swap, "9"),
            new KeyValuePair<FundingAccountType, string>(FundingAccountType.Option, "12"),
            new KeyValuePair<FundingAccountType, string>(FundingAccountType.MiningAccount, "14"),
        };
    }
}