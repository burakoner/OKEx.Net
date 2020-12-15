## Release Notes
* Version 1.4.0 - 15 Dec 2020
    * Added Futures Rest Api Support
    * Added System endpoints below
        * SystemStatus(OkexSystemMaintenanceStatus? status = null, CancellationToken ct = default)
        * SystemStatus_Async(OkexSystemMaintenanceStatus? status = null, CancellationToken ct = default)

    * Added Spot Trading Methods below
        * Spot_AlgoPlaceOrder(...)
        * Spot_AlgoPlaceOrder_Async(...)
        * Spot_AlgoCancelOrder(string symbol, OkexAlgoOrderType type, IEnumerable<long> algo_ids, CancellationToken ct = default)
        * Spot_AlgoCancelOrder_Async(string symbol, OkexAlgoOrderType type, IEnumerable<long> algo_ids, CancellationToken ct = default)
        * Spot_AlgoGetOrders(string symbol, OkexAlgoOrderType type, OkexAlgoStatus? status = null, IEnumerable<long> algo_ids = null, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default)
        * Spot_AlgoGetOrders_Async(string symbol, OkexAlgoOrderType type, OkexAlgoStatus? status = null, IEnumerable<long> algo_ids = null, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default)

    * Added Futures Trading Methods below
        * Futures_GetPositions(CancellationToken ct = default)
        * Futures_GetPositions_Async(CancellationToken ct = default)
        * Futures_GetPositions(string contract, CancellationToken ct = default)
        * Futures_GetPositions_Async(string contract, CancellationToken ct = default)
        * Futures_GetBalances(CancellationToken ct = default)
        * Futures_GetBalances_Async(CancellationToken ct = default)
        * Futures_GetBalances(string underlying, CancellationToken ct = default)
        * Futures_GetBalances_Async(string underlying, CancellationToken ct = default)
        * Futures_GetLeverage(string underlying, CancellationToken ct = default)
        * Futures_GetLeverage_Async(string underlying, CancellationToken ct = default)
        * Futures_SetLeverage(OkexFuturesMarginMode mode, string underlying, int leverage, string? instrument_id = null, OkexFuturesDirection? direction = null, CancellationToken ct = default)
        * Futures_SetLeverage_Async(OkexFuturesMarginMode mode, string underlying, int leverage, string? instrument_id = null, OkexFuturesDirection? direction = null, CancellationToken ct = default)
        * Futures_GetSymbolBills(string underlying, OkexFuturesBillType? type = null, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default)
        * Futures_GetSymbolBills_Async(string underlying, OkexFuturesBillType? type = null, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default)
        * Futures_PlaceOrder(string symbol, OkexFuturesOrderType type, decimal size, OkexFuturesTimeInForce timeInForce = OkexFuturesTimeInForce.NormalOrder, decimal? price = null, bool match_price = false, string? clientOrderId = null, CancellationToken ct = default)
        * Futures_PlaceOrder_Async(string symbol, OkexFuturesOrderType type, decimal size, OkexFuturesTimeInForce timeInForce = OkexFuturesTimeInForce.NormalOrder, decimal? price = null, bool match_price = false, string? clientOrderId = null, CancellationToken ct = default)
        * Futures_BatchPlaceOrders(string symbol, IEnumerable<OkexFuturesPlaceOrder> orders, CancellationToken ct = default)
        * Futures_BatchPlaceOrders_Async(string symbol, IEnumerable<OkexFuturesPlaceOrder> orders, CancellationToken ct = default)
        * Futures_ModifyOrder(string symbol, long? orderId = null, string? clientOrderId = null, string? requestId = null, decimal? newSize = null, decimal? newPrice = null, bool? cancelOnFail = null, CancellationToken ct = default)
        * Futures_ModifyOrder_Async(string symbol, long? orderId = null, string? clientOrderId = null, string? requestId = null, decimal? newSize = null, decimal? newPrice = null, bool? cancelOnFail = null, CancellationToken ct = default)
        * Futures_BatchModifyOrders(string symbol, IEnumerable<OkexFuturesModifyOrder> orders, CancellationToken ct = default)
        * Futures_BatchModifyOrders_Async(string symbol, IEnumerable<OkexFuturesModifyOrder> orders, CancellationToken ct = default)
        * Futures_CancelOrder(string symbol, long? orderId = null, string? clientOrderId = null, CancellationToken ct = default)
        * Futures_CancelOrder_Async(string symbol, long? orderId = null, string? clientOrderId = null, CancellationToken ct = default)
        * Futures_BatchCancelOrders(string symbol, IEnumerable<long> orderIds, IEnumerable<string> clientOrderIds, CancellationToken ct = default)
        * Futures_BatchCancelOrders_Async(string symbol, IEnumerable<long> orderIds, IEnumerable<string> clientOrderIds, CancellationToken ct = default)
        * Futures_GetAllOrders(string symbol, OkexFuturesOrderState state, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default)
        * Futures_GetAllOrders_Async(string symbol, OkexFuturesOrderState state, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default)
        * Futures_GetOrderDetails(string symbol, long? orderId = null, string? clientOrderId = null, CancellationToken ct = default)
        * Futures_GetOrderDetails_Async(string symbol, long? orderId = null, string? clientOrderId = null, CancellationToken ct = default)
        * Futures_GetTransactionDetails(string symbol, long? orderId = null, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default)
        * Futures_GetTransactionDetails_Async(string symbol, long? orderId = null, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default)
        * Futures_SetAccountMode(string symbol, OkexFuturesMarginMode margin_mode, CancellationToken ct = default)
        * Futures_SetAccountMode_Async(string symbol, OkexFuturesMarginMode margin_mode, CancellationToken ct = default)
        * Futures_GetTradeFeeRates(string? symbol = null, int? category = null, CancellationToken ct = default)
        * Futures_GetTradeFeeRates_Async(string? symbol = null, int? category = null, CancellationToken ct = default)
        * Futures_MarketCloseAll(string symbol, OkexFuturesDirection direction, CancellationToken ct = default)
        * Futures_MarketCloseAll_Async(string symbol, OkexFuturesDirection direction, CancellationToken ct = default)
        * Futures_CancelAll(string symbol, OkexFuturesDirection direction, CancellationToken ct = default)
        * Futures_CancelAll_Async(string symbol, OkexFuturesDirection direction, CancellationToken ct = default)
        * Futures_GetHoldAmount(string symbol, CancellationToken ct = default)
        * Futures_GetHoldAmount_Async(string symbol, CancellationToken ct = default)
        * Futures_AlgoPlaceOrder(...)
        * Futures_AlgoPlaceOrder_Async(...)
        * Futures_AlgoCancelOrder(string symbol, OkexAlgoOrderType type, IEnumerable<long> algo_ids, CancellationToken ct = default)
        * Futures_AlgoCancelOrder_Async(string symbol, OkexAlgoOrderType type, IEnumerable<long> algo_ids, CancellationToken ct = default)
        * Futures_AlgoGetOrders(string symbol, OkexAlgoOrderType type, OkexAlgoStatus? status = null, IEnumerable<long> algo_ids = null, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default)
        * Futures_AlgoGetOrders_Async(string symbol, OkexAlgoOrderType type, OkexAlgoStatus? status = null, IEnumerable<long> algo_ids = null, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default)
        * Futures_IncreaseDecreaseMargin(string symbol, OkexFuturesDirection direction, OkexFuturesMarginAction action, decimal amount, CancellationToken ct = default)
        * Futures_IncreaseDecreaseMargin_Async(string symbol, OkexFuturesDirection direction, OkexFuturesMarginAction action, decimal amount, CancellationToken ct = default)
        * Futures_AutoMargin(string symbol, OkexFuturesAutoMargin status, CancellationToken ct = default)
        * Futures_AutoMargin_Async(string symbol, OkexFuturesAutoMargin status, CancellationToken ct = default)

    * Added Index Api Methods below
        * Index_GetConstituents(string symbol,CancellationToken ct = default)
        * Index_GetConstituents_Async(string symbol, CancellationToken ct = default)

    * Added Oracle Api Methods below
        * Oracle_GetData(CancellationToken ct = default)
        * Oracle_GetData_Async( CancellationToken ct = default)

    * Added lots of enums, models and converters

