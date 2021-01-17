using CryptoExchange.Net.Converters;
using Okex.Net.Enums;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    public class SpotOrderSideConverter : BaseConverter<OkexSpotOrderSide>
    {
        public SpotOrderSideConverter() : this(true) { }
        public SpotOrderSideConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OkexSpotOrderSide, string>> Mapping => new List<KeyValuePair<OkexSpotOrderSide, string>>
        {
            new KeyValuePair<OkexSpotOrderSide, string>(OkexSpotOrderSide.Buy, "buy"),
            new KeyValuePair<OkexSpotOrderSide, string>(OkexSpotOrderSide.Sell, "sell")
        };
    }
}
