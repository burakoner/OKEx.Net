using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using Okex.Net.CoreObjects;
using Okex.Net.Helpers;
using Okex.Net.Interfaces;
using Okex.Net.SocketObjects.Structure;
using System;
using System.Globalization;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Okex.Net
{
    public partial class OkexSocketClient: IOkexSocketClient
    {
        private SecureString? Key;
        private SecureString? Secret;
        private SecureString? PassPhrase;
        private HMACSHA256? _hmacEncryptor;
        public bool Authendicated { get; private set; }

        #region WS Auth Methods
        /// <summary>
        /// Set the API key and secret
        /// </summary>
        /// <param name="apiKey">The api key</param>
        /// <param name="apiSecret">The api secret</param>
        /// <param name="passPhrase">The api pass phrase</param>
        public void SetApiCredentials(string apiKey, string apiSecret, string passPhrase)
        {
            Key = apiKey.ToSecureString();
            Secret = apiSecret.ToSecureString();
            PassPhrase = passPhrase.ToSecureString();
        }

        public CallResult<OkexSocketLoginResponse> Auth_Login(string apiKey, string apiSecret, string passPhrase) => Auth_Login_Async(apiKey, apiSecret, passPhrase).Result;
        public async Task<CallResult<OkexSocketLoginResponse>> Auth_Login_Async(string apiKey, string apiSecret, string passPhrase)
        {
            if (string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(apiSecret) || string.IsNullOrEmpty(passPhrase))
                return new CallResult<OkexSocketLoginResponse>(default, new NoApiCredentialsError());

            Key = apiKey.ToSecureString();
            Secret = apiSecret.ToSecureString();
            PassPhrase = passPhrase.ToSecureString();

            var time = (DateTime.UtcNow.ToUnixTimeMilliSeconds() / 1000.0m).ToString(CultureInfo.InvariantCulture);
            var signtext = time + "GET" + "/users/self/verify";
            _hmacEncryptor = new HMACSHA256(Encoding.ASCII.GetBytes(Secret.GetString()));
            var signature = OkexAuthenticationProvider.Base64Encode(_hmacEncryptor.ComputeHash(Encoding.UTF8.GetBytes(signtext)));
            var request = new OkexSocketRequest(OkexSocketOperation.Login, Key.GetString(), PassPhrase.GetString(), time, signature);

            var result = await Query<OkexSocketLoginResponse>(request, false).ConfigureAwait(true);
            Authendicated = result != null && result.Data != null && result.Data.Success;
            return await Query<OkexSocketLoginResponse>(request, false).ConfigureAwait(true);
        }
        #endregion
    }
}
