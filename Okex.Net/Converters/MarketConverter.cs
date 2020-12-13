using CryptoExchange.Net.Converters;
using Okex.Net.Enums;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    internal class MarketConverter : BaseConverter<OkexMarket>
    {
        public MarketConverter() : this(true) { }
        public MarketConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OkexMarket, string>> Mapping => new List<KeyValuePair<OkexMarket, string>>
        {
            new KeyValuePair<OkexMarket, string>(OkexMarket.Spot, "1"),
            new KeyValuePair<OkexMarket, string>(OkexMarket.Margin, "2"),
        };
    }
}