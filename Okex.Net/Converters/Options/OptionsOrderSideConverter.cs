
using CryptoExchange.Net.Converters;
using Okex.Net.Enums;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    public class OptionsOrderSideConverter : BaseConverter<OkexOptionsOrderSide>
    {
        public OptionsOrderSideConverter() : this(true) { }
        public OptionsOrderSideConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OkexOptionsOrderSide, string>> Mapping => new List<KeyValuePair<OkexOptionsOrderSide, string>>
        {
            new KeyValuePair<OkexOptionsOrderSide, string>(OkexOptionsOrderSide.Buy, "buy"),
            new KeyValuePair<OkexOptionsOrderSide, string>(OkexOptionsOrderSide.Sell, "sell"),
        };
    }
}