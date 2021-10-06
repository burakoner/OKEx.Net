using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using Newtonsoft.Json;
using Okex.Net.Converters;
using Okex.Net.CoreObjects;
using Okex.Net.Enums;
using Okex.Net.Helpers;
using Okex.Net.RestObjects;
using Okex.Net.RestObjects.Account;
using Okex.Net.RestObjects.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Okex.Net
{
    public partial class OkexClient
    {
        #region Account API Endpoints
        public virtual WebCallResult<OkexAccountBalance> GetAccountBalance(string currency = null, CancellationToken ct = default) => GetAccountBalance_Async(currency, ct).Result;
        public virtual async Task<WebCallResult<OkexAccountBalance>> GetAccountBalance_Async(string currency = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("ccy", currency);

            var result = await SendRequestAsync<OkexApiResponse<IEnumerable<OkexAccountBalance>>>(GetUrl(Endpoints_V5_Account_Balance), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<OkexAccountBalance>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<OkexAccountBalance>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage));

            return new WebCallResult<OkexAccountBalance>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data.FirstOrDefault(), null);
        }


        public virtual WebCallResult<IEnumerable<OkexPosition>> GetAccountPositions(
            OkexInstrumentType? instrumentType = null,
            string instrumentId = null,
            string positionId = null,
            CancellationToken ct = default) => GetAccountPositions_Async(instrumentType, instrumentId, positionId, ct).Result;
        public virtual async Task<WebCallResult<IEnumerable<OkexPosition>>> GetAccountPositions_Async(
            OkexInstrumentType? instrumentType = null,
            string instrumentId = null,
            string positionId = null,
            CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            if (instrumentType.HasValue)
                parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)));
            parameters.AddOptionalParameter("instId", instrumentId);
            parameters.AddOptionalParameter("posId", positionId);

            var result = await SendRequestAsync<OkexApiResponse<IEnumerable<OkexPosition>>>(GetUrl(Endpoints_V5_Account_Positions), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<IEnumerable<OkexPosition>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<IEnumerable<OkexPosition>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage));

            return new WebCallResult<IEnumerable<OkexPosition>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }


        public virtual WebCallResult<IEnumerable<OkexPositionRisk>> GetAccountPositionRisk(OkexInstrumentType? instrumentType = null, CancellationToken ct = default) => GetAccountPositionRisk_Async(instrumentType, ct).Result;
        public virtual async Task<WebCallResult<IEnumerable<OkexPositionRisk>>> GetAccountPositionRisk_Async(OkexInstrumentType? instrumentType = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            if (instrumentType.HasValue)
                parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)));

            var result = await SendRequestAsync<OkexApiResponse<IEnumerable<OkexPositionRisk>>>(GetUrl(Endpoints_V5_Account_PositionRisk), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<IEnumerable<OkexPositionRisk>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<IEnumerable<OkexPositionRisk>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage));

            return new WebCallResult<IEnumerable<OkexPositionRisk>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }


        public virtual WebCallResult<IEnumerable<OkexAccountBill>> GetBillHistory(
            OkexInstrumentType? instrumentType = null,
            string currency = null,
            OkexMarginMode? marginMode = null,
            OkexContractType? contractType = null,
            OkexAccountBillType? billType = null,
            OkexAccountBillSubType? billSubType = null,
            long? after = null,
            long? before = null,
            int limit = 100,
            CancellationToken ct = default) => GetBillHistory_Async(
            instrumentType,
            currency,
            marginMode,
            contractType,
            billType,
            billSubType,
            after,
            before,
            limit,
            ct).Result;
        public virtual async Task<WebCallResult<IEnumerable<OkexAccountBill>>> GetBillHistory_Async(
            OkexInstrumentType? instrumentType = null,
            string currency = null,
            OkexMarginMode? marginMode = null,
            OkexContractType? contractType = null,
            OkexAccountBillType? billType = null,
            OkexAccountBillSubType? billSubType = null,
            long? after = null,
            long? before = null,
            int limit = 100,
            CancellationToken ct = default)
        {
            if (limit < 1 || limit > 100)
                throw new ArgumentException("Limit can be between 1-100.");

            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("ccy", currency);
            parameters.AddOptionalParameter("after", after?.ToString());
            parameters.AddOptionalParameter("before", before?.ToString());
            parameters.AddOptionalParameter("limit", limit.ToString());

            if (instrumentType.HasValue)
                parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)));
            if (marginMode.HasValue)
                parameters.AddOptionalParameter("mgnMode", JsonConvert.SerializeObject(marginMode, new MarginModeConverter(false)));
            if (contractType.HasValue)
                parameters.AddOptionalParameter("ctType", JsonConvert.SerializeObject(contractType, new ContractTypeConverter(false)));
            if (billType.HasValue)
                parameters.AddOptionalParameter("type", JsonConvert.SerializeObject(billType, new AccountBillTypeConverter(false)));
            if (billSubType.HasValue)
                parameters.AddOptionalParameter("subType", JsonConvert.SerializeObject(billSubType, new AccountBillSubTypeConverter(false)));

            var result = await SendRequestAsync<OkexApiResponse<IEnumerable<OkexAccountBill>>>(GetUrl(Endpoints_V5_Account_Bills), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<IEnumerable<OkexAccountBill>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<IEnumerable<OkexAccountBill>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage));

            return new WebCallResult<IEnumerable<OkexAccountBill>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }


        public virtual WebCallResult<IEnumerable<OkexAccountBill>> GetBillArchive(
            OkexInstrumentType? instrumentType = null,
            string currency = null,
            OkexMarginMode? marginMode = null,
            OkexContractType? contractType = null,
            OkexAccountBillType? billType = null,
            OkexAccountBillSubType? billSubType = null,
            long? after = null,
            long? before = null,
            int limit = 100,
            CancellationToken ct = default) => GetBillArchive_Async(
            instrumentType,
            currency,
            marginMode,
            contractType,
            billType,
            billSubType,
            after,
            before,
            limit,
            ct).Result;
        public virtual async Task<WebCallResult<IEnumerable<OkexAccountBill>>> GetBillArchive_Async(
            OkexInstrumentType? instrumentType = null,
            string currency = null,
            OkexMarginMode? marginMode = null,
            OkexContractType? contractType = null,
            OkexAccountBillType? billType = null,
            OkexAccountBillSubType? billSubType = null,
            long? after = null,
            long? before = null,
            int limit = 100,
            CancellationToken ct = default)
        {
            if (limit < 1 || limit > 100)
                throw new ArgumentException("Limit can be between 1-100.");

            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("ccy", currency);
            parameters.AddOptionalParameter("after", after?.ToString());
            parameters.AddOptionalParameter("before", before?.ToString());
            parameters.AddOptionalParameter("limit", limit.ToString());

            if (instrumentType.HasValue)
                parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)));
            if (marginMode.HasValue)
                parameters.AddOptionalParameter("mgnMode", JsonConvert.SerializeObject(marginMode, new MarginModeConverter(false)));
            if (contractType.HasValue)
                parameters.AddOptionalParameter("ctType", JsonConvert.SerializeObject(contractType, new ContractTypeConverter(false)));
            if (billType.HasValue)
                parameters.AddOptionalParameter("type", JsonConvert.SerializeObject(billType, new AccountBillTypeConverter(false)));
            if (billSubType.HasValue)
                parameters.AddOptionalParameter("subType", JsonConvert.SerializeObject(billSubType, new AccountBillSubTypeConverter(false)));

            var result = await SendRequestAsync<OkexApiResponse<IEnumerable<OkexAccountBill>>>(GetUrl(Endpoints_V5_Account_BillsArchive), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<IEnumerable<OkexAccountBill>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<IEnumerable<OkexAccountBill>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage));

            return new WebCallResult<IEnumerable<OkexAccountBill>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }


        public virtual WebCallResult<OkexConfiguration> GetAccountConfiguration(CancellationToken ct = default) => GetAccountConfiguration_Async(ct).Result;
        public virtual async Task<WebCallResult<OkexConfiguration>> GetAccountConfiguration_Async(CancellationToken ct = default)
        {
            var result = await SendRequestAsync<OkexApiResponse<IEnumerable<OkexConfiguration>>>(GetUrl(Endpoints_V5_Account_Config), HttpMethod.Get, ct, null, true).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<OkexConfiguration>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<OkexConfiguration>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage));

            return new WebCallResult<OkexConfiguration>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data.FirstOrDefault(), null);
        }


        public virtual WebCallResult<RestObjects.Account.OkexPositionMode> SetAccountPositionMode(Enums.OkexPositionMode positionMode, CancellationToken ct = default) => SetAccountPositionMode_Async(positionMode, ct).Result;
        public virtual async Task<WebCallResult<RestObjects.Account.OkexPositionMode>> SetAccountPositionMode_Async(Enums.OkexPositionMode positionMode, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> {
                {"posMode", JsonConvert.SerializeObject(positionMode, new PositionModeConverter(false)) },
            };

            var result = await base.SendRequestAsync<OkexApiResponse<IEnumerable<RestObjects.Account.OkexPositionMode>>>(GetUrl(Endpoints_V5_Account_SetPositionMode), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<RestObjects.Account.OkexPositionMode>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<RestObjects.Account.OkexPositionMode>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage));

            return new WebCallResult<RestObjects.Account.OkexPositionMode>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data.FirstOrDefault(), null);
        }









        public virtual WebCallResult<IEnumerable<OkexLeverage>> GetAccountLeverage(
            string instrumentIds,
            OkexMarginMode marginMode,
            CancellationToken ct = default) => GetAccountLeverage_Async(instrumentIds, marginMode, ct).Result;
        public virtual async Task<WebCallResult<IEnumerable<OkexLeverage>>> GetAccountLeverage_Async(
            string instrumentIds,
            OkexMarginMode marginMode,
            CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> {
                {"instId", instrumentIds },
                {"mgnMode", JsonConvert.SerializeObject(marginMode, new MarginModeConverter(false)) },
            };

            var result = await SendRequestAsync<OkexApiResponse<IEnumerable<OkexLeverage>>>(GetUrl(Endpoints_V5_Account_LeverageInfo), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<IEnumerable<OkexLeverage>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<IEnumerable<OkexLeverage>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage));

            return new WebCallResult<IEnumerable<OkexLeverage>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }


        public virtual WebCallResult<IEnumerable<OkexLeverage>> SetAccountLeverage(
            int leverage,
            string currency = null,
            string instrumentId = null,
            OkexMarginMode? marginMode = null,
            OkexPositionSide? positionSide = null,
            CancellationToken ct = default) => SetAccountLeverage_Async(leverage, currency, instrumentId, marginMode, positionSide, ct).Result;
        public virtual async Task<WebCallResult<IEnumerable<OkexLeverage>>> SetAccountLeverage_Async(
            int leverage,
            string currency = null,
            string instrumentId = null,
            OkexMarginMode? marginMode = null,
            OkexPositionSide? positionSide = null,
            CancellationToken ct = default)
        {
            if (leverage < 1)
                throw new ArgumentException("Invalid Leverage");

            if (string.IsNullOrEmpty(currency) && string.IsNullOrEmpty(instrumentId))
                throw new ArgumentException("Either instId or ccy is required; if both are passed, instId will be used by default.");

            if (marginMode == null)
                throw new ArgumentException("marginMode is required");

            var parameters = new Dictionary<string, object> {
                {"lever", leverage.ToString() },
                {"mgnMode", JsonConvert.SerializeObject(marginMode, new MarginModeConverter(false)) },
            };
            parameters.AddOptionalParameter("ccy", currency);
            parameters.AddOptionalParameter("instId", instrumentId);
            if (positionSide.HasValue)
                parameters.AddOptionalParameter("posSide", JsonConvert.SerializeObject(positionSide, new PositionSideConverter(false)));

            var result = await SendRequestAsync<OkexApiResponse<IEnumerable<OkexLeverage>>>(GetUrl(Endpoints_V5_Account_SetLeverage), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<IEnumerable<OkexLeverage>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<IEnumerable<OkexLeverage>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage));

            return new WebCallResult<IEnumerable<OkexLeverage>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }


        public virtual WebCallResult<IEnumerable<OkexMaximumAmount>> GetMaximumAmount(
            string instrumentId,
            OkexTradeMode tradeMode,
            string currency = null,
            decimal? price = null,
            CancellationToken ct = default) => GetMaximumAmount_Async(instrumentId, tradeMode, currency, price, ct).Result;
        public virtual async Task<WebCallResult<IEnumerable<OkexMaximumAmount>>> GetMaximumAmount_Async(
            string instrumentId,
            OkexTradeMode tradeMode,
            string currency = null,
            decimal? price = null,
            CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> {
                {"instId", instrumentId },
                {"tdMode", JsonConvert.SerializeObject(tradeMode, new TradeModeConverter(false)) },
            };
            parameters.AddOptionalParameter("ccy", currency);
            parameters.AddOptionalParameter("px", price?.ToString(OkexGlobals.OkexCultureInfo));

            var result = await SendRequestAsync<OkexApiResponse<IEnumerable<OkexMaximumAmount>>>(GetUrl(Endpoints_V5_Account_MaxSize), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<IEnumerable<OkexMaximumAmount>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<IEnumerable<OkexMaximumAmount>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage));

            return new WebCallResult<IEnumerable<OkexMaximumAmount>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }


        public virtual WebCallResult<IEnumerable<OkexMaximumAvailableAmount>> GetMaximumAvailableAmount(
            string instrumentId,
            OkexTradeMode tradeMode,
            string currency = null,
            bool? reduceOnly = null,
            CancellationToken ct = default) => GetMaximumAvailableAmount_Async(instrumentId, tradeMode, currency, reduceOnly, ct).Result;
        public virtual async Task<WebCallResult<IEnumerable<OkexMaximumAvailableAmount>>> GetMaximumAvailableAmount_Async(
            string instrumentId,
            OkexTradeMode tradeMode,
            string currency = null,
            bool? reduceOnly = null,
            CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> {
                {"instId", instrumentId },
                {"tdMode", JsonConvert.SerializeObject(tradeMode, new TradeModeConverter(false)) },
            };
            parameters.AddOptionalParameter("ccy", currency);
            parameters.AddOptionalParameter("reduceOnly", reduceOnly);

            var result = await SendRequestAsync<OkexApiResponse<IEnumerable<OkexMaximumAvailableAmount>>>(GetUrl(Endpoints_V5_Account_MaxAvailSize), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<IEnumerable<OkexMaximumAvailableAmount>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<IEnumerable<OkexMaximumAvailableAmount>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage));

            return new WebCallResult<IEnumerable<OkexMaximumAvailableAmount>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }


        public virtual WebCallResult<IEnumerable<OkexMarginAmount>> SetMarginAmount(
            string instrumentId,
            OkexPositionSide positionSide,
            OkexMarginAddReduce marginAddReduce,
            decimal amount,
            CancellationToken ct = default) => SetMarginAmount_Async(instrumentId, positionSide, marginAddReduce, amount, ct).Result;
        public virtual async Task<WebCallResult<IEnumerable<OkexMarginAmount>>> SetMarginAmount_Async(
            string instrumentId,
            OkexPositionSide positionSide,
            OkexMarginAddReduce marginAddReduce,
            decimal amount,
            CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> {
                {"instId", instrumentId },
                {"posSide", JsonConvert.SerializeObject(positionSide, new PositionSideConverter(false)) },
                {"type", JsonConvert.SerializeObject(marginAddReduce, new MarginAddReduceConverter(false)) },
                {"amt", amount.ToString(OkexGlobals.OkexCultureInfo) },
            };

            var result = await SendRequestAsync<OkexApiResponse<IEnumerable<OkexMarginAmount>>>(GetUrl(Endpoints_V5_Account_PositionMarginBalance), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<IEnumerable<OkexMarginAmount>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<IEnumerable<OkexMarginAmount>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage));

            return new WebCallResult<IEnumerable<OkexMarginAmount>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }


        public virtual WebCallResult<IEnumerable<OkexMaximumLoanAmount>> GetMaximumLoanAmount(
            string instrumentId,
            OkexMarginMode marginMode,
            string marginCurrency = null,
            CancellationToken ct = default) => GetMaximumLoanAmount_Async(instrumentId, marginMode, marginCurrency, ct).Result;
        public virtual async Task<WebCallResult<IEnumerable<OkexMaximumLoanAmount>>> GetMaximumLoanAmount_Async(
            string instrumentId,
            OkexMarginMode marginMode,
            string marginCurrency = null,
            CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> {
                {"instId", instrumentId },
                {"mgnMode", JsonConvert.SerializeObject(marginMode, new MarginModeConverter(false)) },
            };
            parameters.AddOptionalParameter("mgnCcy", marginCurrency);

            var result = await SendRequestAsync<OkexApiResponse<IEnumerable<OkexMaximumLoanAmount>>>(GetUrl(Endpoints_V5_Account_MaxLoan), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<IEnumerable<OkexMaximumLoanAmount>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<IEnumerable<OkexMaximumLoanAmount>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage));

            return new WebCallResult<IEnumerable<OkexMaximumLoanAmount>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }
        

        public virtual WebCallResult<OkexFeeRate> GetFeeRates(
            OkexInstrumentType instrumentType,
            string instrumentId=null,
            string underlying=null,
            OkexFeeRateCategory? category=null,
            CancellationToken ct = default) => GetFeeRates_Async(instrumentType, instrumentId, underlying, category, ct).Result;
        public virtual async Task<WebCallResult<OkexFeeRate>> GetFeeRates_Async(
            OkexInstrumentType instrumentType,
            string instrumentId = null,
            string underlying = null,
            OkexFeeRateCategory? category = null,
            CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> {
                {"instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)) },
            };
            parameters.AddOptionalParameter("instId", instrumentId);
            parameters.AddOptionalParameter("uly", underlying);
            parameters.AddOptionalParameter("category", JsonConvert.SerializeObject(category, new FeeRateCategoryConverter(false)));

            var result = await SendRequestAsync<OkexApiResponse<IEnumerable<OkexFeeRate>>>(GetUrl(Endpoints_V5_Account_TradeFee), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
            if (!result.Success) return WebCallResult< OkexFeeRate >.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult< OkexFeeRate >.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage));

            return new WebCallResult<OkexFeeRate>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data.FirstOrDefault(), null);
        }


        public virtual WebCallResult<IEnumerable<OkexInterestAccrued>> GetInterestAccrued(
            string instrumentId = null,
            string currency = null,
            OkexMarginMode? marginMode = null,
            long? after = null,
            long? before = null,
            int limit = 100,
            CancellationToken ct = default) => GetInterestAccrued_Async(
            instrumentId,
            currency,
            marginMode,
            after,
            before,
            limit,
            ct).Result;
        public virtual async Task<WebCallResult<IEnumerable<OkexInterestAccrued>>> GetInterestAccrued_Async(
            string instrumentId = null,
            string currency = null,
            OkexMarginMode? marginMode = null,
            long? after = null,
            long? before = null,
            int limit = 100,
            CancellationToken ct = default)
        {
            if (limit < 1 || limit > 100)
                throw new ArgumentException("Limit can be between 1-100.");

            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("instId", instrumentId);
            parameters.AddOptionalParameter("ccy", currency);
            parameters.AddOptionalParameter("after", after?.ToString());
            parameters.AddOptionalParameter("before", before?.ToString());
            parameters.AddOptionalParameter("limit", limit.ToString());

            if (marginMode.HasValue)
                parameters.AddOptionalParameter("mgnMode", JsonConvert.SerializeObject(marginMode, new MarginModeConverter(false)));

            var result = await SendRequestAsync<OkexApiResponse<IEnumerable<OkexInterestAccrued>>>(GetUrl(Endpoints_V5_Account_InterestAccrued), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<IEnumerable<OkexInterestAccrued>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<IEnumerable<OkexInterestAccrued>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage));

            return new WebCallResult<IEnumerable<OkexInterestAccrued>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }
        

        public virtual WebCallResult<IEnumerable<OkexInterestRate>> GetInterestRate(
            string currency = null,
            CancellationToken ct = default) => GetInterestRate_Async(
            currency,
            ct).Result;
        public virtual async Task<WebCallResult<IEnumerable<OkexInterestRate>>> GetInterestRate_Async(
            string currency = null,
            CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("ccy", currency);

            var result = await SendRequestAsync<OkexApiResponse<IEnumerable<OkexInterestRate>>>(GetUrl(Endpoints_V5_Account_InterestRate), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<IEnumerable<OkexInterestRate>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<IEnumerable<OkexInterestRate>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage));

            return new WebCallResult<IEnumerable<OkexInterestRate>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }


        public virtual WebCallResult<RestObjects.Account.OkexGreeksType> SetGreeks(Enums.OkexGreeksType greeksType, CancellationToken ct = default) => SetGreeks_Async(greeksType, ct).Result;
        public virtual async Task<WebCallResult<RestObjects.Account.OkexGreeksType>> SetGreeks_Async(Enums.OkexGreeksType greeksType, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> {
                {"greeksType", JsonConvert.SerializeObject(greeksType, new GreeksTypeConverter(false)) },
            };

            var result = await base.SendRequestAsync<OkexApiResponse<IEnumerable<RestObjects.Account.OkexGreeksType>>>(GetUrl(Endpoints_V5_Account_SetGreeks), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<RestObjects.Account.OkexGreeksType>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<RestObjects.Account.OkexGreeksType>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage));

            return new WebCallResult<RestObjects.Account.OkexGreeksType>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data.FirstOrDefault(), null);
        }


        public virtual WebCallResult<IEnumerable<OkexWithdrawalAmount>> GetMaximumWithdrawals(
            string currency = null,
            CancellationToken ct = default) => GetMaximumWithdrawals_Async(
            currency,
            ct).Result;
        public virtual async Task<WebCallResult<IEnumerable<OkexWithdrawalAmount>>> GetMaximumWithdrawals_Async(
            string currency = null,
            CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("ccy", currency);

            var result = await SendRequestAsync<OkexApiResponse<IEnumerable<OkexWithdrawalAmount>>>(GetUrl(Endpoints_V5_Account_MaxWithdrawal), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<IEnumerable<OkexWithdrawalAmount>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<IEnumerable<OkexWithdrawalAmount>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage));

            return new WebCallResult<IEnumerable<OkexWithdrawalAmount>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }


        #endregion
    }
}


