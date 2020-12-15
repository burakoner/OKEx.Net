using CryptoExchange.Net.Converters;
using Okex.Net.Enums;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    internal class FuturesTimeInForceConverter : BaseConverter<OkexFuturesTimeInForce>
    {
        public FuturesTimeInForceConverter() : this(true) { }
        public FuturesTimeInForceConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OkexFuturesTimeInForce, string>> Mapping => new List<KeyValuePair<OkexFuturesTimeInForce, string>>
        {
            new KeyValuePair<OkexFuturesTimeInForce, string>(OkexFuturesTimeInForce.NormalOrder, "0"),
            new KeyValuePair<OkexFuturesTimeInForce, string>(OkexFuturesTimeInForce.PostOnly, "1"),
            new KeyValuePair<OkexFuturesTimeInForce, string>(OkexFuturesTimeInForce.FillOrKil, "2"),
            new KeyValuePair<OkexFuturesTimeInForce, string>(OkexFuturesTimeInForce.ImmediateOrCancel, "3"),
            new KeyValuePair<OkexFuturesTimeInForce, string>(OkexFuturesTimeInForce.Market, "4"),
        };
    }
}