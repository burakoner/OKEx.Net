using CryptoExchange.Net.Objects;
using Okex.Net.Enums;
using Okex.Net.RestObjects;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Okex.Net.Interfaces
{
    public interface IOkexClientSpot
    {
        WebCallResult<OkexSpotAlgoCancelledOrder> Spot_AlgoCancelOrder(string symbol, OkexAlgoOrderType type, IEnumerable<string> algo_ids, CancellationToken ct = default);
        Task<WebCallResult<OkexSpotAlgoCancelledOrder>> Spot_AlgoCancelOrder_Async(string symbol, OkexAlgoOrderType type, IEnumerable<string> algo_ids, CancellationToken ct = default);
        WebCallResult<Dictionary<string, IEnumerable<OkexSpotAlgoOrder>>> Spot_AlgoGetOrders(string symbol, OkexAlgoOrderType type, OkexAlgoStatus? status = null, IEnumerable<string> algo_ids = null, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default);
        Task<WebCallResult<Dictionary<string, IEnumerable<OkexSpotAlgoOrder>>>> Spot_AlgoGetOrders_Async(string symbol, OkexAlgoOrderType type, OkexAlgoStatus? status = null, IEnumerable<string> algo_ids = null, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default);
        WebCallResult<OkexSpotAlgoPlacedOrder> Spot_AlgoPlaceOrder(string symbol, OkexAlgoOrderType type, OkexMarket mode, OkexSpotOrderSide side, decimal size, decimal? trigger_price = null, decimal? trigger_algo_price = null, OkexAlgoPriceType? trigger_algo_type = null, decimal? trail_callback_rate = null, decimal? trail_trigger_price = null, decimal? iceberg_algo_variance = null, decimal? iceberg_avg_amount = null, decimal? iceberg_limit_price = null, decimal? twap_sweep_range = null, decimal? twap_sweep_ratio = null, int? twap_single_limit = null, decimal? twap_limit_price = null, int? twap_time_interval = null, OkexAlgoPriceType? tp_trigger_type = null, decimal? tp_trigger_price = null, decimal? tp_price = null, OkexAlgoPriceType? sl_trigger_type = null, decimal? sl_trigger_price = null, decimal? sl_price = null, CancellationToken ct = default);
        Task<WebCallResult<OkexSpotAlgoPlacedOrder>> Spot_AlgoPlaceOrder_Async(string symbol, OkexAlgoOrderType type, OkexMarket mode, OkexSpotOrderSide side, decimal size, decimal? trigger_price = null, decimal? trigger_algo_price = null, OkexAlgoPriceType? trigger_algo_type = null, decimal? trail_callback_rate = null, decimal? trail_trigger_price = null, decimal? iceberg_algo_variance = null, decimal? iceberg_avg_amount = null, decimal? iceberg_limit_price = null, decimal? twap_sweep_range = null, decimal? twap_sweep_ratio = null, int? twap_single_limit = null, decimal? twap_limit_price = null, int? twap_time_interval = null, OkexAlgoPriceType? tp_trigger_type = null, decimal? tp_trigger_price = null, decimal? tp_price = null, OkexAlgoPriceType? sl_trigger_type = null, decimal? sl_trigger_price = null, decimal? sl_price = null, CancellationToken ct = default);
        WebCallResult<Dictionary<string, IEnumerable<OkexSpotPlacedOrder>>> Spot_BatchCancelOrders(IEnumerable<OkexSpotCancelOrder> orders, CancellationToken ct = default);
        Task<WebCallResult<Dictionary<string, IEnumerable<OkexSpotPlacedOrder>>>> Spot_BatchCancelOrders_Async(IEnumerable<OkexSpotCancelOrder> orders, CancellationToken ct = default);
        WebCallResult<Dictionary<string, IEnumerable<OkexSpotPlacedOrder>>> Spot_BatchModifyOrders(IEnumerable<OkexSpotModifyOrder> orders, CancellationToken ct = default);
        Task<WebCallResult<Dictionary<string, IEnumerable<OkexSpotPlacedOrder>>>> Spot_BatchModifyOrders_Async(IEnumerable<OkexSpotModifyOrder> orders, CancellationToken ct = default);
        WebCallResult<Dictionary<string, IEnumerable<OkexSpotPlacedOrder>>> Spot_BatchPlaceOrders(IEnumerable<OkexSpotPlaceOrder> orders, CancellationToken ct = default);
        Task<WebCallResult<Dictionary<string, IEnumerable<OkexSpotPlacedOrder>>>> Spot_BatchPlaceOrders_Async(IEnumerable<OkexSpotPlaceOrder> orders, CancellationToken ct = default);
        WebCallResult<OkexSpotPlacedOrder> Spot_CancelOrder(string symbol, long? orderId = null, string? clientOrderId = null, CancellationToken ct = default);
        Task<WebCallResult<OkexSpotPlacedOrder>> Spot_CancelOrder_Async(string symbol, long? orderId = null, string? clientOrderId = null, CancellationToken ct = default);
        WebCallResult<IEnumerable<OkexSpotBalance>> Spot_GetAllBalances(CancellationToken ct = default);
        Task<WebCallResult<IEnumerable<OkexSpotBalance>>> Spot_GetAllBalances_Async(CancellationToken ct = default);
        WebCallResult<IEnumerable<OkexSpotOrderDetails>> Spot_GetAllOrders(string symbol, OkexSpotOrderState state, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default);
        Task<WebCallResult<IEnumerable<OkexSpotOrderDetails>>> Spot_GetAllOrders_Async(string symbol, OkexSpotOrderState state, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default);
        WebCallResult<IEnumerable<OkexSpotTicker>> Spot_GetAllTickers(CancellationToken ct = default);
        Task<WebCallResult<IEnumerable<OkexSpotTicker>>> Spot_GetAllTickers_Async(CancellationToken ct = default);
        WebCallResult<IEnumerable<OkexSpotCandle>> Spot_GetCandles(string symbol, OkexSpotPeriod period, DateTime? start = null, DateTime? end = null, CancellationToken ct = default);
        Task<WebCallResult<IEnumerable<OkexSpotCandle>>> Spot_GetCandles_Async(string symbol, OkexSpotPeriod period, DateTime? start = null, DateTime? end = null, CancellationToken ct = default);
        WebCallResult<IEnumerable<OkexSpotCandle>> Spot_GetHistoricalCandles(string symbol, OkexSpotPeriod period, DateTime? start = null, DateTime? end = null, int limit = 300, CancellationToken ct = default);
        Task<WebCallResult<IEnumerable<OkexSpotCandle>>> Spot_GetHistoricalCandles_Async(string symbol, OkexSpotPeriod period, DateTime? start = null, DateTime? end = null, int limit = 300, CancellationToken ct = default);
        WebCallResult<IEnumerable<OkexSpotOrderDetails>> Spot_GetOpenOrders(string symbol, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default);
        Task<WebCallResult<IEnumerable<OkexSpotOrderDetails>>> Spot_GetOpenOrders_Async(string symbol, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default);
        WebCallResult<OkexSpotOrderBook> Spot_GetOrderBook(string symbol, int? size = null, decimal? depth = null, CancellationToken ct = default);
        Task<WebCallResult<OkexSpotOrderBook>> Spot_GetOrderBook_Async(string symbol, int? size = null, decimal? depth = null, CancellationToken ct = default);
        WebCallResult<OkexSpotOrderDetails> Spot_GetOrderDetails(string symbol, long? orderId = null, string? clientOrderId = null, CancellationToken ct = default);
        Task<WebCallResult<OkexSpotOrderDetails>> Spot_GetOrderDetails_Async(string symbol, long? orderId = null, string? clientOrderId = null, CancellationToken ct = default);
        WebCallResult<OkexSpotBalance> Spot_GetSymbolBalance(string currency, CancellationToken ct = default);
        Task<WebCallResult<OkexSpotBalance>> Spot_GetSymbolBalance_Async(string currency, CancellationToken ct = default);
        WebCallResult<IEnumerable<OkexSpotBill>> Spot_GetSymbolBills(string currency, OkexSpotBillType? type = null, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default);
        Task<WebCallResult<IEnumerable<OkexSpotBill>>> Spot_GetSymbolBills_Async(string currency, OkexSpotBillType? type = null, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default);
        WebCallResult<OkexSpotTicker> Spot_GetSymbolTicker(string symbol, CancellationToken ct = default);
        Task<WebCallResult<OkexSpotTicker>> Spot_GetSymbolTicker_Async(string symbol, CancellationToken ct = default);
        WebCallResult<IEnumerable<OkexSpotTrade>> Spot_GetTrades(string symbol, int limit = 60, CancellationToken ct = default);
        Task<WebCallResult<IEnumerable<OkexSpotTrade>>> Spot_GetTrades_Async(string symbol, int limit = 60, CancellationToken ct = default);
        WebCallResult<IEnumerable<OkexSpotPair>> Spot_GetTradingPairs(CancellationToken ct = default);
        Task<WebCallResult<IEnumerable<OkexSpotPair>>> Spot_GetTradingPairs_Async(CancellationToken ct = default);
        WebCallResult<IEnumerable<OkexSpotTransaction>> Spot_GetTransactionDetails(string symbol, long? orderId = null, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default);
        Task<WebCallResult<IEnumerable<OkexSpotTransaction>>> Spot_GetTransactionDetails_Async(string symbol, long? orderId = null, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default);
        WebCallResult<OkexSpotPlacedOrder> Spot_ModifyOrder(string symbol, long? orderId = null, string? clientOrderId = null, string? requestId = null, decimal? newSize = null, decimal? newPrice = null, bool? cancelOnFail = null, CancellationToken ct = default);
        Task<WebCallResult<OkexSpotPlacedOrder>> Spot_ModifyOrder_Async(string symbol, long? orderId = null, string? clientOrderId = null, string? requestId = null, decimal? newSize = null, decimal? newPrice = null, bool? cancelOnFail = null, CancellationToken ct = default);
        WebCallResult<OkexSpotPlacedOrder> Spot_PlaceOrder(OkexSpotPlaceOrder order, CancellationToken ct = default);
        WebCallResult<OkexSpotPlacedOrder> Spot_PlaceOrder(string symbol, OkexSpotOrderSide side, OkexSpotOrderType type, OkexSpotTimeInForce timeInForce = OkexSpotTimeInForce.NormalOrder, decimal? price = null, decimal? size = null, decimal? notional = null, string? clientOrderId = null, CancellationToken ct = default);
        Task<WebCallResult<OkexSpotPlacedOrder>> Spot_PlaceOrder_Async(OkexSpotPlaceOrder order, CancellationToken ct = default);
        Task<WebCallResult<OkexSpotPlacedOrder>> Spot_PlaceOrder_Async(string symbol, OkexSpotOrderSide side, OkexSpotOrderType type, OkexSpotTimeInForce timeInForce = OkexSpotTimeInForce.NormalOrder, decimal? price = null, decimal? size = null, decimal? notional = null, string? clientOrderId = null, CancellationToken ct = default);
        WebCallResult<OkexSpotTradingFee> Spot_TradeFeeRates(CancellationToken ct = default);
        Task<WebCallResult<OkexSpotTradingFee>> Spot_TradeFeeRates_Async(CancellationToken ct = default);
    }
}