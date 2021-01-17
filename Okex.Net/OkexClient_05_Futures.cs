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
    public partial class OkexClient : IOkexClientFutures
    {
        #region Futures Trading API

        #region Private Signed Endpoints

        /// <summary>
        /// Retrieve the information on all your positions in the futures account. You are recommended to get the information one token at a time to improve performance.
        /// Rate Limit: 5 requests per 2 seconds (Speed limit based on UserID)
        /// </summary>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<OkexFuturesPositions> Futures_GetPositions(CancellationToken ct = default) => Futures_GetPositions_Async(ct).Result;
        /// <summary>
        /// Retrieve the information on all your positions in the futures account. You are recommended to get the information one token at a time to improve performance.
        /// Rate Limit: 5 requests per 2 seconds (Speed limit based on UserID)
        /// </summary>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<OkexFuturesPositions>> Futures_GetPositions_Async(CancellationToken ct = default)
        {
            return await SendRequest<OkexFuturesPositions>(GetUrl(Endpoints_Futures_Positions), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve information on your positions of a single contract.
        /// Rate Limit: 20 requests per 2 seconds (Depending on the underlying speed limit)
        /// </summary>
        /// <param name="contract">Contract ID, e.g.BTC-USD-180213 ,BTC-USDT-191227</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<OkexFuturesPositionsOfContract> Futures_GetPositions(string contract, CancellationToken ct = default) => Futures_GetPositions_Async(contract, ct).Result;
        /// <summary>
        /// Retrieve information on your positions of a single contract.
        /// Rate Limit: 20 requests per 2 seconds (Depending on the underlying speed limit)
        /// </summary>
        /// <param name="contract">Contract ID, e.g.BTC-USD-180213 ,BTC-USDT-191227</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<OkexFuturesPositionsOfContract>> Futures_GetPositions_Async(string contract, CancellationToken ct = default)
        {
            return await SendRequest<OkexFuturesPositionsOfContract>(GetUrl(Endpoints_Futures_PositionsOfContract, contract), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve information from all tokens in the futures account. You are recommended to get the information one token at a time to improve performance.
        /// Rate Limit: 1 per 10 seconds (Speed limit based on UserID)
        /// Notes:
        /// - For "all open interests/all account info" futures account endpoints, if no position/token is held then no response will be returned. For "single open interests/single account info" endpoints, if no position/token is held then the response will return with default value.
        /// - Fixed-margin mode:
        /// - - Account equity = Balance of Funding Account + Balance of Fixed-margin Account + RPL (Realized Profit and Loss) of All Contracts + UPL (Unrealized Profit and Loss) of All Contracts
        /// - - Available Margin = Balance of Funding Account + Balance of Fixed-margin Account + RPL (Realized Profit and Loss) of the Contract - Maintenance Margin of the Open Interests - Margin frozen for Open Orders
        /// - Cross-margin mode:
        /// - - Account Equity = Balance of Fund Account + RPL (Realized Profit and Loss) of All Contracts + UPL (Unrealized Profit and Loss) of All Contracts
        /// - - Available Margin = Balance of Fund Account + RPL (Realized Profit and Loss) of All Contracts + UPL (Unrealized Profit and Loss) of All Contracts - Maintenance Margin of the Open Interests - Margin frozen for Open Orders
        /// </summary>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<OkexFuturesBalances> Futures_GetBalances(CancellationToken ct = default) => Futures_GetBalances_Async(ct).Result;
        /// <summary>
        /// Retrieve information from all tokens in the futures account. You are recommended to get the information one token at a time to improve performance.
        /// Rate Limit: 1 per 10 seconds (Speed limit based on UserID)
        /// Notes:
        /// - For "all open interests/all account info" futures account endpoints, if no position/token is held then no response will be returned. For "single open interests/single account info" endpoints, if no position/token is held then the response will return with default value.
        /// - Fixed-margin mode:
        /// - - Account equity = Balance of Funding Account + Balance of Fixed-margin Account + RPL (Realized Profit and Loss) of All Contracts + UPL (Unrealized Profit and Loss) of All Contracts
        /// - - Available Margin = Balance of Funding Account + Balance of Fixed-margin Account + RPL (Realized Profit and Loss) of the Contract - Maintenance Margin of the Open Interests - Margin frozen for Open Orders
        /// - Cross-margin mode:
        /// - - Account Equity = Balance of Fund Account + RPL (Realized Profit and Loss) of All Contracts + UPL (Unrealized Profit and Loss) of All Contracts
        /// - - Available Margin = Balance of Fund Account + RPL (Realized Profit and Loss) of All Contracts + UPL (Unrealized Profit and Loss) of All Contracts - Maintenance Margin of the Open Interests - Margin frozen for Open Orders
        /// </summary>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<OkexFuturesBalances>> Futures_GetBalances_Async(CancellationToken ct = default)
        {
            return await SendRequest<OkexFuturesBalances>(GetUrl(Endpoints_Futures_Accounts), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve the futures account information of a single token.
        /// Rate Limit: 20 requests per 2 seconds (Depending on the underlying speed limit)
        /// Notes:
        /// - For "all open interests/all account info" futures account endpoints, if no position/token is held then no response will be returned. For "single open interests/single account info" endpoints, if no position/token is held then the response will return with default value.
        /// - Fixed-margin mode:
        /// - - Account equity = Balance of Funding Account + Balance of Fixed-margin Account + RPL (Realized Profit and Loss) of All Contracts + UPL (Unrealized Profit and Loss) of All Contracts
        /// - - Available Margin = Balance of Funding Account + Balance of Fixed-margin Account + RPL (Realized Profit and Loss) of the Contract - Maintenance Margin of the Open Interests - Margin frozen for Open Orders
        /// - Cross-margin mode:
        /// - - Account Equity = Balance of Fund Account + RPL (Realized Profit and Loss) of All Contracts + UPL (Unrealized Profit and Loss) of All Contracts
        /// - - Available Margin = Balance of Fund Account + RPL (Realized Profit and Loss) of All Contracts + UPL (Unrealized Profit and Loss) of All Contracts - Maintenance Margin of the Open Interests - Margin frozen for Open Orders
        /// </summary>
        /// <param name="underlying">Underlying index eg：BTC-USD BTC-USDT</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<OkexFuturesBalance> Futures_GetBalances(string underlying, CancellationToken ct = default) => Futures_GetBalances_Async(underlying, ct).Result;
        /// <summary>
        /// Retrieve the futures account information of a single token.
        /// Rate Limit: 20 requests per 2 seconds (Depending on the underlying speed limit)
        /// Notes:
        /// - For "all open interests/all account info" futures account endpoints, if no position/token is held then no response will be returned. For "single open interests/single account info" endpoints, if no position/token is held then the response will return with default value.
        /// - Fixed-margin mode:
        /// - - Account equity = Balance of Funding Account + Balance of Fixed-margin Account + RPL (Realized Profit and Loss) of All Contracts + UPL (Unrealized Profit and Loss) of All Contracts
        /// - - Available Margin = Balance of Funding Account + Balance of Fixed-margin Account + RPL (Realized Profit and Loss) of the Contract - Maintenance Margin of the Open Interests - Margin frozen for Open Orders
        /// - Cross-margin mode:
        /// - - Account Equity = Balance of Fund Account + RPL (Realized Profit and Loss) of All Contracts + UPL (Unrealized Profit and Loss) of All Contracts
        /// - - Available Margin = Balance of Fund Account + RPL (Realized Profit and Loss) of All Contracts + UPL (Unrealized Profit and Loss) of All Contracts - Maintenance Margin of the Open Interests - Margin frozen for Open Orders
        /// </summary>
        /// <param name="underlying">Underlying index eg：BTC-USD BTC-USDT</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<OkexFuturesBalance>> Futures_GetBalances_Async(string underlying, CancellationToken ct = default)
        {
            return await SendRequest<OkexFuturesBalance>(GetUrl(Endpoints_Futures_AccountsOfCurrency, underlying), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve leverage ratio of the futures account
        /// Rate Limit: 5 requests per 2 seconds (Depending on the underlying speed limit)
        /// Notes:
        /// For cross-margin mode, only one leverage ratio is allowed per trading pair. For fixed-margin mode, one leverage ratio is allowed per contract per side (long or short).
        /// Example:
        /// - Cross-magin mode: if you are holding a 10x BTC quarterly contract, then you must also use 10x leverage for opening any other new BTC contracts. But you can choose 20x leverage for opening contracts of other tokens.
        /// - Fixed-margin mode: if you have opened a long 10x BTC quarterly contract, then you can choose 10x for opening new long weekly contracts, 20x for short contracts and 20x for bi-weekly BTC contracts.
        /// </summary>
        /// <param name="underlying">Underlying index，eg：BTC-USD BTC-USDT</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<OkexFuturesLeverage> Futures_GetLeverage(string underlying, CancellationToken ct = default) => Futures_GetLeverage_Async(underlying, ct).Result;

        /// <summary>
        /// Retrieve leverage ratio of the futures account
        /// Rate Limit: 5 requests per 2 seconds (Depending on the underlying speed limit)
        /// Notes:
        /// For cross-margin mode, only one leverage ratio is allowed per trading pair. For fixed-margin mode, one leverage ratio is allowed per contract per side (long or short).
        /// Example:
        /// - Cross-magin mode: if you are holding a 10x BTC quarterly contract, then you must also use 10x leverage for opening any other new BTC contracts. But you can choose 20x leverage for opening contracts of other tokens.
        /// - Fixed-margin mode: if you have opened a long 10x BTC quarterly contract, then you can choose 10x for opening new long weekly contracts, 20x for short contracts and 20x for bi-weekly BTC contracts.
        /// </summary>
        /// <param name="underlying">Underlying index，eg：BTC-USD BTC-USDT</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<OkexFuturesLeverage>> Futures_GetLeverage_Async(string underlying, CancellationToken ct = default)
        {
            return await SendRequest<OkexFuturesLeverage>(GetUrl(Endpoints_Futures_GetFuturesLeverage, underlying), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// This is used to set the leverage level for assets in the futures account. When placing an order, the system will deploy the leverage level you set accordingly.
        /// Rate Limit: 5 requests per 2 seconds (Depending on the underlying speed limit)
        /// Example Request
        /// - (Cross_margin): POST /api/futures/v3/accounts/btc-usd/leverage{"leverage":"10"}
        /// - (Fixed_margin): POST /api/futures/v3/accounts/btc-usd/leverage{"instrument_id":"BTC-USD-180213","direction":"long","leverage":"10"}
        /// Notes
        /// - For cross-margin mode, only one leverage ratio is allowed per trading pair. For fixed-margin mode, one leverage ratio is allowed per contract per side (long or short).
        /// </summary>
        /// <param name="mode">Margin Mode</param>
        /// <param name="underlying">	Underlying index，eg：BTC-USD BTC-USDT</param>
        /// <param name="leverage">1-100x leverage</param>
        /// <param name="instrument_id">Contract ID, e.g. BTC-USD-180213,BTC-USDT-191227</param>
        /// <param name="direction">opening side (long or short)</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<OkexFuturesLeverage> Futures_SetLeverage(OkexFuturesMarginMode mode, string underlying, int leverage, string? instrument_id = null, OkexFuturesDirection? direction = null, CancellationToken ct = default) => Futures_SetLeverage_Async(mode, underlying, leverage, instrument_id, direction, ct).Result;
        /// <summary>
        /// This is used to set the leverage level for assets in the futures account. When placing an order, the system will deploy the leverage level you set accordingly.
        /// Rate Limit: 5 requests per 2 seconds (Depending on the underlying speed limit)
        /// Example Request
        /// - (Cross_margin): POST /api/futures/v3/accounts/btc-usd/leverage{"leverage":"10"}
        /// - (Fixed_margin): POST /api/futures/v3/accounts/btc-usd/leverage{"instrument_id":"BTC-USD-180213","direction":"long","leverage":"10"}
        /// Notes
        /// - For cross-margin mode, only one leverage ratio is allowed per trading pair. For fixed-margin mode, one leverage ratio is allowed per contract per side (long or short).
        /// </summary>
        /// <param name="mode">Margin Mode</param>
        /// <param name="underlying">	Underlying index，eg：BTC-USD BTC-USDT</param>
        /// <param name="leverage">1-100x leverage</param>
        /// <param name="instrument_id">Contract ID, e.g. BTC-USD-180213,BTC-USDT-191227</param>
        /// <param name="direction">opening side (long or short)</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<OkexFuturesLeverage>> Futures_SetLeverage_Async(OkexFuturesMarginMode mode, string underlying, int leverage, string? instrument_id = null, OkexFuturesDirection? direction = null, CancellationToken ct = default)
        {

            leverage.ValidateIntBetween(nameof(leverage), 1, 100);

            var parameters = new Dictionary<string, object>();
            if (mode == OkexFuturesMarginMode.Crossed)
            {
                if (string.IsNullOrEmpty(underlying))
                    throw new ArgumentException("underlying must be provided for Crossed Margin");

                parameters.Add("leverage", leverage.ToString(ci));
            }
            else if (mode == OkexFuturesMarginMode.Fixed)
            {
                if (string.IsNullOrEmpty(underlying))
                    throw new ArgumentException("underlying must be provided for Fixed Margin");
                if (string.IsNullOrEmpty(instrument_id))
                    throw new ArgumentException("instrument_id must be provided for Fixed Margin");

                parameters.Add("leverage", leverage.ToString(ci));
                parameters.Add("direction", JsonConvert.SerializeObject(direction, new FuturesDirectionConverter(false)));
                parameters.Add("instrument_id", instrument_id!);
            }

            return await SendRequest<OkexFuturesLeverage>(GetUrl(Endpoints_Futures_SetFuturesLeverage, underlying), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve the bills of the futures account. The bill refers to all the records that results in changing the balance of an account. This API can retrieve data in the last Three months.
        /// Rate Limit: 5 requests per 2 seconds (Depending on the underlying speed limit)
        /// </summary>
        /// <param name="underlying">underlying，eg：BTC-USD BTC-USDT</param>
        /// <param name="type">1:Open Long 2:Open Short 3:Close Long 4:Close Short 5:Transaction Fee 6:Transfer In，7:Transfer Out 8:Settled RPL 13: Full Liquidation of Long 14: Full Liquidation of Short 15: Delivery Long 16: Delivery Short 17:Settled UPL Long 18:Settled UPL Short 20:Partial Liquidation of Short 21:Partial Liquidation of Long</param>
        /// <param name="limit">Number of results per request. The maximum is 100; the default is 100</param>
        /// <param name="before">Pagination of data to return records newer than the requested ledger_id</param>
        /// <param name="after">Pagination of data to return records earlier than the requested ledger_id</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<IEnumerable<OkexFuturesBill>> Futures_GetSymbolBills(string underlying, OkexFuturesBillType? type = null, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default) => Futures_GetSymbolBills_Async(underlying, type, limit, before, after, ct).Result;
        /// <summary>
        /// Retrieve the bills of the futures account. The bill refers to all the records that results in changing the balance of an account. This API can retrieve data in the last Three months.
        /// Rate Limit: 5 requests per 2 seconds (Depending on the underlying speed limit)
        /// </summary>
        /// <param name="underlying">underlying，eg：BTC-USD BTC-USDT</param>
        /// <param name="type">1:Open Long 2:Open Short 3:Close Long 4:Close Short 5:Transaction Fee 6:Transfer In，7:Transfer Out 8:Settled RPL 13: Full Liquidation of Long 14: Full Liquidation of Short 15: Delivery Long 16: Delivery Short 17:Settled UPL Long 18:Settled UPL Short 20:Partial Liquidation of Short 21:Partial Liquidation of Long</param>
        /// <param name="limit">Number of results per request. The maximum is 100; the default is 100</param>
        /// <param name="before">Pagination of data to return records newer than the requested ledger_id</param>
        /// <param name="after">Pagination of data to return records earlier than the requested ledger_id</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<IEnumerable<OkexFuturesBill>>> Futures_GetSymbolBills_Async(string underlying, OkexFuturesBillType? type = null, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default)
        {
            underlying = underlying.ValidateSymbol();
            limit.ValidateIntBetween(nameof(limit), 1, 100);

            var parameters = new Dictionary<string, object>
            {
                { "limit", limit.ToString(ci) },
            };
            if (type != null) parameters.AddOptionalParameter("type", JsonConvert.SerializeObject(type, new FuturesBillTypeConverter(false)));
            parameters.AddOptionalParameter("before", before?.ToString(ci));
            parameters.AddOptionalParameter("after", after?.ToString(ci));

            return await SendRequest<IEnumerable<OkexFuturesBill>>(GetUrl(Endpoints_Futures_Bills, underlying), HttpMethod.Get, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// OKEx futures trading supports limit and market orders. You can place an order only if you have sufficient funds. Once your order is placed, the amount will be put on hold during the order lifecycle. The assets and amount on hold depends on the order's specific type and parameters.
        /// The futures maximum openable leverage multiple is determined by your positions, pending orders and the number of new orders placed at the time of opening. For details: https://www.okex.com/derivatives/futures/position
        /// Rate Limit: 60 requests per 2 seconds (Speed limit rules: 1) The speed limit is not accumulated between different contracts； 2) Api limit is separated by underlying. Different tenure of the same underlying share the limit； 3) The speed limit between the Coin Margined Futures and the USDT Margined Futures is not accumulated)
        /// Notes:
        /// - instrument_id
        ///   The instrument_id must match a valid contract ID. The contract list is available via the /instruments endpoint.
        /// - client_oid
        ///   The client_oid is optional. It should be a unique ID generated by your trading system. This parameter is used to identify your orders in the public orders feed. No warning is sent when client_oid is not unique.
        ///	  In case of multiple identical client_oid, only the latest entry will be returned.
        ///	- type
        ///	  You can specify the order type when placing an order. If you are not holding any positions, you can only open new positions, either long or short. You can only close the positions that has been already held.
        ///	  The price must be specified in tick size product units. The tick size is the smallest unit of price. Can be obtained through the /instrument interface.
        ///	- price
        ///	  The price is the price of buying or selling a contract. price must be an incremental multiple of the tick_size. tick_size is the smallest incremental unit of price, which is available via the /instruments endpoint.
        ///	- size
        ///	  size is the number of contracts bought or sold. The value must be an integer.
        ///	- match_price
        ///	  The match_price means that you prefer the order to be filled at a best price of the counterpart, where your buy order will be filled at the price of Ask-1. The match_price means that your sell order will be filled at the price of Bid-1.
        ///	- Order life cycle
        ///	  The HTTP Request will respond when an order is either rejected (insufficient funds, invalid parameters, etc) or received (accepted by the matching engine). A 200 response indicates that the order was received and is active.
        ///	  Active orders may execute immediately (depending on price and market conditions) either partially or fully. A partial execution will put the remaining size of the order in the open state. An order that is filled Fully, will go into the completed state.
        ///	  Users listening to streaming market data are encouraged to use the client_oid field to identify their received messages in the feed. The REST response with a server order_id may come after the received message in the public data feed.
        ///	- Response
        ///	  A successful order will be assigned an order id. A successful order is defined as one that has been accepted by the matching engine. Open orders will not expire until filled or canceled.
        /// </summary>
        /// <param name="symbol">Contract ID,e.g. BTC-USD-180213 ,BTC-USDT-191227</param>
        /// <param name="type">1:open long 2:open short 3:close long 4:close short</param>
        /// <param name="size">The number of contracts bought or sold (minimum size as 1)</param>
        /// <param name="timeInForce">‘0’: Normal order. Parameter will be deemed as '0' if left blank. ‘1’: Post only (Order shall be filled only as maker) ‘2’: Fill or Kill (FOK) ‘3’: Immediate or Cancel (IOC) 4：Market</param>
        /// <param name="price">Price of each contract</param>
        /// <param name="match_price">Whether order is placed at best counter party price (‘0’:no ‘1’:yes). The parameter is defaulted as ‘0’. If it is set as '1', the price parameter will be ignored，When posting orders at best bid price, order_type can only be '0' (regular order)</param>
        /// <param name="clientOrderId">You can customize order IDs to identify your orders. The system supports alphabets + numbers(case-sensitive，e.g:A123、a123), or alphabets (case-sensitive，e.g:Abc、abc) only, between 1-32 characters.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<OkexFuturesPlacedOrder> Futures_PlaceOrder(string symbol, OkexFuturesOrderType type, decimal size, OkexFuturesTimeInForce timeInForce = OkexFuturesTimeInForce.NormalOrder, decimal? price = null, bool match_price = false, string? clientOrderId = null, CancellationToken ct = default) => Futures_PlaceOrder_Async(symbol, type, size, timeInForce, price, match_price, clientOrderId, ct).Result;
        /// <summary>
        /// OKEx futures trading supports limit and market orders. You can place an order only if you have sufficient funds. Once your order is placed, the amount will be put on hold during the order lifecycle. The assets and amount on hold depends on the order's specific type and parameters.
        /// The futures maximum openable leverage multiple is determined by your positions, pending orders and the number of new orders placed at the time of opening. For details: https://www.okex.com/derivatives/futures/position
        /// Rate Limit: 60 requests per 2 seconds (Speed limit rules: 1) The speed limit is not accumulated between different contracts； 2) Api limit is separated by underlying. Different tenure of the same underlying share the limit； 3) The speed limit between the Coin Margined Futures and the USDT Margined Futures is not accumulated)
        /// Notes:
        /// - instrument_id
        ///   The instrument_id must match a valid contract ID. The contract list is available via the /instruments endpoint.
        /// - client_oid
        ///   The client_oid is optional. It should be a unique ID generated by your trading system. This parameter is used to identify your orders in the public orders feed. No warning is sent when client_oid is not unique.
        ///	  In case of multiple identical client_oid, only the latest entry will be returned.
        ///	- type
        ///	  You can specify the order type when placing an order. If you are not holding any positions, you can only open new positions, either long or short. You can only close the positions that has been already held.
        ///	  The price must be specified in tick size product units. The tick size is the smallest unit of price. Can be obtained through the /instrument interface.
        ///	- price
        ///	  The price is the price of buying or selling a contract. price must be an incremental multiple of the tick_size. tick_size is the smallest incremental unit of price, which is available via the /instruments endpoint.
        ///	- size
        ///	  size is the number of contracts bought or sold. The value must be an integer.
        ///	- match_price
        ///	  The match_price means that you prefer the order to be filled at a best price of the counterpart, where your buy order will be filled at the price of Ask-1. The match_price means that your sell order will be filled at the price of Bid-1.
        ///	- Order life cycle
        ///	  The HTTP Request will respond when an order is either rejected (insufficient funds, invalid parameters, etc) or received (accepted by the matching engine). A 200 response indicates that the order was received and is active.
        ///	  Active orders may execute immediately (depending on price and market conditions) either partially or fully. A partial execution will put the remaining size of the order in the open state. An order that is filled Fully, will go into the completed state.
        ///	  Users listening to streaming market data are encouraged to use the client_oid field to identify their received messages in the feed. The REST response with a server order_id may come after the received message in the public data feed.
        ///	- Response
        ///	  A successful order will be assigned an order id. A successful order is defined as one that has been accepted by the matching engine. Open orders will not expire until filled or canceled.
        /// </summary>
        /// <param name="symbol">Contract ID,e.g. BTC-USD-180213 ,BTC-USDT-191227</param>
        /// <param name="type">1:open long 2:open short 3:close long 4:close short</param>
        /// <param name="size">The number of contracts bought or sold (minimum size as 1)</param>
        /// <param name="timeInForce">‘0’: Normal order. Parameter will be deemed as '0' if left blank. ‘1’: Post only (Order shall be filled only as maker) ‘2’: Fill or Kill (FOK) ‘3’: Immediate or Cancel (IOC) 4：Market</param>
        /// <param name="price">Price of each contract</param>
        /// <param name="match_price">Whether order is placed at best counter party price (‘0’:no ‘1’:yes). The parameter is defaulted as ‘0’. If it is set as '1', the price parameter will be ignored，When posting orders at best bid price, order_type can only be '0' (regular order)</param>
        /// <param name="clientOrderId">You can customize order IDs to identify your orders. The system supports alphabets + numbers(case-sensitive，e.g:A123、a123), or alphabets (case-sensitive，e.g:Abc、abc) only, between 1-32 characters.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<OkexFuturesPlacedOrder>> Futures_PlaceOrder_Async(string symbol, OkexFuturesOrderType type, decimal size, OkexFuturesTimeInForce timeInForce = OkexFuturesTimeInForce.NormalOrder, decimal? price = null, bool match_price = false, string? clientOrderId = null, CancellationToken ct = default)
        {
            symbol = symbol.ValidateSymbol();
            clientOrderId?.ValidateStringLength("clientOrderId", 0, 32);
            if (clientOrderId != null && !Regex.IsMatch(clientOrderId, "^(([a-z]|[A-Z]|[0-9]){0,32})$"))
                throw new ArgumentException("ClientOrderId supports alphabets (case-sensitive) + numbers, or letters (case-sensitive) between 1-32 characters.");

            var parameters = new Dictionary<string, object>
            {
                { "instrument_id", symbol },
                { "type", JsonConvert.SerializeObject(type, new FuturesOrderTypeConverter(false)) },
                { "size", size.ToString(ci) },
                { "match_price", match_price?"1":"0" },
                { "order_type", JsonConvert.SerializeObject(timeInForce, new FuturesTimeInForceConverter(false)) },
            };
            parameters.AddOptionalParameter("client_oid", clientOrderId);
            parameters.AddOptionalParameter("price", price?.ToString(ci));

            return await SendRequest<OkexFuturesPlacedOrder>(GetUrl(Endpoints_Futures_PlaceOrder), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Place contract orders in a batch. Maximum 10 orders can be placed at a time for each contract.
        /// Rate Limit: 30 requests per 2 seconds (Speed limit rules: 1) The speed limit is not accumulated between different contracts； 2) Api limit is separated by underlying. Different tenure of the same underlying share the limit ； 3) The speed limit between the Coin Margined Futures and the USDT Margined Futures is not accumulated)
        /// Notes:
        /// - The client_oid is optional.It should be a unique ID generated by your trading system.This parameter is used to identify your orers in the public orders feed.No warning is sent when client_oid is not unique.
        /// - In case of multiple identical client_oid, only the latest entry will be returned.
        /// - As long as any of the orders are successful, result returns true. The response message is returned in the same order as that of the orders_data submitted. If the order fails to be placed, order_id is -1.
        /// </summary>
        /// <param name="symbol">Contract ID, e.g.BTC-USD-180213,BTC-USDT-191227</param>
        /// <param name="orders">JSON String for placing multiple orders, for example: [{order_type:"0",price:"5",size:"2",type:"1",match_price:"1"},{order_type:"0",price:"2",size:"3",type:"1",match_price:"1"}] A maximum of 10 orders can be placed. If the match_price is ‘1’, the order_type must be ‘0’</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<OkexFuturesBatchPlacedOrder> Futures_BatchPlaceOrders(string symbol, IEnumerable<OkexFuturesPlaceOrder> orders, CancellationToken ct = default) => Futures_BatchPlaceOrders_Async(symbol, orders, ct).Result;
        /// <summary>
        /// Place contract orders in a batch. Maximum 10 orders can be placed at a time for each contract.
        /// Rate Limit: 30 requests per 2 seconds (Speed limit rules: 1) The speed limit is not accumulated between different contracts； 2) Api limit is separated by underlying. Different tenure of the same underlying share the limit ； 3) The speed limit between the Coin Margined Futures and the USDT Margined Futures is not accumulated)
        /// Notes:
        /// - The client_oid is optional.It should be a unique ID generated by your trading system.This parameter is used to identify your orers in the public orders feed.No warning is sent when client_oid is not unique.
        /// - In case of multiple identical client_oid, only the latest entry will be returned.
        /// - As long as any of the orders are successful, result returns true. The response message is returned in the same order as that of the orders_data submitted. If the order fails to be placed, order_id is -1.
        /// </summary>
        /// <param name="symbol">Contract ID, e.g.BTC-USD-180213,BTC-USDT-191227</param>
        /// <param name="orders">JSON String for placing multiple orders, for example: [{order_type:"0",price:"5",size:"2",type:"1",match_price:"1"},{order_type:"0",price:"2",size:"3",type:"1",match_price:"1"}] A maximum of 10 orders can be placed. If the match_price is ‘1’, the order_type must be ‘0’</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<OkexFuturesBatchPlacedOrder>> Futures_BatchPlaceOrders_Async(string symbol, IEnumerable<OkexFuturesPlaceOrder> orders, CancellationToken ct = default)
        {
            if (orders == null || orders.Count() == 0)
                throw new ArgumentException("Orders cant be null or with zero-elements");

            symbol = symbol.ValidateSymbol();
            for (var i = 0; i < orders.Count(); i++)
            {
                var order = orders.ElementAt(i);
                var suffix = $"(Order: {(i + 1)} of {orders.Count()})";

                order.ClientOrderId?.ValidateStringLength("clientOrderId", 0, 32, messageSuffix: suffix);
                if (order.ClientOrderId != null && !Regex.IsMatch(order.ClientOrderId, "^(([a-z]|[A-Z]|[0-9]){0,32})$"))
                    throw new ArgumentException($"ClientOrderId supports alphabets (case-sensitive) + numbers, or letters (case-sensitive) between 1-32 characters. {suffix}");
            }

            var parameters = new Dictionary<string, object>
            {
                { "instrument_id", symbol },
                { "orders_data", orders },
            };

            return await SendRequest<OkexFuturesBatchPlacedOrder>(GetUrl(Endpoints_Futures_BatchPlaceOrders), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Modify an unfilled order
        /// Rate Limit：40 Requests per 2 seconds
        /// Notes:
        /// - If the error_code value in response is NOT zero, then it means the request was rejected because verification failed.
        /// - In order amendment, only order_id will be used if both order_id and client_oid are passed in values at the same time, and client_oid will be ignored.
        /// - The client_oid should be unique.No warning is sent when client_oid is not unique.
        /// - In case of multiple identical client_oid, only the latest entry will be returned.
        /// - If the order cannot be modified because it has already been filled or canceled, the reason will be returned with the error message.
        /// </summary>
        /// <param name="symbol">Contract ID,e.g. BTC-USD-180213 ,BTC-USDT-191227</param>
        /// <param name="orderId">Either client_oid or order_id must be present. Order ID。</param>
        /// <param name="clientOrderId">Either client_oid or order_id must be present. client_oid should be the same Client-supplied order ID when submitting the order. It should be comprised of alpha-numeric characters with length 1 to 32. Both uppercase and lowercase are supported.</param>
        /// <param name="requestId">You can provide the request_id. If provided, the response will include the corresponding request_id to help you identify the request. It should be comprised of alpha-numeric characters with length 1 to 32. Both uppercase and lowercase are supported.</param>
        /// <param name="newSize">Must provide at least one of new_size or new_price. When modifying a partially filled order, the new_size should include the amount that has been filled.</param>
        /// <param name="newPrice">Must provide at least one of new_size or new_price. Modifies the price.</param>
        /// <param name="cancelOnFail">When the order amendment fails, whether to cancell the order automatically: 0: Don't cancel the order automatically 1: Automatically cancel the order. The default value is 0.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<OkexFuturesPlacedOrder> Futures_ModifyOrder(string symbol, long? orderId = null, string? clientOrderId = null, string? requestId = null, decimal? newSize = null, decimal? newPrice = null, bool? cancelOnFail = null, CancellationToken ct = default) => Futures_ModifyOrder_Async(symbol, orderId, clientOrderId, requestId, newSize, newPrice, cancelOnFail, ct).Result;
        /// <summary>
        /// Modify an unfilled order
        /// Rate Limit：40 Requests per 2 seconds
        /// Notes:
        /// - If the error_code value in response is NOT zero, then it means the request was rejected because verification failed.
        /// - In order amendment, only order_id will be used if both order_id and client_oid are passed in values at the same time, and client_oid will be ignored.
        /// - The client_oid should be unique.No warning is sent when client_oid is not unique.
        /// - In case of multiple identical client_oid, only the latest entry will be returned.
        /// - If the order cannot be modified because it has already been filled or canceled, the reason will be returned with the error message.
        /// </summary>
        /// <param name="symbol">Contract ID,e.g. BTC-USD-180213 ,BTC-USDT-191227</param>
        /// <param name="orderId">Either client_oid or order_id must be present. Order ID。</param>
        /// <param name="clientOrderId">Either client_oid or order_id must be present. client_oid should be the same Client-supplied order ID when submitting the order. It should be comprised of alpha-numeric characters with length 1 to 32. Both uppercase and lowercase are supported.</param>
        /// <param name="requestId">You can provide the request_id. If provided, the response will include the corresponding request_id to help you identify the request. It should be comprised of alpha-numeric characters with length 1 to 32. Both uppercase and lowercase are supported.</param>
        /// <param name="newSize">Must provide at least one of new_size or new_price. When modifying a partially filled order, the new_size should include the amount that has been filled.</param>
        /// <param name="newPrice">Must provide at least one of new_size or new_price. Modifies the price.</param>
        /// <param name="cancelOnFail">When the order amendment fails, whether to cancell the order automatically: 0: Don't cancel the order automatically 1: Automatically cancel the order. The default value is 0.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<OkexFuturesPlacedOrder>> Futures_ModifyOrder_Async(string symbol, long? orderId = null, string? clientOrderId = null, string? requestId = null, decimal? newSize = null, decimal? newPrice = null, bool? cancelOnFail = null, CancellationToken ct = default)
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
            if (orderId.HasValue) parameters.AddOptionalParameter("order_id", orderId?.ToString(ci));
            if (!string.IsNullOrEmpty(clientOrderId)) parameters.AddOptionalParameter("client_oid", clientOrderId);
            if (cancelOnFail.HasValue) parameters.AddOptionalParameter("cancel_on_fail", cancelOnFail.Value ? "1" : "0");
            if (!string.IsNullOrEmpty(requestId)) parameters.AddOptionalParameter("request_id", requestId);
            if (newSize.HasValue) parameters.AddOptionalParameter("new_size", newSize?.ToString(ci));
            if (newPrice.HasValue) parameters.AddOptionalParameter("new_price", newPrice?.ToString(ci));

            return await SendRequest<OkexFuturesPlacedOrder>(GetUrl(Endpoints_Futures_ModifyOrder, symbol), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Batch modify open orders; a maximum of 10 orders per underlying can be modified.
        /// Rate Limit：20 Requests per 2 seconds
        /// Notes:
        /// - When an Order ID is listed to be modified in the result list, it does not imply the order has successfully been modified. Orders in the middle of being filled cannot be modified; only unfilled orders can be modified.
        /// - If the error_code value in response is NOT zero, then it means the request was rejected because verification failed.
        /// - When using client_oid for batch order modifications, you need to make sure the ID is unique. In case of multiple identical client_oid, only the latest entry will be returned.
        /// - Modifications of orders are not guaranteed. After placing a modification order you should confirm they are successfully modified by requesting the "Order List" endpoint.
        /// </summary>
        /// <param name="symbol">Contract ID,e.g. BTC-USD-180213 ,BTC-USDT-191227</param>
        /// <param name="orders">Orders to be modified</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<OkexFuturesBatchPlacedOrder> Futures_BatchModifyOrders(string symbol, IEnumerable<OkexFuturesModifyOrder> orders, CancellationToken ct = default) => Futures_BatchModifyOrders_Async(symbol, orders, ct).Result;
        /// <summary>
        /// Batch modify open orders; a maximum of 10 orders per underlying can be modified.
        /// Rate Limit：20 Requests per 2 seconds
        /// Notes:
        /// - When an Order ID is listed to be modified in the result list, it does not imply the order has successfully been modified. Orders in the middle of being filled cannot be modified; only unfilled orders can be modified.
        /// - If the error_code value in response is NOT zero, then it means the request was rejected because verification failed.
        /// - When using client_oid for batch order modifications, you need to make sure the ID is unique. In case of multiple identical client_oid, only the latest entry will be returned.
        /// - Modifications of orders are not guaranteed. After placing a modification order you should confirm they are successfully modified by requesting the "Order List" endpoint.
        /// </summary>
        /// <param name="symbol">Contract ID,e.g. BTC-USD-180213 ,BTC-USDT-191227</param>
        /// <param name="orders">Orders to be modified</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<OkexFuturesBatchPlacedOrder>> Futures_BatchModifyOrders_Async(string symbol, IEnumerable<OkexFuturesModifyOrder> orders, CancellationToken ct = default)
        {
            if (orders == null || orders.Count() == 0)
                throw new ArgumentException("Orders cant be null or with zero-elements");

            if (orders.Count() > 10)
                throw new ArgumentException("Exceed maximum order count(10)");

            symbol = symbol.ValidateSymbol();
            for (var i = 0; i < orders.Count(); i++)
            {
                var order = orders.ElementAt(i);
                var suffix = $"(Order: {(i + 1)} of {orders.Count()})";

                if (order.OrderId == null && string.IsNullOrEmpty(order.ClientOrderId))
                    throw new ArgumentException($"Either orderId or clientOrderId must be present. {suffix}");

                if (order.OrderId != null && !string.IsNullOrEmpty(order.ClientOrderId))
                    throw new ArgumentException($"Either orderId or clientOrderId must be present. {suffix}");

                if (!order.NewSize.HasValue && !order.NewPrice.HasValue)
                    throw new ArgumentException($"Must provide at least one of new_size or new_price. {suffix}");
            }

            var parameters = new Dictionary<string, object>
            {
                { "amend_data", orders },
            };

            return await SendRequest<OkexFuturesBatchPlacedOrder>(GetUrl(Endpoints_Futures_BatchModifyOrders, symbol), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// This is used to cancel an unfilled order.
        /// Rate limit: 60 requests per 2 seconds (Speed limit rules: 1) The speed limit is not accumulated between different contracts； 2) Api limit is separated by underlying. Different tenure of the same underlying share the limit； 3) The speed limit between the Coin Margined Futures and the USDT Margined Futures is not accumulated)
        /// Notes
        /// - Only one of order_id or client_oid parameters should be passed per request
        /// - The client_oid should be unique.No warning is sent when client_oid is not unique.
        /// - In case of multiple identical client_oid, only the latest entry will be returned.
        /// - If the order cannot be canceled because it has already filled or been canceled, the reason will be returned with the error message.
        /// - The response includes order_id, which does not confirm that the orders has been canceled successfully.Orders that are being filled cannot be canceled whereas orders that have not been filled could been canceled.
        /// </summary>
        /// <param name="symbol">Contract ID,e.g. BTC-USD-180309,BTC-USDT-191227</param>
        /// <param name="orderId">Either client_oid or order_id must be present. Order ID</param>
        /// <param name="clientOrderId">Either client_oid or order_id must be present. Client-supplied order ID that you can customize. It should be comprised of alpha-numeric characters with length 1 to 32. Both uppercase and lowercase are supported</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<OkexFuturesPlacedOrder> Futures_CancelOrder(string symbol, long? orderId = null, string? clientOrderId = null, CancellationToken ct = default) => Futures_CancelOrder_Async(symbol, orderId, clientOrderId, ct).Result;
        /// <summary>
        /// This is used to cancel an unfilled order.
        /// Rate limit: 60 requests per 2 seconds (Speed limit rules: 1) The speed limit is not accumulated between different contracts； 2) Api limit is separated by underlying. Different tenure of the same underlying share the limit； 3) The speed limit between the Coin Margined Futures and the USDT Margined Futures is not accumulated)
        /// Notes
        /// - Only one of order_id or client_oid parameters should be passed per request
        /// - The client_oid should be unique.No warning is sent when client_oid is not unique.
        /// - In case of multiple identical client_oid, only the latest entry will be returned.
        /// - If the order cannot be canceled because it has already filled or been canceled, the reason will be returned with the error message.
        /// - The response includes order_id, which does not confirm that the orders has been canceled successfully.Orders that are being filled cannot be canceled whereas orders that have not been filled could been canceled.
        /// </summary>
        /// <param name="symbol">Contract ID,e.g. BTC-USD-180309,BTC-USDT-191227</param>
        /// <param name="orderId">Either client_oid or order_id must be present. Order ID</param>
        /// <param name="clientOrderId">Either client_oid or order_id must be present. Client-supplied order ID that you can customize. It should be comprised of alpha-numeric characters with length 1 to 32. Both uppercase and lowercase are supported</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<OkexFuturesPlacedOrder>> Futures_CancelOrder_Async(string symbol, long? orderId = null, string? clientOrderId = null, CancellationToken ct = default)
        {
            symbol = symbol.ValidateSymbol();

            if (orderId == null && string.IsNullOrEmpty(clientOrderId))
                throw new ArgumentException("Either orderId or clientOrderId must be present.");

            if (orderId != null && !string.IsNullOrEmpty(clientOrderId))
                throw new ArgumentException("Either orderId or clientOrderId must be present.");

            return await SendRequest<OkexFuturesPlacedOrder>(GetUrl(Endpoints_Futures_CancelOrder.Replace("<instrument_id>", symbol), orderId.HasValue ? orderId.ToString() : clientOrderId!), HttpMethod.Post, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Cancel multiple open orders with order_id or client_oid，Maximum 10 orders can be cancelled at a time for each contract."
        /// Rate Limit: 30 requests per 2 seconds (Speed limit rules: 1) The speed limit is not accumulated between different contracts； 2) Api limit is separated by underlying. Different tenure of the same underlying share the limit ； 3) The speed limit between the Coin Margined Futures and the USDT Margined Futures is not accumulated)
        /// Notes:
        /// - For batch order cancellation, only one of order_id or client_oid parameters should be passed per request.Otherwise an error will be returned.
        /// - When using client_oid for batch order cancellation, up to 10 orders can be canceled per trading pair.You need to make sure the ID is unique.In case of multiple identical client_oid, only the latest entry will be returned.
        /// - Cancellations of orders are not guaranteed.After placing a cancel order you should confirm they are successfully canceled by requesting the "Get Order List" endpoint.
        /// </summary>
        /// <param name="symbol">The orders of the contract to be canceled e.g BTC-USD-180309,BTC-USDT-191227</param>
        /// <param name="orderIds">Either client_oid or order_id must be present. ID of the orders to be canceled</param>
        /// <param name="clientOrderIds">Either client_oid or order_id must be present. Client-supplied order ID that you can customize. It should be comprised of alpha-numeric characters with length 1 to 32. Both uppercase and lowercase are supported</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<OkexFuturesBatchOrders> Futures_BatchCancelOrders(string symbol, IEnumerable<string> orderIds, IEnumerable<string> clientOrderIds, CancellationToken ct = default) => Futures_BatchCancelOrders_Async(symbol, orderIds, clientOrderIds, ct).Result;
        /// <summary>
        /// Cancel multiple open orders with order_id or client_oid，Maximum 10 orders can be cancelled at a time for each contract."
        /// Rate Limit: 30 requests per 2 seconds (Speed limit rules: 1) The speed limit is not accumulated between different contracts； 2) Api limit is separated by underlying. Different tenure of the same underlying share the limit ； 3) The speed limit between the Coin Margined Futures and the USDT Margined Futures is not accumulated)
        /// Notes:
        /// - For batch order cancellation, only one of order_id or client_oid parameters should be passed per request.Otherwise an error will be returned.
        /// - When using client_oid for batch order cancellation, up to 10 orders can be canceled per trading pair.You need to make sure the ID is unique.In case of multiple identical client_oid, only the latest entry will be returned.
        /// - Cancellations of orders are not guaranteed.After placing a cancel order you should confirm they are successfully canceled by requesting the "Get Order List" endpoint.
        /// </summary>
        /// <param name="symbol">The orders of the contract to be canceled e.g BTC-USD-180309,BTC-USDT-191227</param>
        /// <param name="orderIds">Either client_oid or order_id must be present. ID of the orders to be canceled</param>
        /// <param name="clientOrderIds">Either client_oid or order_id must be present. Client-supplied order ID that you can customize. It should be comprised of alpha-numeric characters with length 1 to 32. Both uppercase and lowercase are supported</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<OkexFuturesBatchOrders>> Futures_BatchCancelOrders_Async(string symbol, IEnumerable<string> orderIds, IEnumerable<string> clientOrderIds, CancellationToken ct = default)
        {
            symbol = symbol.ValidateSymbol();

            if ((orderIds == null || orderIds.Count() == 0) && (clientOrderIds == null || clientOrderIds.Count() == 0))
                throw new ArgumentException("Either orderIds or clientOrderIds must be present.");

            if ((orderIds != null && orderIds.Count() > 0) && (clientOrderIds != null && clientOrderIds.Count() > 0))
                throw new ArgumentException("Either orderIds or clientOrderIds must be present.");

            var parameters = new Dictionary<string, object>();
            if (orderIds != null && orderIds.Count() > 0) parameters.Add("order_ids", orderIds);
            if (clientOrderIds != null && clientOrderIds.Count() > 0) parameters.Add("client_oids", clientOrderIds);

            return await SendRequest<OkexFuturesBatchOrders>(GetUrl(Endpoints_Futures_BatchCancelOrders, symbol), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// This retrieves the list of your orders from the last Three months .This request supports paging and is stored according to the order time in chronological order from latest to earliest.
        /// Rate limit: 10 requests per 2 seconds (Depending on the underlying speed limit)
        /// Notes
        /// - The closed position realized profit and loss does not include the fee.
        /// - status is the older version of state and both can be used interchangeably in the short term. It is recommended to switch to state.
        /// - The client_oid is optional and you can customize it using alpha-numeric characters with length 1 to 32. This parameter is used to identify your orders in the public orders feed.No warning is sent when client_oid is not unique. In case of multiple identical client_oid, only the latest entry will be returned.
        /// - If the order is not filled in the order life cycle, the record may be removed.
        /// - The state of unfilled orders may change during the time of endpoint request and response, depending on the market condition.
        /// </summary>
        /// <param name="symbol">Trading pair symbol BTC-USD-180213,BTC-USDT-191227</param>
        /// <param name="state">Order Status: -2 = Failed -1 = Canceled 0 = Open 1 = Partially Filled 2 = Fully Filled 3 = Submitting 4 = Canceling 6 = Incomplete (open + partially filled) 7 = Complete (canceled + fully filled)</param>
        /// <param name="limit">Number of results per request. The maximum is 100; the default is 100</param>
        /// <param name="before">Pagination of data to return records new than the requested order_id</param>
        /// <param name="after">Pagination of data to return records earlier than the requested order_id</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<OkexFuturesOrderList> Futures_GetAllOrders(string symbol, OkexFuturesOrderState state, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default) => Futures_GetAllOrders_Async(symbol, state, limit, before, after, ct).Result;
        /// <summary>
        /// This retrieves the list of your orders from the last Three months .This request supports paging and is stored according to the order time in chronological order from latest to earliest.
        /// Rate limit: 10 requests per 2 seconds (Depending on the underlying speed limit)
        /// Notes
        /// - The closed position realized profit and loss does not include the fee.
        /// - status is the older version of state and both can be used interchangeably in the short term. It is recommended to switch to state.
        /// - The client_oid is optional and you can customize it using alpha-numeric characters with length 1 to 32. This parameter is used to identify your orders in the public orders feed.No warning is sent when client_oid is not unique. In case of multiple identical client_oid, only the latest entry will be returned.
        /// - If the order is not filled in the order life cycle, the record may be removed.
        /// - The state of unfilled orders may change during the time of endpoint request and response, depending on the market condition.
        /// </summary>
        /// <param name="symbol">Trading pair symbol BTC-USD-180213,BTC-USDT-191227</param>
        /// <param name="state">Order Status: -2 = Failed -1 = Canceled 0 = Open 1 = Partially Filled 2 = Fully Filled 3 = Submitting 4 = Canceling 6 = Incomplete (open + partially filled) 7 = Complete (canceled + fully filled)</param>
        /// <param name="limit">Number of results per request. The maximum is 100; the default is 100</param>
        /// <param name="before">Pagination of data to return records new than the requested order_id</param>
        /// <param name="after">Pagination of data to return records earlier than the requested order_id</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<OkexFuturesOrderList>> Futures_GetAllOrders_Async(string symbol, OkexFuturesOrderState state, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default)
        {
            symbol = symbol.ValidateSymbol();
            limit.ValidateIntBetween(nameof(limit), 1, 100);

            var parameters = new Dictionary<string, object>
            {
                { "state", JsonConvert.SerializeObject(state, new FuturesOrderStateConverter(false)) },
                { "limit", limit.ToString(ci) },
            };
            parameters.AddOptionalParameter("before", before?.ToString(ci));
            parameters.AddOptionalParameter("after", after?.ToString(ci));

            return await SendRequest<OkexFuturesOrderList>(GetUrl(Endpoints_Futures_OrderList, symbol), HttpMethod.Get, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve order details by order ID. Unfilled orders will be kept in record for only two hours after it is canceled.
        /// Rate limit: 60 requests per 2 seconds (Depending on the underlying speed limit)
        /// Notes:
        /// - status is the older version ofstateand both can be used interchangeably in the short term.It is recommended to switch tostate`.
        /// - The client_oid is optional and you can customize it using alpha-numeric characters with length 1 to 32. This parameter is used to identify your orders in the public orders feed.No warning is sent when client_oid is not unique. In case of multiple identical client_oid, only the latest entry will be returned.
        /// - If the order is not filled in the order life cycle, the record may be removed.
        /// - Unfilled order status may change according to the market conditions.
        /// - Can get order information for nearly 3 months
        /// </summary>
        /// <param name="symbol">Contract ID,e.g.BTC-USD-180213 ,BTC-USDT-191227</param>
        /// <param name="orderId">Order ID Either client_oid or order_id must be present.</param>
        /// <param name="clientOrderId">Client-supplied order ID Either client_oid or order_id must be present.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<OkexFuturesOrder> Futures_GetOrderDetails(string symbol, long? orderId = null, string? clientOrderId = null, CancellationToken ct = default) => Futures_GetOrderDetails_Async(symbol, orderId, clientOrderId, ct).Result;
        /// <summary>
        /// Retrieve order details by order ID. Unfilled orders will be kept in record for only two hours after it is canceled.
        /// Rate limit: 60 requests per 2 seconds (Depending on the underlying speed limit)
        /// Notes:
        /// - status is the older version ofstateand both can be used interchangeably in the short term.It is recommended to switch tostate`.
        /// - The client_oid is optional and you can customize it using alpha-numeric characters with length 1 to 32. This parameter is used to identify your orders in the public orders feed.No warning is sent when client_oid is not unique. In case of multiple identical client_oid, only the latest entry will be returned.
        /// - If the order is not filled in the order life cycle, the record may be removed.
        /// - Unfilled order status may change according to the market conditions.
        /// - Can get order information for nearly 3 months
        /// </summary>
        /// <param name="symbol">Contract ID,e.g.BTC-USD-180213 ,BTC-USDT-191227</param>
        /// <param name="orderId">Order ID Either client_oid or order_id must be present.</param>
        /// <param name="clientOrderId">Client-supplied order ID Either client_oid or order_id must be present.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<OkexFuturesOrder>> Futures_GetOrderDetails_Async(string symbol, long? orderId = null, string? clientOrderId = null, CancellationToken ct = default)
        {
            symbol = symbol.ValidateSymbol();

            if (orderId == null && string.IsNullOrEmpty(clientOrderId))
                throw new ArgumentException("Either orderId or clientOrderId must be present.");

            if (orderId != null && !string.IsNullOrEmpty(clientOrderId))
                throw new ArgumentException("Either orderId or clientOrderId must be present.");

            return await SendRequest<OkexFuturesOrder>(GetUrl(Endpoints_Futures_OrderDetails.Replace("<instrument_id>", symbol), orderId.HasValue ? orderId.ToString() : clientOrderId!), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve recently filled transaction details. Pagination is supported and the response is sorted with most recent first in reverse chronological order. Data from the past 7 days can be retrieved.
        /// Rate limit: 10 requests per 2 seconds (Depending on the underlying speed limit)
        /// Notes:
        /// - Transaction Fees
        ///   If the value of the side is points_fee, the value of fee should be the amount settled by LP(Loyalty Points).
        /// - Liquidity
        ///   The exec_type specifies whether the order is maker or taker. ‘M’ stands for Maker and ‘T’ stands for Taker.
        /// - Pagination
        ///   The trade_id is listed in a descending order, from biggest to smallest.The first trade_idin this page can be found under OK-BEFORE, and the last one can be found under OK-AFTER.It would be easier to retrieve to other trade_id by referring to these two parameters.
        /// </summary>
        /// <param name="symbol">Contract ID, e.g., BTC-USD-180213 ,BTC-USDT-191227</param>
        /// <param name="orderId">Order ID, Complete transaction details for will be returned if the instrument_id is left blank</param>
        /// <param name="limit">Number of results per request. The maximum is 100; the default is 100</param>
        /// <param name="before">Pagination of data to return records newer than the requested trade_id</param>
        /// <param name="after">Pagination of data to return records earlier than the requested trade_id</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<IEnumerable<OkexFuturesTransaction>> Futures_GetTransactionDetails(string symbol, long? orderId = null, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default) => Futures_GetTransactionDetails_Async(symbol, orderId, limit, before, after, ct).Result;
        /// <summary>
        /// Retrieve recently filled transaction details. Pagination is supported and the response is sorted with most recent first in reverse chronological order. Data from the past 7 days can be retrieved.
        /// Rate limit: 10 requests per 2 seconds (Depending on the underlying speed limit)
        /// Notes:
        /// - Transaction Fees
        ///   If the value of the side is points_fee, the value of fee should be the amount settled by LP(Loyalty Points).
        /// - Liquidity
        ///   The exec_type specifies whether the order is maker or taker. ‘M’ stands for Maker and ‘T’ stands for Taker.
        /// - Pagination
        ///   The trade_id is listed in a descending order, from biggest to smallest.The first trade_idin this page can be found under OK-BEFORE, and the last one can be found under OK-AFTER.It would be easier to retrieve to other trade_id by referring to these two parameters.
        /// </summary>
        /// <param name="symbol">Contract ID, e.g., BTC-USD-180213 ,BTC-USDT-191227</param>
        /// <param name="orderId">Order ID, Complete transaction details for will be returned if the instrument_id is left blank</param>
        /// <param name="limit">Number of results per request. The maximum is 100; the default is 100</param>
        /// <param name="before">Pagination of data to return records newer than the requested trade_id</param>
        /// <param name="after">Pagination of data to return records earlier than the requested trade_id</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<IEnumerable<OkexFuturesTransaction>>> Futures_GetTransactionDetails_Async(string symbol, long? orderId = null, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default)
        {
            symbol = symbol.ValidateSymbol();
            limit.ValidateIntBetween(nameof(limit), 1, 100);

            var parameters = new Dictionary<string, object>
            {
                { "instrument_id", symbol },
                { "limit", limit.ToString(ci) },
            };
            parameters.AddOptionalParameter("order_id", orderId?.ToString(ci));
            parameters.AddOptionalParameter("before", before?.ToString(ci));
            parameters.AddOptionalParameter("after", after?.ToString(ci));

            return await SendRequest<IEnumerable<OkexFuturesTransaction>>(GetUrl(Endpoints_Futures_TransactionDetails), HttpMethod.Get, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// This is used to set account mode of a contract. The account mode cannot be changed if there is open interest or open order.
        /// Rate Limit: 5 requests per 2 seconds
        /// </summary>
        /// <param name="symbol">Underlying index，eg：BTC-USD BTC-USDT</param>
        /// <param name="margin_mode">Margin mode: crossed / fixed</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<OkexFuturesAccountMode> Futures_SetAccountMode(string symbol, OkexFuturesMarginMode margin_mode, CancellationToken ct = default) => Futures_SetAccountMode_Async(symbol, margin_mode, ct).Result;
        /// <summary>
        /// This is used to set account mode of a contract. The account mode cannot be changed if there is open interest or open order.
        /// Rate Limit: 5 requests per 2 seconds
        /// </summary>
        /// <param name="symbol">Underlying index，eg：BTC-USD BTC-USDT</param>
        /// <param name="margin_mode">Margin mode: crossed / fixed</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<OkexFuturesAccountMode>> Futures_SetAccountMode_Async(string symbol, OkexFuturesMarginMode margin_mode, CancellationToken ct = default)
        {
            symbol = symbol.ValidateSymbol();
            var parameters = new Dictionary<string, object>
            {
                { "underlying", symbol },
                { "margin_mode", JsonConvert.SerializeObject(margin_mode, new FuturesMarginModeConverter(false)) },
            };

            return await SendRequest<OkexFuturesAccountMode>(GetUrl(Endpoints_Futures_SetAccountMode), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Obtain the transaction fee rate corresponding to your current account transaction level. The sub-account rate under the parent account is the same as the parent account. Update every day at 0am
        /// Rate limit: 20 requests per 2 seconds
        /// </summary>
        /// <param name="symbol">Underlying index，eg：BTC-USD ；Choose and enter one parameter between category and underlying</param>
        /// <param name="category">Fee Schedule Tier: Tier 1; Tier 2; Choose and enter one parameter between category and underlying</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<IEnumerable<OkexFuturesTradeFee>> Futures_GetTradeFeeRates(string? symbol = null, int? category = null, CancellationToken ct = default) => Futures_GetTradeFeeRates_Async(symbol, category, ct).Result;
        /// <summary>
        /// Obtain the transaction fee rate corresponding to your current account transaction level. The sub-account rate under the parent account is the same as the parent account. Update every day at 0am
        /// Rate limit: 20 requests per 2 seconds
        /// </summary>
        /// <param name="symbol">Underlying index，eg：BTC-USD ；Choose and enter one parameter between category and underlying</param>
        /// <param name="category">Fee Schedule Tier: Tier 1; Tier 2; Choose and enter one parameter between category and underlying</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<IEnumerable<OkexFuturesTradeFee>>> Futures_GetTradeFeeRates_Async(string? symbol = null, int? category = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("underlying", symbol);
            parameters.AddOptionalParameter("category", category?.ToString(ci));

            return await SendRequest<IEnumerable<OkexFuturesTradeFee>>(GetUrl(Endpoints_Futures_TradeFee), HttpMethod.Get, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Close all positions via market order. This endpoint is only available when user's position is less or equal than 999 contracts size for BTC, otherwise it will return error. Similarly the position should be less or equal than 9,999 contracts size for other assets.
        /// Rate Limit: 2 requests per 2 seconds (Depending on the underlying speed limit)
        /// </summary>
        /// <param name="symbol">Contract ID, e.g.BTC-USD-180213,BTC-USDT-191227</param>
        /// <param name="direction">Side (long or short)</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<OkexFuturesDirectionResponse> Futures_MarketCloseAll(string symbol, OkexFuturesDirection direction, CancellationToken ct = default) => Futures_MarketCloseAll_Async(symbol, direction, ct).Result;
        /// <summary>
        /// Close all positions via market order. This endpoint is only available when user's position is less or equal than 999 contracts size for BTC, otherwise it will return error. Similarly the position should be less or equal than 9,999 contracts size for other assets.
        /// Rate Limit: 2 requests per 2 seconds (Depending on the underlying speed limit)
        /// </summary>
        /// <param name="symbol">Contract ID, e.g.BTC-USD-180213,BTC-USDT-191227</param>
        /// <param name="direction">Side (long or short)</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<OkexFuturesDirectionResponse>> Futures_MarketCloseAll_Async(string symbol, OkexFuturesDirection direction, CancellationToken ct = default)
        {
            symbol = symbol.ValidateSymbol();
            var parameters = new Dictionary<string, object>
            {
                { "instrument_id", symbol },
                { "direction", JsonConvert.SerializeObject(direction, new FuturesDirectionConverter(false)) },
            };

            return await base.SendRequest<OkexFuturesDirectionResponse>(GetUrl(Endpoints_Futures_MarketCloseAll), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Cancel all the outstanding orders which type equal 3 (close long) or 4 (close short).
        /// Rate Limit: 5 requests per 2 seconds
        /// </summary>
        /// <param name="symbol">Contract ID, e.g.BTC-USD-180213,BTC-USDT-191227</param>
        /// <param name="direction">side (long or short)</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<OkexFuturesDirectionResponse> Futures_CancelAll(string symbol, OkexFuturesDirection direction, CancellationToken ct = default) => Futures_CancelAll_Async(symbol, direction, ct).Result;
        /// <summary>
        /// Cancel all the outstanding orders which type equal 3 (close long) or 4 (close short).
        /// Rate Limit: 5 requests per 2 seconds
        /// </summary>
        /// <param name="symbol">Contract ID, e.g.BTC-USD-180213,BTC-USDT-191227</param>
        /// <param name="direction">side (long or short)</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<OkexFuturesDirectionResponse>> Futures_CancelAll_Async(string symbol, OkexFuturesDirection direction, CancellationToken ct = default)
        {
            symbol = symbol.ValidateSymbol();
            var parameters = new Dictionary<string, object>
            {
                { "instrument_id", symbol },
                { "direction", JsonConvert.SerializeObject(direction, new FuturesDirectionConverter(false)) },
            };

            return await base.SendRequest<OkexFuturesDirectionResponse>(GetUrl(Endpoints_Futures_CancelAll), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Get the amount of futures with hold.
        /// Limit: 5 times / 2s (Depending on the underlying speed limit)
        /// </summary>
        /// <param name="symbol">Contract ID,e.g. BTC-USD-190329,BTC-USDT-191227</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<OkexFuturesHoldAmount> Futures_GetHoldAmount(string symbol, CancellationToken ct = default) => Futures_GetHoldAmount_Async(symbol, ct).Result;
        /// <summary>
        /// Get the amount of futures with hold.
        /// Limit: 5 times / 2s (Depending on the underlying speed limit)
        /// </summary>
        /// <param name="symbol">Contract ID,e.g. BTC-USD-190329,BTC-USDT-191227</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<OkexFuturesHoldAmount>> Futures_GetHoldAmount_Async(string symbol, CancellationToken ct = default)
        {
            symbol = symbol.ValidateSymbol();

            return await base.SendRequest<OkexFuturesHoldAmount>(GetUrl(Endpoints_Futures_HoldAmount, symbol), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Includes trigger order, trail order, iceberg order and time-weighted average price .
        /// Rate limit：40 times40 requests per 2 seconds (Depending on the underlying speed limit)
        /// </summary>
        /// <param name="symbol">Trading pair symbol</param>
        /// <param name="type">1. open long; 2. open short; 3. close long; 4. close short;</param>
        /// <param name="order_type">1. trigger order； 2. trail order; 3. iceberg order; 4. time-weighted average price 5. stop order;</param>
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
        /// <param name="twap_price_limit">Price limit must be between 0 and 1,000,000, incl, 1,000,000</param>
        /// <param name="twap_time_interval">Time interval must be between 5 and 120, incl. both numbers</param>
        /// <param name="tp_trigger_type">1:limit 2:market；TP trigger type，The default is limit price；</param>
        /// <param name="tp_trigger_price">TP trigger price must be between 0 and 1,000,000</param>
        /// <param name="tp_price">TP order price must be between 0 and 1,000,000</param>
        /// <param name="sl_trigger_type">1:limit 2:market；TP trigger type，The default is limit price；When it is the market price, the tp_price does not need to be filled;</param>
        /// <param name="sl_trigger_price">SL trigger price must be between 0 and 1,000,000</param>
        /// <param name="sl_price">SL order price must be between 0 and 1,000,000</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<OkexFuturesAlgoPlacedOrder> Futures_AlgoPlaceOrder(
            /* General Parameters */
            string symbol,
            OkexFuturesOrderType type,
            OkexAlgoOrderType order_type,
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
            decimal? twap_price_limit = null,
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
            => Futures_AlgoPlaceOrder_Async(
            symbol,
            type,
            order_type,
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
            twap_price_limit,
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
        /// Includes trigger order, trail order, iceberg order and time-weighted average price .
        /// Rate limit：40 times40 requests per 2 seconds (Depending on the underlying speed limit)
        /// </summary>
        /// <param name="symbol">Trading pair symbol</param>
        /// <param name="type">1. open long; 2. open short; 3. close long; 4. close short;</param>
        /// <param name="order_type">1. trigger order； 2. trail order; 3. iceberg order; 4. time-weighted average price 5. stop order;</param>
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
        /// <param name="twap_price_limit">Price limit must be between 0 and 1,000,000, incl, 1,000,000</param>
        /// <param name="twap_time_interval">Time interval must be between 5 and 120, incl. both numbers</param>
        /// <param name="tp_trigger_type">1:limit 2:market；TP trigger type，The default is limit price；</param>
        /// <param name="tp_trigger_price">TP trigger price must be between 0 and 1,000,000</param>
        /// <param name="tp_price">TP order price must be between 0 and 1,000,000</param>
        /// <param name="sl_trigger_type">1:limit 2:market；TP trigger type，The default is limit price；When it is the market price, the tp_price does not need to be filled;</param>
        /// <param name="sl_trigger_price">SL trigger price must be between 0 and 1,000,000</param>
        /// <param name="sl_price">SL order price must be between 0 and 1,000,000</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<OkexFuturesAlgoPlacedOrder>> Futures_AlgoPlaceOrder_Async(
            /* General Parameters */
            string symbol,
            OkexFuturesOrderType type,
            OkexAlgoOrderType order_type,
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
            decimal? twap_price_limit = null,
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
        {
            symbol = symbol.ValidateSymbol();

            var parameters = new Dictionary<string, object>
            {
                { "instrument_id", symbol },
                { "type", JsonConvert.SerializeObject(order_type, new FuturesOrderTypeConverter(false)) },
                { "order_type", JsonConvert.SerializeObject(order_type, new AlgoOrderTypeConverter(false)) },
                { "size", size.ToString(ci) },
            };

            if (order_type == OkexAlgoOrderType.TriggerOrder)
            {
                if (trigger_price == null) throw new ArgumentException("trigger_price is mandatory for Trigger Order");
                if (trigger_algo_price == null) throw new ArgumentException("trigger_algo_price is mandatory for Trigger Order");
                // if(trigger_algo_type == null) throw new ArgumentException("trigger_algo_type is mandatory for Trigger Order");

                parameters.AddParameter("trigger_price", trigger_price.Value.ToString(ci));
                parameters.AddParameter("algo_price", trigger_algo_price.Value.ToString(ci));
                parameters.AddOptionalParameter("algo_type", JsonConvert.SerializeObject(trigger_algo_type, new AlgoPriceTypeConverter(false)));
            }

            else if (order_type == OkexAlgoOrderType.TrailOrder)
            {
                if (trail_callback_rate == null) throw new ArgumentException("trail_callback_rate is mandatory for Trail Order");
                if (trail_trigger_price == null) throw new ArgumentException("trail_trigger_price is mandatory for Trail Order");

                parameters.AddParameter("callback_rate", trail_callback_rate.Value.ToString(ci));
                parameters.AddParameter("trigger_price", trail_trigger_price.Value.ToString(ci));
            }

            else if (order_type == OkexAlgoOrderType.IcebergOrder)
            {
                if (iceberg_algo_variance == null) throw new ArgumentException("iceberg_algo_variance is mandatory for Iceberg Order");
                if (iceberg_avg_amount == null) throw new ArgumentException("iceberg_avg_amount is mandatory for Iceberg Order");
                if (iceberg_limit_price == null) throw new ArgumentException("iceberg_limit_price is mandatory for Iceberg Order");

                parameters.AddParameter("algo_variance", iceberg_algo_variance.Value.ToString(ci));
                parameters.AddParameter("avg_amount", iceberg_avg_amount.Value.ToString(ci));
                parameters.AddParameter("limit_price", iceberg_limit_price.Value.ToString(ci));
            }

            else if (order_type == OkexAlgoOrderType.TWAP)
            {
                if (twap_sweep_range == null) throw new ArgumentException("twap_sweep_range is mandatory for TWAP Order");
                if (twap_sweep_ratio == null) throw new ArgumentException("twap_sweep_ratio is mandatory for TWAP Order");
                if (twap_single_limit == null) throw new ArgumentException("twap_single_limit is mandatory for TWAP Order");
                if (twap_price_limit == null) throw new ArgumentException("twap_price_limit is mandatory for TWAP Order");
                if (twap_time_interval == null) throw new ArgumentException("twap_time_interval is mandatory for TWAP Order");

                parameters.AddParameter("sweep_range", twap_sweep_range.Value.ToString(ci));
                parameters.AddParameter("sweep_ratio", twap_sweep_ratio.Value.ToString(ci));
                parameters.AddParameter("single_limit", twap_single_limit.Value.ToString(ci));
                parameters.AddParameter("price_limit", twap_price_limit.Value.ToString(ci));
                parameters.AddParameter("time_interval", twap_time_interval.Value.ToString(ci));
            }

            else if (order_type == OkexAlgoOrderType.StopOrder)
            {
                //if(tp_trigger_type == null) throw new ArgumentException("tp_trigger_type is mandatory for Stop Order");
                //if(tp_trigger_price == null) throw new ArgumentException("tp_trigger_price is mandatory for Stop Order");
                //if(tp_price == null) throw new ArgumentException("tp_price is mandatory for Stop Order");
                //if(sl_trigger_type == null) throw new ArgumentException("sl_trigger_type is mandatory for Stop Order");
                //if(sl_trigger_price == null) throw new ArgumentException("sl_trigger_price is mandatory for Stop Order");
                //if(sl_price == null) throw new ArgumentException("sl_price is mandatory for Stop Order");

                if (tp_trigger_type != null) parameters.AddOptionalParameter("tp_trigger_type", JsonConvert.SerializeObject(tp_trigger_type, new AlgoPriceTypeConverter(false)));
                parameters.AddOptionalParameter("tp_trigger_price", tp_trigger_price?.ToString(ci));
                parameters.AddOptionalParameter("tp_price", tp_price?.ToString(ci));
                if (sl_trigger_type != null) parameters.AddOptionalParameter("sl_trigger_type", JsonConvert.SerializeObject(sl_trigger_type, new AlgoPriceTypeConverter(false)));
                parameters.AddOptionalParameter("sl_trigger_price", sl_trigger_price?.ToString(ci));
                parameters.AddOptionalParameter("sl_price", sl_price?.ToString(ci));
            }

            return await SendRequest<OkexFuturesAlgoPlacedOrder>(GetUrl(Endpoints_Futures_AlgoPlaceOrder), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// If user use "algo_id" to cancel unfulfilled orders, they can cancel a maximum of 6 iceberg/TWAP or 10 trigger/trail orders at the same time.
        /// Rate limit：20 requests per 2 seconds
        /// </summary>
        /// <param name="symbol">Cancel specific types of contract e.g EOS-USD-191227,EOS-USDT-191227</param>
        /// <param name="type">1. trigger order; 2. trail order; 3. iceberg order; 4. time-weighted average price ; 5. stop order</param>
        /// <param name="algo_ids">Cancel specific order ID</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns>
        /// Return Parameter: Return parameter is the order ID of canceled orders. This does not mean that the orders are successfully canceled. Orders that are pending cannot be canceled, only unfulfilled orders can be canceled.
        /// Description: This does not guarantee orders are canceled successfully. Users are advised to request the order list to confirm after using the cancelation endpoint.
        /// </returns>
        public virtual WebCallResult<OkexFuturesAlgoCancelledOrder> Futures_AlgoCancelOrder(string symbol, OkexAlgoOrderType type, IEnumerable<string> algo_ids, CancellationToken ct = default) => Futures_AlgoCancelOrder_Async(symbol, type, algo_ids, ct).Result;
        /// <summary>
        /// If user use "algo_id" to cancel unfulfilled orders, they can cancel a maximum of 6 iceberg/TWAP or 10 trigger/trail orders at the same time.
        /// Rate limit：20 requests per 2 seconds
        /// </summary>
        /// <param name="symbol">Cancel specific types of contract e.g EOS-USD-191227,EOS-USDT-191227</param>
        /// <param name="type">1. trigger order; 2. trail order; 3. iceberg order; 4. time-weighted average price ; 5. stop order</param>
        /// <param name="algo_ids">Cancel specific order ID</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns>
        /// Return Parameter: Return parameter is the order ID of canceled orders. This does not mean that the orders are successfully canceled. Orders that are pending cannot be canceled, only unfulfilled orders can be canceled.
        /// Description: This does not guarantee orders are canceled successfully. Users are advised to request the order list to confirm after using the cancelation endpoint.
        /// </returns>
        public virtual async Task<WebCallResult<OkexFuturesAlgoCancelledOrder>> Futures_AlgoCancelOrder_Async(string symbol, OkexAlgoOrderType type, IEnumerable<string> algo_ids, CancellationToken ct = default)
        {
            symbol = symbol.ValidateSymbol();

            if (algo_ids == null && algo_ids.Count() == 0)
                throw new ArgumentException("algo_ids is mandatory.");

            var parameters = new Dictionary<string, object>
            {
                { "instrument_id", symbol },
                { "algo_ids", algo_ids! },
                { "order_type", JsonConvert.SerializeObject(type, new AlgoOrderTypeConverter(false)) },
            };

            return await SendRequest<OkexFuturesAlgoCancelledOrder>(GetUrl(Endpoints_Futures_AlgoCancelOrder), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
        }

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8604 // Possible null reference argument.
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
        /// <returns>Symbol grouped algo orders list. Dictionary&lt;string: symbol, IEnumerable&lt;OkexFuturesAlgoOrder&gt;: algo orders&gt;</returns>
        /// <returns></returns>
        public virtual WebCallResult<IEnumerable<OkexFuturesAlgoOrder>> Futures_AlgoGetOrders(string symbol, OkexAlgoOrderType type, OkexAlgoStatus? status = null, IEnumerable<string> algo_ids = null, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default) => Futures_AlgoGetOrders_Async(symbol, type, status, algo_ids, limit, before, after, ct).Result;
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
        /// <returns>Symbol grouped algo orders list. Dictionary&lt;string: symbol, IEnumerable&lt;OkexFuturesAlgoOrder&gt;: algo orders&gt;</returns>
        /// <returns></returns>
        public virtual async Task<WebCallResult<IEnumerable<OkexFuturesAlgoOrder>>> Futures_AlgoGetOrders_Async(string symbol, OkexAlgoOrderType type, OkexAlgoStatus? status = null, IEnumerable<string> algo_ids = null, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default)
        {
            symbol = symbol.ValidateSymbol();
            limit.ValidateIntBetween(nameof(limit), 1, 100);

            if (status == null && (algo_ids == null || algo_ids.Count() == 0))
                throw new ArgumentException("status and algo_ids are mandatory, select either one");

            var parameters = new Dictionary<string, object>
            {
                { "instrument_id", symbol },
                { "order_type", JsonConvert.SerializeObject(type, new AlgoOrderTypeConverter(false)) },
                { "limit", limit.ToString(ci) },
            };
            parameters.AddOptionalParameter("status", JsonConvert.SerializeObject(status, new AlgoStatusConverter(false)));
            parameters.AddOptionalParameter("algo_ids", algo_ids);
            parameters.AddOptionalParameter("before", before?.ToString(ci));
            parameters.AddOptionalParameter("after", after?.ToString(ci));

            return await SendRequest<IEnumerable<OkexFuturesAlgoOrder>>(GetUrl(Endpoints_Futures_AlgoOrderList), HttpMethod.Get, ct, parameters, signed: true).ConfigureAwait(false);
        }
#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

        /// <summary>
        /// Increase / decrease margin of the fixed mode.
        /// Rate Limit: 5 requests per 2 seconds (Depending on the underlying speed limit)
        /// </summary>
        /// <param name="symbol">Contract ID, e.g. BTC-USD-180309,BTC-USDT-191227</param>
        /// <param name="direction">opening side (long or short)</param>
        /// <param name="action">1：increase 2：decrease</param>
        /// <param name="amount">Amount to be increase or decrease</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<OkexFuturesMarginActionResponse> Futures_IncreaseDecreaseMargin(string symbol, OkexFuturesDirection direction, OkexFuturesMarginAction action, decimal amount, CancellationToken ct = default) => Futures_IncreaseDecreaseMargin_Async(symbol, direction, action, amount, ct).Result;
        /// <summary>
        /// Increase / decrease margin of the fixed mode.
        /// Rate Limit: 5 requests per 2 seconds (Depending on the underlying speed limit)
        /// </summary>
        /// <param name="symbol">Contract ID, e.g. BTC-USD-180309,BTC-USDT-191227</param>
        /// <param name="direction">opening side (long or short)</param>
        /// <param name="action">1：increase 2：decrease</param>
        /// <param name="amount">Amount to be increase or decrease</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<OkexFuturesMarginActionResponse>> Futures_IncreaseDecreaseMargin_Async(string symbol, OkexFuturesDirection direction, OkexFuturesMarginAction action, decimal amount, CancellationToken ct = default)
        {
            symbol = symbol.ValidateSymbol();
            var parameters = new Dictionary<string, object>
            {
                { "instrument_id", symbol },
                { "direction", JsonConvert.SerializeObject(direction, new FuturesDirectionConverter(false)) },
                { "type", JsonConvert.SerializeObject(action, new FuturesMarginActionConverter(false)) },
                { "amount", amount.ToString(ci) },
            };

            return await base.SendRequest<OkexFuturesMarginActionResponse>(GetUrl(Endpoints_Futures_IncreaseDecreaseMargin), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Set up automatically increase margin for fixed margin mode
        /// Rate limit: 5 requests per 2 seconds (Depending on the underlying speed limit)
        /// </summary>
        /// <param name="symbol">Underlying index，eg：BTC-USD BTC-USDT</param>
        /// <param name="status">On / off automatically increases margin: 1, on 2, off</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<OkexFuturesAutoMarginResponse> Futures_AutoMargin(string symbol, OkexFuturesAutoMargin status, CancellationToken ct = default) => Futures_AutoMargin_Async(symbol, status, ct).Result;
        /// <summary>
        /// Set up automatically increase margin for fixed margin mode
        /// Rate limit: 5 requests per 2 seconds (Depending on the underlying speed limit)
        /// </summary>
        /// <param name="symbol">Underlying index，eg：BTC-USD BTC-USDT</param>
        /// <param name="status">On / off automatically increases margin: 1, on 2, off</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<OkexFuturesAutoMarginResponse>> Futures_AutoMargin_Async(string symbol, OkexFuturesAutoMargin status, CancellationToken ct = default)
        {
            symbol = symbol.ValidateSymbol();
            var parameters = new Dictionary<string, object>
            {
                { "underlying", symbol },
                { "type", JsonConvert.SerializeObject(status, new FuturesAutoMarginConverter(false)) },
            };

            return await base.SendRequest<OkexFuturesAutoMarginResponse>(GetUrl(Endpoints_Futures_SetMarginAutomatically), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
        }

        #endregion

        #region Public Unsigned Endpoints

        /// <summary>
        /// Get market data. This endpoint provides the snapshots of market data and can be used without verifications.
        /// Rate Limit: 20 requests per 2 seconds (Based on IP speed limit)
        /// Notes
        /// - The tick size is the smallest unit of price.The order price must be a multiple of the tick size.If the tick size is 0.01, entering a price of 0.022 will be adjusted to 0.02 instead.
        /// </summary>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<IEnumerable<OkexFuturesContract>> Futures_GetTradingContracts(CancellationToken ct = default) => Futures_GetTradingContracts_Async(ct).Result;
        /// <summary>
        /// Get market data. This endpoint provides the snapshots of market data and can be used without verifications.
        /// Rate Limit: 20 requests per 2 seconds (Based on IP speed limit)
        /// Notes
        /// - The tick size is the smallest unit of price.The order price must be a multiple of the tick size.If the tick size is 0.01, entering a price of 0.022 will be adjusted to 0.02 instead.
        /// </summary>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<IEnumerable<OkexFuturesContract>>> Futures_GetTradingContracts_Async(CancellationToken ct = default)
        {
            return await SendRequest<IEnumerable<OkexFuturesContract>>(GetUrl(Endpoints_Futures_TradingContracts), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve a trading pair's order book. Pagination is not supported here; the entire orderbook will be returned per request. This is publicly accessible without account authentication. WebSocket is recommended here.
        /// Rate limit: 20 requests per 2 seconds (Depending on the underlying speed limit)
        /// Notes:
        /// - Aggregation of the order book means that orders within a certain price range is combined and considered as one order cluster.
        /// - When size is not passed in the parameters, one entry is returned; when size is 0, no entry is returned.The maximum size is 200. When requesting more than 200 entries, at most 200 entries are returned.
        /// </summary>
        /// <param name="symbol">Contract ID,e.g. BTC-USD-180213,BTC-USDT-191227</param>
        /// <param name="size">Number of results per request. Maximum 200</param>
        /// <param name="depth">Aggregation of the order book. e.g . 0.1, 0.001</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<OkexFuturesOrderBook> Futures_GetOrderBook(string symbol, int? size = null, decimal? depth = null, CancellationToken ct = default) => Futures_GetOrderBook_Async(symbol, size, depth, ct).Result;
        /// <summary>
        /// Retrieve a trading pair's order book. Pagination is not supported here; the entire orderbook will be returned per request. This is publicly accessible without account authentication. WebSocket is recommended here.
        /// Rate limit: 20 requests per 2 seconds (Depending on the underlying speed limit)
        /// Notes:
        /// - Aggregation of the order book means that orders within a certain price range is combined and considered as one order cluster.
        /// - When size is not passed in the parameters, one entry is returned; when size is 0, no entry is returned.The maximum size is 200. When requesting more than 200 entries, at most 200 entries are returned.
        /// </summary>
        /// <param name="symbol">Contract ID,e.g. BTC-USD-180213,BTC-USDT-191227</param>
        /// <param name="size">Number of results per request. Maximum 200</param>
        /// <param name="depth">Aggregation of the order book. e.g . 0.1, 0.001</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<OkexFuturesOrderBook>> Futures_GetOrderBook_Async(string symbol, int? size = null, decimal? depth = null, CancellationToken ct = default)
        {
            symbol = symbol.ValidateSymbol();
            size?.ValidateIntBetween(nameof(size), 1, 200);

            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("size", size?.ToString(ci));
            parameters.AddOptionalParameter("depth", depth?.ToString(ci));

            return await SendRequest<OkexFuturesOrderBook>(GetUrl(Endpoints_Futures_OrderBook, symbol), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve the latest price snapshot, best bid/ask price, and trading volume in the last 24 hours for all trading pairs. This is publicly accessible without account authentication.
        /// Rate limit: 20 requests per 2 seconds (Based on IP speed limit)
        /// </summary>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<IEnumerable<OkexFuturesTicker>> Futures_GetAllTickers(CancellationToken ct = default) => Futures_GetAllTickers_Async(ct).Result;
        /// <summary>
        /// Retrieve the latest price snapshot, best bid/ask price, and trading volume in the last 24 hours for all trading pairs. This is publicly accessible without account authentication.
        /// Rate limit: 20 requests per 2 seconds (Based on IP speed limit)
        /// </summary>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<IEnumerable<OkexFuturesTicker>>> Futures_GetAllTickers_Async(CancellationToken ct = default)
        {
            return await SendRequest<IEnumerable<OkexFuturesTicker>>(GetUrl(Endpoints_Futures_TradingContractsTicker), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve the latest price snapshot, best bid/ask price, and trading volume in the last 24 hours for a particular trading pair. This is publicly accessible without account authentication.
        /// Rate limit: 20 requests per 2 seconds (Depending on the underlying speed limit)
        /// Notes:
        /// - The parameters for highest price, lowest price and trading volume are all computed based on the data in the last 24 hours.
        /// </summary>
        /// <param name="symbol">Contract ID,e.g. BTC-USD-180213,BTC-USDT-191227</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<OkexFuturesTicker> Futures_GetSymbolTicker(string symbol, CancellationToken ct = default) => Futures_GetSymbolTicker_Async(symbol, ct).Result;
        /// <summary>
        /// Retrieve the latest price snapshot, best bid/ask price, and trading volume in the last 24 hours for a particular trading pair. This is publicly accessible without account authentication.
        /// Rate limit: 20 requests per 2 seconds (Depending on the underlying speed limit)
        /// Notes:
        /// - The parameters for highest price, lowest price and trading volume are all computed based on the data in the last 24 hours.
        /// </summary>
        /// <param name="symbol">Contract ID,e.g. BTC-USD-180213,BTC-USDT-191227</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<OkexFuturesTicker>> Futures_GetSymbolTicker_Async(string symbol, CancellationToken ct = default)
        {
            symbol = symbol.ValidateSymbol();
            return await SendRequest<OkexFuturesTicker>(GetUrl(Endpoints_Futures_TradingContractsTickerOfSymbol, symbol), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <summary>
        /// Get the recent 300 transactions of all contracts. Pagination is not supported here. The entire book will be returned in one request. WebSocket is recommended here.
        /// Rate limit: 20 requests per 2 seconds (Depending on the underlying speed limit)
        /// Notes
        /// - The side indicates the side of the order that is filled by the taker.The "taker" means actively taking the order on the order book.The buy indicates the taker is taking liquidity from the order book, so the price rises as a result, whereas the sell indicates the price falls as a result.
        /// - The trade_id is the ID referring to the filled order; it is generated incrementally and could be incomplete in some cases.
        /// </summary>
        /// <param name="symbol">Contract ID,e.g. BTC-USD-180213,BTC-USDT-191227</param>
        /// <param name="limit">Number of results per request. The maximum is 100; the default is 100</param>
        /// <param name="before">Pagination of data to return records newer than the requested trade_id</param>
        /// <param name="after">Pagination of data to return records earlier than the requested trade_id</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<IEnumerable<OkexFuturesTrade>> Futures_GetTrades(string symbol, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default) => Futures_GetTrades_Async(symbol, limit, before, after, ct).Result;
        /// <summary>
        /// Get the recent 300 transactions of all contracts. Pagination is not supported here. The entire book will be returned in one request. WebSocket is recommended here.
        /// Rate limit: 20 requests per 2 seconds (Depending on the underlying speed limit)
        /// Notes
        /// - The side indicates the side of the order that is filled by the taker.The "taker" means actively taking the order on the order book.The buy indicates the taker is taking liquidity from the order book, so the price rises as a result, whereas the sell indicates the price falls as a result.
        /// - The trade_id is the ID referring to the filled order; it is generated incrementally and could be incomplete in some cases.
        /// </summary>
        /// <param name="symbol">Contract ID,e.g. BTC-USD-180213,BTC-USDT-191227</param>
        /// <param name="limit">Number of results per request. The maximum is 100; the default is 100</param>
        /// <param name="before">Pagination of data to return records newer than the requested trade_id</param>
        /// <param name="after">Pagination of data to return records earlier than the requested trade_id</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<IEnumerable<OkexFuturesTrade>>> Futures_GetTrades_Async(string symbol, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default)
        {
            symbol = symbol.ValidateSymbol();
            limit.ValidateIntBetween(nameof(limit), 1, 100);

            var parameters = new Dictionary<string, object>
            {
                { "limit", limit.ToString(ci) },
            };
            parameters.AddOptionalParameter("before", before?.ToString(ci));
            parameters.AddOptionalParameter("after", after?.ToString(ci));

            return await SendRequest<IEnumerable<OkexFuturesTrade>>(GetUrl(Endpoints_Futures_Trades, symbol), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve the candlestick charts of the trading pairs. This API can retrieve the latest 1,440 entries of data. Charts are returned in groups based on requested granularity. Maximum 2,880 recent Strings of chart data can be retrieved.
        /// Rate limit: 20 requests per 2 seconds (Depending on the underlying speed limit)
        /// Notes:
        /// - Timestamp must be in ISO 8601 format, otherwise an error be returned.
        /// - When the next-week contract is delivered: the old next-week contract is matched with the new this-week contract, the old this-week contract is matched with the new next-week contract, and the quarterly-contract is unchanged;
        /// - When quarter contract delivery: the old this-week contract is matched with the new quarter contract, the old quarter contract is matched with the new next-week contract, and the old next-week contract is matched with the new this-week contract;
        /// - Both parameters will be ignored if either one of start or end are not provided.The last 300 records of data will be returned if the time range is not specified in the request.
        /// - The granularity field must be one of the following values: [60, 180, 300, 900, 1800, 3600, 7200, 14400, 21600, 43200, 86400, 604800, 2678400, 8035200, 16070400, 31536000]. Otherwise, your request will be rejected.These values correspond to timeslices representing [1 minute, 3 minutes, 5 minutes, 15 minutes, 30 minutes, 1 hour, 2 hours, 4 hours, 6 hours, 12 hours, 1 day, 1 week,1 month ,3 months, 6 months and 1 year] respectively.
        /// - The candlestick data may be incomplete, and should not be polled repeatedly.
        /// - The maximum data set is 300 candles for a single request.If the request made with the parameters start, end and granularity will result in more data than that is allowed, only 300 candles will be returned. If finer granularity over a larger time range is needed, please make multiple requests as needed.
        /// - The data returned will be arranged in an array as below: [timestamp, open, high, low, close, volume, currency_volume]
        /// </summary>
        /// <param name="symbol">Contract ID,e.g. BTC-USD-180213,BTC-USDT-191227</param>
        /// <param name="period">Bar size in seconds, default 60, must be one of [60/180/300/900/1800/3600/7200/14400/21600/43200/86400/604800] otherwise returns error</param>
        /// <param name="start">Start time in ISO 8601</param>
        /// <param name="end">End time in ISO 8601</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<IEnumerable<OkexFuturesCandle>> Futures_GetCandles(string symbol, OkexSpotPeriod period, DateTime? start = null, DateTime? end = null, CancellationToken ct = default) => Futures_GetCandles_Async(symbol, period, start, end, ct).Result;
        /// <summary>
        /// Retrieve the candlestick charts of the trading pairs. This API can retrieve the latest 1,440 entries of data. Charts are returned in groups based on requested granularity. Maximum 2,880 recent Strings of chart data can be retrieved.
        /// Rate limit: 20 requests per 2 seconds (Depending on the underlying speed limit)
        /// Notes:
        /// - Timestamp must be in ISO 8601 format, otherwise an error be returned.
        /// - When the next-week contract is delivered: the old next-week contract is matched with the new this-week contract, the old this-week contract is matched with the new next-week contract, and the quarterly-contract is unchanged;
        /// - When quarter contract delivery: the old this-week contract is matched with the new quarter contract, the old quarter contract is matched with the new next-week contract, and the old next-week contract is matched with the new this-week contract;
        /// - Both parameters will be ignored if either one of start or end are not provided.The last 300 records of data will be returned if the time range is not specified in the request.
        /// - The granularity field must be one of the following values: [60, 180, 300, 900, 1800, 3600, 7200, 14400, 21600, 43200, 86400, 604800, 2678400, 8035200, 16070400, 31536000]. Otherwise, your request will be rejected.These values correspond to timeslices representing [1 minute, 3 minutes, 5 minutes, 15 minutes, 30 minutes, 1 hour, 2 hours, 4 hours, 6 hours, 12 hours, 1 day, 1 week,1 month ,3 months, 6 months and 1 year] respectively.
        /// - The candlestick data may be incomplete, and should not be polled repeatedly.
        /// - The maximum data set is 300 candles for a single request.If the request made with the parameters start, end and granularity will result in more data than that is allowed, only 300 candles will be returned. If finer granularity over a larger time range is needed, please make multiple requests as needed.
        /// - The data returned will be arranged in an array as below: [timestamp, open, high, low, close, volume, currency_volume]
        /// </summary>
        /// <param name="symbol">Contract ID,e.g. BTC-USD-180213,BTC-USDT-191227</param>
        /// <param name="period">Bar size in seconds, default 60, must be one of [60/180/300/900/1800/3600/7200/14400/21600/43200/86400/604800] otherwise returns error</param>
        /// <param name="start">Start time in ISO 8601</param>
        /// <param name="end">End time in ISO 8601</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<IEnumerable<OkexFuturesCandle>>> Futures_GetCandles_Async(string symbol, OkexSpotPeriod period, DateTime? start = null, DateTime? end = null, CancellationToken ct = default)
        {
            symbol = symbol.ValidateSymbol();

            var parameters = new Dictionary<string, object>
            {
                { "granularity", JsonConvert.SerializeObject(period, new SpotPeriodConverter(false)) },
            };
            parameters.AddOptionalParameter("start", start?.DateTimeToIso8601String());
            parameters.AddOptionalParameter("end", end?.DateTimeToIso8601String());

            return await SendRequest<IEnumerable<OkexFuturesCandle>>(GetUrl(Endpoints_Futures_Candles, symbol), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve indices of tokens. This is publicly accessible without account authentication.
        /// Rate Limit: 20 requests per 2 seconds (Depending on the underlying speed limit)
        /// Notes:
        /// - The token displayed after the hyphen "-" is the quote currency of the index.
        /// </summary>
        /// <param name="symbol">Index, e.g. BTC-USD-190628,BTC-USDT-191227</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<OkexFuturesIndex> Futures_GetIndices(string symbol, CancellationToken ct = default) => Futures_GetIndices_Async(symbol, ct).Result;
        /// <summary>
        /// Retrieve indices of tokens. This is publicly accessible without account authentication.
        /// Rate Limit: 20 requests per 2 seconds (Depending on the underlying speed limit)
        /// Notes:
        /// - The token displayed after the hyphen "-" is the quote currency of the index.
        /// </summary>
        /// <param name="symbol">Index, e.g. BTC-USD-190628,BTC-USDT-191227</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<OkexFuturesIndex>> Futures_GetIndices_Async(string symbol, CancellationToken ct = default)
        {
            symbol = symbol.ValidateSymbol();

            return await SendRequest<OkexFuturesIndex>(GetUrl(Endpoints_Futures_Indices, symbol), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve the fiat exchange rates. This is publicly accessible without account authentication.
        /// Rate limit: 20 requests per 2 seconds (Based on IP speed limit)
        /// </summary>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<OkexFuturesRate> Futures_GetFiatExchangeRates(CancellationToken ct = default) => Futures_GetFiatExchangeRates_Async(ct).Result;
        /// <summary>
        /// Retrieve the fiat exchange rates. This is publicly accessible without account authentication.
        /// Rate limit: 20 requests per 2 seconds (Based on IP speed limit)
        /// </summary>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<OkexFuturesRate>> Futures_GetFiatExchangeRates_Async(CancellationToken ct = default)
        {
            return await SendRequest<OkexFuturesRate>(GetUrl(Endpoints_Futures_ExchangeRates), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <summary>
        /// Get the estimated delivery price. It is available 1 hour before delivery. This is a public endpoint, no identity verification is needed.
        /// Limit: 20 times / 2s (Depending on the underlying speed limit)
        /// </summary>
        /// <param name="symbol">Contract ID, e.g. BTC-USD-180213,BTC-USDT-191227</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<OkexFuturesEstimatedPrice> Futures_GetEstimatedPrice(string symbol, CancellationToken ct = default) => Futures_GetEstimatedPrice_Async(symbol, ct).Result;
        /// <summary>
        /// Get the estimated delivery price. It is available 1 hour before delivery. This is a public endpoint, no identity verification is needed.
        /// Limit: 20 times / 2s (Depending on the underlying speed limit)
        /// </summary>
        /// <param name="symbol">Contract ID, e.g. BTC-USD-180213,BTC-USDT-191227</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<OkexFuturesEstimatedPrice>> Futures_GetEstimatedPrice_Async(string symbol, CancellationToken ct = default)
        {
            symbol = symbol.ValidateSymbol();

            return await SendRequest<OkexFuturesEstimatedPrice>(GetUrl(Endpoints_Futures_EstimatedDeliveryPrice, symbol), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve the total open interest of a contract on OKEx. This is publicly accessible without account authentication.
        /// Rate limit: 20 requests per 2 seconds (Depending on the underlying speed limit)
        /// </summary>
        /// <param name="symbol">Contract ID,e.g. BTC-USD-180213,BTC-USDT-191227</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<OkexFuturesInterest> Futures_GetOpenInterests(string symbol, CancellationToken ct = default) => Futures_GetOpenInterests_Async(symbol, ct).Result;
        /// <summary>
        /// Retrieve the total open interest of a contract on OKEx. This is publicly accessible without account authentication.
        /// Rate limit: 20 requests per 2 seconds (Depending on the underlying speed limit)
        /// </summary>
        /// <param name="symbol">Contract ID,e.g. BTC-USD-180213,BTC-USDT-191227</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<OkexFuturesInterest>> Futures_GetOpenInterests_Async(string symbol, CancellationToken ct = default)
        {
            symbol = symbol.ValidateSymbol();

            return await SendRequest<OkexFuturesInterest>(GetUrl(Endpoints_Futures_OpenInterest, symbol), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve the ceiling of the buy price and floor of sell price of the contract. This is publicly accessible without account authentication.
        /// Rate limit: 20 requests per 2 seconds (Depending on the underlying speed limit)
        /// </summary>
        /// <param name="symbol">Contract ID,e.g. BTC-USD-180309 ,BTC-USDT-191227</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<OkexFuturesPriceRange> Futures_GetPriceLimit(string symbol, CancellationToken ct = default) => Futures_GetPriceLimit_Async(symbol, ct).Result;
        /// <summary>
        /// Retrieve the ceiling of the buy price and floor of sell price of the contract. This is publicly accessible without account authentication.
        /// Rate limit: 20 requests per 2 seconds (Depending on the underlying speed limit)
        /// </summary>
        /// <param name="symbol">Contract ID,e.g. BTC-USD-180309 ,BTC-USDT-191227</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<OkexFuturesPriceRange>> Futures_GetPriceLimit_Async(string symbol, CancellationToken ct = default)
        {
            symbol = symbol.ValidateSymbol();

            return await SendRequest<OkexFuturesPriceRange>(GetUrl(Endpoints_Futures_PriceLimit, symbol), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <summary>
        /// Get the tag price. This is a public endpoint, no identity verification is needed.
        /// Rate Limit: 20 requests per 2 seconds (Depending on the underlying speed limit)
        /// Notes:
        /// - In order to prevent the malicious manipulation of the market by individual users, the contract price fluctuates violently, we set the marked price according to spot index and reasonable basis difference.
        /// </summary>
        /// <param name="symbol">Contract ID,e.g. BTC-USD-190329,BTC-USDT-191227</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<OkexFuturesMarkPrice> Futures_GetMarkPrice(string symbol, CancellationToken ct = default) => Futures_GetMarkPrice_Async(symbol, ct).Result;
        /// <summary>
        /// Get the tag price. This is a public endpoint, no identity verification is needed.
        /// Rate Limit: 20 requests per 2 seconds (Depending on the underlying speed limit)
        /// Notes:
        /// - In order to prevent the malicious manipulation of the market by individual users, the contract price fluctuates violently, we set the marked price according to spot index and reasonable basis difference.
        /// </summary>
        /// <param name="symbol">Contract ID,e.g. BTC-USD-190329,BTC-USDT-191227</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<OkexFuturesMarkPrice>> Futures_GetMarkPrice_Async(string symbol, CancellationToken ct = default)
        {
            symbol = symbol.ValidateSymbol();

            return await SendRequest<OkexFuturesMarkPrice>(GetUrl(Endpoints_Futures_MarkPrice, symbol), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve the liquidated orders. This is publicly accessible without account authentication.
        /// Rate limit: 20 requests per 2 seconds (Depending on the underlying speed limit)
        /// </summary>
        /// <param name="symbol">Contract ID, e.g. BTC-USD-180309,BTC-USDT-191227</param>
        /// <param name="status">0:unfilled in the recent 7 days; 1:filled orders in the recent 7 days)</param>
        /// <param name="limit">Number of results per request. The maximum is 100; the default is 100</param>
        /// <param name="from">Pagination content before requesting this id (older data) e.g 2</param>
        /// <param name="to">Pagination content after requesting this id (updated data) e.g 2</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<IEnumerable<OkexFuturesLiquidatedOrder>> Futures_GetLiquidatedOrders(string symbol, OkexFuturesLiquidationStatus status, int limit = 100, long? from = null, long? to = null, CancellationToken ct = default) => Futures_GetLiquidatedOrders_Async(symbol, status, limit, from, to, ct).Result;
        /// <summary>
        /// Retrieve the liquidated orders. This is publicly accessible without account authentication.
        /// Rate limit: 20 requests per 2 seconds (Depending on the underlying speed limit)
        /// </summary>
        /// <param name="symbol">Contract ID, e.g. BTC-USD-180309,BTC-USDT-191227</param>
        /// <param name="status">0:unfilled in the recent 7 days; 1:filled orders in the recent 7 days)</param>
        /// <param name="limit">Number of results per request. The maximum is 100; the default is 100</param>
        /// <param name="from">Pagination content before requesting this id (older data) e.g 2</param>
        /// <param name="to">Pagination content after requesting this id (updated data) e.g 2</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<IEnumerable<OkexFuturesLiquidatedOrder>>> Futures_GetLiquidatedOrders_Async(string symbol, OkexFuturesLiquidationStatus status, int limit = 100, long? from = null, long? to = null, CancellationToken ct = default)
        {
            symbol = symbol.ValidateSymbol();
            limit.ValidateIntBetween(nameof(limit), 1, 100);

            var parameters = new Dictionary<string, object>
            {
                { "limit", limit.ToString(ci) },
                { "status", JsonConvert.SerializeObject(status, new FuturesLiquidationStatusConverter(false)) },
            };
            parameters.AddOptionalParameter("from", from?.ToString(ci));
            parameters.AddOptionalParameter("to", to?.ToString(ci));

            return await SendRequest<IEnumerable<OkexFuturesLiquidatedOrder>>(GetUrl(Endpoints_Futures_LiquidatedOrders, symbol), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// When using instrument_id to query, i.e., when querying the historical settlement/delivery of a contract, only support querying historical settlement/delivery records of a contract which is after 2020.
        /// Rate limit: 1 requests per 60 seconds (Based on IP speed limit)
        /// Notes
        /// - Support querying historical settlement/delivery records of a contract via instrument_id;
        /// - Support querying the historical settlement/delivery records of all contracts through undering;
        /// </summary>
        /// <param name="instrument">Contract ID,e.g. BTC-USD-180213,instrument_id和underlying must choose one to fill in</param>
        /// <param name="underlying">The underlying index that the contract is based upon, e.g. BTC-USD. ,instrument_id和underlying must choose one to fill in</param>
        /// <param name="start">Start time (ISO 8601 standard, for example:2020-03-08T08:00:00Z）</param>
        /// <param name="end">End time (ISO 8601 standard, for example:2020-03-10T08:00:00Z）</param>
        /// <param name="limit">100 Articles per page</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<IEnumerable<OkexFuturesSettlementHistory>> Futures_GetSettlementHistory(string? instrument = null, string? underlying = null, DateTime? start = null, DateTime? end = null, int limit = 100, CancellationToken ct = default) => Futures_GetSettlementHistory_Async(instrument, underlying, start, end, limit, ct).Result;
        /// <summary>
        /// When using instrument_id to query, i.e., when querying the historical settlement/delivery of a contract, only support querying historical settlement/delivery records of a contract which is after 2020.
        /// Rate limit: 1 requests per 60 seconds (Based on IP speed limit)
        /// Notes
        /// - Support querying historical settlement/delivery records of a contract via instrument_id;
        /// - Support querying the historical settlement/delivery records of all contracts through undering;
        /// </summary>
        /// <param name="instrument">Contract ID,e.g. BTC-USD-180213,instrument_id和underlying must choose one to fill in</param>
        /// <param name="underlying">The underlying index that the contract is based upon, e.g. BTC-USD. ,instrument_id和underlying must choose one to fill in</param>
        /// <param name="start">Start time (ISO 8601 standard, for example:2020-03-08T08:00:00Z）</param>
        /// <param name="end">End time (ISO 8601 standard, for example:2020-03-10T08:00:00Z）</param>
        /// <param name="limit">100 Articles per page</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<IEnumerable<OkexFuturesSettlementHistory>>> Futures_GetSettlementHistory_Async(string? instrument = null, string? underlying = null, DateTime? start = null, DateTime? end = null, int limit = 100, CancellationToken ct = default)
        {
            limit.ValidateIntBetween(nameof(limit), 1, 100);

            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("instrument_id", instrument);
            parameters.AddOptionalParameter("underlying", underlying);
            parameters.AddOptionalParameter("start", start?.DateTimeToIso8601String());
            parameters.AddOptionalParameter("end", end?.DateTimeToIso8601String());
            parameters.AddOptionalParameter("limit", limit.ToString(ci));

            return await SendRequest<IEnumerable<OkexFuturesSettlementHistory>>(GetUrl(Endpoints_Futures_SettlementHistory), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve the history candles of the contract.As of now, the historical candels of 9 major currencies are provided: BTC, ETH, LTC, ETC, XRP, EOS, BCH, BSV, TRX.
        /// Rate limit: 20 requests per 2 seconds (Depending on the underlying speed limit)
        /// Notes
        /// - The return values are[timestamp, open, high, low, close, volume, currency_volume]
        /// - start > end, start defaults to the latest, end defaults to the oldest, limit defaults to a fixed number, and returns the latest(limit) records between start and end.If the number between start and end is greater than the limit, begining from start.
        /// </summary>
        /// <param name="symbol">Contract ID,e.g. BTC-USD-180213,BTC-USDT-191227,Must use the currently existing contract id to get k-line data</param>
        /// <param name="period">Bar size in seconds, default 60, must be one of [60/180/300/900/1800/3600/7200/14400/21600/43200/86400/604800] otherwise returns error</param>
        /// <param name="start">Start time in ISO 8601,start is a newer time, the default is the current time.</param>
        /// <param name="end">End time in ISO 8601, end is the older time</param>
        /// <param name="limit">The number of candles returned, the default is 300，and the maximum is 300</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual WebCallResult<IEnumerable<OkexFuturesCandle>> Futures_GetHistoricalMarketData(string symbol, OkexSpotPeriod period, DateTime? start = null, DateTime? end = null, int limit = 300, CancellationToken ct = default) => Futures_GetHistoricalMarketData_Async(symbol, period, start, end, limit, ct).Result;
        /// <summary>
        /// Retrieve the history candles of the contract.As of now, the historical candels of 9 major currencies are provided: BTC, ETH, LTC, ETC, XRP, EOS, BCH, BSV, TRX.
        /// Rate limit: 20 requests per 2 seconds (Depending on the underlying speed limit)
        /// Notes
        /// - The return values are[timestamp, open, high, low, close, volume, currency_volume]
        /// - start > end, start defaults to the latest, end defaults to the oldest, limit defaults to a fixed number, and returns the latest(limit) records between start and end.If the number between start and end is greater than the limit, begining from start.
        /// </summary>
        /// <param name="symbol">Contract ID,e.g. BTC-USD-180213,BTC-USDT-191227,Must use the currently existing contract id to get k-line data</param>
        /// <param name="period">Bar size in seconds, default 60, must be one of [60/180/300/900/1800/3600/7200/14400/21600/43200/86400/604800] otherwise returns error</param>
        /// <param name="start">Start time in ISO 8601,start is a newer time, the default is the current time.</param>
        /// <param name="end">End time in ISO 8601, end is the older time</param>
        /// <param name="limit">The number of candles returned, the default is 300，and the maximum is 300</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public virtual async Task<WebCallResult<IEnumerable<OkexFuturesCandle>>> Futures_GetHistoricalMarketData_Async(string symbol, OkexSpotPeriod period, DateTime? start = null, DateTime? end = null, int limit = 300, CancellationToken ct = default)
        {
            symbol = symbol.ValidateSymbol();
            limit.ValidateIntBetween(nameof(limit), 1, 300);

            var parameters = new Dictionary<string, object>
            {
                { "granularity", JsonConvert.SerializeObject(period, new SpotPeriodConverter(false)) },
            };
            parameters.AddOptionalParameter("start", start?.DateTimeToIso8601String());
            parameters.AddOptionalParameter("end", end?.DateTimeToIso8601String());
            parameters.AddOptionalParameter("limit", limit.ToString(ci));

            return await SendRequest<IEnumerable<OkexFuturesCandle>>(GetUrl(Endpoints_Futures_HistoricalMarketData, symbol), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        #endregion

        #endregion

    }
}