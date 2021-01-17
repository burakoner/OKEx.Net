using CryptoExchange.Net.Converters;
using Okex.Net.Enums;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    public class AlgoOrderTypeConverter : BaseConverter<OkexAlgoOrderType>
    {
        public AlgoOrderTypeConverter() : this(true) { }
        public AlgoOrderTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OkexAlgoOrderType, string>> Mapping => new List<KeyValuePair<OkexAlgoOrderType, string>>
        {
            new KeyValuePair<OkexAlgoOrderType, string>(OkexAlgoOrderType.TriggerOrder, "1"),
            new KeyValuePair<OkexAlgoOrderType, string>(OkexAlgoOrderType.TrailOrder, "2"),
            new KeyValuePair<OkexAlgoOrderType, string>(OkexAlgoOrderType.IcebergOrder, "3"),
            new KeyValuePair<OkexAlgoOrderType, string>(OkexAlgoOrderType.TWAP, "4"),
            new KeyValuePair<OkexAlgoOrderType, string>(OkexAlgoOrderType.StopOrder, "5"),
        };
    }
}
