using CryptoExchange.Net.Converters;
using Okex.Net.Enums;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    public class TraderRoleConverter : BaseConverter<OkexTraderRole>
    {
        public TraderRoleConverter() : this(true) { }
        public TraderRoleConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OkexTraderRole, string>> Mapping => new List<KeyValuePair<OkexTraderRole, string>>
        {
            new KeyValuePair<OkexTraderRole, string>(OkexTraderRole.Taker, "T"),
            new KeyValuePair<OkexTraderRole, string>(OkexTraderRole.Maker, "M"),
        };
    }
}