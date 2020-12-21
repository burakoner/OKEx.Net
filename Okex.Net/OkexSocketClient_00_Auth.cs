using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Okex.Net.Converters;
using Okex.Net.RestObjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Okex.Net.SocketObjects.Structure;
using Okex.Net.SocketObjects.Containers;
using Okex.Net.Helpers;
using Okex.Net.Enums;
using CryptoExchange.Net;
using System.Security.Cryptography;
using System.Text;
using System.Globalization;

namespace Okex.Net
{
    public partial class OkexSocketClient
    {
        #region WS Auth Methods
        public CallResult<OkexSocketLoginResponse> User_Login(string apiKey, string apiSecret, string passPhrase) => User_Login_Async(apiKey, apiSecret, passPhrase).Result;
        public async Task<CallResult<OkexSocketLoginResponse>> User_Login_Async(string apiKey, string apiSecret, string passPhrase)
        {
            if (string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(apiSecret) || string.IsNullOrEmpty(passPhrase))
                return new CallResult<OkexSocketLoginResponse>(default, new NoApiCredentialsError());

            this.Key = apiKey.ToSecureString();
            this.Secret = apiSecret.ToSecureString();
            this.PassPhrase = passPhrase.ToSecureString();

            var time = (DateTime.UtcNow.ToUnixTimeMilliSeconds() / 1000.0m).ToString(CultureInfo.InvariantCulture);
            var signtext = time + "GET" + "/users/self/verify";
            this._hmacEncryptor = new HMACSHA256(Encoding.ASCII.GetBytes(this.Secret.GetString()));
            var signature = OkexAuthenticationProvider.Base64Encode(_hmacEncryptor.ComputeHash(Encoding.UTF8.GetBytes(signtext)));
            var request = new OkexSocketRequest(OkexSocketOperation.Login, this.Key.GetString(), this.PassPhrase.GetString(), time, signature);

            var result = await Query<OkexSocketLoginResponse>(request, false).ConfigureAwait(true);
            this.Authendicated = result != null && result.Data != null && result.Data.Success;
            return await Query<OkexSocketLoginResponse>(request, false).ConfigureAwait(true);
        }
        #endregion
    }
}
