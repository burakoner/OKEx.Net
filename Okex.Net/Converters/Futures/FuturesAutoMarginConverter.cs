using CryptoExchange.Net.Converters;
using Okex.Net.Enums;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    internal class FuturesAutoMarginConverter : BaseConverter<OkexFuturesAutoMargin>
    {
        public FuturesAutoMarginConverter() : this(true) { }
        public FuturesAutoMarginConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OkexFuturesAutoMargin, string>> Mapping => new List<KeyValuePair<OkexFuturesAutoMargin, string>>
        {
            new KeyValuePair<OkexFuturesAutoMargin, string>(OkexFuturesAutoMargin.On, "1"),
            new KeyValuePair<OkexFuturesAutoMargin, string>(OkexFuturesAutoMargin.Off, "2"),
        };
    }
}