## Release Notes
* Version 5.2.6 - 13 Oct 2022
	* Fixed some issues on Github
	* Fixed minor bugs

* Version 5.2.5 - 21 Aug 2022
    * Added CancelWithdrawal CancelWithdrawalAsync methods
    * Corrected some deprecated endpoints
    * Merged some community pull requests
    * Fixed minor bugs

* Version 5.2.4 - 21 Aug 2022
    * Synced with CryptoExchange.Net v5.2.4
    * Removed "_" (underscore) from async method names (Dummy_Async() => DummyAsync())
    * Okex.Net.CoreObjects namespace changed as Okex.Net.Objects.Core
    * Okex.Net.RestObjects.* namespace changed as Okex.Net.Objects.*
    * Added: Okex.Net.Objects.Core.OkexApiAddresses Class
    * Added: Okex.Net.Objects.Core.OkexApiCredentials Class
    * Added/Changed some Core Objects according to new version of CryptoExchange.Net
    * Added: Public => GetVIPInterestRates GetVIPInterestRatesAsync methods
    * Added: Public => GetInsuranceFund GetInsuranceFundAsync methods
    * Added: Public => UnitConvert UnitConvertAsync methods
    * Added: Market => GetTradesHistory GetTradesHistoryAsync methods
    * Added: Market => GetBlockTickers GetBlockTickersAsync methods
    * Added: Market => GetBlockTicker GetBlockTickerAsync methods
    * Added: Market => GetBlockTrades GetBlockTradesAsync methods
    * Added: SubAccount => ResetSubAccountApiKey ResetSubAccountApiKeyAsync methods
    * Added: SubAccount => GetSubAccountTradingBalances GetSubAccountTradingBalancesAsync methods
    * Added: SubAccount => GetSubAccountFundingBalances GetSubAccountFundingBalancesAsync methods
    * Added: Funding => GetSavingBalances GetSavingBalancesAsync methods
    * Removed: SubAccount => CreateSubAccountApiKey CreateSubAccountApiKey_Async methods
    * Removed: SubAccount => GetSubAccountApiKey GetSubAccountApiKey_Async methods
    * Removed: SubAccount => ModifySubAccountApiKey ModifySubAccountApiKey_Async methods
    * Removed: SubAccount => DeleteSubAccountApiKey DeleteSubAccountApiKey_Async methods
    * Removed: SubAccount => GetSubAccountBalance GetSubAccountBalance_Async methods
    * Removed: Funding => PiggyBankAction PiggyBankAction_Async methods
    * Removed: Funding => PiggyBankBalance PiggyBankBalance_Async methods
    * Renamed: Okex.Net.RestObjects.Account.OkexPositionMode => Okex.Net.Objects.Account.OkexAccountPositionMode
    * Renamed: Okex.Net.RestObjects.Account.OkexGreeksType => Okex.Net.Objects.Account.OkexAccountGreeksType
    * Renamed: Okex.Net.RestObjects.Account.OkexConfiguration => Okex.Net.Objects.Account.OkexAccountConfiguration
    * Added/Changed/Fixed many models (added some extra models for new methods, added missing fields, removed some fields, fixed minor bugs for existing models)
    * Notes: CryptoExchange.Net v5 library is very different from version 4. I tried to keep main structure in OKEx.Net, but I had to make some changes. I know CryptoExchange.Net v5 is has different usage algorithm, but there are many developers using OKEx.Net. So I decided not to change main structure considering those developers. I can build a different wrapper for CryptoExchange.Net v5 later. I'm not sure...

* Version 5.2.0 - 08 Jan 2022
    * Migrated to okx.com

* Version 5.1.3 - 7 Dec 2021
    * Fixed minor bugs

* Version 5.1.2 - 26 Nov 2021
    * Fixed Websocket disconnect error

* Version 5.1.0 - 25 Nov 2021
    * Added Demo Trading Service

* Version 5.0.4 - 22 Oct 2021
    * Fixed minor bugs

* Version 5.0.3 - 16 Oct 2021
    * Fixed minor bugs

* Version 5.0.2 - 09 Oct 2021
    * Added WS Api V5 Support
    * Added method summaries
    * Updated ReadMe
    * Updated Examples

* Version 5.0.1 - 07 Oct 2021
    * Switched to OKEx V5 API

* Version 2.5.0 - 18 Sep 2021
    * Synced with CryptoExchange.Net v4.1.0

* Version 2.1.0 - 31 Mar 2021
    * Updated dependencies

* Version 2.0.2 - 23 Feb 2021
    * Fixed WebSocket Client Ping query response bug

* Version 2.0.1 - 01 Feb 2021
    * Updated CryptoExchange.Net to 3.6.0

* Version 2.0.0 - 17 Jan 2021
    * All methods are virtual now. You can customize methods by overriding.
    * Fixed several minor bugs

* Version 1.6.0 - 16 Jan 2021
    * Fixed several minor bugs

* Version 1.5.8 - 12 Jan 2021
    * Updated CryptoExchange.Net to 3.5.0

* Version 1.5.7 - 28 Dec 2020
    * Fixed minor bugs

