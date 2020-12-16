using CryptoExchange.Net.Converters;
using Okex.Net.Enums;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    internal class SwapOrderStateConverter : BaseConverter<OkexSwapOrderState>
    {
        public SwapOrderStateConverter() : this(true) { }
        public SwapOrderStateConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OkexSwapOrderState, string>> Mapping => new List<KeyValuePair<OkexSwapOrderState, string>>
        {
            new KeyValuePair<OkexSwapOrderState, string>(OkexSwapOrderState.Failed, "-2"),
            new KeyValuePair<OkexSwapOrderState, string>(OkexSwapOrderState.Canceled, "-1"),
            new KeyValuePair<OkexSwapOrderState, string>(OkexSwapOrderState.Open, "0"),
            new KeyValuePair<OkexSwapOrderState, string>(OkexSwapOrderState.PartiallyFilled, "1"),
            new KeyValuePair<OkexSwapOrderState, string>(OkexSwapOrderState.FullyFilled, "2"),
            new KeyValuePair<OkexSwapOrderState, string>(OkexSwapOrderState.Submitting, "3"),
            new KeyValuePair<OkexSwapOrderState, string>(OkexSwapOrderState.Canceling, "4"),
            new KeyValuePair<OkexSwapOrderState, string>(OkexSwapOrderState.Incomplete, "6"),
            new KeyValuePair<OkexSwapOrderState, string>(OkexSwapOrderState.Complete, "7"),
        };
    }
}