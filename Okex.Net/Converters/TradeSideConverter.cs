using CryptoExchange.Net.Converters;
using Okex.Net.Enums;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    internal class TradeSideConverter : BaseConverter<OkexTradeSide>
    {
        public TradeSideConverter() : this(true) { }
        public TradeSideConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OkexTradeSide, string>> Mapping => new List<KeyValuePair<OkexTradeSide, string>>
        {
            new KeyValuePair<OkexTradeSide, string>(OkexTradeSide.Buy, "buy"),
            new KeyValuePair<OkexTradeSide, string>(OkexTradeSide.Sell, "sell"),
        };
    }
}