using CryptoExchange.Net.Converters;
using Okex.Net.Enums;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    internal class SwapOrderTypeConverter : BaseConverter<OkexSwapOrderType>
    {
        public SwapOrderTypeConverter() : this(true) { }
        public SwapOrderTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OkexSwapOrderType, string>> Mapping => new List<KeyValuePair<OkexSwapOrderType, string>>
        {
            new KeyValuePair<OkexSwapOrderType, string>(OkexSwapOrderType.OpenLong, "1"),
            new KeyValuePair<OkexSwapOrderType, string>(OkexSwapOrderType.OpenShort, "2"),
            new KeyValuePair<OkexSwapOrderType, string>(OkexSwapOrderType.CloseLong, "3"),
            new KeyValuePair<OkexSwapOrderType, string>(OkexSwapOrderType.CloseShort, "4"),
        };
    }
}