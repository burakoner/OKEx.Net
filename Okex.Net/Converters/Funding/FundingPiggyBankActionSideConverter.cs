using CryptoExchange.Net.Converters;
using Okex.Net.Enums;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    public class FundingPiggyBankActionSideConverter : BaseConverter<OkexFundingPiggyBankActionSide>
    {
        public FundingPiggyBankActionSideConverter() : this(true) { }
        public FundingPiggyBankActionSideConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OkexFundingPiggyBankActionSide, string>> Mapping => new List<KeyValuePair<OkexFundingPiggyBankActionSide, string>>
        {
            new KeyValuePair<OkexFundingPiggyBankActionSide, string>(OkexFundingPiggyBankActionSide.Purchase, "purchase"),
            new KeyValuePair<OkexFundingPiggyBankActionSide, string>(OkexFundingPiggyBankActionSide.Redempt, "redempt"),
        };
    }
}