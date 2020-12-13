using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Okex.Net.Interfaces;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Web;

namespace Okex.Net
{
	/// <summary>
	/// Client for the Okex REST API
	/// </summary>
	public partial class OkexClient : RestClient, IOkexClient, IOkexClientGeneral, IOkexClientFunding, IOkexClientSpot
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
		private const string Endpoints_Funding_GetSubAccount = "api/account/v3/sub-account";
		private const string Endpoints_Funding_GetAssetValuation = "api/account/v3/asset-valuation";
		private const string Endpoints_Funding_GetCurrencyBalance = "api/account/v3/wallet/<currency>";
		private const string Endpoints_Funding_Transfer = "api/account/v3/transfer";
		private const string Endpoints_Funding_Withdrawal = "api/account/v3/withdrawal";
		private const string Endpoints_Funding_WithdrawalHistory = "api/account/v3/withdrawal/history";
		private const string Endpoints_Funding_WithdrawalHistoryCurrency = "api/account/v3/withdrawal/history/<currency>";
		private const string Endpoints_Funding_Bills = "api/account/v3/ledger";
		private const string Endpoints_Funding_DepositAddress = "api/account/v3/deposit/address";
		private const string Endpoints_Funding_DepositHistory = "api/account/v3/deposit/history";
		private const string Endpoints_Funding_DepositHistoryCurrency = "api/account/v3/deposit/history/<currency>";
		private const string Endpoints_Funding_GetCurrencies = "api/account/v3/currencies";
		private const string Endpoints_Funding_GetUserID = "api/account/v3/uid";
		private const string Endpoints_Funding_WithdrawalFees = "api/account/v3/withdrawal/fee";
		private const string Endpoints_Funding_PiggyBank = "api/account/v3/purchase_redempt";
		#endregion

		#region Spot Trading API Endpoints

        #region Private Signed Endpoints
        private const string Endpoints_Spot_Accounts = "api/spot/v3/accounts";
		private const string Endpoints_Spot_Account = "api/spot/v3/accounts/<currency>";
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
		#endregion

		#region Public Unsigned Endpoints
		private const string Endpoints_Spot_TradingPairs = "api/spot/v3/instruments";
		private const string Endpoints_Spot_OrderBook = "api/spot/v3/instruments/<instrument_id>/book";
		private const string Endpoints_Spot_TradingPairsTicker = "api/spot/v3/instruments/ticker";
		private const string Endpoints_Spot_TradingPairTicker = "api/spot/v3/instruments/<instrument_id>/ticker";
		private const string Endpoints_Spot_Trades = "api/spot/v3/instruments/<instrument_id>/trades";
		private const string Endpoints_Spot_Candles = "api/spot/v3/instruments/<instrument_id>/candles";
		private const string Endpoints_Spot_HistoricalCandles = "api/spot/v3/instruments/<instrument_id>/history/candles";
		#endregion

		#endregion
		
		#region Algo Trading API Endpoints

        #region Private Signed Endpoints
		private const string Endpoints_Algo_PlaceOrder = "api/spot/v3/order_algo";
		private const string Endpoints_Algo_CancelOrder = "api/spot/v3/cancel_batch_algos";
		private const string Endpoints_Algo_OrderList = "api/spot/v3/algo";
		#endregion

		#region Public Unsigned Endpoints
		#endregion

		#endregion

		#region Margin Trading API Endpoints

        #region Private Signed Endpoints
		private const string Endpoints_Margin_Accounts = "api/margin/v3/accounts";
		private const string Endpoints_Margin_Account = "api/margin/v3/accounts/<instrument_id>";
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
		private const string Endpoints_Margin_Leverage = "api/margin/v3/accounts/<instrument_id>/leverage"; // Get & Set
		private const string Endpoints_Margin_OrderDetails = "api/margin/v3/orders/<order_id>";
		private const string Endpoints_Margin_OpenOrders = "api/margin/v3/orders_pending";
		private const string Endpoints_Margin_TransactionDetails = "api/margin/v3/fills";
		#endregion

