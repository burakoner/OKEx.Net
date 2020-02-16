using CryptoExchange.Net.Converters;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    internal class SpotMarginOrderSourceTypeConverter : BaseConverter<OkexSpotMarginOrderSourceType>
    {
        public SpotMarginOrderSourceTypeConverter() : this(true) { }
        public SpotMarginOrderSourceTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OkexSpotMarginOrderSourceType, string>> Mapping => new List<KeyValuePair<OkexSpotMarginOrderSourceType, string>>
        {
            new KeyValuePair<OkexSpotMarginOrderSourceType, string>(OkexSpotMarginOrderSourceType.Spot, "1"),
            new KeyValuePair<OkexSpotMarginOrderSourceType, string>(OkexSpotMarginOrderSourceType.Margin, "2"),
        };
    }
}