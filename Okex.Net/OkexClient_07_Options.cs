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
    public partial class OkexClient : IOkexClientOptions
    {
        #region Options Trading API

        #region Private Signed Endpoints

        /// <summary>
        /// Retrieve the information on your option positions of an underlying.
        /// Rate Limit：20 Requests per 2 seconds
        /// </summary>
        /// <param name="underlying">The underlying index that the contract is based upon, e.g. BTC-USD</param>
        /// <param name="instrument">Instrument ID, e.g. BTC-USD-190927-5000-C</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<OkexOptionsPositions> Options_GetPositions(string underlying, string? instrument = null, CancellationToken ct = default) => Options_GetPositions_Async(underlying, instrument, ct).Result;
        /// <summary>
        /// Retrieve the information on your option positions of an underlying.
        /// Rate Limit：20 Requests per 2 seconds
        /// </summary>
        /// <param name="underlying">The underlying index that the contract is based upon, e.g. BTC-USD</param>
        /// <param name="instrument">Instrument ID, e.g. BTC-USD-190927-5000-C</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<OkexOptionsPositions>> Options_GetPositions_Async(string underlying, string? instrument = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("instrument_id", instrument);

            return await SendRequest<OkexOptionsPositions>(GetUrl(Endpoints_Options_Positions), HttpMethod.Get, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve the options account information for a single underlying index.
        /// Rate Limit：20 Requests per 2 seconds
        /// </summary>
        /// <param name="underlying">The underlying index that the contract is based upon, e.g. BTC-USD.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<OkexOptionsBalance> Options_GetBalances(string underlying, CancellationToken ct = default) => Options_GetBalances_Async(underlying, ct).Result;
        /// <summary>
        /// Retrieve the options account information for a single underlying index.
        /// Rate Limit：20 Requests per 2 seconds
        /// </summary>
        /// <param name="underlying">The underlying index that the contract is based upon, e.g. BTC-USD.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<OkexOptionsBalance>> Options_GetBalances_Async(string underlying, CancellationToken ct = default)
        {
            return await SendRequest<OkexOptionsBalance>(GetUrl(Endpoints_Options_Account, underlying), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Places an order
        /// Rate Limit：20 Requests per second
        /// Notes:
        /// - If the error_code value in response is NOT zero, then it means the request was rejected because verification failed.
        /// - instrument_id
        ///   The instrument_id must match a valid contract ID. The contract list is available via the GET/api/option/v3/instruments/<underlying> endpoint.
        /// - client_oid
        ///   The client_oid is optional and you can customize it using alpha-numeric characters with length 1 to 32. This parameter is used to identify your orders in the public orders feed. No warning is sent when client_oid is not unique. In case of multiple identical client_oid, only the latest entry will be returned.
        /// - type
        ///   You can specify the order type when placing an order. If you are not holding any positions, you can only open new positions, either long or short. You can only close the positions that has been already held.
        ///   The price must be specified in tick size product units. The tick size is the smallest unit of price. Can be obtained through the /instrument interface.
        /// - price
        ///   The price is the price of buying or selling a contract. price must be an incremental multiple of the tick_size. tick_size is the smallest incremental unit of price, which is available via the /instruments endpoint.
        /// - size
        ///   size is the number of contracts bought or sold. The value must be an integer.
        /// - match_price
        ///   The match_price means that you prefer the order to be filled at a best price of the counterpart, where your buy order will be filled at the price of Ask-1. The match_price means that your sell order will be filled at the price of Bid-1.
        /// - Order life cycle
        ///   The HTTP Request will respond when an order is either rejected (insufficient funds, invalid parameters, etc) or received (accepted by the matching engine). A 200 response indicates that the order was received and is active. Active orders may execute immediately (depending on price and market conditions) either partially or fully. A partial execution will put the remaining size of the order in the open state. An order that is filled completely, will go into the completed state.
        ///   Users listening to streaming market data are encouraged to use the client_oid field to identify their received messages in the feed. The REST response with a server order_id may come after the received message in the public data feed.
        /// - Response
        ///   A successful order will be assigned an order id. A successful order is defined as one that has been accepted by the matching engine. Open orders will not expire until filled or canceled.
        /// </summary>
        /// <param name="instrument">Instrument ID, e.g. BTC-USD-190927-5000-C</param>
        /// <param name="side">buy or sell</param>
        /// <param name="price">Price of each contract</param>
        /// <param name="size">The buying or selling quantity （in lots)</param>
        /// <param name="timeInForce">‘0’: Normal order. Parameter will be deemed as '0' if left blank.</param>
        /// <param name="match_price">Whether order is placed at best counter party price (‘0’:no ‘1’:yes). The parameter is defaulted as ‘0’. If it is set as '1', the price parameter will be ignored，</param>
        /// <param name="clientOrderId">You can customize order IDs to identify your orders. The system supports alphabets + numbers(case-sensitive，e.g:A123、a123), or alphabets (case-sensitive，e.g:Abc、abc) only, between 1-32 characters.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<OkexOptionsPlacedOrder> Options_PlaceOrder(string instrument, OkexOptionsOrderSide side, decimal price, decimal size, OkexOptionsTimeInForce timeInForce = OkexOptionsTimeInForce.NormalOrder, bool match_price = false, string? clientOrderId = null, CancellationToken ct = default) => Options_PlaceOrder_Async(instrument, side, price, size, timeInForce, match_price, clientOrderId, ct).Result;
        /// <summary>
        /// Places an order
        /// Rate Limit：20 Requests per second
        /// Notes:
        /// - If the error_code value in response is NOT zero, then it means the request was rejected because verification failed.
        /// - instrument_id
        ///   The instrument_id must match a valid contract ID. The contract list is available via the GET/api/option/v3/instruments/<underlying> endpoint.
        /// - client_oid
        ///   The client_oid is optional and you can customize it using alpha-numeric characters with length 1 to 32. This parameter is used to identify your orders in the public orders feed. No warning is sent when client_oid is not unique. In case of multiple identical client_oid, only the latest entry will be returned.
        /// - type
        ///   You can specify the order type when placing an order. If you are not holding any positions, you can only open new positions, either long or short. You can only close the positions that has been already held.
        ///   The price must be specified in tick size product units. The tick size is the smallest unit of price. Can be obtained through the /instrument interface.
        /// - price
        ///   The price is the price of buying or selling a contract. price must be an incremental multiple of the tick_size. tick_size is the smallest incremental unit of price, which is available via the /instruments endpoint.
        /// - size
        ///   size is the number of contracts bought or sold. The value must be an integer.
        /// - match_price
        ///   The match_price means that you prefer the order to be filled at a best price of the counterpart, where your buy order will be filled at the price of Ask-1. The match_price means that your sell order will be filled at the price of Bid-1.
        /// - Order life cycle
        ///   The HTTP Request will respond when an order is either rejected (insufficient funds, invalid parameters, etc) or received (accepted by the matching engine). A 200 response indicates that the order was received and is active. Active orders may execute immediately (depending on price and market conditions) either partially or fully. A partial execution will put the remaining size of the order in the open state. An order that is filled completely, will go into the completed state.
        ///   Users listening to streaming market data are encouraged to use the client_oid field to identify their received messages in the feed. The REST response with a server order_id may come after the received message in the public data feed.
        /// - Response
        ///   A successful order will be assigned an order id. A successful order is defined as one that has been accepted by the matching engine. Open orders will not expire until filled or canceled.
        /// </summary>
        /// <param name="instrument">Instrument ID, e.g. BTC-USD-190927-5000-C</param>
        /// <param name="side">buy or sell</param>
        /// <param name="price">Price of each contract</param>
        /// <param name="size">The buying or selling quantity （in lots)</param>
        /// <param name="timeInForce">‘0’: Normal order. Parameter will be deemed as '0' if left blank.</param>
        /// <param name="match_price">Whether order is placed at best counter party price (‘0’:no ‘1’:yes). The parameter is defaulted as ‘0’. If it is set as '1', the price parameter will be ignored，</param>
        /// <param name="clientOrderId">You can customize order IDs to identify your orders. The system supports alphabets + numbers(case-sensitive，e.g:A123、a123), or alphabets (case-sensitive，e.g:Abc、abc) only, between 1-32 characters.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<OkexOptionsPlacedOrder>> Options_PlaceOrder_Async(string instrument, OkexOptionsOrderSide side, decimal price, decimal size, OkexOptionsTimeInForce timeInForce = OkexOptionsTimeInForce.NormalOrder, bool match_price = false, string? clientOrderId = null, CancellationToken ct = default)
        {
            instrument = instrument.ValidateSymbol();
            clientOrderId?.ValidateStringLength("clientOrderId", 0, 32);
            if (clientOrderId != null && !Regex.IsMatch(clientOrderId, "^(([a-z]|[A-Z]|[0-9]){0,32})$"))
                throw new ArgumentException("ClientOrderId supports alphabets (case-sensitive) + numbers, or letters (case-sensitive) between 1-32 characters.");

            var parameters = new Dictionary<string, object>
            {
                { "instrument_id", instrument },
                { "size", size },
                { "price", price },
                { "match_price", match_price ? 1 : 0 },
                { "side", JsonConvert.SerializeObject(side, new OptionsOrderSideConverter(false)) },
                { "order_type", JsonConvert.SerializeObject(timeInForce, new OptionsTimeInForceConverter(false)) },
            };
            parameters.AddOptionalParameter("client_oid", clientOrderId);

            return await SendRequest<OkexOptionsPlacedOrder>(GetUrl(Endpoints_Options_PlaceOrder), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Place contract orders in a batch. Maximum 10 orders can be placed at a time for each underlying.
        /// Rate Limit：20 Requests per 2 seconds
        /// Notes
        /// - If the error_code value in response is NOT zero, then it means the request was rejected because verification failed.
        /// - The client_oid is optional and you can customize it using alpha-numeric characters with length 1 to 32. This parameter is used to identify your orders in the public orders feed.No warning is sent when client_oid is not unique. In case of multiple identical client_oid, only the latest entry will be returned.
        /// - As long as any of the orders are successful, result returns ‘true’. The response message is returned in the same order as that of the order_data submitted.If the order fails to be placed, order_id is ‘-1’.
        /// </summary>
        /// <param name="underlying">The underlying index that the contract is based upon, e.g. BTC-USD. Each batch of orders must be mapped to a single underlying.</param>
        /// <param name="orders">Orders List</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<OkexOptionsBatchOrders> Options_BatchPlaceOrders(string underlying, IEnumerable<OkexOptionsPlaceOrder> orders, CancellationToken ct = default) => Options_BatchPlaceOrders_Async(underlying, orders, ct).Result;
        /// <summary>
        /// Place contract orders in a batch. Maximum 10 orders can be placed at a time for each underlying.
        /// Rate Limit：20 Requests per 2 seconds
        /// Notes
        /// - If the error_code value in response is NOT zero, then it means the request was rejected because verification failed.
        /// - The client_oid is optional and you can customize it using alpha-numeric characters with length 1 to 32. This parameter is used to identify your orders in the public orders feed.No warning is sent when client_oid is not unique. In case of multiple identical client_oid, only the latest entry will be returned.
        /// - As long as any of the orders are successful, result returns ‘true’. The response message is returned in the same order as that of the order_data submitted.If the order fails to be placed, order_id is ‘-1’.
        /// </summary>
        /// <param name="underlying">The underlying index that the contract is based upon, e.g. BTC-USD. Each batch of orders must be mapped to a single underlying.</param>
        /// <param name="orders">Orders List</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<OkexOptionsBatchOrders>> Options_BatchPlaceOrders_Async(string underlying, IEnumerable<OkexOptionsPlaceOrder> orders, CancellationToken ct = default)
        {
            if (orders == null || orders.Count() == 0)
                throw new ArgumentException("Orders cant be null or with zero-elements");

            underlying = underlying.ValidateSymbol();
            for (var i = 0; i < orders.Count(); i++)
            {
                var order = orders.ElementAt(i);
                var suffix = $"(Order: {(i + 1)} of {orders.Count()})";

                order.ClientOrderId?.ValidateStringLength("clientOrderId", 0, 32, messageSuffix: suffix);
                if (order.ClientOrderId != null && !Regex.IsMatch(order.ClientOrderId, "^(([a-z]|[A-Z]|[0-9]){0,32})$"))
                    throw new ArgumentException($"ClientOrderId supports alphabets (case-sensitive) + numbers, or letters (case-sensitive) between 1-32 characters. {suffix}");
            }

            var parameters = new Dictionary<string, object>
            {
                { "underlying", underlying },
                { "order_data", orders },
            };

            return await SendRequest<OkexOptionsBatchOrders>(GetUrl(Endpoints_Options_PlaceBatchOrders), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Cancel an unfilled order
        /// Rate Limit：20 Requests per second
        /// Notes
        /// - If the error_code value in response is NOT zero, then it means the request was rejected because verification failed.
        /// - Only one of order_id or client_oid parameters should be passed per request.
        /// - The client_oid should be unique. No warning is sent when client_oid is not unique.
        /// - In case of multiple identical client_oid, only the latest entry will be returned.
        /// - If the order cannot be canceled because it has already been filled or canceled, the reason will be returned with the error message.
        /// </summary>
        /// <param name="underlying">The underlying index that the contract Filled Price based upon, e.g. BTC-USD.</param>
        /// <param name="orderId">Either client_oid or order_id must be present. Order ID</param>
        /// <param name="clientOrderId">Either client_oid or order_id must be present. client_oid should be the same Client-supplied order ID when submitting the order. It should be comprised of alpha-numeric characters with length 1 to 32. Both uppercase and lowercase are supported.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<OkexOptionsPlacedOrder> Options_CancelOrder(string underlying, long? orderId = null, string? clientOrderId = null, CancellationToken ct = default) => Options_CancelOrder_Async(underlying, orderId, clientOrderId, ct).Result;
        /// <summary>
        /// Cancel an unfilled order
        /// Rate Limit：20 Requests per second
        /// Notes
        /// - If the error_code value in response is NOT zero, then it means the request was rejected because verification failed.
        /// - Only one of order_id or client_oid parameters should be passed per request.
        /// - The client_oid should be unique. No warning is sent when client_oid is not unique.
        /// - In case of multiple identical client_oid, only the latest entry will be returned.
        /// - If the order cannot be canceled because it has already been filled or canceled, the reason will be returned with the error message.
        /// </summary>
        /// <param name="underlying">The underlying index that the contract Filled Price based upon, e.g. BTC-USD.</param>
        /// <param name="orderId">Either client_oid or order_id must be present. Order ID</param>
        /// <param name="clientOrderId">Either client_oid or order_id must be present. client_oid should be the same Client-supplied order ID when submitting the order. It should be comprised of alpha-numeric characters with length 1 to 32. Both uppercase and lowercase are supported.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<OkexOptionsPlacedOrder>> Options_CancelOrder_Async(string underlying, long? orderId = null, string? clientOrderId = null, CancellationToken ct = default)
        {
            underlying = underlying.ValidateSymbol();

            if (orderId == null && string.IsNullOrEmpty(clientOrderId))
                throw new ArgumentException("Either orderId or clientOrderId must be present.");

            if (orderId != null && !string.IsNullOrEmpty(clientOrderId))
                throw new ArgumentException("Either orderId or clientOrderId must be present.");

            return await SendRequest<OkexOptionsPlacedOrder>(GetUrl(Endpoints_Options_CancelOrder.Replace("<underlying>", underlying), orderId.HasValue ? orderId.ToString() : clientOrderId!), HttpMethod.Post, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Batch cancel open orders; a maximum of 10 orders can be canceled at a time.
        /// Rate Limit：20 Requests per 2 seconds
        /// When an Order ID is listed to be canceled in the result list, it does not imply the order has successfully been canceled. Orders in the middle of being filled cannot be canceled; only unfilled orders can be canceled.
        /// Notes:
        /// - If the error_code value in response is NOT zero, then it means the request was rejected because verification failed.
        /// - For batch order cancellation, only one of order_ids or client_oids parameters should be passed per request.Otherwise an error will be returned.
        /// - When using client_oid for batch order cancellation, you need to make sure the ID is unique.In case of multiple identical client_oid, only the latest entry will be returned.
        /// - Cancellation of orders are not guaranteed.After placing a batch cancel order you should confirm they are successfully canceled by requesting the "Order List" endpoint.
        /// </summary>
        /// <param name="underlying">The underlying index that the contract is based upon, e.g. BTC-USD. Each batch of orders must be mapped to a single underlying.</param>
        /// <param name="orderIds">Either client_oids or order_ids must be present. List of Order ID.</param>
        /// <param name="clientOrderIds">Either client_oids or order_ids must be present. List of client_oid, which should be the same Client-supplied order ID when submitting the order. It should be comprised of alpha-numeric characters with length 1 to 32. Both uppercase and lowercase are supported.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<OkexOptionsBatchOrders> Options_BatchCancelOrders(string underlying, IEnumerable<long> orderIds, IEnumerable<string> clientOrderIds, CancellationToken ct = default) => Options_BatchCancelOrders_Async(underlying, orderIds, clientOrderIds, ct).Result;
        /// <summary>
        /// Batch cancel open orders; a maximum of 10 orders can be canceled at a time.
        /// Rate Limit：20 Requests per 2 seconds
        /// When an Order ID is listed to be canceled in the result list, it does not imply the order has successfully been canceled. Orders in the middle of being filled cannot be canceled; only unfilled orders can be canceled.
        /// Notes:
        /// - If the error_code value in response is NOT zero, then it means the request was rejected because verification failed.
        /// - For batch order cancellation, only one of order_ids or client_oids parameters should be passed per request.Otherwise an error will be returned.
        /// - When using client_oid for batch order cancellation, you need to make sure the ID is unique.In case of multiple identical client_oid, only the latest entry will be returned.
        /// - Cancellation of orders are not guaranteed.After placing a batch cancel order you should confirm they are successfully canceled by requesting the "Order List" endpoint.
        /// </summary>
        /// <param name="underlying">The underlying index that the contract is based upon, e.g. BTC-USD. Each batch of orders must be mapped to a single underlying.</param>
        /// <param name="orderIds">Either client_oids or order_ids must be present. List of Order ID.</param>
        /// <param name="clientOrderIds">Either client_oids or order_ids must be present. List of client_oid, which should be the same Client-supplied order ID when submitting the order. It should be comprised of alpha-numeric characters with length 1 to 32. Both uppercase and lowercase are supported.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<OkexOptionsBatchOrders>> Options_BatchCancelOrders_Async(string underlying, IEnumerable<long> orderIds, IEnumerable<string> clientOrderIds, CancellationToken ct = default)
        {
            underlying = underlying.ValidateSymbol();

            if ((orderIds == null || orderIds.Count() == 0) && (clientOrderIds == null || clientOrderIds.Count() == 0))
                throw new ArgumentException("Either orderIds or clientOrderIds must be present.");

            if ((orderIds != null && orderIds.Count() > 0) && (clientOrderIds != null && clientOrderIds.Count() > 0))
                throw new ArgumentException("Either orderIds or clientOrderIds must be present.");

            var parameters = new Dictionary<string, object>();
            if (orderIds != null && orderIds.Count() > 0) parameters.Add("order_ids", orderIds);
            if (clientOrderIds != null && clientOrderIds.Count() > 0) parameters.Add("client_oids", clientOrderIds);

            return await SendRequest<OkexOptionsBatchOrders>(GetUrl(Endpoints_Options_CancelBatchOrders, underlying), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Cancel all pending orders placed on the same Underlying
        /// Rate Limit：1 Request per second
        /// </summary>
        /// <param name="underlying">The underlying index that the contract is based upon, e.g. BTC-USD.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<OkexOptionsResponse> Options_CancelAllOrders(string underlying, CancellationToken ct = default) => Options_CancelAllOrders_Async(underlying, ct).Result;
        /// <summary>
        /// Cancel all pending orders placed on the same Underlying
        /// Rate Limit：1 Request per second
        /// </summary>
        /// <param name="underlying">The underlying index that the contract is based upon, e.g. BTC-USD.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<OkexOptionsResponse>> Options_CancelAllOrders_Async(string underlying, CancellationToken ct = default)
        {
            underlying = underlying.ValidateSymbol();

            return await SendRequest<OkexOptionsResponse>(GetUrl(Endpoints_Options_CancelAll, underlying), HttpMethod.Post, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Modify an unfilled order
        /// Rate Limit：20 Requests per second
        /// Notes:
        /// - If the error_code value in response is NOT zero, then it means the request was rejected because verification failed.
        /// - In order amendment, only order_id will be used if both order_id and client_oid are passed in values at the same time, and client_oid will be ignored.
        /// - The client_oid should be unique.No warning is sent when client_oid is not unique.
        /// - In case of multiple identical client_oid, only the latest entry will be returned.
        /// - If the order cannot be modified because it has already been filled or canceled, the reason will be returned with the error message.
        /// </summary>
        /// <param name="underlying">The underlying index that the contract is based upon, e.g. BTC-USD.</param>
        /// <param name="orderId">Either client_oid or order_id must be present. Order ID。</param>
        /// <param name="clientOrderId">Either client_oid or order_id must be present. client_oid should be the same Client-supplied order ID when submitting the order. It should be comprised of alpha-numeric characters with length 1 to 32. Both uppercase and lowercase are supported.</param>
        /// <param name="requestId">You can provide the request_id. If provided, the response will include the corresponding request_id to help you identify the request. It should be comprised of alpha-numeric characters with length 1 to 32. Both uppercase and lowercase are supported.</param>
        /// <param name="newSize">Must provide at least one of new_size or new_price. When modifying a partially filled order, the new_size should include the amount that has been filled.</param>
        /// <param name="newPrice">Must provide at least one of new_size or new_price. Modifies the price.</param>
        /// <param name="cancelOnFail">When the order amendment fails, whether to cancell the order automatically: 0: Don't cancel the order automatically 1: Automatically cancel the order. The default value is 0.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<OkexOptionsPlacedOrder> Options_ModifyOrder(string underlying, long? orderId = null, string? clientOrderId = null, string? requestId = null, decimal? newSize = null, decimal? newPrice = null, bool? cancelOnFail = null, CancellationToken ct = default) => Options_ModifyOrder_Async(underlying, orderId, clientOrderId, requestId, newSize, newPrice, cancelOnFail, ct).Result;
        /// <summary>
        /// Modify an unfilled order
        /// Rate Limit：20 Requests per second
        /// Notes:
        /// - If the error_code value in response is NOT zero, then it means the request was rejected because verification failed.
        /// - In order amendment, only order_id will be used if both order_id and client_oid are passed in values at the same time, and client_oid will be ignored.
        /// - The client_oid should be unique.No warning is sent when client_oid is not unique.
        /// - In case of multiple identical client_oid, only the latest entry will be returned.
        /// - If the order cannot be modified because it has already been filled or canceled, the reason will be returned with the error message.
        /// </summary>
        /// <param name="underlying">The underlying index that the contract is based upon, e.g. BTC-USD.</param>
        /// <param name="orderId">Either client_oid or order_id must be present. Order ID。</param>
        /// <param name="clientOrderId">Either client_oid or order_id must be present. client_oid should be the same Client-supplied order ID when submitting the order. It should be comprised of alpha-numeric characters with length 1 to 32. Both uppercase and lowercase are supported.</param>
        /// <param name="requestId">You can provide the request_id. If provided, the response will include the corresponding request_id to help you identify the request. It should be comprised of alpha-numeric characters with length 1 to 32. Both uppercase and lowercase are supported.</param>
        /// <param name="newSize">Must provide at least one of new_size or new_price. When modifying a partially filled order, the new_size should include the amount that has been filled.</param>
        /// <param name="newPrice">Must provide at least one of new_size or new_price. Modifies the price.</param>
        /// <param name="cancelOnFail">When the order amendment fails, whether to cancell the order automatically: 0: Don't cancel the order automatically 1: Automatically cancel the order. The default value is 0.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<OkexOptionsPlacedOrder>> Options_ModifyOrder_Async(string underlying, long? orderId = null, string? clientOrderId = null, string? requestId = null, decimal? newSize = null, decimal? newPrice = null, bool? cancelOnFail = null, CancellationToken ct = default)
        {
            underlying = underlying.ValidateSymbol();

            if (orderId == null && string.IsNullOrEmpty(clientOrderId))
                throw new ArgumentException("Either orderId or clientOrderId must be present.");

            if (orderId != null && !string.IsNullOrEmpty(clientOrderId))
                throw new ArgumentException("Either orderId or clientOrderId must be present.");

            if (!newSize.HasValue && !newPrice.HasValue)
                throw new ArgumentException("Must provide at least one of new_size or new_price");

            var parameters = new Dictionary<string, object>();
            if (orderId.HasValue) parameters.AddOptionalParameter("order_id", orderId);
            if (!string.IsNullOrEmpty(clientOrderId)) parameters.AddOptionalParameter("client_oid", clientOrderId);
            if (cancelOnFail.HasValue) parameters.AddOptionalParameter("cancel_on_fail", cancelOnFail.Value ? 1 : 0);
            if (!string.IsNullOrEmpty(requestId)) parameters.AddOptionalParameter("request_id", requestId);
            if (newSize.HasValue) parameters.AddOptionalParameter("new_size", newSize);
            if (newPrice.HasValue) parameters.AddOptionalParameter("new_price", newPrice);

            return await SendRequest<OkexOptionsPlacedOrder>(GetUrl(Endpoints_Options_MofiyOrder, underlying), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Batch modify open orders; a maximum of 10 orders per underlying can be modified.
        /// Rate Limit：20 Requests per 2 seconds
        /// Notes:
        /// - When an Order ID is listed to be modified in the result list, it does not imply the order has successfully been modified. Orders in the middle of being filled cannot be modified; only unfilled orders can be modified.
        /// - If the error_code value in response is NOT zero, then it means the request was rejected because verification failed.
        /// - When using client_oid for batch order modifications, you need to make sure the ID is unique.In case of multiple identical client_oid, only the latest entry will be returned.
        /// - Modifications of orders are not guaranteed.After placing a modification order you should confirm they are successfully modified by requesting the "Order List" endpoint.
        /// </summary>
        /// <param name="underlying">The underlying index that the contract is based upon, e.g. BTC-USD. Each batch of orders must be mapped to a single underlying.</param>
        /// <param name="orders">Orders Lİst</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<OkexOptionsBatchOrders> Options_BatchModifyOrders(string underlying, IEnumerable<OkexOptionsModifyOrder> orders, CancellationToken ct = default) => Options_BatchModifyOrders_Async(underlying, orders, ct).Result;
        /// <summary>
        /// Batch modify open orders; a maximum of 10 orders per underlying can be modified.
        /// Rate Limit：20 Requests per 2 seconds
        /// Notes:
        /// - When an Order ID is listed to be modified in the result list, it does not imply the order has successfully been modified. Orders in the middle of being filled cannot be modified; only unfilled orders can be modified.
        /// - If the error_code value in response is NOT zero, then it means the request was rejected because verification failed.
        /// - When using client_oid for batch order modifications, you need to make sure the ID is unique.In case of multiple identical client_oid, only the latest entry will be returned.
        /// - Modifications of orders are not guaranteed.After placing a modification order you should confirm they are successfully modified by requesting the "Order List" endpoint.
        /// </summary>
        /// <param name="underlying">The underlying index that the contract is based upon, e.g. BTC-USD. Each batch of orders must be mapped to a single underlying.</param>
        /// <param name="orders">Orders Lİst</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<OkexOptionsBatchOrders>> Options_BatchModifyOrders_Async(string underlying, IEnumerable<OkexOptionsModifyOrder> orders, CancellationToken ct = default)
        {
            if (orders == null || orders.Count() == 0)
                throw new ArgumentException("Orders cant be null or with zero-elements");

            if (orders.Count() > 10)
                throw new ArgumentException("Exceed maximum order count(10)");

            underlying = underlying.ValidateSymbol();
            for (var i = 0; i < orders.Count(); i++)
            {
                var order = orders.ElementAt(i);
                var suffix = $"(Order: {(i + 1)} of {orders.Count()})";

                if (order.OrderId == null && string.IsNullOrEmpty(order.ClientOrderId))
                    throw new ArgumentException($"Either orderId or clientOrderId must be present. {suffix}");

                if (order.OrderId != null && !string.IsNullOrEmpty(order.ClientOrderId))
                    throw new ArgumentException($"Either orderId or clientOrderId must be present. {suffix}");

                if (!order.NewSize.HasValue && !order.NewPrice.HasValue)
                    throw new ArgumentException($"Must provide at least one of new_size or new_price. {suffix}");
            }

            var parameters = new Dictionary<string, object>
            {
                { "amend_data", orders },
            };

            return await SendRequest<OkexOptionsBatchOrders>(GetUrl(Endpoints_Options_BatchModifyOrders, underlying), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve order details. Unfilled orders will be kept in record for only two hours after it is canceled.
        /// Rate Limit：40 Requests per 2 seconds
        /// Notes
        /// - Only one of order_id or client_oid parameters should be passed per request.
        /// - The client_oid should be unique. No warning is sent when client_oid is not unique. In case of multiple identical client_oid, only the latest entry will be returned.
        /// </summary>
        /// <param name="underlying">The underlying index that the contract is based upon, e.g. BTC-USD.</param>
        /// <param name="orderId">Either client_oid or order_id must be present. Order ID。</param>
        /// <param name="clientOrderId">Either client_oid or order_id must be present. client_oid should be the same Client-supplied order ID when submitting the order. It should be comprised of alpha-numeric characters with length 1 to 32. Both uppercase and lowercase are supported.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<OkexOptionsOrder> Options_GetOrderDetails(string underlying, long? orderId = null, string? clientOrderId = null, CancellationToken ct = default) => Options_GetOrderDetails_Async(underlying, orderId, clientOrderId, ct).Result;
        /// <summary>
        /// Retrieve order details. Unfilled orders will be kept in record for only two hours after it is canceled.
        /// Rate Limit：40 Requests per 2 seconds
        /// Notes
        /// - Only one of order_id or client_oid parameters should be passed per request.
        /// - The client_oid should be unique. No warning is sent when client_oid is not unique. In case of multiple identical client_oid, only the latest entry will be returned.
        /// </summary>
        /// <param name="underlying">The underlying index that the contract is based upon, e.g. BTC-USD.</param>
        /// <param name="orderId">Either client_oid or order_id must be present. Order ID。</param>
        /// <param name="clientOrderId">Either client_oid or order_id must be present. client_oid should be the same Client-supplied order ID when submitting the order. It should be comprised of alpha-numeric characters with length 1 to 32. Both uppercase and lowercase are supported.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<OkexOptionsOrder>> Options_GetOrderDetails_Async(string underlying, long? orderId = null, string? clientOrderId = null, CancellationToken ct = default)
        {
            underlying = underlying.ValidateSymbol();

            if (orderId == null && string.IsNullOrEmpty(clientOrderId))
                throw new ArgumentException("Either orderId or clientOrderId must be present.");

            if (orderId != null && !string.IsNullOrEmpty(clientOrderId))
                throw new ArgumentException("Either orderId or clientOrderId must be present.");

            return await SendRequest<OkexOptionsOrder>(GetUrl(Endpoints_Options_OrderDetails.Replace("<underlying>", underlying), orderId.HasValue ? orderId.ToString() : clientOrderId!), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve all orders of the underlying. This endpoint only retrieves data from the most recent 7 days.
        /// Rate Limit：10 Requests per 2 seconds
        /// </summary>
        /// <param name="underlying">The underlying index that the contract is based upon, e.g. BTC-USD.</param>
        /// <param name="state">Order State: -2 = Failed -1 = Canceled 0 = Open 1 = Partially Filled 2 = Fully Filled 3 = Submitting 4 = Canceling 6 = Incomplete (Open + Partially Filled) 7 = Complete (Canceled + Fully Filled)</param>
        /// <param name="instrument">Instrument ID，e.g. BTC-USD-190927-12500-C. If the asset referenced by the instrument_id differs from that of the underlying, error code will be returned.</param>
        /// <param name="limit">Number of results per request. The maximum is 100; the default is 100</param>
        /// <param name="before">Pagination of data to return records newer than the requested order_id	</param>
        /// <param name="after">Pagination of data to return records earlier than the requested order_id</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<OkexOptionsOrderList> Options_GetAllOrders(string underlying, OkexOptionsOrderState state, string? instrument = null, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default) => Options_GetAllOrders_Async(underlying, state, instrument, limit, before, after, ct).Result;
        /// <summary>
        /// Retrieve all orders of the underlying. This endpoint only retrieves data from the most recent 7 days.
        /// Rate Limit：10 Requests per 2 seconds
        /// </summary>
        /// <param name="underlying">The underlying index that the contract is based upon, e.g. BTC-USD.</param>
        /// <param name="state">Order State: -2 = Failed -1 = Canceled 0 = Open 1 = Partially Filled 2 = Fully Filled 3 = Submitting 4 = Canceling 6 = Incomplete (Open + Partially Filled) 7 = Complete (Canceled + Fully Filled)</param>
        /// <param name="instrument">Instrument ID，e.g. BTC-USD-190927-12500-C. If the asset referenced by the instrument_id differs from that of the underlying, error code will be returned.</param>
        /// <param name="limit">Number of results per request. The maximum is 100; the default is 100</param>
        /// <param name="before">Pagination of data to return records newer than the requested order_id	</param>
        /// <param name="after">Pagination of data to return records earlier than the requested order_id</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<OkexOptionsOrderList>> Options_GetAllOrders_Async(string underlying, OkexOptionsOrderState state, string? instrument = null, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default)
        {
            underlying = underlying.ValidateSymbol();
            limit.ValidateIntBetween(nameof(limit), 1, 100);

            var parameters = new Dictionary<string, object>
            {
                { "state", JsonConvert.SerializeObject(state, new OptionsOrderStateConverter(false)) },
                { "limit", limit },
            };
            parameters.AddOptionalParameter("instrument_id", instrument);
            parameters.AddOptionalParameter("before", before);
            parameters.AddOptionalParameter("after", after);

            return await SendRequest<OkexOptionsOrderList>(GetUrl(Endpoints_Options_OrderList, underlying), HttpMethod.Get, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve recently filled transaction details. This endpoint only retrieves data from the most recent 7 days.
        /// Rate Limit：10 Requests per 2 seconds
        /// </summary>
        /// <param name="underlying">The underlying index that the contract is based upon, e.g. BTC-USD.</param>
        /// <param name="instrument">Instrument ID, e.g. BTC-USD-190927-12500-C</param>
        /// <param name="orderId">Order ID</param>
        /// <param name="limit">Number of results per request. The maximum is 100; the default is 100</param>
        /// <param name="before">Pagination of data to return records newer than the requested trade_id</param>
        /// <param name="after">Pagination of data to return records earlier than the requested trade_id</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<IEnumerable<OkexOptionsTransaction>> Options_GetTransactionDetails(string underlying, string? instrument = null, long? orderId = null, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default) => Options_GetTransactionDetails_Async(underlying, instrument, orderId, limit, before, after, ct).Result;
        /// <summary>
        /// Retrieve recently filled transaction details. This endpoint only retrieves data from the most recent 7 days.
        /// Rate Limit：10 Requests per 2 seconds
        /// </summary>
        /// <param name="underlying">The underlying index that the contract is based upon, e.g. BTC-USD.</param>
        /// <param name="instrument">Instrument ID, e.g. BTC-USD-190927-12500-C</param>
        /// <param name="orderId">Order ID</param>
        /// <param name="limit">Number of results per request. The maximum is 100; the default is 100</param>
        /// <param name="before">Pagination of data to return records newer than the requested trade_id</param>
        /// <param name="after">Pagination of data to return records earlier than the requested trade_id</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<OkexOptionsTransaction>>> Options_GetTransactionDetails_Async(string underlying, string? instrument = null, long? orderId = null, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default)
        {
            underlying = underlying.ValidateSymbol();
            limit.ValidateIntBetween(nameof(limit), 1, 100);

            var parameters = new Dictionary<string, object>
            {
                { "limit", limit },
            };
            parameters.AddOptionalParameter("instrument_id", instrument);
            parameters.AddOptionalParameter("order_id", orderId);
            parameters.AddOptionalParameter("before", before);
            parameters.AddOptionalParameter("after", after);

            return await SendRequest<IEnumerable<OkexOptionsTransaction>>(GetUrl(Endpoints_Options_TransactionDetails, underlying), HttpMethod.Get, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve the bills of the options account. The bill refers to all transaction records that results in changing the balance of an account. Pagination is supported and the response is sorted with most recent first in reverse chronological order. This API can retrieve data from the last 7 days.
        /// Rate Limit：5 Requests per 2 seconds
        /// </summary>
        /// <param name="underlying">The underlying index that the contract is based upon, e.g. BTC-USD.</param>
        /// <param name="limit">Number of results per request. The maximum is 100; the default is 100</param>
        /// <param name="before">Pagination of data to return records newer than the requested ledger_id</param>
        /// <param name="after">Pagination of data to return records earlier than the requested ledger_id</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<IEnumerable<OkexOptionsBill>> Options_GetBills(string underlying, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default) => Options_GetBills_Async(underlying, limit, before, after, ct).Result;
        /// <summary>
        /// Retrieve the bills of the options account. The bill refers to all transaction records that results in changing the balance of an account. Pagination is supported and the response is sorted with most recent first in reverse chronological order. This API can retrieve data from the last 7 days.
        /// Rate Limit：5 Requests per 2 seconds
        /// </summary>
        /// <param name="underlying">The underlying index that the contract is based upon, e.g. BTC-USD.</param>
        /// <param name="limit">Number of results per request. The maximum is 100; the default is 100</param>
        /// <param name="before">Pagination of data to return records newer than the requested ledger_id</param>
        /// <param name="after">Pagination of data to return records earlier than the requested ledger_id</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<OkexOptionsBill>>> Options_GetBills_Async(string underlying, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default)
        {
            underlying = underlying.ValidateSymbol();
            limit.ValidateIntBetween(nameof(limit), 1, 100);

            var parameters = new Dictionary<string, object>
            {
                { "limit", limit },
            };
            parameters.AddOptionalParameter("before", before);
            parameters.AddOptionalParameter("after", after);

            return await SendRequest<IEnumerable<OkexOptionsBill>>(GetUrl(Endpoints_Options_Bills, underlying), HttpMethod.Get, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve the account's trading fee rates. The fee rates of the sub accounts are the same as the fee rate of the parent account (Updated daily at midnight)
        /// Rate Limit：20 requests per 2 seconds
        /// </summary>
        /// <param name="underlying">The underlying index that the contract Filled Price based upon, e.g. BTC-USD. ；Choose and enter one parameter between category and underlying</param>
        /// <param name="category">Fee Schedule Tier: 1:Tier 1; Choose and enter one parameter between category and underlying</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<OkexOptionsTradeFee> Options_GetTradeFeeRates(string? underlying = null, int? category = null, CancellationToken ct = default) => Options_GetTradeFeeRates_Async(underlying, category, ct).Result;
        /// <summary>
        /// Retrieve the account's trading fee rates. The fee rates of the sub accounts are the same as the fee rate of the parent account (Updated daily at midnight)
        /// Rate Limit：20 requests per 2 seconds
        /// </summary>
        /// <param name="underlying">The underlying index that the contract Filled Price based upon, e.g. BTC-USD. ；Choose and enter one parameter between category and underlying</param>
        /// <param name="category">Fee Schedule Tier: 1:Tier 1; Choose and enter one parameter between category and underlying</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<OkexOptionsTradeFee>> Options_GetTradeFeeRates_Async(string? underlying = null, int? category = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();

            if (string.IsNullOrEmpty(underlying) && !category.HasValue)
                throw new ArgumentException("Either underlying or category must be present.");

            parameters.AddOptionalParameter("underlying", underlying);
            parameters.AddOptionalParameter("category", category);

            return await SendRequest<OkexOptionsTradeFee>(GetUrl(Endpoints_Options_TradeFee), HttpMethod.Get, ct, parameters, signed: true).ConfigureAwait(false);
        }

        #endregion

        #region Public Unsigned Endpoints

        /// <summary>
        /// Retrieve supported underlying indexes for options trading
        /// Rate Limit：20 Requests per 2 seconds
        /// </summary>
        /// <param name="ct">Cancellation Token</param>
        /// <returns>Underlying String Array</returns>
        public WebCallResult<IEnumerable<string>> Options_GetUnderlyingList(CancellationToken ct = default) => Options_GetUnderlyingList_Async(ct).Result;
        /// <summary>
        /// Retrieve supported underlying indexes for options trading
        /// Rate Limit：20 Requests per 2 seconds
        /// </summary>
        /// <param name="ct">Cancellation Token</param>
        /// <returns>Underlying String Array</returns>
        public async Task<WebCallResult<IEnumerable<string>>> Options_GetUnderlyingList_Async(CancellationToken ct = default)
        {
            return await SendRequest<IEnumerable<string>>(GetUrl(Endpoints_Options_Underlying), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve list of instruments with open contracts for options trading.
        /// Rate Limit：20 Requests per 2 seconds
        /// </summary>
        /// <param name="underlying">The underlying index that the contract is based upon, e.g. BTC-USD.</param>
        /// <param name="instrument">Instrument ID, e.g. BTC-USD-190830-9000-C. If this parameter is provided, only data for corresponding instrument is returned.</param>
        /// <param name="delivery">Contract delivery date, with format "YYMMDD"，e.g. "190527". If this parameter is provided, only data for corresponding instrument is returned.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<IEnumerable<OkexOptionsInstrument>> Options_GetInstruments(string underlying, string? instrument = null, DateTime? delivery = null, CancellationToken ct = default) => Options_GetInstruments_Async(underlying, instrument, delivery, ct).Result;
        /// <summary>
        /// Retrieve list of instruments with open contracts for options trading.
        /// Rate Limit：20 Requests per 2 seconds
        /// </summary>
        /// <param name="underlying">The underlying index that the contract is based upon, e.g. BTC-USD.</param>
        /// <param name="instrument">Instrument ID, e.g. BTC-USD-190830-9000-C. If this parameter is provided, only data for corresponding instrument is returned.</param>
        /// <param name="delivery">Contract delivery date, with format "YYMMDD"，e.g. "190527". If this parameter is provided, only data for corresponding instrument is returned.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<OkexOptionsInstrument>>> Options_GetInstruments_Async(string underlying, string? instrument = null, DateTime? delivery = null, CancellationToken ct = default)
        {
            underlying = underlying.ValidateSymbol();

            var parameters = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(instrument)) parameters.AddParameter("instrument_id", instrument!);
            if (delivery.HasValue) parameters.AddParameter("delivery", delivery.Value.ToString("yyMMdd"));

            return await SendRequest<IEnumerable<OkexOptionsInstrument>>(GetUrl(Endpoints_Options_Instruments, underlying), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve market data summary for all instrument of an underlying index.
        /// Rate Limit：20 Requests per 2 seconds
        /// </summary>
        /// <param name="underlying">The underlying index that the contract is based upon, e.g. BTC-USD.</param>
        /// <param name="delivery">Contract delivery date, with format "YYMMDD"，e.g. "190527". If this parameter is provided, only data for corresponding instrument is returned.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<IEnumerable<OkexOptionsMarketData>> Options_GetMarketData(string underlying, DateTime? delivery = null, CancellationToken ct = default) => Options_GetMarketData_Async(underlying, delivery, ct).Result;
        /// <summary>
        /// Retrieve market data summary for all instrument of an underlying index.
        /// Rate Limit：20 Requests per 2 seconds
        /// </summary>
        /// <param name="underlying">The underlying index that the contract is based upon, e.g. BTC-USD.</param>
        /// <param name="delivery">Contract delivery date, with format "YYMMDD"，e.g. "190527". If this parameter is provided, only data for corresponding instrument is returned.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<OkexOptionsMarketData>>> Options_GetMarketData_Async(string underlying, DateTime? delivery = null, CancellationToken ct = default)
        {
            underlying = underlying.ValidateSymbol();

            var parameters = new Dictionary<string, object>();
            if (delivery.HasValue) parameters.AddParameter("delivery", delivery.Value.ToString("yyMMdd"));

            return await SendRequest<IEnumerable<OkexOptionsMarketData>>(GetUrl(Endpoints_Options_MarketData, underlying), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve market data summary of a single instrument.
        /// Rate Limit：20 Requests per 2 seconds
        /// </summary>
        /// <param name="underlying">The underlying index that the contract is based upon, e.g. BTC-USD.</param>
        /// <param name="instrument">Instrument ID，e.g. BTC-USD-190830-9000-C</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<OkexOptionsMarketData> Options_GetMarketData(string underlying, string instrument, CancellationToken ct = default) => Options_GetMarketData_Async(underlying, instrument, ct).Result;
        /// <summary>
        /// Retrieve market data summary of a single instrument.
        /// Rate Limit：20 Requests per 2 seconds
        /// </summary>
        /// <param name="underlying">The underlying index that the contract is based upon, e.g. BTC-USD.</param>
        /// <param name="instrument">Instrument ID，e.g. BTC-USD-190830-9000-C</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<OkexOptionsMarketData>> Options_GetMarketData_Async(string underlying, string instrument, CancellationToken ct = default)
        {
            underlying = underlying.ValidateSymbol();
            instrument = instrument.ValidateSymbol();
            var url = Endpoints_Options_MarketDataOfInstrument.Replace("<underlying>", underlying).Replace("<instrument_id>", instrument);

            return await SendRequest<OkexOptionsMarketData>(GetUrl(url), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve an instrument's order book.
        /// Rate Limit：20 Requests per 2 seconds
        /// Notes:
        /// - If size is left blank, 200 results will be returned. If size is '0', no data will be returned. If size is larger than '200', only 200 results will be returned.
        /// - Each entry of the asks/bids list is formatted as [Price，Quantity，Number of liquidated orders, Number of orders], e.g. ["5621.8", "125", "0", "5"]
        /// </summary>
        /// <param name="symbol">Instrument ID, e.g. BTC-USD-190830-9000-C</param>
        /// <param name="size">Number of market depth results returned. Maximum 200</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<OkexOptionsOrderBook> Options_GetOrderBook(string instrument, int size = 200, CancellationToken ct = default) => Options_GetOrderBook_Async(instrument, size, ct).Result;
        /// <summary>
        /// Retrieve an instrument's order book.
        /// Rate Limit：20 Requests per 2 seconds
        /// Notes:
        /// - If size is left blank, 200 results will be returned. If size is '0', no data will be returned. If size is larger than '200', only 200 results will be returned.
        /// - Each entry of the asks/bids list is formatted as [Price，Quantity，Number of liquidated orders, Number of orders], e.g. ["5621.8", "125", "0", "5"]
        /// </summary>
        /// <param name="symbol">Instrument ID, e.g. BTC-USD-190830-9000-C</param>
        /// <param name="size">Number of market depth results returned. Maximum 200</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<OkexOptionsOrderBook>> Options_GetOrderBook_Async(string instrument, int size, CancellationToken ct = default)
        {
            instrument = instrument.ValidateSymbol();
            size.ValidateIntBetween(nameof(size), 1, 200);

            var parameters = new Dictionary<string, object>
            {
                { "size", size}
            };

            return await SendRequest<OkexOptionsOrderBook>(GetUrl(Endpoints_Options_OrderBook, instrument), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve recent transactions of an instrument.
        /// Rate Limit：20 Requests per 2 seconds
        /// </summary>
        /// <param name="instrument">Instrument ID, e.g. BTC-USD-190830-9000-C</param>
        /// <param name="limit">Number of results per request. The maximum is 100; the default is 100</param>
        /// <param name="before">Pagination of data to return records newer than the requested trade_id</param>
        /// <param name="after">Pagination of data to return records earlier than the requested trade_id</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<IEnumerable<OkexOptionsTrade>> Options_GetTrades(string instrument, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default) => Options_GetTrades_Async(instrument, limit, before, after, ct).Result;
        /// <summary>
        /// Retrieve recent transactions of an instrument.
        /// Rate Limit：20 Requests per 2 seconds
        /// </summary>
        /// <param name="instrument">Instrument ID, e.g. BTC-USD-190830-9000-C</param>
        /// <param name="limit">Number of results per request. The maximum is 100; the default is 100</param>
        /// <param name="before">Pagination of data to return records newer than the requested trade_id</param>
        /// <param name="after">Pagination of data to return records earlier than the requested trade_id</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<OkexOptionsTrade>>> Options_GetTrades_Async(string instrument, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default)
        {
            instrument = instrument.ValidateSymbol();
            limit.ValidateIntBetween(nameof(limit), 1, 100);

            var parameters = new Dictionary<string, object>
            {
                { "limit", limit },
            };
            parameters.AddOptionalParameter("before", before);
            parameters.AddOptionalParameter("after", after);

            return await SendRequest<IEnumerable<OkexOptionsTrade>>(GetUrl(Endpoints_Options_Trades, instrument), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// This retrieves the latest traded price, best-bid price, best-ask price etc.
        /// Rate Limit：20 Requests per 2 seconds
        /// </summary>
        /// <param name="instrument">Instrument ID, e.g. BTC-USD-190830-9000-C</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<OkexOptionsTicker> Options_GetTicker(string instrument, CancellationToken ct = default) => Options_GetTicker_Async(instrument, ct).Result;
        /// <summary>
        /// This retrieves the latest traded price, best-bid price, best-ask price etc.
        /// Rate Limit：20 Requests per 2 seconds
        /// </summary>
        /// <param name="instrument">Instrument ID, e.g. BTC-USD-190830-9000-C</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<OkexOptionsTicker>> Options_GetTicker_Async(string instrument, CancellationToken ct = default)
        {
            return await SendRequest<OkexOptionsTicker>(GetUrl(Endpoints_Options_Ticker, instrument), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve the candlestick data of an instrument, This API can retrieve the latest 1440 entries of data.
        /// Rate Limit：20 Requests per 2 seconds
        /// Notes
        /// - The granularity field must be one of the following values: [60, 180, 300, 900, 1800, 3600, 7200, 14400, 21600, 43200, 86400, 604800, 2678400, 8035200, 16070400, 31536000]. Otherwise, your request will be rejected.These values correspond to timeslices representing[1 minute, 3 minutes, 5 minutes, 15 minutes, 30 minutes, 1 hour, 2 hours, 4 hours, 6 hours, 12 hours, 1 day, 1 week, 1 month, 3 months, 6 months and 1 year] respectively.
        /// - The candlestick data may be incomplete.
        /// - The data returned will be arranged in an array as below: [timestamp, open, high, low, close, volume, currency_volume]
        /// </summary>
        /// <param name="instrument">Instrument ID, e.g. BTC-USD-190830-9000-C</param>
        /// <param name="period">Bar size in seconds, default 60, must be one of [60/180/300/900/1800/3600/7200/14400/21600/43200/86400/604800] or returns error</param>
        /// <param name="start">Start time，in ISO 8601 format; if blank default 200 results</param>
        /// <param name="end">Start time，in ISO 8601 format; if blank default default 200 results</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<IEnumerable<OkexOptionsCandle>> Options_GetCandles(string instrument, OkexSpotPeriod period, DateTime? start = null, DateTime? end = null, CancellationToken ct = default) => Options_GetCandles_Async(instrument, period, start, end, ct).Result;
        /// <summary>
        /// Retrieve the candlestick data of an instrument, This API can retrieve the latest 1440 entries of data.
        /// Rate Limit：20 Requests per 2 seconds
        /// Notes
        /// - The granularity field must be one of the following values: [60, 180, 300, 900, 1800, 3600, 7200, 14400, 21600, 43200, 86400, 604800, 2678400, 8035200, 16070400, 31536000]. Otherwise, your request will be rejected.These values correspond to timeslices representing[1 minute, 3 minutes, 5 minutes, 15 minutes, 30 minutes, 1 hour, 2 hours, 4 hours, 6 hours, 12 hours, 1 day, 1 week, 1 month, 3 months, 6 months and 1 year] respectively.
        /// - The candlestick data may be incomplete.
        /// - The data returned will be arranged in an array as below: [timestamp, open, high, low, close, volume, currency_volume]
        /// </summary>
        /// <param name="instrument">Instrument ID, e.g. BTC-USD-190830-9000-C</param>
        /// <param name="period">Bar size in seconds, default 60, must be one of [60/180/300/900/1800/3600/7200/14400/21600/43200/86400/604800] or returns error</param>
        /// <param name="start">Start time，in ISO 8601 format; if blank default 200 results</param>
        /// <param name="end">Start time，in ISO 8601 format; if blank default default 200 results</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<OkexOptionsCandle>>> Options_GetCandles_Async(string instrument, OkexSpotPeriod period, DateTime? start = null, DateTime? end = null, CancellationToken ct = default)
        {
            instrument = instrument.ValidateSymbol();

            var parameters = new Dictionary<string, object>
            {
                { "granularity", JsonConvert.SerializeObject(period, new SpotPeriodConverter(false)) },
            };
            parameters.AddOptionalParameter("start", start?.DateTimeToIso8601String());
            parameters.AddOptionalParameter("end", end?.DateTimeToIso8601String());

            return await SendRequest<IEnumerable<OkexOptionsCandle>>(GetUrl(Endpoints_Options_Candles, instrument), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Get historical settlement/exercise records
        /// Rate Limit：1 Requests per 10 seconds
        /// </summary>
        /// <param name="underlying">The underlying index that the contract is based upon, e.g. BTC-USD.</param>
        /// <param name="start">Start time (ISO 8601 standard, for example:2019-12-30T11:08:06.122Z）</param>
        /// <param name="end">End time (ISO 8601 standard, for example:2019-12-30T11:08:06.122Z）</param>
        /// <param name="limit">The default is 5</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<IEnumerable<OkexOptionsSettlementHistory>> Options_GetSettlementHistory(string underlying, DateTime? start = null, DateTime? end = null, int limit = 5, CancellationToken ct = default) => Options_GetSettlementHistory_Async(underlying, start, end, limit, ct).Result;
        /// <summary>
        /// Get historical settlement/exercise records
        /// Rate Limit：1 Requests per 10 seconds
        /// </summary>
        /// <param name="underlying">The underlying index that the contract is based upon, e.g. BTC-USD.</param>
        /// <param name="start">Start time (ISO 8601 standard, for example:2019-12-30T11:08:06.122Z）</param>
        /// <param name="end">End time (ISO 8601 standard, for example:2019-12-30T11:08:06.122Z）</param>
        /// <param name="limit">The default is 5</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<OkexOptionsSettlementHistory>>> Options_GetSettlementHistory_Async(string underlying, DateTime? start = null, DateTime? end = null, int limit = 5, CancellationToken ct = default)
        {
            limit.ValidateIntBetween(nameof(limit), 1, 5);

            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("start", start);
            parameters.AddOptionalParameter("end", end);
            parameters.AddOptionalParameter("limit", limit);

            return await SendRequest<IEnumerable<OkexOptionsSettlementHistory>>(GetUrl(Endpoints_Options_SettlementHistory, underlying), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        #endregion

        #endregion
    }
}