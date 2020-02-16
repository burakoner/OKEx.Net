using CryptoExchange.Net.Converters;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    internal class SpotTimeInForceConverter : BaseConverter<OkexSpotTimeInForce>
    {
        public SpotTimeInForceConverter() : this(true) { }
        public SpotTimeInForceConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OkexSpotTimeInForce, string>> Mapping => new List<KeyValuePair<OkexSpotTimeInForce, string>>
        {
            new KeyValuePair<OkexSpotTimeInForce, string>(OkexSpotTimeInForce.NormalOrder, "0"),
            new KeyValuePair<OkexSpotTimeInForce, string>(OkexSpotTimeInForce.PostOnly, "1"),
            new KeyValuePair<OkexSpotTimeInForce, string>(OkexSpotTimeInForce.FillOrKil, "2"),
            new KeyValuePair<OkexSpotTimeInForce, string>(OkexSpotTimeInForce.ImmediateOrCancel, "3"),
        };
    }
}