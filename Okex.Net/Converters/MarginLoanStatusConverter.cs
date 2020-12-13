using CryptoExchange.Net.Converters;
using Okex.Net.Enums;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    internal class MarginLoanStatusConverter : BaseConverter<OkexMarginLoanStatus>
    {
        public MarginLoanStatusConverter() : this(true) { }
        public MarginLoanStatusConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OkexMarginLoanStatus, string>> Mapping => new List<KeyValuePair<OkexMarginLoanStatus, string>>
        {
            new KeyValuePair<OkexMarginLoanStatus, string>(OkexMarginLoanStatus.Outstanding, "0"),
            new KeyValuePair<OkexMarginLoanStatus, string>(OkexMarginLoanStatus.Repaid, "1"),
        };
    }
}