using CryptoExchange.Net.Converters;
using Okex.Net.Enums;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    internal class FuturesDeliverySettlementConverter : BaseConverter<OkexFuturesDeliverySettlement>
    {
        public FuturesDeliverySettlementConverter() : this(true) { }
        public FuturesDeliverySettlementConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OkexFuturesDeliverySettlement, string>> Mapping => new List<KeyValuePair<OkexFuturesDeliverySettlement, string>>
        {
            new KeyValuePair<OkexFuturesDeliverySettlement, string>(OkexFuturesDeliverySettlement.Delivery , "delivery"),
            new KeyValuePair<OkexFuturesDeliverySettlement, string>(OkexFuturesDeliverySettlement.Settlement, "settlement"),
        };
    }
}