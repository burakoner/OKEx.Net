using CryptoExchange.Net.Converters;
using Okex.Net.Enums;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    internal class MarginLoanStateConverter : BaseConverter<OkexMarginLoanState>
    {
        public MarginLoanStateConverter() : this(true) { }
        public MarginLoanStateConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OkexMarginLoanState, string>> Mapping => new List<KeyValuePair<OkexMarginLoanState, string>>
        {
            new KeyValuePair<OkexMarginLoanState, string>(OkexMarginLoanState.Failed, "-2"),
            new KeyValuePair<OkexMarginLoanState, string>(OkexMarginLoanState.Cancelled, "-1"),
            new KeyValuePair<OkexMarginLoanState, string>(OkexMarginLoanState.Open, "0"),
            new KeyValuePair<OkexMarginLoanState, string>(OkexMarginLoanState.PartiallyFilled, "1"),
            new KeyValuePair<OkexMarginLoanState, string>(OkexMarginLoanState.FullyFilled, "2"),
            new KeyValuePair<OkexMarginLoanState, string>(OkexMarginLoanState.Submitting, "3"),
            new KeyValuePair<OkexMarginLoanState, string>(OkexMarginLoanState.Cancelling, "4"),
            new KeyValuePair<OkexMarginLoanState, string>(OkexMarginLoanState.Incomplete, "6"),
            new KeyValuePair<OkexMarginLoanState, string>(OkexMarginLoanState.Complete, "7"),
        };
    }
}