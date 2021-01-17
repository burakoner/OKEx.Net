using CryptoExchange.Net.Converters;
using Okex.Net.Enums;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    public class SpotOrderStateConverter : BaseConverter<OkexSpotOrderState>
    {
        public SpotOrderStateConverter() : this(true) { }
        public SpotOrderStateConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OkexSpotOrderState, string>> Mapping => new List<KeyValuePair<OkexSpotOrderState, string>>
        {
            new KeyValuePair<OkexSpotOrderState, string>(OkexSpotOrderState.Failed, "-2"),
            new KeyValuePair<OkexSpotOrderState, string>(OkexSpotOrderState.Canceled, "-1"),
            new KeyValuePair<OkexSpotOrderState, string>(OkexSpotOrderState.Open, "0"),
            new KeyValuePair<OkexSpotOrderState, string>(OkexSpotOrderState.PartiallyFilled, "1"),
            new KeyValuePair<OkexSpotOrderState, string>(OkexSpotOrderState.FullyFilled, "2"),
            new KeyValuePair<OkexSpotOrderState, string>(OkexSpotOrderState.Submitting, "3"),
            new KeyValuePair<OkexSpotOrderState, string>(OkexSpotOrderState.Canceling, "4"),
            new KeyValuePair<OkexSpotOrderState, string>(OkexSpotOrderState.Incomplete, "6"),
            new KeyValuePair<OkexSpotOrderState, string>(OkexSpotOrderState.Complete, "7"),
        };
    }
}