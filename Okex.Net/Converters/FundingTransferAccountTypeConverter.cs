using CryptoExchange.Net.Converters;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    internal class FundingTransferAccountTypeConverter : BaseConverter<OkexFundingTransferAccountType>
    {
        public FundingTransferAccountTypeConverter() : this(true) { }
        public FundingTransferAccountTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OkexFundingTransferAccountType, string>> Mapping => new List<KeyValuePair<OkexFundingTransferAccountType, string>>
        {
            new KeyValuePair<OkexFundingTransferAccountType, string>(OkexFundingTransferAccountType.SubAccount, "0"),
            new KeyValuePair<OkexFundingTransferAccountType, string>(OkexFundingTransferAccountType.Spot, "1"),
            new KeyValuePair<OkexFundingTransferAccountType, string>(OkexFundingTransferAccountType.Futures, "3"),
            new KeyValuePair<OkexFundingTransferAccountType, string>(OkexFundingTransferAccountType.C2C, "4"),
            new KeyValuePair<OkexFundingTransferAccountType, string>(OkexFundingTransferAccountType.Margin, "5"),
            new KeyValuePair<OkexFundingTransferAccountType, string>(OkexFundingTransferAccountType.FundingAccount, "6"),
            new KeyValuePair<OkexFundingTransferAccountType, string>(OkexFundingTransferAccountType.PiggyBank, "8"),
            new KeyValuePair<OkexFundingTransferAccountType, string>(OkexFundingTransferAccountType.Swap, "9"),
            new KeyValuePair<OkexFundingTransferAccountType, string>(OkexFundingTransferAccountType.Option, "12"),
        };
    }
}