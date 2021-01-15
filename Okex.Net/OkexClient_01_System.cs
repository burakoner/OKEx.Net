using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using Newtonsoft.Json;
using Okex.Net.Converters;
using Okex.Net.Enums;
using Okex.Net.Interfaces;
using Okex.Net.RestObjects;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Okex.Net
{
    public partial class OkexClient : IOkexClientSystem
    {
        #region 01 - General & System API Endpoints

        /// API server time. This is a public endpoint, no verification is required.
        /// </summary>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<OkexSystemTime> SystemTime(CancellationToken ct = default) => SystemTime_Async(ct).Result;
        /// <summary>
        /// API server time. This is a public endpoint, no verification is required.
        /// </summary>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<OkexSystemTime>> SystemTime_Async(CancellationToken ct = default)
        {
            return await SendRequest<OkexSystemTime>(GetUrl(Endpoints_General_Time), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets status of system upgrade and maintenance
        /// </summary>
        /// <param name="status">System maintenance status 0: waiting; 1: processing; 2: completed If this parameter is not filled, the data with status 0 and 1 will be returned by default</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<IEnumerable<OkexSystemStatus>> SystemStatus(OkexSystemMaintenanceStatus? status = null, CancellationToken ct = default) => SystemStatus_Async(status, ct).Result;
        /// <summary>
        /// Gets status of system upgrade and maintenance
        /// </summary>
        /// <param name="status">System maintenance status 0: waiting; 1: processing; 2: completed If this parameter is not filled, the data with status 0 and 1 will be returned by default</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<OkexSystemStatus>>> SystemStatus_Async(OkexSystemMaintenanceStatus? status = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            if (status != null) parameters.AddOptionalParameter("status", JsonConvert.SerializeObject(status, new SystemMaintenanceStatusConverter(false)));

            return await SendRequest<IEnumerable<OkexSystemStatus>>(GetUrl(Endpoints_System_Status), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }
        #endregion
    }
}
