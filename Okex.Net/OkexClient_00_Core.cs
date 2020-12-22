namespace Okex.Net
{
    public partial class OkexClient
	{
		#region Endpoints

		#region 01 - General & System API Endpoints
		private const string Endpoints_General_Time = "api/general/v3/time";
		private const string Endpoints_System_Status = "api/system/v3/status";
		#endregion

		#region 02 - Funding Account API Endpoints
		private const string Endpoints_Funding_GetAllBalances = "api/account/v3/wallet";
		private const string Endpoints_Funding_GetSubAccount = "api/account/v3/sub-account";
		private const string Endpoints_Funding_GetAssetValuation = "api/account/v3/asset-valuation";
		private const string Endpoints_Funding_GetCurrencyBalance = "api/account/v3/wallet/<currency>";
		private const string Endpoints_Funding_Transfer = "api/account/v3/transfer";
		private const string Endpoints_Funding_Withdrawal = "api/account/v3/withdrawal";
		private const string Endpoints_Funding_WithdrawalHistory = "api/account/v3/withdrawal/history";
		private const string Endpoints_Funding_WithdrawalHistoryOfCurrency = "api/account/v3/withdrawal/history/<currency>";
		private const string Endpoints_Funding_Bills = "api/account/v3/ledger";
		private const string Endpoints_Funding_DepositAddress = "api/account/v3/deposit/address";
		private const string Endpoints_Funding_DepositHistory = "api/account/v3/deposit/history";
		private const string Endpoints_Funding_DepositHistoryOfCurrency = "api/account/v3/deposit/history/<currency>";
		private const string Endpoints_Funding_GetCurrencies = "api/account/v3/currencies";
		private const string Endpoints_Funding_GetUserID = "api/account/v3/uid";
		private const string Endpoints_Funding_WithdrawalFees = "api/account/v3/withdrawal/fee";
		private const string Endpoints_Funding_PiggyBank = "api/account/v3/purchase_redempt";
		#endregion

		#region 03 - Spot Trading API Endpoints

        #region Private Signed Endpoints
        private const string Endpoints_Spot_Accounts = "api/spot/v3/accounts";
		private const string Endpoints_Spot_AccountsOfCurrency = "api/spot/v3/accounts/<currency>";
		private const string Endpoints_Spot_Bills = "api/spot/v3/accounts/<currency>/ledger";
		private const string Endpoints_Spot_PlaceOrder = "api/spot/v3/orders";
		private const string Endpoints_Spot_PlaceBatchOrders = "api/spot/v3/batch_orders";
		private const string Endpoints_Spot_CancelOrder = "api/spot/v3/cancel_orders/<order_id>";
		private const string Endpoints_Spot_CancelBatchOrders = "api/spot/v3/cancel_batch_orders";
		private const string Endpoints_Spot_ModifyOrder = "api/spot/v3/amend_order/<instrument_id>";
		private const string Endpoints_Spot_BatchModifyOrders = "api/spot/v3/amend_batch_orders/<instrument_id>";
		private const string Endpoints_Spot_OrderList = "api/spot/v3/orders";
		private const string Endpoints_Spot_OpenOrders = "api/spot/v3/orders_pending";
		private const string Endpoints_Spot_OrderDetails = "api/spot/v3/orders/<order_id>";
		private const string Endpoints_Spot_TradeFee = "api/spot/v3/trade_fee";
		private const string Endpoints_Spot_TransactionDetails = "api/spot/v3/fills";
		private const string Endpoints_Spot_AlgoPlaceOrder = "api/spot/v3/order_algo";
		private const string Endpoints_Spot_AlgoCancelOrder = "api/spot/v3/cancel_batch_algos";
		private const string Endpoints_Spot_AlgoOrderList = "api/spot/v3/algo";
		#endregion

		#region Public Unsigned Endpoints
		private const string Endpoints_Spot_TradingPairs = "api/spot/v3/instruments";
		private const string Endpoints_Spot_OrderBook = "api/spot/v3/instruments/<instrument_id>/book";
		private const string Endpoints_Spot_TradingPairsTicker = "api/spot/v3/instruments/ticker";
		private const string Endpoints_Spot_TradingPairsTickerOfSymbol = "api/spot/v3/instruments/<instrument_id>/ticker";
		private const string Endpoints_Spot_Trades = "api/spot/v3/instruments/<instrument_id>/trades";
		private const string Endpoints_Spot_Candles = "api/spot/v3/instruments/<instrument_id>/candles";
		private const string Endpoints_Spot_HistoricalCandles = "api/spot/v3/instruments/<instrument_id>/history/candles";
		#endregion

		#endregion
		
		#region 04 - Margin Trading API Endpoints

        #region Private Signed Endpoints
		private const string Endpoints_Margin_Accounts = "api/margin/v3/accounts";
		private const string Endpoints_Margin_AccountsOfSymbol = "api/margin/v3/accounts/<instrument_id>";
		private const string Endpoints_Margin_Bills = "api/margin/v3/accounts/<currency>/ledger";
		private const string Endpoints_Margin_AccountSettings = "api/margin/v3/accounts/availability";
		private const string Endpoints_Margin_AccountSettingsOfCurrency = "api/margin/v3/accounts/<instrument_id>/availability";
		private const string Endpoints_Margin_LoanHistory = "api/margin/v3/accounts/borrowed";
		private const string Endpoints_Margin_LoanHistoryOfCurrency = "api/margin/v3/accounts/<instrument_id>/borrowed";
		private const string Endpoints_Margin_Loan = "api/margin/v3/accounts/borrow";
		private const string Endpoints_Margin_Repayment = "api/margin/v3/accounts/repayment";
		private const string Endpoints_Margin_PlaceOrder = "api/margin/v3/orders";
		private const string Endpoints_Margin_PlaceBatchOrders = "api/margin/v3/batch_orders";
		private const string Endpoints_Margin_CancelOrder = "api/margin/v3/cancel_orders/<order_id>";
		private const string Endpoints_Margin_CancelBatchOrders = "api/margin/v3/cancel_batch_orders";
		private const string Endpoints_Margin_OrderList = "api/margin/v3/orders";
		private const string Endpoints_Margin_GetLeverage = "api/margin/v3/accounts/<instrument_id>/leverage";
		private const string Endpoints_Margin_SetLeverage = "api/margin/v3/accounts/<instrument_id>/leverage";
		private const string Endpoints_Margin_OrderDetails = "api/margin/v3/orders/<order_id>";
		private const string Endpoints_Margin_OpenOrders = "api/margin/v3/orders_pending";
		private const string Endpoints_Margin_TransactionDetails = "api/margin/v3/fills";
		#endregion

		#region Public Unsigned Endpoints
		private const string Endpoints_Margin_MarkPrice = "api/margin/v3/instruments/<instrument_id>/mark_price";
		#endregion

		#endregion

		#region 05 - Futures Trading API Endpoints

		#region Private Signed Endpoints
		private const string Endpoints_Futures_Positions = "api/futures/v3/position";
		private const string Endpoints_Futures_PositionsOfContract = "api/futures/v3/<instrument_id>/position";
		private const string Endpoints_Futures_Accounts = "api/futures/v3/accounts";
		private const string Endpoints_Futures_AccountsOfCurrency = "api/futures/v3/accounts/<underlying>";
		private const string Endpoints_Futures_GetFuturesLeverage = "api/futures/v3/accounts/<underlying>/leverage";
		private const string Endpoints_Futures_SetFuturesLeverage = "api/futures/v3/accounts/<underlying>/leverage";
		private const string Endpoints_Futures_Bills = "api/futures/v3/accounts/<underlying>/ledger";
		private const string Endpoints_Futures_PlaceOrder = "api/futures/v3/order";
		private const string Endpoints_Futures_BatchPlaceOrders = "api/futures/v3/orders";
		private const string Endpoints_Futures_ModifyOrder = "api/futures/v3/amend_order/<instrument_id>";
		private const string Endpoints_Futures_BatchModifyOrders = "api/futures/v3/amend_batch_orders/<instrument_id>";
		private const string Endpoints_Futures_CancelOrder = "api/futures/v3/cancel_order/<instrument_id>/<order_id>";
		private const string Endpoints_Futures_BatchCancelOrders = "api/futures/v3/cancel_batch_orders/<instrument_id>";
		private const string Endpoints_Futures_OrderList = "api/futures/v3/orders/<instrument_id>";
		private const string Endpoints_Futures_OrderDetails = "api/futures/v3/orders/<instrument_id>/<order_id>";
		private const string Endpoints_Futures_TransactionDetails = "api/futures/v3/fills";
		private const string Endpoints_Futures_SetAccountMode = "api/futures/v3/accounts/margin_mode";
		private const string Endpoints_Futures_TradeFee = "api/futures/v3/trade_fee";
		private const string Endpoints_Futures_MarketCloseAll = "api/futures/v3/close_position";
		private const string Endpoints_Futures_CancelAll = "api/futures/v3/cancel_all";
		private const string Endpoints_Futures_HoldAmount = "api/futures/v3/accounts/<instrument_id>/holds";
		private const string Endpoints_Futures_AlgoPlaceOrder = "api/futures/v3/order_algo";
		private const string Endpoints_Futures_AlgoCancelOrder = "api/futures/v3/cancel_algos";
		private const string Endpoints_Futures_AlgoOrderList = "api/futures/v3/order_algo/<instrument_id>";
		private const string Endpoints_Futures_IncreaseDecreaseMargin = "api/futures/v3/position/margin";
		private const string Endpoints_Futures_SetMarginAutomatically = "api/futures/v3/accounts/auto_margin";
		#endregion

		#region Public Unsigned Endpoints
		private const string Endpoints_Futures_TradingContracts = "api/futures/v3/instruments";
		private const string Endpoints_Futures_OrderBook = "api/futures/v3/instruments/<instrument_id>/book";
		private const string Endpoints_Futures_TradingContractsTicker = "api/futures/v3/instruments/ticker";
		private const string Endpoints_Futures_TradingContractsTickerOfSymbol = "api/futures/v3/instruments/<instrument_id>/ticker";
		private const string Endpoints_Futures_Trades = "api/futures/v3/instruments/<instrument_id>/trades";
		private const string Endpoints_Futures_Candles = "api/futures/v3/instruments/<instrument-id>/candles";
		private const string Endpoints_Futures_Indices = "api/futures/v3/instruments/<instrument_id>/index";
		private const string Endpoints_Futures_ExchangeRates = "api/futures/v3/rate";
		private const string Endpoints_Futures_EstimatedDeliveryPrice = "api/futures/v3/instruments/<instrument_id>/estimated_price";
		private const string Endpoints_Futures_OpenInterest = "api/futures/v3/instruments/<instrument_id>/open_interest";
		private const string Endpoints_Futures_PriceLimit = "api/futures/v3/instruments/<instrument_id>/price_limit";
		private const string Endpoints_Futures_LiquidatedOrders = "api/futures/v3/instruments/<instrument_id>/liquidation";
		private const string Endpoints_Futures_SettlementHistory = "api/futures/v3/settlement/history";
		private const string Endpoints_Futures_MarkPrice = "api/futures/v3/instruments/<instrument_id>/mark_price";
		private const string Endpoints_Futures_HistoricalMarketData = "api/futures/v3/instruments/<instrument_id>/history/candles";
		#endregion

		#endregion

		#region 06 - Perpetual Swap API Endpoints

		#region Private Signed Endpoints
		private const string Endpoints_Swap_Positions = "api/swap/v3/position";
		private const string Endpoints_Swap_PositionsOfContract = "api/swap/v3/<instrument_id>/position";
		private const string Endpoints_Swap_Accounts = "api/swap/v3/accounts";
		private const string Endpoints_Swap_AccountsOfCurrency = "api/swap/v3/<instrument_id>/accounts";
		private const string Endpoints_Swap_GetSwapLeverage = "api/swap/v3/accounts/<instrument_id>/settings";
		private const string Endpoints_Swap_SetSwapLeverage = "api/swap/v3/accounts/<instrument_id>/leverage";
		private const string Endpoints_Swap_Bills = "api/swap/v3/accounts/<instrument_id>/ledger";
		private const string Endpoints_Swap_PlaceOrder = "api/swap/v3/order";
		private const string Endpoints_Swap_BatchPlaceOrders = "api/swap/v3/orders";
		private const string Endpoints_Swap_CancelOrder = "api/swap/v3/cancel_order/<instrument_id>/<order_id>";
		private const string Endpoints_Swap_BatchCancelOrders = "api/swap/v3/cancel_batch_orders/<instrument_id>";
		private const string Endpoints_Swap_ModifyOrder = "api/swap/v3/amend_order/<instrument_id>";
		private const string Endpoints_Swap_BatchModifyOrder = "api/swap/v3/amend_batch_orders/<instrument_id>";
		private const string Endpoints_Swap_OrderList = "api/swap/v3/orders/<instrument_id>";
		private const string Endpoints_Swap_OrderDetails = "api/swap/v3/orders/<instrument_id>/<order_id>";
		private const string Endpoints_Swap_TransactionDetails = "api/swap/v3/fills";
		private const string Endpoints_Swap_HoldAmount = "api/swap/v3/accounts/<instrument_id>/holds";
		private const string Endpoints_Swap_AccountTierRate = "api/swap/v3/trade_fee";
		private const string Endpoints_Swap_CloseAll = "api/swap/v3/close_position";
		private const string Endpoints_Swap_CancelAll = "api/swap/v3/cancel_all";
		private const string Endpoints_Swap_AlgoPlaceOrder = "api/swap/v3/order_algo";
		private const string Endpoints_Swap_AlgoCancelOrders = "api/swap/v3/cancel_algos";
		private const string Endpoints_Swap_AlgoOrderList = "api/swap/v3/order_algo/<instrument_id>";
		#endregion

		#region Public Unsigned Endpoints
		private const string Endpoints_Swap_TradingContracts = "api/swap/v3/instruments";
		private const string Endpoints_Swap_OrderBook = "api/swap/v3/instruments/<instrument_id>/depth";
		private const string Endpoints_Swap_Ticker = "api/swap/v3/instruments/ticker";
		private const string Endpoints_Swap_TickerOfSymbol = "api/swap/v3/instruments/<instrument_id>/ticker";
		private const string Endpoints_Swap_Trades = "api/swap/v3/instruments/<instrument_id>/trades";
		private const string Endpoints_Swap_Candles = "api/swap/v3/instruments/<instrument_id>/candles";
		private const string Endpoints_Swap_Indices = "api/swap/v3/instruments/<instrument_id>/index";
		private const string Endpoints_Swap_ExchangeRates = "api/swap/v3/rate";
		private const string Endpoints_Swap_OpenInterest = "api/swap/v3/instruments/<instrument_id>/open_interest";
		private const string Endpoints_Swap_PriceLimit = "api/swap/v3/instruments/<instrument_id>/price_limit";
		private const string Endpoints_Swap_LiquidatedOrders = "api/swap/v3/instruments/<instrument_id>/liquidation";
		private const string Endpoints_Swap_NextSettlementTime = "api/swap/v3/instruments/<instrument_id>/funding_time";
		private const string Endpoints_Swap_MarkPrice = "api/swap/v3/instruments/<instrument_id>/mark_price";
		private const string Endpoints_Swap_FundingRateHistory = "api/swap/v3/instruments/<instrument_id>/historical_funding_rate";
		private const string Endpoints_Swap_HistoricalMarketData = "api/swap/v3/instruments/<instrument_id>/history/candles";
		#endregion

		#endregion

		#region 07 - Options Trading API Endpoints

		#region Private Signed Endpoints
		private const string Endpoints_Options_Positions = "api/option/v3/<underlying>/position";
		private const string Endpoints_Options_Account = "api/option/v3/accounts/<underlying>";
		private const string Endpoints_Options_PlaceOrder = "api/option/v3/order";
		private const string Endpoints_Options_PlaceBatchOrders = "api/option/v3/orders";
		private const string Endpoints_Options_CancelOrder = "api/option/v3/cancel_order/<underlying>/<order_id>";
		private const string Endpoints_Options_CancelBatchOrders = "api/option/v3/cancel_batch_orders/<underlying>";
		private const string Endpoints_Options_CancelAll = "api/option/v3/cancel_all/<underlying>";
		private const string Endpoints_Options_MofiyOrder = "api/option/v3/amend_order/<underlying>";
		private const string Endpoints_Options_BatchModifyOrders = "api/option/v3/amend_batch_orders/<underlying>";
		private const string Endpoints_Options_OrderDetails = "api/option/v3/orders/<underlying>/<order_id>";
		private const string Endpoints_Options_OrderList = "api/option/v3/orders/<underlying>";
		private const string Endpoints_Options_TransactionDetails = "api/option/v3/fills/<underlying>";
		private const string Endpoints_Options_Bills = "api/option/v3/accounts/<underlying>/ledger";
		private const string Endpoints_Options_TradeFee = "api/option/v3/trade_fee";
		#endregion

		#region Public Unsigned Endpoints
		private const string Endpoints_Options_Underlying = "api/option/v3/underlying";
		private const string Endpoints_Options_Instruments = "api/option/v3/instruments/<underlying>";
		private const string Endpoints_Options_MarketData = "api/option/v3/instruments/<underlying>/summary";
		private const string Endpoints_Options_MarketDataOfInstrument = "api/option/v3/instruments/<underlying>/summary/<instrument_id>";
		private const string Endpoints_Options_OrderBook = "api/option/v3/instruments/<instrument_id>/book";
		private const string Endpoints_Options_Trades = "api/option/v3/instruments/<instrument_id>/trades";
		private const string Endpoints_Options_Ticker = "api/option/v3/instruments/<instrument_id>/ticker";
		private const string Endpoints_Options_Candles = "api/option/v3/instruments/<instrument_id>/candles";
		private const string Endpoints_Options_SettlementHistory = "api/option/v3/settlement/history/<underlying>";
		#endregion

		#endregion

		#region 08 - Contract Trading API Endpoints

		#region Public Unsigned Endpoints
		private const string Endpoints_Contract_LongShortRatio = "api/information/v3/<currency>/long_short_ratio";
		private const string Endpoints_Contract_Volume = "api/information/v3/<currency>/volume";
		private const string Endpoints_Contract_Taker = "api/information/v3/<currency>/taker";
		private const string Endpoints_Contract_Sentiment = "api/information/v3/<currency>/sentiment";
		private const string Endpoints_Contract_Margin = "api/information/v3/<currency>/margin";
		#endregion

		#endregion

		#region 09 - Index API Endpoints
		private const string Endpoints_Index_Constituents = "api/index/v3/<instrument_id>/constituents";
		#endregion

		#region 10 - Public-Oracle API Endpoints
		private const string Endpoints_Oracle = "api/market/v3/oracle";
		#endregion

		#endregion
	}
}