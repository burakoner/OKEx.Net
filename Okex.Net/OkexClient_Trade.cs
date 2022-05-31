using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using Newtonsoft.Json;
using Okex.Net.Converters;
using Okex.Net.CoreObjects;
using Okex.Net.Enums;
using Okex.Net.Helpers;
using Okex.Net.RestObjects.Trade;
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
        #region Trade API Endpoints
        /// <summary>
        /// You can place an order only if you have sufficient funds.
        /// </summary>
        /// <param name="instrumentId">Instrument ID</param>
        /// <param name="tradeMode">Trade Mode</param>
        /// <param name="orderSide">Order Side</param>
        /// <param name="positionSide">Position Side</param>
        /// <param name="orderType">Order Type</param>
        /// <param name="size">Size</param>
        /// <param name="price">Price</param>
        /// <param name="currency">Currency</param>
        /// <param name="clientOrderId">Client Order ID</param>
        /// <param name="tag">Tag</param>
        /// <param name="reduceOnly">Whether to reduce position only or not, true false, the default is false.</param>
        /// <param name="quantityType">Quantity Type</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<OkexOrderPlaceResponse> PlaceOrder(
            string instrumentId,
            OkexTradeMode tradeMode,
            OkexOrderSide orderSide,
            OkexPositionSide positionSide,
            OkexOrderType orderType,
            decimal size,
            decimal? price = null,
            string currency = null,
            string clientOrderId = null,
            string tag = null,
            bool? reduceOnly = null,
            OkexQuantityType? quantityType = null,
            CancellationToken ct = default) => PlaceOrder_Async(
            instrumentId,
            tradeMode,
            orderSide,
            positionSide,
            orderType,
            size,
            price,
            currency,
            clientOrderId,
            tag,
            reduceOnly,
            quantityType,
            ct).Result;
        /// <summary>
        /// You can place an order only if you have sufficient funds.
        /// </summary>
        /// <param name="instrumentId">Instrument ID</param>
        /// <param name="tradeMode">Trade Mode</param>
        /// <param name="orderSide">Order Side</param>
        /// <param name="positionSide">Position Side</param>
        /// <param name="orderType">Order Type</param>
        /// <param name="size">Size</param>
        /// <param name="price">Price</param>
        /// <param name="currency">Currency</param>
        /// <param name="clientOrderId">Client Order ID</param>
        /// <param name="tag">Tag</param>
        /// <param name="reduceOnly">Whether to reduce position only or not, true false, the default is false.</param>
        /// <param name="quantityType">Quantity Type</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<OkexOrderPlaceResponse>> PlaceOrder_Async(
            string instrumentId,
            OkexTradeMode tradeMode,
            OkexOrderSide orderSide,
            OkexPositionSide positionSide,
            OkexOrderType orderType,
            decimal size,
            decimal? price = null,
            string currency = null,
            string clientOrderId = null,
            string tag = null,
            bool? reduceOnly = null,
            OkexQuantityType? quantityType = null,
            CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> {
                {"instId", instrumentId },
                {"tdMode", JsonConvert.SerializeObject(tradeMode, new TradeModeConverter(false)) },
                {"side", JsonConvert.SerializeObject(orderSide, new OrderSideConverter(false)) },
                {"posSide", JsonConvert.SerializeObject(positionSide, new PositionSideConverter(false)) },
                {"ordType", JsonConvert.SerializeObject(orderType, new OrderTypeConverter(false)) },
                {"sz", size.ToString(OkexGlobals.OkexCultureInfo) },
            };
            parameters.AddOptionalParameter("px", price?.ToString(OkexGlobals.OkexCultureInfo));
            parameters.AddOptionalParameter("ccy", currency);
            parameters.AddOptionalParameter("clOrdId", clientOrderId);
            parameters.AddOptionalParameter("tag", tag);
            parameters.AddOptionalParameter("reduceOnly", reduceOnly);
            if (quantityType.HasValue)
                parameters.AddOptionalParameter("tgtCcy", JsonConvert.SerializeObject(quantityType, new QuantityTypeConverter(false)));

            var result = await SendRequestAsync<OkexRestApiResponse<IEnumerable<OkexOrderPlaceResponse>>>(GetUrl(Endpoints_V5_Trade_Order), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<OkexOrderPlaceResponse>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<OkexOrderPlaceResponse>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage, result.Data.Data));

            return new WebCallResult<OkexOrderPlaceResponse>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data.FirstOrDefault(), null);
        }

        /// <summary>
        /// Place orders in batches. Maximum 20 orders can be placed at a time. Request parameters should be passed in the form of an array.
        /// </summary>
        /// <param name="orders">Orders</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<IEnumerable<OkexOrderPlaceResponse>> PlaceMultipleOrders(IEnumerable<OkexOrderPlaceRequest> orders, CancellationToken ct = default) => PlaceMultipleOrders_Async(orders, ct).Result;
        /// <summary>
        /// Place orders in batches. Maximum 20 orders can be placed at a time. Request parameters should be passed in the form of an array.
        /// </summary>
        /// <param name="orders">Orders</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<IEnumerable<OkexOrderPlaceResponse>>> PlaceMultipleOrders_Async(IEnumerable<OkexOrderPlaceRequest> orders, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { BodyParameterKey, orders },
            };

            var result = await SendRequestAsync<OkexRestApiResponse<IEnumerable<OkexOrderPlaceResponse>>>(GetUrl(Endpoints_V5_Trade_BatchOrders), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<IEnumerable<OkexOrderPlaceResponse>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<IEnumerable<OkexOrderPlaceResponse>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage, result.Data.Data));

            return new WebCallResult<IEnumerable<OkexOrderPlaceResponse>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }

        /// <summary>
        /// Cancel an incomplete order.
        /// </summary>
        /// <param name="instrumentId">Instrument ID</param>
        /// <param name="orderId">Order ID</param>
        /// <param name="clientOrderId">Client Order ID</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<OkexOrderCancelResponse> CancelOrder(string instrumentId, long? orderId = null, string clientOrderId = null, CancellationToken ct = default) => CancelOrder_Async(instrumentId, orderId, clientOrderId, ct).Result;
        /// <summary>
        /// Cancel an incomplete order.
        /// </summary>
        /// <param name="instrumentId">Instrument ID</param>
        /// <param name="orderId">Order ID</param>
        /// <param name="clientOrderId">Client Order ID</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<OkexOrderCancelResponse>> CancelOrder_Async(string instrumentId, long? orderId = null, string clientOrderId = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> {
                {"instId", instrumentId },
            };
            parameters.AddOptionalParameter("ordId", orderId?.ToString(OkexGlobals.OkexCultureInfo));
            parameters.AddOptionalParameter("clOrdId", clientOrderId);

            var result = await SendRequestAsync<OkexRestApiResponse<IEnumerable<OkexOrderCancelResponse>>>(GetUrl(Endpoints_V5_Trade_CancelOrder), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<OkexOrderCancelResponse>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<OkexOrderCancelResponse>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage, result.Data.Data));

            return new WebCallResult<OkexOrderCancelResponse>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data.FirstOrDefault(), null);
        }

        /// <summary>
        /// Cancel incomplete orders in batches. Maximum 20 orders can be canceled at a time. Request parameters should be passed in the form of an array.
        /// </summary>
        /// <param name="orders">Orders</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<IEnumerable<OkexOrderCancelResponse>> CancelMultipleOrders(IEnumerable<OkexOrderCancelRequest> orders, CancellationToken ct = default) => CancelMultipleOrders_Async(orders, ct).Result;
        /// <summary>
        /// Cancel incomplete orders in batches. Maximum 20 orders can be canceled at a time. Request parameters should be passed in the form of an array.
        /// </summary>
        /// <param name="orders">Orders</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<IEnumerable<OkexOrderCancelResponse>>> CancelMultipleOrders_Async(IEnumerable<OkexOrderCancelRequest> orders, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> {
                { BodyParameterKey, orders },
            };

            var result = await SendRequestAsync<OkexRestApiResponse<IEnumerable<OkexOrderCancelResponse>>>(GetUrl(Endpoints_V5_Trade_CancelBatchOrders), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<IEnumerable<OkexOrderCancelResponse>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<IEnumerable<OkexOrderCancelResponse>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage, result.Data.Data));

            return new WebCallResult<IEnumerable<OkexOrderCancelResponse>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }

        /// <summary>
        /// Amend an incomplete order.
        /// </summary>
        /// <param name="instrumentId">Instrument ID</param>
        /// <param name="orderId">Order ID</param>
        /// <param name="clientOrderId">Client Order ID</param>
        /// <param name="requestId">Request ID</param>
        /// <param name="cancelOnFail">Cancel On Fail</param>
        /// <param name="newQuantity">New Quantity</param>
        /// <param name="newPrice">New Price</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<OkexOrderAmendResponse> AmendOrder(
            string instrumentId,
            long? orderId = null,
            string clientOrderId = null,
            string requestId = null,
            bool? cancelOnFail = null,
            decimal? newQuantity = null,
            decimal? newPrice = null,
            CancellationToken ct = default) => AmendOrder_Async(instrumentId, orderId, clientOrderId, requestId, cancelOnFail, newQuantity, newPrice, ct).Result;
        /// <summary>
        /// Amend an incomplete order.
        /// </summary>
        /// <param name="instrumentId">Instrument ID</param>
        /// <param name="orderId">Order ID</param>
        /// <param name="clientOrderId">Client Order ID</param>
        /// <param name="requestId">Request ID</param>
        /// <param name="cancelOnFail">Cancel On Fail</param>
        /// <param name="newQuantity">New Quantity</param>
        /// <param name="newPrice">New Price</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<OkexOrderAmendResponse>> AmendOrder_Async(
            string instrumentId,
            long? orderId = null,
            string clientOrderId = null,
            string requestId = null,
            bool? cancelOnFail = null,
            decimal? newQuantity = null,
            decimal? newPrice = null,
            CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("instId", instrumentId);
            parameters.AddOptionalParameter("ordId", orderId?.ToString(OkexGlobals.OkexCultureInfo));
            parameters.AddOptionalParameter("clOrdId", clientOrderId);
            parameters.AddOptionalParameter("cxlOnFail", cancelOnFail);
            parameters.AddOptionalParameter("reqId", requestId);
            parameters.AddOptionalParameter("newSz", newQuantity);
            parameters.AddOptionalParameter("newPx", newPrice);

            var result = await SendRequestAsync<OkexRestApiResponse<IEnumerable<OkexOrderAmendResponse>>>(GetUrl(Endpoints_V5_Trade_AmendOrder), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<OkexOrderAmendResponse>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<OkexOrderAmendResponse>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage, result.Data.Data));

            return new WebCallResult<OkexOrderAmendResponse>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data.FirstOrDefault(), null);
        }

        /// <summary>
        /// Amend incomplete orders in batches. Maximum 20 orders can be amended at a time. Request parameters should be passed in the form of an array.
        /// </summary>
        /// <param name="orders">Orders</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<IEnumerable<OkexOrderAmendResponse>> AmendMultipleOrders(IEnumerable<OkexOrderAmendRequest> orders, CancellationToken ct = default) => AmendMultipleOrders_Async(orders, ct).Result;
        /// <summary>
        /// Amend incomplete orders in batches. Maximum 20 orders can be amended at a time. Request parameters should be passed in the form of an array.
        /// </summary>
        /// <param name="orders">Orders</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<IEnumerable<OkexOrderAmendResponse>>> AmendMultipleOrders_Async(IEnumerable<OkexOrderAmendRequest> orders, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> {
                { BodyParameterKey, orders },
            };

            var result = await SendRequestAsync<OkexRestApiResponse<IEnumerable<OkexOrderAmendResponse>>>(GetUrl(Endpoints_V5_Trade_AmendBatchOrders), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<IEnumerable<OkexOrderAmendResponse>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<IEnumerable<OkexOrderAmendResponse>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage, result.Data.Data));

            return new WebCallResult<IEnumerable<OkexOrderAmendResponse>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }

        /// <summary>
        /// Close all positions of an instrument via a market order.
        /// </summary>
        /// <param name="instrumentId">Instrument ID</param>
        /// <param name="marginMode">Margin Mode</param>
        /// <param name="positionSide">Position Side</param>
        /// <param name="currency">Currency</param>
        /// <param name="autoCxl">Whether any pending orders for closing out needs to be automatically canceled when close position via a market order.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<OkexClosePositionResponse> ClosePosition(
            string instrumentId,
            OkexMarginMode marginMode,
            OkexPositionSide? positionSide = null,
            string currency = null,
            bool? autoCxl = null,
            CancellationToken ct = default) => ClosePosition_Async(instrumentId, marginMode, positionSide, currency, autoCxl, ct).Result;
        /// <summary>
        /// Close all positions of an instrument via a market order.
        /// </summary>
        /// <param name="instrumentId">Instrument ID</param>
        /// <param name="marginMode">Margin Mode</param>
        /// <param name="positionSide">Position Side</param>
        /// <param name="currency">Currency</param>
        /// <param name="autoCxl">Whether any pending orders for closing out needs to be automatically canceled when close position via a market order.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<OkexClosePositionResponse>> ClosePosition_Async(
            string instrumentId,
            OkexMarginMode marginMode,
            OkexPositionSide? positionSide = null,
            string currency = null,
            bool? autoCxl = null,
            CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> {
                {"instId", instrumentId },
                {"mgnMode", JsonConvert.SerializeObject(marginMode, new MarginModeConverter(false)) },
            };
            if (positionSide.HasValue)
                parameters.AddOptionalParameter("posSide", JsonConvert.SerializeObject(positionSide, new PositionSideConverter(false)));
            parameters.AddOptionalParameter("ccy", currency);

            if (autoCxl.HasValue)
                parameters.AddOptionalParameter("autoCxl", autoCxl);

            var result = await SendRequestAsync<OkexRestApiResponse<IEnumerable<OkexClosePositionResponse>>>(GetUrl(Endpoints_V5_Trade_ClosePosition), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<OkexClosePositionResponse>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<OkexClosePositionResponse>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage, result.Data.Data));

            return new WebCallResult<OkexClosePositionResponse>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data.FirstOrDefault(), null);
        }

        /// <summary>
        /// Retrieve order details.
        /// </summary>
        /// <param name="instrumentId">Instrument ID</param>
        /// <param name="orderId">Order ID</param>
        /// <param name="clientOrderId">Client Order ID</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<OkexOrder> GetOrderDetails(
            string instrumentId,
            long? orderId = null,
            string clientOrderId = null,
            CancellationToken ct = default) => GetOrderDetails_Async(instrumentId, orderId, clientOrderId, ct).Result;
        /// <summary>
        /// Retrieve order details.
        /// </summary>
        /// <param name="instrumentId">Instrument ID</param>
        /// <param name="orderId">Order ID</param>
        /// <param name="clientOrderId">Client Order ID</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<OkexOrder>> GetOrderDetails_Async(
            string instrumentId,
            long? orderId = null,
            string clientOrderId = null,
            CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> {
                {"instId", instrumentId },
            };
            parameters.AddOptionalParameter("ordId", orderId?.ToString());
            parameters.AddOptionalParameter("clOrdId", clientOrderId);

            var result = await SendRequestAsync<OkexRestApiResponse<IEnumerable<OkexOrder>>>(GetUrl(Endpoints_V5_Trade_Order), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<OkexOrder>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<OkexOrder>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage, result.Data.Data));

            return new WebCallResult<OkexOrder>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data.FirstOrDefault(), null);
        }

        /// <summary>
        /// Retrieve all incomplete orders under the current account.
        /// </summary>
        /// <param name="instrumentType">Instrument Type</param>
        /// <param name="instrumentId">Instrument ID</param>
        /// <param name="underlying">Underlying</param>
        /// <param name="orderType">Order Type</param>
        /// <param name="state">State</param>
        /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
        /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
        /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<IEnumerable<OkexOrder>> GetOrderList(
            OkexInstrumentType? instrumentType = null,
            string instrumentId = null,
            string underlying = null,
            OkexOrderType? orderType = null,
            OkexOrderState? state = null,
            long? after = null,
            long? before = null,
            int limit = 100,
            CancellationToken ct = default) => GetOrderList_Async(
            instrumentType,
            instrumentId,
            underlying,
            orderType,
            state,
            after,
            before,
            limit,
            ct).Result;
        /// <summary>
        /// Retrieve all incomplete orders under the current account.
        /// </summary>
        /// <param name="instrumentType">Instrument Type</param>
        /// <param name="instrumentId">Instrument ID</param>
        /// <param name="underlying">Underlying</param>
        /// <param name="orderType">Order Type</param>
        /// <param name="state">State</param>
        /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
        /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
        /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<IEnumerable<OkexOrder>>> GetOrderList_Async(
            OkexInstrumentType? instrumentType = null,
            string instrumentId = null,
            string underlying = null,
            OkexOrderType? orderType = null,
            OkexOrderState? state = null,
            long? after = null,
            long? before = null,
            int limit = 100,
            CancellationToken ct = default)
        {
            if (limit < 1 || limit > 100)
                throw new ArgumentException("Limit can be between 1-100.");

            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("instId", instrumentId);
            parameters.AddOptionalParameter("uly", underlying);
            parameters.AddOptionalParameter("after", after?.ToString());
            parameters.AddOptionalParameter("before", before?.ToString());
            parameters.AddOptionalParameter("limit", limit.ToString());

            if (instrumentType.HasValue)
                parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)));

            if (orderType.HasValue)
                parameters.AddOptionalParameter("ordType", JsonConvert.SerializeObject(orderType, new OrderTypeConverter(false)));

            if (state.HasValue)
                parameters.AddOptionalParameter("state", JsonConvert.SerializeObject(state, new OrderStateConverter(false)));

            var result = await SendRequestAsync<OkexRestApiResponse<IEnumerable<OkexOrder>>>(GetUrl(Endpoints_V5_Trade_OrdersPending), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<IEnumerable<OkexOrder>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<IEnumerable<OkexOrder>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage, result.Data.Data));

            return new WebCallResult<IEnumerable<OkexOrder>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }

        /// <summary>
        /// Retrieve the completed order data for the last 7 days, and the incomplete orders that have been cancelled are only reserved for 2 hours.
        /// </summary>
        /// <param name="instrumentType">Instrument Type</param>
        /// <param name="instrumentId">Instrument ID</param>
        /// <param name="underlying">Underlying</param>
        /// <param name="orderType">Order Type</param>
        /// <param name="state">State</param>
        /// <param name="category">Category</param>
        /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
        /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
        /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<IEnumerable<OkexOrder>> GetOrderHistory(
            OkexInstrumentType instrumentType,
            string instrumentId = null,
            string underlying = null,
            OkexOrderType? orderType = null,
            OkexOrderState? state = null,
            OkexOrderCategory? category = null,
            long? after = null,
            long? before = null,
            int limit = 100,
            CancellationToken ct = default) => GetOrderHistory_Async(
            instrumentType,
            instrumentId,
            underlying,
            orderType,
            state,
            category,
            after,
            before,
            limit,
            ct).Result;
        /// <summary>
        /// Retrieve the completed order data for the last 7 days, and the incomplete orders that have been cancelled are only reserved for 2 hours.
        /// </summary>
        /// <param name="instrumentType">Instrument Type</param>
        /// <param name="instrumentId">Instrument ID</param>
        /// <param name="underlying">Underlying</param>
        /// <param name="orderType">Order Type</param>
        /// <param name="state">State</param>
        /// <param name="category">Category</param>
        /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
        /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
        /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<IEnumerable<OkexOrder>>> GetOrderHistory_Async(
            OkexInstrumentType instrumentType,
            string instrumentId = null,
            string underlying = null,
            OkexOrderType? orderType = null,
            OkexOrderState? state = null,
            OkexOrderCategory? category = null,
            long? after = null,
            long? before = null,
            int limit = 100,
            CancellationToken ct = default)
        {
            if (limit < 1 || limit > 100)
                throw new ArgumentException("Limit can be between 1-100.");

            var parameters = new Dictionary<string, object>
            {
                {"instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false))}
            };
            parameters.AddOptionalParameter("instId", instrumentId);
            parameters.AddOptionalParameter("uly", underlying);
            parameters.AddOptionalParameter("after", after?.ToString());
            parameters.AddOptionalParameter("before", before?.ToString());
            parameters.AddOptionalParameter("limit", limit.ToString());

            if (orderType.HasValue)
                parameters.AddOptionalParameter("ordType", JsonConvert.SerializeObject(orderType, new OrderTypeConverter(false)));

            if (state.HasValue)
                parameters.AddOptionalParameter("state", JsonConvert.SerializeObject(state, new OrderStateConverter(false)));

            if (category.HasValue)
                parameters.AddOptionalParameter("category", JsonConvert.SerializeObject(category, new OrderCategoryConverter(false)));

            var result = await SendRequestAsync<OkexRestApiResponse<IEnumerable<OkexOrder>>>(GetUrl(Endpoints_V5_Trade_OrdersHistory), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<IEnumerable<OkexOrder>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<IEnumerable<OkexOrder>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage, result.Data.Data));

            return new WebCallResult<IEnumerable<OkexOrder>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }

        /// <summary>
        /// Retrieve the completed order data of the last 3 months, and the incomplete orders that have been canceled are only reserved for 2 hours.
        /// </summary>
        /// <param name="instrumentType">Instrument Type</param>
        /// <param name="instrumentId">Instrument ID</param>
        /// <param name="underlying">Underlying</param>
        /// <param name="orderType">Order Type</param>
        /// <param name="state">State</param>
        /// <param name="category">Category</param>
        /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
        /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
        /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<IEnumerable<OkexOrder>> GetOrderArchive(
            OkexInstrumentType instrumentType,
            string instrumentId = null,
            string underlying = null,
            OkexOrderType? orderType = null,
            OkexOrderState? state = null,
            OkexOrderCategory? category = null,
            long? after = null,
            long? before = null,
            int limit = 100,
            CancellationToken ct = default) => GetOrderArchive_Async(
            instrumentType,
            instrumentId,
            underlying,
            orderType,
            state,
            category,
            after,
            before,
            limit,
            ct).Result;
        /// <summary>
        /// Retrieve the completed order data of the last 3 months, and the incomplete orders that have been canceled are only reserved for 2 hours.
        /// </summary>
        /// <param name="instrumentType">Instrument Type</param>
        /// <param name="instrumentId">Instrument ID</param>
        /// <param name="underlying">Underlying</param>
        /// <param name="orderType">Order Type</param>
        /// <param name="state">State</param>
        /// <param name="category">Category</param>
        /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
        /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
        /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<IEnumerable<OkexOrder>>> GetOrderArchive_Async(
            OkexInstrumentType instrumentType,
            string instrumentId = null,
            string underlying = null,
            OkexOrderType? orderType = null,
            OkexOrderState? state = null,
            OkexOrderCategory? category = null,
            long? after = null,
            long? before = null,
            int limit = 100,
            CancellationToken ct = default)
        {
            if (limit < 1 || limit > 100)
                throw new ArgumentException("Limit can be between 1-100.");

            var parameters = new Dictionary<string, object>
            {
                {"instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false))}
            };
            parameters.AddOptionalParameter("instId", instrumentId);
            parameters.AddOptionalParameter("uly", underlying);
            parameters.AddOptionalParameter("after", after?.ToString());
            parameters.AddOptionalParameter("before", before?.ToString());
            parameters.AddOptionalParameter("limit", limit.ToString());

            if (orderType.HasValue)
                parameters.AddOptionalParameter("ordType", JsonConvert.SerializeObject(orderType, new OrderTypeConverter(false)));

            if (state.HasValue)
                parameters.AddOptionalParameter("state", JsonConvert.SerializeObject(state, new OrderStateConverter(false)));

            if (category.HasValue)
                parameters.AddOptionalParameter("category", JsonConvert.SerializeObject(category, new OrderCategoryConverter(false)));

            var result = await SendRequestAsync<OkexRestApiResponse<IEnumerable<OkexOrder>>>(GetUrl(Endpoints_V5_Trade_OrdersHistoryArchive), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<IEnumerable<OkexOrder>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<IEnumerable<OkexOrder>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage, result.Data.Data));

            return new WebCallResult<IEnumerable<OkexOrder>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }

        /// <summary>
        /// Retrieve recently-filled transaction details in the last 3 day.
        /// </summary>
        /// <param name="instrumentType">Instrument Type</param>
        /// <param name="instrumentId">Instrument ID</param>
        /// <param name="underlying">Underlying</param>
        /// <param name="orderId">Order ID</param>
        /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
        /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
        /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<IEnumerable<OkexTransaction>> GetTransactionHistory(
            OkexInstrumentType? instrumentType = null,
            string instrumentId = null,
            string underlying = null,
            long? orderId = null,
            long? after = null,
            long? before = null,
            int limit = 100,
            CancellationToken ct = default) => GetTransactionHistory_Async(
            instrumentType,
            instrumentId,
            underlying,
            orderId,
            after,
            before,
            limit,
            ct).Result;
        /// <summary>
        /// Retrieve recently-filled transaction details in the last 3 day.
        /// </summary>
        /// <param name="instrumentType">Instrument Type</param>
        /// <param name="instrumentId">Instrument ID</param>
        /// <param name="underlying">Underlying</param>
        /// <param name="orderId">Order ID</param>
        /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
        /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
        /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<IEnumerable<OkexTransaction>>> GetTransactionHistory_Async(
            OkexInstrumentType? instrumentType = null,
            string instrumentId = null,
            string underlying = null,
            long? orderId = null,
            long? after = null,
            long? before = null,
            int limit = 100,
            CancellationToken ct = default)
        {
            if (limit < 1 || limit > 100)
                throw new ArgumentException("Limit can be between 1-100.");

            var parameters = new Dictionary<string, object>();
            if (instrumentType.HasValue)
                parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)));
            parameters.AddOptionalParameter("instId", instrumentId);
            parameters.AddOptionalParameter("uly", underlying);
            parameters.AddOptionalParameter("ordId", orderId?.ToString());
            parameters.AddOptionalParameter("after", after?.ToString());
            parameters.AddOptionalParameter("before", before?.ToString());
            parameters.AddOptionalParameter("limit", limit.ToString());

            var result = await SendRequestAsync<OkexRestApiResponse<IEnumerable<OkexTransaction>>>(GetUrl(Endpoints_V5_Trade_Fills), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<IEnumerable<OkexTransaction>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<IEnumerable<OkexTransaction>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage, result.Data.Data));

            return new WebCallResult<IEnumerable<OkexTransaction>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }

        /// <summary>
        /// Retrieve recently-filled transaction details in the last 3 months.
        /// </summary>
        /// <param name="instrumentType">Instrument Type</param>
        /// <param name="instrumentId">Instrument ID</param>
        /// <param name="underlying">Underlying</param>
        /// <param name="orderId">Order ID</param>
        /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
        /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
        /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<IEnumerable<OkexTransaction>> GetTransactionArchive(
            OkexInstrumentType instrumentType,
            string instrumentId = null,
            string underlying = null,
            long? orderId = null,
            long? after = null,
            long? before = null,
            int limit = 100,
            CancellationToken ct = default) => GetTransactionArchive_Async(
            instrumentType,
            instrumentId,
            underlying,
            orderId,
            after,
            before,
            limit,
            ct).Result;
        /// <summary>
        /// Retrieve recently-filled transaction details in the last 3 months.
        /// </summary>
        /// <param name="instrumentType">Instrument Type</param>
        /// <param name="instrumentId">Instrument ID</param>
        /// <param name="underlying">Underlying</param>
        /// <param name="orderId">Order ID</param>
        /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
        /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
        /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<IEnumerable<OkexTransaction>>> GetTransactionArchive_Async(
            OkexInstrumentType instrumentType,
            string instrumentId = null,
            string underlying = null,
            long? orderId = null,
            long? after = null,
            long? before = null,
            int limit = 100,
            CancellationToken ct = default)
        {
            if (limit < 1 || limit > 100)
                throw new ArgumentException("Limit can be between 1-100.");

            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)));
            parameters.AddOptionalParameter("instId", instrumentId);
            parameters.AddOptionalParameter("uly", underlying);
            parameters.AddOptionalParameter("ordId", orderId?.ToString());
            parameters.AddOptionalParameter("after", after?.ToString());
            parameters.AddOptionalParameter("before", before?.ToString());
            parameters.AddOptionalParameter("limit", limit.ToString());

            var result = await SendRequestAsync<OkexRestApiResponse<IEnumerable<OkexTransaction>>>(GetUrl(Endpoints_V5_Trade_FillsHistory), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<IEnumerable<OkexTransaction>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<IEnumerable<OkexTransaction>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage, result.Data.Data));

            return new WebCallResult<IEnumerable<OkexTransaction>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }

        /// <summary>
        /// The algo order includes trigger order, oco order, conditional order,iceberg order and twap order.
        /// </summary>
        /// <param name="instrumentId">Instrument ID</param>
        /// <param name="tradeMode">Trade Mode</param>
        /// <param name="orderSide">Order Side</param>
        /// <param name="algoOrderType">Algo Order Type</param>
        /// <param name="size">Size</param>
        /// <param name="currency">Currency</param>
        /// <param name="reduceOnly">Reduce Only</param>
        /// <param name="positionSide">Position Side</param>
        /// <param name="quantityType">Quantity Type</param>
        /// <param name="tpTriggerPrice">Take Profit Trigger Price</param>
        /// <param name="tpOrderPrice">Take Profit Order Price</param>
        /// <param name="slTriggerPrice">Stop Loss Trigger Price</param>
        /// <param name="slOrderPrice">Stop Loss Order Price</param>
        /// <param name="triggerPrice">Trigger Price</param>
        /// <param name="orderPrice">Order Price</param>
        /// <param name="pxVar">Price Variance</param>
        /// <param name="priceRatio">Price Ratio</param>
        /// <param name="sizeLimit">Size Limit</param>
        /// <param name="priceLimit">Price Limit</param>
        /// <param name="timeInterval">Time Interval</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<OkexAlgoOrderResponse> PlaceAlgoOrder(
            /* Common */
            string instrumentId,
            OkexTradeMode tradeMode,
            OkexOrderSide orderSide,
            OkexAlgoOrderType algoOrderType,
            decimal size,
            string currency = null,
            bool? reduceOnly = null,
            OkexPositionSide? positionSide = null,
            OkexQuantityType? quantityType = null,

            /* Stop Order */
            decimal? tpTriggerPrice = null,
            decimal? tpOrderPrice = null,
            decimal? slTriggerPrice = null,
            decimal? slOrderPrice = null,

            /* Trigger Order */
            decimal? triggerPrice = null,
            decimal? orderPrice = null,

            /* Iceberg Order */
            /* TWAP Order */
            OkexPriceVariance? pxVar = null,
            decimal? priceRatio = null,
            decimal? sizeLimit = null,
            decimal? priceLimit = null,

            /* TWAP Order */
            long? timeInterval = null,

            /* Cancellation Token */
            CancellationToken ct = default) => PlaceAlgoOrder_Async(
            /* Common */
            instrumentId,
            tradeMode,
            orderSide,
            algoOrderType,
            size,
            currency,
            reduceOnly,
            positionSide,
            quantityType,

            /* Stop Order */
            tpTriggerPrice,
            tpOrderPrice,
            slTriggerPrice,
            slOrderPrice,

            /* Trigger Order */
            triggerPrice,
            orderPrice,

            /* Iceberg Order */
            /* TWAP Order */
            pxVar,
            priceRatio,
            sizeLimit,
            priceLimit,

            /* TWAP Order */
            timeInterval,

            /* Cancellation Token */
            ct).Result;
        /// <summary>
        /// The algo order includes trigger order, oco order, conditional order,iceberg order and twap order.
        /// </summary>
        /// <param name="instrumentId">Instrument ID</param>
        /// <param name="tradeMode">Trade Mode</param>
        /// <param name="orderSide">Order Side</param>
        /// <param name="algoOrderType">Algo Order Type</param>
        /// <param name="size">Size</param>
        /// <param name="currency">Currency</param>
        /// <param name="reduceOnly">Reduce Only</param>
        /// <param name="positionSide">Position Side</param>
        /// <param name="quantityType">Quantity Type</param>
        /// <param name="tpTriggerPrice">Take Profit Trigger Price</param>
        /// <param name="tpOrderPrice">Take Profit Order Price</param>
        /// <param name="slTriggerPrice">Stop Loss Trigger Price</param>
        /// <param name="slOrderPrice">Stop Loss Order Price</param>
        /// <param name="triggerPrice">Trigger Price</param>
        /// <param name="orderPrice">Order Price</param>
        /// <param name="pxVar">Price Variance</param>
        /// <param name="priceRatio">Price Ratio</param>
        /// <param name="sizeLimit">Size Limit</param>
        /// <param name="priceLimit">Price Limit</param>
        /// <param name="timeInterval">Time Interval</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<OkexAlgoOrderResponse>> PlaceAlgoOrder_Async(
            //* Common */
            string instrumentId,
            OkexTradeMode tradeMode,
            OkexOrderSide orderSide,
            OkexAlgoOrderType algoOrderType,
            decimal size,
            string currency = null,
            bool? reduceOnly = null,
            OkexPositionSide? positionSide = null,
            OkexQuantityType? quantityType = null,

            /* Stop Order */
            decimal? tpTriggerPrice = null,
            decimal? tpOrderPrice = null,
            decimal? slTriggerPrice = null,
            decimal? slOrderPrice = null,

            /* Trigger Order */
            decimal? triggerPrice = null,
            decimal? orderPrice = null,

            /* Iceberg Order */
            /* TWAP Order */
            OkexPriceVariance? pxVar = null,
            decimal? priceRatio = null,
            decimal? sizeLimit = null,
            decimal? priceLimit = null,

            /* TWAP Order */
            long? timeInterval = null,

            /* Cancellation Token */
            CancellationToken ct = default)
        {
            /* Common */
            var parameters = new Dictionary<string, object> {
                {"instId", instrumentId },
                {"tdMode", JsonConvert.SerializeObject(tradeMode, new TradeModeConverter(false)) },
                {"side", JsonConvert.SerializeObject(orderSide, new OrderSideConverter(false)) },
                {"ordType", JsonConvert.SerializeObject(algoOrderType, new AlgoOrderTypeConverter(false)) },
                {"sz", size.ToString(OkexGlobals.OkexCultureInfo) },
            };
            parameters.AddOptionalParameter("ccy", currency);
            parameters.AddOptionalParameter("reduceOnly", reduceOnly);

            if (positionSide.HasValue)
                parameters.AddOptionalParameter("posSide", JsonConvert.SerializeObject(positionSide, new PositionSideConverter(false)));
            if (quantityType.HasValue)
                parameters.AddOptionalParameter("tgtCcy", JsonConvert.SerializeObject(quantityType, new QuantityTypeConverter(false)));

            /* Stop Order */
            parameters.AddOptionalParameter("tpTriggerPx", tpTriggerPrice?.ToString(OkexGlobals.OkexCultureInfo));
            parameters.AddOptionalParameter("tpOrdPx", tpOrderPrice?.ToString(OkexGlobals.OkexCultureInfo));
            parameters.AddOptionalParameter("slTriggerPx", slTriggerPrice?.ToString(OkexGlobals.OkexCultureInfo));
            parameters.AddOptionalParameter("slOrdPx", slOrderPrice?.ToString(OkexGlobals.OkexCultureInfo));

            /* Trigger Order */
            parameters.AddOptionalParameter("triggerPx", triggerPrice?.ToString(OkexGlobals.OkexCultureInfo));
            parameters.AddOptionalParameter("orderPx", orderPrice?.ToString(OkexGlobals.OkexCultureInfo));

            /* Iceberg Order */
            /* TWAP Order */
            if (pxVar.HasValue)
                parameters.AddOptionalParameter("pxVar", JsonConvert.SerializeObject(pxVar, new PriceVarianceConverter(false)));
            parameters.AddOptionalParameter("pxSpread", priceRatio?.ToString(OkexGlobals.OkexCultureInfo));
            parameters.AddOptionalParameter("szLimit", sizeLimit?.ToString(OkexGlobals.OkexCultureInfo));
            parameters.AddOptionalParameter("pxLimit", priceLimit?.ToString(OkexGlobals.OkexCultureInfo));

            /* TWAP Order */
            parameters.AddOptionalParameter("timeInterval", timeInterval?.ToString(OkexGlobals.OkexCultureInfo));

            var result = await SendRequestAsync<OkexRestApiResponse<IEnumerable<OkexAlgoOrderResponse>>>(GetUrl(Endpoints_V5_Trade_OrderAlgo), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<OkexAlgoOrderResponse>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<OkexAlgoOrderResponse>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage, result.Data.Data));

            return new WebCallResult<OkexAlgoOrderResponse>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data.FirstOrDefault(), null);
        }

        /// <summary>
        /// Cancel unfilled algo orders(trigger order, oco order, conditional order). A maximum of 10 orders can be canceled at a time. Request parameters should be passed in the form of an array.
        /// </summary>
        /// <param name="orders">Orders</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<OkexAlgoOrderResponse> CancelAlgoOrder(IEnumerable<OkexAlgoOrderRequest> orders, CancellationToken ct = default) => CancelAlgoOrder_Async(orders, ct).Result;
        /// <summary>
        /// Cancel unfilled algo orders(trigger order, oco order, conditional order). A maximum of 10 orders can be canceled at a time. Request parameters should be passed in the form of an array.
        /// </summary>
        /// <param name="orders">Orders</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<OkexAlgoOrderResponse>> CancelAlgoOrder_Async(IEnumerable<OkexAlgoOrderRequest> orders, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> {
                {BodyParameterKey, orders },
            };

            var result = await SendRequestAsync<OkexRestApiResponse<IEnumerable<OkexAlgoOrderResponse>>>(GetUrl(Endpoints_V5_Trade_CancelAlgos), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<OkexAlgoOrderResponse>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<OkexAlgoOrderResponse>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage, result.Data.Data));

            return new WebCallResult<OkexAlgoOrderResponse>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data.FirstOrDefault(), null);
        }

        /// <summary>
        /// Cancel unfilled algo orders(iceberg order and twap order). A maximum of 10 orders can be canceled at a time. Request parameters should be passed in the form of an array.
        /// </summary>
        /// <param name="orders">Orders</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<OkexAlgoOrderResponse> CancelAdvanceAlgoOrder(IEnumerable<OkexAlgoOrderRequest> orders, CancellationToken ct = default) => CancelAdvanceAlgoOrder_Async(orders, ct).Result;
        /// <summary>
        /// Cancel unfilled algo orders(iceberg order and twap order). A maximum of 10 orders can be canceled at a time. Request parameters should be passed in the form of an array.
        /// </summary>
        /// <param name="orders">Orders</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<OkexAlgoOrderResponse>> CancelAdvanceAlgoOrder_Async(IEnumerable<OkexAlgoOrderRequest> orders, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> {
                {BodyParameterKey, orders },
            };

            var result = await SendRequestAsync<OkexRestApiResponse<IEnumerable<OkexAlgoOrderResponse>>>(GetUrl(Endpoints_V5_Trade_CancelAdvanceAlgos), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<OkexAlgoOrderResponse>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<OkexAlgoOrderResponse>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage, result.Data.Data));

            return new WebCallResult<OkexAlgoOrderResponse>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data.FirstOrDefault(), null);
        }

        /// <summary>
        /// Cancel unfilled algo orders(iceberg order and twap order). A maximum of 10 orders can be canceled at a time. Request parameters should be passed in the form of an array.
        /// </summary>
        /// <param name="algoOrderType">Algo Order Type</param>
        /// <param name="algoId">Algo ID</param>
        /// <param name="instrumentType">Instrument Type</param>
        /// <param name="instrumentId">Instrument ID</param>
        /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
        /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
        /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<IEnumerable<OkexAlgoOrder>> GetAlgoOrderList(
            OkexAlgoOrderType algoOrderType,
            long? algoId = null,
            OkexInstrumentType? instrumentType = null,
            string instrumentId = null,
            long? after = null,
            long? before = null,
            int limit = 100,
            CancellationToken ct = default) => GetAlgoOrderList_Async(
            algoOrderType,
            algoId,
            instrumentType,
            instrumentId,
            after,
            before,
            limit,
            ct).Result;
        /// <summary>
        /// Cancel unfilled algo orders(iceberg order and twap order). A maximum of 10 orders can be canceled at a time. Request parameters should be passed in the form of an array.
        /// </summary>
        /// <param name="algoOrderType">Algo Order Type</param>
        /// <param name="algoId">Algo ID</param>
        /// <param name="instrumentType">Instrument Type</param>
        /// <param name="instrumentId">Instrument ID</param>
        /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
        /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
        /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<IEnumerable<OkexAlgoOrder>>> GetAlgoOrderList_Async(
            OkexAlgoOrderType algoOrderType,
            long? algoId = null,
            OkexInstrumentType? instrumentType = null,
            string instrumentId = null,
            long? after = null,
            long? before = null,
            int limit = 100,
            CancellationToken ct = default)
        {
            if (limit < 1 || limit > 100)
                throw new ArgumentException("Limit can be between 1-100.");

            var parameters = new Dictionary<string, object>
            {
                {"ordType",   JsonConvert.SerializeObject(algoOrderType, new AlgoOrderTypeConverter(false))}
            };
            parameters.AddOptionalParameter("algoId", algoId?.ToString(OkexGlobals.OkexCultureInfo));
            parameters.AddOptionalParameter("instId", instrumentId);
            parameters.AddOptionalParameter("after", after?.ToString());
            parameters.AddOptionalParameter("before", before?.ToString());
            parameters.AddOptionalParameter("limit", limit.ToString());

            if (instrumentType.HasValue)
                parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)));

            var result = await SendRequestAsync<OkexRestApiResponse<IEnumerable<OkexAlgoOrder>>>(GetUrl(Endpoints_V5_Trade_OrdersAlgoPending), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<IEnumerable<OkexAlgoOrder>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<IEnumerable<OkexAlgoOrder>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage, result.Data.Data));

            return new WebCallResult<IEnumerable<OkexAlgoOrder>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }

        /// <summary>
        /// Retrieve a list of untriggered Algo orders under the current account.
        /// </summary>
        /// <param name="algoOrderType">Algo Order Type</param>
        /// <param name="algoOrderState">Algo Order State</param>
        /// <param name="algoId">Algo ID</param>
        /// <param name="instrumentType">Instrument Type</param>
        /// <param name="instrumentId">Instrument ID</param>
        /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
        /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
        /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<IEnumerable<OkexAlgoOrder>> GetAlgoOrderHistory(
            OkexAlgoOrderType algoOrderType,
            OkexAlgoOrderState? algoOrderState = null,
            long? algoId = null,
            OkexInstrumentType? instrumentType = null,
            string instrumentId = null,
            long? after = null,
            long? before = null,
            int limit = 100,
            CancellationToken ct = default) => GetAlgoOrderHistory_Async(
            algoOrderType,
            algoOrderState,
            algoId,
            instrumentType,
            instrumentId,
            after,
            before,
            limit,
            ct).Result;
        /// <summary>
        /// Retrieve a list of untriggered Algo orders under the current account.
        /// </summary>
        /// <param name="algoOrderType">Algo Order Type</param>
        /// <param name="algoOrderState">Algo Order State</param>
        /// <param name="algoId">Algo ID</param>
        /// <param name="instrumentType">Instrument Type</param>
        /// <param name="instrumentId">Instrument ID</param>
        /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
        /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
        /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<IEnumerable<OkexAlgoOrder>>> GetAlgoOrderHistory_Async(
            OkexAlgoOrderType algoOrderType,
            OkexAlgoOrderState? algoOrderState = null,
            long? algoId = null,
            OkexInstrumentType? instrumentType = null,
            string instrumentId = null,
            long? after = null,
            long? before = null,
            int limit = 100,
            CancellationToken ct = default)
        {
            if (limit < 1 || limit > 100)
                throw new ArgumentException("Limit can be between 1-100.");

            var parameters = new Dictionary<string, object>
            {
                {"ordType",   JsonConvert.SerializeObject(algoOrderType, new AlgoOrderTypeConverter(false))}
            };
            parameters.AddOptionalParameter("algoId", algoId?.ToString(OkexGlobals.OkexCultureInfo));
            parameters.AddOptionalParameter("instId", instrumentId);
            parameters.AddOptionalParameter("after", after?.ToString());
            parameters.AddOptionalParameter("before", before?.ToString());
            parameters.AddOptionalParameter("limit", limit.ToString());

            if (algoOrderState.HasValue)
                parameters.AddOptionalParameter("state", JsonConvert.SerializeObject(algoOrderState, new AlgoOrderStateConverter(false)));

            if (instrumentType.HasValue)
                parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)));

            var result = await SendRequestAsync<OkexRestApiResponse<IEnumerable<OkexAlgoOrder>>>(GetUrl(Endpoints_V5_Trade_OrdersAlgoHistory), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<IEnumerable<OkexAlgoOrder>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
            if (result.Data.ErrorCode > 0) return WebCallResult<IEnumerable<OkexAlgoOrder>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage, result.Data.Data));

            return new WebCallResult<IEnumerable<OkexAlgoOrder>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }

        #endregion
    }
}