using CryptoExchange.Net.Converters;
using Okex.Net.Enums;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    public class MarginBillTypeConverter : BaseConverter<OkexMarginBillType>
    {
        public MarginBillTypeConverter() : this(true) { }
        public MarginBillTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OkexMarginBillType, string>> Mapping => new List<KeyValuePair<OkexMarginBillType, string>>
        {
            new KeyValuePair<OkexMarginBillType, string>(OkexMarginBillType.TokensBorrowed, "3"),
            new KeyValuePair<OkexMarginBillType, string>(OkexMarginBillType.TokensRepaid, "4"),
            new KeyValuePair<OkexMarginBillType, string>(OkexMarginBillType.InterestAccrued, "5"),
            new KeyValuePair<OkexMarginBillType, string>(OkexMarginBillType.Buy, "7"),
            new KeyValuePair<OkexMarginBillType, string>(OkexMarginBillType.Sell, "8"),
            new KeyValuePair<OkexMarginBillType, string>(OkexMarginBillType.FromFunding, "9"),
            new KeyValuePair<OkexMarginBillType, string>(OkexMarginBillType.FromC2C, "10"),
            new KeyValuePair<OkexMarginBillType, string>(OkexMarginBillType.FromSpot, "12"),
            new KeyValuePair<OkexMarginBillType, string>(OkexMarginBillType.ToFunding, "14"),
            new KeyValuePair<OkexMarginBillType, string>(OkexMarginBillType.ToC2C, "15"),
            new KeyValuePair<OkexMarginBillType, string>(OkexMarginBillType.ToSpot, "16"),
            new KeyValuePair<OkexMarginBillType, string>(OkexMarginBillType.AutoInterestPayment, "19"),
            new KeyValuePair<OkexMarginBillType, string>(OkexMarginBillType.LiquidationFees, "24"),
            new KeyValuePair<OkexMarginBillType, string>(OkexMarginBillType.RepayCandy, "59"),
            new KeyValuePair<OkexMarginBillType, string>(OkexMarginBillType.ToMargin, "61"),
            new KeyValuePair<OkexMarginBillType, string>(OkexMarginBillType.FromMargin, "62"),
        };
    }
}