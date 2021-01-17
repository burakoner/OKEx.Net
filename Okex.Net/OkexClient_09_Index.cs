using CryptoExchange.Net.Objects;
using Okex.Net.Helpers;
using Okex.Net.Interfaces;
using Okex.Net.RestObjects;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Okex.Net
{
    public partial class OkexClient : IOkexClientIndex
    {
        #region 09 - Index API

        /// <summary>
        /// get constituents of index
        /// Rate limit: 20 20 requests per 2 seconds
        /// </summary>
        /// <param name="symbol">trading pair of index . for example, such as BTC/USD index is BTC-USD, BTC/USDT index is BTC-USDT, and EOS/BTC index is EOS-BTC</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<OkexIndexConstituents> Index_GetConstituents(string symbol, CancellationToken ct = default) => Index_GetConstituents_Async(symbol, ct).Result;
        /// <summary>
        /// get constituents of index
        /// Rate limit: 20 20 requests per 2 seconds
        /// </summary>
        /// <param name="symbol">trading pair of index . for example, such as BTC/USD index is BTC-USD, BTC/USDT index is BTC-USDT, and EOS/BTC index is EOS-BTC</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<OkexIndexConstituents>> Index_GetConstituents_Async(string symbol, CancellationToken ct = default)
        {
            symbol = symbol.ValidateSymbol();

            return await SendRequest<OkexIndexConstituents>(GetUrl(Endpoints_Index_Constituents, symbol), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        #endregion
    }
}
