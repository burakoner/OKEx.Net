using CryptoExchange.Net.Converters;
using Okex.Net.Enums;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    public class SystemMaintenanceProductConverter : BaseConverter<OkexSystemMaintenanceProduct>
    {
        public SystemMaintenanceProductConverter() : this(true) { }
        public SystemMaintenanceProductConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OkexSystemMaintenanceProduct, string>> Mapping => new List<KeyValuePair<OkexSystemMaintenanceProduct, string>>
        {
            new KeyValuePair<OkexSystemMaintenanceProduct, string>(OkexSystemMaintenanceProduct.WebSocket, "0"),
            new KeyValuePair<OkexSystemMaintenanceProduct, string>(OkexSystemMaintenanceProduct.SpotMargin, "1"),
            new KeyValuePair<OkexSystemMaintenanceProduct, string>(OkexSystemMaintenanceProduct.Futures, "2"),
            new KeyValuePair<OkexSystemMaintenanceProduct, string>(OkexSystemMaintenanceProduct.Perpetual, "3"),
            new KeyValuePair<OkexSystemMaintenanceProduct, string>(OkexSystemMaintenanceProduct.Options, "4"),
        };
    }
}