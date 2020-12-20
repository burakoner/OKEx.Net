using CryptoExchange.Net.Converters;
using Okex.Net.Enums;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    internal class OptionsTimeInForceConverter : BaseConverter<OkexOptionsTimeInForce>
    {
        public OptionsTimeInForceConverter() : this(true) { }
        public OptionsTimeInForceConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OkexOptionsTimeInForce, string>> Mapping => new List<KeyValuePair<OkexOptionsTimeInForce, string>>
        {
            new KeyValuePair<OkexOptionsTimeInForce, string>(OkexOptionsTimeInForce.NormalOrder, "0"),
            //new KeyValuePair<OkexOptionsTimeInForce, string>(OkexOptionsTimeInForce.PostOnly, "1"),
            //new KeyValuePair<OkexOptionsTimeInForce, string>(OkexOptionsTimeInForce.FillOrKil, "2"),
            //new KeyValuePair<OkexOptionsTimeInForce, string>(OkexOptionsTimeInForce.ImmediateOrCancel, "3"),
            //new KeyValuePair<OkexOptionsTimeInForce, string>(OkexOptionsTimeInForce.Market, "4"),
        };
    }
}