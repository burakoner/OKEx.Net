using CryptoExchange.Net.Converters;
using Okex.Net.Enums;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    internal class OrderSideConverter : BaseConverter<OkexOrderSide>
    {
        public OrderSideConverter() : this(true) { }
        public OrderSideConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OkexOrderSide, string>> Mapping => new List<KeyValuePair<OkexOrderSide, string>>
        {
            new KeyValuePair<OkexOrderSide, string>(OkexOrderSide.Buy, "buy"),
            new KeyValuePair<OkexOrderSide, string>(OkexOrderSide.Sell, "sell"),
        };
    }
}