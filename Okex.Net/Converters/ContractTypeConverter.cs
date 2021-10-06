using CryptoExchange.Net.Converters;
using Okex.Net.Enums;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    internal class ContractTypeConverter : BaseConverter<OkexContractType>
    {
        public ContractTypeConverter() : this(true) { }
        public ContractTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OkexContractType, string>> Mapping => new List<KeyValuePair<OkexContractType, string>>
        {
            new KeyValuePair<OkexContractType, string>(OkexContractType.Linear, "linear"),
            new KeyValuePair<OkexContractType, string>(OkexContractType.Inverse, "inverse"),
        };
    }
}