using CryptoExchange.Net.Converters;
using Okex.Net.Enums;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    public class SystemMaintenanceStatusConverter : BaseConverter<OkexSystemMaintenanceStatus>
    {
        public SystemMaintenanceStatusConverter() : this(true) { }
        public SystemMaintenanceStatusConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OkexSystemMaintenanceStatus, string>> Mapping => new List<KeyValuePair<OkexSystemMaintenanceStatus, string>>
        {
            new KeyValuePair<OkexSystemMaintenanceStatus, string>(OkexSystemMaintenanceStatus.Waiting, "0"),
            new KeyValuePair<OkexSystemMaintenanceStatus, string>(OkexSystemMaintenanceStatus.Processing, "1"),
            new KeyValuePair<OkexSystemMaintenanceStatus, string>(OkexSystemMaintenanceStatus.Completed, "2"),
        };
    }
}