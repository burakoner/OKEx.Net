using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using Okex.Net.Converters;
using Okex.Net.RestObjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Okex.Net.Helpers;
using System.Linq;
using Okex.Net.Enums;
using Okex.Net.Interfaces;

namespace Okex.Net
{
    public partial class OkexClient : IOkexClientContract
    {
        #region Contract Trading API

        #region Public Unsigned Endpoints

        /// <summary>
        /// This is a public endpoint, no identity verification is needed.
        /// Rate limit: 20 requests per 2 seconds
        /// </summary>
        /// <param name="currency">Token symbol, e.g. BTC</param>
        /// <param name="period">Bar size in seconds, default 300, must be one of [300/3600/86400] otherwise returns error</param>
        /// <param name="start">Start time in ISO 8601</param>
        /// <param name="end">End time in ISO 8601</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<IEnumerable<OkexContractRatio>> Contract_GetLongShortRatio(string currency, OkexContractPeriod period = OkexContractPeriod.FiveMinutes, DateTime? start = null, DateTime? end = null, CancellationToken ct = default) => Contract_GetLongShortRatio_Async(currency, period, start, end, ct).Result;
        /// <summary>
        /// This is a public endpoint, no identity verification is needed.
        /// Rate limit: 20 requests per 2 seconds
        /// </summary>
        /// <param name="currency">Token symbol, e.g. BTC</param>
        /// <param name="period">Bar size in seconds, default 300, must be one of [300/3600/86400] otherwise returns error</param>
        /// <param name="start">Start time in ISO 8601</param>
        /// <param name="end">End time in ISO 8601</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<OkexContractRatio>>> Contract_GetLongShortRatio_Async(string currency, OkexContractPeriod period = OkexContractPeriod.FiveMinutes, DateTime? start = null, DateTime? end = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "granularity", JsonConvert.SerializeObject(period, new ContractPeriodConverter(false)) },
            };
            parameters.AddOptionalParameter("start", start?.DateTimeToIso8601String());
            parameters.AddOptionalParameter("end", end?.DateTimeToIso8601String());

            return await SendRequest<IEnumerable<OkexContractRatio>>(GetUrl(Endpoints_Contract_LongShortRatio, currency), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// This is a public endpoint, no identity verification is needed.
        /// Rate limit: 20 requests per 2 seconds
        /// </summary>
        /// <param name="currency">Token symbol, e.g. BTC</param>
        /// <param name="period">Bar size in seconds, default 300, must be one of [300/3600/86400] otherwise returns error</param>
        /// <param name="start">Start time in ISO 8601</param>
        /// <param name="end">End time in ISO 8601</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<IEnumerable<OkexContractVolume>> Contract_GetVolume(string currency, OkexContractPeriod period = OkexContractPeriod.FiveMinutes, DateTime? start = null, DateTime? end = null, CancellationToken ct = default) => Contract_GetVolume_Async(currency, period, start, end, ct).Result;
        /// <summary>
        /// This is a public endpoint, no identity verification is needed.
        /// Rate limit: 20 requests per 2 seconds
        /// </summary>
        /// <param name="currency">Token symbol, e.g. BTC</param>
        /// <param name="period">Bar size in seconds, default 300, must be one of [300/3600/86400] otherwise returns error</param>
        /// <param name="start">Start time in ISO 8601</param>
        /// <param name="end">End time in ISO 8601</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<OkexContractVolume>>> Contract_GetVolume_Async(string currency, OkexContractPeriod period = OkexContractPeriod.FiveMinutes, DateTime? start = null, DateTime? end = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "granularity", JsonConvert.SerializeObject(period, new ContractPeriodConverter(false)) },
            };
            parameters.AddOptionalParameter("start", start?.DateTimeToIso8601String());
            parameters.AddOptionalParameter("end", end?.DateTimeToIso8601String());

