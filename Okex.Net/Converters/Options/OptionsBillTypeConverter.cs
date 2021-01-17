using CryptoExchange.Net.Converters;
using Okex.Net.Enums;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    public class OptionsBillTypeConverter : BaseConverter<OkexOptionsBillType>
    {
        public OptionsBillTypeConverter() : this(true) { }
        public OptionsBillTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OkexOptionsBillType, string>> Mapping => new List<KeyValuePair<OkexOptionsBillType, string>>
        {
            new KeyValuePair<OkexOptionsBillType, string>(OkexOptionsBillType.Transfer, "transfer"),
            new KeyValuePair<OkexOptionsBillType, string>(OkexOptionsBillType.Match, "match"),
            new KeyValuePair<OkexOptionsBillType, string>(OkexOptionsBillType.Fee, "fee"),
            new KeyValuePair<OkexOptionsBillType, string>(OkexOptionsBillType.Settlement, "settlement"),
            new KeyValuePair<OkexOptionsBillType, string>(OkexOptionsBillType.Liquidation, "liquidation"),
        };
    }
}