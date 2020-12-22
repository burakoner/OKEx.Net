using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Okex.Net.RestObjects;
using System;
using System.Threading.Tasks;

namespace Okex.Net.Interfaces
{
    public interface IOkexSocketClientMargin
    {
        CallResult<UpdateSubscription> Margin_SubscribeToBalance(string symbol, Action<OkexMarginBalance> onData);
        Task<CallResult<UpdateSubscription>> Margin_SubscribeToBalance_Async(string symbol, Action<OkexMarginBalance> onData);
    }
}