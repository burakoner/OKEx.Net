
using CryptoExchange.Net.Converters;
using Okex.Net.Enums;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    internal class OptionsStateConverter : BaseConverter<OkexOptionsState>
    {
        public OptionsStateConverter() : this(true) { }
        public OptionsStateConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OkexOptionsState, string>> Mapping => new List<KeyValuePair<OkexOptionsState, string>>
        {
            new KeyValuePair<OkexOptionsState, string>(OkexOptionsState.PreOpen, "1"),
            new KeyValuePair<OkexOptionsState, string>(OkexOptionsState.Live, "2"),
            new KeyValuePair<OkexOptionsState, string>(OkexOptionsState.Suspended, "3"),
            new KeyValuePair<OkexOptionsState, string>(OkexOptionsState.Settlement, "4"),
        };
    }
}
