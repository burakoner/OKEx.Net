using CryptoExchange.Net.Converters;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    internal class SpotOrderBookDataTypeConverter : BaseConverter<SpotOrderBookDataType>
    {
        public SpotOrderBookDataTypeConverter() : this(true) { }
        public SpotOrderBookDataTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<SpotOrderBookDataType, string>> Mapping => new List<KeyValuePair<SpotOrderBookDataType, string>>
        {
            new KeyValuePair<SpotOrderBookDataType, string>(SpotOrderBookDataType.Api, "api"),
            new KeyValuePair<SpotOrderBookDataType, string>(SpotOrderBookDataType.DepthTop5, "depth5"),
            new KeyValuePair<SpotOrderBookDataType, string>(SpotOrderBookDataType.DepthPartial, "partial"),
            new KeyValuePair<SpotOrderBookDataType, string>(SpotOrderBookDataType.DepthUpdate, "update"),
        };
    }
}
