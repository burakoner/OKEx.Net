using CryptoExchange.Net.Converters;
using Okex.Net.Enums;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    public class FuturesMarginActionConverter : BaseConverter<OkexFuturesMarginAction>
    {
        public FuturesMarginActionConverter() : this(true) { }
        public FuturesMarginActionConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OkexFuturesMarginAction, string>> Mapping => new List<KeyValuePair<OkexFuturesMarginAction, string>>
        {
            new KeyValuePair<OkexFuturesMarginAction, string>(OkexFuturesMarginAction.Increase, "1"),
            new KeyValuePair<OkexFuturesMarginAction, string>(OkexFuturesMarginAction.Decrease, "2"),
        };
    }
}