using CryptoExchange.Net.Converters;
using Okex.Net.Enums;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    internal class AlgoActualSideConverter : BaseConverter<OkexAlgoActualSide>
    {
        public AlgoActualSideConverter() : this(true) { }
        public AlgoActualSideConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OkexAlgoActualSide, string>> Mapping => new List<KeyValuePair<OkexAlgoActualSide, string>>
        {
            new KeyValuePair<OkexAlgoActualSide, string>(OkexAlgoActualSide.StopLoss, "sl"),
            new KeyValuePair<OkexAlgoActualSide, string>(OkexAlgoActualSide.TakeProfit, "tp"),
        };
    }
}