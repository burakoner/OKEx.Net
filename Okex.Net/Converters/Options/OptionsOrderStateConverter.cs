
using CryptoExchange.Net.Converters;
using Okex.Net.Enums;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    internal class OptionsOrderStateConverter : BaseConverter<OkexOptionsOrderState>
    {
        public OptionsOrderStateConverter() : this(true) { }
        public OptionsOrderStateConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OkexOptionsOrderState, string>> Mapping => new List<KeyValuePair<OkexOptionsOrderState, string>>
        {
            new KeyValuePair<OkexOptionsOrderState, string>(OkexOptionsOrderState.Failed, "-2"),
            new KeyValuePair<OkexOptionsOrderState, string>(OkexOptionsOrderState.Canceled, "-1"),
            new KeyValuePair<OkexOptionsOrderState, string>(OkexOptionsOrderState.Open, "0"),
            new KeyValuePair<OkexOptionsOrderState, string>(OkexOptionsOrderState.PartiallyFilled, "1"),
            new KeyValuePair<OkexOptionsOrderState, string>(OkexOptionsOrderState.FullyFilled, "2"),
            new KeyValuePair<OkexOptionsOrderState, string>(OkexOptionsOrderState.Submitting, "3"),
            new KeyValuePair<OkexOptionsOrderState, string>(OkexOptionsOrderState.Canceling, "4"),
            new KeyValuePair<OkexOptionsOrderState, string>(OkexOptionsOrderState.PendingAmend, "5"),
            new KeyValuePair<OkexOptionsOrderState, string>(OkexOptionsOrderState.Incomplete, "6"),
            new KeyValuePair<OkexOptionsOrderState, string>(OkexOptionsOrderState.Complete, "7"),
        };
    }
}