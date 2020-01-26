using CryptoExchange.Net.Objects;
using Okex.Net.Interfaces;

namespace Okex.Net
{
    /// <summary>
    /// Client Options
    /// </summary>
    public class OkexClientOptions: RestClientOptions
    {
        /// <summary>
        /// Whether public requests should be signed if ApiCredentials are provided. Needed for accurate rate limiting.
        /// </summary>
        public bool SignPublicRequests { get; set; } = false;

        /// <summary>
        /// ctor
        /// </summary>
        public OkexClientOptions(): base("https://www.okex.com")
        {
        }

        /// <summary>
        /// Copy
        /// </summary>
        /// <returns></returns>
        public OkexClientOptions Copy()
        {
            var copy = Copy<OkexClientOptions>();
            copy.SignPublicRequests = this.SignPublicRequests;
            return copy;
        }
    }
}