* Version 1.5.6 - 22 Dec 2020
    * Added Index Api WebSocket Support
        * Index_SubscribeToCandlesticks(string symbol, OkexSpotPeriod period, Action<OkexIndexCandle> onData);
        * Index_SubscribeToCandlesticks_Async(string symbol, OkexSpotPeriod period, Action<OkexIndexCandle> onData);
        * Index_SubscribeToTicker(IEnumerable<string> symbol, Action<OkexOptionsTicker> onData);
        * Index_SubscribeToTicker(string symbol, Action<OkexIndexTicker> onData);
        * Index_SubscribeToTicker_Async(IEnumerable<string> symbol, Action<OkexOptionsTicker> onData);
        * Index_SubscribeToTicker_Async(string symbol, Action<OkexIndexTicker> onData);
        
* Version 1.5.5 - 22 Dec 2020
    * Added Options WebSocket Api Support
        * Options_SubscribeToBalance(string underlying, Action<OkexOptionsBalance> onData);
        * Options_SubscribeToBalance_Async(string underlying, Action<OkexOptionsBalance> onData);
        * Options_SubscribeToCandlesticks(string instrument, OkexSpotPeriod period, Action<OkexOptionsCandle> onData);
        * Options_SubscribeToCandlesticks_Async(string instrument, OkexSpotPeriod period, Action<OkexOptionsCandle> onData);
        * Options_SubscribeToContracts(string underlying, Action<OkexOptionsInstrument> onData);
        * Options_SubscribeToContracts_Async(string underlying, Action<OkexOptionsInstrument> onData);
        * Options_SubscribeToMarketData(IEnumerable<string> underlyings, Action<OkexOptionsMarketData> onData);
        * Options_SubscribeToMarketData(string underlying, Action<OkexOptionsMarketData> onData);
        * Options_SubscribeToMarketData_Async(IEnumerable<string> underlyings, Action<OkexOptionsMarketData> onData);
        * Options_SubscribeToMarketData_Async(string underlying, Action<OkexOptionsMarketData> onData);
        * Options_SubscribeToOrderBook(string instrument, OkexOrderBookDepth depth, Action<OkexOptionsOrderBook> onData);
        * Options_SubscribeToOrderBook_Async(string instrument, OkexOrderBookDepth depth, Action<OkexOptionsOrderBook> onData);
        * Options_SubscribeToOrders(string underlying, Action<OkexOptionsOrder> onData);
        * Options_SubscribeToOrders_Async(string underlying, Action<OkexOptionsOrder> onData);
        * Options_SubscribeToPositions(string underlying, Action<OkexOptionsPosition> onData);
        * Options_SubscribeToPositions_Async(string underlying, Action<OkexOptionsPosition> onData);
        * Options_SubscribeToTicker(IEnumerable<string> underlyings, Action<OkexOptionsTicker> onData);
        * Options_SubscribeToTicker(string instrument, Action<OkexOptionsTicker> onData);
        * Options_SubscribeToTicker_Async(IEnumerable<string> underlyings, Action<OkexOptionsTicker> onData);
        * Options_SubscribeToTicker_Async(string instrument, Action<OkexOptionsTicker> onData);
        * Options_SubscribeToTrades(string instrument, Action<OkexOptionsTrade> onData);
        * Options_SubscribeToTrades_Async(string instrument, Action<OkexOptionsTrade> onData);
        
* Version 1.5.4 - 21 Dec 2020
    * Added Swap WebSocket Api Support
        * Swap_SubscribeToAlgoOrders(string symbol, Action<OkexSwapAlgoOrder> onData);
        * Swap_SubscribeToAlgoOrders_Async(string symbol, Action<OkexSwapAlgoOrder> onData);
        * Swap_SubscribeToBalance(string symbol, Action<OkexSwapBalance> onData);
        * Swap_SubscribeToBalance_Async(string symbol, Action<OkexSwapBalance> onData);
        * Swap_SubscribeToCandlesticks(string symbol, OkexSpotPeriod period, Action<OkexSwapCandle> onData);
        * Swap_SubscribeToCandlesticks_Async(string symbol, OkexSpotPeriod period, Action<OkexSwapCandle> onData);
        * Swap_SubscribeToFundingRate(string symbol, Action<OkexSwapFundingRate> onData);
        * Swap_SubscribeToFundingRate_Async(string symbol, Action<OkexSwapFundingRate> onData);
        * Swap_SubscribeToMarkPrice(string symbol, Action<OkexSwapMarkPrice> onData);
        * Swap_SubscribeToMarkPrice_Async(string symbol, Action<OkexSwapMarkPrice> onData);
        * Swap_SubscribeToOrderBook(string symbol, OkexOrderBookDepth depth, Action<OkexSwapOrderBook> onData);
        * Swap_SubscribeToOrderBook_Async(string symbol, OkexOrderBookDepth depth, Action<OkexSwapOrderBook> onData);
        * Swap_SubscribeToOrders(string symbol, Action<OkexSwapOrder> onData);
        * Swap_SubscribeToOrders_Async(string symbol, Action<OkexSwapOrder> onData);
        * Swap_SubscribeToPositions(string symbol, Action<OkexSwapPosition> onData);
        * Swap_SubscribeToPositions_Async(string symbol, Action<OkexSwapPosition> onData);
        * Swap_SubscribeToPriceRange(string symbol, Action<OkexSwapPriceRange> onData);
        * Swap_SubscribeToPriceRange_Async(string symbol, Action<OkexSwapPriceRange> onData);
        * Swap_SubscribeToTicker(IEnumerable<string> symbols, Action<OkexSwapTicker> onData);
        * Swap_SubscribeToTicker(string symbol, Action<OkexSwapTicker> onData);
        * Swap_SubscribeToTicker_Async(IEnumerable<string> symbols, Action<OkexSwapTicker> onData);
        * Swap_SubscribeToTicker_Async(string symbol, Action<OkexSwapTicker> onData);
        * Swap_SubscribeToTrades(string symbol, Action<OkexSwapTrade> onData);
        * Swap_SubscribeToTrades_Async(string symbol, Action<OkexSwapTrade> onData);
        
