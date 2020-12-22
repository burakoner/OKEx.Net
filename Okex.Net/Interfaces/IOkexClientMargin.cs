using CryptoExchange.Net.Objects;
using Okex.Net.Enums;
using Okex.Net.RestObjects;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Okex.Net.Interfaces
{
    public interface IOkexClientMargin
    {   
        WebCallResult<OkexSpotAlgoCancelledOrder> Margin_AlgoCancelOrder(string symbol, OkexAlgoOrderType type, IEnumerable<long> algo_ids, CancellationToken ct = default);
        Task<WebCallResult<OkexSpotAlgoCancelledOrder>> Margin_AlgoCancelOrder_Async(string symbol, OkexAlgoOrderType type, IEnumerable<long> algo_ids, CancellationToken ct = default);
        WebCallResult<Dictionary<string, IEnumerable<OkexSpotAlgoOrder>>> Margin_AlgoGetOrders(string symbol, OkexAlgoOrderType type, OkexAlgoStatus? status = null, IEnumerable<long> algo_ids = null, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default);
        Task<WebCallResult<Dictionary<string, IEnumerable<OkexSpotAlgoOrder>>>> Margin_AlgoGetOrders_Async(string symbol, OkexAlgoOrderType type, OkexAlgoStatus? status = null, IEnumerable<long> algo_ids = null, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default);
        WebCallResult<OkexSpotAlgoPlacedOrder> Margin_AlgoPlaceOrder(string symbol, OkexAlgoOrderType type, OkexMarket mode, OkexSpotOrderSide side, decimal size, decimal? trigger_price = null, decimal? trigger_algo_price = null, OkexAlgoPriceType? trigger_algo_type = null, decimal? trail_callback_rate = null, decimal? trail_trigger_price = null, decimal? iceberg_algo_variance = null, decimal? iceberg_avg_amount = null, decimal? iceberg_limit_price = null, decimal? twap_sweep_range = null, decimal? twap_sweep_ratio = null, int? twap_single_limit = null, decimal? twap_limit_price = null, int? twap_time_interval = null, OkexAlgoPriceType? tp_trigger_type = null, decimal? tp_trigger_price = null, decimal? tp_price = null, OkexAlgoPriceType? sl_trigger_type = null, decimal? sl_trigger_price = null, decimal? sl_price = null, CancellationToken ct = default);
        Task<WebCallResult<OkexSpotAlgoPlacedOrder>> Margin_AlgoPlaceOrder_Async(string symbol, OkexAlgoOrderType type, OkexMarket mode, OkexSpotOrderSide side, decimal size, decimal? trigger_price = null, decimal? trigger_algo_price = null, OkexAlgoPriceType? trigger_algo_type = null, decimal? trail_callback_rate = null, decimal? trail_trigger_price = null, decimal? iceberg_algo_variance = null, decimal? iceberg_avg_amount = null, decimal? iceberg_limit_price = null, decimal? twap_sweep_range = null, decimal? twap_sweep_ratio = null, int? twap_single_limit = null, decimal? twap_limit_price = null, int? twap_time_interval = null, OkexAlgoPriceType? tp_trigger_type = null, decimal? tp_trigger_price = null, decimal? tp_price = null, OkexAlgoPriceType? sl_trigger_type = null, decimal? sl_trigger_price = null, decimal? sl_price = null, CancellationToken ct = default);
        WebCallResult<Dictionary<string, IEnumerable<OkexSpotPlacedOrder>>> Margin_BatchCancelOrders(IEnumerable<OkexSpotCancelOrder> orders, CancellationToken ct = default);
        Task<WebCallResult<Dictionary<string, IEnumerable<OkexSpotPlacedOrder>>>> Margin_BatchCancelOrders_Async(IEnumerable<OkexSpotCancelOrder> orders, CancellationToken ct = default);
        WebCallResult<Dictionary<string, IEnumerable<OkexSpotPlacedOrder>>> Margin_BatchPlaceOrders(IEnumerable<OkexSpotPlaceOrder> orders, CancellationToken ct = default);
        Task<WebCallResult<Dictionary<string, IEnumerable<OkexSpotPlacedOrder>>>> Margin_BatchPlaceOrders_Async(IEnumerable<OkexSpotPlaceOrder> orders, CancellationToken ct = default);
        WebCallResult<OkexSpotPlacedOrder> Margin_CancelOrder(string symbol, long? orderId = null, string? clientOrderId = null, CancellationToken ct = default);
        Task<WebCallResult<OkexSpotPlacedOrder>> Margin_CancelOrder_Async(string symbol, long? orderId = null, string? clientOrderId = null, CancellationToken ct = default);
        WebCallResult<IEnumerable<OkexMarginAccountSettings>> Margin_GetAccountSettings(CancellationToken ct = default);
        WebCallResult<IEnumerable<OkexMarginAccountSettings>> Margin_GetAccountSettings(string symbol, CancellationToken ct = default);
        Task<WebCallResult<IEnumerable<OkexMarginAccountSettings>>> Margin_GetAccountSettings_Async(CancellationToken ct = default);
        Task<WebCallResult<IEnumerable<OkexMarginAccountSettings>>> Margin_GetAccountSettings_Async(string symbol, CancellationToken ct = default);
        WebCallResult<IEnumerable<OkexMarginBalance>> Margin_GetAllBalances(CancellationToken ct = default);
        Task<WebCallResult<IEnumerable<OkexMarginBalance>>> Margin_GetAllBalances_Async(CancellationToken ct = default);
        WebCallResult<IEnumerable<OkexSpotOrderDetails>> Margin_GetAllOrders(string symbol, OkexSpotOrderState state, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default);
        Task<WebCallResult<IEnumerable<OkexSpotOrderDetails>>> Margin_GetAllOrders_Async(string symbol, OkexSpotOrderState state, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default);
        WebCallResult<OkexMarginLeverageResponse> Margin_GetLeverage(string symbol, CancellationToken ct = default);
        Task<WebCallResult<OkexMarginLeverageResponse>> Margin_GetLeverage_Async(string symbol, CancellationToken ct = default);
        WebCallResult<IEnumerable<OkexMarginLoanHistory>> Margin_GetLoanHistory(OkexMarginLoanStatus status, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default);
        WebCallResult<IEnumerable<OkexMarginLoanHistory>> Margin_GetLoanHistory(string symbol, OkexMarginLoanState state, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default);
        Task<WebCallResult<IEnumerable<OkexMarginLoanHistory>>> Margin_GetLoanHistory_Async(OkexMarginLoanStatus status, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default);
        Task<WebCallResult<IEnumerable<OkexMarginLoanHistory>>> Margin_GetLoanHistory_Async(string symbol, OkexMarginLoanState state, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default);
        WebCallResult<OkexMarginMarkPrice> Margin_GetMarkPrice(string symbol, CancellationToken ct = default);
        Task<WebCallResult<OkexMarginMarkPrice>> Margin_GetMarkPrice_Async(string symbol, CancellationToken ct = default);
        WebCallResult<IEnumerable<OkexMarginOrderDetails>> Margin_GetOpenOrders(string symbol, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default);
        Task<WebCallResult<IEnumerable<OkexMarginOrderDetails>>> Margin_GetOpenOrders_Async(string symbol, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default);
        WebCallResult<OkexMarginOrderDetails> Margin_GetOrderDetails(string symbol, long? orderId = null, string? clientOrderId = null, CancellationToken ct = default);
        Task<WebCallResult<OkexMarginOrderDetails>> Margin_GetOrderDetails_Async(string symbol, long? orderId = null, string? clientOrderId = null, CancellationToken ct = default);
        WebCallResult<OkexMarginBalance> Margin_GetSymbolBalance(string symbol, CancellationToken ct = default);
        Task<WebCallResult<OkexMarginBalance>> Margin_GetSymbolBalance_Async(string symbol, CancellationToken ct = default);
        WebCallResult<IEnumerable<OkexMarginBill>> Margin_GetSymbolBills(string symbol, OkexMarginBillType? type = null, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default);
        Task<WebCallResult<IEnumerable<OkexMarginBill>>> Margin_GetSymbolBills_Async(string symbol, OkexMarginBillType? type = null, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default);
        WebCallResult<IEnumerable<OkexMarginTransaction>> Margin_GetTransactionDetails(string symbol, long? orderId = null, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default);
        Task<WebCallResult<IEnumerable<OkexMarginTransaction>>> Margin_GetTransactionDetails_Async(string symbol, long? orderId = null, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default);
        WebCallResult<OkexMarginLoanResponse> Margin_Loan(string symbol, string currency, decimal amount, string? clientOrderId = null, CancellationToken ct = default);
        Task<WebCallResult<OkexMarginLoanResponse>> Margin_Loan_Async(string symbol, string currency, decimal amount, string? clientOrderId = null, CancellationToken ct = default);
        WebCallResult<OkexSpotPlacedOrder> Margin_PlaceOrder(OkexSpotPlaceOrder order, CancellationToken ct = default);
        WebCallResult<OkexSpotPlacedOrder> Margin_PlaceOrder(string symbol, OkexSpotOrderSide side, OkexSpotOrderType type, OkexSpotTimeInForce timeInForce = OkexSpotTimeInForce.NormalOrder, decimal? price = null, decimal? size = null, decimal? notional = null, string? clientOrderId = null, CancellationToken ct = default);
        Task<WebCallResult<OkexSpotPlacedOrder>> Margin_PlaceOrder_Async(OkexSpotPlaceOrder order, CancellationToken ct = default);
        Task<WebCallResult<OkexSpotPlacedOrder>> Margin_PlaceOrder_Async(string symbol, OkexSpotOrderSide side, OkexSpotOrderType type, OkexSpotTimeInForce timeInForce = OkexSpotTimeInForce.NormalOrder, decimal? price = null, decimal? size = null, decimal? notional = null, string? clientOrderId = null, CancellationToken ct = default);
        WebCallResult<OkexMarginRepaymentResponse> Margin_Repayment(string symbol, string currency, decimal amount, long? borrow_id = null, string? clientOrderId = null, CancellationToken ct = default);
        Task<WebCallResult<OkexMarginRepaymentResponse>> Margin_Repayment_Async(string symbol, string currency, decimal amount, long? borrow_id = null, string? clientOrderId = null, CancellationToken ct = default);
        WebCallResult<OkexMarginLeverageResponse> Margin_SetLeverage(string symbol, int leverage, CancellationToken ct = default);
        Task<WebCallResult<OkexMarginLeverageResponse>> Margin_SetLeverage_Async(string symbol, int leverage, CancellationToken ct = default);
    }
}