using CryptoExchange.Net.Converters;
using Okex.Net.Enums;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    internal class FuturesOrderSideConverter : BaseConverter<OkexFuturesOrderSide>
    {
        public FuturesOrderSideConverter() : this(true) { }
        public FuturesOrderSideConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OkexFuturesOrderSide, string>> Mapping => new List<KeyValuePair<OkexFuturesOrderSide, string>>
        {
            new KeyValuePair<OkexFuturesOrderSide, string>(OkexFuturesOrderSide.Buy, "buy"),
            new KeyValuePair<OkexFuturesOrderSide, string>(OkexFuturesOrderSide.Sell, "sell")
        };
    }
}
