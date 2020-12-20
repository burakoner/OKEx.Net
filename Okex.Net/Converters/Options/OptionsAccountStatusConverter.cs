
using CryptoExchange.Net.Converters;
using Okex.Net.Enums;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    internal class OptionsAccountStatusConverter : BaseConverter<OkexOptionsAccountStatus>
    {
        public OptionsAccountStatusConverter() : this(true) { }
        public OptionsAccountStatusConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OkexOptionsAccountStatus, string>> Mapping => new List<KeyValuePair<OkexOptionsAccountStatus, string>>
        {
            new KeyValuePair<OkexOptionsAccountStatus, string>(OkexOptionsAccountStatus.Normal, "Normal"),
            new KeyValuePair<OkexOptionsAccountStatus, string>(OkexOptionsAccountStatus.DelayedDeleveraging, "Delayed deleveraging"),
            new KeyValuePair<OkexOptionsAccountStatus, string>(OkexOptionsAccountStatus.Deleveraging, "Deleveraging"),
        };
    }
}