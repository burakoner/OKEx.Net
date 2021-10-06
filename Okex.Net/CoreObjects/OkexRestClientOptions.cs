using CryptoExchange.Net.Objects;

namespace Okex.Net.CoreObjects
{
    /// <summary>
    /// Client Options
    /// </summary>
    public class OkexRestClientOptions : RestClientOptions
    {
        /// <summary>
        /// Whether public requests should be signed if ApiCredentials are provided. Needed for accurate rate limiting.
        /// </summary>
        public bool SignPublicRequests { get; set; } = false;

        /// <summary>
        /// ctor
        /// </summary>
        public OkexRestClientOptions() : base("https://www.okex.com")
        {
        }

        /// <summary>
        /// Copy
        /// </summary>
        /// <returns></returns>
        public OkexRestClientOptions Copy()
        {
            var copy = Copy<OkexRestClientOptions>();
            copy.SignPublicRequests = SignPublicRequests;
            return copy;
        }
    }
}
