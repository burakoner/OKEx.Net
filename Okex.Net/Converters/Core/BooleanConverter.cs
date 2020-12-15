using CryptoExchange.Net.Converters;
using Okex.Net.Enums;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    internal class BooleanConverter : BaseConverter<bool>
    {
        public BooleanConverter() : this(true) { }
        public BooleanConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<bool, string>> Mapping => new List<KeyValuePair<bool, string>>
        {
            new KeyValuePair<bool, string>(false, "0"),
            new KeyValuePair<bool, string>(true, "1"),
        };
    }
}