* Version 1.5.3 - 21 Dec 2020
    * Added Futures WebSocket Api Support
        * Futures_SubscribeToAlgoOrders(string symbol, Action<OkexFuturesAlgoOrder> onData);
        * Futures_SubscribeToAlgoOrders_Async(string symbol, Action<OkexFuturesAlgoOrder> onData);
        * Futures_SubscribeToBalance(string symbol, Action<OkexFuturesBalance> onData);
        * Futures_SubscribeToBalance_Async(string symbol, Action<OkexFuturesBalance> onData);
        * Futures_SubscribeToCandlesticks(string symbol, OkexSpotPeriod period, Action<OkexFuturesCandle> onData);
        * Futures_SubscribeToCandlesticks_Async(string symbol, OkexSpotPeriod period, Action<OkexFuturesCandle> onData);
        * Futures_SubscribeToContracts(Action<OkexFuturesContract> onData);
        * Futures_SubscribeToContracts_Async(Action<OkexFuturesContract> onData);
        * Futures_SubscribeToEstimatedPrice(string symbol, Action<OkexFuturesEstimatedPrice> onData);
        * Futures_SubscribeToEstimatedPrice_Async(string symbol, Action<OkexFuturesEstimatedPrice> onData);
        * Futures_SubscribeToMarkPrice(string symbol, Action<OkexFuturesMarkPrice> onData);
        * Futures_SubscribeToMarkPrice_Async(string symbol, Action<OkexFuturesMarkPrice> onData);
        * Futures_SubscribeToOrderBook(string symbol, OkexOrderBookDepth depth, Action<OkexFuturesOrderBook> onData);
        * Futures_SubscribeToOrderBook_Async(string symbol, OkexOrderBookDepth depth, Action<OkexFuturesOrderBook> onData);
        * Futures_SubscribeToOrders(string symbol, Action<OkexFuturesOrder> onData);
        * Futures_SubscribeToOrders_Async(string symbol, Action<OkexFuturesOrder> onData);
        * Futures_SubscribeToPositions(string symbol, Action<OkexFuturesPosition> onData);
        * Futures_SubscribeToPositions_Async(string symbol, Action<OkexFuturesPosition> onData);
        * Futures_SubscribeToPriceRange(string symbol, Action<OkexFuturesPriceRange> onData);
        * Futures_SubscribeToPriceRange_Async(string symbol, Action<OkexFuturesPriceRange> onData);
        * Futures_SubscribeToTicker(IEnumerable<string> symbols, Action<OkexFuturesTicker> onData);
        * Futures_SubscribeToTicker(string symbol, Action<OkexFuturesTicker> onData);
        * Futures_SubscribeToTicker_Async(IEnumerable<string> symbols, Action<OkexFuturesTicker> onData);
        * Futures_SubscribeToTicker_Async(string symbol, Action<OkexFuturesTicker> onData);
        * Futures_SubscribeToTrades(string symbol, Action<OkexFuturesTrade> onData);
        * Futures_SubscribeToTrades_Async(string symbol, Action<OkexFuturesTrade> onData);
        
* Version 1.5.2 - 21 Dec 2020
    * Added Margin WebSocket Api Support
        * Margin_SubscribeToBalance(string symbol, Action<OkexMarginBalance> onData);
        * Margin_SubscribeToBalance_Async(string symbol, Action<OkexMarginBalance> onData);
        
* Version 1.5.1 - 21 Dec 2020
    * Added Spot WebSocket Api Support
        * Spot_SubscribeToAlgoOrders(string symbol, Action<OkexSpotAlgoOrder> onData);
        * Spot_SubscribeToAlgoOrders_Async(string symbol, Action<OkexSpotAlgoOrder> onData);
        * Spot_SubscribeToBalance(string currency, Action<OkexSpotBalance> onData);
        * Spot_SubscribeToBalance_Async(string currency, Action<OkexSpotBalance> onData);
        * Spot_SubscribeToCandlesticks(string symbol, OkexSpotPeriod period, Action<OkexSpotCandle> onData);
        * Spot_SubscribeToCandlesticks_Async(string symbol, OkexSpotPeriod period, Action<OkexSpotCandle> onData);
        * Spot_SubscribeToOrderBook(string symbol, OkexOrderBookDepth depth, Action<OkexSpotOrderBook> onData);
        * Spot_SubscribeToOrders(string symbol, Action<OkexSpotOrderDetails> onData);
        * Spot_SubscribeToOrders_Async(string symbol, Action<OkexSpotOrderDetails> onData);
        * Spot_SubscribeToTicker(IEnumerable<string> symbols, Action<OkexSpotTicker> onData);
        * Spot_SubscribeToTicker(string symbol, Action<OkexSpotTicker> onData);
        * Spot_SubscribeToTicker_Async(IEnumerable<string> symbols, Action<OkexSpotTicker> onData);
        * Spot_SubscribeToTicker_Async(string symbol, Action<OkexSpotTicker> onData);
        * Spot_SubscribeToTrades(string symbol, Action<OkexSpotTrade> onData);
        * Spot_SubscribeToTrades_Async(string symbol, Action<OkexSpotTrade> onData);
        * Spot_SubscribeToTrades_Async(string symbol, OkexOrderBookDepth depth, Action<OkexSpotOrderBook> onData);

