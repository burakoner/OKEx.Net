using CryptoExchange.Net.Objects;
using Okex.Net.RestObjects;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Okex.Net
{
	public partial class OkexClient
	{
		#region General API
		/// API server time. This is a public endpoint, no verification is required.
		/// </summary>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public WebCallResult<OkexGeneralServerTime> General_ServerTime(CancellationToken ct = default) => General_ServerTime_Async(ct).Result;
		/// <summary>
		/// API server time. This is a public endpoint, no verification is required.
		/// </summary>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public async Task<WebCallResult<OkexGeneralServerTime>> General_ServerTime_Async(CancellationToken ct = default)
		{
			return await SendRequest<OkexGeneralServerTime>(GetUrl(Endpoints_General_Time), HttpMethod.Get, ct).ConfigureAwait(false);
		}
		#endregion
	}
}
