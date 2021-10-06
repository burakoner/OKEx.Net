using CryptoExchange.Net.Converters;
using Okex.Net.Enums;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    internal class PiggyBankActionSideConverter : BaseConverter<OkexPiggyBankActionSide>
    {
        public PiggyBankActionSideConverter() : this(true) { }
        public PiggyBankActionSideConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OkexPiggyBankActionSide, string>> Mapping => new List<KeyValuePair<OkexPiggyBankActionSide, string>>
        {
            new KeyValuePair<OkexPiggyBankActionSide, string>(OkexPiggyBankActionSide.Purchase, "purchase"),
            new KeyValuePair<OkexPiggyBankActionSide, string>(OkexPiggyBankActionSide.Redempt, "redempt"),
        };
    }
}