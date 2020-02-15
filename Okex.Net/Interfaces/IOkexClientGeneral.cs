using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using Okex.Net.RestObjects;

namespace Okex.Net.Interfaces
{
	public interface IOkexClientGeneral
	{
		/// API server time. This is a public endpoint, no verification is required.
		/// </summary>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		WebCallResult<OkexGeneralServerTime> General_ServerTime(CancellationToken ct = default);
		/// <summary>
		/// API server time. This is a public endpoint, no verification is required.
		/// </summary>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		Task<WebCallResult<OkexGeneralServerTime>> General_ServerTime_Async(CancellationToken ct = default);
	}
}