* Version 1.5.0 - 21 Dec 2020
    * Fixed WebSocket Service Api Bugs
    * Added Web Socket Auth Algorithm

* Version 1.4.3 - 20 Dec 2020
    * Added Options Rest Api Support
        * Options_BatchCancelOrders(string underlying, IEnumerable<long> orderIds, IEnumerable<string> clientOrderIds, CancellationToken ct = default);
        * Options_BatchCancelOrders_Async(string underlying, IEnumerable<long> orderIds, IEnumerable<string> clientOrderIds, CancellationToken ct = default);
        * Options_BatchModifyOrders(string underlying, IEnumerable<OkexOptionsModifyOrder> orders, CancellationToken ct = default);
        * Options_BatchModifyOrders_Async(string underlying, IEnumerable<OkexOptionsModifyOrder> orders, CancellationToken ct = default);
        * Options_BatchPlaceOrders(string underlying, IEnumerable<OkexOptionsPlaceOrder> orders, CancellationToken ct = default);
        * Options_BatchPlaceOrders_Async(string underlying, IEnumerable<OkexOptionsPlaceOrder> orders, CancellationToken ct = default);
        * Options_CancelAllOrders(string underlying, CancellationToken ct = default);
        * Options_CancelAllOrders_Async(string underlying, CancellationToken ct = default);
        * Options_CancelOrder(string underlying, long? orderId = null, string clientOrderId = null, CancellationToken ct = default);
        * Options_CancelOrder_Async(string underlying, long? orderId = null, string clientOrderId = null, CancellationToken ct = default);
        * Options_GetAllOrders(string underlying, OkexOptionsOrderState state, string instrument = null, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default);
        * Options_GetAllOrders_Async(string underlying, OkexOptionsOrderState state, string instrument = null, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default);
        * Options_GetBalances(string underlying, CancellationToken ct = default);
        * Options_GetBalances_Async(string underlying, CancellationToken ct = default);
        * Options_GetBills(string underlying, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default);
        * Options_GetBills_Async(string underlying, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default);
        * Options_GetCandles(string instrument, OkexSpotPeriod period, DateTime? start = null, DateTime? end = null, CancellationToken ct = default);
        * Options_GetCandles_Async(string instrument, OkexSpotPeriod period, DateTime? start = null, DateTime? end = null, CancellationToken ct = default);
        * Options_GetInstruments(string underlying, string instrument = null, DateTime? delivery = null, CancellationToken ct = default);
        * Options_GetInstruments_Async(string underlying, string instrument = null, DateTime? delivery = null, CancellationToken ct = default);
        * Options_GetMarketData(string underlying, DateTime? delivery = null, CancellationToken ct = default);
        * Options_GetMarketData(string underlying, string instrument, CancellationToken ct = default);
        * Options_GetMarketData_Async(string underlying, DateTime? delivery = null, CancellationToken ct = default);
        * Options_GetMarketData_Async(string underlying, string instrument, CancellationToken ct = default);
        * Options_GetOrderBook(string instrument, int size = 200, CancellationToken ct = default);
        * Options_GetOrderBook_Async(string instrument, int size, CancellationToken ct = default);
        * Options_GetOrderDetails(string underlying, long? orderId = null, string clientOrderId = null, CancellationToken ct = default);
        * Options_GetOrderDetails_Async(string underlying, long? orderId = null, string clientOrderId = null, CancellationToken ct = default);
        * Options_GetPositions(string underlying, string instrument = null, CancellationToken ct = default);
        * Options_GetPositions_Async(string underlying, string instrument = null, CancellationToken ct = default);
        * Options_GetSettlementHistory(string underlying, DateTime? start = null, DateTime? end = null, int limit = 5, CancellationToken ct = default);
        * Options_GetSettlementHistory_Async(string underlying, DateTime? start = null, DateTime? end = null, int limit = 5, CancellationToken ct = default);
        * Options_GetTicker(string instrument, CancellationToken ct = default);
        * Options_GetTicker_Async(string instrument, CancellationToken ct = default);
        * Options_GetTradeFeeRates(string underlying = null, int? category = null, CancellationToken ct = default);
        * Options_GetTradeFeeRates_Async(string underlying = null, int? category = null, CancellationToken ct = default);
        * Options_GetTrades(string instrument, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default);
        * Options_GetTrades_Async(string instrument, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default);
        * Options_GetTransactionDetails(string underlying, string instrument = null, long? orderId = null, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default);
        * Options_GetTransactionDetails_Async(string underlying, string instrument = null, long? orderId = null, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default);
        * Options_GetUnderlyingList(CancellationToken ct = default);
        * Options_GetUnderlyingList_Async(CancellationToken ct = default);
        * Options_ModifyOrder(string underlying, long? orderId = null, string clientOrderId = null, string requestId = null, decimal? newSize = null, decimal? newPrice = null, bool? cancelOnFail = null, CancellationToken ct = default);
        * Options_ModifyOrder_Async(string underlying, long? orderId = null, string clientOrderId = null, string requestId = null, decimal? newSize = null, decimal? newPrice = null, bool? cancelOnFail = null, CancellationToken ct = default);
        * Options_PlaceOrder(string instrument, OkexOptionsOrderSide side, decimal price, decimal size, OkexOptionsTimeInForce timeInForce = OkexOptionsTimeInForce.NormalOrder, bool match_price = false, string clientOrderId = null, CancellationToken ct = default);
        * Options_PlaceOrder_Async(string instrument, OkexOptionsOrderSide side, decimal price, decimal size, OkexOptionsTimeInForce timeInForce = OkexOptionsTimeInForce.NormalOrder, bool match_price = false, string clientOrderId = null, CancellationToken ct = default);

