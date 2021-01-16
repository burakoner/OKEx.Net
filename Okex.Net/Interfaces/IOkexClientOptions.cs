using CryptoExchange.Net.Objects;
using Okex.Net.Enums;
using Okex.Net.RestObjects;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Okex.Net.Interfaces
{
    public interface IOkexClientOptions
    {
        WebCallResult<OkexOptionsBatchOrders> Options_BatchCancelOrders(string underlying, IEnumerable<string> orderIds, IEnumerable<string> clientOrderIds, CancellationToken ct = default);
        Task<WebCallResult<OkexOptionsBatchOrders>> Options_BatchCancelOrders_Async(string underlying, IEnumerable<string> orderIds, IEnumerable<string> clientOrderIds, CancellationToken ct = default);
        WebCallResult<OkexOptionsBatchOrders> Options_BatchModifyOrders(string underlying, IEnumerable<OkexOptionsModifyOrder> orders, CancellationToken ct = default);
        Task<WebCallResult<OkexOptionsBatchOrders>> Options_BatchModifyOrders_Async(string underlying, IEnumerable<OkexOptionsModifyOrder> orders, CancellationToken ct = default);
        WebCallResult<OkexOptionsBatchOrders> Options_BatchPlaceOrders(string underlying, IEnumerable<OkexOptionsPlaceOrder> orders, CancellationToken ct = default);
        Task<WebCallResult<OkexOptionsBatchOrders>> Options_BatchPlaceOrders_Async(string underlying, IEnumerable<OkexOptionsPlaceOrder> orders, CancellationToken ct = default);
        WebCallResult<OkexOptionsResponse> Options_CancelAllOrders(string underlying, CancellationToken ct = default);
        Task<WebCallResult<OkexOptionsResponse>> Options_CancelAllOrders_Async(string underlying, CancellationToken ct = default);
        WebCallResult<OkexOptionsPlacedOrder> Options_CancelOrder(string underlying, long? orderId = null, string? clientOrderId = null, CancellationToken ct = default);
        Task<WebCallResult<OkexOptionsPlacedOrder>> Options_CancelOrder_Async(string underlying, long? orderId = null, string? clientOrderId = null, CancellationToken ct = default);
        WebCallResult<OkexOptionsOrderList> Options_GetAllOrders(string underlying, OkexOptionsOrderState state, string? instrument = null, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default);
        Task<WebCallResult<OkexOptionsOrderList>> Options_GetAllOrders_Async(string underlying, OkexOptionsOrderState state, string? instrument = null, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default);
        WebCallResult<OkexOptionsBalance> Options_GetBalances(string underlying, CancellationToken ct = default);
        Task<WebCallResult<OkexOptionsBalance>> Options_GetBalances_Async(string underlying, CancellationToken ct = default);
        WebCallResult<IEnumerable<OkexOptionsBill>> Options_GetBills(string underlying, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default);
        Task<WebCallResult<IEnumerable<OkexOptionsBill>>> Options_GetBills_Async(string underlying, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default);
        WebCallResult<IEnumerable<OkexOptionsCandle>> Options_GetCandles(string instrument, OkexSpotPeriod period, DateTime? start = null, DateTime? end = null, CancellationToken ct = default);
        Task<WebCallResult<IEnumerable<OkexOptionsCandle>>> Options_GetCandles_Async(string instrument, OkexSpotPeriod period, DateTime? start = null, DateTime? end = null, CancellationToken ct = default);
        WebCallResult<IEnumerable<OkexOptionsInstrument>> Options_GetInstruments(string underlying, string? instrument = null, DateTime? delivery = null, CancellationToken ct = default);
        Task<WebCallResult<IEnumerable<OkexOptionsInstrument>>> Options_GetInstruments_Async(string underlying, string? instrument = null, DateTime? delivery = null, CancellationToken ct = default);
        WebCallResult<IEnumerable<OkexOptionsMarketData>> Options_GetMarketData(string underlying, DateTime? delivery = null, CancellationToken ct = default);
        WebCallResult<OkexOptionsMarketData> Options_GetMarketData(string underlying, string instrument, CancellationToken ct = default);
        Task<WebCallResult<IEnumerable<OkexOptionsMarketData>>> Options_GetMarketData_Async(string underlying, DateTime? delivery = null, CancellationToken ct = default);
        Task<WebCallResult<OkexOptionsMarketData>> Options_GetMarketData_Async(string underlying, string instrument, CancellationToken ct = default);
        WebCallResult<OkexOptionsOrderBook> Options_GetOrderBook(string instrument, int size = 200, CancellationToken ct = default);
        Task<WebCallResult<OkexOptionsOrderBook>> Options_GetOrderBook_Async(string instrument, int size, CancellationToken ct = default);
        WebCallResult<OkexOptionsOrder> Options_GetOrderDetails(string underlying, long? orderId = null, string? clientOrderId = null, CancellationToken ct = default);
        Task<WebCallResult<OkexOptionsOrder>> Options_GetOrderDetails_Async(string underlying, long? orderId = null, string? clientOrderId = null, CancellationToken ct = default);
        WebCallResult<OkexOptionsPositions> Options_GetPositions(string underlying, string? instrument = null, CancellationToken ct = default);
        Task<WebCallResult<OkexOptionsPositions>> Options_GetPositions_Async(string underlying, string? instrument = null, CancellationToken ct = default);
        WebCallResult<IEnumerable<OkexOptionsSettlementHistory>> Options_GetSettlementHistory(string underlying, DateTime? start = null, DateTime? end = null, int limit = 5, CancellationToken ct = default);
        Task<WebCallResult<IEnumerable<OkexOptionsSettlementHistory>>> Options_GetSettlementHistory_Async(string underlying, DateTime? start = null, DateTime? end = null, int limit = 5, CancellationToken ct = default);
        WebCallResult<OkexOptionsTicker> Options_GetTicker(string instrument, CancellationToken ct = default);
        Task<WebCallResult<OkexOptionsTicker>> Options_GetTicker_Async(string instrument, CancellationToken ct = default);
        WebCallResult<OkexOptionsTradeFee> Options_GetTradeFeeRates(string? underlying = null, int? category = null, CancellationToken ct = default);
        Task<WebCallResult<OkexOptionsTradeFee>> Options_GetTradeFeeRates_Async(string? underlying = null, int? category = null, CancellationToken ct = default);
        WebCallResult<IEnumerable<OkexOptionsTrade>> Options_GetTrades(string instrument, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default);
        Task<WebCallResult<IEnumerable<OkexOptionsTrade>>> Options_GetTrades_Async(string instrument, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default);
        WebCallResult<IEnumerable<OkexOptionsTransaction>> Options_GetTransactionDetails(string underlying, string? instrument = null, long? orderId = null, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default);
        Task<WebCallResult<IEnumerable<OkexOptionsTransaction>>> Options_GetTransactionDetails_Async(string underlying, string? instrument = null, long? orderId = null, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default);
        WebCallResult<IEnumerable<string>> Options_GetUnderlyingList(CancellationToken ct = default);
        Task<WebCallResult<IEnumerable<string>>> Options_GetUnderlyingList_Async(CancellationToken ct = default);
        WebCallResult<OkexOptionsPlacedOrder> Options_ModifyOrder(string underlying, long? orderId = null, string? clientOrderId = null, string? requestId = null, decimal? newSize = null, decimal? newPrice = null, bool? cancelOnFail = null, CancellationToken ct = default);
        Task<WebCallResult<OkexOptionsPlacedOrder>> Options_ModifyOrder_Async(string underlying, long? orderId = null, string? clientOrderId = null, string? requestId = null, decimal? newSize = null, decimal? newPrice = null, bool? cancelOnFail = null, CancellationToken ct = default);
        WebCallResult<OkexOptionsPlacedOrder> Options_PlaceOrder(string instrument, OkexOptionsOrderSide side, decimal price, decimal size, OkexOptionsTimeInForce timeInForce = OkexOptionsTimeInForce.NormalOrder, bool match_price = false, string? clientOrderId = null, CancellationToken ct = default);
        Task<WebCallResult<OkexOptionsPlacedOrder>> Options_PlaceOrder_Async(string instrument, OkexOptionsOrderSide side, decimal price, decimal size, OkexOptionsTimeInForce timeInForce = OkexOptionsTimeInForce.NormalOrder, bool match_price = false, string? clientOrderId = null, CancellationToken ct = default);
    }
}