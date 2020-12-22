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
    public partial class OkexClient : IOkexClientSwap
    {
        #region Perpetual Swap Trading API

        #region Private Signed Endpoints

        /// <summary>
        /// Retrieve the information on all your positions in the swap account. You are recommended to get the information one token at a time to improve performance.
        /// Rate limit：5 requests per 2 seconds
        /// </summary>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<IEnumerable<OkexSwapPosition>> Swap_GetPositions(CancellationToken ct = default) => Swap_GetPositions_Async(ct).Result;
        /// <summary>
        /// Retrieve the information on all your positions in the swap account. You are recommended to get the information one token at a time to improve performance.
        /// Rate limit：5 requests per 2 seconds
        /// </summary>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<OkexSwapPosition>>> Swap_GetPositions_Async(CancellationToken ct = default)
        {
            return await SendRequest<IEnumerable<OkexSwapPosition>>(GetUrl(Endpoints_Swap_Positions), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve information on your positions of a single contract.
        /// Rate limit: 20 requests per 2 seconds
        /// </summary>
        /// <param name="symbol">Contract ID, e.g. BTC-USD-SWAP,BTC-USDT-SWAP</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<OkexSwapPosition> Swap_GetPositions(string symbol, CancellationToken ct = default) => Swap_GetPositions_Async(symbol, ct).Result;
        /// <summary>
        /// Retrieve information on your positions of a single contract.
        /// Rate limit: 20 requests per 2 seconds
        /// </summary>
        /// <param name="symbol">Contract ID, e.g. BTC-USD-SWAP,BTC-USDT-SWAP</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<OkexSwapPosition>> Swap_GetPositions_Async(string symbol, CancellationToken ct = default)
        {
            return await SendRequest<OkexSwapPosition>(GetUrl(Endpoints_Swap_PositionsOfContract, symbol), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve information from all tokens in the perpetual swap account. Margin ratio is set as 10,000 when users have no open position.
        /// Rate Limit: 1 request per 10 seconds
        /// Notes
        /// - For "all open interests/all account info" futures and perpetual swap account endpoints, if no position/token is held then no response will be returned.For "single open interests/single account info" endpoints, if no position/token is held then the response will return with default value.
        /// - Fixed-margin mode:
        ///   - Account equity = Balance of Funding Account + Balance of Fixed-margin Account + RPL(Realized Profit and Loss) of All Contracts + UPL(Unrealized Profit and Loss) of All Contracts
        ///   - Available Margin = Balance of Funding Account + Balance of Fixed-margin Account + RPL(Realized Profit and Loss) of the Contract - Maintenance Margin of the Open Interests - Margin frozen for Open Orders
        /// - Cross-margin mode:
        ///   - Account Equity = Balance of Fund Account + RPL(Realized Profit and Loss) of All Contracts + UPL(Unrealized Profit and Loss) of All Contracts
        ///   - Available Margin = Balance of Fund Account + RPL(Realized Profit and Loss) of All Contracts + UPL(Unrealized Profit and Loss) of All Contracts - Maintenance Margin of the Open Interests - Margin frozen for Open Orders
        /// </summary>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<OkexSwapBalances> Swap_GetBalances(CancellationToken ct = default) => Swap_GetBalances_Async(ct).Result;
        /// <summary>
        /// Retrieve information from all tokens in the perpetual swap account. Margin ratio is set as 10,000 when users have no open position.
        /// Rate Limit: 1 request per 10 seconds
        /// Notes
        /// - For "all open interests/all account info" futures and perpetual swap account endpoints, if no position/token is held then no response will be returned.For "single open interests/single account info" endpoints, if no position/token is held then the response will return with default value.
        /// - Fixed-margin mode:
        ///   - Account equity = Balance of Funding Account + Balance of Fixed-margin Account + RPL(Realized Profit and Loss) of All Contracts + UPL(Unrealized Profit and Loss) of All Contracts
        ///   - Available Margin = Balance of Funding Account + Balance of Fixed-margin Account + RPL(Realized Profit and Loss) of the Contract - Maintenance Margin of the Open Interests - Margin frozen for Open Orders
        /// - Cross-margin mode:
        ///   - Account Equity = Balance of Fund Account + RPL(Realized Profit and Loss) of All Contracts + UPL(Unrealized Profit and Loss) of All Contracts
        ///   - Available Margin = Balance of Fund Account + RPL(Realized Profit and Loss) of All Contracts + UPL(Unrealized Profit and Loss) of All Contracts - Maintenance Margin of the Open Interests - Margin frozen for Open Orders
        /// </summary>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<OkexSwapBalances>> Swap_GetBalances_Async(CancellationToken ct = default)
        {
            return await SendRequest<OkexSwapBalances>(GetUrl(Endpoints_Swap_Accounts), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve the perpetual swap account information of a single trading pair. Margin ratio set as 10,000 when users have no open position.
        /// Rate Limit: 20 requests per 2 seconds
        /// Notes
        /// - For "all open interests/all account info" futures and perpetual swap account endpoints, if no position/token is held then no response will be returned.For "single open interests/single account info" endpoints, if no position/token is held then the response will return with default value.
        /// - Fixed-margin mode:
        ///   - Account equity = Balance of Funding Account + Balance of Fixed-margin Account + RPL(Realized Profit and Loss) of All Contracts + UPL(Unrealized Profit and Loss) of All Contracts
        ///   - Available Margin = Balance of Funding Account + Balance of Fixed-margin Account + RPL(Realized Profit and Loss) of the Contract - Maintenance Margin of the Open Interests - Margin frozen for Open Orders
        /// - Cross-margin mode:
        ///   - Account Equity = Balance of Fund Account + RPL(Realized Profit and Loss) of All Contracts + UPL(Unrealized Profit and Loss) of All Contracts
        ///   - Available Margin = Balance of Fund Account + RPL(Realized Profit and Loss) of All Contracts + UPL(Unrealized Profit and Loss) of All Contracts - Maintenance Margin of the Open Interests - Margin frozen for Open Orders
        /// </summary>
        /// <param name="symbol">Contract ID, e.g. BTC-USD-SWAP,BTC-USDT-SWAP</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<OkexSwapBalanceOfSymbol> Swap_GetBalances(string symbol, CancellationToken ct = default) => Swap_GetBalances_Async(symbol, ct).Result;
        /// <summary>
        /// Retrieve the perpetual swap account information of a single trading pair. Margin ratio set as 10,000 when users have no open position.
        /// Rate Limit: 20 requests per 2 seconds
        /// Notes
        /// - For "all open interests/all account info" futures and perpetual swap account endpoints, if no position/token is held then no response will be returned.For "single open interests/single account info" endpoints, if no position/token is held then the response will return with default value.
        /// - Fixed-margin mode:
        ///   - Account equity = Balance of Funding Account + Balance of Fixed-margin Account + RPL(Realized Profit and Loss) of All Contracts + UPL(Unrealized Profit and Loss) of All Contracts
        ///   - Available Margin = Balance of Funding Account + Balance of Fixed-margin Account + RPL(Realized Profit and Loss) of the Contract - Maintenance Margin of the Open Interests - Margin frozen for Open Orders
        /// - Cross-margin mode:
        ///   - Account Equity = Balance of Fund Account + RPL(Realized Profit and Loss) of All Contracts + UPL(Unrealized Profit and Loss) of All Contracts
        ///   - Available Margin = Balance of Fund Account + RPL(Realized Profit and Loss) of All Contracts + UPL(Unrealized Profit and Loss) of All Contracts - Maintenance Margin of the Open Interests - Margin frozen for Open Orders
        /// </summary>
        /// <param name="symbol">Contract ID, e.g. BTC-USD-SWAP,BTC-USDT-SWAP</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<OkexSwapBalanceOfSymbol>> Swap_GetBalances_Async(string symbol, CancellationToken ct = default)
        {
            return await SendRequest<OkexSwapBalanceOfSymbol>(GetUrl(Endpoints_Swap_AccountsOfCurrency, symbol), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve the leverage ratio and margin mode of a perpetual swap
        /// Rate limit：5 requests per 2 seconds
        /// Notes:
        /// - For cross-margin mode, only one leverage ratio is allowed per trading pair. For fixed-margin mode, one leverage ratio is allowed per contract per side (long or short).
        /// </summary>
        /// <param name="symbol">Contract ID, e.g. BTC-USD-SWAP,BTC-USDT-SWAP</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<OkexSwapLeverage> Swap_GetLeverage(string symbol, CancellationToken ct = default) => Swap_GetLeverage_Async(symbol, ct).Result;
        /// <summary>
        /// Retrieve the leverage ratio and margin mode of a perpetual swap
        /// Rate limit：5 requests per 2 seconds
        /// Notes:
        /// - For cross-margin mode, only one leverage ratio is allowed per trading pair. For fixed-margin mode, one leverage ratio is allowed per contract per side (long or short).
        /// </summary>
        /// <param name="symbol">Contract ID, e.g. BTC-USD-SWAP,BTC-USDT-SWAP</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<OkexSwapLeverage>> Swap_GetLeverage_Async(string symbol, CancellationToken ct = default)
        {
            return await SendRequest<OkexSwapLeverage>(GetUrl(Endpoints_Swap_GetSwapLeverage, symbol), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Used to adjust the leverage for perpetual swap account
        /// Rate Limit: 5 requests per 2 seconds
        /// </summary>
        /// <param name="symbol">Contract ID, e.g. BTC-USD-SWAP,BTC-USDT-SWAP</param>
        /// <param name="side">'1’. Fixed-margin for long position; ‘2’. Fixed-margin for short position; ‘3’. Crossed-margin</param>
        /// <param name="leverage">New leverage level from 1-100</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<OkexSwapLeverage> Swap_SetLeverage(string symbol, OkexSwapLeverageSide side, int leverage, CancellationToken ct = default) => Swap_SetLeverage_Async(symbol, side, leverage, ct).Result;
        /// <summary>
        /// Used to adjust the leverage for perpetual swap account
        /// Rate Limit: 5 requests per 2 seconds
        /// </summary>
        /// <param name="symbol">Contract ID, e.g. BTC-USD-SWAP,BTC-USDT-SWAP</param>
        /// <param name="side">'1’. Fixed-margin for long position; ‘2’. Fixed-margin for short position; ‘3’. Crossed-margin</param>
        /// <param name="leverage">New leverage level from 1-100</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<OkexSwapLeverage>> Swap_SetLeverage_Async(string symbol, OkexSwapLeverageSide side, int leverage, CancellationToken ct = default)
        {
            leverage.ValidateIntBetween(nameof(leverage), 1, 100);
            var parameters = new Dictionary<string, object>()
            {
                { "leverage", leverage },
                { "side", JsonConvert.SerializeObject(side, new SwapLeverageSideConverter(false)) },
            };

            return await SendRequest<OkexSwapLeverage>(GetUrl(Endpoints_Swap_SetSwapLeverage, symbol), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve the bills of the perpetual swap account. The bill refers to all the records that results in changing the balance of an account. This API can retrieve data from the last 7 days.
        /// Rate limit：5 requests per 2 seconds
        /// </summary>
        /// <param name="symbol">Contract ID, e.g. BTC-USD-SWAP,BTC-USDT-SWAP</param>
        /// <param name="type">1:Open Long 2:Open Short 3:Close Long 4:Close Short 5:Transfer In，6:Transfer Out 7:Settled UPL 8:Clawback 9:Insurance Fund 10:Full Liquidation of Long 11:Full Liquidation of Short 14: Funding Fee 15: Manually Add Margin 16: Manually Reduce Margin 17: Auto-Margin 18: Switch Margin Mode 19: Partial Liquidation of Long 20 Partial Liquidation of Short 21 Margin Added with Lowered Leverage 22: Settled RPL</param>
        /// <param name="limit">Number of results per request. The maximum is 100; the default is 100</param>
        /// <param name="before">Pagination of data to return records newer than the requested ledger_id</param>
        /// <param name="after">Pagination of data to return records earlier than the requested ledger_id</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<IEnumerable<OkexSwapBill>> Swap_GetBills(string symbol, OkexSwapBillType? type = null, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default) => Swap_GetBills_Async(symbol, type, limit, before, after, ct).Result;
        /// <summary>
        /// Retrieve the bills of the perpetual swap account. The bill refers to all the records that results in changing the balance of an account. This API can retrieve data from the last 7 days.
        /// Rate limit：5 requests per 2 seconds
        /// </summary>
        /// <param name="symbol">Contract ID, e.g. BTC-USD-SWAP,BTC-USDT-SWAP</param>
        /// <param name="type">1:Open Long 2:Open Short 3:Close Long 4:Close Short 5:Transfer In，6:Transfer Out 7:Settled UPL 8:Clawback 9:Insurance Fund 10:Full Liquidation of Long 11:Full Liquidation of Short 14: Funding Fee 15: Manually Add Margin 16: Manually Reduce Margin 17: Auto-Margin 18: Switch Margin Mode 19: Partial Liquidation of Long 20 Partial Liquidation of Short 21 Margin Added with Lowered Leverage 22: Settled RPL</param>
        /// <param name="limit">Number of results per request. The maximum is 100; the default is 100</param>
        /// <param name="before">Pagination of data to return records newer than the requested ledger_id</param>
        /// <param name="after">Pagination of data to return records earlier than the requested ledger_id</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<OkexSwapBill>>> Swap_GetBills_Async(string symbol, OkexSwapBillType? type = null, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default)
        {
            symbol = symbol.ValidateSymbol();
            limit.ValidateIntBetween(nameof(limit), 1, 100);

            var parameters = new Dictionary<string, object>
            {
                { "limit", limit },
            };
            if (type != null) parameters.AddOptionalParameter("type", JsonConvert.SerializeObject(type, new SwapBillTypeConverter(false)));
            parameters.AddOptionalParameter("before", before);
            parameters.AddOptionalParameter("after", after);

            return await SendRequest<IEnumerable<OkexSwapBill>>(GetUrl(Endpoints_Swap_Bills, symbol), HttpMethod.Get, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// OKEx perpetual swap supports limit and market orders.. You can place an order only if you have sufficient funds. Once your order is placed, the amount will be put on hold during the order life cycle. The assets and amount put on hold depends on the order's specific type and parameters. Only USD is supported as quote currency.
        /// Rate Limit: 40 requests per 2 seconds (Speed limit rules: 1) The speed limit is not accumulated between different contracts； 2) Api limit is separated per swap instrument)
        /// Notes:
        /// - The parameters need to be verified: 
        ///   1) The instrument_id must match a valid contract ID; 
        ///   2) The price must meet the requirements of filled order price (neither premium ceiling nor liquidation price has been reached); 
        ///   3) The types will trigger an error code if it is not one of: 1: open long; 2: open short; 3: close long 4: close short; 
        ///   4) The size cannot be less than 0. It also cannot be larger than the available contract size.
        /// - If the error_code value in response is NOT zero, then it means the request was rejected because verification failed.
        /// - instrument_id
        ///   The instrument_id must match a valid contract ID. The contract list is available via the /instruments endpoint.
        /// - client_oid
        ///   The client_oid is optional and you can customize it using alpha-numeric characters with length 1 to 32. This parameter is used to identify your orders in the public orders feed. No warning is sent when client_oid is not unique. In case of multiple identical client_oid, only the latest entry will be returned.
        /// - type
        ///   You can specify the order type when placing an order. If you are not holding any positions, you can only open new positions, either long or short. You can only close the positions that has been already held.
        ///   The price must be specified in tick size product units.The tick size is the smallest unit of price.Can be obtained through the /instrument interface.
        /// - price
        ///   The price is the price of buying or selling a contract. price must be an incremental multiple of the tick_size. tick_size is the smallest incremental unit of price, which is available via the /instruments endpoint.
        /// - size
        ///   size is the number of contracts bought or sold. The value must be an integer.
        /// - match_price
        ///   The match_price means that you prefer the order to be filled at a best price of the counterpart, where your buy order will be filled at the price of Ask-1. The match_price means that your sell order will be filled at the price of Bid-1.
        /// - Order life cycle
        ///   The HTTP Request will respond when an order is either rejected (insufficient funds, invalid parameters, etc) or received (accepted by the matching engine). A 200 response indicates that the order was received and is active. Active orders may execute immediately (depending on price and market conditions) either partially or fully. 
        ///   A partial execution will put the remaining size of the order in the open state. An order that is filled Fully, will go into the completed state.
        ///   Users listening to streaming market data are encouraged to use the client_oid field to identify their received messages in the feed. The REST response with a server order_id may come after the received message in the public data feed.
        /// - Response
        ///   A successful order will be assigned an order id. A successful order is defined as one that has been accepted by the matching engine. Open orders will not expire until filled or canceled.
        /// </summary>
        /// <param name="symbol">Contract ID, e.g. BTC-USD-SWAP,BTC-USDT-SWAP</param>
        /// <param name="type">1:open long 2:open short 3:close long 4:close short</param>
        /// <param name="size">The number of contracts bought or sold (minimum size as 1)</param>
        /// <param name="timeInForce">‘0’: Normal order. Parameter will be deemed as '0' if left blank. ‘1’: Post only (Order shall be filled only as maker) ‘2’: Fill or Kill (FOK) ‘3’: Immediate or Cancel (IOC) 4：Market</param>
        /// <param name="price">Price of each contract</param>
        /// <param name="match_price">Whether order is placed at best counter party price (‘0’:no ‘1’:yes). The parameter is defaulted as ‘0’. If it is set as '1', the price parameter will be ignored，When posting orders at best bid price, order_type can only be '0' (regular order)</param>
        /// <param name="clientOrderId">You can customize order IDs to identify your orders. The system supports alphabets + numbers(case-sensitive，e.g:A123、a123), or alphabets (case-sensitive，e.g:Abc、abc) only, between 1-32 characters.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<OkexSwapPlacedOrder> Swap_PlaceOrder(string symbol, OkexSwapOrderType type, decimal size, OkexSwapTimeInForce timeInForce = OkexSwapTimeInForce.NormalOrder, decimal? price = null, bool match_price = false, string? clientOrderId = null, CancellationToken ct = default) => Swap_PlaceOrder_Async(symbol, type, size, timeInForce, price, match_price, clientOrderId, ct).Result;
        /// <summary>
        /// OKEx perpetual swap supports limit and market orders.. You can place an order only if you have sufficient funds. Once your order is placed, the amount will be put on hold during the order life cycle. The assets and amount put on hold depends on the order's specific type and parameters. Only USD is supported as quote currency.
        /// Rate Limit: 40 requests per 2 seconds (Speed limit rules: 1) The speed limit is not accumulated between different contracts； 2) Api limit is separated per swap instrument)
        /// Notes:
        /// - The parameters need to be verified: 
        ///   1) The instrument_id must match a valid contract ID; 
        ///   2) The price must meet the requirements of filled order price (neither premium ceiling nor liquidation price has been reached); 
        ///   3) The types will trigger an error code if it is not one of: 1: open long; 2: open short; 3: close long 4: close short; 
        ///   4) The size cannot be less than 0. It also cannot be larger than the available contract size.
        /// - If the error_code value in response is NOT zero, then it means the request was rejected because verification failed.
        /// - instrument_id
        ///   The instrument_id must match a valid contract ID. The contract list is available via the /instruments endpoint.
        /// - client_oid
        ///   The client_oid is optional and you can customize it using alpha-numeric characters with length 1 to 32. This parameter is used to identify your orders in the public orders feed. No warning is sent when client_oid is not unique. In case of multiple identical client_oid, only the latest entry will be returned.
        /// - type
        ///   You can specify the order type when placing an order. If you are not holding any positions, you can only open new positions, either long or short. You can only close the positions that has been already held.
        ///   The price must be specified in tick size product units.The tick size is the smallest unit of price.Can be obtained through the /instrument interface.
        /// - price
        ///   The price is the price of buying or selling a contract. price must be an incremental multiple of the tick_size. tick_size is the smallest incremental unit of price, which is available via the /instruments endpoint.
        /// - size
        ///   size is the number of contracts bought or sold. The value must be an integer.
        /// - match_price
        ///   The match_price means that you prefer the order to be filled at a best price of the counterpart, where your buy order will be filled at the price of Ask-1. The match_price means that your sell order will be filled at the price of Bid-1.
        /// - Order life cycle
        ///   The HTTP Request will respond when an order is either rejected (insufficient funds, invalid parameters, etc) or received (accepted by the matching engine). A 200 response indicates that the order was received and is active. Active orders may execute immediately (depending on price and market conditions) either partially or fully. 
        ///   A partial execution will put the remaining size of the order in the open state. An order that is filled Fully, will go into the completed state.
        ///   Users listening to streaming market data are encouraged to use the client_oid field to identify their received messages in the feed. The REST response with a server order_id may come after the received message in the public data feed.
        /// - Response
        ///   A successful order will be assigned an order id. A successful order is defined as one that has been accepted by the matching engine. Open orders will not expire until filled or canceled.
        /// </summary>
        /// <param name="symbol">Contract ID, e.g. BTC-USD-SWAP,BTC-USDT-SWAP</param>
        /// <param name="type">1:open long 2:open short 3:close long 4:close short</param>
        /// <param name="size">The number of contracts bought or sold (minimum size as 1)</param>
        /// <param name="timeInForce">‘0’: Normal order. Parameter will be deemed as '0' if left blank. ‘1’: Post only (Order shall be filled only as maker) ‘2’: Fill or Kill (FOK) ‘3’: Immediate or Cancel (IOC) 4：Market</param>
        /// <param name="price">Price of each contract</param>
        /// <param name="match_price">Whether order is placed at best counter party price (‘0’:no ‘1’:yes). The parameter is defaulted as ‘0’. If it is set as '1', the price parameter will be ignored，When posting orders at best bid price, order_type can only be '0' (regular order)</param>
        /// <param name="clientOrderId">You can customize order IDs to identify your orders. The system supports alphabets + numbers(case-sensitive，e.g:A123、a123), or alphabets (case-sensitive，e.g:Abc、abc) only, between 1-32 characters.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<OkexSwapPlacedOrder>> Swap_PlaceOrder_Async(string symbol, OkexSwapOrderType type, decimal size, OkexSwapTimeInForce timeInForce = OkexSwapTimeInForce.NormalOrder, decimal? price = null, bool match_price = false, string? clientOrderId = null, CancellationToken ct = default)
        {
            symbol = symbol.ValidateSymbol();
            clientOrderId?.ValidateStringLength("clientOrderId", 0, 32);
            if (clientOrderId != null && !Regex.IsMatch(clientOrderId, "^(([a-z]|[A-Z]|[0-9]){0,32})$"))
                throw new ArgumentException("ClientOrderId supports alphabets (case-sensitive) + numbers, or letters (case-sensitive) between 1-32 characters.");

            var parameters = new Dictionary<string, object>
            {
                { "instrument_id", symbol },
                { "type", JsonConvert.SerializeObject(type, new SwapOrderTypeConverter(false)) },
                { "size", size },
                { "match_price", match_price?1:0 },
                { "order_type", JsonConvert.SerializeObject(timeInForce, new SwapTimeInForceConverter(false)) },
            };
            parameters.AddOptionalParameter("client_oid", clientOrderId);
            parameters.AddOptionalParameter("price", price);

            return await SendRequest<OkexSwapPlacedOrder>(GetUrl(Endpoints_Swap_PlaceOrder), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Place contract orders in a batch. Maximum 20 orders can be placed at a time for each contract.
        /// Rate Limit: 20 requests per 2 seconds (Speed limit rules: 1) The speed limit is not accumulated between different contracts； 2) Api limit is separated per swap instrument)
        /// Notes:
        /// - If the error_code value in response is NOT zero, then it means the request was rejected because verification failed.
        /// - The client_oid is optional.It should be a unique ID generated by your trading system.This parameter is used to identify your orers in the public orders feed.No warning is sent when client_oid is not unique.
        /// - In case of multiple identical client_oid, only the latest entry will be returned.
        /// - As long as any of the orders are successful, result returns ‘true’. The response message is returned in the same order as that of the order_data submitted.If the order fails to be placed, order_id is ‘-1’.
        /// </summary>
        /// <param name="symbol">Contract ID, e.g. BTC-USD-SWAP,BTC-USDT-SWAP</param>
        /// <param name="orders">Order List</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<OkexSwapBatchPlacedOrder> Swap_BatchPlaceOrders(string symbol, IEnumerable<OkexSwapPlaceOrder> orders, CancellationToken ct = default) => Swap_BatchPlaceOrders_Async(symbol, orders, ct).Result;
        /// <summary>
        /// Place contract orders in a batch. Maximum 20 orders can be placed at a time for each contract.
        /// Rate Limit: 20 requests per 2 seconds (Speed limit rules: 1) The speed limit is not accumulated between different contracts； 2) Api limit is separated per swap instrument)
        /// Notes:
        /// - If the error_code value in response is NOT zero, then it means the request was rejected because verification failed.
        /// - The client_oid is optional.It should be a unique ID generated by your trading system.This parameter is used to identify your orers in the public orders feed.No warning is sent when client_oid is not unique.
        /// - In case of multiple identical client_oid, only the latest entry will be returned.
        /// - As long as any of the orders are successful, result returns ‘true’. The response message is returned in the same order as that of the order_data submitted.If the order fails to be placed, order_id is ‘-1’.
        /// </summary>
        /// <param name="symbol">Contract ID, e.g. BTC-USD-SWAP,BTC-USDT-SWAP</param>
        /// <param name="orders">Order List</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<OkexSwapBatchPlacedOrder>> Swap_BatchPlaceOrders_Async(string symbol, IEnumerable<OkexSwapPlaceOrder> orders, CancellationToken ct = default)
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
                { "order_data", orders },
            };

            return await SendRequest<OkexSwapBatchPlacedOrder>(GetUrl(Endpoints_Swap_BatchPlaceOrders), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// This is used to cancel an unfilled order.
        /// Rate limit: 40 requests per 2 seconds
        /// Request Parameters Verification: Contract ID and order ID must already exist
        /// Notes
        /// - Only one of order_id or client_oid parameters should be passed per request
        /// - The client_oid should be unique.No warning is sent when client_oid is not unique.
        /// - In case of multiple identical client_oid, only the latest entry will be returned.
        /// - If the order cannot be canceled because it has already filled or been canceled, the reason will be returned with the error message.
        /// - The response includes order_id, which does not confirm that the orders has been canceled successfully.Orders that are being filled cannot be canceled whereas orders that have not been filled could been canceled.
        /// </summary>
        /// <param name="symbol">Contract ID, e.g. BTC-USD-SWAP,BTC-USDT-SWAP</param>
        /// <param name="orderId">Either client_oid or order_id must be present. Order ID</param>
        /// <param name="clientOrderId">Either client_oid or order_id must be present. Client-supplied order ID that you can customize. It should be comprised of alpha-numeric characters with length 1 to 32. Both uppercase and lowercase are supported</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<OkexSwapPlacedOrder> Swap_CancelOrder(string symbol, long? orderId = null, string? clientOrderId = null, CancellationToken ct = default) => Swap_CancelOrder_Async(symbol, orderId, clientOrderId, ct).Result;
        /// <summary>
        /// This is used to cancel an unfilled order.
        /// Rate limit: 40 requests per 2 seconds
        /// Request Parameters Verification: Contract ID and order ID must already exist
        /// Notes
        /// - Only one of order_id or client_oid parameters should be passed per request
        /// - The client_oid should be unique.No warning is sent when client_oid is not unique.
        /// - In case of multiple identical client_oid, only the latest entry will be returned.
        /// - If the order cannot be canceled because it has already filled or been canceled, the reason will be returned with the error message.
        /// - The response includes order_id, which does not confirm that the orders has been canceled successfully.Orders that are being filled cannot be canceled whereas orders that have not been filled could been canceled.
        /// </summary>
        /// <param name="symbol">Contract ID, e.g. BTC-USD-SWAP,BTC-USDT-SWAP</param>
        /// <param name="orderId">Either client_oid or order_id must be present. Order ID</param>
        /// <param name="clientOrderId">Either client_oid or order_id must be present. Client-supplied order ID that you can customize. It should be comprised of alpha-numeric characters with length 1 to 32. Both uppercase and lowercase are supported</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<OkexSwapPlacedOrder>> Swap_CancelOrder_Async(string symbol, long? orderId = null, string? clientOrderId = null, CancellationToken ct = default)
        {
            symbol = symbol.ValidateSymbol();

            if (orderId == null && string.IsNullOrEmpty(clientOrderId))
                throw new ArgumentException("Either orderId or clientOrderId must be present.");

            if (orderId != null && !string.IsNullOrEmpty(clientOrderId))
                throw new ArgumentException("Either orderId or clientOrderId must be present.");

            return await SendRequest<OkexSwapPlacedOrder>(GetUrl(Endpoints_Swap_CancelOrder.Replace("<instrument_id>", symbol), orderId.HasValue ? orderId.ToString() : clientOrderId!), HttpMethod.Post, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Cancel multiple open orders with order_id or client_oid，Maximum 10 orders can be cancelled at a time for each contract."
        /// Rate Limit: 20 requests per 2 seconds 
        /// Speed limit rules: 
        /// 1) The speed limit is not accumulated between different contracts； 
        /// 2) Api limit is separated per swap instrument
        /// Notes
        /// - Request Parameters Verification: same as "Cancel an Order".
        /// - For batch order cancellation, only one of order_id or client_oid parameters should be passed per request.Otherwise an error will be returned.
        /// - When using client_oid for batch order cancellation, up to 10 orders can be canceled per contract.You need to make sure the ID is unique.In case of multiple identical client_oid, only the latest entry will be returned.
        /// - Cancellations of orders are not guaranteed. After placing a cancel order you should confirm they are successfully canceled by requesting the "Get Order List" endpoint.
        /// </summary>
        /// <param name="symbol">The orders of the contract to be canceled</param>
        /// <param name="orderIds">Either client_oid or order_id must be present. ID of the orders to be canceled</param>
        /// <param name="clientOrderIds">Either client_oid or order_id must be present. Client-supplied order ID that you can customize. It should be comprised of alpha-numeric characters with length 1 to 32. Both uppercase and lowercase are supported</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<OkexSwapBatchOrders> Swap_BatchCancelOrders(string symbol, IEnumerable<long> orderIds, IEnumerable<string> clientOrderIds, CancellationToken ct = default) => Swap_BatchCancelOrders_Async(symbol, orderIds, clientOrderIds, ct).Result;
        /// <summary>
        /// Cancel multiple open orders with order_id or client_oid，Maximum 10 orders can be cancelled at a time for each contract."
        /// Rate Limit: 20 requests per 2 seconds 
        /// Speed limit rules: 
        /// 1) The speed limit is not accumulated between different contracts； 
        /// 2) Api limit is separated per swap instrument
        /// Notes
        /// - Request Parameters Verification: same as "Cancel an Order".
        /// - For batch order cancellation, only one of order_id or client_oid parameters should be passed per request.Otherwise an error will be returned.
        /// - When using client_oid for batch order cancellation, up to 10 orders can be canceled per contract.You need to make sure the ID is unique.In case of multiple identical client_oid, only the latest entry will be returned.
        /// - Cancellations of orders are not guaranteed. After placing a cancel order you should confirm they are successfully canceled by requesting the "Get Order List" endpoint.
        /// </summary>
        /// <param name="symbol">The orders of the contract to be canceled</param>
        /// <param name="orderIds">Either client_oid or order_id must be present. ID of the orders to be canceled</param>
        /// <param name="clientOrderIds">Either client_oid or order_id must be present. Client-supplied order ID that you can customize. It should be comprised of alpha-numeric characters with length 1 to 32. Both uppercase and lowercase are supported</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<OkexSwapBatchOrders>> Swap_BatchCancelOrders_Async(string symbol, IEnumerable<long> orderIds, IEnumerable<string> clientOrderIds, CancellationToken ct = default)
        {
            symbol = symbol.ValidateSymbol();

            if ((orderIds == null || orderIds.Count() == 0) && (clientOrderIds == null || clientOrderIds.Count() == 0))
                throw new ArgumentException("Either orderIds or clientOrderIds must be present.");

            if ((orderIds != null && orderIds.Count() > 0) && (clientOrderIds != null && clientOrderIds.Count() > 0))
                throw new ArgumentException("Either orderIds or clientOrderIds must be present.");

            var parameters = new Dictionary<string, object>();
            if (orderIds != null && orderIds.Count() > 0) parameters.Add("ids", orderIds);
            if (clientOrderIds != null && clientOrderIds.Count() > 0) parameters.Add("client_oids", clientOrderIds);

            return await SendRequest<OkexSwapBatchOrders>(GetUrl(Endpoints_Swap_BatchCancelOrders, symbol), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Modify an unfilled order
        /// Rate Limit：40 Requests per 2 seconds
        /// Notes
        /// - If the error_code value in response is NOT zero, then it means the request was rejected because verification failed.
        /// - In order amendment, only order_id will be used if both order_id and client_oid are passed in values at the same time, and client_oid will be ignored.
        /// - The client_oid should be unique.No warning is sent when client_oid is not unique.
        /// - In case of multiple identical client_oid, only the latest entry will be returned.
        /// - If the order cannot be modified because it has already been filled or canceled, the reason will be returned with the error message.
        /// </summary>
        /// <param name="symbol">Contract ID,e.g. BTC-USD-SWAP</param>
        /// <param name="orderId">Either client_oid or order_id must be present. Order ID。</param>
        /// <param name="clientOrderId">Either client_oid or order_id must be present. client_oid should be the same Client-supplied order ID when submitting the order. It should be comprised of alpha-numeric characters with length 1 to 32. Both uppercase and lowercase are supported.</param>
        /// <param name="requestId">You can provide the request_id. If provided, the response will include the corresponding request_id to help you identify the request. It should be comprised of alpha-numeric characters with length 1 to 32. Both uppercase and lowercase are supported.</param>
        /// <param name="newSize">Must provide at least one of new_size or new_price. When modifying a partially filled order, the new_size should include the amount that has been filled.</param>
        /// <param name="newPrice">Must provide at least one of new_size or new_price. Modifies the price.</param>
        /// <param name="cancelOnFail">When the order amendment fails, whether to cancell the order automatically: 0: Don't cancel the order automatically 1: Automatically cancel the order. The default value is 0.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<OkexSwapPlacedOrder> Swap_ModifyOrder(string symbol, long? orderId = null, string? clientOrderId = null, string? requestId = null, decimal? newSize = null, decimal? newPrice = null, bool? cancelOnFail = null, CancellationToken ct = default) => Swap_ModifyOrder_Async(symbol, orderId, clientOrderId, requestId, newSize, newPrice, cancelOnFail, ct).Result;
        /// <summary>
        /// Modify an unfilled order
        /// Rate Limit：40 Requests per 2 seconds
        /// Notes
        /// - If the error_code value in response is NOT zero, then it means the request was rejected because verification failed.
        /// - In order amendment, only order_id will be used if both order_id and client_oid are passed in values at the same time, and client_oid will be ignored.
        /// - The client_oid should be unique.No warning is sent when client_oid is not unique.
        /// - In case of multiple identical client_oid, only the latest entry will be returned.
        /// - If the order cannot be modified because it has already been filled or canceled, the reason will be returned with the error message.
        /// </summary>
        /// <param name="symbol">Contract ID,e.g. BTC-USD-SWAP</param>
        /// <param name="orderId">Either client_oid or order_id must be present. Order ID。</param>
        /// <param name="clientOrderId">Either client_oid or order_id must be present. client_oid should be the same Client-supplied order ID when submitting the order. It should be comprised of alpha-numeric characters with length 1 to 32. Both uppercase and lowercase are supported.</param>
        /// <param name="requestId">You can provide the request_id. If provided, the response will include the corresponding request_id to help you identify the request. It should be comprised of alpha-numeric characters with length 1 to 32. Both uppercase and lowercase are supported.</param>
        /// <param name="newSize">Must provide at least one of new_size or new_price. When modifying a partially filled order, the new_size should include the amount that has been filled.</param>
        /// <param name="newPrice">Must provide at least one of new_size or new_price. Modifies the price.</param>
        /// <param name="cancelOnFail">When the order amendment fails, whether to cancell the order automatically: 0: Don't cancel the order automatically 1: Automatically cancel the order. The default value is 0.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<OkexSwapPlacedOrder>> Swap_ModifyOrder_Async(string symbol, long? orderId = null, string? clientOrderId = null, string? requestId = null, decimal? newSize = null, decimal? newPrice = null, bool? cancelOnFail = null, CancellationToken ct = default)
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

            return await SendRequest<OkexSwapPlacedOrder>(GetUrl(Endpoints_Swap_ModifyOrder, symbol), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Batch modify open orders; a maximum of 10 orders per contract can be modified.
        /// Rate Limit：20 Requests per 2 seconds
        /// Notes:
        /// - When an Order ID is listed to be modified in the result list, it does not imply the order has successfully been modified. Orders in the middle of being filled cannot be modified; only unfilled orders can be modified.
        /// - If the error_code value in response is NOT zero, then it means the request was rejected because verification failed.
        /// - When using client_oid for batch order modifications, you need to make sure the ID is unique.In case of multiple identical client_oid, only the latest entry will be returned.
        /// - Modifications of orders are not guaranteed.After placing a modification order you should confirm they are successfully modified by requesting the "Order List" endpoint.
        /// </summary>
        /// <param name="symbol">Contract ID,e.g. BTC-USD-SWAP</param>
        /// <param name="orders">Orders List</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<OkexSwapBatchPlacedOrder> Swap_BatchModifyOrders(string symbol, IEnumerable<OkexSwapModifyOrder> orders, CancellationToken ct = default) => Swap_BatchModifyOrders_Async(symbol, orders, ct).Result;
        /// <summary>
        /// Batch modify open orders; a maximum of 10 orders per contract can be modified.
        /// Rate Limit：20 Requests per 2 seconds
        /// Notes:
        /// - When an Order ID is listed to be modified in the result list, it does not imply the order has successfully been modified. Orders in the middle of being filled cannot be modified; only unfilled orders can be modified.
        /// - If the error_code value in response is NOT zero, then it means the request was rejected because verification failed.
        /// - When using client_oid for batch order modifications, you need to make sure the ID is unique.In case of multiple identical client_oid, only the latest entry will be returned.
        /// - Modifications of orders are not guaranteed.After placing a modification order you should confirm they are successfully modified by requesting the "Order List" endpoint.
        /// </summary>
        /// <param name="symbol">Contract ID,e.g. BTC-USD-SWAP</param>
        /// <param name="orders">Orders List</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<OkexSwapBatchPlacedOrder>> Swap_BatchModifyOrders_Async(string symbol, IEnumerable<OkexSwapModifyOrder> orders, CancellationToken ct = default)
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

            return await SendRequest<OkexSwapBatchPlacedOrder>(GetUrl(Endpoints_Swap_BatchModifyOrder, symbol), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// This retrieves the latest 20,000 entries of your orders from the last 7 days. This request supports paging and is stored according to the order time in chronological order from latest to earliest.
        /// Rate limit: 10 requests per 2 seconds
        /// Notes
        /// - status is the older version of state and both can be used interchangeably in the short term.It is recommended to switch to state
        /// - The client_oid is optional.It should be a unique ID generated by your trading system.This parameter is used to identify your orders in the public orders feed.No warning is sent when client_oid is not unique.
        /// - In case of multiple identical client_oid, only the latest entry will be returned.
        /// - If the order is not filled in the order life cycle, the record may be removed.
        /// - The state of unfilled orders may change during the time of endpoint request and response, depending on the market condition.
        /// </summary>
        /// <param name="symbol">Contract ID, e.g. BTC-USD-SWAP,BTC-USDT-SWAP</param>
        /// <param name="state">Order Status: -2 = Failed -1 = Canceled 0 = Open 1 = Partially Filled 2 = Fully Filled 3 = Submitting 4 = Canceling 6 = Incomplete (open + partially filled) 7 = Complete (canceled + fully filled)</param>
        /// <param name="limit">Number of results per request. The maximum is 100; the default is 100</param>
        /// <param name="before">Pagination of data to return records new than the requested order_id.</param>
        /// <param name="after">Pagination of data to return records earlier than the requested order_id.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<OkexSwapOrderList> Swap_GetAllOrders(string symbol, OkexSwapOrderState state, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default) => Swap_GetAllOrders_Async(symbol, state, limit, before, after, ct).Result;
        /// <summary>
        /// This retrieves the latest 20,000 entries of your orders from the last 7 days. This request supports paging and is stored according to the order time in chronological order from latest to earliest.
        /// Rate limit: 10 requests per 2 seconds
        /// Notes
        /// - status is the older version of state and both can be used interchangeably in the short term.It is recommended to switch to state
        /// - The client_oid is optional.It should be a unique ID generated by your trading system.This parameter is used to identify your orders in the public orders feed.No warning is sent when client_oid is not unique.
        /// - In case of multiple identical client_oid, only the latest entry will be returned.
        /// - If the order is not filled in the order life cycle, the record may be removed.
        /// - The state of unfilled orders may change during the time of endpoint request and response, depending on the market condition.
        /// </summary>
        /// <param name="symbol">Contract ID, e.g. BTC-USD-SWAP,BTC-USDT-SWAP</param>
        /// <param name="state">Order Status: -2 = Failed -1 = Canceled 0 = Open 1 = Partially Filled 2 = Fully Filled 3 = Submitting 4 = Canceling 6 = Incomplete (open + partially filled) 7 = Complete (canceled + fully filled)</param>
        /// <param name="limit">Number of results per request. The maximum is 100; the default is 100</param>
        /// <param name="before">Pagination of data to return records new than the requested order_id.</param>
        /// <param name="after">Pagination of data to return records earlier than the requested order_id.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<OkexSwapOrderList>> Swap_GetAllOrders_Async(string symbol, OkexSwapOrderState state, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default)
        {
            symbol = symbol.ValidateSymbol();
            limit.ValidateIntBetween(nameof(limit), 1, 100);

            var parameters = new Dictionary<string, object>
            {
                { "state", JsonConvert.SerializeObject(state, new SwapOrderStateConverter(false)) },
                { "limit", limit },
            };
            parameters.AddOptionalParameter("before", before);
            parameters.AddOptionalParameter("after", after);

            return await SendRequest<OkexSwapOrderList>(GetUrl(Endpoints_Swap_OrderList, symbol), HttpMethod.Get, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve order details by order ID.This interface can only query the information of the completed and cancelled orders in the last 7 days. Unfilled orders will be kept in record for only 2 hour after it is canceled.
        /// Rate limit: 10 requests per 2 seconds
        /// Notes
        /// - status is the older version ofstateand both can be used interchangeably in the short term.It is recommended to switch tostate`.
        /// - The client_oid is optional.It should be a unique ID generated by your trading system.This parameter is used to identify your orders in the public orders feed.No warning is sent when client_oid is not unique.
        /// - In case of multiple identical client_oid, only the latest entry will be returned.
        /// - If the order is not filled in the order life cycle, the record may be removed.
        /// - Unfilled order status may change according to the market conditions.
        /// </summary>
        /// <param name="symbol">Contract ID, e.g. BTC-USD-SWAP,BTC-USDT-SWAP</param>
        /// <param name="orderId">Order ID Either client_oid or order_id must be present.</param>
        /// <param name="clientOrderId">Client-supplied order ID Either client_oid or order_id must be present.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<OkexSwapOrder> Swap_GetOrderDetails(string symbol, long? orderId = null, string? clientOrderId = null, CancellationToken ct = default) => Swap_GetOrderDetails_Async(symbol, orderId, clientOrderId, ct).Result;
        /// <summary>
        /// Retrieve order details by order ID.This interface can only query the information of the completed and cancelled orders in the last 7 days. Unfilled orders will be kept in record for only 2 hour after it is canceled.
        /// Rate limit: 10 requests per 2 seconds
        /// Notes
        /// - status is the older version ofstateand both can be used interchangeably in the short term.It is recommended to switch tostate`.
        /// - The client_oid is optional.It should be a unique ID generated by your trading system.This parameter is used to identify your orders in the public orders feed.No warning is sent when client_oid is not unique.
        /// - In case of multiple identical client_oid, only the latest entry will be returned.
        /// - If the order is not filled in the order life cycle, the record may be removed.
        /// - Unfilled order status may change according to the market conditions.
        /// </summary>
        /// <param name="symbol">Contract ID, e.g. BTC-USD-SWAP,BTC-USDT-SWAP</param>
        /// <param name="orderId">Order ID Either client_oid or order_id must be present.</param>
        /// <param name="clientOrderId">Client-supplied order ID Either client_oid or order_id must be present.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<OkexSwapOrder>> Swap_GetOrderDetails_Async(string symbol, long? orderId = null, string? clientOrderId = null, CancellationToken ct = default)
        {
            symbol = symbol.ValidateSymbol();

            if (orderId == null && string.IsNullOrEmpty(clientOrderId))
                throw new ArgumentException("Either orderId or clientOrderId must be present.");

            if (orderId != null && !string.IsNullOrEmpty(clientOrderId))
                throw new ArgumentException("Either orderId or clientOrderId must be present.");

            return await SendRequest<OkexSwapOrder>(GetUrl(Endpoints_Swap_OrderDetails.Replace("<instrument_id>", symbol), orderId.HasValue ? orderId.ToString() : clientOrderId!), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve recently filled transaction details. Pagination is supported and the response is sorted with most recent first in reverse chronological order. Data from the past 7 days can be retrieved.
        /// Rate limit: 10 requests per 2 seconds
        /// Notes:
        /// - New status for transaction details: "fee" is either a positive number (invitation rebate) or a negative number (transaction fee deduction)
        /// </summary>
        /// <param name="symbol">Contract ID, e.g. BTC-USD-SWAP,BTC-USDT-SWAP</param>
        /// <param name="orderId">Order ID, Complete transaction details for will be returned if the instrument_id is left blank</param>
        /// <param name="limit">Number of results per request. The maximum is 100; the default is 100</param>
        /// <param name="before">Pagination of data to return records newer than the requested trade_id.</param>
        /// <param name="after">Pagination of data to return records earlier than the requested trade_id.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<IEnumerable<OkexSwapTransaction>> Swap_GetTransactionDetails(string symbol, long? orderId = null, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default) => Swap_GetTransactionDetails_Async(symbol, orderId, limit, before, after, ct).Result;
        /// <summary>
        /// Retrieve recently filled transaction details. Pagination is supported and the response is sorted with most recent first in reverse chronological order. Data from the past 7 days can be retrieved.
        /// Rate limit: 10 requests per 2 seconds
        /// Notes:
        /// - New status for transaction details: "fee" is either a positive number (invitation rebate) or a negative number (transaction fee deduction)
        /// </summary>
        /// <param name="symbol">Contract ID, e.g. BTC-USD-SWAP,BTC-USDT-SWAP</param>
        /// <param name="orderId">Order ID, Complete transaction details for will be returned if the instrument_id is left blank</param>
        /// <param name="limit">Number of results per request. The maximum is 100; the default is 100</param>
        /// <param name="before">Pagination of data to return records newer than the requested trade_id.</param>
        /// <param name="after">Pagination of data to return records earlier than the requested trade_id.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<OkexSwapTransaction>>> Swap_GetTransactionDetails_Async(string symbol, long? orderId = null, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default)
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

            return await SendRequest<IEnumerable<OkexSwapTransaction>>(GetUrl(Endpoints_Swap_TransactionDetails), HttpMethod.Get, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Get On Hold Amount for Open Orders.
        /// Rate limit：55 requests per 2 seconds
        /// Notes:
        /// - Contract name will be verified. If the name differs from database, error code will be triggered.
        /// </summary>
        /// <param name="symbol">Contract ID, e.g. BTC-USD-SWAP,BTC-USDT-SWAP</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<OkexSwapHoldAmount> Swap_GetHoldAmount(string symbol, CancellationToken ct = default) => Swap_GetHoldAmount_Async(symbol, ct).Result;
        /// <summary>
        /// Get On Hold Amount for Open Orders.
        /// Rate limit：55 requests per 2 seconds
        /// Notes:
        /// - Contract name will be verified. If the name differs from database, error code will be triggered.
        /// </summary>
        /// <param name="symbol">Contract ID, e.g. BTC-USD-SWAP,BTC-USDT-SWAP</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<OkexSwapHoldAmount>> Swap_GetHoldAmount_Async(string symbol, CancellationToken ct = default)
        {
            symbol = symbol.ValidateSymbol();

            return await base.SendRequest<OkexSwapHoldAmount>(GetUrl(Endpoints_Swap_HoldAmount, symbol), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Obtain the transaction fee rate corresponding to your current account transaction level. The sub-account rate under the parent account is the same as the parent account. Update every day at 0am
        /// The fee rate returned by this interface is only applicable to(BTC,ETH,EOS,BSV,BCH,LTC,ETC,XRP,TRX)，Rates in other currencies, please confirm based on your fee level inquiry rate description page (https://www.okex.com/fees.html)
        /// Rate limit: 20 requests per 2 seconds
        /// Notes:
        /// - Remarks: The value of maker: negative number, which means the rate of counter commission, and positive number, which means the rate deducted by the platform. (As shown on the web page)
        /// </summary>
        /// <param name="symbol">contract ID，eg：BTC-USD-SWAP ；Choose and enter one parameter between category and instrument_id</param>
        /// <param name="category">Fee Schedule Tier: 1：Tier 1; 2：Tier 2;4：Tier 4 Choose and enter one parameter between category and instrument_id</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<OkexSwapTradeFee> Swap_GetTradeFeeRates(string? symbol = null, int? category = null, CancellationToken ct = default) => Swap_GetTradeFeeRates_Async(symbol, category, ct).Result;
        /// <summary>
        /// Obtain the transaction fee rate corresponding to your current account transaction level. The sub-account rate under the parent account is the same as the parent account. Update every day at 0am
        /// The fee rate returned by this interface is only applicable to(BTC,ETH,EOS,BSV,BCH,LTC,ETC,XRP,TRX)，Rates in other currencies, please confirm based on your fee level inquiry rate description page (https://www.okex.com/fees.html)
        /// Rate limit: 20 requests per 2 seconds
        /// Notes:
        /// - Remarks: The value of maker: negative number, which means the rate of counter commission, and positive number, which means the rate deducted by the platform. (As shown on the web page)
        /// </summary>
        /// <param name="symbol">contract ID，eg：BTC-USD-SWAP ；Choose and enter one parameter between category and instrument_id</param>
        /// <param name="category">Fee Schedule Tier: 1：Tier 1; 2：Tier 2;4：Tier 4 Choose and enter one parameter between category and instrument_id</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<OkexSwapTradeFee>> Swap_GetTradeFeeRates_Async(string? symbol = null, int? category = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("instrument_id", symbol);
            parameters.AddOptionalParameter("category", category);

            return await SendRequest<OkexSwapTradeFee>(GetUrl(Endpoints_Swap_AccountTierRate), HttpMethod.Get, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Close all positions via market order. This endpoint is only available when user's position is less or equal than 999 contracts size for BTC, otherwise it will return error. Similarly the position should be less or equal than 9,999 contracts size for other assets.
        /// Rate Limit: 2 requests per 2 seconds (Depending on the underlying speed limit)
        /// </summary>
        /// <param name="symbol">Contract ID, e.g.BTC-USD-SWAP</param>
        /// <param name="direction">Side (long or short)</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<OkexSwapDirectionResponse> Swap_MarketCloseAll(string symbol, OkexSwapDirection direction, CancellationToken ct = default) => Swap_MarketCloseAll_Async(symbol, direction, ct).Result;
        /// <summary>
        /// Close all positions via market order. This endpoint is only available when user's position is less or equal than 999 contracts size for BTC, otherwise it will return error. Similarly the position should be less or equal than 9,999 contracts size for other assets.
        /// Rate Limit: 2 requests per 2 seconds (Depending on the underlying speed limit)
        /// </summary>
        /// <param name="symbol">Contract ID, e.g.BTC-USD-SWAP</param>
        /// <param name="direction">Side (long or short)</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<OkexSwapDirectionResponse>> Swap_MarketCloseAll_Async(string symbol, OkexSwapDirection direction, CancellationToken ct = default)
        {
            symbol = symbol.ValidateSymbol();
            var parameters = new Dictionary<string, object>
            {
                { "instrument_id", symbol },
                { "direction", JsonConvert.SerializeObject(direction, new SwapDirectionConverter(false)) },
            };

            return await base.SendRequest<OkexSwapDirectionResponse>(GetUrl(Endpoints_Swap_CloseAll), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Cancel all the outstanding orders which type equal 3 (close long) or 4 (close short).
        /// Rate Limit: 5 requests per 2 seconds
        /// </summary>
        /// <param name="symbol">Contract ID, e.g.BTC-USD-SWAP</param>
        /// <param name="direction">side (long or short)</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<OkexSwapDirectionResponse> Swap_CancelAll(string symbol, OkexSwapDirection direction, CancellationToken ct = default) => Swap_CancelAll_Async(symbol, direction, ct).Result;
        /// <summary>
        /// Cancel all the outstanding orders which type equal 3 (close long) or 4 (close short).
        /// Rate Limit: 5 requests per 2 seconds
        /// </summary>
        /// <param name="symbol">Contract ID, e.g.BTC-USD-SWAP</param>
        /// <param name="direction">side (long or short)</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<OkexSwapDirectionResponse>> Swap_CancelAll_Async(string symbol, OkexSwapDirection direction, CancellationToken ct = default)
        {
            symbol = symbol.ValidateSymbol();
            var parameters = new Dictionary<string, object>
            {
                { "instrument_id", symbol },
                { "direction", JsonConvert.SerializeObject(direction, new SwapDirectionConverter(false)) },
            };

            return await base.SendRequest<OkexSwapDirectionResponse>(GetUrl(Endpoints_Swap_CancelAll), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
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
        public WebCallResult<OkexSwapAlgoPlacedOrder> Swap_AlgoPlaceOrder(
            /* General Parameters */
            string symbol,
            OkexSwapOrderType type,
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
            => Swap_AlgoPlaceOrder_Async(
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
        public async Task<WebCallResult<OkexSwapAlgoPlacedOrder>> Swap_AlgoPlaceOrder_Async(
            /* General Parameters */
            string symbol,
            OkexSwapOrderType type,
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
                { "type", JsonConvert.SerializeObject(order_type, new SwapOrderTypeConverter(false)) },
                { "order_type", JsonConvert.SerializeObject(order_type, new AlgoOrderTypeConverter(false)) },
                { "size", size },
            };

            if (order_type == OkexAlgoOrderType.TriggerOrder)
            {
                if (trigger_price == null) throw new ArgumentException("trigger_price is mandatory for Trigger Order");
                if (trigger_algo_price == null) throw new ArgumentException("trigger_algo_price is mandatory for Trigger Order");
                // if(trigger_algo_type == null) throw new ArgumentException("trigger_algo_type is mandatory for Trigger Order");

                parameters.AddParameter("trigger_price", trigger_price);
                parameters.AddParameter("algo_price", trigger_algo_price);
                parameters.AddOptionalParameter("algo_type", JsonConvert.SerializeObject(trigger_algo_type, new AlgoPriceTypeConverter(false)));
            }

            else if (order_type == OkexAlgoOrderType.TrailOrder)
            {
                if (trail_callback_rate == null) throw new ArgumentException("trail_callback_rate is mandatory for Trail Order");
                if (trail_trigger_price == null) throw new ArgumentException("trail_trigger_price is mandatory for Trail Order");

                parameters.AddParameter("callback_rate", trail_callback_rate);
                parameters.AddParameter("trigger_price", trail_trigger_price);
            }

            else if (order_type == OkexAlgoOrderType.IcebergOrder)
            {
                if (iceberg_algo_variance == null) throw new ArgumentException("iceberg_algo_variance is mandatory for Iceberg Order");
                if (iceberg_avg_amount == null) throw new ArgumentException("iceberg_avg_amount is mandatory for Iceberg Order");
                if (iceberg_limit_price == null) throw new ArgumentException("iceberg_limit_price is mandatory for Iceberg Order");

                parameters.AddParameter("algo_variance", iceberg_algo_variance);
                parameters.AddParameter("avg_amount", iceberg_avg_amount);
                parameters.AddParameter("limit_price", iceberg_limit_price);
            }

            else if (order_type == OkexAlgoOrderType.TWAP)
            {
                if (twap_sweep_range == null) throw new ArgumentException("twap_sweep_range is mandatory for TWAP Order");
                if (twap_sweep_ratio == null) throw new ArgumentException("twap_sweep_ratio is mandatory for TWAP Order");
                if (twap_single_limit == null) throw new ArgumentException("twap_single_limit is mandatory for TWAP Order");
                if (twap_price_limit == null) throw new ArgumentException("twap_price_limit is mandatory for TWAP Order");
                if (twap_time_interval == null) throw new ArgumentException("twap_time_interval is mandatory for TWAP Order");

                parameters.AddParameter("sweep_range", twap_sweep_range);
                parameters.AddParameter("sweep_ratio", twap_sweep_ratio);
                parameters.AddParameter("single_limit", twap_single_limit);
                parameters.AddParameter("price_limit", twap_price_limit);
                parameters.AddParameter("time_interval", twap_time_interval);
            }

            else if (order_type == OkexAlgoOrderType.StopOrder)
            {
                //if(tp_trigger_type == null) throw new ArgumentException("tp_trigger_type is mandatory for Stop Order");
                //if(tp_trigger_price == null) throw new ArgumentException("tp_trigger_price is mandatory for Stop Order");
                //if(tp_price == null) throw new ArgumentException("tp_price is mandatory for Stop Order");
                //if(sl_trigger_type == null) throw new ArgumentException("sl_trigger_type is mandatory for Stop Order");
                //if(sl_trigger_price == null) throw new ArgumentException("sl_trigger_price is mandatory for Stop Order");
                //if(sl_price == null) throw new ArgumentException("sl_price is mandatory for Stop Order");

                parameters.AddOptionalParameter("tp_trigger_type", tp_trigger_type);
                parameters.AddOptionalParameter("tp_trigger_price", tp_trigger_price);
                parameters.AddOptionalParameter("tp_price", tp_price);
                parameters.AddOptionalParameter("sl_trigger_type", sl_trigger_type);
                parameters.AddOptionalParameter("sl_trigger_price", sl_trigger_price);
                parameters.AddOptionalParameter("sl_price", sl_price);
            }

            return await SendRequest<OkexSwapAlgoPlacedOrder>(GetUrl(Endpoints_Swap_AlgoPlaceOrder), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
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
        public WebCallResult<OkexSwapAlgoCancelledOrder> Swap_AlgoCancelOrder(string symbol, OkexAlgoOrderType type, IEnumerable<long> algo_ids, CancellationToken ct = default) => Swap_AlgoCancelOrder_Async(symbol, type, algo_ids, ct).Result;
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
        public async Task<WebCallResult<OkexSwapAlgoCancelledOrder>> Swap_AlgoCancelOrder_Async(string symbol, OkexAlgoOrderType type, IEnumerable<long> algo_ids, CancellationToken ct = default)
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

            return await SendRequest<OkexSwapAlgoCancelledOrder>(GetUrl(Endpoints_Swap_AlgoCancelOrders), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
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
        /// <returns>Symbol grouped algo orders list. Dictionary&lt;string: symbol, IEnumerable&lt;OkexSwapAlgoOrder&gt;: algo orders&gt;</returns>
        /// <returns></returns>
        public WebCallResult<IEnumerable<OkexSwapAlgoOrder>> Swap_AlgoGetOrders(string symbol, OkexAlgoOrderType type, OkexAlgoStatus? status = null, IEnumerable<long> algo_ids = null, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default) => Swap_AlgoGetOrders_Async(symbol, type, status, algo_ids, limit, before, after, ct).Result;
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
        /// <returns>Symbol grouped algo orders list. Dictionary&lt;string: symbol, IEnumerable&lt;OkexSwapAlgoOrder&gt;: algo orders&gt;</returns>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<OkexSwapAlgoOrder>>> Swap_AlgoGetOrders_Async(string symbol, OkexAlgoOrderType type, OkexAlgoStatus? status = null, IEnumerable<long> algo_ids = null, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default)
        {
            symbol = symbol.ValidateSymbol();
            limit.ValidateIntBetween(nameof(limit), 1, 100);

            if (status == null && (algo_ids == null || algo_ids.Count() == 0))
                throw new ArgumentException("status and algo_ids are mandatory, select either one");

            var parameters = new Dictionary<string, object>
            {
                { "order_type", JsonConvert.SerializeObject(type, new AlgoOrderTypeConverter(false)) },
                { "limit", limit },
            };
            parameters.AddOptionalParameter("status", JsonConvert.SerializeObject(status, new AlgoStatusConverter(false)));
            parameters.AddOptionalParameter("algo_id", algo_ids);
            parameters.AddOptionalParameter("before", before);
            parameters.AddOptionalParameter("after", after);

            return await SendRequest<IEnumerable<OkexSwapAlgoOrder>>(GetUrl(Endpoints_Swap_AlgoOrderList, symbol), HttpMethod.Get, ct, parameters, signed: true).ConfigureAwait(false);
        }
#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

        #endregion

        #region Public Unsigned Endpoints

        /// <summary>
        /// Get market data.
        /// Rate limit：20 requests per 2 seconds
        /// </summary>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<IEnumerable<OkexSwapContract>> Swap_GetTradingContracts(CancellationToken ct = default) => Swap_GetTradingContracts_Async(ct).Result;
        /// <summary>
        /// Get market data.
        /// Rate limit：20 requests per 2 seconds
        /// </summary>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<OkexSwapContract>>> Swap_GetTradingContracts_Async(CancellationToken ct = default)
        {
            return await SendRequest<IEnumerable<OkexSwapContract>>(GetUrl(Endpoints_Swap_TradingContracts), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve a trading pair's order book. Pagination is not supported here; the entire orderbook will be returned per request. This is publicly accessible without account authentication. WebSocket is recommended here.
        /// Rate limit: 20 requests per 2 seconds
        /// Notes:
        /// Notes on the array returned
        /// - If the size is left blank, one String of data will be returned. If size is '0', no data String will be returned. If size is larger than '200', only 200 Strings of data will be returned.
        /// - ["411.8", "10", "1", "4"]: 411.8 is the price; 10 is the contract size at the price; 1 is the quantity of the liquidated orders at the price; 4 is the number of orders at the price.
        /// </summary>
        /// <param name="symbol">Contract ID, e.g. BTC-USD-SWAP,BTC-USDT-SWAP</param>
        /// <param name="size">Number of results per request. Maximum 200</param>
        /// <param name="depth">Merge depth by price, for e.g: 0.1,0.001</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<OkexSwapOrderBook> Swap_GetOrderBook(string symbol, int? size = null, decimal? depth = null, CancellationToken ct = default) => Swap_GetOrderBook_Async(symbol, size, depth, ct).Result;
        /// <summary>
        /// Retrieve a trading pair's order book. Pagination is not supported here; the entire orderbook will be returned per request. This is publicly accessible without account authentication. WebSocket is recommended here.
        /// Rate limit: 20 requests per 2 seconds
        /// Notes:
        /// Notes on the array returned
        /// - If the size is left blank, one String of data will be returned. If size is '0', no data String will be returned. If size is larger than '200', only 200 Strings of data will be returned.
        /// - ["411.8", "10", "1", "4"]: 411.8 is the price; 10 is the contract size at the price; 1 is the quantity of the liquidated orders at the price; 4 is the number of orders at the price.
        /// </summary>
        /// <param name="symbol">Contract ID, e.g. BTC-USD-SWAP,BTC-USDT-SWAP</param>
        /// <param name="size">Number of results per request. Maximum 200</param>
        /// <param name="depth">Merge depth by price, for e.g: 0.1,0.001</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<OkexSwapOrderBook>> Swap_GetOrderBook_Async(string symbol, int? size = null, decimal? depth = null, CancellationToken ct = default)
        {
            symbol = symbol.ValidateSymbol();
            size?.ValidateIntBetween(nameof(size), 1, 200);

            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("size", size);
            parameters.AddOptionalParameter("depth", depth);

            return await SendRequest<OkexSwapOrderBook>(GetUrl(Endpoints_Swap_OrderBook, symbol), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve the latest price snapshot, best bid/ask price, and trading volume in the last 24 hours for all trading pairs. This is publicly accessible without account authentication.
        /// Rate limit: 20 requests per 2 seconds
        /// Notes:
        /// - The parameters for highest price, lowest price and trading volume are all computed based on the data in the last 24 hours.
        /// </summary>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<IEnumerable<OkexSwapTicker>> Swap_GetAllTickers(CancellationToken ct = default) => Swap_GetAllTickers_Async(ct).Result;
        /// <summary>
        /// Retrieve the latest price snapshot, best bid/ask price, and trading volume in the last 24 hours for all trading pairs. This is publicly accessible without account authentication.
        /// Rate limit: 20 requests per 2 seconds
        /// Notes:
        /// - The parameters for highest price, lowest price and trading volume are all computed based on the data in the last 24 hours.
        /// </summary>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<OkexSwapTicker>>> Swap_GetAllTickers_Async(CancellationToken ct = default)
        {
            return await SendRequest<IEnumerable<OkexSwapTicker>>(GetUrl(Endpoints_Swap_Ticker), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve the latest price snapshot, best bid/ask price, and trading volume in the last 24 hours for a particular trading pair. This is publicly accessible without account authentication.
        /// Rate limit: 20 requests per 2 seconds
        /// Notes:
        /// - The parameters for highest price, lowest price and trading volume are all computed based on the data in the last 24 hours.
        /// </summary>
        /// <param name="symbol">Contract ID, e.g. BTC-USD-SWAP,BTC-USDT-SWAP</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<OkexSwapTicker> Swap_GetSymbolTicker(string symbol, CancellationToken ct = default) => Swap_GetSymbolTicker_Async(symbol, ct).Result;
        /// <summary>
        /// Retrieve the latest price snapshot, best bid/ask price, and trading volume in the last 24 hours for a particular trading pair. This is publicly accessible without account authentication.
        /// Rate limit: 20 requests per 2 seconds
        /// Notes:
        /// - The parameters for highest price, lowest price and trading volume are all computed based on the data in the last 24 hours.
        /// </summary>
        /// <param name="symbol">Contract ID, e.g. BTC-USD-SWAP,BTC-USDT-SWAP</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<OkexSwapTicker>> Swap_GetSymbolTicker_Async(string symbol, CancellationToken ct = default)
        {
            symbol = symbol.ValidateSymbol();
            return await SendRequest<OkexSwapTicker>(GetUrl(Endpoints_Swap_TickerOfSymbol, symbol), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve the latest 300 entries of filled orders. This is publicly accessible without account authentication.
        /// Rate limit: 20 requests per 2 seconds
        /// Notes:
        /// - The side indicates the side of the order that is filled by the taker.The "taker" means actively taking the order on the order book.The buy indicates the taker is taking liquidity from the order book, so the price rises as a result, whereas the sell indicates the price falls as a result.
        /// - The trade_id is the ID referring to the filled order; it is generated incrementally and could be incomplete in some cases.
        /// </summary>
        /// <param name="symbol">Contract ID, e.g. BTC-USD-SWAP,BTC-USDT-SWAP</param>
        /// <param name="limit">Number of results per request. The maximum is 100; the default is 100</param>
        /// <param name="before">Pagination of data to return records newer than the requested trade_id.</param>
        /// <param name="after">Pagination of data to return records earlier than the requested trade_id.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<IEnumerable<OkexSwapTrade>> Swap_GetTrades(string symbol, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default) => Swap_GetTrades_Async(symbol, limit, before, after, ct).Result;
        /// <summary>
        /// Retrieve the latest 300 entries of filled orders. This is publicly accessible without account authentication.
        /// Rate limit: 20 requests per 2 seconds
        /// Notes:
        /// - The side indicates the side of the order that is filled by the taker.The "taker" means actively taking the order on the order book.The buy indicates the taker is taking liquidity from the order book, so the price rises as a result, whereas the sell indicates the price falls as a result.
        /// - The trade_id is the ID referring to the filled order; it is generated incrementally and could be incomplete in some cases.
        /// </summary>
        /// <param name="symbol">Contract ID, e.g. BTC-USD-SWAP,BTC-USDT-SWAP</param>
        /// <param name="limit">Number of results per request. The maximum is 100; the default is 100</param>
        /// <param name="before">Pagination of data to return records newer than the requested trade_id.</param>
        /// <param name="after">Pagination of data to return records earlier than the requested trade_id.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<OkexSwapTrade>>> Swap_GetTrades_Async(string symbol, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default)
        {
            symbol = symbol.ValidateSymbol();
            limit.ValidateIntBetween(nameof(limit), 1, 100);

            var parameters = new Dictionary<string, object>
            {
                { "limit", limit },
            };
            parameters.AddOptionalParameter("before", before);
            parameters.AddOptionalParameter("after", after);

            return await SendRequest<IEnumerable<OkexSwapTrade>>(GetUrl(Endpoints_Swap_Trades, symbol), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve the candlestick charts of the trading pairs. This API can retrieve the latest 1440 entries of data. Candlesticks are returned in groups based on requested granularity.
        /// Rate limit: 20 requests per 2 seconds
        /// Notes:
        /// - Timestamp must be in ISO 8601 format, otherwise an error be returned.
        /// - Both parameters will be ignored if either one of start or end are not provided. The last 200 records of data will be returned if the time range is not specified in the request.
        /// - The granularity field must be one of the following values: [60, 180, 300, 900, 1800, 3600, 7200, 14400, 21600, 43200, 86400, 604800, 2678400, 8035200, 16070400, 31536000]. Otherwise, your request will be rejected.These values correspond to timeslices representing[1 minute, 3 minutes, 5 minutes, 15 minutes, 30 minutes, 1 hour, 2 hours, 4 hours, 6 hours, 12 hours, 1 day, 1 week, 1 month, 3 months, 6 months and 1 year] respectively.
        /// - The candlestick data may be incomplete, and should not be polled repeatedly.
        /// - The maximum data set is 200 candles for a single request.If the request made with the parameters start, end and granularity will result in more data than that is allowed, only 200 candles will be returned.If finer granularity over a larger time range is needed, please make multiple requests as needed.
        /// - The data returned will be arranged in an array as below: [timestamp, open, high, low, close, volume, currency_volume]
        /// - [timestamp,open,high,low,close,volume,currency_volume]
        /// </summary>
        /// <param name="symbol">Contract ID, e.g. BTC-USD-SWAP,BTC-USDT-SWAP</param>
        /// <param name="period">Bar size in seconds, default 60, must be one of [60/180/300/900/1800/3600/7200/14400/21600/43200/86400/604800] otherwise returns error</param>
        /// <param name="start">Start time in ISO 8601</param>
        /// <param name="end">End time in ISO 8601</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<IEnumerable<OkexSwapCandle>> Swap_GetCandles(string symbol, OkexSpotPeriod period, DateTime? start = null, DateTime? end = null, CancellationToken ct = default) => Swap_GetCandles_Async(symbol, period, start, end, ct).Result;
        /// <summary>
        /// Retrieve the candlestick charts of the trading pairs. This API can retrieve the latest 1440 entries of data. Candlesticks are returned in groups based on requested granularity.
        /// Rate limit: 20 requests per 2 seconds
        /// Notes:
        /// - Timestamp must be in ISO 8601 format, otherwise an error be returned.
        /// - Both parameters will be ignored if either one of start or end are not provided. The last 200 records of data will be returned if the time range is not specified in the request.
        /// - The granularity field must be one of the following values: [60, 180, 300, 900, 1800, 3600, 7200, 14400, 21600, 43200, 86400, 604800, 2678400, 8035200, 16070400, 31536000]. Otherwise, your request will be rejected.These values correspond to timeslices representing[1 minute, 3 minutes, 5 minutes, 15 minutes, 30 minutes, 1 hour, 2 hours, 4 hours, 6 hours, 12 hours, 1 day, 1 week, 1 month, 3 months, 6 months and 1 year] respectively.
        /// - The candlestick data may be incomplete, and should not be polled repeatedly.
        /// - The maximum data set is 200 candles for a single request.If the request made with the parameters start, end and granularity will result in more data than that is allowed, only 200 candles will be returned.If finer granularity over a larger time range is needed, please make multiple requests as needed.
        /// - The data returned will be arranged in an array as below: [timestamp, open, high, low, close, volume, currency_volume]
        /// - [timestamp,open,high,low,close,volume,currency_volume]
        /// </summary>
        /// <param name="symbol">Contract ID, e.g. BTC-USD-SWAP,BTC-USDT-SWAP</param>
        /// <param name="period">Bar size in seconds, default 60, must be one of [60/180/300/900/1800/3600/7200/14400/21600/43200/86400/604800] otherwise returns error</param>
        /// <param name="start">Start time in ISO 8601</param>
        /// <param name="end">End time in ISO 8601</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<OkexSwapCandle>>> Swap_GetCandles_Async(string symbol, OkexSpotPeriod period, DateTime? start = null, DateTime? end = null, CancellationToken ct = default)
        {
            symbol = symbol.ValidateSymbol();

            var parameters = new Dictionary<string, object>
            {
                { "granularity", JsonConvert.SerializeObject(period, new SpotPeriodConverter(false)) },
            };
            parameters.AddOptionalParameter("start", start?.DateTimeToIso8601String());
            parameters.AddOptionalParameter("end", end?.DateTimeToIso8601String());

            return await SendRequest<IEnumerable<OkexSwapCandle>>(GetUrl(Endpoints_Swap_Candles, symbol), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve indices of tokens. This is publicly accessible without account authentication.
        /// Rate Limit: 20 requests per 2 seconds
        /// Notes:
        /// - The token displayed after the hyphen "-" is the quote currency of the index.
        /// </summary>
        /// <param name="symbol">Contract ID, e.g. BTC-USD-SWAP,BTC-USDT-SWAP</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<OkexSwapIndex> Swap_GetIndices(string symbol, CancellationToken ct = default) => Swap_GetIndices_Async(symbol, ct).Result;
        /// <summary>
        /// Retrieve indices of tokens. This is publicly accessible without account authentication.
        /// Rate Limit: 20 requests per 2 seconds
        /// Notes:
        /// - The token displayed after the hyphen "-" is the quote currency of the index.
        /// </summary>
        /// <param name="symbol">Contract ID, e.g. BTC-USD-SWAP,BTC-USDT-SWAP</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<OkexSwapIndex>> Swap_GetIndices_Async(string symbol, CancellationToken ct = default)
        {
            symbol = symbol.ValidateSymbol();

            return await SendRequest<OkexSwapIndex>(GetUrl(Endpoints_Swap_Indices, symbol), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve the fiat exchange rates. This is publicly accessible without account authentication.
        /// Rate limit: 20 requests per 2 seconds
        /// </summary>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<OkexSwapRate> Swap_GetFiatExchangeRates(CancellationToken ct = default) => Swap_GetFiatExchangeRates_Async(ct).Result;
        /// <summary>
        /// Retrieve the fiat exchange rates. This is publicly accessible without account authentication.
        /// Rate limit: 20 requests per 2 seconds
        /// </summary>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<OkexSwapRate>> Swap_GetFiatExchangeRates_Async(CancellationToken ct = default)
        {
            return await SendRequest<OkexSwapRate>(GetUrl(Endpoints_Swap_ExchangeRates), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve the total open interest of a contract on OKEx. This is publicly accessible without account authentication.
        /// Rate limit: 20 requests per 2 seconds
        /// </summary>
        /// <param name="symbol">Contract ID, e.g. BTC-USD-SWAP,BTC-USDT-SWAP</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<OkexSwapInterest> Swap_GetOpenInterests(string symbol, CancellationToken ct = default) => Swap_GetOpenInterests_Async(symbol, ct).Result;
        /// <summary>
        /// Retrieve the total open interest of a contract on OKEx. This is publicly accessible without account authentication.
        /// Rate limit: 20 requests per 2 seconds
        /// </summary>
        /// <param name="symbol">Contract ID, e.g. BTC-USD-SWAP,BTC-USDT-SWAP</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<OkexSwapInterest>> Swap_GetOpenInterests_Async(string symbol, CancellationToken ct = default)
        {
            symbol = symbol.ValidateSymbol();

            return await SendRequest<OkexSwapInterest>(GetUrl(Endpoints_Swap_OpenInterest, symbol), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve the ceiling of the buy price and floor of sell price of the contract. This is publicly accessible without account authentication.
        /// Rate limit: 20 requests per 2 seconds
        /// </summary>
        /// <param name="symbol">	Contract ID, e.g. BTC-USD-SWAP,BTC-USDT-SWAP</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<OkexSwapPriceRange> Swap_GetPriceLimit(string symbol, CancellationToken ct = default) => Swap_GetPriceLimit_Async(symbol, ct).Result;
        /// <summary>
        /// Retrieve the ceiling of the buy price and floor of sell price of the contract. This is publicly accessible without account authentication.
        /// Rate limit: 20 requests per 2 seconds
        /// </summary>
        /// <param name="symbol">	Contract ID, e.g. BTC-USD-SWAP,BTC-USDT-SWAP</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<OkexSwapPriceRange>> Swap_GetPriceLimit_Async(string symbol, CancellationToken ct = default)
        {
            symbol = symbol.ValidateSymbol();

            return await SendRequest<OkexSwapPriceRange>(GetUrl(Endpoints_Swap_PriceLimit, symbol), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve the liquidated orders. This is publicly accessible without account authentication.
        /// Rate limit: 20 requests per 2 seconds
        /// </summary>
        /// <param name="symbol">Contract ID, e.g. BTC-USD-SWAP</param>
        /// <param name="status">0:unfilled in the recent 7 days; 1:filled orders in the recent 7 days)</param>
        /// <param name="limit">Number of results per request in reverse chronological order (latest first). Maximum 100.</param>
        /// <param name="from">Pagination content before requesting this id (older data) e.g 2</param>
        /// <param name="to">Pagination content after requesting this id (updated data) e.g 2</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<IEnumerable<OkexSwapLiquidatedOrder>> Swap_GetLiquidatedOrders(string symbol, OkexSwapLiquidationStatus status, int limit = 100, long? from = null, long? to = null, CancellationToken ct = default) => Swap_GetLiquidatedOrders_Async(symbol, status, limit, from, to, ct).Result;
        /// <summary>
        /// Retrieve the liquidated orders. This is publicly accessible without account authentication.
        /// Rate limit: 20 requests per 2 seconds
        /// </summary>
        /// <param name="symbol">Contract ID, e.g. BTC-USD-SWAP</param>
        /// <param name="status">0:unfilled in the recent 7 days; 1:filled orders in the recent 7 days)</param>
        /// <param name="limit">Number of results per request in reverse chronological order (latest first). Maximum 100.</param>
        /// <param name="from">Pagination content before requesting this id (older data) e.g 2</param>
        /// <param name="to">Pagination content after requesting this id (updated data) e.g 2</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<OkexSwapLiquidatedOrder>>> Swap_GetLiquidatedOrders_Async(string symbol, OkexSwapLiquidationStatus status, int limit = 100, long? from = null, long? to = null, CancellationToken ct = default)
        {
            symbol = symbol.ValidateSymbol();
            limit.ValidateIntBetween(nameof(limit), 1, 100);

            var parameters = new Dictionary<string, object>
            {
                { "limit", limit },
                { "status", JsonConvert.SerializeObject(status, new SwapLiquidationStatusConverter(false)) },
            };
            parameters.AddOptionalParameter("from", from);
            parameters.AddOptionalParameter("to", to);

            return await SendRequest<IEnumerable<OkexSwapLiquidatedOrder>>(GetUrl(Endpoints_Swap_LiquidatedOrders, symbol), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Get the time of next settlement.
        /// Rate limit：20 requests per 2 seconds
        /// Notes:
        /// - Contract name will be verified. If the name differs from database, error code will be triggered.
        /// </summary>
        /// <param name="symbol">Contract ID, e.g. BTC-USD-SWAP,BTC-USDT-SWAP</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<OkexSwapSettlementTime> Swap_GetNextSettlementTime(string symbol, CancellationToken ct = default) => Swap_GetNextSettlementTime_Async(symbol, ct).Result;
        /// <summary>
        /// Get the time of next settlement.
        /// Rate limit：20 requests per 2 seconds
        /// Notes:
        /// - Contract name will be verified. If the name differs from database, error code will be triggered.
        /// </summary>
        /// <param name="symbol">Contract ID, e.g. BTC-USD-SWAP,BTC-USDT-SWAP</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<OkexSwapSettlementTime>> Swap_GetNextSettlementTime_Async(string symbol, CancellationToken ct = default)
        {
            symbol = symbol.ValidateSymbol();

            return await SendRequest<OkexSwapSettlementTime>(GetUrl(Endpoints_Swap_NextSettlementTime, symbol), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <summary>
        /// Get Mark Price.
        /// Rate limit：20 requests per 2 seconds
        /// Notes:
        /// - Contract name will be verified. If the name differs from database, error code will be triggered.
        /// </summary>
        /// <param name="symbol">Contract ID, e.g. BTC-USD-SWAP,BTC-USDT-SWAP</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<OkexSwapMarkPrice> Swap_GetMarkPrice(string symbol, CancellationToken ct = default) => Swap_GetMarkPrice_Async(symbol, ct).Result;
        /// <summary>
        /// Get Mark Price.
        /// Rate limit：20 requests per 2 seconds
        /// Notes:
        /// - Contract name will be verified. If the name differs from database, error code will be triggered.
        /// </summary>
        /// <param name="symbol">Contract ID, e.g. BTC-USD-SWAP,BTC-USDT-SWAP</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<OkexSwapMarkPrice>> Swap_GetMarkPrice_Async(string symbol, CancellationToken ct = default)
        {
            symbol = symbol.ValidateSymbol();

            return await SendRequest<OkexSwapMarkPrice>(GetUrl(Endpoints_Swap_MarkPrice, symbol), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <summary>
        /// Get Funding Rate History,This API can retrieve data in the last 1 month.
        /// Rate limit：20 requests per 2 seconds
        /// </summary>
        /// <param name="symbol">Contract ID, e.g. BTC-USD-SWAP,BTC-USDT-SWAP</param>
        /// <param name="limit">number of results per request in chronological order (latest at the front). Maximum 100.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<IEnumerable<OkexSwapFundingRate>> Swap_GetFundingRateHistory(string symbol, int limit = 100, CancellationToken ct = default) => Swap_GetFundingRateHistory_Async(symbol, limit, ct).Result;
        /// <summary>
        /// Get Funding Rate History,This API can retrieve data in the last 1 month.
        /// Rate limit：20 requests per 2 seconds
        /// </summary>
        /// <param name="symbol">Contract ID, e.g. BTC-USD-SWAP,BTC-USDT-SWAP</param>
        /// <param name="limit">number of results per request in chronological order (latest at the front). Maximum 100.</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<OkexSwapFundingRate>>> Swap_GetFundingRateHistory_Async(string symbol, int limit = 100, CancellationToken ct = default)
        {
            symbol = symbol.ValidateSymbol();
            limit.ValidateIntBetween(nameof(limit), 1, 100);

            return await SendRequest<IEnumerable<OkexSwapFundingRate>>(GetUrl(Endpoints_Swap_FundingRateHistory, symbol), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve the history candles of the contract.As of now, the historical candels of 9 major currencies are provided: BTC, ETH, LTC, ETC, XRP, EOS, BCH, BSV, TRX.
        /// Rate limit: 5 requests per 2 seconds (Depending on the underlying speed limit)
        /// Notes:
        /// - The return values are[timestamp, open, high, low, close, volume, currency_volume]
        /// - start > end, start defaults to the latest, end defaults to the oldest, limit defaults to a fixed number, and returns the latest(limit) records between start and end.If the number between start and end is greater than the limit, begining from start.
        /// </summary>
        /// <param name="symbol">Contract ID,e.g. BTC-USD-SWAP,BTC-USDT-SWAP,Must use the currently existing contract id to get k-line data</param>
        /// <param name="period">Bar size in seconds, default 60, must be one of [60/180/300/900/1800/3600/7200/14400/21600/43200/86400/604800] otherwise returns error</param>
        /// <param name="start">Start time in ISO 8601</param>
        /// <param name="end">End time in ISO 8601</param>
        /// <param name="limit">The number of candles returned, the default is 300，and the maximum is 300</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public WebCallResult<IEnumerable<OkexSwapCandle>> Swap_GetHistoricalMarketData(string symbol, OkexSpotPeriod period, DateTime? start = null, DateTime? end = null, int limit = 300, CancellationToken ct = default) => Swap_GetHistoricalMarketData_Async(symbol, period, start, end, limit, ct).Result;
        /// <summary>
        /// Retrieve the history candles of the contract.As of now, the historical candels of 9 major currencies are provided: BTC, ETH, LTC, ETC, XRP, EOS, BCH, BSV, TRX.
        /// Rate limit: 5 requests per 2 seconds (Depending on the underlying speed limit)
        /// Notes:
        /// - The return values are[timestamp, open, high, low, close, volume, currency_volume]
        /// - start > end, start defaults to the latest, end defaults to the oldest, limit defaults to a fixed number, and returns the latest(limit) records between start and end.If the number between start and end is greater than the limit, begining from start.
        /// </summary>
        /// <param name="symbol">Contract ID,e.g. BTC-USD-SWAP,BTC-USDT-SWAP,Must use the currently existing contract id to get k-line data</param>
        /// <param name="period">Bar size in seconds, default 60, must be one of [60/180/300/900/1800/3600/7200/14400/21600/43200/86400/604800] otherwise returns error</param>
        /// <param name="start">Start time in ISO 8601</param>
        /// <param name="end">End time in ISO 8601</param>
        /// <param name="limit">The number of candles returned, the default is 300，and the maximum is 300</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<OkexSwapCandle>>> Swap_GetHistoricalMarketData_Async(string symbol, OkexSpotPeriod period, DateTime? start = null, DateTime? end = null, int limit = 300, CancellationToken ct = default)
        {
            symbol = symbol.ValidateSymbol();
            limit.ValidateIntBetween(nameof(limit), 1, 300);

            var parameters = new Dictionary<string, object>
            {
                { "granularity", JsonConvert.SerializeObject(period, new SpotPeriodConverter(false)) },
            };
            parameters.AddOptionalParameter("start", start?.DateTimeToIso8601String());
            parameters.AddOptionalParameter("end", end?.DateTimeToIso8601String());
            parameters.AddOptionalParameter("limit", limit);

            return await SendRequest<IEnumerable<OkexSwapCandle>>(GetUrl(Endpoints_Swap_HistoricalMarketData, symbol), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        #endregion

        #endregion
    }
}