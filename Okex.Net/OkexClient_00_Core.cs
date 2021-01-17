namespace Okex.Net
{
    public partial class OkexClient
    {
        #region Endpoints

        #region 01 - General & System API Endpoints
        protected const string Endpoints_General_Time = "api/general/v3/time";
        protected const string Endpoints_System_Status = "api/system/v3/status";
        #endregion

        #region 02 - Funding Account API Endpoints
        protected const string Endpoints_Funding_GetAllBalances = "api/account/v3/wallet";
        protected const string Endpoints_Funding_GetSubAccount = "api/account/v3/sub-account";
        protected const string Endpoints_Funding_GetAssetValuation = "api/account/v3/asset-valuation";
        protected const string Endpoints_Funding_GetCurrencyBalance = "api/account/v3/wallet/<currency>";
        protected const string Endpoints_Funding_Transfer = "api/account/v3/transfer";
        protected const string Endpoints_Funding_Withdrawal = "api/account/v3/withdrawal";
        protected const string Endpoints_Funding_WithdrawalHistory = "api/account/v3/withdrawal/history";
        protected const string Endpoints_Funding_WithdrawalHistoryOfCurrency = "api/account/v3/withdrawal/history/<currency>";
        protected const string Endpoints_Funding_Bills = "api/account/v3/ledger";
        protected const string Endpoints_Funding_DepositAddress = "api/account/v3/deposit/address";
        protected const string Endpoints_Funding_DepositHistory = "api/account/v3/deposit/history";
        protected const string Endpoints_Funding_DepositHistoryOfCurrency = "api/account/v3/deposit/history/<currency>";
        protected const string Endpoints_Funding_GetCurrencies = "api/account/v3/currencies";
        protected const string Endpoints_Funding_GetUserID = "api/account/v3/uid";
        protected const string Endpoints_Funding_WithdrawalFees = "api/account/v3/withdrawal/fee";
        protected const string Endpoints_Funding_PiggyBank = "api/account/v3/purchase_redempt";
        #endregion

        #region 03 - Spot Trading API Endpoints

        #region Private Signed Endpoints
        protected const string Endpoints_Spot_Accounts = "api/spot/v3/accounts";
        protected const string Endpoints_Spot_AccountsOfCurrency = "api/spot/v3/accounts/<currency>";
        protected const string Endpoints_Spot_Bills = "api/spot/v3/accounts/<currency>/ledger";
        protected const string Endpoints_Spot_PlaceOrder = "api/spot/v3/orders";
        protected const string Endpoints_Spot_PlaceBatchOrders = "api/spot/v3/batch_orders";
        protected const string Endpoints_Spot_CancelOrder = "api/spot/v3/cancel_orders/<order_id>";
        protected const string Endpoints_Spot_CancelBatchOrders = "api/spot/v3/cancel_batch_orders";
        protected const string Endpoints_Spot_ModifyOrder = "api/spot/v3/amend_order/<instrument_id>";
        protected const string Endpoints_Spot_BatchModifyOrders = "api/spot/v3/amend_batch_orders/<instrument_id>";
        protected const string Endpoints_Spot_OrderList = "api/spot/v3/orders";
        protected const string Endpoints_Spot_OpenOrders = "api/spot/v3/orders_pending";
        protected const string Endpoints_Spot_OrderDetails = "api/spot/v3/orders/<order_id>";
        protected const string Endpoints_Spot_TradeFee = "api/spot/v3/trade_fee";
        protected const string Endpoints_Spot_TransactionDetails = "api/spot/v3/fills";
        protected const string Endpoints_Spot_AlgoPlaceOrder = "api/spot/v3/order_algo";
        protected const string Endpoints_Spot_AlgoCancelOrder = "api/spot/v3/cancel_batch_algos";
        protected const string Endpoints_Spot_AlgoOrderList = "api/spot/v3/algo";
        #endregion

        #region Public Unsigned Endpoints
        protected const string Endpoints_Spot_TradingPairs = "api/spot/v3/instruments";
        protected const string Endpoints_Spot_OrderBook = "api/spot/v3/instruments/<instrument_id>/book";
        protected const string Endpoints_Spot_TradingPairsTicker = "api/spot/v3/instruments/ticker";
        protected const string Endpoints_Spot_TradingPairsTickerOfSymbol = "api/spot/v3/instruments/<instrument_id>/ticker";
        protected const string Endpoints_Spot_Trades = "api/spot/v3/instruments/<instrument_id>/trades";
        protected const string Endpoints_Spot_Candles = "api/spot/v3/instruments/<instrument_id>/candles";
        protected const string Endpoints_Spot_HistoricalCandles = "api/spot/v3/instruments/<instrument_id>/history/candles";
        #endregion

        #endregion

        #region 04 - Margin Trading API Endpoints

        #region Private Signed Endpoints
        protected const string Endpoints_Margin_Accounts = "api/margin/v3/accounts";
        protected const string Endpoints_Margin_AccountsOfSymbol = "api/margin/v3/accounts/<instrument_id>";
        protected const string Endpoints_Margin_Bills = "api/margin/v3/accounts/<currency>/ledger";
        protected const string Endpoints_Margin_AccountSettings = "api/margin/v3/accounts/availability";
        protected const string Endpoints_Margin_AccountSettingsOfCurrency = "api/margin/v3/accounts/<instrument_id>/availability";
        protected const string Endpoints_Margin_LoanHistory = "api/margin/v3/accounts/borrowed";
        protected const string Endpoints_Margin_LoanHistoryOfCurrency = "api/margin/v3/accounts/<instrument_id>/borrowed";
        protected const string Endpoints_Margin_Loan = "api/margin/v3/accounts/borrow";
        protected const string Endpoints_Margin_Repayment = "api/margin/v3/accounts/repayment";
        protected const string Endpoints_Margin_PlaceOrder = "api/margin/v3/orders";
        protected const string Endpoints_Margin_PlaceBatchOrders = "api/margin/v3/batch_orders";
        protected const string Endpoints_Margin_CancelOrder = "api/margin/v3/cancel_orders/<order_id>";
        protected const string Endpoints_Margin_CancelBatchOrders = "api/margin/v3/cancel_batch_orders";
        protected const string Endpoints_Margin_OrderList = "api/margin/v3/orders";
        protected const string Endpoints_Margin_GetLeverage = "api/margin/v3/accounts/<instrument_id>/leverage";
        protected const string Endpoints_Margin_SetLeverage = "api/margin/v3/accounts/<instrument_id>/leverage";
        protected const string Endpoints_Margin_OrderDetails = "api/margin/v3/orders/<order_id>";
        protected const string Endpoints_Margin_OpenOrders = "api/margin/v3/orders_pending";
        protected const string Endpoints_Margin_TransactionDetails = "api/margin/v3/fills";
        #endregion

        #region Public Unsigned Endpoints
        protected const string Endpoints_Margin_MarkPrice = "api/margin/v3/instruments/<instrument_id>/mark_price";
        #endregion

        #endregion

        #region 05 - Futures Trading API Endpoints

        #region Private Signed Endpoints
        protected const string Endpoints_Futures_Positions = "api/futures/v3/position";
        protected const string Endpoints_Futures_PositionsOfContract = "api/futures/v3/<instrument_id>/position";
        protected const string Endpoints_Futures_Accounts = "api/futures/v3/accounts";
        protected const string Endpoints_Futures_AccountsOfCurrency = "api/futures/v3/accounts/<underlying>";
        protected const string Endpoints_Futures_GetFuturesLeverage = "api/futures/v3/accounts/<underlying>/leverage";
        protected const string Endpoints_Futures_SetFuturesLeverage = "api/futures/v3/accounts/<underlying>/leverage";
        protected const string Endpoints_Futures_Bills = "api/futures/v3/accounts/<underlying>/ledger";
        protected const string Endpoints_Futures_PlaceOrder = "api/futures/v3/order";
        protected const string Endpoints_Futures_BatchPlaceOrders = "api/futures/v3/orders";
        protected const string Endpoints_Futures_ModifyOrder = "api/futures/v3/amend_order/<instrument_id>";
        protected const string Endpoints_Futures_BatchModifyOrders = "api/futures/v3/amend_batch_orders/<instrument_id>";
        protected const string Endpoints_Futures_CancelOrder = "api/futures/v3/cancel_order/<instrument_id>/<order_id>";
        protected const string Endpoints_Futures_BatchCancelOrders = "api/futures/v3/cancel_batch_orders/<instrument_id>";
        protected const string Endpoints_Futures_OrderList = "api/futures/v3/orders/<instrument_id>";
        protected const string Endpoints_Futures_OrderDetails = "api/futures/v3/orders/<instrument_id>/<order_id>";
        protected const string Endpoints_Futures_TransactionDetails = "api/futures/v3/fills";
        protected const string Endpoints_Futures_SetAccountMode = "api/futures/v3/accounts/margin_mode";
        protected const string Endpoints_Futures_TradeFee = "api/futures/v3/trade_fee";
        protected const string Endpoints_Futures_MarketCloseAll = "api/futures/v3/close_position";
        protected const string Endpoints_Futures_CancelAll = "api/futures/v3/cancel_all";
        protected const string Endpoints_Futures_HoldAmount = "api/futures/v3/accounts/<instrument_id>/holds";
        protected const string Endpoints_Futures_AlgoPlaceOrder = "api/futures/v3/order_algo";
        protected const string Endpoints_Futures_AlgoCancelOrder = "api/futures/v3/cancel_algos";
        protected const string Endpoints_Futures_AlgoOrderList = "api/futures/v3/order_algo/<instrument_id>";
        protected const string Endpoints_Futures_IncreaseDecreaseMargin = "api/futures/v3/position/margin";
        protected const string Endpoints_Futures_SetMarginAutomatically = "api/futures/v3/accounts/auto_margin";
        #endregion

        #region Public Unsigned Endpoints
        protected const string Endpoints_Futures_TradingContracts = "api/futures/v3/instruments";
        protected const string Endpoints_Futures_OrderBook = "api/futures/v3/instruments/<instrument_id>/book";
        protected const string Endpoints_Futures_TradingContractsTicker = "api/futures/v3/instruments/ticker";
        protected const string Endpoints_Futures_TradingContractsTickerOfSymbol = "api/futures/v3/instruments/<instrument_id>/ticker";
        protected const string Endpoints_Futures_Trades = "api/futures/v3/instruments/<instrument_id>/trades";
        protected const string Endpoints_Futures_Candles = "api/futures/v3/instruments/<instrument-id>/candles";
        protected const string Endpoints_Futures_Indices = "api/futures/v3/instruments/<instrument_id>/index";
        protected const string Endpoints_Futures_ExchangeRates = "api/futures/v3/rate";
        protected const string Endpoints_Futures_EstimatedDeliveryPrice = "api/futures/v3/instruments/<instrument_id>/estimated_price";
        protected const string Endpoints_Futures_OpenInterest = "api/futures/v3/instruments/<instrument_id>/open_interest";
        protected const string Endpoints_Futures_PriceLimit = "api/futures/v3/instruments/<instrument_id>/price_limit";
        protected const string Endpoints_Futures_LiquidatedOrders = "api/futures/v3/instruments/<instrument_id>/liquidation";
        protected const string Endpoints_Futures_SettlementHistory = "api/futures/v3/settlement/history";
        protected const string Endpoints_Futures_MarkPrice = "api/futures/v3/instruments/<instrument_id>/mark_price";
        protected const string Endpoints_Futures_HistoricalMarketData = "api/futures/v3/instruments/<instrument_id>/history/candles";
        #endregion

        #endregion

        #region 06 - Perpetual Swap API Endpoints

        #region Private Signed Endpoints
        protected const string Endpoints_Swap_Positions = "api/swap/v3/position";
        protected const string Endpoints_Swap_PositionsOfContract = "api/swap/v3/<instrument_id>/position";
        protected const string Endpoints_Swap_Accounts = "api/swap/v3/accounts";
        protected const string Endpoints_Swap_AccountsOfCurrency = "api/swap/v3/<instrument_id>/accounts";
        protected const string Endpoints_Swap_GetSwapLeverage = "api/swap/v3/accounts/<instrument_id>/settings";
        protected const string Endpoints_Swap_SetSwapLeverage = "api/swap/v3/accounts/<instrument_id>/leverage";
        protected const string Endpoints_Swap_Bills = "api/swap/v3/accounts/<instrument_id>/ledger";
        protected const string Endpoints_Swap_PlaceOrder = "api/swap/v3/order";
        protected const string Endpoints_Swap_BatchPlaceOrders = "api/swap/v3/orders";
        protected const string Endpoints_Swap_CancelOrder = "api/swap/v3/cancel_order/<instrument_id>/<order_id>";
        protected const string Endpoints_Swap_BatchCancelOrders = "api/swap/v3/cancel_batch_orders/<instrument_id>";
        protected const string Endpoints_Swap_ModifyOrder = "api/swap/v3/amend_order/<instrument_id>";
        protected const string Endpoints_Swap_BatchModifyOrder = "api/swap/v3/amend_batch_orders/<instrument_id>";
        protected const string Endpoints_Swap_OrderList = "api/swap/v3/orders/<instrument_id>";
        protected const string Endpoints_Swap_OrderDetails = "api/swap/v3/orders/<instrument_id>/<order_id>";
        protected const string Endpoints_Swap_TransactionDetails = "api/swap/v3/fills";
        protected const string Endpoints_Swap_HoldAmount = "api/swap/v3/accounts/<instrument_id>/holds";
        protected const string Endpoints_Swap_AccountTierRate = "api/swap/v3/trade_fee";
        protected const string Endpoints_Swap_CloseAll = "api/swap/v3/close_position";
        protected const string Endpoints_Swap_CancelAll = "api/swap/v3/cancel_all";
        protected const string Endpoints_Swap_AlgoPlaceOrder = "api/swap/v3/order_algo";
        protected const string Endpoints_Swap_AlgoCancelOrders = "api/swap/v3/cancel_algos";
        protected const string Endpoints_Swap_AlgoOrderList = "api/swap/v3/order_algo/<instrument_id>";
        #endregion

        #region Public Unsigned Endpoints
        protected const string Endpoints_Swap_TradingContracts = "api/swap/v3/instruments";
        protected const string Endpoints_Swap_OrderBook = "api/swap/v3/instruments/<instrument_id>/depth";
        protected const string Endpoints_Swap_Ticker = "api/swap/v3/instruments/ticker";
        protected const string Endpoints_Swap_TickerOfSymbol = "api/swap/v3/instruments/<instrument_id>/ticker";
        protected const string Endpoints_Swap_Trades = "api/swap/v3/instruments/<instrument_id>/trades";
        protected const string Endpoints_Swap_Candles = "api/swap/v3/instruments/<instrument_id>/candles";
        protected const string Endpoints_Swap_Indices = "api/swap/v3/instruments/<instrument_id>/index";
        protected const string Endpoints_Swap_ExchangeRates = "api/swap/v3/rate";
        protected const string Endpoints_Swap_OpenInterest = "api/swap/v3/instruments/<instrument_id>/open_interest";
        protected const string Endpoints_Swap_PriceLimit = "api/swap/v3/instruments/<instrument_id>/price_limit";
        protected const string Endpoints_Swap_LiquidatedOrders = "api/swap/v3/instruments/<instrument_id>/liquidation";
        protected const string Endpoints_Swap_NextSettlementTime = "api/swap/v3/instruments/<instrument_id>/funding_time";
        protected const string Endpoints_Swap_MarkPrice = "api/swap/v3/instruments/<instrument_id>/mark_price";
        protected const string Endpoints_Swap_FundingRateHistory = "api/swap/v3/instruments/<instrument_id>/historical_funding_rate";
        protected const string Endpoints_Swap_HistoricalMarketData = "api/swap/v3/instruments/<instrument_id>/history/candles";
        #endregion

        #endregion

        #region 07 - Options Trading API Endpoints

        #region Private Signed Endpoints
        protected const string Endpoints_Options_Positions = "api/option/v3/<underlying>/position";
        protected const string Endpoints_Options_Account = "api/option/v3/accounts/<underlying>";
        protected const string Endpoints_Options_PlaceOrder = "api/option/v3/order";
        protected const string Endpoints_Options_PlaceBatchOrders = "api/option/v3/orders";
        protected const string Endpoints_Options_CancelOrder = "api/option/v3/cancel_order/<underlying>/<order_id>";
        protected const string Endpoints_Options_CancelBatchOrders = "api/option/v3/cancel_batch_orders/<underlying>";
        protected const string Endpoints_Options_CancelAll = "api/option/v3/cancel_all/<underlying>";
        protected const string Endpoints_Options_MofiyOrder = "api/option/v3/amend_order/<underlying>";
        protected const string Endpoints_Options_BatchModifyOrders = "api/option/v3/amend_batch_orders/<underlying>";
        protected const string Endpoints_Options_OrderDetails = "api/option/v3/orders/<underlying>/<order_id>";
        protected const string Endpoints_Options_OrderList = "api/option/v3/orders/<underlying>";
        protected const string Endpoints_Options_TransactionDetails = "api/option/v3/fills/<underlying>";
        protected const string Endpoints_Options_Bills = "api/option/v3/accounts/<underlying>/ledger";
        protected const string Endpoints_Options_TradeFee = "api/option/v3/trade_fee";
        #endregion

        #region Public Unsigned Endpoints
        protected const string Endpoints_Options_Underlying = "api/option/v3/underlying";
        protected const string Endpoints_Options_Instruments = "api/option/v3/instruments/<underlying>";
        protected const string Endpoints_Options_MarketData = "api/option/v3/instruments/<underlying>/summary";
        protected const string Endpoints_Options_MarketDataOfInstrument = "api/option/v3/instruments/<underlying>/summary/<instrument_id>";
        protected const string Endpoints_Options_OrderBook = "api/option/v3/instruments/<instrument_id>/book";
        protected const string Endpoints_Options_Trades = "api/option/v3/instruments/<instrument_id>/trades";
        protected const string Endpoints_Options_Ticker = "api/option/v3/instruments/<instrument_id>/ticker";
        protected const string Endpoints_Options_Candles = "api/option/v3/instruments/<instrument_id>/candles";
        protected const string Endpoints_Options_SettlementHistory = "api/option/v3/settlement/history/<underlying>";
        #endregion

        #endregion

        #region 08 - Contract Trading API Endpoints

        #region Public Unsigned Endpoints
        protected const string Endpoints_Contract_LongShortRatio = "api/information/v3/<currency>/long_short_ratio";
        protected const string Endpoints_Contract_Volume = "api/information/v3/<currency>/volume";
        protected const string Endpoints_Contract_Taker = "api/information/v3/<currency>/taker";
        protected const string Endpoints_Contract_Sentiment = "api/information/v3/<currency>/sentiment";
        protected const string Endpoints_Contract_Margin = "api/information/v3/<currency>/margin";
        #endregion

        #endregion

        #region 09 - Index API Endpoints
        protected const string Endpoints_Index_Constituents = "api/index/v3/<instrument_id>/constituents";
        #endregion

        #region 10 - Public-Oracle API Endpoints
        protected const string Endpoints_Oracle = "api/market/v3/oracle";
        #endregion

        #endregion
    }
}