using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Okex.Net.Enums;
using Okex.Net.RestObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Okex.Net.Interfaces
{
    public interface IOkexSocketClientSwap
    {
        CallResult<UpdateSubscription> Swap_SubscribeToAlgoOrders(string symbol, Action<OkexSwapAlgoOrder> onData);
        Task<CallResult<UpdateSubscription>> Swap_SubscribeToAlgoOrders_Async(string symbol, Action<OkexSwapAlgoOrder> onData);
        CallResult<UpdateSubscription> Swap_SubscribeToBalance(string symbol, Action<OkexSwapBalanceExt> onData);
        Task<CallResult<UpdateSubscription>> Swap_SubscribeToBalance_Async(string symbol, Action<OkexSwapBalanceExt> onData);
        CallResult<UpdateSubscription> Swap_SubscribeToCandlesticks(string symbol, OkexSpotPeriod period, Action<OkexSwapCandle> onData);
        Task<CallResult<UpdateSubscription>> Swap_SubscribeToCandlesticks_Async(string symbol, OkexSpotPeriod period, Action<OkexSwapCandle> onData);
        CallResult<UpdateSubscription> Swap_SubscribeToFundingRate(string symbol, Action<OkexSwapFundingRate> onData);
        Task<CallResult<UpdateSubscription>> Swap_SubscribeToFundingRate_Async(string symbol, Action<OkexSwapFundingRate> onData);
        CallResult<UpdateSubscription> Swap_SubscribeToMarkPrice(string symbol, Action<OkexSwapMarkPrice> onData);
        Task<CallResult<UpdateSubscription>> Swap_SubscribeToMarkPrice_Async(string symbol, Action<OkexSwapMarkPrice> onData);
        CallResult<UpdateSubscription> Swap_SubscribeToOrderBook(string symbol, OkexOrderBookDepth depth, Action<OkexSwapOrderBook> onData);
        Task<CallResult<UpdateSubscription>> Swap_SubscribeToOrderBook_Async(string symbol, OkexOrderBookDepth depth, Action<OkexSwapOrderBook> onData);
        CallResult<UpdateSubscription> Swap_SubscribeToOrders(string symbol, Action<OkexSwapOrder> onData);
        Task<CallResult<UpdateSubscription>> Swap_SubscribeToOrders_Async(string symbol, Action<OkexSwapOrder> onData);
        CallResult<UpdateSubscription> Swap_SubscribeToPositions(string symbol, Action<OkexSwapPosition> onData);
        Task<CallResult<UpdateSubscription>> Swap_SubscribeToPositions_Async(string symbol, Action<OkexSwapPosition> onData);
        CallResult<UpdateSubscription> Swap_SubscribeToPriceRange(string symbol, Action<OkexSwapPriceRange> onData);
        Task<CallResult<UpdateSubscription>> Swap_SubscribeToPriceRange_Async(string symbol, Action<OkexSwapPriceRange> onData);
        CallResult<UpdateSubscription> Swap_SubscribeToTicker(IEnumerable<string> symbols, Action<OkexSwapTicker> onData);
        CallResult<UpdateSubscription> Swap_SubscribeToTicker(string symbol, Action<OkexSwapTicker> onData);
        Task<CallResult<UpdateSubscription>> Swap_SubscribeToTicker_Async(IEnumerable<string> symbols, Action<OkexSwapTicker> onData);
        Task<CallResult<UpdateSubscription>> Swap_SubscribeToTicker_Async(string symbol, Action<OkexSwapTicker> onData);
        CallResult<UpdateSubscription> Swap_SubscribeToTrades(string symbol, Action<OkexSwapTrade> onData);
        Task<CallResult<UpdateSubscription>> Swap_SubscribeToTrades_Async(string symbol, Action<OkexSwapTrade> onData);
    }
}