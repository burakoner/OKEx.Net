
using CryptoExchange.Net.Converters;
using Okex.Net.Enums;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    internal class OptionsTypeConverter : BaseConverter<OkexOptionsType>
    {
        public OptionsTypeConverter() : this(true) { }
        public OptionsTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OkexOptionsType, string>> Mapping => new List<KeyValuePair<OkexOptionsType, string>>
        {
            new KeyValuePair<OkexOptionsType, string>(OkexOptionsType.C, "C"),
            new KeyValuePair<OkexOptionsType, string>(OkexOptionsType.P, "P"),
        };
    }
}