using CryptoExchange.Net.Converters;
using Okex.Net.Enums;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    public class FundingWithdrawalStatusConverter : BaseConverter<OkexFundingWithdrawalStatus>
    {
        public FundingWithdrawalStatusConverter() : this(true) { }
        public FundingWithdrawalStatusConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OkexFundingWithdrawalStatus, string>> Mapping => new List<KeyValuePair<OkexFundingWithdrawalStatus, string>>
        {
            new KeyValuePair<OkexFundingWithdrawalStatus, string>(OkexFundingWithdrawalStatus.PendingCancel, "-3"),
            new KeyValuePair<OkexFundingWithdrawalStatus, string>(OkexFundingWithdrawalStatus.Cancelled, "-2"),
            new KeyValuePair<OkexFundingWithdrawalStatus, string>(OkexFundingWithdrawalStatus.Failed, "-1"),
            new KeyValuePair<OkexFundingWithdrawalStatus, string>(OkexFundingWithdrawalStatus.Pending, "0"),
            new KeyValuePair<OkexFundingWithdrawalStatus, string>(OkexFundingWithdrawalStatus.Sending, "1"),
            new KeyValuePair<OkexFundingWithdrawalStatus, string>(OkexFundingWithdrawalStatus.Sent, "2"),
            new KeyValuePair<OkexFundingWithdrawalStatus, string>(OkexFundingWithdrawalStatus.AwaitingEmailVerification, "3"),
            new KeyValuePair<OkexFundingWithdrawalStatus, string>(OkexFundingWithdrawalStatus.AwaitingManualVerification, "4"),
            new KeyValuePair<OkexFundingWithdrawalStatus, string>(OkexFundingWithdrawalStatus.AwaitingIdentityVerification, "5"),
        };
    }
}