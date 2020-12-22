using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Okex.Net.Enums;
using Okex.Net.RestObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Okex.Net.Interfaces
{
    public interface IOkexSocketClientIndex
    {
        CallResult<UpdateSubscription> Index_SubscribeToCandlesticks(string symbol, OkexSpotPeriod period, Action<OkexIndexCandle> onData);
        Task<CallResult<UpdateSubscription>> Index_SubscribeToCandlesticks_Async(string symbol, OkexSpotPeriod period, Action<OkexIndexCandle> onData);
        CallResult<UpdateSubscription> Index_SubscribeToTicker(IEnumerable<string> symbol, Action<OkexOptionsTicker> onData);
        CallResult<UpdateSubscription> Index_SubscribeToTicker(string symbol, Action<OkexIndexTicker> onData);
        Task<CallResult<UpdateSubscription>> Index_SubscribeToTicker_Async(IEnumerable<string> symbol, Action<OkexOptionsTicker> onData);
        Task<CallResult<UpdateSubscription>> Index_SubscribeToTicker_Async(string symbol, Action<OkexIndexTicker> onData);
    }
}