using CryptoExchange.Net.Converters;
using Okex.Net.Enums;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    internal class SwapMarginModeConverter : BaseConverter<OkexSwapMarginMode>
    {
        public SwapMarginModeConverter() : this(true) { }
        public SwapMarginModeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OkexSwapMarginMode, string>> Mapping => new List<KeyValuePair<OkexSwapMarginMode, string>>
        {
            new KeyValuePair<OkexSwapMarginMode, string>(OkexSwapMarginMode.Crossed, "crossed"),
            new KeyValuePair<OkexSwapMarginMode, string>(OkexSwapMarginMode.Fixed, "fixed"),
        };
    }
}