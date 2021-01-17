using CryptoExchange.Net.Converters;
using Okex.Net.Enums;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    public class FuturesLiquidationStatusConverter : BaseConverter<OkexFuturesLiquidationStatus>
    {
        public FuturesLiquidationStatusConverter() : this(true) { }
        public FuturesLiquidationStatusConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OkexFuturesLiquidationStatus, string>> Mapping => new List<KeyValuePair<OkexFuturesLiquidationStatus, string>>
        {
            new KeyValuePair<OkexFuturesLiquidationStatus, string>(OkexFuturesLiquidationStatus.UnfilledInTheRecent7Days, "0"),
            new KeyValuePair<OkexFuturesLiquidationStatus, string>(OkexFuturesLiquidationStatus.FilledOrdersInTheRecent7Days, "1"),
        };
    }
}