* Version 1.4.2 - 17 Dec 2020
    * Added Contract Rest Api Support
        * Contract_GetLongShortRatio(string currency, OkexContractPeriod period = OkexContractPeriod.FiveMinutes, DateTime? start = null, DateTime? end = null, CancellationToken ct = default);
        * Contract_GetLongShortRatio_Async(string currency, OkexContractPeriod period = OkexContractPeriod.FiveMinutes, DateTime? start = null, DateTime? end = null, CancellationToken ct = default);
        * Contract_GetMargin(string currency, OkexContractPeriod period = OkexContractPeriod.FiveMinutes, DateTime? start = null, DateTime? end = null, CancellationToken ct = default);
        * Contract_GetMargin_Async(string currency, OkexContractPeriod period = OkexContractPeriod.FiveMinutes, DateTime? start = null, DateTime? end = null, CancellationToken ct = default);
        * Contract_GetSentiment(string currency, OkexContractPeriod period = OkexContractPeriod.FiveMinutes, DateTime? start = null, DateTime? end = null, CancellationToken ct = default);
        * Contract_GetSentiment_Async(string currency, OkexContractPeriod period = OkexContractPeriod.FiveMinutes, DateTime? start = null, DateTime? end = null, CancellationToken ct = default);
        * Contract_GetTakerVolume(string currency, OkexContractPeriod period = OkexContractPeriod.FiveMinutes, DateTime? start = null, DateTime? end = null, CancellationToken ct = default);
        * Contract_GetTakerVolume_Async(string currency, OkexContractPeriod period = OkexContractPeriod.FiveMinutes, DateTime? start = null, DateTime? end = null, CancellationToken ct = default);
        * Contract_GetVolume(string currency, OkexContractPeriod period = OkexContractPeriod.FiveMinutes, DateTime? start = null, DateTime? end = null, CancellationToken ct = default);
        * Contract_GetVolume_Async(string currency, OkexContractPeriod period = OkexContractPeriod.FiveMinutes, DateTime? start = null, DateTime? end = null, CancellationToken ct = default);