            return await SendRequest<IEnumerable<OkexContractVolume>>(GetUrl(Endpoints_Contract_Volume, currency), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// This is a public endpoint, no identity verification is needed.
        /// Rate limit: 20 requests per 2 seconds
        /// </summary>
        /// <param name="currency">Token symbol, e.g. BTC</param>
        /// <param name="period">Bar size in seconds, default 300, must be one of [300/3600/86400] otherwise returns error</param>
        /// <param name="start">Start time in ISO 8601</param>
        /// <param name="end">End time in ISO 8601</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<IEnumerable<OkexContractTakerVolume>> Contract_GetTakerVolume(string currency, OkexContractPeriod period = OkexContractPeriod.FiveMinutes, DateTime? start = null, DateTime? end = null, CancellationToken ct = default) => Contract_GetTakerVolume_Async(currency, period, start, end, ct).Result;
        /// <summary>
        /// This is a public endpoint, no identity verification is needed.
        /// Rate limit: 20 requests per 2 seconds
        /// </summary>
        /// <param name="currency">Token symbol, e.g. BTC</param>
        /// <param name="period">Bar size in seconds, default 300, must be one of [300/3600/86400] otherwise returns error</param>
        /// <param name="start">Start time in ISO 8601</param>
        /// <param name="end">End time in ISO 8601</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<OkexContractTakerVolume>>> Contract_GetTakerVolume_Async(string currency, OkexContractPeriod period = OkexContractPeriod.FiveMinutes, DateTime? start = null, DateTime? end = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "granularity", JsonConvert.SerializeObject(period, new ContractPeriodConverter(false)) },
            };
            parameters.AddOptionalParameter("start", start?.DateTimeToIso8601String());
            parameters.AddOptionalParameter("end", end?.DateTimeToIso8601String());

            return await SendRequest<IEnumerable<OkexContractTakerVolume>>(GetUrl(Endpoints_Contract_Taker, currency), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// This is a public endpoint, no identity verification is needed.
        /// Rate limit: 20 requests per 2 seconds
        /// </summary>
        /// <param name="currency">Token symbol, e.g. BTC</param>
        /// <param name="period">Bar size in seconds, default 300, must be one of [300/3600/86400] otherwise returns error</param>
        /// <param name="start">Start time in ISO 8601</param>
        /// <param name="end">End time in ISO 8601</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<IEnumerable<OkexContractSentiment>> Contract_GetSentiment(string currency, OkexContractPeriod period = OkexContractPeriod.FiveMinutes, DateTime? start = null, DateTime? end = null, CancellationToken ct = default) => Contract_GetSentiment_Async(currency, period, start, end, ct).Result;
        /// <summary>
        /// This is a public endpoint, no identity verification is needed.
        /// Rate limit: 20 requests per 2 seconds
        /// </summary>
        /// <param name="currency">Token symbol, e.g. BTC</param>
        /// <param name="period">Bar size in seconds, default 300, must be one of [300/3600/86400] otherwise returns error</param>
        /// <param name="start">Start time in ISO 8601</param>
        /// <param name="end">End time in ISO 8601</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<OkexContractSentiment>>> Contract_GetSentiment_Async(string currency, OkexContractPeriod period = OkexContractPeriod.FiveMinutes, DateTime? start = null, DateTime? end = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "granularity", JsonConvert.SerializeObject(period, new ContractPeriodConverter(false)) },
            };
            parameters.AddOptionalParameter("start", start?.DateTimeToIso8601String());
            parameters.AddOptionalParameter("end", end?.DateTimeToIso8601String());

            return await SendRequest<IEnumerable<OkexContractSentiment>>(GetUrl(Endpoints_Contract_Sentiment, currency), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// This is a public endpoint, no identity verification is needed.
        /// Rate limit: 20 requests per 2 seconds
        /// </summary>
        /// <param name="currency">Token symbol, e.g. BTC</param>
        /// <param name="period">Bar size in seconds, default 300, must be one of [300/3600/86400] otherwise returns error</param>
        /// <param name="start">Start time in ISO 8601</param>
        /// <param name="end">End time in ISO 8601</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<IEnumerable<OkexContractMargin>> Contract_GetMargin(string currency, OkexContractPeriod period = OkexContractPeriod.FiveMinutes, DateTime? start = null, DateTime? end = null, CancellationToken ct = default) => Contract_GetMargin_Async(currency, period, start, end, ct).Result;
        /// <summary>
        /// This is a public endpoint, no identity verification is needed.
        /// Rate limit: 20 requests per 2 seconds
        /// </summary>
        /// <param name="currency">Token symbol, e.g. BTC</param>
        /// <param name="period">Bar size in seconds, default 300, must be one of [300/3600/86400] otherwise returns error</param>
        /// <param name="start">Start time in ISO 8601</param>
        /// <param name="end">End time in ISO 8601</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<OkexContractMargin>>> Contract_GetMargin_Async(string currency, OkexContractPeriod period = OkexContractPeriod.FiveMinutes, DateTime? start = null, DateTime? end = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "granularity", JsonConvert.SerializeObject(period, new ContractPeriodConverter(false)) },
            };
            parameters.AddOptionalParameter("start", start?.DateTimeToIso8601String());
            parameters.AddOptionalParameter("end", end?.DateTimeToIso8601String());

            return await SendRequest<IEnumerable<OkexContractMargin>>(GetUrl(Endpoints_Contract_Margin, currency), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        #endregion

        #endregion
    }
}