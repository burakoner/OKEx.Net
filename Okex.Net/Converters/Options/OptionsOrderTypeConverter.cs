using CryptoExchange.Net.Converters;
using Okex.Net.Enums;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    public class OptionsOrderTypeConverter : BaseConverter<OkexOptionsOrderType>
    {
        public OptionsOrderTypeConverter() : this(true) { }
        public OptionsOrderTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OkexOptionsOrderType, string>> Mapping => new List<KeyValuePair<OkexOptionsOrderType, string>>
        {
            new KeyValuePair<OkexOptionsOrderType, string>(OkexOptionsOrderType.Buy, "1"),
            new KeyValuePair<OkexOptionsOrderType, string>(OkexOptionsOrderType.Sell, "2"),
            new KeyValuePair<OkexOptionsOrderType, string>(OkexOptionsOrderType.LiquidationBuy, "12"),
            new KeyValuePair<OkexOptionsOrderType, string>(OkexOptionsOrderType.LiquidationSell, "11"),
            new KeyValuePair<OkexOptionsOrderType, string>(OkexOptionsOrderType.PartialLiquidationBuy, "14"),
            new KeyValuePair<OkexOptionsOrderType, string>(OkexOptionsOrderType.PartialLiquidationSell, "13"),
            new KeyValuePair<OkexOptionsOrderType, string>(OkexOptionsOrderType.Sell, "2"),
        };
    }
}