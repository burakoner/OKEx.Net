namespace Okex.Net;

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
        bool? reduceOnly = null,
        OkexQuantityType? quantityType = null,
        CancellationToken ct = default)
        => PlaceOrderAsync(
        instrumentId,
        tradeMode,
        orderSide,
        positionSide,
        orderType,
        size,
        price,
        currency,
        clientOrderId,
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
    /// <param name="reduceOnly">Whether to reduce position only or not, true false, the default is false.</param>
    /// <param name="quantityType">Quantity Type</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<WebCallResult<OkexOrderPlaceResponse>> PlaceOrderAsync(
        string instrumentId,
        OkexTradeMode tradeMode,
        OkexOrderSide orderSide,
        OkexPositionSide positionSide,
        OkexOrderType orderType,
        decimal size,
        decimal? price = null,
        string currency = null,
        string clientOrderId = null,
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
            {"tag", "538a3965e538BCDE" },
        };
        parameters.AddOptionalParameter("px", price?.ToString(OkexGlobals.OkexCultureInfo));
        parameters.AddOptionalParameter("ccy", currency);
        parameters.AddOptionalParameter("clOrdId", clientOrderId);
        parameters.AddOptionalParameter("reduceOnly", reduceOnly);
        if (quantityType.HasValue)
            parameters.AddOptionalParameter("tgtCcy", JsonConvert.SerializeObject(quantityType, new QuantityTypeConverter(false)));

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexOrderPlaceResponse>>>(UnifiedApi.GetUri(Endpoints_V5_Trade_Order), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<OkexOrderPlaceResponse>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<OkexOrderPlaceResponse>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data.FirstOrDefault());
    }

    /// <summary>
    /// Place orders in batches. Maximum 20 orders can be placed at a time. Request parameters should be passed in the form of an array.
    /// </summary>
    /// <param name="orders">Orders</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual WebCallResult<IEnumerable<OkexOrderPlaceResponse>> PlaceMultipleOrders(IEnumerable<OkexOrderPlaceRequest> orders, CancellationToken ct = default)
        => PlaceMultipleOrdersAsync(orders, ct).Result;
    /// <summary>
    /// Place orders in batches. Maximum 20 orders can be placed at a time. Request parameters should be passed in the form of an array.
    /// </summary>
    /// <param name="orders">Orders</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<WebCallResult<IEnumerable<OkexOrderPlaceResponse>>> PlaceMultipleOrdersAsync(IEnumerable<OkexOrderPlaceRequest> orders, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { BodyParameterKey, orders },
        };

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexOrderPlaceResponse>>>(UnifiedApi.GetUri(Endpoints_V5_Trade_BatchOrders), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OkexOrderPlaceResponse>>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OkexOrderPlaceResponse>>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data);
    }

    /// <summary>
    /// Cancel an incomplete order.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="orderId">Order ID</param>
    /// <param name="clientOrderId">Client Order ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual WebCallResult<OkexOrderCancelResponse> CancelOrder(string instrumentId, long? orderId = null, string clientOrderId = null, CancellationToken ct = default)
        => CancelOrderAsync(instrumentId, orderId, clientOrderId, ct).Result;
    /// <summary>
    /// Cancel an incomplete order.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="orderId">Order ID</param>
    /// <param name="clientOrderId">Client Order ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<WebCallResult<OkexOrderCancelResponse>> CancelOrderAsync(string instrumentId, long? orderId = null, string clientOrderId = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            {"instId", instrumentId },
        };
        parameters.AddOptionalParameter("ordId", orderId?.ToString(OkexGlobals.OkexCultureInfo));
        parameters.AddOptionalParameter("clOrdId", clientOrderId);

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexOrderCancelResponse>>>(UnifiedApi.GetUri(Endpoints_V5_Trade_CancelOrder), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<OkexOrderCancelResponse>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<OkexOrderCancelResponse>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data.FirstOrDefault());
    }

    /// <summary>
    /// Cancel incomplete orders in batches. Maximum 20 orders can be canceled at a time. Request parameters should be passed in the form of an array.
    /// </summary>
    /// <param name="orders">Orders</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual WebCallResult<IEnumerable<OkexOrderCancelResponse>> CancelMultipleOrders(IEnumerable<OkexOrderCancelRequest> orders, CancellationToken ct = default)
        => CancelMultipleOrdersAsync(orders, ct).Result;
    /// <summary>
    /// Cancel incomplete orders in batches. Maximum 20 orders can be canceled at a time. Request parameters should be passed in the form of an array.
    /// </summary>
    /// <param name="orders">Orders</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<WebCallResult<IEnumerable<OkexOrderCancelResponse>>> CancelMultipleOrdersAsync(IEnumerable<OkexOrderCancelRequest> orders, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { BodyParameterKey, orders },
        };

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexOrderCancelResponse>>>(UnifiedApi.GetUri(Endpoints_V5_Trade_CancelBatchOrders), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OkexOrderCancelResponse>>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OkexOrderCancelResponse>>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data);
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
        CancellationToken ct = default)
        => AmendOrderAsync(instrumentId, orderId, clientOrderId, requestId, cancelOnFail, newQuantity, newPrice, ct).Result;
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
    public virtual async Task<WebCallResult<OkexOrderAmendResponse>> AmendOrderAsync(
        string instrumentId,
        long? orderId = null,
        string clientOrderId = null,
        string requestId = null,
        bool? cancelOnFail = null,
        decimal? newQuantity = null,
        decimal? newPrice = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "instId", instrumentId },
        };
        parameters.AddOptionalParameter("ordId", orderId?.ToString(OkexGlobals.OkexCultureInfo));
        parameters.AddOptionalParameter("clOrdId", clientOrderId);
        parameters.AddOptionalParameter("cxlOnFail", cancelOnFail);
        parameters.AddOptionalParameter("reqId", requestId);
        parameters.AddOptionalParameter("newSz", newQuantity?.ToString(OkexGlobals.OkexCultureInfo));
        parameters.AddOptionalParameter("newPx", newPrice?.ToString(OkexGlobals.OkexCultureInfo));

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexOrderAmendResponse>>>(UnifiedApi.GetUri(Endpoints_V5_Trade_AmendOrder), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<OkexOrderAmendResponse>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<OkexOrderAmendResponse>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data.FirstOrDefault());
    }

    /// <summary>
    /// Amend incomplete orders in batches. Maximum 20 orders can be amended at a time. Request parameters should be passed in the form of an array.
    /// </summary>
    /// <param name="orders">Orders</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual WebCallResult<IEnumerable<OkexOrderAmendResponse>> AmendMultipleOrders(IEnumerable<OkexOrderAmendRequest> orders, CancellationToken ct = default)
        => AmendMultipleOrdersAsync(orders, ct).Result;
    /// <summary>
    /// Amend incomplete orders in batches. Maximum 20 orders can be amended at a time. Request parameters should be passed in the form of an array.
    /// </summary>
    /// <param name="orders">Orders</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<WebCallResult<IEnumerable<OkexOrderAmendResponse>>> AmendMultipleOrdersAsync(IEnumerable<OkexOrderAmendRequest> orders, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { BodyParameterKey, orders },
        };

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexOrderAmendResponse>>>(UnifiedApi.GetUri(Endpoints_V5_Trade_AmendBatchOrders), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OkexOrderAmendResponse>>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OkexOrderAmendResponse>>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data);
    }

    /// <summary>
    /// Close all positions of an instrument via a market order.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="marginMode">Margin Mode</param>
    /// <param name="positionSide">Position Side</param>
    /// <param name="currency">Currency</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual WebCallResult<OkexClosePositionResponse> ClosePosition(
        string instrumentId,
        OkexMarginMode marginMode,
        OkexPositionSide? positionSide = null,
        string currency = null,
        CancellationToken ct = default)
        => ClosePositionAsync(instrumentId, marginMode, positionSide, currency, ct).Result;
    /// <summary>
    /// Close all positions of an instrument via a market order.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="marginMode">Margin Mode</param>
    /// <param name="positionSide">Position Side</param>
    /// <param name="currency">Currency</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<WebCallResult<OkexClosePositionResponse>> ClosePositionAsync(
        string instrumentId,
        OkexMarginMode marginMode,
        OkexPositionSide? positionSide = null,
        string currency = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            {"instId", instrumentId },
            {"mgnMode", JsonConvert.SerializeObject(marginMode, new MarginModeConverter(false)) },
        };
        if (positionSide.HasValue)
            parameters.AddOptionalParameter("posSide", JsonConvert.SerializeObject(positionSide, new PositionSideConverter(false)));
        parameters.AddOptionalParameter("ccy", currency);

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexClosePositionResponse>>>(UnifiedApi.GetUri(Endpoints_V5_Trade_ClosePosition), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<OkexClosePositionResponse>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<OkexClosePositionResponse>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data.FirstOrDefault());
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
        CancellationToken ct = default)
        => GetOrderDetailsAsync(instrumentId, orderId, clientOrderId, ct).Result;
    /// <summary>
    /// Retrieve order details.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="orderId">Order ID</param>
    /// <param name="clientOrderId">Client Order ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<WebCallResult<OkexOrder>> GetOrderDetailsAsync(
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

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexOrder>>>(UnifiedApi.GetUri(Endpoints_V5_Trade_Order), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<OkexOrder>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<OkexOrder>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data.FirstOrDefault());
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
        CancellationToken ct = default)
        => GetOrderListAsync(
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
    public virtual async Task<WebCallResult<IEnumerable<OkexOrder>>> GetOrderListAsync(
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

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexOrder>>>(UnifiedApi.GetUri(Endpoints_V5_Trade_OrdersPending), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OkexOrder>>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OkexOrder>>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data);
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
        CancellationToken ct = default)
        => GetOrderHistoryAsync(
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
    public virtual async Task<WebCallResult<IEnumerable<OkexOrder>>> GetOrderHistoryAsync(
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

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexOrder>>>(UnifiedApi.GetUri(Endpoints_V5_Trade_OrdersHistory), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OkexOrder>>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OkexOrder>>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data);
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
        CancellationToken ct = default)
        => GetOrderArchiveAsync(
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
    public virtual async Task<WebCallResult<IEnumerable<OkexOrder>>> GetOrderArchiveAsync(
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

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexOrder>>>(UnifiedApi.GetUri(Endpoints_V5_Trade_OrdersHistoryArchive), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OkexOrder>>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OkexOrder>>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data);
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
        CancellationToken ct = default)
        => GetTransactionHistoryAsync(
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
    public virtual async Task<WebCallResult<IEnumerable<OkexTransaction>>> GetTransactionHistoryAsync(
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

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexTransaction>>>(UnifiedApi.GetUri(Endpoints_V5_Trade_Fills), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OkexTransaction>>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OkexTransaction>>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data);
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
        CancellationToken ct = default)
        => GetTransactionArchiveAsync(
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
    public virtual async Task<WebCallResult<IEnumerable<OkexTransaction>>> GetTransactionArchiveAsync(
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

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexTransaction>>>(UnifiedApi.GetUri(Endpoints_V5_Trade_FillsHistory), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OkexTransaction>>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OkexTransaction>>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data);
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
    /// <param name="tpTriggerPxType">Take-profit trigger price type</param>
    /// <param name="tpTriggerPrice">Take Profit Trigger Price</param>
    /// <param name="tpOrderPrice">Take Profit Order Price</param>
    /// <param name="slTriggerPxType">Stop-loss trigger price. If you fill in this parameter, you should fill in the stop-loss order price.</param>
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
        OkexAlgoPriceType? tpTriggerPxType = null,
        decimal? tpTriggerPrice = null,
        decimal? tpOrderPrice = null,
        OkexAlgoPriceType? slTriggerPxType = null,
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
        => PlaceAlgoOrderAsync(
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
        tpTriggerPxType,
        tpTriggerPrice,
        tpOrderPrice,
        slTriggerPxType,
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
    /// <param name="tpTriggerPxType">Take-profit trigger price type</param>
    /// <param name="tpTriggerPrice">Take Profit Trigger Price</param>
    /// <param name="tpOrderPrice">Take Profit Order Price</param>
    /// <param name="slTriggerPxType">Stop-loss trigger price. If you fill in this parameter, you should fill in the stop-loss order price.</param>
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
    public virtual async Task<WebCallResult<OkexAlgoOrderResponse>> PlaceAlgoOrderAsync(
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
        OkexAlgoPriceType? tpTriggerPxType = null,
        decimal? tpTriggerPrice = null,
        decimal? tpOrderPrice = null,
        OkexAlgoPriceType? slTriggerPxType = null,
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
        if (tpTriggerPxType.HasValue)
            parameters.AddOptionalParameter("tpTriggerPxType", JsonConvert.SerializeObject(tpTriggerPxType, new AlgoPriceTypeConverter(false)));
        parameters.AddOptionalParameter("tpTriggerPx", tpTriggerPrice?.ToString(OkexGlobals.OkexCultureInfo));
        parameters.AddOptionalParameter("tpOrdPx", tpOrderPrice?.ToString(OkexGlobals.OkexCultureInfo));
        if (slTriggerPxType.HasValue)
            parameters.AddOptionalParameter("slTriggerPxType", JsonConvert.SerializeObject(slTriggerPxType, new AlgoPriceTypeConverter(false)));
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

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexAlgoOrderResponse>>>(UnifiedApi.GetUri(Endpoints_V5_Trade_OrderAlgo), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<OkexAlgoOrderResponse>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<OkexAlgoOrderResponse>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data.FirstOrDefault());
    }

    /// <summary>
    /// Cancel unfilled algo orders(trigger order, oco order, conditional order). A maximum of 10 orders can be canceled at a time. Request parameters should be passed in the form of an array.
    /// </summary>
    /// <param name="orders">Orders</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual WebCallResult<OkexAlgoOrderResponse> CancelAlgoOrder(IEnumerable<OkexAlgoOrderRequest> orders, CancellationToken ct = default)
        => CancelAlgoOrderAsync(orders, ct).Result;
    /// <summary>
    /// Cancel unfilled algo orders(trigger order, oco order, conditional order). A maximum of 10 orders can be canceled at a time. Request parameters should be passed in the form of an array.
    /// </summary>
    /// <param name="orders">Orders</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<WebCallResult<OkexAlgoOrderResponse>> CancelAlgoOrderAsync(IEnumerable<OkexAlgoOrderRequest> orders, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            {BodyParameterKey, orders },
        };

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexAlgoOrderResponse>>>(UnifiedApi.GetUri(Endpoints_V5_Trade_CancelAlgos), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<OkexAlgoOrderResponse>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<OkexAlgoOrderResponse>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data.FirstOrDefault());
    }

    /// <summary>
    /// Cancel unfilled algo orders(iceberg order and twap order). A maximum of 10 orders can be canceled at a time. Request parameters should be passed in the form of an array.
    /// </summary>
    /// <param name="orders">Orders</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual WebCallResult<OkexAlgoOrderResponse> CancelAdvanceAlgoOrder(IEnumerable<OkexAlgoOrderRequest> orders, CancellationToken ct = default)
        => CancelAdvanceAlgoOrderAsync(orders, ct).Result;
    /// <summary>
    /// Cancel unfilled algo orders(iceberg order and twap order). A maximum of 10 orders can be canceled at a time. Request parameters should be passed in the form of an array.
    /// </summary>
    /// <param name="orders">Orders</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<WebCallResult<OkexAlgoOrderResponse>> CancelAdvanceAlgoOrderAsync(IEnumerable<OkexAlgoOrderRequest> orders, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            {BodyParameterKey, orders },
        };

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexAlgoOrderResponse>>>(UnifiedApi.GetUri(Endpoints_V5_Trade_CancelAdvanceAlgos), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<OkexAlgoOrderResponse>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<OkexAlgoOrderResponse>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data.FirstOrDefault());
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
        CancellationToken ct = default)
        => GetAlgoOrderListAsync(
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
    public virtual async Task<WebCallResult<IEnumerable<OkexAlgoOrder>>> GetAlgoOrderListAsync(
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

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexAlgoOrder>>>(UnifiedApi.GetUri(Endpoints_V5_Trade_OrdersAlgoPending), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OkexAlgoOrder>>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OkexAlgoOrder>>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data);
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
        CancellationToken ct = default)
        => GetAlgoOrderHistoryAsync(
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
    public virtual async Task<WebCallResult<IEnumerable<OkexAlgoOrder>>> GetAlgoOrderHistoryAsync(
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

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexAlgoOrder>>>(UnifiedApi.GetUri(Endpoints_V5_Trade_OrdersAlgoHistory), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OkexAlgoOrder>>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OkexAlgoOrder>>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data);
    }

    #endregion
}