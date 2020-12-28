using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using Okex.Net.SocketObjects.Structure;
using System.Threading.Tasks;

namespace Okex.Net.Interfaces
{
    /// <summary>
    /// Interface for the Okex socket client
    /// </summary>
    public interface IOkexSocketClient
    {
        bool Authendicated { get; }
        void SetApiCredentials(string apiKey, string apiSecret, string passPhrase);
        CallResult<OkexSocketLoginResponse> Auth_Login(string apiKey, string apiSecret, string passPhrase);
        Task<CallResult<OkexSocketLoginResponse>> Auth_Login_Async(string apiKey, string apiSecret, string passPhrase);
    }
}