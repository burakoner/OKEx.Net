namespace Okex.Net.Interfaces
{
    public interface IOkexClient
    {
        /// <summary>
        /// Whether public requests should be signed if OkexApiCredentials are provided. Needed for accurate rate limiting.
        /// </summary>
        bool SignPublicRequests { get; }

        /// <summary>
        /// Set the API key and secret
        /// </summary>
        /// <param name="apiKey">The api key</param>
        /// <param name="apiSecret">The api secret</param>
        /// <param name="passPhrase">The passphrase you specified when creating the API key</param>
        void SetApiCredentials(string apiKey, string apiSecret, string passPhrase);
    }
}