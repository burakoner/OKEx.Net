
using CryptoExchange.Net.Converters;
using Okex.Net.Enums;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    internal class OptionsSettlementTypeConverter : BaseConverter<OkexOptionsSettlementType>
    {
        public OptionsSettlementTypeConverter() : this(true) { }
        public OptionsSettlementTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OkexOptionsSettlementType, string>> Mapping => new List<KeyValuePair<OkexOptionsSettlementType, string>>
        {
            new KeyValuePair<OkexOptionsSettlementType, string>(OkexOptionsSettlementType.Settled, "Settled"),
            new KeyValuePair<OkexOptionsSettlementType, string>(OkexOptionsSettlementType.Exercised, "Exercised"),
            new KeyValuePair<OkexOptionsSettlementType, string>(OkexOptionsSettlementType.ExpiredOTM, "Expired OTM"),
        };
    }
}