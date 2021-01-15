using CryptoExchange.Net.Objects;

namespace Okex.Net.CoreObjects
{
    /// <summary>
    /// Socket client options
    /// </summary>
    public class OkexSocketClientOptions : SocketClientOptions
    {
        public OkexSocketClientOptions() : base("wss://real.okex.com:8443/ws/v3")
        {
            SocketSubscriptionsCombineTarget = 100;
        }

        public OkexSocketClientOptions Copy()
        {
            var copy = Copy<OkexSocketClientOptions>();
            return copy;
        }
    }
}
