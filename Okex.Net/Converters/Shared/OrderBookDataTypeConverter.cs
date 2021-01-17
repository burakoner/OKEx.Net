using CryptoExchange.Net.Converters;
using Okex.Net.Enums;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    public class OrderBookDataTypeConverter : BaseConverter<OkexOrderBookDataType>
    {
        public OrderBookDataTypeConverter() : this(true) { }
        public OrderBookDataTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OkexOrderBookDataType, string>> Mapping => new List<KeyValuePair<OkexOrderBookDataType, string>>
        {
            new KeyValuePair<OkexOrderBookDataType, string>(OkexOrderBookDataType.Api, "api"),
            new KeyValuePair<OkexOrderBookDataType, string>(OkexOrderBookDataType.DepthTop5, "depth5"),
            new KeyValuePair<OkexOrderBookDataType, string>(OkexOrderBookDataType.DepthPartial, "partial"),
            new KeyValuePair<OkexOrderBookDataType, string>(OkexOrderBookDataType.DepthUpdate, "update"),
        };
    }
}
