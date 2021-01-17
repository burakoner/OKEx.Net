using CryptoExchange.Net.Converters;
using Okex.Net.Enums;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    public class SwapLeverageSideConverter : BaseConverter<OkexSwapLeverageSide>
    {
        public SwapLeverageSideConverter() : this(true) { }
        public SwapLeverageSideConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OkexSwapLeverageSide, string>> Mapping => new List<KeyValuePair<OkexSwapLeverageSide, string>>
        {
            new KeyValuePair<OkexSwapLeverageSide, string>(OkexSwapLeverageSide.FixedMarginForLongPosition, "1"),
            new KeyValuePair<OkexSwapLeverageSide, string>(OkexSwapLeverageSide.FixedMarginForShortPosition, "2"),
            new KeyValuePair<OkexSwapLeverageSide, string>(OkexSwapLeverageSide.CrossedMargin, "3"),
        };
    }
}