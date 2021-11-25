using CryptoExchange.Net.Objects;

namespace Okex.Net.CoreObjects
{
    /// <summary>
    /// Socket client options
    /// </summary>
    public class OkexSocketClientOptions : SocketClientOptions
    {
        /// <summary>
        /// Flag for Demo Trading Services of OKEx. Use this option if you want to use demo trading in OKEx
        /// </summary>
        public bool DemoTradingService { get; set; } = false;

        public OkexSocketClientOptions() : base("")
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
