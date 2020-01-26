using CryptoExchange.Net.Converters;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    internal class SpotOrderTypeConverter : BaseConverter<SpotOrderType>
    {
        public SpotOrderTypeConverter() : this(true) { }
        public SpotOrderTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<SpotOrderType, string>> Mapping => new List<KeyValuePair<SpotOrderType, string>>
        {
            new KeyValuePair<SpotOrderType, string>(SpotOrderType.Limit, "limit"),
            new KeyValuePair<SpotOrderType, string>(SpotOrderType.Market, "market"),
        };
    }
}
