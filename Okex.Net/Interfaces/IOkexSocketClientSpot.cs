using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Okex.Net.Enums;
using Okex.Net.RestObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Okex.Net.Interfaces
{
    public interface IOkexSocketClientSpot
    {
        CallResult<UpdateSubscription> Spot_SubscribeToAlgoOrders(string symbol, Action<OkexSpotAlgoOrder> onData);
        Task<CallResult<UpdateSubscription>> Spot_SubscribeToAlgoOrders_Async(string symbol, Action<OkexSpotAlgoOrder> onData);
        CallResult<UpdateSubscription> Spot_SubscribeToBalance(string currency, Action<OkexSpotBalance> onData);
        Task<CallResult<UpdateSubscription>> Spot_SubscribeToBalance_Async(string currency, Action<OkexSpotBalance> onData);
        CallResult<UpdateSubscription> Spot_SubscribeToCandlesticks(string symbol, OkexSpotPeriod period, Action<OkexSpotCandle> onData);
        Task<CallResult<UpdateSubscription>> Spot_SubscribeToCandlesticks_Async(string symbol, OkexSpotPeriod period, Action<OkexSpotCandle> onData);
        CallResult<UpdateSubscription> Spot_SubscribeToOrderBook(string symbol, OkexOrderBookDepth depth, Action<OkexSpotOrderBook> onData);
        CallResult<UpdateSubscription> Spot_SubscribeToOrders(string symbol, Action<OkexSpotOrderDetails> onData);
        Task<CallResult<UpdateSubscription>> Spot_SubscribeToOrders_Async(string symbol, Action<OkexSpotOrderDetails> onData);
        CallResult<UpdateSubscription> Spot_SubscribeToTicker(IEnumerable<string> symbols, Action<OkexSpotTicker> onData);
        CallResult<UpdateSubscription> Spot_SubscribeToTicker(string symbol, Action<OkexSpotTicker> onData);
        Task<CallResult<UpdateSubscription>> Spot_SubscribeToTicker_Async(IEnumerable<string> symbols, Action<OkexSpotTicker> onData);
        Task<CallResult<UpdateSubscription>> Spot_SubscribeToTicker_Async(string symbol, Action<OkexSpotTicker> onData);
        CallResult<UpdateSubscription> Spot_SubscribeToTrades(string symbol, Action<OkexSpotTrade> onData);
        Task<CallResult<UpdateSubscription>> Spot_SubscribeToTrades_Async(string symbol, Action<OkexSpotTrade> onData);
        Task<CallResult<UpdateSubscription>> Spot_SubscribeToTrades_Async(string symbol, OkexOrderBookDepth depth, Action<OkexSpotOrderBook> onData);
    }
}