using CryptoExchange.Net.Converters;
using Okex.Net.Enums;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    public class SwapLiquidationStatusConverter : BaseConverter<OkexSwapLiquidationStatus>
    {
        public SwapLiquidationStatusConverter() : this(true) { }
        public SwapLiquidationStatusConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OkexSwapLiquidationStatus, string>> Mapping => new List<KeyValuePair<OkexSwapLiquidationStatus, string>>
        {
            new KeyValuePair<OkexSwapLiquidationStatus, string>(OkexSwapLiquidationStatus.UnfilledInTheRecent7Days, "0"),
            new KeyValuePair<OkexSwapLiquidationStatus, string>(OkexSwapLiquidationStatus.FilledOrdersInTheRecent7Days, "1"),
        };
    }
}