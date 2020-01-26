using CryptoExchange.Net.Converters;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    internal class SpotOrderStateConverter : BaseConverter<SpotOrderState>
    {
        public SpotOrderStateConverter() : this(true) { }
        public SpotOrderStateConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<SpotOrderState, string>> Mapping => new List<KeyValuePair<SpotOrderState, string>>
        {
            new KeyValuePair<SpotOrderState, string>(SpotOrderState.Failed, "-2"),
            new KeyValuePair<SpotOrderState, string>(SpotOrderState.Canceled, "-1"),
            new KeyValuePair<SpotOrderState, string>(SpotOrderState.Open, "0"),
            new KeyValuePair<SpotOrderState, string>(SpotOrderState.PartiallyFilled, "1"),
            new KeyValuePair<SpotOrderState, string>(SpotOrderState.FullyFilled, "2"),
            new KeyValuePair<SpotOrderState, string>(SpotOrderState.Submitting, "3"),
            new KeyValuePair<SpotOrderState, string>(SpotOrderState.Canceling, "4"),
            new KeyValuePair<SpotOrderState, string>(SpotOrderState.Incomplete, "6"),
            new KeyValuePair<SpotOrderState, string>(SpotOrderState.Complete, "7"),
        };
    }
}