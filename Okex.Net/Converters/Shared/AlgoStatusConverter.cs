using CryptoExchange.Net.Converters;
using Okex.Net.Enums;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    public class AlgoStatusConverter : BaseConverter<OkexAlgoStatus>
    {
        public AlgoStatusConverter() : this(true) { }
        public AlgoStatusConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OkexAlgoStatus, string>> Mapping => new List<KeyValuePair<OkexAlgoStatus, string>>
        {
            new KeyValuePair<OkexAlgoStatus, string>(OkexAlgoStatus.Pending, "1"),
            new KeyValuePair<OkexAlgoStatus, string>(OkexAlgoStatus.Effective, "2"),
            new KeyValuePair<OkexAlgoStatus, string>(OkexAlgoStatus.Cancelled, "3"),
            new KeyValuePair<OkexAlgoStatus, string>(OkexAlgoStatus.PartiallyEffective, "4"),
            new KeyValuePair<OkexAlgoStatus, string>(OkexAlgoStatus.Paused, "5"),
            new KeyValuePair<OkexAlgoStatus, string>(OkexAlgoStatus.OrderFailed, "6"),
        };
    }
}
