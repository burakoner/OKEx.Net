using CryptoExchange.Net.Converters;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    internal class FundingDepositStatusConverter : BaseConverter<FundingDepositStatus>
    {
        public FundingDepositStatusConverter() : this(true) { }
        public FundingDepositStatusConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<FundingDepositStatus, string>> Mapping => new List<KeyValuePair<FundingDepositStatus, string>>
        {
            new KeyValuePair<FundingDepositStatus, string>(FundingDepositStatus.WaitingForConfirmation, "0"),
            new KeyValuePair<FundingDepositStatus, string>(FundingDepositStatus.DepositCredited, "1"),
            new KeyValuePair<FundingDepositStatus, string>(FundingDepositStatus.DepositSuccessful, "2"),
        };
    }
}