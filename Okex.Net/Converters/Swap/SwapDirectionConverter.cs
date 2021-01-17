using CryptoExchange.Net.Converters;
using Okex.Net.Enums;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    public class SwapDirectionConverter : BaseConverter<OkexSwapDirection>
    {
        public SwapDirectionConverter() : this(true) { }
        public SwapDirectionConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OkexSwapDirection, string>> Mapping => new List<KeyValuePair<OkexSwapDirection, string>>
        {
            new KeyValuePair<OkexSwapDirection, string>(OkexSwapDirection.Long, "long"),
            new KeyValuePair<OkexSwapDirection, string>(OkexSwapDirection.Short, "short"),
        };
    }
}