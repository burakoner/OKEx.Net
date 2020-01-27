using CryptoExchange.Net.Converters;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    internal class FundingWithdrawalStatusConverter : BaseConverter<FundingWithdrawalStatus>
    {
        public FundingWithdrawalStatusConverter() : this(true) { }
        public FundingWithdrawalStatusConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<FundingWithdrawalStatus, string>> Mapping => new List<KeyValuePair<FundingWithdrawalStatus, string>>
        {
            new KeyValuePair<FundingWithdrawalStatus, string>(FundingWithdrawalStatus.PendingCancel, "-3"),
            new KeyValuePair<FundingWithdrawalStatus, string>(FundingWithdrawalStatus.Cancelled, "-2"),
            new KeyValuePair<FundingWithdrawalStatus, string>(FundingWithdrawalStatus.Failed, "-1"),
            new KeyValuePair<FundingWithdrawalStatus, string>(FundingWithdrawalStatus.Pending, "0"),
            new KeyValuePair<FundingWithdrawalStatus, string>(FundingWithdrawalStatus.Sending, "1"),
            new KeyValuePair<FundingWithdrawalStatus, string>(FundingWithdrawalStatus.Sent, "2"),
            new KeyValuePair<FundingWithdrawalStatus, string>(FundingWithdrawalStatus.AwaitingEmailVerification, "3"),
            new KeyValuePair<FundingWithdrawalStatus, string>(FundingWithdrawalStatus.AwaitingManualVerification, "4"),
            new KeyValuePair<FundingWithdrawalStatus, string>(FundingWithdrawalStatus.AwaitingIdentityVerification, "5"),
        };
    }
}