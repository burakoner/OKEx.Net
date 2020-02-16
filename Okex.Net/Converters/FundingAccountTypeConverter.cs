using CryptoExchange.Net.Converters;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    internal class FundingAccountTypeConverter : BaseConverter<OkexFundingAccountType>
    {
        public FundingAccountTypeConverter() : this(true) { }
        public FundingAccountTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OkexFundingAccountType, string>> Mapping => new List<KeyValuePair<OkexFundingAccountType, string>>
        {
            new KeyValuePair<OkexFundingAccountType, string>(OkexFundingAccountType.TotalAccountAssets, "0"),
            new KeyValuePair<OkexFundingAccountType, string>(OkexFundingAccountType.Spot, "1"),
            new KeyValuePair<OkexFundingAccountType, string>(OkexFundingAccountType.Futures, "3"),
            new KeyValuePair<OkexFundingAccountType, string>(OkexFundingAccountType.C2C, "4"),
            new KeyValuePair<OkexFundingAccountType, string>(OkexFundingAccountType.Margin, "5"),
            new KeyValuePair<OkexFundingAccountType, string>(OkexFundingAccountType.FundingAccount, "6"),
            new KeyValuePair<OkexFundingAccountType, string>(OkexFundingAccountType.PiggyBank, "8"),
            new KeyValuePair<OkexFundingAccountType, string>(OkexFundingAccountType.Swap, "9"),
            new KeyValuePair<OkexFundingAccountType, string>(OkexFundingAccountType.Option, "12"),
            new KeyValuePair<OkexFundingAccountType, string>(OkexFundingAccountType.MiningAccount, "14"),
        };
    }
}