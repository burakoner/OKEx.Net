using CryptoExchange.Net.Converters;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    internal class FundingBillTypeConverter : BaseConverter<OkexFundingBillType>
    {
        public FundingBillTypeConverter() : this(true) { }
        public FundingBillTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OkexFundingBillType, string>> Mapping => new List<KeyValuePair<OkexFundingBillType, string>>
        {
            new KeyValuePair<OkexFundingBillType, string>(OkexFundingBillType.Deposit, "1"),
            new KeyValuePair<OkexFundingBillType, string>(OkexFundingBillType.Withdrawal, "2"),
            new KeyValuePair<OkexFundingBillType, string>(OkexFundingBillType.CanceledWithdrawal, "13"),
            new KeyValuePair<OkexFundingBillType, string>(OkexFundingBillType.TransferIntoFutures, "18"),
            new KeyValuePair<OkexFundingBillType, string>(OkexFundingBillType.TransferFromFutures, "19"),
            new KeyValuePair<OkexFundingBillType, string>(OkexFundingBillType.TransferIntoSubAccount, "20"),
            new KeyValuePair<OkexFundingBillType, string>(OkexFundingBillType.TransferFromSubAccount, "21"),
            new KeyValuePair<OkexFundingBillType, string>(OkexFundingBillType.Claim, "28"),
            new KeyValuePair<OkexFundingBillType, string>(OkexFundingBillType.TransferIntoETT, "29"),
            new KeyValuePair<OkexFundingBillType, string>(OkexFundingBillType.TransferFromETT, "30"),
            new KeyValuePair<OkexFundingBillType, string>(OkexFundingBillType.TransferIntoC2C, "31"),
            new KeyValuePair<OkexFundingBillType, string>(OkexFundingBillType.TransferFromC2C, "32"),
            new KeyValuePair<OkexFundingBillType, string>(OkexFundingBillType.TransferIntoMargin, "33"),
            new KeyValuePair<OkexFundingBillType, string>(OkexFundingBillType.TransferFromMargin, "34"),
            new KeyValuePair<OkexFundingBillType, string>(OkexFundingBillType.TransferIntoSpotAccount, "37"),
            new KeyValuePair<OkexFundingBillType, string>(OkexFundingBillType.TransferFromSpotAccount, "38"),
        };
    }
}