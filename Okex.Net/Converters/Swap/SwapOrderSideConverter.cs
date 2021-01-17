using CryptoExchange.Net.Converters;
using Okex.Net.Enums;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    public class SwapOrderSideConverter : BaseConverter<OkexSwapOrderSide>
    {
        public SwapOrderSideConverter() : this(true) { }
        public SwapOrderSideConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OkexSwapOrderSide, string>> Mapping => new List<KeyValuePair<OkexSwapOrderSide, string>>
        {
            new KeyValuePair<OkexSwapOrderSide, string>(OkexSwapOrderSide.Buy, "buy"),
            new KeyValuePair<OkexSwapOrderSide, string>(OkexSwapOrderSide.Sell, "sell")
        };
    }
}
