using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using Okex.Net.Converters;
using Okex.Net.RestObjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Okex.Net.Interfaces;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace Okex.Net
{
	/// <summary>
	/// Client for the Okex REST API
	/// </summary>
	public class OkexClient : RestClient, IOkexClient
	{
		#region Fields

		#region Client Options
		private static OkexClientOptions defaultOptions = new OkexClientOptions();
		private static OkexClientOptions DefaultOptions => defaultOptions.Copy();

		/// <summary>
		/// Whether public requests should be signed if ApiCredentials are provided. Needed for accurate rate limiting.
		/// </summary>
		public bool SignPublicRequests { get; }
		#endregion

		#region General API Endpoints
		private const string Endpoints_General_Time = "api/general/v3/time";
		#endregion

		#region Funding Account API Endpoints
		private const string Endpoints_Funding_GetAllBalances = "api/account/v3/wallet";
		private const string Endpoints_Funding_GetCurrencyBalance = "api/account/v3/wallet/<currency>";
		private const string Endpoints_Funding_GetAssetValuation = "api/account/v3/asset-valuation";
		private const string Endpoints_Funding_GetSubAccount = "api/account/v3/sub-account";
		private const string Endpoints_Funding_Transfer = "api/account/v3/transfer";
		private const string Endpoints_Funding_Bills = "api/account/v3/ledger";
		private const string Endpoints_Funding_GetCurrencies = "api/account/v3/currencies";
		private const string Endpoints_Funding_Withdrawal = "api/account/v3/withdrawal";
		private const string Endpoints_Funding_WithdrawalFees = "api/account/v3/withdrawal/fee";
		private const string Endpoints_Funding_WithdrawalHistory = "api/account/v3/withdrawal/history";
		private const string Endpoints_Funding_WithdrawalHistoryCurrency = "api/account/v3/withdrawal/history/<currency>";
		private const string Endpoints_Funding_DepositAddress = "api/account/v3/deposit/address";
		private const string Endpoints_Funding_DepositHistory = "api/account/v3/deposit/history";
		private const string Endpoints_Funding_DepositHistoryCurrency = "api/account/v3/deposit/history/<currency>";
		#endregion

		#region Spot Trading API Endpoints
		private const string Endpoints_Spot_TradingPairs = "api/spot/v3/instruments"; // Public
		private const string Endpoints_Spot_OrderBook = "api/spot/v3/instruments/<instrument_id>/book"; // Public
		private const string Endpoints_Spot_TradingPairsTicker = "api/spot/v3/instruments/ticker"; // Public
		private const string Endpoints_Spot_TradingPairTicker = "api/spot/v3/instruments/<instrument_id>/ticker"; // Public
		private const string Endpoints_Spot_Trades = "api/spot/v3/instruments/<instrument_id>/trades"; // Public
		private const string Endpoints_Spot_Candles = "api/spot/v3/instruments/<instrument_id>/candles"; // Public
		private const string Endpoints_Spot_Accounts = "api/spot/v3/accounts";
		private const string Endpoints_Spot_Account = "api/spot/v3/accounts/<currency>";
		private const string Endpoints_Spot_Bills = "api/spot/v3/accounts/<currency>/ledger";
		private const string Endpoints_Spot_PlaceOrder = "api/spot/v3/orders";
		// TODO: Spot Batch Orders
		private const string Endpoints_Spot_PlaceBatchOrders = "api/spot/v3/batch_orders";
		private const string Endpoints_Spot_CancelOrder = "api/spot/v3/cancel_orders/<order_id>";
		// TODO: Cancel Batch Orders
		private const string Endpoints_Spot_CancelBatchOrders = "api/spot/v3/cancel_batch_orders";
		private const string Endpoints_Spot_OrderList = "api/spot/v3/orders";
		private const string Endpoints_Spot_OpenOrders = "api/spot/v3/orders_pending";
		private const string Endpoints_Spot_OrderDetails = "api/spot/v3/orders/<order_id>";
		private const string Endpoints_Spot_TradeFee = "api/spot/v3/trade_fee";
		// TODO: Transaction Details
		private const string Endpoints_Spot_TransactionDetails = "api/spot/v3/fills";
		// TODO: Place Algo Order
		private const string Endpoints_Spot_PlaceAlgoOrder = "api/spot/v3/order_algo";
		// TODO: Cancel Algo Order
		private const string Endpoints_Spot_CancelBatchAlgoOrders = "api/spot/v3/cancel_batch_algos";
		// TODO: Get Algo Order List
		private const string Endpoints_Spot_AlgoOrderList = "api/spot/v3/algo";
		#endregion

		#region Margin Trading API Endpoints
		private const string Endpoints_Margin_TradingPairs = "api/margin/v3/instruments"; // Public
		private const string Endpoints_Margin_OrderBook = "api/margin/v3/instruments/<instrument_id>/book"; // Public
		private const string Endpoints_Margin_TradingPairsTicker = "api/margin/v3/instruments/ticker"; // Public
		private const string Endpoints_Margin_TradingPairTicker = "api/margin/v3/instruments/<instrument_id>/ticker"; // Public
		private const string Endpoints_Margin_Trades = "api/margin/v3/instruments/<instrument_id>/trades"; // Public
		private const string Endpoints_Margin_Candles = "api/margin/v3/instruments/<instrument_id>/candles"; // Public
		private const string Endpoints_Margin_Accounts = "api/margin/v3/accounts";
		private const string Endpoints_Margin_Account = "api/margin/v3/accounts/<instrument_id>";
		private const string Endpoints_Margin_Bills = "api/margin/v3/accounts/<currency>/ledger";
		private const string Endpoints_Margin_PlaceOrder = "api/margin/v3/orders";
		private const string Endpoints_Margin_PlaceBatchOrders = "api/margin/v3/batch_orders";
		private const string Endpoints_Margin_CancelOrder = "api/margin/v3/cancel_orders/<order_id>";
		private const string Endpoints_Margin_CancelBatchOrders = "api/margin/v3/cancel_batch_orders";
		private const string Endpoints_Margin_OrderList = "api/margin/v3/orders";
		private const string Endpoints_Margin_OpenOrders = "api/margin/v3/orders_pending";
		private const string Endpoints_Margin_OrderDetails = "api/margin/v3/orders/<order_id>";
		private const string Endpoints_Margin_TradeFee = "api/margin/v3/trade_fee";
		private const string Endpoints_Margin_TransactionDetails = "api/margin/v3/fills";
		private const string Endpoints_Margin_PlaceAlgoOrder = "api/margin/v3/order_algo";
		private const string Endpoints_Margin_CancelBatchAlgoOrders = "api/margin/v3/cancel_batch_algos";
		private const string Endpoints_Margin_AlgoOrderList = "api/margin/v3/algo";
		#endregion

		#region Futures Trading API Endpoints
		private const string Endpoints_Futures_TradingContracts = "api/futures/v3/instruments"; // Public
		private const string Endpoints_Futures_OrderBook = "api/futures/v3/instruments/<instrument_id>/book"; // Public
		private const string Endpoints_Futures_TradingContractsTicker = "api/futures/v3/instruments/ticker"; // Public
		private const string Endpoints_Futures_TradingContractTicker = "api/futures/v3/instruments/<instrument_id>/ticker"; // Public
		private const string Endpoints_Futures_Trades = "api/futures/v3/instruments/<instrument_id>/trades"; // Public
		private const string Endpoints_Futures_Candles = "api/futures/v3/instruments/<instrument-id>/candles"; // Public
		private const string Endpoints_Futures_Indices = "api/futures/v3/instruments/<instrument_id>/index"; // Public
		private const string Endpoints_Futures_ExchangeRates = "api/futures/v3/rate"; // Public
		private const string Endpoints_Futures_EstimatedDeliveryPrice = "api/futures/v3/instruments/estimated_price"; // Public
		private const string Endpoints_Futures_OpenInterest = "api/futures/v3/instruments/<instrument_id>/open_interest"; // Public
		private const string Endpoints_Futures_PriceLimit = "api/futures/v3/instruments/<instrument_id>/price_limit"; // Public
		private const string Endpoints_Futures_MarkPrice = "api/futures/v3/instruments/<instrument_id>/mark_price"; // Public
		private const string Endpoints_Futures_LiquidatedOrders = "api/futures/v3/instruments/<instrument_id>/liquidation"; // Public
		private const string Endpoints_Futures_Positions = "api/futures/v3/position";
		private const string Endpoints_Futures_ContractPositions = "api/futures/v3/<instrument_id>/position";
		private const string Endpoints_Futures_Accounts = "api/futures/v3/accounts";
		private const string Endpoints_Futures_Account = "api/futures/v3/accounts/<underlying>";
		private const string Endpoints_Futures_GetFuturesLeverage = "api/futures/v3/accounts/<underlying>/leverage";
		private const string Endpoints_Futures_SetFuturesLeverage = "api/futures/v3/accounts/<underlying>/leverage";
		private const string Endpoints_Futures_Bills = "api/futures/v3/accounts/<underlying>/ledger";
		private const string Endpoints_Futures_PlaceOrder = "api/futures/v3/order";
		private const string Endpoints_Futures_PlaceBatchOrders = "api/futures/v3/orders";
		private const string Endpoints_Futures_CancelOrder = "api/futures/v3/cancel_order/<instrument_id>/<order_id>";
		private const string Endpoints_Futures_CancelBatchOrders = "api/futures/v3/cancel_batch_orders/<instrument_id>";
		private const string Endpoints_Futures_OrderList = "api/futures/v3/orders/<instrument_id>";
		private const string Endpoints_Futures_OrderDetails = "api/futures/v3/orders/<instrument_id>/<order_id>";
		private const string Endpoints_Futures_TransactionDetails = "api/futures/v3/fills";
		private const string Endpoints_Futures_SetAccountMode = "api/futures/v3/accounts/margin_mode";
		private const string Endpoints_Futures_MarketCloseAll = "api/futures/v3/close_position";
		private const string Endpoints_Futures_TradeFee = "api/futures/v3/trade_fee";
		private const string Endpoints_Futures_CancelAllPositionReductionOrders = "api/futures/v3/cancel_all";
		private const string Endpoints_Futures_HoldAmount = "api/futures/v3/accounts/<instrument_id>/holds";
		private const string Endpoints_Futures_PlaceAlgoOrder = "api/futures/v3/order_algo";
		private const string Endpoints_Futures_CancelBatchAlgoOrders = "api/futures/v3/cancel_algos";
		private const string Endpoints_Futures_AlgoOrderList = "api/futures/v3/order_algo/<instrument_id>";
		#endregion

		#region Swap API Endpoints
		private const string Endpoints_Swap_TradingContracts = "api/swap/v3/instruments"; // Public
		private const string Endpoints_Swap_OrderBook = "api/swap/v3/instruments/<instrument_id>/depth"; // Public
		private const string Endpoints_Swap_TradingContractsTicker = "api/swap/v3/instruments/ticker"; // Public
		private const string Endpoints_Swap_TradingContractTicker = "api/swap/v3/instruments/<instrument_id>/ticker"; // Public
		private const string Endpoints_Swap_Trades = "api/swap/v3/instruments/<instrument_id>/trades"; // Public
		private const string Endpoints_Swap_Candles = "api/swap/v3/instruments/<instrument_id>/candles"; // Public
		private const string Endpoints_Swap_Indices = "api/swap/v3/instruments/<instrument_id>/index"; // Public
		private const string Endpoints_Swap_ExchangeRates = "api/swap/v3/rate"; // Public
		private const string Endpoints_Swap_OpenInterest = "api/swap/v3/instruments/<instrument_id>/open_interest"; // Public
		private const string Endpoints_Swap_PriceLimit = "api/swap/v3/instruments/<instrument_id>/price_limit"; // Public
		private const string Endpoints_Swap_LiquidatedOrders = "api/swap/v3/instruments/<instrument_id>/liquidation"; // Public
		private const string Endpoints_Swap_NextSettlementTime = "api/swap/v3/instruments/<instrument_id>/funding_time"; // Public
		private const string Endpoints_Swap_MarkPrice = "api/swap/v3/instruments/<instrument_id>/mark_price"; // Public
		private const string Endpoints_Swap_FundingRateHistory = "api/swap/v3/instruments/<instrument_id>/historical_funding_rate"; // Public
		private const string Endpoints_Swap_Positions = "api/swap/v3/position";
		private const string Endpoints_Swap_ContractPositions = "api/swap/v3/<instrument_id>/position";
		private const string Endpoints_Swap_Accounts = "api/swap/v3/accounts";
		private const string Endpoints_Swap_Account = "api/swap/v3/<instrument_id>/accounts";
		private const string Endpoints_Swap_GetSwapLeverage = "api/swap/v3/accounts/<instrument_id>/leverage";
		private const string Endpoints_Swap_SetSwapLeverage = "api/swap/v3/accounts/<instrument_id>/leverage";
		private const string Endpoints_Swap_Bills = "api/swap/v3/accounts/<instrument_id>/ledger";
		private const string Endpoints_Swap_PlaceOrder = "api/swap/v3/order";
		private const string Endpoints_Swap_PlaceBatchOrders = "api/swap/v3/orders";
		private const string Endpoints_Swap_CancelOrder = "api/swap/v3/cancel_order/<instrument_id>/<order_id>";
		private const string Endpoints_Swap_CancelBatchOrders = "api/swap/v3/cancel_batch_orders/<instrument_id>";
		private const string Endpoints_Swap_OrderList = "api/swap/v3/orders/<instrument_id>";
		private const string Endpoints_Swap_OrderDetails = "api/swap/v3/orders/<instrument_id>/<order_id>";
		private const string Endpoints_Swap_TransactionDetails = "api/swap/v3/fills";
		private const string Endpoints_Swap_HoldAmount = "api/swap/v3/accounts/<instrument_id>/holds";
		private const string Endpoints_Swap_TradeFee = "api/swap/v3/trade_fee";
		private const string Endpoints_Swap_PlaceAlgoOrder = "api/swap/v3/order_algo";
		private const string Endpoints_Swap_CancelBatchAlgoOrders = "api/swap/v3/cancel_algos";
		private const string Endpoints_Swap_AlgoOrderList = "api/swap/v3/order_algo/<instrument_id>";
		#endregion

		#region Options Trading API Endpoints
		private const string Endpoints_Options_UnderlyingIndex = "api/option/v3/underlying"; // Public
		private const string Endpoints_Options_OptionInstruments = "api/option/v3/instruments/<underlying>"; // Public
		private const string Endpoints_Options_OptionMarketData = "api/option/v3/instruments/<underlying>/summary"; // Public
		private const string Endpoints_Options_OptionInstrumentMarketData = "api/option/v3/instruments/<underlying>/summary/<instrument_id>"; // Public
		private const string Endpoints_Options_OrderBook = "api/option/v3/instruments/<instrument_id>/book"; // Public
		private const string Endpoints_Options_Trades = "api/option/v3/instruments/<instrument_id>/trades"; // Public
		private const string Endpoints_Options_OptionInstrumentTicker = "api/option/v3/instruments/<instrument_id>/ticker"; // Public
		private const string Endpoints_Options_Candles = "api/option/v3/instruments/<instrument_id>/candles"; // Public
		private const string Endpoints_Options_Positions = "api/option/v3/<underlying>/position";
		private const string Endpoints_Options_Account = "api/option/v3/accounts/<underlying>";
		private const string Endpoints_Options_PlaceOrder = "api/option/v3/order";
		private const string Endpoints_Options_PlaceBatchOrders = "api/option/v3/orders";
		private const string Endpoints_Options_CancelOrder = "api/option/v3/cancel_order/<underlying>/<order_id>";
		private const string Endpoints_Options_CancelBatchOrders = "api/option/v3/cancel_batch_orders/<underlying>";
		private const string Endpoints_Options_MofiyOrder = "api/option/v3/amend_order/<underlying>";
		private const string Endpoints_Options_BatchModifyOrders = "api/option/v3/amend_batch_orders/<underlying>";
		private const string Endpoints_Options_OrderDetails = "api/option/v3/orders/<underlying>/<order_id>";
		private const string Endpoints_Options_OrderList = "api/option/v3/orders/<underlying>";
		private const string Endpoints_Options_TransactionDetails = "api/option/v3/fills/<underlying>";
		private const string Endpoints_Options_Bills = "api/option/v3/accounts/<underlying>/ledger";
		private const string Endpoints_Options_TradeFee = "api/option/v3/trade_fee";
		#endregion

		#region Index API Endpoints
		private const string Endpoints_Index_Constituents = "api/index/v3/<instrument_id>/constituents";
		#endregion

		#endregion

		#region Constructor/Destructor
		/// <summary>
		/// Create a new instance of OkexClient using the default options
		/// </summary>
		public OkexClient() : this(DefaultOptions)
		{
		}

		/// <summary>
		/// Create a new instance of the OkexClient with the provided options
		/// </summary>
		public OkexClient(OkexClientOptions options) : base(options, options.ApiCredentials == null ? null : new OkexAuthenticationProvider(options.ApiCredentials, "", options.SignPublicRequests, ArrayParametersSerialization.Array))
		{
			this.SignPublicRequests = options.SignPublicRequests;
		}
		#endregion

		#region Common Methods
		/// <summary>
		/// Sets the default options to use for new clients
		/// </summary>
		/// <param name="options">The options to use for new clients</param>
		public static void SetDefaultOptions(OkexClientOptions options)
		{
			defaultOptions = options;
		}

		/// <summary>
		/// Set the API key and secret
		/// </summary>
		/// <param name="apiKey">The api key</param>
		/// <param name="apiSecret">The api secret</param>
		/// <param name="passPhrase">The passphrase you specified when creating the API key</param>
		public void SetApiCredentials(string apiKey, string apiSecret, string passPhrase)
		{
			SetAuthenticationProvider(new OkexAuthenticationProvider(new ApiCredentials(apiKey, apiSecret), passPhrase, SignPublicRequests, ArrayParametersSerialization.Array));
		}
		#endregion

		#region General API
		/// API server time. This is a public endpoint, no verification is required.
		/// </summary>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public WebCallResult<RestObjects.General.ServerTime> General_ServerTime(CancellationToken ct = default) => General_ServerTime_Async(ct).Result;
		/// <summary>
		/// API server time. This is a public endpoint, no verification is required.
		/// </summary>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public async Task<WebCallResult<RestObjects.General.ServerTime>> General_ServerTime_Async(CancellationToken ct = default)
		{
			return await SendRequest<RestObjects.General.ServerTime>(GetUrl(Endpoints_General_Time), HttpMethod.Get, ct).ConfigureAwait(false);
		}
		#endregion

		#region Funding Account API
		/// <summary>
		/// This retrieves information on the balances of all the assets, and the amount that is available or on hold.
		/// Limit: 20 requests per 2 seconds
		/// </summary>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public WebCallResult<IEnumerable<RestObjects.Funding.AssetBalance>> Funding_GetAllBalances(CancellationToken ct = default) => Funding_GetAllBalances_Async(ct).Result;
		/// <summary>
		/// This retrieves information on the balances of all the assets, and the amount that is available or on hold.
		/// Limit: 20 requests per 2 seconds
		/// </summary>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public async Task<WebCallResult<IEnumerable<RestObjects.Funding.AssetBalance>>> Funding_GetAllBalances_Async(CancellationToken ct = default)
		{
			return await SendRequest<IEnumerable<RestObjects.Funding.AssetBalance>>(GetUrl(Endpoints_Funding_GetAllBalances), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
		}


		/// <summary>
		/// This retrieves information for a single token in your account, including the remaining balance, and the amount available or on hold.
		/// </summary>
		/// <param name="currency">Token symbol, e.g. 'BTC'</param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public WebCallResult<IEnumerable<RestObjects.Funding.AssetBalance>> Funding_GetCurrencyBalance(string currency, CancellationToken ct = default) => Funding_GetBalance_Async(currency, ct).Result;
		/// <summary>
		/// This retrieves information for a single token in your account, including the remaining balance, and the amount available or on hold.
		/// </summary>
		/// <param name="currency">Token symbol, e.g. 'BTC'</param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public async Task<WebCallResult<IEnumerable<RestObjects.Funding.AssetBalance>>> Funding_GetBalance_Async(string currency, CancellationToken ct = default)
		{
			currency = currency.ValidateCurrency();
			return await SendRequest<IEnumerable<RestObjects.Funding.AssetBalance>>(GetUrl(Endpoints_Funding_GetCurrencyBalance, currency), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
		}


		/// <summary>
		/// Get the valuation of the total assets of the account in btc or fiat currency.
		/// Limit: 1 requests per 20 seconds
		/// </summary>
		/// <param name="accountType">Line of Business Type。0.Total account assets 1.spot 3.futures 4.C2C 5.margin 6.Funding Account 8. PiggyBank 9.swap 12：option 14.mining account Query total assets by default</param>
		/// <param name="valuationCurrency">The valuation according to a certain fiat currency can only be one of the following "BTC USD CNY JPY KRW RUB" The default unit is BTC</param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public WebCallResult<RestObjects.Funding.AssetValuation> Funding_GetAssetValuation(FundingAccountType accountType = FundingAccountType.TotalAccountAssets, string valuationCurrency = "BTC", CancellationToken ct = default) => Funding_GetAssetValuation_Async(accountType, valuationCurrency, ct).Result;
		/// <summary>
		/// Get the valuation of the total assets of the account in btc or fiat currency.
		/// Limit: 1 requests per 20 seconds
		/// </summary>
		/// <param name="accountType">Line of Business Type。0.Total account assets 1.spot 3.futures 4.C2C 5.margin 6.Funding Account 8. PiggyBank 9.swap 12：option 14.mining account Query total assets by default</param>
		/// <param name="valuationCurrency">The valuation according to a certain fiat currency can only be one of the following "BTC USD CNY JPY KRW RUB" The default unit is BTC</param>
		/// <param name="ct">Cancellation Token</param>
		public async Task<WebCallResult<RestObjects.Funding.AssetValuation>> Funding_GetAssetValuation_Async(FundingAccountType accountType = FundingAccountType.TotalAccountAssets, string valuationCurrency = "BTC", CancellationToken ct = default)
		{
			valuationCurrency = valuationCurrency.ValidateCurrency();

			if (string.IsNullOrEmpty(valuationCurrency) || valuationCurrency.IsNotOneOf("BTC", "USD", "CNY", "JPY", "KRW", "RUB"))
				throw new ArgumentException("The valuation according to a certain fiat currency can only be one of the following BTC, USD, CNY, JPY, KRW, RUB");

			var parameters = new Dictionary<string, object>
			{
				{ "account_type", JsonConvert.SerializeObject(accountType, new FundingAccountTypeConverter(false)) },
				{ "valuation_currency", valuationCurrency },
			};

			return await SendRequest<RestObjects.Funding.AssetValuation>(GetUrl(Endpoints_Funding_GetAssetValuation), HttpMethod.Get, ct, parameters, signed: true).ConfigureAwait(false);
		}


		/// <summary>
		/// The account obtains the fund balance information in each account of the sub account
		/// Limit: 1 requests per 20 seconds
		/// </summary>
		/// <param name="subAccountName">Sub Account Name</param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public WebCallResult<RestObjects.Funding.SubAccount> Funding_GetSubAccount(string subAccountName, CancellationToken ct = default) => Funding_GetSubAccount_Async(subAccountName, ct).Result;
		/// <summary>
		/// The account obtains the fund balance information in each account of the sub account
		/// Limit: 1 requests per 20 seconds
		/// </summary>
		/// <param name="subAccountName">Sub Account Name</param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public async Task<WebCallResult<RestObjects.Funding.SubAccount>> Funding_GetSubAccount_Async(string subAccountName, CancellationToken ct = default)
		{
			var parameters = new Dictionary<string, object>
			{
				{ "sub-account", subAccountName },
			};

			return await SendRequest<RestObjects.Funding.SubAccount>(GetUrl(Endpoints_Funding_GetSubAccount), HttpMethod.Get, ct, parameters, signed: true).ConfigureAwait(false);
		}


		/// <summary>
		/// This endpoint supports the transfer of funds among your funding account, trading accounts, main account, and sub accounts.
		/// Limit: 1 request per 2 seconds (per currency)
		/// </summary>
		/// <param name="currency">Token symbol, e.g., 'EOS'</param>
		/// <param name="amount">Amount to be transferred</param>
		/// <param name="fromAccount">Remitting account</param>
		/// <param name="toAccount">Receiving account</param>
		/// <param name="subAccountName">Name of the sub account</param>
		/// <param name="fromSymbol">Margin trading pair of token or underlying of USDT-margined futures transferred out, such as: btc-usdt. Limited to trading pairs available for margin trading or underlying of enabled futures trading.</param>
		/// <param name="toSymbol">Margin trading pair of token or underlying of USDT-margined futures transferred in, such as: btc-usdt. Limited to trading pairs available for margin trading or underlying of enabled futures trading.</param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public WebCallResult<RestObjects.Funding.AssetTransfer> Funding_Transfer(
			string currency,
			decimal amount,
			FundingTransferAccountType fromAccount,
			FundingTransferAccountType toAccount,
			string? subAccountName = null,
			string? fromSymbol = null,
			string? toSymbol = null,
			CancellationToken ct = default)
			=> Funding_Transfer_Async(currency, amount, fromAccount, toAccount, subAccountName, fromSymbol, toSymbol, ct).Result;
		/// <summary>
		/// This endpoint supports the transfer of funds among your funding account, trading accounts, main account, and sub accounts.
		/// Limit: 1 request per 2 seconds (per currency)
		/// </summary>
		/// <param name="currency">Token symbol, e.g., 'EOS'</param>
		/// <param name="amount">Amount to be transferred</param>
		/// <param name="fromAccount">Remitting account</param>
		/// <param name="toAccount">Receiving account</param>
		/// <param name="subAccountName">Name of the sub account</param>
		/// <param name="fromSymbol">Margin trading pair of token or underlying of USDT-margined futures transferred out, such as: btc-usdt. Limited to trading pairs available for margin trading or underlying of enabled futures trading.</param>
		/// <param name="toSymbol">Margin trading pair of token or underlying of USDT-margined futures transferred in, such as: btc-usdt. Limited to trading pairs available for margin trading or underlying of enabled futures trading.</param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public async Task<WebCallResult<RestObjects.Funding.AssetTransfer>> Funding_Transfer_Async(
			string currency,
			decimal amount,
			FundingTransferAccountType fromAccount,
			FundingTransferAccountType toAccount,
			string? subAccountName = null,
			string? fromSymbol = null,
			string? toSymbol = null,
			CancellationToken ct = default)
		{
			currency = currency.ValidateCurrency();

			if (fromAccount == toAccount)
				throw new ArgumentException("'fromAccount' and 'toAccount' must be different.");

			if ((fromAccount == FundingTransferAccountType.SubAccount || toAccount == FundingTransferAccountType.SubAccount) && string.IsNullOrEmpty(subAccountName))
				throw new ArgumentException("When 'fromAccount' or 'toAccount' is SubAccount, subAccountName parameter is required.");

			if (fromAccount == FundingTransferAccountType.SubAccount && toAccount != FundingTransferAccountType.FundingAccount)
				throw new ArgumentException("When 'fromAccount' is SubAccount, 'toAccount' can only be FundingAccount as the sub account can only be transferred to the main account.");

			if ((fromAccount == FundingTransferAccountType.Margin || toAccount == FundingTransferAccountType.Margin) && string.IsNullOrEmpty(fromSymbol))
				throw new ArgumentException("When 'fromAccount' or 'toAccount' is Margin, fromSymbol parameter is required.");

			var parameters = new Dictionary<string, object>
			{
				{ "currency", currency },
				{ "amount", amount },
				{ "from", JsonConvert.SerializeObject(fromAccount, new FundingTransferAccountTypeConverter(false)) },
				{ "to", JsonConvert.SerializeObject(toAccount, new FundingTransferAccountTypeConverter(false)) },
			};
			parameters.AddOptionalParameter("sub_account", subAccountName);
			parameters.AddOptionalParameter("instrument_id", fromSymbol);
			parameters.AddOptionalParameter("to_instrument_id", toSymbol);

			return await SendRequest<RestObjects.Funding.AssetTransfer>(GetUrl(Endpoints_Funding_Transfer, currency), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
		}


		/// <summary>
		/// This retrieves the account bills dating back the past month. Pagination is supported and the response is sorted with most recent first in reverse chronological order. Latest 1 month records will be returned at maximum.
		/// Rate Limit: 20 requests per 2 seconds
		/// </summary>
		/// <param name="currency">The token symbol, e.g. 'BTC'. Complete account statement for will be returned if the field is left blank</param>
		/// <param name="type">Type of Bill</param>
		/// <param name="limit">Number of results per request. The maximum is 100; the default is 100</param>
		/// <param name="before">Pagination of data to return records newer than the requested ledger_id</param>
		/// <param name="after">Pagination of data to return records earlier than the requested ledger_id</param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public WebCallResult<IEnumerable<RestObjects.Funding.Bill>> Funding_GetBills(string? currency = null, FundingBillType? type = null, int limit = 100, int? before = null, int? after = null, CancellationToken ct = default) => Funding_GetBills_Async(currency, type, limit, before, after, ct).Result;
		/// <summary>
		/// This retrieves the account bills dating back the past month. Pagination is supported and the response is sorted with most recent first in reverse chronological order. Latest 1 month records will be returned at maximum.
		/// Rate Limit: 20 requests per 2 seconds
		/// </summary>
		/// <param name="currency">The token symbol, e.g. 'BTC'. Complete account statement for will be returned if the field is left blank</param>
		/// <param name="type">Type of Bill</param>
		/// <param name="limit">Number of results per request. The maximum is 100; the default is 100</param>
		/// <param name="before">Pagination of data to return records newer than the requested ledger_id</param>
		/// <param name="after">Pagination of data to return records earlier than the requested ledger_id</param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public async Task<WebCallResult<IEnumerable<RestObjects.Funding.Bill>>> Funding_GetBills_Async(string? currency = null, FundingBillType? type = null, int limit = 100, int? before = null, int? after = null, CancellationToken ct = default)
		{
			if (!string.IsNullOrEmpty(currency)) currency = currency?.ValidateCurrency();
			limit.ValidateIntBetween(nameof(limit), 1, 100);

			var parameters = new Dictionary<string, object>
			{
				{ "limit", limit },
			};
			parameters.AddOptionalParameter("currency", currency);
			if (type != null) parameters.AddOptionalParameter("type", JsonConvert.SerializeObject(type, new FundingBillTypeConverter(false)));
			parameters.AddOptionalParameter("before", before);
			parameters.AddOptionalParameter("after", after);

			return await SendRequest<IEnumerable<RestObjects.Funding.Bill>>(GetUrl(Endpoints_Funding_Bills), HttpMethod.Get, ct, parameters, signed: true).ConfigureAwait(false);
		}


		/// <summary>
		/// This retrieves a list of all currencies. Not all currencies can be traded. Currencies that have not been defined in ISO 4217 may use a custom symbol.
		/// Rate Limit: 20 requests per 2 seconds
		/// </summary>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public WebCallResult<IEnumerable<RestObjects.Funding.AssetInformation>> Funding_GetAllCurrencies(CancellationToken ct = default) => Funding_GetAllCurrencies_Async(ct).Result;
		/// <summary>
		/// This retrieves a list of all currencies. Not all currencies can be traded. Currencies that have not been defined in ISO 4217 may use a custom symbol.
		/// Rate Limit: 20 requests per 2 seconds
		/// </summary>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public async Task<WebCallResult<IEnumerable<RestObjects.Funding.AssetInformation>>> Funding_GetAllCurrencies_Async(CancellationToken ct = default)
		{
			return await SendRequest<IEnumerable<RestObjects.Funding.AssetInformation>>(GetUrl(Endpoints_Funding_GetCurrencies), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
		}


		/// <summary>
		/// This endpoint supports the withdrawal of tokens
		/// Limit: 20 requests per 2 seconds
		/// </summary>
		/// <param name="currency">Token symbol, e.g., 'BTC'</param>
		/// <param name="amount">Withdrawal amount</param>
		/// <param name="destination">withdrawal address(3:OKEx 4:others 68.CoinAll )</param>
		/// <param name="toAddress">Verified digital currency address, email or mobile number. Some digital currency addresses are formatted as 'address+tag', e.g. 'ARDOR-7JF3-8F2E-QUWZ-CAN7F：123456'</param>
		/// <param name="fundPassword">Fund password</param>
		/// <param name="fee">Network transaction fee. Please refer to the withdrawal fees section below for recommended fee amount</param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public WebCallResult<IEnumerable<RestObjects.Funding.WithdrawalRequest>> Funding_Withdrawal(string currency, decimal amount, FundinWithdrawalDestination destination, string toAddress, string fundPassword, decimal fee,  CancellationToken ct = default) => Funding_Withdrawal_Async( currency,  amount,  destination, toAddress, fundPassword,  fee, ct).Result;
		/// <summary>
		/// This endpoint supports the withdrawal of tokens
		/// Limit: 20 requests per 2 seconds
		/// </summary>
		/// <param name="currency">Token symbol, e.g., 'BTC'</param>
		/// <param name="amount">Withdrawal amount</param>
		/// <param name="destination">withdrawal address(3:OKEx 4:others 68.CoinAll )</param>
		/// <param name="toAddress">Verified digital currency address, email or mobile number. Some digital currency addresses are formatted as 'address+tag', e.g. 'ARDOR-7JF3-8F2E-QUWZ-CAN7F：123456'</param>
		/// <param name="fundPassword">Fund password</param>
		/// <param name="fee">Network transaction fee. Please refer to the withdrawal fees section below for recommended fee amount</param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public async Task<WebCallResult<IEnumerable<RestObjects.Funding.WithdrawalRequest>>> Funding_Withdrawal_Async(string currency, decimal amount, FundinWithdrawalDestination destination, string toAddress, string fundPassword, decimal fee, CancellationToken ct = default)
		{
			currency = currency.ValidateCurrency();

			var parameters = new Dictionary<string, object>
			{
				{ "currency", currency },
				{ "amount", amount },
				{ "destination", JsonConvert.SerializeObject(destination, new FundinWithdrawalDestinationConverter(false)) },
				{ "to_address", toAddress },
				{ "trade_pwd", fundPassword },
				{ "fee", fee },
			};

			return await SendRequest<IEnumerable<RestObjects.Funding.WithdrawalRequest>>(GetUrl(Endpoints_Funding_Withdrawal), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
		}


		/// <summary>
		/// This retrieves the information about the recommended network transaction fee for withdrawals to digital currency addresses. The higher the fees are set, the faster the confirmations.
		/// <param name="currency">Token symbol, e.g. 'BTC', if left blank, information for all tokens will be returned</param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public WebCallResult<IEnumerable<RestObjects.Funding.WithdrawalFee>> Funding_GetWithdrawalFees(string? currency=null, CancellationToken ct = default) => Funding_GetWithdrawalFees_Async(currency, ct).Result;
		/// <summary>
		/// This retrieves the information about the recommended network transaction fee for withdrawals to digital currency addresses. The higher the fees are set, the faster the confirmations.
		/// <param name="currency">Token symbol, e.g. 'BTC', if left blank, information for all tokens will be returned</param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public async Task<WebCallResult<IEnumerable<RestObjects.Funding.WithdrawalFee>>> Funding_GetWithdrawalFees_Async(string? currency = null, CancellationToken ct = default)
		{
			if (!string.IsNullOrEmpty(currency)) currency = currency?.ValidateCurrency();

			var parameters = new Dictionary<string, object>();
			parameters.AddOptionalParameter("currency", currency);

			return await SendRequest<IEnumerable<RestObjects.Funding.WithdrawalFee>>(GetUrl(Endpoints_Funding_WithdrawalFees), HttpMethod.Get, ct, parameters, signed: true).ConfigureAwait(false);
		}


		/// <summary>
		/// This retrieves up to 100 recent withdrawal records.
		/// Limit: 20 requests per 2 seconds
		/// </summary>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public WebCallResult<IEnumerable<RestObjects.Funding.WithdrawalDetails>> Funding_GetWithdrawalHistory(CancellationToken ct = default) => Funding_GetWithdrawalHistory_Async(ct).Result;
		/// <summary>
		/// This retrieves up to 100 recent withdrawal records.
		/// Limit: 20 requests per 2 seconds
		/// </summary>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public async Task<WebCallResult<IEnumerable<RestObjects.Funding.WithdrawalDetails>>> Funding_GetWithdrawalHistory_Async(CancellationToken ct = default)
		{
			return await SendRequest<IEnumerable<RestObjects.Funding.WithdrawalDetails>>(GetUrl(Endpoints_Funding_WithdrawalHistory), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
		}


		/// <summary>
		/// This retrieves the withdrawal records of a specific currency.
		/// Limit: 20 requests per 2 seconds
		/// </summary>
		/// <param name="currency">Token symbol</param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public WebCallResult<IEnumerable<RestObjects.Funding.WithdrawalDetails>> Funding_GetWithdrawalHistoryByCurrency(string currency, CancellationToken ct = default) => Funding_GetWithdrawalHistoryByCurrency_Async(currency,ct).Result;
		/// <summary>
		/// This retrieves the withdrawal records of a specific currency.
		/// Limit: 20 requests per 2 seconds
		/// </summary>
		/// <param name="currency">Token symbol</param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public async Task<WebCallResult<IEnumerable<RestObjects.Funding.WithdrawalDetails>>> Funding_GetWithdrawalHistoryByCurrency_Async(string currency, CancellationToken ct = default)
		{
			currency = currency.ValidateCurrency();
			return await SendRequest<IEnumerable<RestObjects.Funding.WithdrawalDetails>>(GetUrl(Endpoints_Funding_WithdrawalHistoryCurrency, currency), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
		}


		/// <summary>
		/// This retrieves the deposit addresses of currencies, including previously used addresses.
		/// Limit: 20 requests per 2 seconds
		/// </summary>
		/// <param name="currency">Token symbol</param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public WebCallResult<IEnumerable<RestObjects.Funding.DepositAddress>> Funding_GetDepositAddress(string currency, CancellationToken ct = default) => Funding_GetDepositAddress_Async(currency , ct).Result;
		/// <summary>
		/// This retrieves the deposit addresses of currencies, including previously used addresses.
		/// Limit: 20 requests per 2 seconds
		/// </summary>
		/// <param name="currency">Token symbol</param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public async Task<WebCallResult<IEnumerable<RestObjects.Funding.DepositAddress>>> Funding_GetDepositAddress_Async(string currency, CancellationToken ct = default)
		{
			currency = currency.ValidateCurrency();

			var parameters = new Dictionary<string, object>
			{
				{ "currency", currency },
			};

			return await SendRequest<IEnumerable<RestObjects.Funding.DepositAddress>>(GetUrl(Endpoints_Funding_DepositAddress), HttpMethod.Get, ct, parameters, signed: true).ConfigureAwait(false);
		}


		/// <summary>
		/// This retrieves the deposit history of all currencies, up to 100 recent records.
		/// Limit: 20 requests per 2 seconds
		/// </summary>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public WebCallResult<IEnumerable<RestObjects.Funding.DepositDetails>> Funding_GetDepositHistory(CancellationToken ct = default) => Funding_GetDepositHistory_Async(ct).Result;
		/// <summary>
		/// This retrieves the deposit history of all currencies, up to 100 recent records.
		/// Limit: 20 requests per 2 seconds
		/// </summary>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public async Task<WebCallResult<IEnumerable<RestObjects.Funding.DepositDetails>>> Funding_GetDepositHistory_Async(CancellationToken ct = default)
		{
			return await SendRequest<IEnumerable<RestObjects.Funding.DepositDetails>>(GetUrl(Endpoints_Funding_DepositHistory), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
		}


		/// <summary>
		/// This retrieves the deposit history of a currency, up to 100 recent records returned.
		/// Limit: 20 requests per 2 seconds
		/// </summary>
		/// <param name="currency">Token Symbol</param>
		/// <param name="ct"></param>
		/// <returns></returns>
		public WebCallResult<IEnumerable<RestObjects.Funding.DepositDetails>> Funding_GetDepositHistoryByCurrency(string currency, CancellationToken ct = default) => Funding_GetDepositHistoryByCurrency_Async( currency, ct).Result;
		/// <summary>
		/// This retrieves the deposit history of a currency, up to 100 recent records returned.
		/// Limit: 20 requests per 2 seconds
		/// </summary>
		/// <param name="currency">Token Symbol</param>
		/// <param name="ct"></param>
		/// <returns></returns>
		public async Task<WebCallResult<IEnumerable<RestObjects.Funding.DepositDetails>>> Funding_GetDepositHistoryByCurrency_Async(string currency, CancellationToken ct = default)
		{
			currency = currency.ValidateCurrency();
			return await SendRequest<IEnumerable<RestObjects.Funding.DepositDetails>>(GetUrl(Endpoints_Funding_DepositHistoryCurrency, currency), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
		}
		#endregion

		#region Spot Tradimg API

		#region Public EndPoints
		/// <summary>
		/// This provides snapshots of market data and is publicly accessible without account authentication.
		/// Retrieves list of trading pairs, trading limit, and unit increment.
		/// Rate limit: 20 Requests per 2 seconds
		/// </summary>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public WebCallResult<IEnumerable<RestObjects.Spot.TradingPair>> Spot_GetTradingPairs(CancellationToken ct = default) => Spot_GetTradingPairs_Async(ct).Result;
		/// <summary>
		/// This provides snapshots of market data and is publicly accessible without account authentication.
		/// Retrieves list of trading pairs, trading limit, and unit increment.
		/// Rate limit: 20 Requests per 2 seconds
		/// </summary>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public async Task<WebCallResult<IEnumerable<RestObjects.Spot.TradingPair>>> Spot_GetTradingPairs_Async(CancellationToken ct = default)
		{
			return await SendRequest<IEnumerable<RestObjects.Spot.TradingPair>>(GetUrl(Endpoints_Spot_TradingPairs), HttpMethod.Get, ct).ConfigureAwait(false);
		}


		/// <summary>
		/// Retrieve a trading pair's order book. Pagination is not supported here; the entire orderbook will be returned per request. This is publicly accessible without account authentication. WebSocket is recommended here.
		/// Rate limit: 20 requests per 2 seconds
		/// </summary>
		/// <param name="symbol">Trading pair symbol</param>
		/// <param name="size">Number of results per request. Maximum 200</param>
		/// <param name="depth">Aggregation of the order book. e.g . 0.1, 0.001</param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public WebCallResult<RestObjects.Spot.OrderBook> Spot_GetOrderBook(string symbol, int? size = null, decimal? depth = null, CancellationToken ct = default) => Spot_GetOrderBook_Async(symbol, size, depth, ct).Result;
		/// <summary>
		/// Retrieve a trading pair's order book. Pagination is not supported here; the entire orderbook will be returned per request. This is publicly accessible without account authentication. WebSocket is recommended here.
		/// Rate limit: 20 requests per 2 seconds
		/// </summary>
		/// <param name="symbol">Trading pair symbol</param>
		/// <param name="size">Number of results per request. Maximum 200</param>
		/// <param name="depth">Aggregation of the order book. e.g . 0.1, 0.001</param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public async Task<WebCallResult<RestObjects.Spot.OrderBook>> Spot_GetOrderBook_Async(string symbol, int? size = null, decimal? depth = null, CancellationToken ct = default)
		{
			symbol = symbol.ValidateSymbol();
			size?.ValidateIntBetween(nameof(size), 1, 200);

			var parameters = new Dictionary<string, object>();
			parameters.AddOptionalParameter("size", size);
			parameters.AddOptionalParameter("depth", depth);

			#region Default Method
			// return await SendRequest<RestObjects.Spot.OrderBook>(GetUrl(Endpoints_Spot_OrderBook, symbol), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
			#endregion

			#region Modified Method
			var result = await SendRequest<RestObjects.Spot.OrderBook>(GetUrl(Endpoints_Spot_OrderBook, symbol), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
			if (!result || result.Data == null)
				return new WebCallResult<RestObjects.Spot.OrderBook>(result.ResponseStatusCode, result.ResponseHeaders, default, result.Error);

			if (result.Error != null)
				return new WebCallResult<RestObjects.Spot.OrderBook>(result.ResponseStatusCode, result.ResponseHeaders, default, new ServerError(result.Error.Code, result.Error.Message));

			result.Data.Symbol = symbol.ToUpper(OkexHelpers.OkexCultureInfo);
			return new WebCallResult<RestObjects.Spot.OrderBook>(result.ResponseStatusCode, result.ResponseHeaders, result.Data, null);
			#endregion
		}


		/// <summary>
		/// Retrieve the latest price snapshot, best bid/ask price, and trading volume in the last 24 hours for all trading pairs. This is publicly accessible without account authentication.
		/// Rate limit: 20 requests per 2 seconds
		/// </summary>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public WebCallResult<IEnumerable<RestObjects.Spot.Ticker>> Spot_GetAllTickers(CancellationToken ct = default) => Spot_GetAllTickers_Async(ct).Result;
		/// <summary>
		/// Retrieve the latest price snapshot, best bid/ask price, and trading volume in the last 24 hours for all trading pairs. This is publicly accessible without account authentication.
		/// Rate limit: 20 requests per 2 seconds
		/// </summary>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public async Task<WebCallResult<IEnumerable<RestObjects.Spot.Ticker>>> Spot_GetAllTickers_Async(CancellationToken ct = default)
		{
			return await SendRequest<IEnumerable<RestObjects.Spot.Ticker>>(GetUrl(Endpoints_Spot_TradingPairsTicker), HttpMethod.Get, ct).ConfigureAwait(false);
		}


		/// <summary>
		/// Retrieve the latest price snapshot, best bid/ask price, and trading volume in the last 24 hours for a particular trading pair. This is publicly accessible without account authentication.
		/// Rate limit: 20 requests per 2 seconds
		/// </summary>
		/// <param name="symbol">Trading Pair symbol</param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public WebCallResult<RestObjects.Spot.Ticker> Spot_GetSymbolTicker(string symbol, CancellationToken ct = default) => Spot_GetSymbolTicker_Async(symbol, ct).Result;
		/// <summary>
		/// Retrieve the latest price snapshot, best bid/ask price, and trading volume in the last 24 hours for a particular trading pair. This is publicly accessible without account authentication.
		/// Rate limit: 20 requests per 2 seconds
		/// </summary>
		/// <param name="symbol">Trading Pair symbol</param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public async Task<WebCallResult<RestObjects.Spot.Ticker>> Spot_GetSymbolTicker_Async(string symbol, CancellationToken ct = default)
		{
			symbol = symbol.ValidateSymbol();
			return await SendRequest<RestObjects.Spot.Ticker>(GetUrl(Endpoints_Spot_TradingPairTicker, symbol), HttpMethod.Get, ct).ConfigureAwait(false);
		}


		/// <summary>
		/// Retrieve the latest 60 transactions of all trading pairs.
		/// Rate limit: 20 requests per 2 seconds
		/// </summary>
		/// <param name="symbol">Trading pair symbol</param>
		/// <param name="limit">Number of results per request. The maximum is 60; the default is 60</param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public WebCallResult<IEnumerable<RestObjects.Spot.Trade>> Spot_GetTrades(string symbol, int limit = 60, CancellationToken ct = default) => Spot_GetTrades_Async(symbol, limit, ct).Result;
		/// <summary>
		/// Retrieve the latest 60 transactions of all trading pairs.
		/// Rate limit: 20 requests per 2 seconds
		/// </summary>
		/// <param name="symbol">Trading pair symbol</param>
		/// <param name="limit">Number of results per request. The maximum is 60; the default is 60</param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public async Task<WebCallResult<IEnumerable<RestObjects.Spot.Trade>>> Spot_GetTrades_Async(string symbol, int limit = 60, CancellationToken ct = default)
		{
			symbol = symbol.ValidateSymbol();
			limit.ValidateIntBetween(nameof(limit), 1, 60);

			var parameters = new Dictionary<string, object>
			{
				{ "limit", limit },
			};

			#region Default Method
			// return await SendRequest<IEnumerable<RestObjects.Spot.Trade>>(GetUrl(Endpoints_Spot_Trades, symbol), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
			#endregion

			#region Modified Method
			var result = await SendRequest<IEnumerable<RestObjects.Spot.Trade>>(GetUrl(Endpoints_Spot_Trades, symbol), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
			if (!result || result.Data == null)
				return new WebCallResult<IEnumerable<RestObjects.Spot.Trade>>(result.ResponseStatusCode, result.ResponseHeaders, default, result.Error);

			if (result.Error != null)
				return new WebCallResult<IEnumerable<RestObjects.Spot.Trade>>(result.ResponseStatusCode, result.ResponseHeaders, default, new ServerError(result.Error.Code, result.Error.Message));

			foreach (var data in result.Data)
			{
				data.Symbol = symbol.ToUpper(OkexHelpers.OkexCultureInfo);
			}

			return new WebCallResult<IEnumerable<RestObjects.Spot.Trade>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data, null);
			#endregion
		}


            /// <summary>
            /// Retrieve the candlestick charts of the trading pairs. This API can retrieve the latest 2000 entries of data. Candlesticks are returned in groups based on requested granularity. Maximum of 2,000 entries can be retrieved.
            /// Rate limit: 20 requests per 2 seconds
            /// </summary>
            /// <param name="symbol">Trading pairs symbol</param>
            /// <param name="period">Start time in ISO 8601</param>
            /// <param name="start">End time in ISO 8601</param>
            /// <param name="end">Bar size in seconds, default 60, must be one of [60/180/300/900/1800/3600/7200/14400/21600/43200/86400/604800] or returns error</param>
            /// <param name="ct">Cancellation Token</param>
            /// <returns></returns>
            public WebCallResult<IEnumerable<RestObjects.Spot.Candle>> Spot_GetCandles(string symbol, SpotPeriod period, DateTime? start = null, DateTime? end = null, CancellationToken ct = default) => Spot_GetCandles_Async(symbol, period, start, end, ct).Result;
		/// <summary>
		/// Retrieve the candlestick charts of the trading pairs. This API can retrieve the latest 2000 entries of data. Candlesticks are returned in groups based on requested granularity. Maximum of 2,000 entries can be retrieved.
		/// Rate limit: 20 requests per 2 seconds
		/// </summary>
		/// <param name="symbol">Trading pairs symbol</param>
		/// <param name="period">Start time in ISO 8601</param>
		/// <param name="start">End time in ISO 8601</param>
		/// <param name="end">Bar size in seconds, default 60, must be one of [60/180/300/900/1800/3600/7200/14400/21600/43200/86400/604800] or returns error</param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public async Task<WebCallResult<IEnumerable<RestObjects.Spot.Candle>>> Spot_GetCandles_Async(string symbol, SpotPeriod period, DateTime? start = null, DateTime? end = null, CancellationToken ct = default)
		{
			symbol = symbol.ValidateSymbol();

			var parameters = new Dictionary<string, object>
			{
				{ "granularity", JsonConvert.SerializeObject(period, new SpotPeriodConverter(false)) },
			};
			parameters.AddOptionalParameter("start", start?.DateTimeToIso8601String());
			parameters.AddOptionalParameter("end", end?.DateTimeToIso8601String());

			#region Default Method
			// return await SendRequest<IEnumerable<RestObjects.Spot.Candle>>(GetUrl(Endpoints_Spot_Candles, symbol), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
			#endregion

			#region Modified Method
			var result = await SendRequest<IEnumerable<RestObjects.Spot.Candle>>(GetUrl(Endpoints_Spot_Candles, symbol), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
			if (!result || result.Data == null)
				return new WebCallResult<IEnumerable<RestObjects.Spot.Candle>>(result.ResponseStatusCode, result.ResponseHeaders, default, result.Error);

			if (result.Error != null)
				return new WebCallResult<IEnumerable<RestObjects.Spot.Candle>>(result.ResponseStatusCode, result.ResponseHeaders, default, new ServerError(result.Error.Code, result.Error.Message));

			foreach (var data in result.Data)
			{
				data.Symbol = symbol.ToUpper(OkexHelpers.OkexCultureInfo);
			}

			return new WebCallResult<IEnumerable<RestObjects.Spot.Candle>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data, null);
			#endregion
		}
		#endregion

		#region Private EndPoints
		/// <summary>
		/// This retrieves the list of assets, (with non-zero balance), remaining balance, and amount available in the spot trading account.
		/// Rate limit: 20 requests per 2 seconds
		/// </summary>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public WebCallResult<IEnumerable<RestObjects.Spot.Account>> Spot_GetAllBalances(CancellationToken ct = default) => Spot_GetAllBalances_Async(ct).Result;
		/// <summary>
		/// This retrieves the list of assets, (with non-zero balance), remaining balance, and amount available in the spot trading account.
		/// Rate limit: 20 requests per 2 seconds
		/// </summary>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public async Task<WebCallResult<IEnumerable<RestObjects.Spot.Account>>> Spot_GetAllBalances_Async(CancellationToken ct = default)
		{
			return await SendRequest<IEnumerable<RestObjects.Spot.Account>>(GetUrl(Endpoints_Spot_Accounts), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
		}


		/// <summary>
		/// This retrieves information for a single currency in your account, including the remaining balance, and the amount available or on hold.
		/// Rate Limit: 20 requests per 2 seconds
		/// </summary>
		/// <param name="symbol">Token symbol</param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public WebCallResult<RestObjects.Spot.Account> Spot_GetSymbolBalance(string currency, CancellationToken ct = default) => Spot_GetSymbolBalance_Async(currency, ct).Result;
		/// <summary>
		/// This retrieves information for a single currency in your account, including the remaining balance, and the amount available or on hold.
		/// Rate Limit: 20 requests per 2 seconds
		/// </summary>
		/// <param name="symbol">Token symbol</param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public async Task<WebCallResult<RestObjects.Spot.Account>> Spot_GetSymbolBalance_Async(string currency, CancellationToken ct = default)
		{
			currency = currency.ValidateCurrency();
			return await SendRequest<RestObjects.Spot.Account>(GetUrl(Endpoints_Spot_Account, currency), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
		}


		/// <summary>
		/// This retrieves the spot account bills dating back the past 3 months. Pagination is supported and the response is sorted with most recent first in reverse chronological order.
		/// Rate Limit: 20 requests per 2 seconds
		/// <param name="currency">The token symbol, e.g. 'BTC'. Complete account statement for will be returned if the field is left blank</param>
		/// <param name="type">Bill Type of Transaction</param>
		/// <param name="limit">Number of results per request. The maximum is 100; the default is 100</param>
		/// <param name="before">Pagination of data to return records newer than the requested ledger_id</param>
		/// <param name="after">Pagination of data to return records earlier than the requested ledger_id</param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public WebCallResult<IEnumerable<RestObjects.Spot.Bill>> Spot_GetSymbolBills(string currency, SpotBillType? type = null, int limit = 100, int? before = null, int? after = null, CancellationToken ct = default) => Spot_GetSymbolBills_Async(currency, type, limit, before, after, ct).Result;
		/// <summary>
		/// This retrieves the spot account bills dating back the past 3 months. Pagination is supported and the response is sorted with most recent first in reverse chronological order.
		/// Rate Limit: 20 requests per 2 seconds
		/// <param name="currency">The token symbol, e.g. 'BTC'. Complete account statement for will be returned if the field is left blank</param>
		/// <param name="type">Bill Type of Transaction</param>
		/// <param name="limit">Number of results per request. The maximum is 100; the default is 100</param>
		/// <param name="before">Pagination of data to return records newer than the requested ledger_id</param>
		/// <param name="after">Pagination of data to return records earlier than the requested ledger_id</param>
		/// <param name="ct">Cancellation Token</param>
		public async Task<WebCallResult<IEnumerable<RestObjects.Spot.Bill>>> Spot_GetSymbolBills_Async(string currency, SpotBillType? type = null, int limit = 100, int? before = null, int? after = null, CancellationToken ct = default)
		{
			currency = currency.ValidateCurrency();
			limit.ValidateIntBetween(nameof(limit), 1, 100);

			var parameters = new Dictionary<string, object>
			{
				{ "limit", limit },
			};
			if (type != null) parameters.AddOptionalParameter("type", JsonConvert.SerializeObject(type, new SpotBillTypeConverter(false)));
			parameters.AddOptionalParameter("before", before);
			parameters.AddOptionalParameter("after", after);

			return await SendRequest<IEnumerable<RestObjects.Spot.Bill>>(GetUrl(Endpoints_Spot_Bills, currency), HttpMethod.Get, ct, parameters, signed: true).ConfigureAwait(false);
		}


		/// <summary>
		/// OKEx spot trading supports only limit and market orders. More order types will become available in the future. You can place an order only if you have enough funds.
		/// Once your order is placed, the amount will be put on hold until the order is executed.
		/// Rate limit: 100 requests per 2 seconds
		/// </summary>
		/// <param name="symbol">
		///		Trading pair symbol
		///		The instrument_id must match a valid instrument. The instruments list is available via the /instrument endpoint
		///	</param>
		/// <param name="side">
		///		Specify buy or sell
		/// </param>
		/// <param name="type">
		///		Supports types limit or market (default: limit). When placing market orders, order_type must be 0 (normal order)
		///		You can specify the order type when placing an order. The order type you specify will decide which order parameters are required further as well as how your order will be executed by the matching engine. If type is not specified, the order will default to a limit order. Limit order is the default order type, and it is also the basic order type. A limit order requires specifying a price and size. The limit order will be filled at the specifie price or better. Specifically, A sell order can be filled at the specified or higher price per the quote token. A buy order can be filled at the specified or lower price per the quote token. If the limit order is not filled immediately, it will be sent into the open order book until filled or canceled. Market orders differ from limit orders in that they have NO price specification. It provides an order type to buy or sell specific amount of tokens without the need to specify the price. Market orders execute immediately and will not be sent into the open order book. Market orders are always considered as ‘takers’ and incur taker fees. Warming: Market order is strongly discouraged and if an order to sell/buy a large amount is placed it will probably cause turbulence in the market.
		/// </param>
		/// <param name="timeInForce">
		///		Specify 0: Normal order (Unfilled and 0 imply normal limit order) 1: Post only 2: Fill or Kill 3: Immediate Or Cancel
		/// </param>
		/// <param name="price">
		///		Limit Order: Price
		///		The price must be specified in unit of quote_increment which is the smallest incremental unit of the price. It can be acquired via the /instrument endpoint.
		/// </param>
		/// <param name="size">
		///		Limit Order: Quantity to buy or sell
		///		Market Order: Quantity to be sold. Required for market sells
		///		Size is the quantity of buying or selling and must be larger than the min_size. size_increment is the minimum increment size. It can be acquired via the /instrument endpoint.
		///		Example: If the min_size of OKB/USDT is 10 and the size_increment is 0.0001, then it is impossible to trade 9.99 OKB but possible to trade 10.0001 OKB.
		/// </param>
		/// <param name="notional">
		///		Market Order: Amount to spend. Required for market buys
		///		The notional field is the quantity of quoted currency when placing market orders; it is required for market orders. For example, a market buy for BTC-USDT with quantity specified as 5000 will spend 5000 USDT to buy BTC.
		/// </param>
		/// <param name="clientOrderId">
		///		You can customize order IDs to identify your orders. The system supports alphabets (case-sensitive) + numbers, or letters (case-sensitive) between 1-32 characters.
		///		The client_oid is optional. It should be a unique ID generated by your trading system. This parameter is used to identify your orders in the public orders feed. No warning is sent when client_oid is not unique.
		///		In case of multiple identical client_oid, only the latest entry will be returned.
		/// </param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public WebCallResult<RestObjects.Spot.NewOrder> Spot_PlaceOrder(
			string symbol,
			SpotOrderSide side,
			SpotOrderType type,
			SpotTimeInForce timeInForce = SpotTimeInForce.NormalOrder,
			decimal? price = null,
			decimal? size = null,
			decimal? notional = null,
			string? clientOrderId = null,
			CancellationToken ct = default)
			=> Spot_PlaceOrder_Async(symbol, side, type, timeInForce, price, size, notional, clientOrderId, ct = default).Result;
		/// <summary>
		/// OKEx spot trading supports only limit and market orders. More order types will become available in the future. You can place an order only if you have enough funds.
		/// Once your order is placed, the amount will be put on hold until the order is executed.
		/// Rate limit: 100 requests per 2 seconds
		/// </summary>
		/// <param name="symbol">
		///		Trading pair symbol
		///		The instrument_id must match a valid instrument. The instruments list is available via the /instrument endpoint
		///	</param>
		/// <param name="side">
		///		Specify buy or sell
		/// </param>
		/// <param name="type">
		///		Supports types limit or market (default: limit). When placing market orders, order_type must be 0 (normal order)
		///		You can specify the order type when placing an order. The order type you specify will decide which order parameters are required further as well as how your order will be executed by the matching engine. If type is not specified, the order will default to a limit order. Limit order is the default order type, and it is also the basic order type. A limit order requires specifying a price and size. The limit order will be filled at the specifie price or better. Specifically, A sell order can be filled at the specified or higher price per the quote token. A buy order can be filled at the specified or lower price per the quote token. If the limit order is not filled immediately, it will be sent into the open order book until filled or canceled. Market orders differ from limit orders in that they have NO price specification. It provides an order type to buy or sell specific amount of tokens without the need to specify the price. Market orders execute immediately and will not be sent into the open order book. Market orders are always considered as ‘takers’ and incur taker fees. Warming: Market order is strongly discouraged and if an order to sell/buy a large amount is placed it will probably cause turbulence in the market.
		/// </param>
		/// <param name="timeInForce">
		///		Specify 0: Normal order (Unfilled and 0 imply normal limit order) 1: Post only 2: Fill or Kill 3: Immediate Or Cancel
		/// </param>
		/// <param name="price">
		///		Limit Order: Price
		///		The price must be specified in unit of quote_increment which is the smallest incremental unit of the price. It can be acquired via the /instrument endpoint.
		/// </param>
		/// <param name="size">
		///		Limit Order: Quantity to buy or sell
		///		Market Order: Quantity to be sold. Required for market sells
		///		Size is the quantity of buying or selling and must be larger than the min_size. size_increment is the minimum increment size. It can be acquired via the /instrument endpoint.
		///		Example: If the min_size of OKB/USDT is 10 and the size_increment is 0.0001, then it is impossible to trade 9.99 OKB but possible to trade 10.0001 OKB.
		/// </param>
		/// <param name="notional">
		///		Market Order: Amount to spend. Required for market buys
		///		The notional field is the quantity of quoted currency when placing market orders; it is required for market orders. For example, a market buy for BTC-USDT with quantity specified as 5000 will spend 5000 USDT to buy BTC.
		/// </param>
		/// <param name="clientOrderId">
		///		You can customize order IDs to identify your orders. The system supports alphabets (case-sensitive) + numbers, or letters (case-sensitive) between 1-32 characters.
		///		The client_oid is optional. It should be a unique ID generated by your trading system. This parameter is used to identify your orders in the public orders feed. No warning is sent when client_oid is not unique.
		///		In case of multiple identical client_oid, only the latest entry will be returned.
		/// </param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public async Task<WebCallResult<RestObjects.Spot.NewOrder>> Spot_PlaceOrder_Async(
			string symbol,
			SpotOrderSide side,
			SpotOrderType type,
			SpotTimeInForce timeInForce = SpotTimeInForce.NormalOrder,
			decimal? price = null,
			decimal? size = null,
			decimal? notional = null,
			string? clientOrderId = null,
			CancellationToken ct = default)
		{
			symbol = symbol.ValidateSymbol();
			clientOrderId?.ValidateStringLength("clientOrderId", 0, 32);

			if (type == SpotOrderType.Limit && (price == null || size == null))
				throw new ArgumentException("Both price and size must be provided for Limit Orders");

			if (type == SpotOrderType.Market && (size == null || notional == null))
				throw new ArgumentException("Both price and notional must be provided for Market Orders");

			if (type == SpotOrderType.Market && timeInForce != SpotTimeInForce.NormalOrder)
				throw new ArgumentException("When placing market orders, TimeInForce must be Normal Order");

			if (clientOrderId != null && !Regex.IsMatch(clientOrderId, "^(([a-z]|[A-Z]|[0-9]){0,32})$"))
				throw new ArgumentException("ClientOrderId supports alphabets (case-sensitive) + numbers, or letters (case-sensitive) between 1-32 characters.");

			var parameters = new Dictionary<string, object>
			{
				{ "instrument_id", symbol },
				{ "side", JsonConvert.SerializeObject(side, new SpotOrderSideConverter(false)) },
				{ "type", JsonConvert.SerializeObject(type, new SpotOrderTypeConverter(false)) },
				{ "order_type", JsonConvert.SerializeObject(timeInForce, new SpotTimeInForceConverter(false)) },
			};
			parameters.AddOptionalParameter("client_oid", clientOrderId);
			parameters.AddOptionalParameter("notional", notional);
			parameters.AddOptionalParameter("price", price);
			parameters.AddOptionalParameter("size", size);

			return await SendRequest<RestObjects.Spot.NewOrder>(GetUrl(Endpoints_Spot_PlaceOrder), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
		}


		/// <summary>
		/// This is used to cancel an unfilled order.
		/// Rate limit: 100 requests per 2 seconds
		/// </summary>
		/// <param name="symbol">Specify the trading pair to cancel the corresponding order. An error would be returned if the parameter is not provided.</param>
		/// <param name="orderId">Either client_oids or order_ids must be present. Order ID</param>
		/// <param name="clientOrderId">Either client_oids or order_ids must be present. Client-supplied order ID that you can customize. It should be comprised of alpha-numeric characters with length 1 to 32. Both uppercase and lowercase are supported</param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public WebCallResult<RestObjects.Spot.NewOrder> Spot_CancelOrder(string symbol, long? orderId = null, string? clientOrderId = null, CancellationToken ct = default) => Spot_CancelOrder_Async(symbol, orderId, clientOrderId, ct).Result;
		/// <summary>
		/// This is used to cancel an unfilled order.
		/// Rate limit: 100 requests per 2 seconds
		/// </summary>
		/// <param name="symbol">Specify the trading pair to cancel the corresponding order. An error would be returned if the parameter is not provided.</param>
		/// <param name="orderId">Either client_oids or order_ids must be present. Order ID</param>
		/// <param name="clientOrderId">Either client_oids or order_ids must be present. Client-supplied order ID that you can customize. It should be comprised of alpha-numeric characters with length 1 to 32. Both uppercase and lowercase are supported</param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public async Task<WebCallResult<RestObjects.Spot.NewOrder>> Spot_CancelOrder_Async(string symbol, long? orderId = null, string? clientOrderId = null, CancellationToken ct = default)
		{
			symbol = symbol.ValidateSymbol();

			if (orderId == null && string.IsNullOrEmpty(clientOrderId))
				throw new ArgumentException("Either orderId or clientOrderId must be present.");

			if (orderId != null && !string.IsNullOrEmpty(clientOrderId))
				throw new ArgumentException("Either orderId or clientOrderId must be present.");

			var parameters = new Dictionary<string, object>
			{
				{ "instrument_id", symbol },
			};

			return await SendRequest<RestObjects.Spot.NewOrder>(GetUrl(Endpoints_Spot_CancelOrder, orderId.HasValue ? orderId.ToString() : clientOrderId!), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
		}


		/// <summary>
		/// This retrieves the list of your orders from the most recent 3 months. This request supports paging and is stored according to the order time in chronological order from latest to earliest.
		/// Rate limit: 20 requests per 2 seconds
		/// </summary>
		/// <param name="symbol">Trading pair symbol</param>
		/// <param name="state">Order Status: -2 = Failed -1 = Canceled 0 = Open 1 = Partially Filled 2 = Fully Filled 3 = Submitting 4 = Canceling 6 = Incomplete (open + partially filled) 7 = Complete (canceled + fully filled)</param>
		/// <param name="limit">Number of results per request. The maximum is 100; the default is 100</param>
		/// <param name="before">Pagination of data to return records newer than the requested order_id</param>
		/// <param name="after">Pagination of data to return records earlier than the requested order_id</param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public WebCallResult<IEnumerable<RestObjects.Spot.OrderDetails>> Spot_GetAllOrders(string symbol, SpotOrderState state, int limit = 100, int? before = null, int? after = null, CancellationToken ct = default) => Spot_GetAllOrders_Async(symbol, state, limit, before, after, ct).Result;
		/// <summary>
		/// This retrieves the list of your orders from the most recent 3 months. This request supports paging and is stored according to the order time in chronological order from latest to earliest.
		/// Rate limit: 20 requests per 2 seconds
		/// </summary>
		/// <param name="symbol">Trading pair symbol</param>
		/// <param name="state">Order Status: -2 = Failed -1 = Canceled 0 = Open 1 = Partially Filled 2 = Fully Filled 3 = Submitting 4 = Canceling 6 = Incomplete (open + partially filled) 7 = Complete (canceled + fully filled)</param>
		/// <param name="limit">Number of results per request. The maximum is 100; the default is 100</param>
		/// <param name="before">Pagination of data to return records newer than the requested order_id</param>
		/// <param name="after">Pagination of data to return records earlier than the requested order_id</param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public async Task<WebCallResult<IEnumerable<RestObjects.Spot.OrderDetails>>> Spot_GetAllOrders_Async(string symbol, SpotOrderState state, int limit = 100, int? before = null, int? after = null, CancellationToken ct = default)
		{
			symbol = symbol.ValidateSymbol();
			limit.ValidateIntBetween(nameof(limit), 1, 100);

			var parameters = new Dictionary<string, object>
			{
				{ "instrument_id", symbol },
				{ "state", JsonConvert.SerializeObject(state, new SpotOrderStateConverter(false)) },
				{ "limit", limit },
			};
			parameters.AddOptionalParameter("before", before);
			parameters.AddOptionalParameter("after", after);

			return await SendRequest<IEnumerable<RestObjects.Spot.OrderDetails>>(GetUrl(Endpoints_Spot_OrderList), HttpMethod.Get, ct, parameters, signed: true).ConfigureAwait(false);
		}



		/// <summary>
		/// This retrieves the list of your current open orders. Pagination is supported and the response is sorted with most recent first in reverse chronological order.
		/// </summary>
		/// <param name="symbol">Trading pair symbol</param>
		/// <param name="limit">Number of results per request. The maximum is 100; the default is 100</param>
		/// <param name="before">Pagination of data to return records newer than the requested order_id</param>
		/// <param name="after">Pagination of data to return records earlier than the requested order_id</param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public WebCallResult<IEnumerable<RestObjects.Spot.OrderDetails>> Spot_GetOpenOrders(string symbol, int limit = 100, int? before = null, int? after = null, CancellationToken ct = default) => Spot_GetOpenOrders_Async(symbol, limit, before, after, ct).Result;
		/// <summary>
		/// This retrieves the list of your current open orders. Pagination is supported and the response is sorted with most recent first in reverse chronological order.
		/// </summary>
		/// <param name="symbol">Trading pair symbol</param>
		/// <param name="limit">Number of results per request. The maximum is 100; the default is 100</param>
		/// <param name="before">Pagination of data to return records newer than the requested order_id</param>
		/// <param name="after">Pagination of data to return records earlier than the requested order_id</param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public async Task<WebCallResult<IEnumerable<RestObjects.Spot.OrderDetails>>> Spot_GetOpenOrders_Async(string symbol, int limit = 100, int? before = null, int? after = null, CancellationToken ct = default)
		{
			symbol = symbol.ValidateSymbol();
			limit.ValidateIntBetween(nameof(limit), 1, 100);

			var parameters = new Dictionary<string, object>
			{
				{ "instrument_id", symbol },
				{ "limit", limit },
			};
			parameters.AddOptionalParameter("before", before);
			parameters.AddOptionalParameter("after", after);

			return await SendRequest<IEnumerable<RestObjects.Spot.OrderDetails>>(GetUrl(Endpoints_Spot_OpenOrders), HttpMethod.Get, ct, parameters, signed: true).ConfigureAwait(false);
		}


		/// <summary>
		/// Retrieve order details by order ID.Can get order information for nearly 3 months。 Unfilled orders will be kept in record for only two hours after it is canceled.
		/// Rate limit: 20 requests per 2 seconds
		/// </summary>
		/// <param name="symbol">Trading pair symbol</param>
		/// <param name="orderId">Order ID Either client_oid or order_id must be present.</param>
		/// <param name="clientOrderId">Client-supplied order ID Either client_oid or order_id must be present.</param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public WebCallResult<RestObjects.Spot.OrderDetails> Spot_GetOrderDetails(string symbol, long? orderId = null, string? clientOrderId = null, CancellationToken ct = default) => Spot_GetOrderDetails_Async(symbol, orderId, clientOrderId, ct).Result;
		/// <summary>
		/// Retrieve order details by order ID.Can get order information for nearly 3 months。 Unfilled orders will be kept in record for only two hours after it is canceled.
		/// Rate limit: 20 requests per 2 seconds
		/// </summary>
		/// <param name="symbol">Trading pair symbol</param>
		/// <param name="orderId">Order ID Either client_oid or order_id must be present.</param>
		/// <param name="clientOrderId">Client-supplied order ID Either client_oid or order_id must be present.</param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public async Task<WebCallResult<RestObjects.Spot.OrderDetails>> Spot_GetOrderDetails_Async(string symbol, long? orderId = null, string? clientOrderId = null, CancellationToken ct = default)
		{
			symbol = symbol.ValidateSymbol();

			if (orderId == null && string.IsNullOrEmpty(clientOrderId))
				throw new ArgumentException("Either orderId or clientOrderId must be present.");

			if (orderId != null && !string.IsNullOrEmpty(clientOrderId))
				throw new ArgumentException("Either orderId or clientOrderId must be present.");

			var parameters = new Dictionary<string, object>
			{
				{ "instrument_id", symbol },
			};

			return await SendRequest<RestObjects.Spot.OrderDetails>(GetUrl(Endpoints_Spot_OrderDetails, orderId.HasValue ? orderId.ToString() : clientOrderId!), HttpMethod.Get, ct, parameters, signed: true).ConfigureAwait(false);
		}


		/// <summary>
		/// Obtain the transaction fee rate corresponding to your current account transaction level. The sub-account rate under the parent account is the same as the parent account. Update every day at 0am
		/// Rate limit: 1 requests per 10 seconds
		/// </summary>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public WebCallResult<RestObjects.Spot.TradeFeeRates> Spot_TradeFeeRates(CancellationToken ct = default) => Spot_TradeFeeRates_Async(ct).Result;
		/// <summary>
		/// Obtain the transaction fee rate corresponding to your current account transaction level. The sub-account rate under the parent account is the same as the parent account. Update every day at 0am
		/// Rate limit: 1 requests per 10 seconds
		/// </summary>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public async Task<WebCallResult<RestObjects.Spot.TradeFeeRates>> Spot_TradeFeeRates_Async(CancellationToken ct = default)
		{
			return await SendRequest<RestObjects.Spot.TradeFeeRates>(GetUrl(Endpoints_Spot_TradeFee), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
		}

		#endregion

		#endregion

		#region Protected & Private Methods
		protected override IRequest ConstructRequest(Uri uri, HttpMethod method, Dictionary<string, object>? parameters, bool signed)
		{
			if (parameters == null)
				parameters = new Dictionary<string, object>();

			var uriString = uri.ToString();
			if (authProvider != null)
				parameters = authProvider.AddAuthenticationToParameters(uriString, method, parameters, signed);

			if ((method == HttpMethod.Get || method == HttpMethod.Delete || postParametersPosition == PostParameters.InUri) && parameters?.Any() == true)
				uriString += "?" + parameters.CreateParamString(true, arraySerialization);

			if (method == HttpMethod.Post && signed)
			{
				var uriParamNames = new[] { "AccessKeyId", "SignatureMethod", "SignatureVersion", "Timestamp", "Signature" };
				var uriParams = parameters.Where(p => uriParamNames.Contains(p.Key)).ToDictionary(k => k.Key, k => k.Value);
				uriString += "?" + uriParams.CreateParamString(true, ArrayParametersSerialization.MultipleValues);
				parameters = parameters.Where(p => !uriParamNames.Contains(p.Key)).ToDictionary(k => k.Key, k => k.Value);
			}

			var contentType = requestBodyFormat == RequestBodyFormat.Json ? Constants.JsonContentHeader : Constants.FormContentHeader;
			var request = RequestFactory.Create(method, uriString);
			request.Accept = Constants.JsonContentHeader;

			var headers = new Dictionary<string, string>();
			if (authProvider != null)
				headers = authProvider.AddAuthenticationToHeaders(uriString, method, parameters!, signed);

			foreach (var header in headers)
				request.AddHeader(header.Key, header.Value);

			if ((method == HttpMethod.Post || method == HttpMethod.Put) && postParametersPosition != PostParameters.InUri)
			{
				if (parameters?.Any() == true)
					WriteParamBody(request, parameters, contentType);
				else
					request.SetContent("{}", contentType);
			}

			return request;
		}

		protected override Error ParseErrorResponse(JToken error)
		{
			if (error["code"] == null || error["message"] == null)
				return new ServerError(error.ToString());

			return new ServerError((int)error["code"], (string)error["message"]);
		}

		protected Uri GetUrl(string endpoint, string param = "")
		{
			var x = endpoint.IndexOf('<');
			var y = endpoint.IndexOf('>');
			if (x > -1 && y > -1) endpoint = endpoint.Replace(endpoint.Substring(x, y - x + 1), param);

			return new Uri($"{BaseAddress}/{endpoint}");
		}

		private static long ToUnixTimestamp(DateTime time)
		{
			return (long)(time - new DateTime(1970, 1, 1)).TotalMilliseconds;
		}

		#endregion

	}
}
