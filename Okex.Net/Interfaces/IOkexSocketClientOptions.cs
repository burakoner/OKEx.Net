using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Okex.Net.Enums;
using Okex.Net.RestObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Okex.Net.Interfaces
{
    public interface IOkexSocketClientOptions
    {
        CallResult<UpdateSubscription> Options_SubscribeToBalance(string underlying, Action<OkexOptionsBalance> onData);
        Task<CallResult<UpdateSubscription>> Options_SubscribeToBalance_Async(string underlying, Action<OkexOptionsBalance> onData);
        CallResult<UpdateSubscription> Options_SubscribeToCandlesticks(string instrument, OkexSpotPeriod period, Action<OkexOptionsCandle> onData);
        Task<CallResult<UpdateSubscription>> Options_SubscribeToCandlesticks_Async(string instrument, OkexSpotPeriod period, Action<OkexOptionsCandle> onData);
        CallResult<UpdateSubscription> Options_SubscribeToContracts(string underlying, Action<OkexOptionsInstrument> onData);
        Task<CallResult<UpdateSubscription>> Options_SubscribeToContracts_Async(string underlying, Action<OkexOptionsInstrument> onData);
        CallResult<UpdateSubscription> Options_SubscribeToMarketData(IEnumerable<string> underlyings, Action<OkexOptionsMarketData> onData);
        CallResult<UpdateSubscription> Options_SubscribeToMarketData(string underlying, Action<OkexOptionsMarketData> onData);
        Task<CallResult<UpdateSubscription>> Options_SubscribeToMarketData_Async(IEnumerable<string> underlyings, Action<OkexOptionsMarketData> onData);
        Task<CallResult<UpdateSubscription>> Options_SubscribeToMarketData_Async(string underlying, Action<OkexOptionsMarketData> onData);
        CallResult<UpdateSubscription> Options_SubscribeToOrderBook(string instrument, OkexOrderBookDepth depth, Action<OkexOptionsOrderBook> onData);
        Task<CallResult<UpdateSubscription>> Options_SubscribeToOrderBook_Async(string instrument, OkexOrderBookDepth depth, Action<OkexOptionsOrderBook> onData);
        CallResult<UpdateSubscription> Options_SubscribeToOrders(string underlying, Action<OkexOptionsOrder> onData);
        Task<CallResult<UpdateSubscription>> Options_SubscribeToOrders_Async(string underlying, Action<OkexOptionsOrder> onData);
        CallResult<UpdateSubscription> Options_SubscribeToPositions(string underlying, Action<OkexOptionsPosition> onData);
        Task<CallResult<UpdateSubscription>> Options_SubscribeToPositions_Async(string underlying, Action<OkexOptionsPosition> onData);
        CallResult<UpdateSubscription> Options_SubscribeToTicker(IEnumerable<string> underlyings, Action<OkexOptionsTicker> onData);
        CallResult<UpdateSubscription> Options_SubscribeToTicker(string instrument, Action<OkexOptionsTicker> onData);
        Task<CallResult<UpdateSubscription>> Options_SubscribeToTicker_Async(IEnumerable<string> underlyings, Action<OkexOptionsTicker> onData);
        Task<CallResult<UpdateSubscription>> Options_SubscribeToTicker_Async(string instrument, Action<OkexOptionsTicker> onData);
        CallResult<UpdateSubscription> Options_SubscribeToTrades(string instrument, Action<OkexOptionsTrade> onData);
        Task<CallResult<UpdateSubscription>> Options_SubscribeToTrades_Async(string instrument, Action<OkexOptionsTrade> onData);
    }
}