using CryptoExchange.Net.Converters;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    internal class SpotOrderSideConverter : BaseConverter<SpotOrderSide>
    {
        public SpotOrderSideConverter() : this(true) { }
        public SpotOrderSideConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<SpotOrderSide, string>> Mapping => new List<KeyValuePair<SpotOrderSide, string>>
        {
            new KeyValuePair<SpotOrderSide, string>(SpotOrderSide.Buy, "buy"),
            new KeyValuePair<SpotOrderSide, string>(SpotOrderSide.Sell, "sell")
        };
    }
}
