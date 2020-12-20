using CryptoExchange.Net.Objects;
using Okex.Net.Enums;
using Okex.Net.RestObjects;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Okex.Net.Interfaces
{
    public interface IOkexClientContract
    {
        WebCallResult<IEnumerable<OkexContractRatio>> Contract_GetLongShortRatio(string currency, OkexContractPeriod period = OkexContractPeriod.FiveMinutes, DateTime? start = null, DateTime? end = null, CancellationToken ct = default);
        Task<WebCallResult<IEnumerable<OkexContractRatio>>> Contract_GetLongShortRatio_Async(string currency, OkexContractPeriod period = OkexContractPeriod.FiveMinutes, DateTime? start = null, DateTime? end = null, CancellationToken ct = default);
        WebCallResult<IEnumerable<OkexContractMargin>> Contract_GetMargin(string currency, OkexContractPeriod period = OkexContractPeriod.FiveMinutes, DateTime? start = null, DateTime? end = null, CancellationToken ct = default);
        Task<WebCallResult<IEnumerable<OkexContractMargin>>> Contract_GetMargin_Async(string currency, OkexContractPeriod period = OkexContractPeriod.FiveMinutes, DateTime? start = null, DateTime? end = null, CancellationToken ct = default);
        WebCallResult<IEnumerable<OkexContractSentiment>> Contract_GetSentiment(string currency, OkexContractPeriod period = OkexContractPeriod.FiveMinutes, DateTime? start = null, DateTime? end = null, CancellationToken ct = default);
        Task<WebCallResult<IEnumerable<OkexContractSentiment>>> Contract_GetSentiment_Async(string currency, OkexContractPeriod period = OkexContractPeriod.FiveMinutes, DateTime? start = null, DateTime? end = null, CancellationToken ct = default);
        WebCallResult<IEnumerable<OkexContractTakerVolume>> Contract_GetTakerVolume(string currency, OkexContractPeriod period = OkexContractPeriod.FiveMinutes, DateTime? start = null, DateTime? end = null, CancellationToken ct = default);
        Task<WebCallResult<IEnumerable<OkexContractTakerVolume>>> Contract_GetTakerVolume_Async(string currency, OkexContractPeriod period = OkexContractPeriod.FiveMinutes, DateTime? start = null, DateTime? end = null, CancellationToken ct = default);
        WebCallResult<IEnumerable<OkexContractVolume>> Contract_GetVolume(string currency, OkexContractPeriod period = OkexContractPeriod.FiveMinutes, DateTime? start = null, DateTime? end = null, CancellationToken ct = default);
        Task<WebCallResult<IEnumerable<OkexContractVolume>>> Contract_GetVolume_Async(string currency, OkexContractPeriod period = OkexContractPeriod.FiveMinutes, DateTime? start = null, DateTime? end = null, CancellationToken ct = default);
    }
}