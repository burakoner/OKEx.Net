using CryptoExchange.Net.Converters;
using Okex.Net.Enums;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    internal class ContractPeriodConverter : BaseConverter<OkexContractPeriod>
    {
        public ContractPeriodConverter() : this(true) { }
        public ContractPeriodConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OkexContractPeriod, string>> Mapping => new List<KeyValuePair<OkexContractPeriod, string>>
        {
            new KeyValuePair<OkexContractPeriod, string>(OkexContractPeriod.FiveMinutes, "300"),
            new KeyValuePair<OkexContractPeriod, string>(OkexContractPeriod.OneHour, "3600"),
            new KeyValuePair<OkexContractPeriod, string>(OkexContractPeriod.OneDay, "86400"),
        };
    }
}