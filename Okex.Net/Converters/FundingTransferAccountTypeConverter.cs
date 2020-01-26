using CryptoExchange.Net.Converters;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    internal class FundingTransferAccountTypeConverter : BaseConverter<FundingTransferAccountType>
    {
        public FundingTransferAccountTypeConverter() : this(true) { }
        public FundingTransferAccountTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<FundingTransferAccountType, string>> Mapping => new List<KeyValuePair<FundingTransferAccountType, string>>
        {
            new KeyValuePair<FundingTransferAccountType, string>(FundingTransferAccountType.SubAccount, "0"),
            new KeyValuePair<FundingTransferAccountType, string>(FundingTransferAccountType.Spot, "1"),
            new KeyValuePair<FundingTransferAccountType, string>(FundingTransferAccountType.Futures, "3"),
            new KeyValuePair<FundingTransferAccountType, string>(FundingTransferAccountType.C2C, "4"),
            new KeyValuePair<FundingTransferAccountType, string>(FundingTransferAccountType.Margin, "5"),
            new KeyValuePair<FundingTransferAccountType, string>(FundingTransferAccountType.FundingAccount, "6"),
            new KeyValuePair<FundingTransferAccountType, string>(FundingTransferAccountType.PiggyBank, "8"),
            new KeyValuePair<FundingTransferAccountType, string>(FundingTransferAccountType.Swap, "9"),
            new KeyValuePair<FundingTransferAccountType, string>(FundingTransferAccountType.Option, "12"),
        };
    }
}