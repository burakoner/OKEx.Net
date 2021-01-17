using CryptoExchange.Net.Converters;
using Okex.Net.Enums;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    public class FuturesBillTypeConverter : BaseConverter<OkexFuturesBillType>
    {
        public FuturesBillTypeConverter() : this(true) { }
        public FuturesBillTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OkexFuturesBillType, string>> Mapping => new List<KeyValuePair<OkexFuturesBillType, string>>
        {
            new KeyValuePair<OkexFuturesBillType, string>(OkexFuturesBillType.OpenLong, "1"),
            new KeyValuePair<OkexFuturesBillType, string>(OkexFuturesBillType.OpenShort, "2"),
            new KeyValuePair<OkexFuturesBillType, string>(OkexFuturesBillType.CloseLong, "3"),
            new KeyValuePair<OkexFuturesBillType, string>(OkexFuturesBillType.CloseShort, "4"),
            new KeyValuePair<OkexFuturesBillType, string>(OkexFuturesBillType.TransactionFee, "5"),
            new KeyValuePair<OkexFuturesBillType, string>(OkexFuturesBillType.TransferIn, "6"),
            new KeyValuePair<OkexFuturesBillType, string>(OkexFuturesBillType.TransferOut, "7"),
            new KeyValuePair<OkexFuturesBillType, string>(OkexFuturesBillType.SettledRPL, "8"),
            new KeyValuePair<OkexFuturesBillType, string>(OkexFuturesBillType.FullLiquidationOfLong, "13"),
            new KeyValuePair<OkexFuturesBillType, string>(OkexFuturesBillType.FullLiquidationOfShort, "14"),
            new KeyValuePair<OkexFuturesBillType, string>(OkexFuturesBillType.DeliveryLong, "15"),
            new KeyValuePair<OkexFuturesBillType, string>(OkexFuturesBillType.DeliveryShort, "16"),
            new KeyValuePair<OkexFuturesBillType, string>(OkexFuturesBillType.SettledUPLLong, "17"),
            new KeyValuePair<OkexFuturesBillType, string>(OkexFuturesBillType.SettledUPLShort, "18"),
            new KeyValuePair<OkexFuturesBillType, string>(OkexFuturesBillType.PartialLiquidationOfShort, "20"),
            new KeyValuePair<OkexFuturesBillType, string>(OkexFuturesBillType.PartialLiquidationOfLong, "21"),
        };
    }
}