using CryptoExchange.Net.Converters;
using Okex.Net.Enums;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    internal class SpotOrderBookDataTypeConverter : BaseConverter<OkexSpotOrderBookDataType>
    {
        public SpotOrderBookDataTypeConverter() : this(true) { }
        public SpotOrderBookDataTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OkexSpotOrderBookDataType, string>> Mapping => new List<KeyValuePair<OkexSpotOrderBookDataType, string>>
        {
            new KeyValuePair<OkexSpotOrderBookDataType, string>(OkexSpotOrderBookDataType.Api, "api"),
            new KeyValuePair<OkexSpotOrderBookDataType, string>(OkexSpotOrderBookDataType.DepthTop5, "depth5"),
            new KeyValuePair<OkexSpotOrderBookDataType, string>(OkexSpotOrderBookDataType.DepthPartial, "partial"),
            new KeyValuePair<OkexSpotOrderBookDataType, string>(OkexSpotOrderBookDataType.DepthUpdate, "update"),
        };
    }
}
