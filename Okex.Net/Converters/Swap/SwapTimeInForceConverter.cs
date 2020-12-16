using CryptoExchange.Net.Converters;
using Okex.Net.Enums;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    internal class SwapTimeInForceConverter : BaseConverter<OkexSwapTimeInForce>
    {
        public SwapTimeInForceConverter() : this(true) { }
        public SwapTimeInForceConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OkexSwapTimeInForce, string>> Mapping => new List<KeyValuePair<OkexSwapTimeInForce, string>>
        {
            new KeyValuePair<OkexSwapTimeInForce, string>(OkexSwapTimeInForce.NormalOrder, "0"),
            new KeyValuePair<OkexSwapTimeInForce, string>(OkexSwapTimeInForce.PostOnly, "1"),
            new KeyValuePair<OkexSwapTimeInForce, string>(OkexSwapTimeInForce.FillOrKil, "2"),
            new KeyValuePair<OkexSwapTimeInForce, string>(OkexSwapTimeInForce.ImmediateOrCancel, "3"),
            new KeyValuePair<OkexSwapTimeInForce, string>(OkexSwapTimeInForce.Market, "4"),
        };
    }
}