using CryptoExchange.Net.Objects;
using Okex.Net.Enums;
using Okex.Net.RestObjects;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Okex.Net.Interfaces
{
    public interface IOkexClientOracle
    {
        WebCallResult<OkexOracleData> Oracle_GetData(CancellationToken ct = default);
        Task<WebCallResult<OkexOracleData>> Oracle_GetData_Async(CancellationToken ct = default);
    }
}