* Version 1.3.1 - 13 Dec 2020
    * Added TypedDataConverter
    * Completed OkexMarginBalance Model
    * Completed OkexMarginAccountSettings Model

* Version 1.3.0 - 13 Dec 2020
    * Core Features
        * Overrided to CryptoExchange.Net.RestClient.WriteParamBody
        * Changed signature algorithm
    * Added Funding Methods below
        * Funding_GetUserID(CancellationToken ct = default)
        * Funding_GetUserID_Async(CancellationToken ct = default)
        * Funding_PiggyBank(string currency, decimal amount, OkexFundingPiggyBankActionSide side, CancellationToken ct = default)
        * Funding_PiggyBank_Async(string currency, decimal amount, OkexFundingPiggyBankActionSide side, CancellationToken ct = default)
    * Added Spot Trading Methods below
        * Spot_PlaceOrder(OkexSpotPlaceOrder order, CancellationToken ct = default)
        * Spot_PlaceOrder_Async(OkexSpotPlaceOrder order, CancellationToken ct = default)
        * Spot_BatchPlaceOrders(IEnumerable<OkexSpotPlaceOrder> orders, CancellationToken ct = default)
        * Spot_BatchPlaceOrders_Async(IEnumerable<OkexSpotPlaceOrder> orders, CancellationToken ct = default)
        * Spot_BatchCancelOrders(IEnumerable<OkexSpotCancelOrder> orders, CancellationToken ct = default)
        * Spot_BatchCancelOrders_Async(IEnumerable<OkexSpotCancelOrder> orders, CancellationToken ct = default)
        * Spot_ModifyOrder(string symbol, long? orderId = null, string? clientOrderId = null, string? requestId = null, decimal? newSize = null, decimal? newPrice = null, bool? cancelOnFail = null, CancellationToken ct = default)
        * Spot_ModifyOrder_Async(string symbol, long? orderId = null, string? clientOrderId = null, string? requestId = null, decimal? newSize = null, decimal? newPrice = null, bool? cancelOnFail = null, CancellationToken ct = default)
        * Spot_BatchModifyOrders(IEnumerable<OkexSpotModifyOrder> orders, CancellationToken ct = default)
        * Spot_BatchModifyOrders_Async(IEnumerable<OkexSpotModifyOrder> orders, CancellationToken ct = default)
        * Spot_GetTransactionDetails(string symbol, long? orderId = null, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default)
        * Spot_GetTransactionDetails_Async(string symbol, long? orderId = null, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default)
        * Spot_GetHistoricalCandles(string symbol, OkexSpotPeriod period, DateTime? start = null, DateTime? end = null, int limit = 300, CancellationToken ct = default) 
        * Spot_GetHistoricalCandles_Async(string symbol, OkexSpotPeriod period, DateTime? start = null, DateTime? end = null, int limit = 300, CancellationToken ct = default)
    * Added Algo Trading Methods below
        * Algo_PlaceOrder(...)
        * Algo_PlaceOrder_Async(...)
        * Algo_CancelOrder(string symbol, OkexAlgoOrderType type, IEnumerable<long> algo_ids, CancellationToken ct = default)
        * Algo_CancelOrder_Async(string symbol, OkexAlgoOrderType type, IEnumerable<long> algo_ids, CancellationToken ct = default)
        * Algo_GetOrders(string symbol, OkexAlgoOrderType type, OkexAlgoStatus? status = null, IEnumerable<long> algo_ids = null, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default)
        * Algo_GetOrders_Async(string symbol, OkexAlgoOrderType type, OkexAlgoStatus? status = null, IEnumerable<long> algo_ids = null, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default)
    * Added Margin Trading Methods below
        * Margin_GetAllBalances(CancellationToken ct = default)
        * Margin_GetAllBalances_Async(CancellationToken ct = default)
        * Margin_GetSymbolBalance(string symbol, CancellationToken ct = default)
        * Margin_GetSymbolBalance_Async(string symbol, CancellationToken ct = default)
        * Margin_GetSymbolBills(string symbol, OkexMarginBillType? type = null, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default)
        * Margin_GetSymbolBills_Async(string symbol, OkexMarginBillType? type = null, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default)
        * Margin_GetAccountSettings(CancellationToken ct = default)
        * Margin_GetAccountSettings_Async(CancellationToken ct = default)
        * Margin_GetAccountSettings(string symbol, CancellationToken ct = default)
        * Margin_GetAccountSettings_Async(string symbol, CancellationToken ct = default)
        * Margin_GetLoanHistory(OkexMarginLoanStatus status, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default)
        * Margin_GetLoanHistory_Async(OkexMarginLoanStatus status, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default)
        * Margin_GetLoanHistory(string symbol, OkexMarginLoanState state, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default)
        * Margin_GetLoanHistory_Async(string symbol, OkexMarginLoanState state, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default)
        * Margin_Loan(string symbol, string currency, decimal amount, string? clientOrderId = null, CancellationToken ct = default)
        * Margin_Loan_Async(string symbol, string currency, decimal amount, string? clientOrderId = null, CancellationToken ct = default)
        * Margin_Repayment(string symbol, string currency, decimal amount, long? borrow_id = null, string? clientOrderId = null, CancellationToken ct = default)
        * Margin_Repayment_Async(string symbol, string currency, decimal amount, long? borrow_id = null, string? clientOrderId = null, CancellationToken ct = default)
        * Margin_PlaceOrder(OkexSpotPlaceOrder order, CancellationToken ct = default)
        * Margin_PlaceOrder_Async(OkexSpotPlaceOrder order, CancellationToken ct = default)
        * Margin_PlaceOrder(string symbol, OkexSpotOrderSide side, OkexSpotOrderType type, OkexSpotTimeInForce timeInForce = OkexSpotTimeInForce.NormalOrder, decimal? price = null, decimal? size = null, decimal? notional = null, string? clientOrderId = null, CancellationToken ct = default)
        * Margin_PlaceOrder_Async(string symbol, OkexSpotOrderSide side, OkexSpotOrderType type, OkexSpotTimeInForce timeInForce = OkexSpotTimeInForce.NormalOrder, decimal? price = null, decimal? size = null, decimal? notional = null, string? clientOrderId = null, CancellationToken ct = default)
        * Margin_BatchPlaceOrders(IEnumerable<OkexSpotPlaceOrder> orders, CancellationToken ct = default)
        * Margin_BatchPlaceOrders_Async(IEnumerable<OkexSpotPlaceOrder> orders, CancellationToken ct = default)
        * Margin_CancelOrder(string symbol, long? orderId = null, string? clientOrderId = null, CancellationToken ct = default)
        * Margin_CancelOrder_Async(string symbol, long? orderId = null, string? clientOrderId = null, CancellationToken ct = default)
        * Margin_BatchCancelOrders(IEnumerable<OkexSpotCancelOrder> orders, CancellationToken ct = default)
        * Margin_BatchCancelOrders_Async(IEnumerable<OkexSpotCancelOrder> orders, CancellationToken ct = default)
        * Margin_GetAllOrders(string symbol, OkexSpotOrderState state, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default)
        * Margin_GetAllOrders_Async(string symbol, OkexSpotOrderState state, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default)
        * Margin_GetLeverage(string symbol, CancellationToken ct = default)
        * Margin_GetLeverage_Async(string symbol, CancellationToken ct = default)
        * Margin_SetLeverage(string symbol, int leverage, CancellationToken ct = default)
        * Margin_SetLeverage_Async(string symbol, int leverage, CancellationToken ct = default)
        * Margin_GetOrderDetails(string symbol, long? orderId = null, string? clientOrderId = null, CancellationToken ct = default)
        * Margin_GetOrderDetails_Async(string symbol, long? orderId = null, string? clientOrderId = null, CancellationToken ct = default)
        * Margin_GetOpenOrders(string symbol, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default)
        * Margin_GetOpenOrders_Async(string symbol, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default)
        * Margin_GetTransactionDetails(string symbol, long? orderId = null, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default)
        * Margin_GetTransactionDetails_Async(string symbol, long? orderId = null, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default)
        * Margin_GetMarkPrice(string symbol, CancellationToken ct = default)
        * Margin_GetMarkPrice_Async(string symbol, CancellationToken ct = default)
    * Added many enums, models and converters
    * Changed Namespaces below
        * Okex.Net -> Okex.Net.Enums (for enums only)
    * Changed Method names below
        * Okex.Net.Converters: SpotMarginOrderSourceTypeConverter -> MarketConverter
    * Changed Enum names below
        * OkexSpotMarginOrderSourceType -> OkexMarket
    * Changed Class names below
        * Okex.Net.RestObjects: OkexSpotAccount -> OkexSpotBalance
    * Fixed minor bugs

* Version 1.2.4 - 12 Dec 2020
    * CryptoExchange version updated to 3.3.0

* Version 1.2.3 - 08 Nov 2020
    * Fixed duplicate slashes on BaseAddress caused by CryptoExchange

* Version 1.2.1 - 08 Nov 2020
    * CryptoExchange version updated to 3.1.0

* Version 1.2.0 - 21 Sep 2020
    * CryptoExchange version updated to 3.0.14

* Version 1.0.5 - 02 Feb 2020
    * Added Support to Subscribe Tickers for Multiple Symbols

* Version 1.0.4 - 31 Jan 2020
    * Added Amount field to Funding-DepositHistory

* Version 1.0.3 - 29 Jan 2020
    * Added Ping-Pong Mechanism

* Version 1.0.2 - 28 Jan 2020
    * Upgraded to CryptoExchange.Net v3.0.3
    * Added Funding API Deposit Endpoints
    * Added Funding API Withdrawal Endpoints
    * Added Websockets Login Method
    * Added Websockets User Stream Subscriptions
    * Adapted some Spot Api endpoint models with websocket models
    * Added Examples Project

* Version 1.0.0 - 26 Jan 2020
    * First Release