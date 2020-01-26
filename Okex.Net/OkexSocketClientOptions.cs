using CryptoExchange.Net.Objects;
using Okex.Net.Interfaces;

namespace Okex.Net
{
    /// <summary>
    /// Socket client options
    /// </summary>
    public class OkexSocketClientOptions : SocketClientOptions
    {
        public OkexSocketClientOptions(): base("wss://real.okex.com:8443/ws/v3")
        {
            SocketSubscriptionsCombineTarget = 10;
        }

        public OkexSocketClientOptions Copy()
        {
            var copy = Copy<OkexSocketClientOptions>();
            return copy;
        }
    }
}
