using CryptoExchange.Net.Objects;
using Okex.Net.Enums;
using Okex.Net.RestObjects;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Okex.Net.Interfaces
{
    public interface IOkexClientSystem
    {
        WebCallResult<IEnumerable<OkexSystemStatus>> SystemStatus(OkexSystemMaintenanceStatus? status = null, CancellationToken ct = default);
        Task<WebCallResult<IEnumerable<OkexSystemStatus>>> SystemStatus_Async(OkexSystemMaintenanceStatus? status = null, CancellationToken ct = default);
        WebCallResult<OkexSystemTime> SystemTime(CancellationToken ct = default);
        Task<WebCallResult<OkexSystemTime>> SystemTime_Async(CancellationToken ct = default);
    }
}