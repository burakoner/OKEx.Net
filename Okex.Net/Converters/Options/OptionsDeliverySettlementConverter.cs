using CryptoExchange.Net.Converters;
using Okex.Net.Enums;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    internal class OptionsDeliverySettlementConverter : BaseConverter<OkexOptionsDeliverySettlement>
    {
        public OptionsDeliverySettlementConverter() : this(true) { }
        public OptionsDeliverySettlementConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OkexOptionsDeliverySettlement, string>> Mapping => new List<KeyValuePair<OkexOptionsDeliverySettlement, string>>
        {
            new KeyValuePair<OkexOptionsDeliverySettlement, string>(OkexOptionsDeliverySettlement.Delivery , "delivery"),
            new KeyValuePair<OkexOptionsDeliverySettlement, string>(OkexOptionsDeliverySettlement.Settlement, "settlement"),
        };
    }
}