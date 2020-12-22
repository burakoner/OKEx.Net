using CryptoExchange.Net.Objects;
using Okex.Net.SocketObjects.Containers;
using System.Threading.Tasks;

namespace Okex.Net.Interfaces
{
    public interface IOkexSocketClientSystem
    {
        CallResult<OkexGeneralPingPongContainer> Ping();
        Task<CallResult<OkexGeneralPingPongContainer>> PingAsync();
    }
}