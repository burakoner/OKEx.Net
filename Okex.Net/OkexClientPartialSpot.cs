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
using System.Linq;
using Okex.Net.Enums;

namespace Okex.Net
{
	public partial class OkexClient
	{
		#region Spot Tradimg API

		#region Private Signed Endpoints
		/// <summary>
		/// This retrieves the list of assets, (with non-zero balance), remaining balance, and amount available in the spot trading account.
		/// Rate limit: 20 requests per 2 seconds
		/// </summary>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public WebCallResult<IEnumerable<OkexSpotBalance>> Spot_GetAllBalances(CancellationToken ct = default) => Spot_GetAllBalances_Async(ct).Result;
		/// <summary>
		/// This retrieves the list of assets, (with non-zero balance), remaining balance, and amount available in the spot trading account.
		/// Rate limit: 20 requests per 2 seconds
		/// </summary>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public async Task<WebCallResult<IEnumerable<OkexSpotBalance>>> Spot_GetAllBalances_Async(CancellationToken ct = default)
		{
			return await SendRequest<IEnumerable<OkexSpotBalance>>(GetUrl(Endpoints_Spot_Accounts), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
		}

		/// <summary>
		/// This retrieves information for a single currency in your account, including the remaining balance, and the amount available or on hold.
		/// Rate Limit: 20 requests per 2 seconds
		/// </summary>
		/// <param name="symbol">Token symbol</param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public WebCallResult<OkexSpotBalance> Spot_GetSymbolBalance(string currency, CancellationToken ct = default) => Spot_GetSymbolBalance_Async(currency, ct).Result;
		/// <summary>
		/// This retrieves information for a single currency in your account, including the remaining balance, and the amount available or on hold.
		/// Rate Limit: 20 requests per 2 seconds
		/// </summary>
		/// <param name="symbol">Token symbol</param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public async Task<WebCallResult<OkexSpotBalance>> Spot_GetSymbolBalance_Async(string currency, CancellationToken ct = default)
		{
			currency = currency.ValidateCurrency();
			return await SendRequest<OkexSpotBalance>(GetUrl(Endpoints_Spot_Account, currency), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
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
		public WebCallResult<IEnumerable<OkexSpotBill>> Spot_GetSymbolBills(string currency, OkexSpotBillType? type = null, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default) => Spot_GetSymbolBills_Async(currency, type, limit, before, after, ct).Result;
		/// <summary>
		/// This retrieves the spot account bills dating back the past 3 months. Pagination is supported and the response is sorted with most recent first in reverse chronological order.
		/// Rate Limit: 20 requests per 2 seconds
		/// <param name="currency">The token symbol, e.g. 'BTC'. Complete account statement for will be returned if the field is left blank</param>
		/// <param name="type">Bill Type of Transaction</param>
		/// <param name="limit">Number of results per request. The maximum is 100; the default is 100</param>
		/// <param name="before">Pagination of data to return records newer than the requested ledger_id</param>
		/// <param name="after">Pagination of data to return records earlier than the requested ledger_id</param>
		/// <param name="ct">Cancellation Token</param>
		public async Task<WebCallResult<IEnumerable<OkexSpotBill>>> Spot_GetSymbolBills_Async(string currency, OkexSpotBillType? type = null, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default)
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
		public WebCallResult<OkexSpotPlacedOrder> Spot_PlaceOrder(OkexSpotPlaceOrder order, CancellationToken ct = default) => Spot_PlaceOrder_Async(order.Symbol, order.Side, order.Type, order.TimeInForce, order.Price, order.Size, order.Notional, order.ClientOrderId, ct = default).Result;
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
		public Task<WebCallResult<OkexSpotPlacedOrder>> Spot_PlaceOrder_Async(OkexSpotPlaceOrder order, CancellationToken ct = default) => Spot_PlaceOrder_Async(order.Symbol, order.Side, order.Type, order.TimeInForce, order.Price, order.Size, order.Notional, order.ClientOrderId, ct = default);
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
		public WebCallResult<OkexSpotPlacedOrder> Spot_PlaceOrder(string symbol, OkexSpotOrderSide side, OkexSpotOrderType type, OkexSpotTimeInForce timeInForce = OkexSpotTimeInForce.NormalOrder, decimal? price = null, decimal? size = null, decimal? notional = null, string? clientOrderId = null, CancellationToken ct = default) => Spot_PlaceOrder_Async(symbol, side, type, timeInForce, price, size, notional, clientOrderId, ct = default).Result;
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
		public async Task<WebCallResult<OkexSpotPlacedOrder>> Spot_PlaceOrder_Async(string symbol, OkexSpotOrderSide side, OkexSpotOrderType type, OkexSpotTimeInForce timeInForce = OkexSpotTimeInForce.NormalOrder, decimal? price = null, decimal? size = null, decimal? notional = null, string? clientOrderId = null, CancellationToken ct = default)
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
		/// This supports placing multiple orders in batches for up to 4 trading pairs and a maximum of 10 orders per trading pair can be placed at a time.
		/// Rate limit: 50 requests per 2 seconds （The speed limit is accumulated between different trading pair symbols)
		/// Notes:
		/// - The client_oid is optional and you can customize it using alpha-numeric characters with length 1 to 32. No warning is sent when client_oid is not unique. In case of multiple identical client_oid, only the latest entry will be returned.
		/// - You may place batch orders for up to 4 trading pairs, each with 10 orders at maximum. If you cancel the batch orders, you should confirm they are successfully canceled by requesting the "Get Order List" endpoint.
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
		/// <returns>Symbol grouped order results. Dictionary&lt;string: symbol, IEnumerable&lt;OkexSpotPlacedOrder&gt;: order results&gt;</returns>
		public WebCallResult<Dictionary<string, IEnumerable<OkexSpotPlacedOrder>>> Spot_BatchPlaceOrders(IEnumerable<OkexSpotPlaceOrder> orders, CancellationToken ct = default) => Spot_BatchPlaceOrders_Async(orders, ct = default).Result;
		/// <summary>
		/// This supports placing multiple orders in batches for up to 4 trading pairs and a maximum of 10 orders per trading pair can be placed at a time.
		/// Rate limit: 50 requests per 2 seconds （The speed limit is accumulated between different trading pair symbols)
		/// Notes:
		/// - The client_oid is optional and you can customize it using alpha-numeric characters with length 1 to 32. No warning is sent when client_oid is not unique. In case of multiple identical client_oid, only the latest entry will be returned.
		/// - You may place batch orders for up to 4 trading pairs, each with 10 orders at maximum. If you cancel the batch orders, you should confirm they are successfully canceled by requesting the "Get Order List" endpoint.
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
		/// <returns>Symbol grouped order results. Dictionary&lt;string: symbol, IEnumerable&lt;OkexSpotPlacedOrder&gt;: order results&gt;</returns>
		public async Task<WebCallResult<Dictionary<string, IEnumerable<OkexSpotPlacedOrder>>>> Spot_BatchPlaceOrders_Async(IEnumerable<OkexSpotPlaceOrder> orders, CancellationToken ct = default)
		{
			if (orders == null || orders.Count() == 0)
				throw new ArgumentException("Orders cant be null or with zero-elements");

			for (var i = 0; i < orders.Count(); i++)
			{
				var order = orders.ElementAt(i);
				var suffix = $"(Order: {(i + 1)} of {orders.Count()})";
				order.Symbol = order.Symbol.ValidateSymbol(messageSuffix: suffix);
				order.ClientOrderId?.ValidateStringLength("clientOrderId", 0, 32, messageSuffix: suffix);

				if (order.Type == OkexSpotOrderType.Limit && (order.Price == null || order.Size == null))
					throw new ArgumentException($"Both price and size must be provided for Limit Orders {suffix}");

				if (order.Type == OkexSpotOrderType.Market && order.Side == OkexSpotOrderSide.Buy && order.Notional == null)
					throw new ArgumentException($"Notional must be provided for Market Buy Orders {suffix}");

				if (order.Type == OkexSpotOrderType.Market && order.Side == OkexSpotOrderSide.Sell && order.Size == null)
					throw new ArgumentException($"Size must be provided for Market Sell Orders {suffix}");

				if (order.Type == OkexSpotOrderType.Market && order.TimeInForce != OkexSpotTimeInForce.NormalOrder)
					throw new ArgumentException($"When placing market orders, TimeInForce must be Normal Order {suffix}");

				if (order.ClientOrderId != null && !Regex.IsMatch(order.ClientOrderId, "^(([a-z]|[A-Z]|[0-9]){0,32})$"))
					throw new ArgumentException($"ClientOrderId supports alphabets (case-sensitive) + numbers, or letters (case-sensitive) between 1-32 characters. {suffix}");

				// Spot Trading Flag
				order.MarginTrading = null;
			}

			var parameters = new Dictionary<string, object>
			{
				{ BodyParameterKey, orders },
			};

			return await SendRequest<Dictionary<string, IEnumerable<OkexSpotPlacedOrder>>>(GetUrl(Endpoints_Spot_PlaceBatchOrders), HttpMethod.Post, ct, parameters, arraySerialization: ArrayParametersSerialization.Array, signed: true).ConfigureAwait(false);
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
		/// Cancel multiple open orders with order_id or client_oid. Up to 4 trading pairs, and maximum 10 orders can be canceled at a time for each trading pair.
		/// Rate limit: 20 requests per 2 seconds （The speed limit is accumulated between different trading pair symbols)
		///	Notes:
		///	- Either client_oids or order_ids must be present.
		///	- For batch order cancellation, only one of order_id or client_oid parameters should be passed per request. Otherwise an error will be returned.
		///	- When using client_oid for batch order cancellation, only one client_oid is canceled per trading pair, and up to a maximum of 4 trading pairs can be processed per request. You need to make sure the ID is unique. In case of multiple identical client_oid, only the latest entry will be returned.
		///	- Using order_id you may cancel orders for up to 4 trading pairs, each with 10 orders at maximum. After placing a cancel order you should confirm they are successfully canceled by requesting the "Get Order List" endpoint.
		///	- Cancellations of orders are not guaranteed. After placing a cancel order you should confirm they are successfully canceled by requesting the "Get Order List" endpoint.
		/// </summary>
		/// <param name="orders">
		///	OrderIds: Order ID List
		///	ClientOrderIds: Client-supplied order ID that you can customize. It should be comprised of alpha-numeric characters with length 1 to 32. Both uppercase and lowercase are supported
		/// </param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns>Symbol grouped order results. Dictionary&lt;string: symbol, IEnumerable&lt;OkexSpotPlacedOrder&gt;: order results&gt;</returns>
		public WebCallResult<Dictionary<string, IEnumerable<OkexSpotPlacedOrder>>> Spot_BatchCancelOrders(IEnumerable<OkexSpotCancelOrder> orders, CancellationToken ct = default) => Spot_BatchCancelOrders_Async(orders, ct).Result;
		/// <summary>
		/// Cancel multiple open orders with order_id or client_oid. Up to 4 trading pairs, and maximum 10 orders can be canceled at a time for each trading pair.
		/// Rate limit: 20 requests per 2 seconds （The speed limit is accumulated between different trading pair symbols)
		///	Notes:
		///	- Either client_oids or order_ids must be present.
		///	- For batch order cancellation, only one of order_id or client_oid parameters should be passed per request. Otherwise an error will be returned.
		///	- When using client_oid for batch order cancellation, only one client_oid is canceled per trading pair, and up to a maximum of 4 trading pairs can be processed per request. You need to make sure the ID is unique. In case of multiple identical client_oid, only the latest entry will be returned.
		///	- Using order_id you may cancel orders for up to 4 trading pairs, each with 10 orders at maximum. After placing a cancel order you should confirm they are successfully canceled by requesting the "Get Order List" endpoint.
		///	- Cancellations of orders are not guaranteed. After placing a cancel order you should confirm they are successfully canceled by requesting the "Get Order List" endpoint.
		/// </summary>
		/// <param name="orders">
		///	OrderIds: Order ID List
		///	ClientOrderIds: Client-supplied order ID that you can customize. It should be comprised of alpha-numeric characters with length 1 to 32. Both uppercase and lowercase are supported
		/// </param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns>Symbol grouped order results. Dictionary&lt;string: symbol, IEnumerable&lt;OkexSpotPlacedOrder&gt;: order results&gt;</returns>
		public async Task<WebCallResult<Dictionary<string, IEnumerable<OkexSpotPlacedOrder>>>> Spot_BatchCancelOrders_Async(IEnumerable<OkexSpotCancelOrder> orders, CancellationToken ct = default)
		{
			if (orders == null || orders.Count() == 0)
				throw new ArgumentException("Orders cant be null or with zero-elements");

			for (var i = 0; i < orders.Count(); i++)
			{
				var order = orders.ElementAt(i);
				var suffix = $"(Order: {(i + 1)} of {orders.Count()})";
				order.Symbol = order.Symbol.ValidateSymbol(messageSuffix: suffix);

				if (order.OrderIds != null && order.OrderIds.Count() > 0 && order.ClientOrderIds != null && order.ClientOrderIds.Count() > 0)
					throw new ArgumentException($"Either orderIds or clientOrderIds must be present. {suffix}");
			}

			var parameters = new Dictionary<string, object>
			{
				{ BodyParameterKey, orders },
			};

			return await SendRequest<Dictionary<string, IEnumerable<OkexSpotPlacedOrder>>>(GetUrl(Endpoints_Spot_CancelBatchOrders), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
		}

		/// <summary>
		/// Modify an unfilled order
		/// Rate Limit：40 Requests per 2 seconds
		/// Notes:
		/// - If the error_code value in response is NOT zero, then it means the request was rejected because verification failed.
		/// - In order amendment, only order_id will be used if both order_id and client_oid are passed in values at the same time, and client_oid will be ignored.
		/// - The client_oid should be unique. No warning is sent when client_oid is not unique.
		/// - In case of multiple identical client_oid, only the latest entry will be returned.
		/// - If the order cannot be modified because it has already been filled or canceled, the reason will be returned with the error message.
		/// </summary>
		/// <param name="symbol">Contract ID,e.g. BTC-USDT</param>
		/// <param name="orderId">Either client_oid or order_id must be present. Order ID.</param>
		/// <param name="clientOrderId">Either client_oid or order_id must be present. client_oid should be the same Client-supplied order ID when submitting the order. It should be comprised of alpha-numeric characters with length 1 to 32. Both uppercase and lowercase are supported.</param>
		/// <param name="requestId">You can provide the request_id. If provided, the response will include the corresponding request_id to help you identify the request. It should be comprised of alpha-numeric characters with length 1 to 32. Both uppercase and lowercase are supported.</param>
		/// <param name="newSize">Must provide at least one of new_size or new_price. When modifying a partially filled order, the new_size should include the amount that has been filled.</param>
		/// <param name="newPrice">Must provide at least one of new_size or new_price. Modifies the price.</param>
		/// <param name="cancelOnFail">When the order amendment fails, whether to cancell the order automatically: 0: Don't cancel the order automatically 1: Automatically cancel the order. The default value is 0.</param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public WebCallResult<OkexSpotPlacedOrder> Spot_ModifyOrder(string symbol, long? orderId = null, string? clientOrderId = null, string? requestId = null, decimal? newSize = null, decimal? newPrice = null, bool? cancelOnFail = null, CancellationToken ct = default) => Spot_ModifyOrder_Async(symbol, orderId, clientOrderId, requestId, newSize, newPrice, cancelOnFail, ct).Result;
		/// <summary>
		/// Modify an unfilled order
		/// Rate Limit：40 Requests per 2 seconds
		/// Notes:
		/// - If the error_code value in response is NOT zero, then it means the request was rejected because verification failed.
		/// - In order amendment, only order_id will be used if both order_id and client_oid are passed in values at the same time, and client_oid will be ignored.
		/// - The client_oid should be unique. No warning is sent when client_oid is not unique.
		/// - In case of multiple identical client_oid, only the latest entry will be returned.
		/// - If the order cannot be modified because it has already been filled or canceled, the reason will be returned with the error message.
		/// </summary>
		/// <param name="symbol">Contract ID,e.g. BTC-USDT</param>
		/// <param name="orderId">Either client_oid or order_id must be present. Order ID.</param>
		/// <param name="clientOrderId">Either client_oid or order_id must be present. client_oid should be the same Client-supplied order ID when submitting the order. It should be comprised of alpha-numeric characters with length 1 to 32. Both uppercase and lowercase are supported.</param>
		/// <param name="requestId">You can provide the request_id. If provided, the response will include the corresponding request_id to help you identify the request. It should be comprised of alpha-numeric characters with length 1 to 32. Both uppercase and lowercase are supported.</param>
		/// <param name="newSize">Must provide at least one of new_size or new_price. When modifying a partially filled order, the new_size should include the amount that has been filled.</param>
		/// <param name="newPrice">Must provide at least one of new_size or new_price. Modifies the price.</param>
		/// <param name="cancelOnFail">When the order amendment fails, whether to cancell the order automatically: 0: Don't cancel the order automatically 1: Automatically cancel the order. The default value is 0.</param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public async Task<WebCallResult<OkexSpotPlacedOrder>> Spot_ModifyOrder_Async(string symbol, long? orderId = null, string? clientOrderId = null, string? requestId = null, decimal? newSize = null, decimal? newPrice = null, bool? cancelOnFail = null, CancellationToken ct = default)
		{
			symbol = symbol.ValidateSymbol();

			if (orderId == null && string.IsNullOrEmpty(clientOrderId))
				throw new ArgumentException("Either orderId or clientOrderId must be present.");

			if (orderId != null && !string.IsNullOrEmpty(clientOrderId))
				throw new ArgumentException("Either orderId or clientOrderId must be present.");

			if (!newSize.HasValue && !newPrice.HasValue)
				throw new ArgumentException("Must provide at least one of new_size or new_price");

			var parameters = new Dictionary<string, object>
			{
				{ "instrument_id", symbol },
			};
			if (orderId.HasValue) parameters.AddOptionalParameter("order_id", orderId);
			if (!string.IsNullOrEmpty(clientOrderId)) parameters.AddOptionalParameter("client_oid", clientOrderId);
			if (cancelOnFail.HasValue) parameters.AddOptionalParameter("cancel_on_fail", cancelOnFail.Value ? 1 : 0);
			if (!string.IsNullOrEmpty(requestId)) parameters.AddOptionalParameter("request_id", requestId);
			if (newSize.HasValue) parameters.AddOptionalParameter("new_size", newSize);
			if (newPrice.HasValue) parameters.AddOptionalParameter("new_price", newPrice);

			return await SendRequest<OkexSpotPlacedOrder>(GetUrl(Endpoints_Spot_ModifyOrder, symbol), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
		}

		/// <summary>
		/// Batch modify open orders; a maximum of 10 orders per underlying can be modified.
		/// Rate Limit：20 Requests per 2 seconds
		/// Notes:
		/// - If the error_code value in response is NOT zero, then it means the request was rejected because verification failed.
		/// - When using client_oid for batch order modifications, you need to make sure the ID is unique. In case of multiple identical client_oid, only the latest entry will be returned.
		/// - Modifications of orders are not guaranteed. After placing a modification order you should confirm they are successfully modified by requesting the "Order List" endpoint.
		/// </summary>
		/// <param name="orders">Order List</param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public WebCallResult<Dictionary<string, IEnumerable<OkexSpotPlacedOrder>>> Spot_BatchModifyOrders(IEnumerable<OkexSpotModifyOrder> orders, CancellationToken ct = default) => Spot_BatchModifyOrders_Async(orders, ct).Result;
		/// <summary>
		/// Batch modify open orders; a maximum of 10 orders per underlying can be modified.
		/// Rate Limit：20 Requests per 2 seconds
		/// Notes:
		/// - If the error_code value in response is NOT zero, then it means the request was rejected because verification failed.
		/// - When using client_oid for batch order modifications, you need to make sure the ID is unique. In case of multiple identical client_oid, only the latest entry will be returned.
		/// - Modifications of orders are not guaranteed. After placing a modification order you should confirm they are successfully modified by requesting the "Order List" endpoint.
		/// </summary>
		/// <param name="orders">Order List</param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public async Task<WebCallResult<Dictionary<string, IEnumerable<OkexSpotPlacedOrder>>>> Spot_BatchModifyOrders_Async(IEnumerable<OkexSpotModifyOrder> orders, CancellationToken ct = default)
		{
			if (orders == null || orders.Count() == 0)
				throw new ArgumentException("Orders cant be null or with zero-elements");

			for (var i = 0; i < orders.Count(); i++)
			{
				var order = orders.ElementAt(i);
				var suffix = $"(Order: {(i + 1)} of {orders.Count()})";
				order.Symbol = order.Symbol.ValidateSymbol(messageSuffix: suffix);

				if (order.OrderId == null && string.IsNullOrEmpty(order.ClientOrderId))
					throw new ArgumentException($"Either orderId or clientOrderId must be present. {suffix}");

				if (order.OrderId != null && !string.IsNullOrEmpty(order.ClientOrderId))
					throw new ArgumentException($"Either orderId or clientOrderId must be present. {suffix}");

				if (!order.NewSize.HasValue && !order.NewPrice.HasValue)
					throw new ArgumentException($"Must provide at least one of new_size or new_price. {suffix}");
			}

			var parameters = new Dictionary<string, object>
			{
				{ BodyParameterKey, orders },
			};

			return await SendRequest<Dictionary<string, IEnumerable<OkexSpotPlacedOrder>>>(GetUrl(Endpoints_Spot_BatchModifyOrders), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
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
		public WebCallResult<IEnumerable<OkexSpotOrderDetails>> Spot_GetAllOrders(string symbol, OkexSpotOrderState state, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default) => Spot_GetAllOrders_Async(symbol, state, limit, before, after, ct).Result;
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
		public async Task<WebCallResult<IEnumerable<OkexSpotOrderDetails>>> Spot_GetAllOrders_Async(string symbol, OkexSpotOrderState state, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default)
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
		public WebCallResult<IEnumerable<OkexSpotOrderDetails>> Spot_GetOpenOrders(string symbol, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default) => Spot_GetOpenOrders_Async(symbol, limit, before, after, ct).Result;
		/// <summary>
		/// This retrieves the list of your current open orders. Pagination is supported and the response is sorted with most recent first in reverse chronological order.
		/// </summary>
		/// <param name="symbol">Trading pair symbol</param>
		/// <param name="limit">Number of results per request. The maximum is 100; the default is 100</param>
		/// <param name="before">Pagination of data to return records newer than the requested order_id</param>
		/// <param name="after">Pagination of data to return records earlier than the requested order_id</param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public async Task<WebCallResult<IEnumerable<OkexSpotOrderDetails>>> Spot_GetOpenOrders_Async(string symbol, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default)
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

		/// <summary>
		/// Retrieve recently filled transaction details. This request supports paging and is stored according to the transaction time in chronological order from latest to earliest. Data for up to 3 months can be retrieved.
		/// Rate limit: 10 requests per 2 seconds
		/// Notes:
		/// - This API will return 2 pieces of data after a transaction is complete. One is calculated based on the quote currency, while the other is calculated based on the base currency.
		/// - Transaction Fees: New status for spot trading transaction details: fee is either a positive number (invitation rebate) or a negative number (transaction fee deduction).
		/// - Liquidity: The exec_type specifies whether the order is maker or taker. ‘M’ stands for Maker and ‘T’ stands for Taker.
		/// - Pagination: The ledger_id is listed in a descending order, from biggest to smallest. The first ledger_id in this page can be found under OK-BEFORE, and the last one can be found under OK-AFTER. It would be easier to retrieve to other ledger_id by referring to these two parameters.
		/// </summary>
		/// <param name="symbol">Trading pair symbol</param>
		/// <param name="orderId">Order ID, Complete transaction details for will be returned if the order_id is left blank</param>
		/// <param name="limit">Number of results per request. The maximum is 100; the default is 100</param>
		/// <param name="before">Pagination of data to return records newer than the requested ledger_id</param>
		/// <param name="after">Pagination of data to return records earlier than the requested ledger_id</param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public WebCallResult<IEnumerable<OkexSpotTransaction>> Spot_GetTransactionDetails(string symbol, long? orderId = null, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default) => Spot_GetTransactionDetails_Async(symbol, orderId, limit, before, after, ct).Result;
		/// <summary>
		/// Retrieve recently filled transaction details. This request supports paging and is stored according to the transaction time in chronological order from latest to earliest. Data for up to 3 months can be retrieved.
		/// Rate limit: 10 requests per 2 seconds
		/// Notes:
		/// - This API will return 2 pieces of data after a transaction is complete. One is calculated based on the quote currency, while the other is calculated based on the base currency.
		/// - Transaction Fees: New status for spot trading transaction details: fee is either a positive number (invitation rebate) or a negative number (transaction fee deduction).
		/// - Liquidity: The exec_type specifies whether the order is maker or taker. ‘M’ stands for Maker and ‘T’ stands for Taker.
		/// - Pagination: The ledger_id is listed in a descending order, from biggest to smallest. The first ledger_id in this page can be found under OK-BEFORE, and the last one can be found under OK-AFTER. It would be easier to retrieve to other ledger_id by referring to these two parameters.
		/// </summary>
		/// <param name="symbol">Trading pair symbol</param>
		/// <param name="orderId">Order ID, Complete transaction details for will be returned if the order_id is left blank</param>
		/// <param name="limit">Number of results per request. The maximum is 100; the default is 100</param>
		/// <param name="before">Pagination of data to return records newer than the requested ledger_id</param>
		/// <param name="after">Pagination of data to return records earlier than the requested ledger_id</param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public async Task<WebCallResult<IEnumerable<OkexSpotTransaction>>> Spot_GetTransactionDetails_Async(string symbol, long? orderId = null, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default)
		{
			symbol = symbol.ValidateSymbol();
			limit.ValidateIntBetween(nameof(limit), 1, 100);

			var parameters = new Dictionary<string, object>
			{
				{ "instrument_id", symbol },
				{ "limit", limit },
			};
			parameters.AddOptionalParameter("order_id", orderId);
			parameters.AddOptionalParameter("before", before);
			parameters.AddOptionalParameter("after", after);

			return await SendRequest<IEnumerable<OkexSpotTransaction>>(GetUrl(Endpoints_Spot_TransactionDetails), HttpMethod.Get, ct, parameters, signed: true).ConfigureAwait(false);
		}

		#endregion

		#region Public Unsigned Endpoints
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
		/// <param name="period">Bar size in seconds, default 60, must be one of [60/180/300/900/1800/3600/7200/14400/21600/43200/86400/604800] or returns error</param>
		/// <param name="start">Start time in ISO 8601</param>
		/// <param name="end">End time in ISO 8601</param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public WebCallResult<IEnumerable<OkexSpotCandle>> Spot_GetCandles(string symbol, OkexSpotPeriod period, DateTime? start = null, DateTime? end = null, CancellationToken ct = default) => Spot_GetCandles_Async(symbol, period, start, end, ct).Result;
		/// <summary>
		/// Retrieve the candlestick charts of the trading pairs. This API can retrieve the latest 2000 entries of data. Candlesticks are returned in groups based on requested granularity. Maximum of 2,000 entries can be retrieved.
		/// Rate limit: 20 requests per 2 seconds
		/// </summary>
		/// <param name="symbol">Trading pairs symbol</param>
		/// <param name="period">Bar size in seconds, default 60, must be one of [60/180/300/900/1800/3600/7200/14400/21600/43200/86400/604800] or returns error</param>
		/// <param name="start">Start time in ISO 8601</param>
		/// <param name="end">End time in ISO 8601</param>
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

		/// <summary>
		/// Retrieve the history candles of the contract.As of now, the historical candels of 9 major currencies are provided: BTC, ETH, LTC, ETC, XRP, EOS, BCH, BSV, TRX.
		/// Rate limit: 20 requests per 2 seconds
		/// </summary>
		/// <param name="symbol">Trading pairs symbol</param>
		/// <param name="period">Bar size in seconds, default 60, must be one of [60/180/300/900/1800/3600/7200/14400/21600/43200/86400/604800] or returns error</param>
		/// <param name="start">Start time in ISO 8601</param>
		/// <param name="end">End time in ISO 8601</param>
		/// <param name="limit">The number of candles returned, the default is 300，and the maximum is 300</param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public WebCallResult<IEnumerable<OkexSpotCandle>> Spot_GetHistoricalCandles(string symbol, OkexSpotPeriod period, DateTime? start = null, DateTime? end = null, int limit = 300, CancellationToken ct = default) => Spot_GetHistoricalCandles_Async(symbol, period, start, end, limit, ct).Result;
		/// <summary>
		/// Retrieve the history candles of the contract.As of now, the historical candels of 9 major currencies are provided: BTC, ETH, LTC, ETC, XRP, EOS, BCH, BSV, TRX.
		/// Rate limit: 20 requests per 2 seconds
		/// </summary>
		/// <param name="symbol">Trading pairs symbol</param>
		/// <param name="period">Bar size in seconds, default 60, must be one of [60/180/300/900/1800/3600/7200/14400/21600/43200/86400/604800] or returns error</param>
		/// <param name="start">Start time in ISO 8601</param>
		/// <param name="end">End time in ISO 8601</param>
		/// <param name="limit">The number of candles returned, the default is 300，and the maximum is 300</param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public async Task<WebCallResult<IEnumerable<OkexSpotCandle>>> Spot_GetHistoricalCandles_Async(string symbol, OkexSpotPeriod period, DateTime? start = null, DateTime? end = null, int limit = 300, CancellationToken ct = default)
		{
			symbol = symbol.ValidateSymbol();
			limit.ValidateIntBetween(nameof(limit), 1, 300);

			var parameters = new Dictionary<string, object>
			{
				{ "granularity", JsonConvert.SerializeObject(period, new SpotPeriodConverter(false)) },
				{ "limit", limit },
			};
			parameters.AddOptionalParameter("start", start?.DateTimeToIso8601String());
			parameters.AddOptionalParameter("end", end?.DateTimeToIso8601String());

			#region Default Method
			// return await SendRequest<IEnumerable<Candle>>(GetUrl(Endpoints_Spot_HistoricalCandles, symbol), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
			#endregion

			#region Modified Method
			var result = await SendRequest<IEnumerable<OkexSpotCandle>>(GetUrl(Endpoints_Spot_HistoricalCandles, symbol), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
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

		#endregion
	}
}
