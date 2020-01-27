using CryptoExchange.Net.Converters;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    internal class SpotMarginOrderSourceTypeConverter : BaseConverter<SpotMarginOrderSourceType>
    {
        public SpotMarginOrderSourceTypeConverter() : this(true) { }
        public SpotMarginOrderSourceTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<SpotMarginOrderSourceType, string>> Mapping => new List<KeyValuePair<SpotMarginOrderSourceType, string>>
        {
            new KeyValuePair<SpotMarginOrderSourceType, string>(SpotMarginOrderSourceType.Spot, "1"),
            new KeyValuePair<SpotMarginOrderSourceType, string>(SpotMarginOrderSourceType.Margin, "2"),
        };
    }
}