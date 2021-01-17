using CryptoExchange.Net.Converters;
using Okex.Net.Enums;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    public class FuturesDirectionConverter : BaseConverter<OkexFuturesDirection>
    {
        public FuturesDirectionConverter() : this(true) { }
        public FuturesDirectionConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OkexFuturesDirection, string>> Mapping => new List<KeyValuePair<OkexFuturesDirection, string>>
        {
            new KeyValuePair<OkexFuturesDirection, string>(OkexFuturesDirection.Long, "long"),
            new KeyValuePair<OkexFuturesDirection, string>(OkexFuturesDirection.Short, "short"),
        };
    }
}