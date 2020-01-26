using CryptoExchange.Net.Converters;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    internal class SpotTimeInForceConverter : BaseConverter<SpotTimeInForce>
    {
        public SpotTimeInForceConverter() : this(true) { }
        public SpotTimeInForceConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<SpotTimeInForce, string>> Mapping => new List<KeyValuePair<SpotTimeInForce, string>>
        {
            new KeyValuePair<SpotTimeInForce, string>(SpotTimeInForce.NormalOrder, "0"),
            new KeyValuePair<SpotTimeInForce, string>(SpotTimeInForce.PostOnly, "1"),
            new KeyValuePair<SpotTimeInForce, string>(SpotTimeInForce.FillOrKil, "2"),
            new KeyValuePair<SpotTimeInForce, string>(SpotTimeInForce.ImmediateOrCancel, "3"),
        };
    }
}