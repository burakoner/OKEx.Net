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

namespace Okex.Net
{
	public partial class OkexClient
	{
		#region Spot Tradimg API

		#region Public EndPoints
		/// <summary>
		/// This provides snapshots of market data and is publicly accessible without account authentication.
		/// Retrieves list of trading pairs, trading limit, and unit increment.
		/// Rate limit: 20 Requests per 2 seconds
		/// </summary>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public WebCallResult<IEnumerable<OkexSpotPair>> Spot_GetTradingPairs(CancellationToken ct = default) => Spot_GetTradingPairs_Async(ct).Result;
		/// <summary>
		/// This provides snapshots of market data and is publicly accessible without account authentication.
		/// Retrieves list of trading pairs, trading limit, and unit increment.
		/// Rate limit: 20 Requests per 2 seconds
		/// </summary>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public async Task<WebCallResult<IEnumerable<OkexSpotPair>>> Spot_GetTradingPairs_Async(CancellationToken ct = default)
		{
			return await SendRequest<IEnumerable<OkexSpotPair>>(GetUrl(Endpoints_Spot_TradingPairs), HttpMethod.Get, ct).ConfigureAwait(false);
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
		public WebCallResult<OkexSpotOrderBook> Spot_GetOrderBook(string symbol, int? size = null, decimal? depth = null, CancellationToken ct = default) => Spot_GetOrderBook_Async(symbol, size, depth, ct).Result;
		/// <summary>
		/// Retrieve a trading pair's order book. Pagination is not supported here; the entire orderbook will be returned per request. This is publicly accessible without account authentication. WebSocket is recommended here.
		/// Rate limit: 20 requests per 2 seconds
		/// </summary>
		/// <param name="symbol">Trading pair symbol</param>
		/// <param name="size">Number of results per request. Maximum 200</param>
		/// <param name="depth">Aggregation of the order book. e.g . 0.1, 0.001</param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public async Task<WebCallResult<OkexSpotOrderBook>> Spot_GetOrderBook_Async(string symbol, int? size = null, decimal? depth = null, CancellationToken ct = default)
		{
			symbol = symbol.ValidateSymbol();
			size?.ValidateIntBetween(nameof(size), 1, 200);

			var parameters = new Dictionary<string, object>();
			parameters.AddOptionalParameter("size", size);
			parameters.AddOptionalParameter("depth", depth);

			#region Default Method
			// return await SendRequest<OrderBook>(GetUrl(Endpoints_Spot_OrderBook, symbol), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
			#endregion

			#region Modified Method
			var result = await SendRequest<OkexSpotOrderBook>(GetUrl(Endpoints_Spot_OrderBook, symbol), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
			if (!result || result.Data == null)
				return new WebCallResult<OkexSpotOrderBook>(result.ResponseStatusCode, result.ResponseHeaders, default, result.Error);

			if (result.Error != null)
				return new WebCallResult<OkexSpotOrderBook>(result.ResponseStatusCode, result.ResponseHeaders, default, new ServerError(/*result.Error.Code,*/ result.Error.Message));

			result.Data.Symbol = symbol.ToUpper(OkexGlobals.OkexCultureInfo);
			return new WebCallResult<OkexSpotOrderBook>(result.ResponseStatusCode, result.ResponseHeaders, result.Data, null);
			#endregion
		}


		/// <summary>
		/// Retrieve the latest price snapshot, best bid/ask price, and trading volume in the last 24 hours for all trading pairs. This is publicly accessible without account authentication.
		/// Rate limit: 20 requests per 2 seconds
		/// </summary>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public WebCallResult<IEnumerable<OkexSpotTicker>> Spot_GetAllTickers(CancellationToken ct = default) => Spot_GetAllTickers_Async(ct).Result;
		/// <summary>
		/// Retrieve the latest price snapshot, best bid/ask price, and trading volume in the last 24 hours for all trading pairs. This is publicly accessible without account authentication.
		/// Rate limit: 20 requests per 2 seconds
		/// </summary>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public async Task<WebCallResult<IEnumerable<OkexSpotTicker>>> Spot_GetAllTickers_Async(CancellationToken ct = default)
		{
			return await SendRequest<IEnumerable<OkexSpotTicker>>(GetUrl(Endpoints_Spot_TradingPairsTicker), HttpMethod.Get, ct).ConfigureAwait(false);
		}


		/// <summary>
		/// Retrieve the latest price snapshot, best bid/ask price, and trading volume in the last 24 hours for a particular trading pair. This is publicly accessible without account authentication.
		/// Rate limit: 20 requests per 2 seconds
		/// </summary>
		/// <param name="symbol">Trading Pair symbol</param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public WebCallResult<OkexSpotTicker> Spot_GetSymbolTicker(string symbol, CancellationToken ct = default) => Spot_GetSymbolTicker_Async(symbol, ct).Result;
		/// <summary>
		/// Retrieve the latest price snapshot, best bid/ask price, and trading volume in the last 24 hours for a particular trading pair. This is publicly accessible without account authentication.
		/// Rate limit: 20 requests per 2 seconds
		/// </summary>
		/// <param name="symbol">Trading Pair symbol</param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public async Task<WebCallResult<OkexSpotTicker>> Spot_GetSymbolTicker_Async(string symbol, CancellationToken ct = default)
		{
			symbol = symbol.ValidateSymbol();
			return await SendRequest<OkexSpotTicker>(GetUrl(Endpoints_Spot_TradingPairTicker, symbol), HttpMethod.Get, ct).ConfigureAwait(false);
		}


		/// <summary>
		/// Retrieve the latest 60 transactions of all trading pairs.
		/// Rate limit: 20 requests per 2 seconds
		/// </summary>
		/// <param name="symbol">Trading pair symbol</param>
		/// <param name="limit">Number of results per request. The maximum is 60; the default is 60</param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public WebCallResult<IEnumerable<OkexSpotTrade>> Spot_GetTrades(string symbol, int limit = 60, CancellationToken ct = default) => Spot_GetTrades_Async(symbol, limit, ct).Result;
		/// <summary>
		/// Retrieve the latest 60 transactions of all trading pairs.
		/// Rate limit: 20 requests per 2 seconds
		/// </summary>
		/// <param name="symbol">Trading pair symbol</param>
		/// <param name="limit">Number of results per request. The maximum is 60; the default is 60</param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public async Task<WebCallResult<IEnumerable<OkexSpotTrade>>> Spot_GetTrades_Async(string symbol, int limit = 60, CancellationToken ct = default)
		{
			symbol = symbol.ValidateSymbol();
			limit.ValidateIntBetween(nameof(limit), 1, 60);

			var parameters = new Dictionary<string, object>
			{
				{ "limit", limit },
			};

			#region Default Method
			// return await SendRequest<IEnumerable<Trade>>(GetUrl(Endpoints_Spot_Trades, symbol), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
			#endregion

			#region Modified Method
			var result = await SendRequest<IEnumerable<OkexSpotTrade>>(GetUrl(Endpoints_Spot_Trades, symbol), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
			if (!result || result.Data == null)
				return new WebCallResult<IEnumerable<OkexSpotTrade>>(result.ResponseStatusCode, result.ResponseHeaders, default, result.Error);

			if (result.Error != null)
				return new WebCallResult<IEnumerable<OkexSpotTrade>>(result.ResponseStatusCode, result.ResponseHeaders, default, new ServerError(/*result.Error.Code,*/ result.Error.Message));

			foreach (var data in result.Data)
			{
				data.Symbol = symbol.ToUpper(OkexGlobals.OkexCultureInfo);
			}

			return new WebCallResult<IEnumerable<OkexSpotTrade>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data, null);
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
            public WebCallResult<IEnumerable<OkexSpotCandle>> Spot_GetCandles(string symbol, OkexSpotPeriod period, DateTime? start = null, DateTime? end = null, CancellationToken ct = default) => Spot_GetCandles_Async(symbol, period, start, end, ct).Result;
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
		public async Task<WebCallResult<IEnumerable<OkexSpotCandle>>> Spot_GetCandles_Async(string symbol, OkexSpotPeriod period, DateTime? start = null, DateTime? end = null, CancellationToken ct = default)
		{
			symbol = symbol.ValidateSymbol();

			var parameters = new Dictionary<string, object>
			{
				{ "granularity", JsonConvert.SerializeObject(period, new SpotPeriodConverter(false)) },
			};
			parameters.AddOptionalParameter("start", start?.DateTimeToIso8601String());
			parameters.AddOptionalParameter("end", end?.DateTimeToIso8601String());

			#region Default Method
			// return await SendRequest<IEnumerable<Candle>>(GetUrl(Endpoints_Spot_Candles, symbol), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
			#endregion

			#region Modified Method
			var result = await SendRequest<IEnumerable<OkexSpotCandle>>(GetUrl(Endpoints_Spot_Candles, symbol), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
			if (!result || result.Data == null)
				return new WebCallResult<IEnumerable<OkexSpotCandle>>(result.ResponseStatusCode, result.ResponseHeaders, default, result.Error);

			if (result.Error != null)
				return new WebCallResult<IEnumerable<OkexSpotCandle>>(result.ResponseStatusCode, result.ResponseHeaders, default, new ServerError(/*result.Error.Code,*/ result.Error.Message));

			foreach (var data in result.Data)
			{
				data.Symbol = symbol.ToUpper(OkexGlobals.OkexCultureInfo);
			}

			return new WebCallResult<IEnumerable<OkexSpotCandle>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data, null);
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
		public WebCallResult<IEnumerable<OkexSpotAccount>> Spot_GetAllBalances(CancellationToken ct = default) => Spot_GetAllBalances_Async(ct).Result;
		/// <summary>
		/// This retrieves the list of assets, (with non-zero balance), remaining balance, and amount available in the spot trading account.
		/// Rate limit: 20 requests per 2 seconds
		/// </summary>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public async Task<WebCallResult<IEnumerable<OkexSpotAccount>>> Spot_GetAllBalances_Async(CancellationToken ct = default)
		{
			return await SendRequest<IEnumerable<OkexSpotAccount>>(GetUrl(Endpoints_Spot_Accounts), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
		}


		/// <summary>
		/// This retrieves information for a single currency in your account, including the remaining balance, and the amount available or on hold.
		/// Rate Limit: 20 requests per 2 seconds
		/// </summary>
		/// <param name="symbol">Token symbol</param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public WebCallResult<OkexSpotAccount> Spot_GetSymbolBalance(string currency, CancellationToken ct = default) => Spot_GetSymbolBalance_Async(currency, ct).Result;
		/// <summary>
		/// This retrieves information for a single currency in your account, including the remaining balance, and the amount available or on hold.
		/// Rate Limit: 20 requests per 2 seconds
		/// </summary>
		/// <param name="symbol">Token symbol</param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public async Task<WebCallResult<OkexSpotAccount>> Spot_GetSymbolBalance_Async(string currency, CancellationToken ct = default)
		{
			currency = currency.ValidateCurrency();
			return await SendRequest<OkexSpotAccount>(GetUrl(Endpoints_Spot_Account, currency), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
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
		public WebCallResult<IEnumerable<OkexSpotBill>> Spot_GetSymbolBills(string currency, OkexSpotBillType? type = null, int limit = 100, int? before = null, int? after = null, CancellationToken ct = default) => Spot_GetSymbolBills_Async(currency, type, limit, before, after, ct).Result;
		/// <summary>
		/// This retrieves the spot account bills dating back the past 3 months. Pagination is supported and the response is sorted with most recent first in reverse chronological order.
		/// Rate Limit: 20 requests per 2 seconds
		/// <param name="currency">The token symbol, e.g. 'BTC'. Complete account statement for will be returned if the field is left blank</param>
		/// <param name="type">Bill Type of Transaction</param>
		/// <param name="limit">Number of results per request. The maximum is 100; the default is 100</param>
		/// <param name="before">Pagination of data to return records newer than the requested ledger_id</param>
		/// <param name="after">Pagination of data to return records earlier than the requested ledger_id</param>
		/// <param name="ct">Cancellation Token</param>
		public async Task<WebCallResult<IEnumerable<OkexSpotBill>>> Spot_GetSymbolBills_Async(string currency, OkexSpotBillType? type = null, int limit = 100, int? before = null, int? after = null, CancellationToken ct = default)
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

			return await SendRequest<IEnumerable<OkexSpotBill>>(GetUrl(Endpoints_Spot_Bills, currency), HttpMethod.Get, ct, parameters, signed: true).ConfigureAwait(false);
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
		public WebCallResult<OkexSpotPlacedOrder> Spot_PlaceOrder(
			string symbol,
			OkexSpotOrderSide side,
			OkexSpotOrderType type,
			OkexSpotTimeInForce timeInForce = OkexSpotTimeInForce.NormalOrder,
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
		public async Task<WebCallResult<OkexSpotPlacedOrder>> Spot_PlaceOrder_Async(
			string symbol,
			OkexSpotOrderSide side,
			OkexSpotOrderType type,
			OkexSpotTimeInForce timeInForce = OkexSpotTimeInForce.NormalOrder,
			decimal? price = null,
			decimal? size = null,
			decimal? notional = null,
			string? clientOrderId = null,
			CancellationToken ct = default)
		{
			symbol = symbol.ValidateSymbol();
			clientOrderId?.ValidateStringLength("clientOrderId", 0, 32);

			if (type == OkexSpotOrderType.Limit && (price == null || size == null))
				throw new ArgumentException("Both price and size must be provided for Limit Orders");

			if (type == OkexSpotOrderType.Market && side == OkexSpotOrderSide.Buy && notional == null)
				throw new ArgumentException("Notional must be provided for Market Buy Orders");

			if (type == OkexSpotOrderType.Market && side == OkexSpotOrderSide.Sell && size == null)
				throw new ArgumentException("Size must be provided for Market Sell Orders");

			if (type == OkexSpotOrderType.Market && timeInForce != OkexSpotTimeInForce.NormalOrder)
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

			return await SendRequest<OkexSpotPlacedOrder>(GetUrl(Endpoints_Spot_PlaceOrder), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
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
		public WebCallResult<OkexSpotPlacedOrder> Spot_CancelOrder(string symbol, long? orderId = null, string? clientOrderId = null, CancellationToken ct = default) => Spot_CancelOrder_Async(symbol, orderId, clientOrderId, ct).Result;
		/// <summary>
		/// This is used to cancel an unfilled order.
		/// Rate limit: 100 requests per 2 seconds
		/// </summary>
		/// <param name="symbol">Specify the trading pair to cancel the corresponding order. An error would be returned if the parameter is not provided.</param>
		/// <param name="orderId">Either client_oids or order_ids must be present. Order ID</param>
		/// <param name="clientOrderId">Either client_oids or order_ids must be present. Client-supplied order ID that you can customize. It should be comprised of alpha-numeric characters with length 1 to 32. Both uppercase and lowercase are supported</param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public async Task<WebCallResult<OkexSpotPlacedOrder>> Spot_CancelOrder_Async(string symbol, long? orderId = null, string? clientOrderId = null, CancellationToken ct = default)
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

			return await SendRequest<OkexSpotPlacedOrder>(GetUrl(Endpoints_Spot_CancelOrder, orderId.HasValue ? orderId.ToString() : clientOrderId!), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
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
		public WebCallResult<IEnumerable<OkexSpotOrderDetails>> Spot_GetAllOrders(string symbol, OkexSpotOrderState state, int limit = 100, int? before = null, int? after = null, CancellationToken ct = default) => Spot_GetAllOrders_Async(symbol, state, limit, before, after, ct).Result;
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
		public async Task<WebCallResult<IEnumerable<OkexSpotOrderDetails>>> Spot_GetAllOrders_Async(string symbol, OkexSpotOrderState state, int limit = 100, int? before = null, int? after = null, CancellationToken ct = default)
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

			return await SendRequest<IEnumerable<OkexSpotOrderDetails>>(GetUrl(Endpoints_Spot_OrderList), HttpMethod.Get, ct, parameters, signed: true).ConfigureAwait(false);
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
		public WebCallResult<IEnumerable<OkexSpotOrderDetails>> Spot_GetOpenOrders(string symbol, int limit = 100, int? before = null, int? after = null, CancellationToken ct = default) => Spot_GetOpenOrders_Async(symbol, limit, before, after, ct).Result;
		/// <summary>
		/// This retrieves the list of your current open orders. Pagination is supported and the response is sorted with most recent first in reverse chronological order.
		/// </summary>
		/// <param name="symbol">Trading pair symbol</param>
		/// <param name="limit">Number of results per request. The maximum is 100; the default is 100</param>
		/// <param name="before">Pagination of data to return records newer than the requested order_id</param>
		/// <param name="after">Pagination of data to return records earlier than the requested order_id</param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public async Task<WebCallResult<IEnumerable<OkexSpotOrderDetails>>> Spot_GetOpenOrders_Async(string symbol, int limit = 100, int? before = null, int? after = null, CancellationToken ct = default)
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

			return await SendRequest<IEnumerable<OkexSpotOrderDetails>>(GetUrl(Endpoints_Spot_OpenOrders), HttpMethod.Get, ct, parameters, signed: true).ConfigureAwait(false);
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
		public WebCallResult<OkexSpotOrderDetails> Spot_GetOrderDetails(string symbol, long? orderId = null, string? clientOrderId = null, CancellationToken ct = default) => Spot_GetOrderDetails_Async(symbol, orderId, clientOrderId, ct).Result;
		/// <summary>
		/// Retrieve order details by order ID.Can get order information for nearly 3 months。 Unfilled orders will be kept in record for only two hours after it is canceled.
		/// Rate limit: 20 requests per 2 seconds
		/// </summary>
		/// <param name="symbol">Trading pair symbol</param>
		/// <param name="orderId">Order ID Either client_oid or order_id must be present.</param>
		/// <param name="clientOrderId">Client-supplied order ID Either client_oid or order_id must be present.</param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public async Task<WebCallResult<OkexSpotOrderDetails>> Spot_GetOrderDetails_Async(string symbol, long? orderId = null, string? clientOrderId = null, CancellationToken ct = default)
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

			return await SendRequest<OkexSpotOrderDetails>(GetUrl(Endpoints_Spot_OrderDetails, orderId.HasValue ? orderId.ToString() : clientOrderId!), HttpMethod.Get, ct, parameters, signed: true).ConfigureAwait(false);
		}


		/// <summary>
		/// Obtain the transaction fee rate corresponding to your current account transaction level. The sub-account rate under the parent account is the same as the parent account. Update every day at 0am
		/// Rate limit: 1 requests per 10 seconds
		/// </summary>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public WebCallResult<OkexSpotTradingFee> Spot_TradeFeeRates(CancellationToken ct = default) => Spot_TradeFeeRates_Async(ct).Result;
		/// <summary>
		/// Obtain the transaction fee rate corresponding to your current account transaction level. The sub-account rate under the parent account is the same as the parent account. Update every day at 0am
		/// Rate limit: 1 requests per 10 seconds
		/// </summary>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public async Task<WebCallResult<OkexSpotTradingFee>> Spot_TradeFeeRates_Async(CancellationToken ct = default)
		{
			return await SendRequest<OkexSpotTradingFee>(GetUrl(Endpoints_Spot_TradeFee), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
		}

		#endregion

		#endregion
	}
}
