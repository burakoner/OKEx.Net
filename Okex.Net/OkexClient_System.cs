using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using Newtonsoft.Json;
using Okex.Net.Converters;
using Okex.Net.CoreObjects;
using Okex.Net.Enums;
using Okex.Net.RestObjects.System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Okex.Net
{
    public partial class OkexClient
    {
        #region System API Endpoints
        /// <summary>
        /// Get event status of system upgrade
        /// </summary>
        /// <param name="state">System maintenance status,scheduled: waiting; ongoing: processing; completed: completed ;canceled: canceled. If this parameter is not filled, the data with status scheduled and ongoing will be returned by default</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<IEnumerable<OkexStatus>> GetSystemStatus(OkexMaintenanceState? state = null, CancellationToken ct = default) => GetSystemStatus_Async(state, ct).Result;
        /// <summary>
        /// Get event status of system upgrade
        /// </summary>
        /// <param name="state">System maintenance status,scheduled: waiting; ongoing: processing; completed: completed ;canceled: canceled. If this parameter is not filled, the data with status scheduled and ongoing will be returned by default</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<IEnumerable<OkexStatus>>> GetSystemStatus_Async(OkexMaintenanceState? state = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            if (state != null) parameters.AddOptionalParameter("state", JsonConvert.SerializeObject(state, new MaintenanceStateConverter(false)));

            var result = await SendRequestAsync<OkexApiResponse<IEnumerable<OkexStatus>>>(GetUrl(Endpoints_V5_System_Status), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<IEnumerable<OkexStatus>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<IEnumerable<OkexStatus>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage));

            return new WebCallResult<IEnumerable<OkexStatus>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }
        #endregion
    }
}
