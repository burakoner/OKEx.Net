using CryptoExchange.Net.Objects;
using Okex.Net.Enums;
using Okex.Net.RestObjects;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Okex.Net.Interfaces
{
    public interface IOkexClientIndex
    {
        WebCallResult<OkexIndexConstituents> Index_GetConstituents(string symbol, CancellationToken ct = default);
        Task<WebCallResult<OkexIndexConstituents>> Index_GetConstituents_Async(string symbol, CancellationToken ct = default);
    }
}