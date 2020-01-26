using CryptoExchange.Net.Converters;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    internal class FundingBillTypeConverter : BaseConverter<FundingBillType>
    {
        public FundingBillTypeConverter() : this(true) { }
        public FundingBillTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<FundingBillType, string>> Mapping => new List<KeyValuePair<FundingBillType, string>>
        {
            new KeyValuePair<FundingBillType, string>(FundingBillType.Deposit, "1"),
            new KeyValuePair<FundingBillType, string>(FundingBillType.Withdrawal, "2"),
            new KeyValuePair<FundingBillType, string>(FundingBillType.CanceledWithdrawal, "13"),
            new KeyValuePair<FundingBillType, string>(FundingBillType.TransferIntoFutures, "18"),
            new KeyValuePair<FundingBillType, string>(FundingBillType.TransferFromFutures, "19"),
            new KeyValuePair<FundingBillType, string>(FundingBillType.TransferIntoSubAccount, "20"),
            new KeyValuePair<FundingBillType, string>(FundingBillType.TransferFromSubAccount, "21"),
            new KeyValuePair<FundingBillType, string>(FundingBillType.Claim, "28"),
            new KeyValuePair<FundingBillType, string>(FundingBillType.TransferIntoETT, "29"),
            new KeyValuePair<FundingBillType, string>(FundingBillType.TransferFromETT, "30"),
            new KeyValuePair<FundingBillType, string>(FundingBillType.TransferIntoC2C, "31"),
            new KeyValuePair<FundingBillType, string>(FundingBillType.TransferFromC2C, "32"),
            new KeyValuePair<FundingBillType, string>(FundingBillType.TransferIntoMargin, "33"),
            new KeyValuePair<FundingBillType, string>(FundingBillType.TransferFromMargin, "34"),
            new KeyValuePair<FundingBillType, string>(FundingBillType.TransferIntoSpotAccount, "37"),
            new KeyValuePair<FundingBillType, string>(FundingBillType.TransferFromSpotAccount, "38"),
        };
    }
}