using CryptoExchange.Net.Converters;
using Okex.Net.Enums;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    public class FundingDepositStatusConverter : BaseConverter<OkexFundingDepositStatus>
    {
        public FundingDepositStatusConverter() : this(true) { }
        public FundingDepositStatusConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OkexFundingDepositStatus, string>> Mapping => new List<KeyValuePair<OkexFundingDepositStatus, string>>
        {
            new KeyValuePair<OkexFundingDepositStatus, string>(OkexFundingDepositStatus.WaitingForConfirmation, "0"),
            new KeyValuePair<OkexFundingDepositStatus, string>(OkexFundingDepositStatus.DepositCredited, "1"),
            new KeyValuePair<OkexFundingDepositStatus, string>(OkexFundingDepositStatus.DepositSuccessful, "2"),
        };
    }
}