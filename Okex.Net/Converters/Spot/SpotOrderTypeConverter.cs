using CryptoExchange.Net.Converters;
using Okex.Net.Enums;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    public class SpotOrderTypeConverter : BaseConverter<OkexSpotOrderType>
    {
        public SpotOrderTypeConverter() : this(true) { }
        public SpotOrderTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OkexSpotOrderType, string>> Mapping => new List<KeyValuePair<OkexSpotOrderType, string>>
        {
            new KeyValuePair<OkexSpotOrderType, string>(OkexSpotOrderType.Limit, "limit"),
            new KeyValuePair<OkexSpotOrderType, string>(OkexSpotOrderType.Market, "market"),
        };
    }
}
