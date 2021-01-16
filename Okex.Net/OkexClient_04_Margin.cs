using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using Newtonsoft.Json;
using Okex.Net.Converters;
using Okex.Net.Enums;
using Okex.Net.Helpers;
using Okex.Net.Interfaces;
using Okex.Net.RestObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Okex.Net
{
    public partial class OkexClient : IOkexClientMargin
    {
        #region Margin Trading API

        #region Private Signed Endpoints

        /// <summary>
        /// This retrieves the list of assets, (with nonzero balance), remaining balance, and amount available in the margin trading account.
        /// Rate limit: 20 requests per 2 seconds
        /// Notes: After you placed an order, the amount of the order will be put on hold. You will not be able to transfer or use in other orders until the order is completed or cancelled.
        /// </summary>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<IEnumerable<OkexMarginBalance>> Margin_GetAllBalances(CancellationToken ct = default) => Margin_GetAllBalances_Async(ct).Result;
        /// <summary>
        /// This retrieves the list of assets, (with nonzero balance), remaining balance, and amount available in the margin trading account.
        /// Rate limit: 20 requests per 2 seconds
        /// Notes: After you placed an order, the amount of the order will be put on hold. You will not be able to transfer or use in other orders until the order is completed or cancelled.
        /// </summary>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<OkexMarginBalance>>> Margin_GetAllBalances_Async(CancellationToken ct = default)
        {
            return await SendRequest<IEnumerable<OkexMarginBalance>>(GetUrl(Endpoints_Margin_Accounts), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Get the balance, amount on hold and more useful information.
        /// Rate limit: 20 requests per 2 seconds
        /// </summary>
        /// <param name="symbol">[required] trading pair</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<OkexMarginBalance> Margin_GetSymbolBalance(string symbol, CancellationToken ct = default) => Margin_GetSymbolBalance_Async(symbol, ct).Result;
        /// <summary>
        /// Get the balance, amount on hold and more useful information.
        /// Rate limit: 20 requests per 2 seconds
        /// </summary>
        /// <param name="symbol">[required] trading pair</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<OkexMarginBalance>> Margin_GetSymbolBalance_Async(string symbol, CancellationToken ct = default)
        {
            symbol = symbol.ValidateSymbol();
            return await SendRequest<OkexMarginBalance>(GetUrl(Endpoints_Margin_AccountsOfSymbol, symbol), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// This retrieves the margin account bills dating back the past 3 months. Pagination is supported and the response is sorted with most recent first in reverse chronological order.
        /// Rate limit: 10 requests per 2 seconds
        /// </summary>
        /// <param name="symbol">Token</param>
        /// <param name="type">3.Tokens Borrowed 4.Tokers Repaid 5.Interest Accrued 7.Buy 8.Sell 9.From Funding 10.From C2C 12.From Spot 14.To Funding 15.To C2C 16.To Spot 19.Auto Interest Payment 24.Liquidation Fees 59.Repay Candy 61.To Margin 62.From Margin</param>
        /// <param name="limit">Number of results per request. Maximum 100. (default 100)</param>
        /// <param name="before">Request page after (newer) this pagination id,the parameter is ledger_id.</param>
        /// <param name="after">Request page before (older) this pagination id,the parameter is ledger_id.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<IEnumerable<OkexMarginBill>> Margin_GetSymbolBills(string symbol, OkexMarginBillType? type = null, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default) => Margin_GetSymbolBills_Async(symbol, type, limit, before, after, ct).Result;
        /// <summary>
        /// This retrieves the margin account bills dating back the past 3 months. Pagination is supported and the response is sorted with most recent first in reverse chronological order.
        /// Rate limit: 10 requests per 2 seconds
        /// </summary>
        /// <param name="symbol">Token</param>
        /// <param name="type">3.Tokens Borrowed 4.Tokers Repaid 5.Interest Accrued 7.Buy 8.Sell 9.From Funding 10.From C2C 12.From Spot 14.To Funding 15.To C2C 16.To Spot 19.Auto Interest Payment 24.Liquidation Fees 59.Repay Candy 61.To Margin 62.From Margin</param>
        /// <param name="limit">Number of results per request. Maximum 100. (default 100)</param>
        /// <param name="before">Request page after (newer) this pagination id,the parameter is ledger_id.</param>
        /// <param name="after">Request page before (older) this pagination id,the parameter is ledger_id.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<OkexMarginBill>>> Margin_GetSymbolBills_Async(string symbol, OkexMarginBillType? type = null, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default)
        {
            symbol = symbol.ValidateSymbol();
            limit.ValidateIntBetween(nameof(limit), 1, 100);

            var parameters = new Dictionary<string, object>
            {
                { "limit", limit .ToString(ci)},
            };
            if (type != null) parameters.AddOptionalParameter("type", JsonConvert.SerializeObject(type, new MarginBillTypeConverter(false)));
            parameters.AddOptionalParameter("before", before?.ToString(ci));
            parameters.AddOptionalParameter("after", after?.ToString(ci));

            return await SendRequest<IEnumerable<OkexMarginBill>>(GetUrl(Endpoints_Margin_Bills, symbol), HttpMethod.Get, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve all the information of the margin trading account, including the maximum loan amount, interest rate, and maximum leverage.
        /// Rate limit：20 requests per 2 seconds
        /// </summary>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<IEnumerable<OkexMarginAccountSettings>> Margin_GetAccountSettings(CancellationToken ct = default) => Margin_GetAccountSettings_Async(ct).Result;
        /// <summary>
        /// Retrieve all the information of the margin trading account, including the maximum loan amount, interest rate, and maximum leverage.
        /// Rate limit：20 requests per 2 seconds
        /// </summary>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<OkexMarginAccountSettings>>> Margin_GetAccountSettings_Async(CancellationToken ct = default)
        {
            return await SendRequest<IEnumerable<OkexMarginAccountSettings>>(GetUrl(Endpoints_Margin_AccountSettings), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Get all information of the margin trading account of a specific token, including the maximum loan amount, interest rate, and maximum leverage.
        /// Rate limit：20 requests per 2 seconds
        /// </summary>
        /// <param name="symbol">[required] trading pair</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<IEnumerable<OkexMarginAccountSettings>> Margin_GetAccountSettings(string symbol, CancellationToken ct = default) => Margin_GetAccountSettings_Async(symbol, ct).Result;
        /// <summary>
        /// Get all information of the margin trading account of a specific token, including the maximum loan amount, interest rate, and maximum leverage.
        /// Rate limit：20 requests per 2 seconds
        /// </summary>
        /// <param name="symbol">[required] trading pair</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<OkexMarginAccountSettings>>> Margin_GetAccountSettings_Async(string symbol, CancellationToken ct = default)
        {
            return await SendRequest<IEnumerable<OkexMarginAccountSettings>>(GetUrl(Endpoints_Margin_AccountSettingsOfCurrency, symbol), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Get loan history of the margin trading account. Pagination is used here. before and after cursor arguments should not be confused with before and after in chronological time. Most paginated requests return the latest information (newest) as the first page sorted by newest (in chronological time) first.
        /// Rate limit：20 requests per 2 seconds
        /// </summary>
        /// <param name="status">status(0: outstanding 1: repaid) If not filled, the default returns 0:outstanding</param>
        /// <param name="limit">Number of results per request. Maximum 100. (default 100)</param>
        /// <param name="before">Request page after (newer) this pagination id, the parameter is borrow_id.</param>
        /// <param name="after">Request page before (older) this pagination id, the parameter is borrow_id.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<IEnumerable<OkexMarginLoanHistory>> Margin_GetLoanHistory(OkexMarginLoanStatus status, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default) => Margin_GetLoanHistory_Async(status, limit, before, after, ct).Result;
        /// <summary>
        /// Get loan history of the margin trading account. Pagination is used here. before and after cursor arguments should not be confused with before and after in chronological time. Most paginated requests return the latest information (newest) as the first page sorted by newest (in chronological time) first.
        /// Rate limit：20 requests per 2 seconds
        /// </summary>
        /// <param name="status">status(0: outstanding 1: repaid) If not filled, the default returns 0:outstanding</param>
        /// <param name="limit">Number of results per request. Maximum 100. (default 100)</param>
        /// <param name="before">Request page after (newer) this pagination id, the parameter is borrow_id.</param>
        /// <param name="after">Request page before (older) this pagination id, the parameter is borrow_id.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<OkexMarginLoanHistory>>> Margin_GetLoanHistory_Async(OkexMarginLoanStatus status, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "status", JsonConvert.SerializeObject(status, new MarginLoanStatusConverter(false)) },
                { "limit", limit.ToString(ci) },
            };
            parameters.AddOptionalParameter("before", before?.ToString(ci));
            parameters.AddOptionalParameter("after", after?.ToString(ci));

            return await SendRequest<IEnumerable<OkexMarginLoanHistory>>(GetUrl(Endpoints_Margin_LoanHistory), HttpMethod.Get, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Get loan history of the margin trading account of a specific token. Pagination is used here. before and after cursor arguments should not be confused with before and after in chronological time. Most paginated requests return the latest information (newest) as the first page sorted by newest (in chronological time) first.
        /// Rate limit：20 requests per 2 seconds
        /// </summary>
        /// <param name="symbol">trading pair</param>
        /// <param name="state">Order state("-2":Failed,"-1":Cancelled,"0":Open ,"1":Partially Filled, "2":Fully Filled,"3":Submitting,"4":Cancelling,"6": Incomplete（open+partially filled），"7":Complete（cancelled+fully filled））</param>
        /// <param name="limit">Number of results per request. Maximum 100. (default 100)</param>
        /// <param name="before">Request page after (newer) this pagination id, the parameter is borrow_id.</param>
        /// <param name="after">Request page before (older) this pagination id, the parameter is borrow_id.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<IEnumerable<OkexMarginLoanHistory>> Margin_GetLoanHistory(string symbol, OkexMarginLoanState state, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default) => Margin_GetLoanHistory_Async(symbol, state, limit, before, after, ct).Result;
        /// <summary>
        /// Get loan history of the margin trading account of a specific token. Pagination is used here. before and after cursor arguments should not be confused with before and after in chronological time. Most paginated requests return the latest information (newest) as the first page sorted by newest (in chronological time) first.
        /// Rate limit：20 requests per 2 seconds
        /// </summary>
        /// <param name="symbol">trading pair</param>
        /// <param name="state">Order state("-2":Failed,"-1":Cancelled,"0":Open ,"1":Partially Filled, "2":Fully Filled,"3":Submitting,"4":Cancelling,"6": Incomplete（open+partially filled），"7":Complete（cancelled+fully filled））</param>
        /// <param name="limit">Number of results per request. Maximum 100. (default 100)</param>
        /// <param name="before">Request page after (newer) this pagination id, the parameter is borrow_id.</param>
        /// <param name="after">Request page before (older) this pagination id, the parameter is borrow_id.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<OkexMarginLoanHistory>>> Margin_GetLoanHistory_Async(string symbol, OkexMarginLoanState state, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "state", JsonConvert.SerializeObject(state, new MarginLoanStateConverter(false)) },
                { "limit", limit.ToString(ci) },
            };
            parameters.AddOptionalParameter("before", before?.ToString(ci));
            parameters.AddOptionalParameter("after", after?.ToString(ci));

            return await SendRequest<IEnumerable<OkexMarginLoanHistory>>(GetUrl(Endpoints_Margin_LoanHistoryOfCurrency, symbol), HttpMethod.Get, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Borrowing tokens in a margin trading account.
        /// Rate limit：100 requests per 2 seconds
        /// </summary>
        /// <param name="symbol">Trading pair</param>
        /// <param name="currency">Token</param>
        /// <param name="amount">Borrowed amount</param>
        /// <param name="clientOrderId"></param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<OkexMarginLoanResponse> Margin_Loan(string symbol, string currency, decimal amount, string? clientOrderId = null, CancellationToken ct = default) => Margin_Loan_Async(symbol, currency, amount, clientOrderId, ct).Result;
        /// <summary>
        /// Borrowing tokens in a margin trading account.
        /// Rate limit：100 requests per 2 seconds
        /// </summary>
        /// <param name="symbol">Trading pair</param>
        /// <param name="currency">Token</param>
        /// <param name="amount">Borrowed amount</param>
        /// <param name="clientOrderId"></param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<OkexMarginLoanResponse>> Margin_Loan_Async(string symbol, string currency, decimal amount, string? clientOrderId = null, CancellationToken ct = default)
        {
            symbol = symbol.ValidateSymbol();
            var parameters = new Dictionary<string, object>
            {
                { "instrument_id", symbol },
                { "currency", currency },
                { "amount", amount.ToString(ci) },
            };
            parameters.AddOptionalParameter("client_oid", clientOrderId);

            return await SendRequest<OkexMarginLoanResponse>(GetUrl(Endpoints_Margin_Loan), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Repaying tokens in a margin trading account.
        /// Rate limit：100 requests per 2 seconds
        /// </summary>
        /// <param name="symbol">trading pair</param>
        /// <param name="currency">token</param>
        /// <param name="amount">amount repaid</param>
        /// <param name="borrow_id">borrow ID . all borrowed token under this trading pair will be repay if the field is left blank</param>
        /// <param name="clientOrderId">client-supplied order ID</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<OkexMarginRepaymentResponse> Margin_Repayment(string symbol, string currency, decimal amount, long? borrow_id = null, string? clientOrderId = null, CancellationToken ct = default) => Margin_Repayment_Async(symbol, currency, amount, borrow_id, clientOrderId, ct).Result;
        /// <summary>
        /// Repaying tokens in a margin trading account.
        /// Rate limit：100 requests per 2 seconds
        /// </summary>
        /// <param name="symbol">trading pair</param>
        /// <param name="currency">token</param>
        /// <param name="amount">amount repaid</param>
        /// <param name="borrow_id">borrow ID . all borrowed token under this trading pair will be repay if the field is left blank</param>
        /// <param name="clientOrderId">client-supplied order ID</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<OkexMarginRepaymentResponse>> Margin_Repayment_Async(string symbol, string currency, decimal amount, long? borrow_id = null, string? clientOrderId = null, CancellationToken ct = default)
        {
            symbol = symbol.ValidateSymbol();
            var parameters = new Dictionary<string, object>
            {
                { "instrument_id", symbol },
                { "currency", currency },
                { "amount", amount.ToString(ci)},
            };
            parameters.AddOptionalParameter("borrow_id", borrow_id?.ToString(ci));
            parameters.AddOptionalParameter("client_oid", clientOrderId);

            return await SendRequest<OkexMarginRepaymentResponse>(GetUrl(Endpoints_Margin_Repayment), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// OKEx API only supports limit and market orders (more order types will become available in the future). You can place an order only if you have enough funds. Once your order is placed, the amount will be put on hold until the order is executed.
        /// Rate limit: 100 requests per 2 seconds （The speed limit is not accumulated between different trading pair symbols)
        /// Hold:
        /// For limit buy order, we will put a hold the quoted currency, of which the amount on hold = specific price x buying size.For limit sell orders, we will put a hold on the currency equal to the amount you want to sell. 
        /// For market buy orders, the amount equal to the quantity for the quoted currency will be on hold.For market sell orders, the amount based on the size of the currency you want to sell will be on hold.Cancelling an open order releases the amount on hold.
        /// Order life cycle: 
        /// The HTTP Request will respond when an order is either rejected (insufficient funds, invalid parameters, etc) or received (accepted by the matching engine). A 200 response indicates that the order was received and is active. 
        /// Active orders may execute immediately (depending on price and market conditions) either partially or fully. A partial execution will put the remaining size of the order in the open state. An order that is filled Fully, will go into the completed state.
        /// Users listening to streaming market data are encouraged to use the client_oid field to identify their received messages in the feed. The REST response with a server order_id may come after the received message in the public data feed.
        /// Response
        /// A successful order will be assigned an order id. A successful order is defined as one that has been accepted by the matching engine. Open orders will not expire until filled or canceled.
        /// </summary>
        /// <param name="symbol">
        /// Trading pair symbol
        /// The instrument_id must match a valid instrument. The instruments list is available via the /instruments endpoint
        /// </param>
        /// <param name="side">
        /// Specify buy or sell
        /// </param>
        /// <param name="type">
        /// Supports types limit or market (default: limit). When placing market orders, order_type must be 0 (normal order)
        /// You can specify the order type when placing an order. The order type you specify will decide which order parameters are required further as well as how your order will be executed by the matching engine.
        /// If type is not specified, the order will default to a limit order. Limit order is the default order type, and it is also the basic order type. A limit order requires specifying a price and size. 
        /// The limit order will be filled at the specifie price or better. Specifically, A sell order can be filled at the specified or higher price per the quote token.
        /// A buy order can be filled at the specified or lower price per the quote token. If the limit order is not filled immediately, it will be sent into the open order book until filled or canceled.
        /// Market orders differ from limit orders in that they have NO price specification. It provides an order type to buy or sell specific amount of tokens without the need to specify the price. 
        /// Market orders execute immediately and will not be sent into the open order book. Market orders are always considered as ‘takers’ and incur taker fees. 
        /// Warming: Market order is strongly discouraged and if an order to sell/buy a large amount is placed it will probably cause turbulence in the market.
        /// </param>
        /// <param name="timeInForce">
        /// Specify 0: Normal order (Unfilled and 0 imply normal limit order) 1: Post only 2: Fill or Kill 3: Immediate Or Cancel
        /// </param>
        /// <param name="price">
        /// Price
        /// The price must be specified in unit of size_increment which is the smallest incremental unit of the price. It can be acquired via the /instruments endpoint.
        /// </param>
        /// <param name="size">
        /// - Limit Order: Quantity to buy or sell
        /// - Market Order: Quantity to be sold. Required for market sells
        /// Size is the quantity of buying or selling and must be larger than the min_size. size_increment is the minimum increment size. It can be acquired via the /instruments endpoint.
        /// Example: If the min_size of OKB/USDT is 10 and the size_increment is 0.0001, then it is impossible to trade 9.99 OKB but possible to trade 10.0001 OKB.
        /// </param>
        /// <param name="notional">
        /// Amount to spend. Required for market buys
        /// The notional field is the quantity of quoted currency when placing market orders; it is required for market orders. For example, a market buy for BTC-USDT with quantity specified as 5000 will spend 5000 USDT to buy BTC.
        /// </param>
        /// <param name="clientOrderId">
        /// Client-supplied order ID that you can customize. The system supports alphabets + numbers(case-sensitive，e.g:A123、a123), or alphabets (case-sensitive，e.g:Abc、abc) only, between 1-32 characters.</param>
        /// The client_oid is optional and you can customize it using alpha-numeric characters with length 1 to 32. No warning is sent when client_oid is not unique. In case of multiple identical client_oid, only the latest entry will be returned.
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<OkexSpotPlacedOrder> Margin_PlaceOrder(OkexSpotPlaceOrder order, CancellationToken ct = default) => Margin_PlaceOrder_Async(order.Symbol, order.Side, order.Type, order.TimeInForce, order.Price?.ToDecimalNullable(), order.Size?.ToDecimalNullable(), order.Notional?.ToDecimalNullable(), order.ClientOrderId, ct).Result;
        /// <summary>
        /// OKEx API only supports limit and market orders (more order types will become available in the future). You can place an order only if you have enough funds. Once your order is placed, the amount will be put on hold until the order is executed.
        /// Rate limit: 100 requests per 2 seconds （The speed limit is not accumulated between different trading pair symbols)
        /// Hold:
        /// For limit buy order, we will put a hold the quoted currency, of which the amount on hold = specific price x buying size.For limit sell orders, we will put a hold on the currency equal to the amount you want to sell. 
        /// For market buy orders, the amount equal to the quantity for the quoted currency will be on hold.For market sell orders, the amount based on the size of the currency you want to sell will be on hold.Cancelling an open order releases the amount on hold.
        /// Order life cycle: 
        /// The HTTP Request will respond when an order is either rejected (insufficient funds, invalid parameters, etc) or received (accepted by the matching engine). A 200 response indicates that the order was received and is active. 
        /// Active orders may execute immediately (depending on price and market conditions) either partially or fully. A partial execution will put the remaining size of the order in the open state. An order that is filled Fully, will go into the completed state.
        /// Users listening to streaming market data are encouraged to use the client_oid field to identify their received messages in the feed. The REST response with a server order_id may come after the received message in the public data feed.
        /// Response
        /// A successful order will be assigned an order id. A successful order is defined as one that has been accepted by the matching engine. Open orders will not expire until filled or canceled.
        /// </summary>
        /// <param name="symbol">
        /// Trading pair symbol
        /// The instrument_id must match a valid instrument. The instruments list is available via the /instruments endpoint
        /// </param>
        /// <param name="side">
        /// Specify buy or sell
        /// </param>
        /// <param name="type">
        /// Supports types limit or market (default: limit). When placing market orders, order_type must be 0 (normal order)
        /// You can specify the order type when placing an order. The order type you specify will decide which order parameters are required further as well as how your order will be executed by the matching engine.
        /// If type is not specified, the order will default to a limit order. Limit order is the default order type, and it is also the basic order type. A limit order requires specifying a price and size. 
        /// The limit order will be filled at the specifie price or better. Specifically, A sell order can be filled at the specified or higher price per the quote token.
        /// A buy order can be filled at the specified or lower price per the quote token. If the limit order is not filled immediately, it will be sent into the open order book until filled or canceled.
        /// Market orders differ from limit orders in that they have NO price specification. It provides an order type to buy or sell specific amount of tokens without the need to specify the price. 
        /// Market orders execute immediately and will not be sent into the open order book. Market orders are always considered as ‘takers’ and incur taker fees. 
        /// Warming: Market order is strongly discouraged and if an order to sell/buy a large amount is placed it will probably cause turbulence in the market.
        /// </param>
        /// <param name="timeInForce">
        /// Specify 0: Normal order (Unfilled and 0 imply normal limit order) 1: Post only 2: Fill or Kill 3: Immediate Or Cancel
        /// </param>
        /// <param name="price">
        /// Price
        /// The price must be specified in unit of size_increment which is the smallest incremental unit of the price. It can be acquired via the /instruments endpoint.
        /// </param>
        /// <param name="size">
        /// - Limit Order: Quantity to buy or sell
        /// - Market Order: Quantity to be sold. Required for market sells
        /// Size is the quantity of buying or selling and must be larger than the min_size. size_increment is the minimum increment size. It can be acquired via the /instruments endpoint.
        /// Example: If the min_size of OKB/USDT is 10 and the size_increment is 0.0001, then it is impossible to trade 9.99 OKB but possible to trade 10.0001 OKB.
        /// </param>
        /// <param name="notional">
        /// Amount to spend. Required for market buys
        /// The notional field is the quantity of quoted currency when placing market orders; it is required for market orders. For example, a market buy for BTC-USDT with quantity specified as 5000 will spend 5000 USDT to buy BTC.
        /// </param>
        /// <param name="clientOrderId">
        /// Client-supplied order ID that you can customize. The system supports alphabets + numbers(case-sensitive，e.g:A123、a123), or alphabets (case-sensitive，e.g:Abc、abc) only, between 1-32 characters.</param>
        /// The client_oid is optional and you can customize it using alpha-numeric characters with length 1 to 32. No warning is sent when client_oid is not unique. In case of multiple identical client_oid, only the latest entry will be returned.
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public Task<WebCallResult<OkexSpotPlacedOrder>> Margin_PlaceOrder_Async(OkexSpotPlaceOrder order, CancellationToken ct = default) => Margin_PlaceOrder_Async(order.Symbol, order.Side, order.Type, order.TimeInForce, order.Price?.ToDecimalNullable(), order.Size?.ToDecimalNullable(), order.Notional?.ToDecimalNullable(), order.ClientOrderId, ct);
        /// <summary>
        /// OKEx API only supports limit and market orders (more order types will become available in the future). You can place an order only if you have enough funds. Once your order is placed, the amount will be put on hold until the order is executed.
        /// Rate limit: 100 requests per 2 seconds （The speed limit is not accumulated between different trading pair symbols)
        /// Hold:
        /// For limit buy order, we will put a hold the quoted currency, of which the amount on hold = specific price x buying size.For limit sell orders, we will put a hold on the currency equal to the amount you want to sell. 
        /// For market buy orders, the amount equal to the quantity for the quoted currency will be on hold.For market sell orders, the amount based on the size of the currency you want to sell will be on hold.Cancelling an open order releases the amount on hold.
        /// Order life cycle: 
        /// The HTTP Request will respond when an order is either rejected (insufficient funds, invalid parameters, etc) or received (accepted by the matching engine). A 200 response indicates that the order was received and is active. 
        /// Active orders may execute immediately (depending on price and market conditions) either partially or fully. A partial execution will put the remaining size of the order in the open state. An order that is filled Fully, will go into the completed state.
        /// Users listening to streaming market data are encouraged to use the client_oid field to identify their received messages in the feed. The REST response with a server order_id may come after the received message in the public data feed.
        /// Response
        /// A successful order will be assigned an order id. A successful order is defined as one that has been accepted by the matching engine. Open orders will not expire until filled or canceled.
        /// </summary>
        /// <param name="symbol">
        /// Trading pair symbol
        /// The instrument_id must match a valid instrument. The instruments list is available via the /instruments endpoint
        /// </param>
        /// <param name="side">
        /// Specify buy or sell
        /// </param>
        /// <param name="type">
        /// Supports types limit or market (default: limit). When placing market orders, order_type must be 0 (normal order)
        /// You can specify the order type when placing an order. The order type you specify will decide which order parameters are required further as well as how your order will be executed by the matching engine.
        /// If type is not specified, the order will default to a limit order. Limit order is the default order type, and it is also the basic order type. A limit order requires specifying a price and size. 
        /// The limit order will be filled at the specifie price or better. Specifically, A sell order can be filled at the specified or higher price per the quote token.
        /// A buy order can be filled at the specified or lower price per the quote token. If the limit order is not filled immediately, it will be sent into the open order book until filled or canceled.
        /// Market orders differ from limit orders in that they have NO price specification. It provides an order type to buy or sell specific amount of tokens without the need to specify the price. 
        /// Market orders execute immediately and will not be sent into the open order book. Market orders are always considered as ‘takers’ and incur taker fees. 
        /// Warming: Market order is strongly discouraged and if an order to sell/buy a large amount is placed it will probably cause turbulence in the market.
        /// </param>
        /// <param name="timeInForce">
        /// Specify 0: Normal order (Unfilled and 0 imply normal limit order) 1: Post only 2: Fill or Kill 3: Immediate Or Cancel
        /// </param>
        /// <param name="price">
        /// Price
        /// The price must be specified in unit of size_increment which is the smallest incremental unit of the price. It can be acquired via the /instruments endpoint.
        /// </param>
        /// <param name="size">
        /// - Limit Order: Quantity to buy or sell
        /// - Market Order: Quantity to be sold. Required for market sells
        /// Size is the quantity of buying or selling and must be larger than the min_size. size_increment is the minimum increment size. It can be acquired via the /instruments endpoint.
        /// Example: If the min_size of OKB/USDT is 10 and the size_increment is 0.0001, then it is impossible to trade 9.99 OKB but possible to trade 10.0001 OKB.
        /// </param>
        /// <param name="notional">
        /// Amount to spend. Required for market buys
        /// The notional field is the quantity of quoted currency when placing market orders; it is required for market orders. For example, a market buy for BTC-USDT with quantity specified as 5000 will spend 5000 USDT to buy BTC.
        /// </param>
        /// <param name="clientOrderId">
        /// Client-supplied order ID that you can customize. The system supports alphabets + numbers(case-sensitive，e.g:A123、a123), or alphabets (case-sensitive，e.g:Abc、abc) only, between 1-32 characters.</param>
        /// The client_oid is optional and you can customize it using alpha-numeric characters with length 1 to 32. No warning is sent when client_oid is not unique. In case of multiple identical client_oid, only the latest entry will be returned.
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<OkexSpotPlacedOrder> Margin_PlaceOrder(string symbol, OkexSpotOrderSide side, OkexSpotOrderType type, OkexSpotTimeInForce timeInForce = OkexSpotTimeInForce.NormalOrder, decimal? price = null, decimal? size = null, decimal? notional = null, string? clientOrderId = null, CancellationToken ct = default) => Margin_PlaceOrder_Async(symbol, side, type, timeInForce, price, size, notional, clientOrderId, ct).Result;
        /// <summary>
        /// OKEx API only supports limit and market orders (more order types will become available in the future). You can place an order only if you have enough funds. Once your order is placed, the amount will be put on hold until the order is executed.
        /// Rate limit: 100 requests per 2 seconds （The speed limit is not accumulated between different trading pair symbols)
        /// Hold:
        /// For limit buy order, we will put a hold the quoted currency, of which the amount on hold = specific price x buying size.For limit sell orders, we will put a hold on the currency equal to the amount you want to sell. 
        /// For market buy orders, the amount equal to the quantity for the quoted currency will be on hold.For market sell orders, the amount based on the size of the currency you want to sell will be on hold.Cancelling an open order releases the amount on hold.
        /// Order life cycle: 
        /// The HTTP Request will respond when an order is either rejected (insufficient funds, invalid parameters, etc) or received (accepted by the matching engine). A 200 response indicates that the order was received and is active. 
        /// Active orders may execute immediately (depending on price and market conditions) either partially or fully. A partial execution will put the remaining size of the order in the open state. An order that is filled Fully, will go into the completed state.
        /// Users listening to streaming market data are encouraged to use the client_oid field to identify their received messages in the feed. The REST response with a server order_id may come after the received message in the public data feed.
        /// Response
        /// A successful order will be assigned an order id. A successful order is defined as one that has been accepted by the matching engine. Open orders will not expire until filled or canceled.
        /// </summary>
        /// <param name="symbol">
        /// Trading pair symbol
        /// The instrument_id must match a valid instrument. The instruments list is available via the /instruments endpoint
        /// </param>
        /// <param name="side">
        /// Specify buy or sell
        /// </param>
        /// <param name="type">
        /// Supports types limit or market (default: limit). When placing market orders, order_type must be 0 (normal order)
        /// You can specify the order type when placing an order. The order type you specify will decide which order parameters are required further as well as how your order will be executed by the matching engine.
        /// If type is not specified, the order will default to a limit order. Limit order is the default order type, and it is also the basic order type. A limit order requires specifying a price and size. 
        /// The limit order will be filled at the specifie price or better. Specifically, A sell order can be filled at the specified or higher price per the quote token.
        /// A buy order can be filled at the specified or lower price per the quote token. If the limit order is not filled immediately, it will be sent into the open order book until filled or canceled.
        /// Market orders differ from limit orders in that they have NO price specification. It provides an order type to buy or sell specific amount of tokens without the need to specify the price. 
        /// Market orders execute immediately and will not be sent into the open order book. Market orders are always considered as ‘takers’ and incur taker fees. 
        /// Warming: Market order is strongly discouraged and if an order to sell/buy a large amount is placed it will probably cause turbulence in the market.
        /// </param>
        /// <param name="timeInForce">
        /// Specify 0: Normal order (Unfilled and 0 imply normal limit order) 1: Post only 2: Fill or Kill 3: Immediate Or Cancel
        /// </param>
        /// <param name="price">
        /// Price
        /// The price must be specified in unit of size_increment which is the smallest incremental unit of the price. It can be acquired via the /instruments endpoint.
        /// </param>
        /// <param name="size">
        /// - Limit Order: Quantity to buy or sell
        /// - Market Order: Quantity to be sold. Required for market sells
        /// Size is the quantity of buying or selling and must be larger than the min_size. size_increment is the minimum increment size. It can be acquired via the /instruments endpoint.
        /// Example: If the min_size of OKB/USDT is 10 and the size_increment is 0.0001, then it is impossible to trade 9.99 OKB but possible to trade 10.0001 OKB.
        /// </param>
        /// <param name="notional">
        /// Amount to spend. Required for market buys
        /// The notional field is the quantity of quoted currency when placing market orders; it is required for market orders. For example, a market buy for BTC-USDT with quantity specified as 5000 will spend 5000 USDT to buy BTC.
        /// </param>
        /// <param name="clientOrderId">
        /// Client-supplied order ID that you can customize. The system supports alphabets + numbers(case-sensitive，e.g:A123、a123), or alphabets (case-sensitive，e.g:Abc、abc) only, between 1-32 characters.</param>
        /// The client_oid is optional and you can customize it using alpha-numeric characters with length 1 to 32. No warning is sent when client_oid is not unique. In case of multiple identical client_oid, only the latest entry will be returned.
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<OkexSpotPlacedOrder>> Margin_PlaceOrder_Async(string symbol, OkexSpotOrderSide side, OkexSpotOrderType type, OkexSpotTimeInForce timeInForce = OkexSpotTimeInForce.NormalOrder, decimal? price = null, decimal? size = null, decimal? notional = null, string? clientOrderId = null, CancellationToken ct = default)
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
                { "margin_trading", "2" }, // The Difference with Spot Order
			};
            parameters.AddOptionalParameter("client_oid", clientOrderId);
            parameters.AddOptionalParameter("notional", notional?.ToString(ci));
            parameters.AddOptionalParameter("price", price?.ToString(ci));
            parameters.AddOptionalParameter("size", size?.ToString(ci));

            return await SendRequest<OkexSpotPlacedOrder>(GetUrl(Endpoints_Margin_PlaceOrder), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
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
        public WebCallResult<Dictionary<string, IEnumerable<OkexSpotPlacedOrder>>> Margin_BatchPlaceOrders(IEnumerable<OkexSpotPlaceOrder> orders, CancellationToken ct = default) => Margin_BatchPlaceOrders_Async(orders, ct = default).Result;
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
        public async Task<WebCallResult<Dictionary<string, IEnumerable<OkexSpotPlacedOrder>>>> Margin_BatchPlaceOrders_Async(IEnumerable<OkexSpotPlaceOrder> orders, CancellationToken ct = default)
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
                order.MarginTrading = "2";
            }

            var parameters = new Dictionary<string, object>
            {
                { BodyParameterKey, orders },
            };

            return await SendRequest<Dictionary<string, IEnumerable<OkexSpotPlacedOrder>>>(GetUrl(Endpoints_Margin_PlaceBatchOrders), HttpMethod.Post, ct, parameters, arraySerialization: ArrayParametersSerialization.Array, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Cancelling an unfilled order.
        /// Rate limit: 100 / 2s （The speed limit is not accumulated between different trading pair symbols)
        /// Notes:
        /// - When using client_oid for order cancellation, The user needs to make sure the ID does not repeat and there is no repetition alert. In case of repetition, only the latest entry will be canceled.
        /// - If the order cannot be cancelled (already settled or cancelled), the corresponding reason can be found in the error description returned.
        /// </summary>
        /// <param name="symbol">by providing this parameter,the corresponding order of a designated trading pair will be cancelled.If not providing this parameter,it will be back to error code.</param>
        /// <param name="orderId">Either client_oid or order_id must be present. order ID</param>
        /// <param name="clientOrderId">Either client_oid or order_id must be present. the order ID created by yourself , The client_oid type should be comprised of alphabets + numbers or only alphabets within 1 – 32 characters， both uppercase and lowercase letters are supported</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<OkexSpotPlacedOrder> Margin_CancelOrder(string symbol, long? orderId = null, string? clientOrderId = null, CancellationToken ct = default) => Margin_CancelOrder_Async(symbol, orderId, clientOrderId, ct).Result;
        /// <summary>
        /// Cancelling an unfilled order.
        /// Rate limit: 100 / 2s （The speed limit is not accumulated between different trading pair symbols)
        /// Notes:
        /// - When using client_oid for order cancellation, The user needs to make sure the ID does not repeat and there is no repetition alert. In case of repetition, only the latest entry will be canceled.
        /// - If the order cannot be cancelled (already settled or cancelled), the corresponding reason can be found in the error description returned.
        /// </summary>
        /// <param name="symbol">by providing this parameter,the corresponding order of a designated trading pair will be cancelled.If not providing this parameter,it will be back to error code.</param>
        /// <param name="orderId">Either client_oid or order_id must be present. order ID</param>
        /// <param name="clientOrderId">Either client_oid or order_id must be present. the order ID created by yourself , The client_oid type should be comprised of alphabets + numbers or only alphabets within 1 – 32 characters， both uppercase and lowercase letters are supported</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<OkexSpotPlacedOrder>> Margin_CancelOrder_Async(string symbol, long? orderId = null, string? clientOrderId = null, CancellationToken ct = default)
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

            return await SendRequest<OkexSpotPlacedOrder>(GetUrl(Endpoints_Margin_CancelOrder, orderId.HasValue ? orderId.ToString() : clientOrderId!), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Cancel multiple open orders with order_id or client_oid. Up to 4 trading pairs, and maximum 10 orders can be canceled at a time for each trading pair.
        /// Rate limit: 50/ 2s （The speed limit is accumulated between different trading pair symbols)
        /// Notes:
        /// - For bulk order cancellation, the parameters should be either all order_id or all client_oid, otherwise it will trigger an error code.
        /// - When using client_oid for bulk order cancellation, only one client_oid canceled for one trading pair, maximum 4 pairs per time. The user needs to make sure the ID does not repeat and there is no repetition alert. In case of repetition, only the latest entry will be canceled.
        /// - You may place up to 4 trading pairs with 10 orders at maximum for each pair. We recommend you to use the "Get order list" endpoint" before using the "Cancel multiple orders" endpoint to confirm the cancellation of orders.
        /// </summary>
        /// <param name="orders">
        ///	OrderIds: Order ID List
        ///	ClientOrderIds: Client-supplied order ID that you can customize. It should be comprised of alpha-numeric characters with length 1 to 32. Both uppercase and lowercase are supported
        /// </param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<Dictionary<string, IEnumerable<OkexSpotPlacedOrder>>> Margin_BatchCancelOrders(IEnumerable<OkexSpotCancelOrder> orders, CancellationToken ct = default) => Margin_BatchCancelOrders_Async(orders, ct).Result;
        /// <summary>
        /// Cancel multiple open orders with order_id or client_oid. Up to 4 trading pairs, and maximum 10 orders can be canceled at a time for each trading pair.
        /// Rate limit: 50/ 2s （The speed limit is accumulated between different trading pair symbols)
        /// Notes:
        /// - For bulk order cancellation, the parameters should be either all order_id or all client_oid, otherwise it will trigger an error code.
        /// - When using client_oid for bulk order cancellation, only one client_oid canceled for one trading pair, maximum 4 pairs per time. The user needs to make sure the ID does not repeat and there is no repetition alert. In case of repetition, only the latest entry will be canceled.
        /// - You may place up to 4 trading pairs with 10 orders at maximum for each pair. We recommend you to use the "Get order list" endpoint" before using the "Cancel multiple orders" endpoint to confirm the cancellation of orders.
        /// </summary>
        /// <param name="orders">
        ///	OrderIds: Order ID List
        ///	ClientOrderIds: Client-supplied order ID that you can customize. It should be comprised of alpha-numeric characters with length 1 to 32. Both uppercase and lowercase are supported
        /// </param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<Dictionary<string, IEnumerable<OkexSpotPlacedOrder>>>> Margin_BatchCancelOrders_Async(IEnumerable<OkexSpotCancelOrder> orders, CancellationToken ct = default)
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

            return await SendRequest<Dictionary<string, IEnumerable<OkexSpotPlacedOrder>>>(GetUrl(Endpoints_Margin_CancelBatchOrders), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// This retrieves the list of your orders from the most recent 3 months. This request supports paging and is stored according to the order time in chronological order from latest to earliest.
        /// Rate limit: 10 requests per 2 seconds
        /// Notes:
        /// - status is the older version of state and both can be used interchangeably in the short term. It is recommended to switch to state.
        /// - The client_oid is optional and you can customize it using alpha-numeric characters with length 1 to 32. This parameter is used to identify your orders in the public orders feed. No warning is sent when client_oid is not unique. In case of multiple identical client_oid, only the latest entry will be returned.
        /// - If the order is not filled in the order life cycle, the record may be removed.
        /// - The status of unfilled orders may change during the time of endpoint request and response, depending on the market condition.
        /// </summary>
        /// <param name="symbol">Trading pair symbol</param>
        /// <param name="state">Order Status: -2 = Failed -1 = Canceled 0 = Open 1 = Partially Filled 2 = Fully Filled 3 = Submitting 4 = Canceling 6 = Incomplete (open + partially filled) 7 = Complete (canceled + fully filled)</param>
        /// <param name="limit">Number of results per request. The maximum is 100; the default is 100</param>
        /// <param name="before">Pagination of data to return records newer than the requested order_id.</param>
        /// <param name="after">Pagination of data to return records earlier than the requested order_id.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<IEnumerable<OkexSpotOrderDetails>> Margin_GetAllOrders(string symbol, OkexSpotOrderState state, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default) => Margin_GetAllOrders_Async(symbol, state, limit, before, after, ct).Result;
        /// <summary>
        /// This retrieves the list of your orders from the most recent 3 months. This request supports paging and is stored according to the order time in chronological order from latest to earliest.
        /// Rate limit: 10 requests per 2 seconds
        /// Notes:
        /// - status is the older version of state and both can be used interchangeably in the short term. It is recommended to switch to state.
        /// - The client_oid is optional and you can customize it using alpha-numeric characters with length 1 to 32. This parameter is used to identify your orders in the public orders feed. No warning is sent when client_oid is not unique. In case of multiple identical client_oid, only the latest entry will be returned.
        /// - If the order is not filled in the order life cycle, the record may be removed.
        /// - The status of unfilled orders may change during the time of endpoint request and response, depending on the market condition.
        /// </summary>
        /// <param name="symbol">Trading pair symbol</param>
        /// <param name="state">Order Status: -2 = Failed -1 = Canceled 0 = Open 1 = Partially Filled 2 = Fully Filled 3 = Submitting 4 = Canceling 6 = Incomplete (open + partially filled) 7 = Complete (canceled + fully filled)</param>
        /// <param name="limit">Number of results per request. The maximum is 100; the default is 100</param>
        /// <param name="before">Pagination of data to return records newer than the requested order_id.</param>
        /// <param name="after">Pagination of data to return records earlier than the requested order_id.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<OkexSpotOrderDetails>>> Margin_GetAllOrders_Async(string symbol, OkexSpotOrderState state, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default)
        {
            symbol = symbol.ValidateSymbol();
            limit.ValidateIntBetween(nameof(limit), 1, 100);

            var parameters = new Dictionary<string, object>
            {
                { "instrument_id", symbol },
                { "state", JsonConvert.SerializeObject(state, new SpotOrderStateConverter(false)) },
                { "limit", limit.ToString(ci) },
            };
            parameters.AddOptionalParameter("before", before?.ToString(ci));
            parameters.AddOptionalParameter("after", after?.ToString(ci));

            return await SendRequest<IEnumerable<OkexSpotOrderDetails>>(GetUrl(Endpoints_Margin_OrderList), HttpMethod.Get, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Get currency leverage account currency leverage
        /// Rate Limit: 100 requests per 2 seconds
        /// </summary>
        /// <param name="symbol">Currency pair name, such as: BTC-USDT</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<OkexMarginLeverageResponse> Margin_GetLeverage(string symbol, CancellationToken ct = default) => Margin_GetLeverage_Async(symbol, ct).Result;
        /// <summary>
        /// Get currency leverage account currency leverage
        /// Rate Limit: 100 requests per 2 seconds
        /// </summary>
        /// <param name="symbol">Currency pair name, such as: BTC-USDT</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<OkexMarginLeverageResponse>> Margin_GetLeverage_Async(string symbol, CancellationToken ct = default)
        {
            symbol = symbol.ValidateSymbol();
            return await SendRequest<OkexMarginLeverageResponse>(GetUrl(Endpoints_Margin_GetLeverage, symbol), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Set the leverage ratio of the currency pair account.
        /// Notes:
        /// - For cross-margin mode, only one leverage ratio is allowed per trading pair. For fixed-margin mode, one leverage ratio is allowed per contract per side (long or short).
        /// </summary>
        /// <param name="symbol">Currency pairs, such as: BTC-USDT</param>
        /// <param name="leverage">2-10x leverage</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<OkexMarginLeverageResponse> Margin_SetLeverage(string symbol, int leverage, CancellationToken ct = default) => Margin_SetLeverage_Async(symbol, leverage, ct).Result;
        /// <summary>
        /// Set the leverage ratio of the currency pair account.
        /// Notes:
        /// - For cross-margin mode, only one leverage ratio is allowed per trading pair. For fixed-margin mode, one leverage ratio is allowed per contract per side (long or short).
        /// </summary>
        /// <param name="symbol">Currency pairs, such as: BTC-USDT</param>
        /// <param name="leverage">2-10x leverage</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<OkexMarginLeverageResponse>> Margin_SetLeverage_Async(string symbol, int leverage, CancellationToken ct = default)
        {
            symbol = symbol.ValidateSymbol();
            var parameters = new Dictionary<string, object>
            {
                { "instrument_id", symbol },
                { "leverage", leverage.ToString(ci)},
            };

            return await SendRequest<OkexMarginLeverageResponse>(GetUrl(Endpoints_Margin_SetLeverage, symbol), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve order details by order ID.Can get order information for nearly 3 months。 Unfilled orders will be kept in record for only two hours after it is canceled.
        /// Rate limit: 20 requests per 2 seconds
        /// Notes:
        /// - status is the older version of state and both can be used interchangeably in the short term. It is recommended to switch to state.
        /// - The client_oid is optional and you can customize it using alpha-numeric characters with length 1 to 32. This parameter is used to identify your orders in the public orders feed. No warning is sent when client_oid is not unique. In case of multiple identical client_oid, only the latest entry will be returned.
        /// - Unfilled order status may change according to the market conditions.
        /// </summary>
        /// <param name="symbol">Trading </param>
        /// <param name="orderId">Order ID Either client_oid or order_id must be present.</param>
        /// <param name="clientOrderId">Client-supplied order ID Either client_oid or order_id must be present.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<OkexMarginOrderDetails> Margin_GetOrderDetails(string symbol, long? orderId = null, string? clientOrderId = null, CancellationToken ct = default) => Margin_GetOrderDetails_Async(symbol, orderId, clientOrderId, ct).Result;
        /// <summary>
        /// Retrieve order details by order ID.Can get order information for nearly 3 months。 Unfilled orders will be kept in record for only two hours after it is canceled.
        /// Rate limit: 20 requests per 2 seconds
        /// Notes:
        /// - status is the older version of state and both can be used interchangeably in the short term. It is recommended to switch to state.
        /// - The client_oid is optional and you can customize it using alpha-numeric characters with length 1 to 32. This parameter is used to identify your orders in the public orders feed. No warning is sent when client_oid is not unique. In case of multiple identical client_oid, only the latest entry will be returned.
        /// - Unfilled order status may change according to the market conditions.
        /// </summary>
        /// <param name="symbol">Trading </param>
        /// <param name="orderId">Order ID Either client_oid or order_id must be present.</param>
        /// <param name="clientOrderId">Client-supplied order ID Either client_oid or order_id must be present.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<OkexMarginOrderDetails>> Margin_GetOrderDetails_Async(string symbol, long? orderId = null, string? clientOrderId = null, CancellationToken ct = default)
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

            return await SendRequest<OkexMarginOrderDetails>(GetUrl(Endpoints_Margin_OrderDetails, orderId.HasValue ? orderId.ToString() : clientOrderId!), HttpMethod.Get, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// This retrieves the list of your current open orders. Pagination is supported and the response is sorted with most recent first in reverse chronological order.
        /// Rate limit: 20 requests per 2 seconds
        /// Notes:
        /// - The parameter status is the older version of state and is compatible in the short term. It is recommended to switch to state.
        /// - The client_oid is optional. It should be a unique ID generated by your trading system. This parameter is used to identify your orders in the public orders feed. No warning is sent when client_oid is not unique.
        /// - In case of multiple identical client_oid, only the latest entry will be returned.
        /// - Only partially filled and open orders will be returned via this endpoint.
        /// - The status of unfilled orders may change during the time of endpoint request and response, depending on the market condition.
        /// </summary>
        /// <param name="symbol">Trading pair symbol</param>
        /// <param name="limit">Number of results per request. The maximum is 100; the default is 100</param>
        /// <param name="before">Pagination of data to return records newer than the requested order_id.</param>
        /// <param name="after">Pagination of data to return records earlier than the requested order_id.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<IEnumerable<OkexMarginOrderDetails>> Margin_GetOpenOrders(string symbol, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default) => Margin_GetOpenOrders_Async(symbol, limit, before, after, ct).Result;
        /// <summary>
        /// This retrieves the list of your current open orders. Pagination is supported and the response is sorted with most recent first in reverse chronological order.
        /// Rate limit: 20 requests per 2 seconds
        /// Notes:
        /// - The parameter status is the older version of state and is compatible in the short term. It is recommended to switch to state.
        /// - The client_oid is optional. It should be a unique ID generated by your trading system. This parameter is used to identify your orders in the public orders feed. No warning is sent when client_oid is not unique.
        /// - In case of multiple identical client_oid, only the latest entry will be returned.
        /// - Only partially filled and open orders will be returned via this endpoint.
        /// - The status of unfilled orders may change during the time of endpoint request and response, depending on the market condition.
        /// </summary>
        /// <param name="symbol">Trading pair symbol</param>
        /// <param name="limit">Number of results per request. The maximum is 100; the default is 100</param>
        /// <param name="before">Pagination of data to return records newer than the requested order_id.</param>
        /// <param name="after">Pagination of data to return records earlier than the requested order_id.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<OkexMarginOrderDetails>>> Margin_GetOpenOrders_Async(string symbol, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default)
        {
            symbol = symbol.ValidateSymbol();
            limit.ValidateIntBetween(nameof(limit), 1, 100);

            var parameters = new Dictionary<string, object>
            {
                { "instrument_id", symbol },
                { "limit", limit.ToString(ci) },
            };
            parameters.AddOptionalParameter("before", before?.ToString(ci));
            parameters.AddOptionalParameter("after", after?.ToString(ci));

            return await SendRequest<IEnumerable<OkexMarginOrderDetails>>(GetUrl(Endpoints_Margin_OpenOrders), HttpMethod.Get, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve recently filled transaction details. Pagination is supported and the response is sorted with most recent first in reverse chronological order. Data for up to 3 months can be retrieved.
        /// Rate limit: 10 requests per 2 seconds
        /// Notes:
        /// - This API will return 2 pieces of data after a transaction is complete. One is calculated based on the quote currency, while the other is calculated based on the base currency.
        /// - Transaction Fees: New status for spot trading transaction details: fee is either a positive number (invitation rebate) or a negative number (transaction fee deduction).
        /// - Liquidity: The exec_type specifies whether the order is maker or taker. ‘M’ stands for Maker and ‘T’ stands for Taker.
        /// - Pagination: The ledger_id is listed in a descending order, from biggest to smallest. The first ledger_id in this page can be found under OK-BEFORE, and the last one can be found under OK-AFTER. It would be easier to retrieve to other ledger_id by referring to these two parameters.
        /// </summary>
        /// <param name="symbol">Trading pair symbol</param>
        /// <param name="orderId">Order ID, Complete transaction details for will be returned if the instrument_id is left blank</param>
        /// <param name="limit">Number of results per request. The maximum is 100; the default is 100</param>
        /// <param name="before">Pagination of data to return records newer than the requested ledger_id</param>
        /// <param name="after">Pagination of data to return records earlier than the requested ledger_id</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<IEnumerable<OkexMarginTransaction>> Margin_GetTransactionDetails(string symbol, long? orderId = null, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default) => Margin_GetTransactionDetails_Async(symbol, orderId, limit, before, after, ct).Result;
        /// <summary>
        /// Retrieve recently filled transaction details. Pagination is supported and the response is sorted with most recent first in reverse chronological order. Data for up to 3 months can be retrieved.
        /// Rate limit: 10 requests per 2 seconds
        /// Notes:
        /// - This API will return 2 pieces of data after a transaction is complete. One is calculated based on the quote currency, while the other is calculated based on the base currency.
        /// - Transaction Fees: New status for spot trading transaction details: fee is either a positive number (invitation rebate) or a negative number (transaction fee deduction).
        /// - Liquidity: The exec_type specifies whether the order is maker or taker. ‘M’ stands for Maker and ‘T’ stands for Taker.
        /// - Pagination: The ledger_id is listed in a descending order, from biggest to smallest. The first ledger_id in this page can be found under OK-BEFORE, and the last one can be found under OK-AFTER. It would be easier to retrieve to other ledger_id by referring to these two parameters.
        /// </summary>
        /// <param name="symbol">Trading pair symbol</param>
        /// <param name="orderId">Order ID, Complete transaction details for will be returned if the instrument_id is left blank</param>
        /// <param name="limit">Number of results per request. The maximum is 100; the default is 100</param>
        /// <param name="before">Pagination of data to return records newer than the requested ledger_id</param>
        /// <param name="after">Pagination of data to return records earlier than the requested ledger_id</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<OkexMarginTransaction>>> Margin_GetTransactionDetails_Async(string symbol, long? orderId = null, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default)
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

            return await SendRequest<IEnumerable<OkexMarginTransaction>>(GetUrl(Endpoints_Margin_TransactionDetails), HttpMethod.Get, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Places an Algo Order
        /// Rate limit：40 requests per 2 seconds
        /// </summary>
        /// <param name="symbol">Trading pair symbol</param>
        /// <param name="type">1. trigger order； 2. trail order; 3. iceberg order; 4. time-weighted average price 5. stop order;</param>
        /// <param name="mode">1：spot 2：margin</param>
        /// <param name="side">buy or sell</param>
        /// <param name="size">Total number of orders must between 0 and 1,000,000, incl. both numbers</param>
        /// <param name="trigger_price">Trigger price must be between 0 and 1,000,000</param>
        /// <param name="trigger_algo_price">Order price must be between 0 and 1,000,000</param>
        /// <param name="trigger_algo_type">1: Limit 2: Market ; trigger price type, default is limit price; when it is the market price, the commission price need not be filled;</param>
        /// <param name="trail_callback_rate">Callback rate must be between 0.001 (0.1%) and 0.05 (5%）</param>
        /// <param name="trail_trigger_price">Trigger price must be between 0 and 1,000,000</param>
        /// <param name="iceberg_algo_variance">Order depth must be between 0.0001 (0.01%) and 0.01 (1%)</param>
        /// <param name="iceberg_avg_amount">Single order average amount,Single average value, fill in the value 1/1000 of the total amount \ <= X \ <= total amount</param>
        /// <param name="iceberg_limit_price">Price limit must be between 0 and 1,000,000</param>
        /// <param name="twap_sweep_range">Auto-cancelling order range must be between 0.005 (0.5%) and 0.01 (1%), incl. both numbers</param>
        /// <param name="twap_sweep_ratio">Auto-cancelling order rate must be between 0.01 and 1, incl. both numbers</param>
        /// <param name="twap_single_limit">Single order limit,fill in the value 1/1000 of the total amount \ <= X \ <= total amount</param>
        /// <param name="twap_limit_price">Price limit must be between 0 and 1,000,000, incl, 1,000,000</param>
        /// <param name="twap_time_interval">Time interval must be between 5 and 120, incl. both numbers</param>
        /// <param name="tp_trigger_type">1:limit 2:market；TP trigger type，The default is limit price；</param>
        /// <param name="tp_trigger_price">TP trigger price must be between 0 and 1,000,000</param>
        /// <param name="tp_price">TP order price must be between 0 and 1,000,000</param>
        /// <param name="sl_trigger_type">1:limit 2:market；TP trigger type，The default is limit price；When it is the market price, the tp_price does not need to be filled;</param>
        /// <param name="sl_trigger_price">SL trigger price must be between 0 and 1,000,000</param>
        /// <param name="sl_price">SL order price must be between 0 and 1,000,000</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<OkexSpotAlgoPlacedOrder> Margin_AlgoPlaceOrder(
            /* General Parameters */
            string symbol,
            OkexAlgoOrderType type,
            OkexMarket mode,
            OkexSpotOrderSide side,
            decimal size,

            /* Trigger Order Parameters */
            decimal? trigger_price = null,
            decimal? trigger_algo_price = null,
            OkexAlgoPriceType? trigger_algo_type = null,

            /* Trail Order Parameters */
            decimal? trail_callback_rate = null,
            decimal? trail_trigger_price = null,

            /* Iceberg Order Parameters (Maximum 6 orders) */
            decimal? iceberg_algo_variance = null,
            decimal? iceberg_avg_amount = null,
            decimal? iceberg_limit_price = null,

            /* TWAP Parameters (Maximum 6 orders) */
            decimal? twap_sweep_range = null,
            decimal? twap_sweep_ratio = null,
            int? twap_single_limit = null,
            decimal? twap_limit_price = null,
            int? twap_time_interval = null,

            /* Stop Order Parameters */
            OkexAlgoPriceType? tp_trigger_type = null,
            decimal? tp_trigger_price = null,
            decimal? tp_price = null,
            OkexAlgoPriceType? sl_trigger_type = null,
            decimal? sl_trigger_price = null,
            decimal? sl_price = null,

            /* Cancellation Token */
            CancellationToken ct = default)
            => Spot_AlgoPlaceOrder_Async(
            symbol,
            type,
            mode,
            side,
            size,

            /* Trigger Order Parameters */
            trigger_price,
            trigger_algo_price,
            trigger_algo_type,

            /* Trail Order Parameters */
            trail_callback_rate,
            trail_trigger_price,

            /* Iceberg Order Parameters (Maximum 6 orders) */
            iceberg_algo_variance,
            iceberg_avg_amount,
            iceberg_limit_price,

            /* TWAP Parameters (Maximum 6 orders) */
            twap_sweep_range,
            twap_sweep_ratio,
            twap_single_limit,
            twap_limit_price,
            twap_time_interval,

            /* Stop Order Parameters */
            tp_trigger_type,
            tp_trigger_price,
            tp_price,
            sl_trigger_type,
            sl_trigger_price,
            sl_price,

            /* Cancellation Token */
            ct).Result;
        /// <summary>
        /// Places an Algo Order
        /// Rate limit：40 requests per 2 seconds
        /// </summary>
        /// <param name="symbol">Trading pair symbol</param>
        /// <param name="type">1. trigger order； 2. trail order; 3. iceberg order; 4. time-weighted average price 5. stop order;</param>
        /// <param name="mode">1：spot 2：margin</param>
        /// <param name="side">buy or sell</param>
        /// <param name="size">Total number of orders must between 0 and 1,000,000, incl. both numbers</param>
        /// <param name="trigger_price">Trigger price must be between 0 and 1,000,000</param>
        /// <param name="trigger_algo_price">Order price must be between 0 and 1,000,000</param>
        /// <param name="trigger_algo_type">1: Limit 2: Market ; trigger price type, default is limit price; when it is the market price, the commission price need not be filled;</param>
        /// <param name="trail_callback_rate">Callback rate must be between 0.001 (0.1%) and 0.05 (5%）</param>
        /// <param name="trail_trigger_price">Trigger price must be between 0 and 1,000,000</param>
        /// <param name="iceberg_algo_variance">Order depth must be between 0.0001 (0.01%) and 0.01 (1%)</param>
        /// <param name="iceberg_avg_amount">Single order average amount,Single average value, fill in the value 1/1000 of the total amount \ <= X \ <= total amount</param>
        /// <param name="iceberg_limit_price">Price limit must be between 0 and 1,000,000</param>
        /// <param name="twap_sweep_range">Auto-cancelling order range must be between 0.005 (0.5%) and 0.01 (1%), incl. both numbers</param>
        /// <param name="twap_sweep_ratio">Auto-cancelling order rate must be between 0.01 and 1, incl. both numbers</param>
        /// <param name="twap_single_limit">Single order limit,fill in the value 1/1000 of the total amount \ <= X \ <= total amount</param>
        /// <param name="twap_limit_price">Price limit must be between 0 and 1,000,000, incl, 1,000,000</param>
        /// <param name="twap_time_interval">Time interval must be between 5 and 120, incl. both numbers</param>
        /// <param name="tp_trigger_type">1:limit 2:market；TP trigger type，The default is limit price；</param>
        /// <param name="tp_trigger_price">TP trigger price must be between 0 and 1,000,000</param>
        /// <param name="tp_price">TP order price must be between 0 and 1,000,000</param>
        /// <param name="sl_trigger_type">1:limit 2:market；TP trigger type，The default is limit price；When it is the market price, the tp_price does not need to be filled;</param>
        /// <param name="sl_trigger_price">SL trigger price must be between 0 and 1,000,000</param>
        /// <param name="sl_price">SL order price must be between 0 and 1,000,000</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public Task<WebCallResult<OkexSpotAlgoPlacedOrder>> Margin_AlgoPlaceOrder_Async(
            /* General Parameters */
            string symbol,
            OkexAlgoOrderType type,
            OkexMarket mode,
            OkexSpotOrderSide side,
            decimal size,

            /* Trigger Order Parameters */
            decimal? trigger_price = null,
            decimal? trigger_algo_price = null,
            OkexAlgoPriceType? trigger_algo_type = null,

            /* Trail Order Parameters */
            decimal? trail_callback_rate = null,
            decimal? trail_trigger_price = null,

            /* Iceberg Order Parameters (Maximum 6 orders) */
            decimal? iceberg_algo_variance = null,
            decimal? iceberg_avg_amount = null,
            decimal? iceberg_limit_price = null,

            /* TWAP Parameters (Maximum 6 orders) */
            decimal? twap_sweep_range = null,
            decimal? twap_sweep_ratio = null,
            int? twap_single_limit = null,
            decimal? twap_limit_price = null,
            int? twap_time_interval = null,

            /* Stop Order Parameters */
            OkexAlgoPriceType? tp_trigger_type = null,
            decimal? tp_trigger_price = null,
            decimal? tp_price = null,
            OkexAlgoPriceType? sl_trigger_type = null,
            decimal? sl_trigger_price = null,
            decimal? sl_price = null,

            /* Cancellation Token */
            CancellationToken ct = default) => Spot_AlgoPlaceOrder_Async(
            symbol,
            type,
            mode,
            side,
            size,

            /* Trigger Order Parameters */
            trigger_price,
            trigger_algo_price,
            trigger_algo_type,

            /* Trail Order Parameters */
            trail_callback_rate,
            trail_trigger_price,

            /* Iceberg Order Parameters (Maximum 6 orders) */
            iceberg_algo_variance,
            iceberg_avg_amount,
            iceberg_limit_price,

            /* TWAP Parameters (Maximum 6 orders) */
            twap_sweep_range,
            twap_sweep_ratio,
            twap_single_limit,
            twap_limit_price,
            twap_time_interval,

            /* Stop Order Parameters */
            tp_trigger_type,
            tp_trigger_price,
            tp_price,
            sl_trigger_type,
            sl_trigger_price,
            sl_price,

            /* Cancellation Token */
            ct);

        /// <summary>
        /// If user use "algo_id" to cancel unfulfilled orders, they can cancel a maximum of 6 iceberg/TWAP or 10 trigger/trail orders at the same time.
        /// Rate limit：20 requests per 2 seconds
        /// Examples:
        /// - Single Order Cancellation: POST /api/spot/v3/cancel_batch_algos{"instrument_id": "BTC-USDT","order_type":"1","algo_ids": ["1600593327162368"]}
        /// - Batch Order Cancellation: POST /api/spot/v3/cancel_batch_algos{"instrument_id": "BTC-USDT","order_type":"1","algo_ids":["1600593327162368","1600593327162369"]}
        /// </summary>
        /// <param name="symbol">Trading pair symbol</param>
        /// <param name="type">1. trigger order; 2. trail order; 3. iceberg order; 4. time-weighted average price ; 5. stop order</param>
        /// <param name="algo_ids">Cancel specific order ID</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns>
        /// Return Parameter: Return parameter is the order ID of canceled orders. This does not mean that the orders are successfully canceled. Orders that are pending cannot be canceled, only unfulfilled orders can be canceled.
        /// Description: This does not guarantee orders are canceled successfully. Users are advised to request the order list to confirm after using the cancelation endpoint.
        /// </returns>
        public WebCallResult<OkexSpotAlgoCancelledOrder> Margin_AlgoCancelOrder(string symbol, OkexAlgoOrderType type, IEnumerable<string> algo_ids, CancellationToken ct = default) => Spot_AlgoCancelOrder_Async(symbol, type, algo_ids, ct).Result;
        /// <summary>
        /// If user use "algo_id" to cancel unfulfilled orders, they can cancel a maximum of 6 iceberg/TWAP or 10 trigger/trail orders at the same time.
        /// Rate limit：20 requests per 2 seconds
        /// Examples:
        /// - Single Order Cancellation: POST /api/spot/v3/cancel_batch_algos{"instrument_id": "BTC-USDT","order_type":"1","algo_ids": ["1600593327162368"]}
        /// - Batch Order Cancellation: POST /api/spot/v3/cancel_batch_algos{"instrument_id": "BTC-USDT","order_type":"1","algo_ids":["1600593327162368","1600593327162369"]}
        /// </summary>
        /// <param name="symbol">Trading pair symbol</param>
        /// <param name="type">1. trigger order; 2. trail order; 3. iceberg order; 4. time-weighted average price ; 5. stop order</param>
        /// <param name="algo_ids">Cancel specific order ID</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns>
        /// Return Parameter: Return parameter is the order ID of canceled orders. This does not mean that the orders are successfully canceled. Orders that are pending cannot be canceled, only unfulfilled orders can be canceled.
        /// Description: This does not guarantee orders are canceled successfully. Users are advised to request the order list to confirm after using the cancelation endpoint.
        /// </returns>
        public Task<WebCallResult<OkexSpotAlgoCancelledOrder>> Margin_AlgoCancelOrder_Async(string symbol, OkexAlgoOrderType type, IEnumerable<string> algo_ids, CancellationToken ct = default) => Spot_AlgoCancelOrder_Async(symbol, type, algo_ids, ct);

        /// <summary>
        /// Obtaining Order List
        /// Rate limit：20 requests per 2 seconds
        /// </summary>
        /// <param name="symbol">Trading pair symbol</param>
        /// <param name="type">1. trigger order; 2. trail order; 3. iceberg order; 4. time-weighted average price ; 5. stop order</param>
        /// <param name="status">[Status and algo_ids are mandatory, select either one] Order status: (1. Pending; 2. 2. Effective; 3. Cancelled; 4. Partially effective; 5. Paused; 6. Order failed [Status 4 and 5 only applies to iceberg and TWAP orders]</param>
        /// <param name="algo_ids">[status and algo_ids are mandatory, select either one] Enquiry specific order ID</param>
        /// <param name="limit">[Optional] Request page content after this ID (updated records)</param>
        /// <param name="before">[Optional] Request page content before this ID (past records)</param>
        /// <param name="after">[Optional] The number of results returned by the page. Default and maximum are both 100 (see the description on page for more details)</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns>Symbol grouped algo orders list. Dictionary&lt;string: symbol, IEnumerable&lt;OkexSpotAlgoOrder&gt;: algo orders&gt;</returns>
        /// <returns></returns>
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8604 // Possible null reference argument.
        public WebCallResult<Dictionary<string, IEnumerable<OkexSpotAlgoOrder>>> Margin_AlgoGetOrders(string symbol, OkexAlgoOrderType type, OkexAlgoStatus? status = null, IEnumerable<string> algo_ids = null, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default) => Spot_AlgoGetOrders_Async(symbol, type, status, algo_ids, limit, before, after, ct).Result;
#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
        /// <summary>
        /// Obtaining Order List
        /// Rate limit：20 requests per 2 seconds
        /// </summary>
        /// <param name="symbol">Trading pair symbol</param>
        /// <param name="type">1. trigger order; 2. trail order; 3. iceberg order; 4. time-weighted average price ; 5. stop order</param>
        /// <param name="status">[Status and algo_ids are mandatory, select either one] Order status: (1. Pending; 2. 2. Effective; 3. Cancelled; 4. Partially effective; 5. Paused; 6. Order failed [Status 4 and 5 only applies to iceberg and TWAP orders]</param>
        /// <param name="algo_ids">[status and algo_ids are mandatory, select either one] Enquiry specific order ID</param>
        /// <param name="limit">[Optional] Request page content after this ID (updated records)</param>
        /// <param name="before">[Optional] Request page content before this ID (past records)</param>
        /// <param name="after">[Optional] The number of results returned by the page. Default and maximum are both 100 (see the description on page for more details)</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns>Symbol grouped algo orders list. Dictionary&lt;string: symbol, IEnumerable&lt;OkexSpotAlgoOrder&gt;: algo orders&gt;</returns>
        /// <returns></returns>
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8604 // Possible null reference argument.
        public Task<WebCallResult<Dictionary<string, IEnumerable<OkexSpotAlgoOrder>>>> Margin_AlgoGetOrders_Async(string symbol, OkexAlgoOrderType type, OkexAlgoStatus? status = null, IEnumerable<string> algo_ids = null, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default) => Spot_AlgoGetOrders_Async(symbol, type, status, algo_ids, limit, before, after, ct);
#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

        #endregion

        #region Public Unsigned Endpoints
        /// <summary>
        /// Get the tag price. This is a public endpoint, no identity verification is needed.
        /// Rate Limit: 20 requests per 2 seconds
        /// </summary>
        /// <param name="symbol">Trading pair symbol</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<OkexMarginMarkPrice> Margin_GetMarkPrice(string symbol, CancellationToken ct = default) => Margin_GetMarkPrice_Async(symbol, ct).Result;
        public async Task<WebCallResult<OkexMarginMarkPrice>> Margin_GetMarkPrice_Async(string symbol, CancellationToken ct = default)
        {
            symbol = symbol.ValidateSymbol();
            return await SendRequest<OkexMarginMarkPrice>(GetUrl(Endpoints_Margin_MarkPrice, symbol), HttpMethod.Get, ct).ConfigureAwait(false);
        }
        #endregion

        #endregion

    }
}
