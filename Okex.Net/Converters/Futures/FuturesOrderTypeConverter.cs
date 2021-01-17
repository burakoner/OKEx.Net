using CryptoExchange.Net.Converters;
using Okex.Net.Enums;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    public class FuturesOrderTypeConverter : BaseConverter<OkexFuturesOrderType>
    {
        public FuturesOrderTypeConverter() : this(true) { }
        public FuturesOrderTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OkexFuturesOrderType, string>> Mapping => new List<KeyValuePair<OkexFuturesOrderType, string>>
        {
            new KeyValuePair<OkexFuturesOrderType, string>(OkexFuturesOrderType.OpenLong, "1"),
            new KeyValuePair<OkexFuturesOrderType, string>(OkexFuturesOrderType.OpenShort, "2"),
            new KeyValuePair<OkexFuturesOrderType, string>(OkexFuturesOrderType.CloseLong, "3"),
            new KeyValuePair<OkexFuturesOrderType, string>(OkexFuturesOrderType.CloseShort, "4"),
        };
    }
}