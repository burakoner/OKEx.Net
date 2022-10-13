using CryptoExchange.Net.Converters;
using Okex.Net.Enums;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    internal class AlgoOrderTypeConverter : BaseConverter<OkexAlgoOrderType>
    {
        public AlgoOrderTypeConverter() : this(true) { }
        public AlgoOrderTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OkexAlgoOrderType, string>> Mapping => new List<KeyValuePair<OkexAlgoOrderType, string>>
        {
            new KeyValuePair<OkexAlgoOrderType, string>(OkexAlgoOrderType.Conditional, "conditional"),
            new KeyValuePair<OkexAlgoOrderType, string>(OkexAlgoOrderType.OCO, "oco"),
            new KeyValuePair<OkexAlgoOrderType, string>(OkexAlgoOrderType.Trigger, "trigger"),
            new KeyValuePair<OkexAlgoOrderType, string>(OkexAlgoOrderType.TrailingOrder, "move_order_stop"),
            new KeyValuePair<OkexAlgoOrderType, string>(OkexAlgoOrderType.Iceberg, "iceberg"),
            new KeyValuePair<OkexAlgoOrderType, string>(OkexAlgoOrderType.TWAP, "twap"),
        };
    }
}