* Version 1.4.1 - 17 Dec 2020
    * Added Perpetual Swap Rest Api Support
        * Swap_AlgoCancelOrder(string symbol, OkexAlgoOrderType type, IEnumerable<long> algo_ids, CancellationToken ct = default)
        * Swap_AlgoCancelOrder_Async(string symbol, OkexAlgoOrderType type, IEnumerable<long> algo_ids, CancellationToken ct = default)
        * Swap_AlgoGetOrders(string symbol, OkexAlgoOrderType type, OkexAlgoStatus? status = null, IEnumerable<long> algo_ids = null, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default)
        * Swap_AlgoGetOrders_Async(string symbol, OkexAlgoOrderType type, OkexAlgoStatus? status = null, IEnumerable<long> algo_ids = null, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default)
        * Swap_AlgoPlaceOrder(string symbol, OkexSwapOrderType type, OkexAlgoOrderType order_type, decimal size, decimal? trigger_price = null, decimal? trigger_algo_price = null, OkexAlgoPriceType? trigger_algo_type = null, decimal? trail_callback_rate = null, decimal? trail_trigger_price = null, decimal? iceberg_algo_variance = null, decimal? iceberg_avg_amount = null, decimal? iceberg_limit_price = null, decimal? twap_sweep_range = null, decimal? twap_sweep_ratio = null, int? twap_single_limit = null, decimal? twap_price_limit = null, int? twap_time_interval = null, OkexAlgoPriceType? tp_trigger_type = null, decimal? tp_trigger_price = null, decimal? tp_price = null, OkexAlgoPriceType? sl_trigger_type = null, decimal? sl_trigger_price = null, decimal? sl_price = null, CancellationToken ct = default)
        * Swap_AlgoPlaceOrder_Async(string symbol, OkexSwapOrderType type, OkexAlgoOrderType order_type, decimal size, decimal? trigger_price = null, decimal? trigger_algo_price = null, OkexAlgoPriceType? trigger_algo_type = null, decimal? trail_callback_rate = null, decimal? trail_trigger_price = null, decimal? iceberg_algo_variance = null, decimal? iceberg_avg_amount = null, decimal? iceberg_limit_price = null, decimal? twap_sweep_range = null, decimal? twap_sweep_ratio = null, int? twap_single_limit = null, decimal? twap_price_limit = null, int? twap_time_interval = null, OkexAlgoPriceType? tp_trigger_type = null, decimal? tp_trigger_price = null, decimal? tp_price = null, OkexAlgoPriceType? sl_trigger_type = null, decimal? sl_trigger_price = null, decimal? sl_price = null, CancellationToken ct = default)
        * Swap_BatchCancelOrders(string symbol, IEnumerable<long> orderIds, IEnumerable<string> clientOrderIds, CancellationToken ct = default)
        * Swap_BatchCancelOrders_Async(string symbol, IEnumerable<long> orderIds, IEnumerable<string> clientOrderIds, CancellationToken ct = default)
        * Swap_BatchModifyOrders(string symbol, IEnumerable<OkexSwapModifyOrder> orders, CancellationToken ct = default)
        * Swap_BatchModifyOrders_Async(string symbol, IEnumerable<OkexSwapModifyOrder> orders, CancellationToken ct = default)
        * Swap_BatchPlaceOrders(string symbol, IEnumerable<OkexSwapPlaceOrder> orders, CancellationToken ct = default)
        * Swap_BatchPlaceOrders_Async(string symbol, IEnumerable<OkexSwapPlaceOrder> orders, CancellationToken ct = default)
        * Swap_CancelAll(string symbol, OkexSwapDirection direction, CancellationToken ct = default)
        * Swap_CancelAll_Async(string symbol, OkexSwapDirection direction, CancellationToken ct = default)
        * Swap_CancelOrder(string symbol, long? orderId = null, string clientOrderId = null, CancellationToken ct = default)
        * Swap_CancelOrder_Async(string symbol, long? orderId = null, string clientOrderId = null, CancellationToken ct = default)
        * Swap_GetAllOrders(string symbol, OkexSwapOrderState state, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default)
        * Swap_GetAllOrders_Async(string symbol, OkexSwapOrderState state, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default)
        * Swap_GetAllTickers(CancellationToken ct = default)
        * Swap_GetAllTickers_Async(CancellationToken ct = default)
        * Swap_GetBalances(CancellationToken ct = default)
        * Swap_GetBalances(string symbol, CancellationToken ct = default)
        * Swap_GetBalances_Async(CancellationToken ct = default)
        * Swap_GetBalances_Async(string symbol, CancellationToken ct = default)
        * Swap_GetBills(string symbol, OkexSwapBillType? type = null, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default)
        * Swap_GetBills_Async(string symbol, OkexSwapBillType? type = null, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default)
        * Swap_GetCandles(string symbol, OkexSpotPeriod period, DateTime? start = null, DateTime? end = null, CancellationToken ct = default)
        * Swap_GetCandles_Async(string symbol, OkexSpotPeriod period, DateTime? start = null, DateTime? end = null, CancellationToken ct = default)
        * Swap_GetFiatExchangeRates(CancellationToken ct = default)
        * Swap_GetFiatExchangeRates_Async(CancellationToken ct = default)
        * Swap_GetFundingRateHistory(string symbol, int limit = 100, CancellationToken ct = default)
        * Swap_GetFundingRateHistory_Async(string symbol, int limit = 100, CancellationToken ct = default)
        * Swap_GetHistoricalMarketData(string symbol, OkexSpotPeriod period, DateTime? start = null, DateTime? end = null, int limit = 300, CancellationToken ct = default)
        * Swap_GetHistoricalMarketData_Async(string symbol, OkexSpotPeriod period, DateTime? start = null, DateTime? end = null, int limit = 300, CancellationToken ct = default)
        * Swap_GetHoldAmount(string symbol, CancellationToken ct = default)
        * Swap_GetHoldAmount_Async(string symbol, CancellationToken ct = default)
        * Swap_GetIndices(string symbol, CancellationToken ct = default)
        * Swap_GetIndices_Async(string symbol, CancellationToken ct = default)
        * Swap_GetLeverage(string symbol, CancellationToken ct = default)
        * Swap_GetLeverage_Async(string symbol, CancellationToken ct = default)
        * Swap_GetLiquidatedOrders(string symbol, OkexSwapLiquidationStatus status, int limit = 100, long? from = null, long? to = null, CancellationToken ct = default)
        * Swap_GetLiquidatedOrders_Async(string symbol, OkexSwapLiquidationStatus status, int limit = 100, long? from = null, long? to = null, CancellationToken ct = default)
        * Swap_GetMarkPrice(string symbol, CancellationToken ct = default)
        * Swap_GetMarkPrice_Async(string symbol, CancellationToken ct = default)
        * Swap_GetNextSettlementTime(string symbol, CancellationToken ct = default)
        * Swap_GetNextSettlementTime_Async(string symbol, CancellationToken ct = default)
        * Swap_GetOpenInterests(string symbol, CancellationToken ct = default)
        * Swap_GetOpenInterests_Async(string symbol, CancellationToken ct = default)
        * Swap_GetOrderBook(string symbol, int? size = null, decimal? depth = null, CancellationToken ct = default)
        * Swap_GetOrderBook_Async(string symbol, int? size = null, decimal? depth = null, CancellationToken ct = default)
        * Swap_GetOrderDetails(string symbol, long? orderId = null, string clientOrderId = null, CancellationToken ct = default)
        * Swap_GetOrderDetails_Async(string symbol, long? orderId = null, string clientOrderId = null, CancellationToken ct = default)
        * Swap_GetPositions(CancellationToken ct = default)
        * Swap_GetPositions(string symbol, CancellationToken ct = default)
        * Swap_GetPositions_Async(CancellationToken ct = default)
        * Swap_GetPositions_Async(string symbol, CancellationToken ct = default)
        * Swap_GetPriceLimit(string symbol, CancellationToken ct = default)
        * Swap_GetPriceLimit_Async(string symbol, CancellationToken ct = default)
        * Swap_GetSymbolTicker(string symbol, CancellationToken ct = default)
        * Swap_GetSymbolTicker_Async(string symbol, CancellationToken ct = default)
        * Swap_GetTradeFeeRates(string symbol = null, int? category = null, CancellationToken ct = default)
        * Swap_GetTradeFeeRates_Async(string symbol = null, int? category = null, CancellationToken ct = default)
        * Swap_GetTrades(string symbol, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default)
        * Swap_GetTrades_Async(string symbol, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default)
        * Swap_GetTradingContracts(CancellationToken ct = default)
        * Swap_GetTradingContracts_Async(CancellationToken ct = default)
        * Swap_GetTransactionDetails(string symbol, long? orderId = null, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default)
        * Swap_GetTransactionDetails_Async(string symbol, long? orderId = null, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default)
        * Swap_MarketCloseAll(string symbol, OkexSwapDirection direction, CancellationToken ct = default)
        * Swap_MarketCloseAll_Async(string symbol, OkexSwapDirection direction, CancellationToken ct = default)
        * Swap_ModifyOrder(string symbol, long? orderId = null, string clientOrderId = null, string requestId = null, decimal? newSize = null, decimal? newPrice = null, bool? cancelOnFail = null, CancellationToken ct = default)
        * Swap_ModifyOrder_Async(string symbol, long? orderId = null, string clientOrderId = null, string requestId = null, decimal? newSize = null, decimal? newPrice = null, bool? cancelOnFail = null, CancellationToken ct = default)
        * Swap_PlaceOrder(string symbol, OkexSwapOrderType type, decimal size, OkexSwapTimeInForce timeInForce = OkexSwapTimeInForce.NormalOrder, decimal? price = null, bool match_price = false, string clientOrderId = null, CancellationToken ct = default)
        * Swap_PlaceOrder_Async(string symbol, OkexSwapOrderType type, decimal size, OkexSwapTimeInForce timeInForce = OkexSwapTimeInForce.NormalOrder, decimal? price = null, bool match_price = false, string clientOrderId = null, CancellationToken ct = default)
        * Swap_SetLeverage(string symbol, OkexSwapLeverageSide side, int leverage, CancellationToken ct = default)
        * Swap_SetLeverage_Async(string symbol, OkexSwapLeverageSide side, int leverage, CancellationToken ct = default)

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
        * Futures_SetLeverage(OkexFuturesMarginMode mode, string underlying, int leverage, string instrument_id = null, OkexFuturesDirection? direction = null, CancellationToken ct = default)
        * Futures_SetLeverage_Async(OkexFuturesMarginMode mode, string underlying, int leverage, string instrument_id = null, OkexFuturesDirection? direction = null, CancellationToken ct = default)
        * Futures_GetSymbolBills(string underlying, OkexFuturesBillType? type = null, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default)
        * Futures_GetSymbolBills_Async(string underlying, OkexFuturesBillType? type = null, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default)
        * Futures_PlaceOrder(string symbol, OkexFuturesOrderType type, decimal size, OkexFuturesTimeInForce timeInForce = OkexFuturesTimeInForce.NormalOrder, decimal? price = null, bool match_price = false, string clientOrderId = null, CancellationToken ct = default)
        * Futures_PlaceOrder_Async(string symbol, OkexFuturesOrderType type, decimal size, OkexFuturesTimeInForce timeInForce = OkexFuturesTimeInForce.NormalOrder, decimal? price = null, bool match_price = false, string clientOrderId = null, CancellationToken ct = default)
        * Futures_BatchPlaceOrders(string symbol, IEnumerable<OkexFuturesPlaceOrder> orders, CancellationToken ct = default)
        * Futures_BatchPlaceOrders_Async(string symbol, IEnumerable<OkexFuturesPlaceOrder> orders, CancellationToken ct = default)
        * Futures_ModifyOrder(string symbol, long? orderId = null, string clientOrderId = null, string requestId = null, decimal? newSize = null, decimal? newPrice = null, bool? cancelOnFail = null, CancellationToken ct = default)
        * Futures_ModifyOrder_Async(string symbol, long? orderId = null, string clientOrderId = null, string requestId = null, decimal? newSize = null, decimal? newPrice = null, bool? cancelOnFail = null, CancellationToken ct = default)
        * Futures_BatchModifyOrders(string symbol, IEnumerable<OkexFuturesModifyOrder> orders, CancellationToken ct = default)
        * Futures_BatchModifyOrders_Async(string symbol, IEnumerable<OkexFuturesModifyOrder> orders, CancellationToken ct = default)
        * Futures_CancelOrder(string symbol, long? orderId = null, string clientOrderId = null, CancellationToken ct = default)
        * Futures_CancelOrder_Async(string symbol, long? orderId = null, string clientOrderId = null, CancellationToken ct = default)
        * Futures_BatchCancelOrders(string symbol, IEnumerable<long> orderIds, IEnumerable<string> clientOrderIds, CancellationToken ct = default)
        * Futures_BatchCancelOrders_Async(string symbol, IEnumerable<long> orderIds, IEnumerable<string> clientOrderIds, CancellationToken ct = default)
        * Futures_GetAllOrders(string symbol, OkexFuturesOrderState state, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default)
        * Futures_GetAllOrders_Async(string symbol, OkexFuturesOrderState state, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default)
        * Futures_GetOrderDetails(string symbol, long? orderId = null, string clientOrderId = null, CancellationToken ct = default)
        * Futures_GetOrderDetails_Async(string symbol, long? orderId = null, string clientOrderId = null, CancellationToken ct = default)
        * Futures_GetTransactionDetails(string symbol, long? orderId = null, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default)
        * Futures_GetTransactionDetails_Async(string symbol, long? orderId = null, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default)
        * Futures_SetAccountMode(string symbol, OkexFuturesMarginMode margin_mode, CancellationToken ct = default)
        * Futures_SetAccountMode_Async(string symbol, OkexFuturesMarginMode margin_mode, CancellationToken ct = default)
        * Futures_GetTradeFeeRates(string symbol = null, int? category = null, CancellationToken ct = default)
        * Futures_GetTradeFeeRates_Async(string symbol = null, int? category = null, CancellationToken ct = default)
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
        * Spot_ModifyOrder(string symbol, long? orderId = null, string clientOrderId = null, string requestId = null, decimal? newSize = null, decimal? newPrice = null, bool? cancelOnFail = null, CancellationToken ct = default)
        * Spot_ModifyOrder_Async(string symbol, long? orderId = null, string clientOrderId = null, string requestId = null, decimal? newSize = null, decimal? newPrice = null, bool? cancelOnFail = null, CancellationToken ct = default)
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
        * Margin_Loan(string symbol, string currency, decimal amount, string clientOrderId = null, CancellationToken ct = default)
        * Margin_Loan_Async(string symbol, string currency, decimal amount, string clientOrderId = null, CancellationToken ct = default)
        * Margin_Repayment(string symbol, string currency, decimal amount, long? borrow_id = null, string clientOrderId = null, CancellationToken ct = default)
        * Margin_Repayment_Async(string symbol, string currency, decimal amount, long? borrow_id = null, string clientOrderId = null, CancellationToken ct = default)
        * Margin_PlaceOrder(OkexSpotPlaceOrder order, CancellationToken ct = default)
        * Margin_PlaceOrder_Async(OkexSpotPlaceOrder order, CancellationToken ct = default)
        * Margin_PlaceOrder(string symbol, OkexSpotOrderSide side, OkexSpotOrderType type, OkexSpotTimeInForce timeInForce = OkexSpotTimeInForce.NormalOrder, decimal? price = null, decimal? size = null, decimal? notional = null, string clientOrderId = null, CancellationToken ct = default)
        * Margin_PlaceOrder_Async(string symbol, OkexSpotOrderSide side, OkexSpotOrderType type, OkexSpotTimeInForce timeInForce = OkexSpotTimeInForce.NormalOrder, decimal? price = null, decimal? size = null, decimal? notional = null, string clientOrderId = null, CancellationToken ct = default)
        * Margin_BatchPlaceOrders(IEnumerable<OkexSpotPlaceOrder> orders, CancellationToken ct = default)
        * Margin_BatchPlaceOrders_Async(IEnumerable<OkexSpotPlaceOrder> orders, CancellationToken ct = default)
        * Margin_CancelOrder(string symbol, long? orderId = null, string clientOrderId = null, CancellationToken ct = default)
        * Margin_CancelOrder_Async(string symbol, long? orderId = null, string clientOrderId = null, CancellationToken ct = default)
        * Margin_BatchCancelOrders(IEnumerable<OkexSpotCancelOrder> orders, CancellationToken ct = default)
        * Margin_BatchCancelOrders_Async(IEnumerable<OkexSpotCancelOrder> orders, CancellationToken ct = default)
        * Margin_GetAllOrders(string symbol, OkexSpotOrderState state, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default)
        * Margin_GetAllOrders_Async(string symbol, OkexSpotOrderState state, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default)
        * Margin_GetLeverage(string symbol, CancellationToken ct = default)
        * Margin_GetLeverage_Async(string symbol, CancellationToken ct = default)
        * Margin_SetLeverage(string symbol, int leverage, CancellationToken ct = default)
        * Margin_SetLeverage_Async(string symbol, int leverage, CancellationToken ct = default)
        * Margin_GetOrderDetails(string symbol, long? orderId = null, string clientOrderId = null, CancellationToken ct = default)
        * Margin_GetOrderDetails_Async(string symbol, long? orderId = null, string clientOrderId = null, CancellationToken ct = default)
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