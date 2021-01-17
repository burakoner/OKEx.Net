using CryptoExchange.Net.Converters;
using Okex.Net.Enums;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    public class SwapBillTypeConverter : BaseConverter<OkexSwapBillType>
    {
        public SwapBillTypeConverter() : this(true) { }
        public SwapBillTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OkexSwapBillType, string>> Mapping => new List<KeyValuePair<OkexSwapBillType, string>>
        {
            new KeyValuePair<OkexSwapBillType, string>(OkexSwapBillType.OpenLong, "1"),
            new KeyValuePair<OkexSwapBillType, string>(OkexSwapBillType.OpenShort, "2"),
            new KeyValuePair<OkexSwapBillType, string>(OkexSwapBillType.CloseLong, "3"),
            new KeyValuePair<OkexSwapBillType, string>(OkexSwapBillType.CloseShort, "4"),
            new KeyValuePair<OkexSwapBillType, string>(OkexSwapBillType.TransferIn, "5"),
            new KeyValuePair<OkexSwapBillType, string>(OkexSwapBillType.TransferOut, "6"),
            new KeyValuePair<OkexSwapBillType, string>(OkexSwapBillType.SettledUPL, "7"),
            new KeyValuePair<OkexSwapBillType, string>(OkexSwapBillType.Clawback, "8"),
            new KeyValuePair<OkexSwapBillType, string>(OkexSwapBillType.InsuranceFund, "9"),
            new KeyValuePair<OkexSwapBillType, string>(OkexSwapBillType.FullLiquidationOfLong, "10"),
            new KeyValuePair<OkexSwapBillType, string>(OkexSwapBillType.FullLiquidationOfShort, "11"),
            new KeyValuePair<OkexSwapBillType, string>(OkexSwapBillType.FundingFee, "14"),
            new KeyValuePair<OkexSwapBillType, string>(OkexSwapBillType.ManuallyAddMargin, "15"),
            new KeyValuePair<OkexSwapBillType, string>(OkexSwapBillType.ManuallyReduceMargin, "16"),
            new KeyValuePair<OkexSwapBillType, string>(OkexSwapBillType.AutoMargin, "17"),
            new KeyValuePair<OkexSwapBillType, string>(OkexSwapBillType.SwitchMarginMode, "18"),
            new KeyValuePair<OkexSwapBillType, string>(OkexSwapBillType.PartialLiquidationOfLong, "19"),
            new KeyValuePair<OkexSwapBillType, string>(OkexSwapBillType.PartialLiquidationOfShort, "20"),
            new KeyValuePair<OkexSwapBillType, string>(OkexSwapBillType.MarginAddedWithLoweredLeverage, "21"),
            new KeyValuePair<OkexSwapBillType, string>(OkexSwapBillType.SettledRP, "22"),
        };
    }
}