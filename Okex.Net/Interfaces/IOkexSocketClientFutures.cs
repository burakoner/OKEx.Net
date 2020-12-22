using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Okex.Net.Enums;
using Okex.Net.RestObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Okex.Net.Interfaces
{
    public interface IOkexSocketClientFutures
    {
        CallResult<UpdateSubscription> Futures_SubscribeToAlgoOrders(string symbol, Action<OkexFuturesAlgoOrder> onData);
        Task<CallResult<UpdateSubscription>> Futures_SubscribeToAlgoOrders_Async(string symbol, Action<OkexFuturesAlgoOrder> onData);
        CallResult<UpdateSubscription> Futures_SubscribeToBalance(string symbol, Action<OkexFuturesBalance> onData);
        Task<CallResult<UpdateSubscription>> Futures_SubscribeToBalance_Async(string symbol, Action<OkexFuturesBalance> onData);
        CallResult<UpdateSubscription> Futures_SubscribeToCandlesticks(string symbol, OkexSpotPeriod period, Action<OkexFuturesCandle> onData);
        Task<CallResult<UpdateSubscription>> Futures_SubscribeToCandlesticks_Async(string symbol, OkexSpotPeriod period, Action<OkexFuturesCandle> onData);
        CallResult<UpdateSubscription> Futures_SubscribeToContracts(Action<OkexFuturesContract> onData);
        Task<CallResult<UpdateSubscription>> Futures_SubscribeToContracts_Async(Action<OkexFuturesContract> onData);
        CallResult<UpdateSubscription> Futures_SubscribeToEstimatedPrice(string symbol, Action<OkexFuturesEstimatedPrice> onData);
        Task<CallResult<UpdateSubscription>> Futures_SubscribeToEstimatedPrice_Async(string symbol, Action<OkexFuturesEstimatedPrice> onData);
        CallResult<UpdateSubscription> Futures_SubscribeToMarkPrice(string symbol, Action<OkexFuturesMarkPrice> onData);
        Task<CallResult<UpdateSubscription>> Futures_SubscribeToMarkPrice_Async(string symbol, Action<OkexFuturesMarkPrice> onData);
        CallResult<UpdateSubscription> Futures_SubscribeToOrderBook(string symbol, OkexOrderBookDepth depth, Action<OkexFuturesOrderBook> onData);
        Task<CallResult<UpdateSubscription>> Futures_SubscribeToOrderBook_Async(string symbol, OkexOrderBookDepth depth, Action<OkexFuturesOrderBook> onData);
        CallResult<UpdateSubscription> Futures_SubscribeToOrders(string symbol, Action<OkexFuturesOrder> onData);
        Task<CallResult<UpdateSubscription>> Futures_SubscribeToOrders_Async(string symbol, Action<OkexFuturesOrder> onData);
        CallResult<UpdateSubscription> Futures_SubscribeToPositions(string symbol, Action<OkexFuturesPosition> onData);
        Task<CallResult<UpdateSubscription>> Futures_SubscribeToPositions_Async(string symbol, Action<OkexFuturesPosition> onData);
        CallResult<UpdateSubscription> Futures_SubscribeToPriceRange(string symbol, Action<OkexFuturesPriceRange> onData);
        Task<CallResult<UpdateSubscription>> Futures_SubscribeToPriceRange_Async(string symbol, Action<OkexFuturesPriceRange> onData);
        CallResult<UpdateSubscription> Futures_SubscribeToTicker(IEnumerable<string> symbols, Action<OkexFuturesTicker> onData);
        CallResult<UpdateSubscription> Futures_SubscribeToTicker(string symbol, Action<OkexFuturesTicker> onData);
        Task<CallResult<UpdateSubscription>> Futures_SubscribeToTicker_Async(IEnumerable<string> symbols, Action<OkexFuturesTicker> onData);
        Task<CallResult<UpdateSubscription>> Futures_SubscribeToTicker_Async(string symbol, Action<OkexFuturesTicker> onData);
        CallResult<UpdateSubscription> Futures_SubscribeToTrades(string symbol, Action<OkexFuturesTrade> onData);
        Task<CallResult<UpdateSubscription>> Futures_SubscribeToTrades_Async(string symbol, Action<OkexFuturesTrade> onData);
    }
}