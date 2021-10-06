using CryptoExchange.Net.Converters;
using Okex.Net.Enums;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    internal class PositionSideConverter : BaseConverter<OkexPositionSide>
    {
        public PositionSideConverter() : this(true) { }
        public PositionSideConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OkexPositionSide, string>> Mapping => new List<KeyValuePair<OkexPositionSide, string>>
        {
            new KeyValuePair<OkexPositionSide, string>(OkexPositionSide.Long, "long"),
            new KeyValuePair<OkexPositionSide, string>(OkexPositionSide.Short, "short"),
            new KeyValuePair<OkexPositionSide, string>(OkexPositionSide.Net, "net"),
        };
    }
}