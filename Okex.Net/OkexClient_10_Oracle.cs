using CryptoExchange.Net.Objects;
using Okex.Net.Interfaces;
using Okex.Net.RestObjects;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Okex.Net
{
    public partial class OkexClient : IOkexClientOracle
    {
        #region 09 - Oracle

        /// <summary>
        /// Cryptographically signed prices available to be posted on-chain, using the Open Oracle standard.
        /// Rate Limit: 1 request per 5 seconds
        /// Notes:
        /// - Messages can be decoded using the Open Oracle Reporter library.
        /// - OKEx Oracle public key is 85615b076615317c80f14cbad6501eec031cd51c
        /// </summary>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<OkexOracleData> Oracle_GetData(CancellationToken ct = default) => Oracle_GetData_Async(ct).Result;
        /// <summary>
        /// Cryptographically signed prices available to be posted on-chain, using the Open Oracle standard.
        /// Rate Limit: 1 request per 5 seconds
        /// Notes:
        /// - Messages can be decoded using the Open Oracle Reporter library.
        /// - OKEx Oracle public key is 85615b076615317c80f14cbad6501eec031cd51c
        /// </summary>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<OkexOracleData>> Oracle_GetData_Async(CancellationToken ct = default)
        {
            return await SendRequestAsync<OkexOracleData>(GetUrl(Endpoints_Oracle), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        #endregion
    }
}
