using CryptoExchange.Net.Converters;
using Okex.Net.Enums;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    public class FuturesOrderStateConverter : BaseConverter<OkexFuturesOrderState>
    {
        public FuturesOrderStateConverter() : this(true) { }
        public FuturesOrderStateConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OkexFuturesOrderState, string>> Mapping => new List<KeyValuePair<OkexFuturesOrderState, string>>
        {
            new KeyValuePair<OkexFuturesOrderState, string>(OkexFuturesOrderState.Failed, "-2"),
            new KeyValuePair<OkexFuturesOrderState, string>(OkexFuturesOrderState.Canceled, "-1"),
            new KeyValuePair<OkexFuturesOrderState, string>(OkexFuturesOrderState.Open, "0"),
            new KeyValuePair<OkexFuturesOrderState, string>(OkexFuturesOrderState.PartiallyFilled, "1"),
            new KeyValuePair<OkexFuturesOrderState, string>(OkexFuturesOrderState.FullyFilled, "2"),
            new KeyValuePair<OkexFuturesOrderState, string>(OkexFuturesOrderState.Submitting, "3"),
            new KeyValuePair<OkexFuturesOrderState, string>(OkexFuturesOrderState.Canceling, "4"),
            new KeyValuePair<OkexFuturesOrderState, string>(OkexFuturesOrderState.Incomplete, "6"),
            new KeyValuePair<OkexFuturesOrderState, string>(OkexFuturesOrderState.Complete, "7"),
        };
    }
}