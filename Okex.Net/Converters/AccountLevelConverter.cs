using CryptoExchange.Net.Converters;
using Okex.Net.Enums;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    internal class AccountLevelConverter : BaseConverter<OkexAccountLevel>
    {
        public AccountLevelConverter() : this(true) { }
        public AccountLevelConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OkexAccountLevel, string>> Mapping => new List<KeyValuePair<OkexAccountLevel, string>>
        {
            new KeyValuePair<OkexAccountLevel, string>(OkexAccountLevel.Simple, "1"),
            new KeyValuePair<OkexAccountLevel, string>(OkexAccountLevel.SingleCurrencyMargin, "2"),
            new KeyValuePair<OkexAccountLevel, string>(OkexAccountLevel.MultiCurrencyMargin, "3"),
        };
    }
}