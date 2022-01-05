using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using Newtonsoft.Json;
using Okex.Net.Converters;
using Okex.Net.CoreObjects;
using Okex.Net.Enums;
using Okex.Net.Helpers;
using Okex.Net.RestObjects.Trading;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Okex.Net
{
    public partial class OkexClient
    {
        #region Trading API Endpoints
        /// <summary>
        /// Get the currency supported by the transaction big data interface
        /// </summary>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<OkexSupportCoins> GetRubikSupportCoin(CancellationToken ct = default) => GetRubikSupportCoin_Async(ct).Result;
        /// <summary>
        /// Get the currency supported by the transaction big data interface
        /// </summary>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<OkexSupportCoins>> GetRubikSupportCoin_Async(CancellationToken ct = default)
        {
            var result = await SendRequestAsync<OkexRestApiResponse<OkexSupportCoins>>(GetUrl(Endpoints_V5_RubikStat_TradingDataSupportCoin), HttpMethod.Get, ct).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<OkexSupportCoins>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<OkexSupportCoins>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage, result.Data.Data));

            return new WebCallResult<OkexSupportCoins>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }

        /// <summary>
        /// This is the taker volume for both buyers and sellers. This shows the influx and exit of funds in and out of {coin}.
        /// </summary>
        /// <param name="currency">Currency</param>
        /// <param name="instrumentType">Instrument Type</param>
        /// <param name="period">period, the default is 5m, e.g. [5m/1H/1D]</param>
        /// <param name="begin">begin, e.g. 1597026383085</param>
        /// <param name="end">end, e.g. 1597026383011</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<IEnumerable<OkexTakerVolume>> GetRubikTakerVolume(
            string currency,
            OkexInstrumentType instrumentType,
            OkexPeriod period = OkexPeriod.FiveMinutes,
            long? begin = null,
            long? end = null,
            CancellationToken ct = default) => GetRubikTakerVolume_Async(currency, instrumentType, period, begin, end, ct).Result;
        /// <summary>
        /// This is the taker volume for both buyers and sellers. This shows the influx and exit of funds in and out of {coin}.
        /// </summary>
        /// <param name="currency">Currency</param>
        /// <param name="instrumentType">Instrument Type</param>
        /// <param name="period">period, the default is 5m, e.g. [5m/1H/1D]</param>
        /// <param name="begin">begin, e.g. 1597026383085</param>
        /// <param name="end">end, e.g. 1597026383011</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<IEnumerable<OkexTakerVolume>>> GetRubikTakerVolume_Async(
            string currency,
            OkexInstrumentType instrumentType,
            OkexPeriod period = OkexPeriod.FiveMinutes,
            long? begin = null,
            long? end = null,
            CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> {
                { "ccy", currency},
                { "instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)) },
                { "period", JsonConvert.SerializeObject(period, new PeriodConverter(false)) },
            };
            parameters.AddOptionalParameter("begin", begin?.ToString(OkexGlobals.OkexCultureInfo));
            parameters.AddOptionalParameter("end", end?.ToString(OkexGlobals.OkexCultureInfo));

            var result = await SendRequestAsync<OkexRestApiResponse<IEnumerable<OkexTakerVolume>>>(GetUrl(Endpoints_V5_RubikStat_TakerVolume), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<IEnumerable<OkexTakerVolume>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<IEnumerable<OkexTakerVolume>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage, result.Data.Data));

            return new WebCallResult<IEnumerable<OkexTakerVolume>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }

        /// <summary>
        /// This indicator shows the ratio of cumulative data value between currency pair leverage quote currency and underlying asset over a given period of time.
        /// </summary>
        /// <param name="currency">Currency</param>
        /// <param name="period">period, the default is 5m, e.g. [5m/1H/1D]</param>
        /// <param name="begin">begin, e.g. 1597026383085</param>
        /// <param name="end">end, e.g. 1597026383085</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<IEnumerable<OkexRatio>> GetRubikMarginLendingRatio(
            string currency,
            OkexPeriod period = OkexPeriod.FiveMinutes,
            long? begin = null,
            long? end = null,
            CancellationToken ct = default) => GetRubikMarginLendingRatio_Async(currency, period, begin, end, ct).Result;
        /// <summary>
        /// This indicator shows the ratio of cumulative data value between currency pair leverage quote currency and underlying asset over a given period of time.
        /// </summary>
        /// <param name="currency">Currency</param>
        /// <param name="period">period, the default is 5m, e.g. [5m/1H/1D]</param>
        /// <param name="begin">begin, e.g. 1597026383085</param>
        /// <param name="end">end, e.g. 1597026383085</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<IEnumerable<OkexRatio>>> GetRubikMarginLendingRatio_Async(
            string currency,
            OkexPeriod period = OkexPeriod.FiveMinutes,
            long? begin = null,
            long? end = null,
            CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> {
                { "ccy", currency},
                { "period", JsonConvert.SerializeObject(period, new PeriodConverter(false)) },
            };
            parameters.AddOptionalParameter("begin", begin?.ToString(OkexGlobals.OkexCultureInfo));
            parameters.AddOptionalParameter("end", end?.ToString(OkexGlobals.OkexCultureInfo));

            var result = await SendRequestAsync<OkexRestApiResponse<IEnumerable<OkexRatio>>>(GetUrl(Endpoints_V5_RubikStat_MarginLoanRatio), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<IEnumerable<OkexRatio>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<IEnumerable<OkexRatio>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage, result.Data.Data));

            return new WebCallResult<IEnumerable<OkexRatio>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }

        /// <summary>
        /// This is the ratio of users with net long vs short positions. It includes data from futures and perpetual swaps.
        /// </summary>
        /// <param name="currency">Currency</param>
        /// <param name="period">period, the default is 5m, e.g. [5m/1H/1D]</param>
        /// <param name="begin">begin, e.g. 1597026383085</param>
        /// <param name="end">end, e.g. 1597026383011</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<IEnumerable<OkexRatio>> GetRubikLongShortRatio(
            string currency,
            OkexPeriod period = OkexPeriod.FiveMinutes,
            long? begin = null,
            long? end = null,
            CancellationToken ct = default) => GetRubikLongShortRatio_Async(currency, period, begin, end, ct).Result;
        /// <summary>
        /// This is the ratio of users with net long vs short positions. It includes data from futures and perpetual swaps.
        /// </summary>
        /// <param name="currency">Currency</param>
        /// <param name="period">period, the default is 5m, e.g. [5m/1H/1D]</param>
        /// <param name="begin">begin, e.g. 1597026383085</param>
        /// <param name="end">end, e.g. 1597026383011</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<IEnumerable<OkexRatio>>> GetRubikLongShortRatio_Async(
            string currency,
            OkexPeriod period = OkexPeriod.FiveMinutes,
            long? begin = null,
            long? end = null,
            CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> {
                { "ccy", currency},
                { "period", JsonConvert.SerializeObject(period, new PeriodConverter(false)) },
            };
            parameters.AddOptionalParameter("begin", begin?.ToString(OkexGlobals.OkexCultureInfo));
            parameters.AddOptionalParameter("end", end?.ToString(OkexGlobals.OkexCultureInfo));

            var result = await SendRequestAsync<OkexRestApiResponse<IEnumerable<OkexRatio>>>(GetUrl(Endpoints_V5_RubikStat_ContractsLongShortAccountRatio), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<IEnumerable<OkexRatio>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<IEnumerable<OkexRatio>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage, result.Data.Data));

            return new WebCallResult<IEnumerable<OkexRatio>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }

        /// <summary>
        /// Open interest is the sum of all long and short futures and perpetual swap positions.
        /// </summary>
        /// <param name="currency">Currency</param>
        /// <param name="period">period, the default is 5m, e.g. [5m/1H/1D]</param>
        /// <param name="begin">begin, e.g. 1597026383085</param>
        /// <param name="end">end, e.g. 1597026383011</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<IEnumerable<OkexInterestVolume>> GetRubikContractSummary(
            string currency,
            OkexPeriod period = OkexPeriod.FiveMinutes,
            long? begin = null,
            long? end = null,
            CancellationToken ct = default) => GetRubikContractSummary_Async(currency, period, begin, end, ct).Result;
        /// <summary>
        /// Open interest is the sum of all long and short futures and perpetual swap positions.
        /// </summary>
        /// <param name="currency">Currency</param>
        /// <param name="period">period, the default is 5m, e.g. [5m/1H/1D]</param>
        /// <param name="begin">begin, e.g. 1597026383085</param>
        /// <param name="end">end, e.g. 1597026383011</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<IEnumerable<OkexInterestVolume>>> GetRubikContractSummary_Async(
            string currency,
            OkexPeriod period = OkexPeriod.FiveMinutes,
            long? begin = null,
            long? end = null,
            CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> {
                { "ccy", currency},
                { "period", JsonConvert.SerializeObject(period, new PeriodConverter(false)) },
            };
            parameters.AddOptionalParameter("begin", begin?.ToString(OkexGlobals.OkexCultureInfo));
            parameters.AddOptionalParameter("end", end?.ToString(OkexGlobals.OkexCultureInfo));

            var result = await SendRequestAsync<OkexRestApiResponse<IEnumerable<OkexInterestVolume>>>(GetUrl(Endpoints_V5_RubikStat_ContractsOpenInterestVolume), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<IEnumerable<OkexInterestVolume>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<IEnumerable<OkexInterestVolume>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage, result.Data.Data));

            return new WebCallResult<IEnumerable<OkexInterestVolume>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }

        /// <summary>
        /// This shows the sum of all open positions and how much total trading volume has taken place.
        /// </summary>
        /// <param name="currency">Currency</param>
        /// <param name="period">period, the default is 8H. e.g. [8H/1D]</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<IEnumerable<OkexInterestVolume>> GetRubikOptionsSummary(
            string currency,
            OkexPeriod period = OkexPeriod.FiveMinutes,
            CancellationToken ct = default) => GetRubikOptionsSummary_Async(currency, period, ct).Result;
        /// <summary>
        /// This shows the sum of all open positions and how much total trading volume has taken place.
        /// </summary>
        /// <param name="currency">Currency</param>
        /// <param name="period">period, the default is 8H. e.g. [8H/1D]</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<IEnumerable<OkexInterestVolume>>> GetRubikOptionsSummary_Async(
            string currency,
            OkexPeriod period = OkexPeriod.FiveMinutes,
            CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> {
                { "ccy", currency},
                { "period", JsonConvert.SerializeObject(period, new PeriodConverter(false)) },
            };

            var result = await SendRequestAsync<OkexRestApiResponse<IEnumerable<OkexInterestVolume>>>(GetUrl(Endpoints_V5_RubikStat_OptionOpenInterestVolume), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<IEnumerable<OkexInterestVolume>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<IEnumerable<OkexInterestVolume>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage, result.Data.Data));

            return new WebCallResult<IEnumerable<OkexInterestVolume>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }

        /// <summary>
        /// This shows the relative buy/sell volume for calls and puts. It shows whether traders are bullish or bearish on price and volatility.
        /// </summary>
        /// <param name="currency">Currency</param>
        /// <param name="period">period, the default is 8H. e.g. [8H/1D]</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<IEnumerable<OkexPutCallRatio>> GetRubikPutCallRatio(
            string currency,
            OkexPeriod period = OkexPeriod.FiveMinutes,
            CancellationToken ct = default) => GetRubikPutCallRatio_Async(currency, period, ct).Result;
        /// <summary>
        /// This shows the relative buy/sell volume for calls and puts. It shows whether traders are bullish or bearish on price and volatility.
        /// </summary>
        /// <param name="currency">Currency</param>
        /// <param name="period">period, the default is 8H. e.g. [8H/1D]</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<IEnumerable<OkexPutCallRatio>>> GetRubikPutCallRatio_Async(
            string currency,
            OkexPeriod period = OkexPeriod.FiveMinutes,
            CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> {
                { "ccy", currency},
                { "period", JsonConvert.SerializeObject(period, new PeriodConverter(false)) },
            };

            var result = await SendRequestAsync<OkexRestApiResponse<IEnumerable<OkexPutCallRatio>>>(GetUrl(Endpoints_V5_RubikStat_OptionOpenInterestVolumeRatio), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<IEnumerable<OkexPutCallRatio>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<IEnumerable<OkexPutCallRatio>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage, result.Data.Data));

            return new WebCallResult<IEnumerable<OkexPutCallRatio>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }

        /// <summary>
        /// This shows the volume and open interest for each upcoming expiration. You can use this to see which expirations are currently the most popular to trade.
        /// </summary>
        /// <param name="currency">Currency</param>
        /// <param name="period">period, the default is 8H. e.g. [8H/1D]</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<IEnumerable<OkexInterestVolumeExpiry>> GetRubikInterestVolumeExpiry(
            string currency,
            OkexPeriod period = OkexPeriod.FiveMinutes,
            CancellationToken ct = default) => GetRubikInterestVolumeExpiry_Async(currency, period, ct).Result;
        /// <summary>
        /// This shows the volume and open interest for each upcoming expiration. You can use this to see which expirations are currently the most popular to trade.
        /// </summary>
        /// <param name="currency">Currency</param>
        /// <param name="period">period, the default is 8H. e.g. [8H/1D]</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<IEnumerable<OkexInterestVolumeExpiry>>> GetRubikInterestVolumeExpiry_Async(
            string currency,
            OkexPeriod period = OkexPeriod.FiveMinutes,
            CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> {
                { "ccy", currency},
                { "period", JsonConvert.SerializeObject(period, new PeriodConverter(false)) },
            };

            var result = await SendRequestAsync<OkexRestApiResponse<IEnumerable<OkexInterestVolumeExpiry>>>(GetUrl(Endpoints_V5_RubikStat_OptionOpenInterestVolumeExpiry), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<IEnumerable<OkexInterestVolumeExpiry>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<IEnumerable<OkexInterestVolumeExpiry>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage, result.Data.Data));

            return new WebCallResult<IEnumerable<OkexInterestVolumeExpiry>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }

        /// <summary>
        /// This shows what option strikes are the most popular for each expiration.
        /// </summary>
        /// <param name="currency">Currency</param>
        /// <param name="expiryTime">expiry time (Format: YYYYMMdd, for example: "20210623")</param>
        /// <param name="period">period, the default is 8H. e.g. [8H/1D]</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<IEnumerable<OkexInterestVolumeStrike>> GetRubikInterestVolumeStrike(
            string currency,
            string expiryTime,
            OkexPeriod period = OkexPeriod.FiveMinutes,
            CancellationToken ct = default) => GetRubikInterestVolumeStrike_Async(currency, expiryTime, period, ct).Result;
        /// <summary>
        /// This shows what option strikes are the most popular for each expiration.
        /// </summary>
        /// <param name="currency">Currency</param>
        /// <param name="expiryTime">expiry time (Format: YYYYMMdd, for example: "20210623")</param>
        /// <param name="period">period, the default is 8H. e.g. [8H/1D]</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<IEnumerable<OkexInterestVolumeStrike>>> GetRubikInterestVolumeStrike_Async(
            string currency,
            string expiryTime,
            OkexPeriod period = OkexPeriod.FiveMinutes,
            CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> {
                { "ccy", currency},
                { "expTime", expiryTime},
                { "period", JsonConvert.SerializeObject(period, new PeriodConverter(false)) },
            };

            var result = await SendRequestAsync<OkexRestApiResponse<IEnumerable<OkexInterestVolumeStrike>>>(GetUrl(Endpoints_V5_RubikStat_OptionOpenInterestVolumeStrike), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<IEnumerable<OkexInterestVolumeStrike>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<IEnumerable<OkexInterestVolumeStrike>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage, result.Data.Data));

            return new WebCallResult<IEnumerable<OkexInterestVolumeStrike>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }

        /// <summary>
        /// This shows the relative buy/sell volume for calls and puts. It shows whether traders are bullish or bearish on price and volatility.
        /// </summary>
        /// <param name="currency">Currency</param>
        /// <param name="period">period, the default is 8H. e.g. [8H/1D]</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<OkexTakerFlow> GetRubikTakerFlow(
            string currency,
            OkexPeriod period = OkexPeriod.FiveMinutes,
            CancellationToken ct = default) => GetRubikTakerFlow_Async(currency, period, ct).Result;
        /// <summary>
        /// This shows the relative buy/sell volume for calls and puts. It shows whether traders are bullish or bearish on price and volatility.
        /// </summary>
        /// <param name="currency">Currency</param>
        /// <param name="period">period, the default is 8H. e.g. [8H/1D]</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<OkexTakerFlow>> GetRubikTakerFlow_Async(
            string currency,
            OkexPeriod period = OkexPeriod.FiveMinutes,
            CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> {
                { "ccy", currency},
                { "period", JsonConvert.SerializeObject(period, new PeriodConverter(false)) },
            };

            var result = await SendRequestAsync<OkexRestApiResponse<OkexTakerFlow>>(GetUrl(Endpoints_V5_RubikStat_OptionTakerBlockVolume), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<OkexTakerFlow>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<OkexTakerFlow>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage, result.Data.Data));

            return new WebCallResult<OkexTakerFlow>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }


        #endregion
    }
}