		#region Public Unsigned Endpoints
		private const string Endpoints_Margin_MarkPrice = "api/margin/v3/instruments/<instrument_id>/mark_price";
		#endregion

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

		#region Perpetual Swap API Endpoints
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

		#region Contract Trading API Endpoints
		#endregion

		#region Index API Endpoints
		private const string Endpoints_Index_Constituents = "api/index/v3/<instrument_id>/constituents";
		#endregion

		#region Public-Status API Endpoints
		#endregion

		#region Public-Oracle API Endpoints
		#endregion

		#region Client Enum Strings
		public static readonly string BodyParameterKey = "<BODY>";
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
		public OkexClient(OkexClientOptions options) : base("Okex", options, options.ApiCredentials == null ? null : new OkexAuthenticationProvider(options.ApiCredentials, "", options.SignPublicRequests, ArrayParametersSerialization.Array))
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

		#region Protected & Private Methods
		protected override IRequest ConstructRequest(Uri uri, HttpMethod method, Dictionary<string, object>? parameters, bool signed, PostParameters postPosition, ArrayParametersSerialization arraySerialization, int requestId)
		{
			if (parameters == null)
				parameters = new Dictionary<string, object>();

			var uriString = uri.ToString();
			if (authProvider != null)
				parameters = authProvider.AddAuthenticationToParameters(uriString, method, parameters, signed, postPosition, arraySerialization);

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
			var request = RequestFactory.Create(method, uriString, requestId);
			request.Accept = Constants.JsonContentHeader;

			var headers = new Dictionary<string, string>();
			if (authProvider != null)
				headers = authProvider.AddAuthenticationToHeaders(uriString, method, parameters!, signed, postPosition, arraySerialization);

			foreach (var header in headers)
				request.AddHeader(header.Key, header.Value);

			if ((method == HttpMethod.Post || method == HttpMethod.Put) && postParametersPosition != PostParameters.InUri)
			{
				if (parameters?.Any() == true)
					WriteParamBody(request, parameters, contentType);
				else
					request.SetContent(requestBodyEmptyContent, contentType);
			}

			return request;
		}

		protected override void WriteParamBody(IRequest request, Dictionary<string, object> parameters, string contentType)
		{
			if (requestBodyFormat == RequestBodyFormat.Json)
			{
				if(parameters.Count==1 && parameters.Keys.First() == BodyParameterKey)
                {
					var stringData = JsonConvert.SerializeObject(parameters[BodyParameterKey]);
					request.SetContent(stringData, contentType);
				} else
                {
					var stringData = JsonConvert.SerializeObject(parameters.OrderBy(p => p.Key).ToDictionary(p => p.Key, p => p.Value));
					request.SetContent(stringData, contentType);
				}
			}
			else if (requestBodyFormat == RequestBodyFormat.FormData)
			{
				var formData = HttpUtility.ParseQueryString(string.Empty);
				foreach (var kvp in parameters.OrderBy(p => p.Key))
				{
					if (kvp.Value.GetType().IsArray)
					{
						var array = (Array)kvp.Value;
						foreach (var value in array)
							formData.Add(kvp.Key, value.ToString());
					}
					else
						formData.Add(kvp.Key, kvp.Value.ToString());
				}
				var stringData = formData.ToString();
				request.SetContent(stringData, contentType);
			}
		}

		protected override Error ParseErrorResponse(JToken error)
		{
			if (error["code"] == null || error["message"] == null)
				return new ServerError(error.ToString());

			return new ServerError((int)error["code"]!, (string)error["message"]!);
		}

		protected Uri GetUrl(string endpoint, string param = "")
		{
			var x = endpoint.IndexOf('<');
			var y = endpoint.IndexOf('>');
			if (x > -1 && y > -1) endpoint = endpoint.Replace(endpoint.Substring(x, y - x + 1), param);

			return new Uri($"{BaseAddress.TrimEnd('/')}/{endpoint}");
		}

		private static long ToUnixTimestamp(DateTime time)
		{
			return (long)(time - new DateTime(1970, 1, 1)).TotalMilliseconds;
		}

		#endregion

	}
}
