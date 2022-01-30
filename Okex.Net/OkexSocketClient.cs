using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Okex.Net.CoreObjects;
using Okex.Net.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Okex.Net
{
    public partial class OkexSocketClient : SocketClient
    {
        protected SecureString Key { get; set; }
        protected SecureString Secret { get; set; }
        protected SecureString PassPhrase { get; set; }
        public bool Authendicated { get; private set; }

        #region Client Options
        protected static OkexSocketClientOptions defaultOptions = new OkexSocketClientOptions();
        protected static OkexSocketClientOptions DefaultOptions => defaultOptions.Copy();
        protected bool DemoTradingService { get; }
        #endregion

        #region Constructor/Destructor
        public OkexSocketClient() : this(DefaultOptions)
        {
        }

        public OkexSocketClient(OkexSocketClientOptions options) : base("OKEx WS Api", options, options.ApiCredentials == null ? null : new OkexAuthenticationProvider(options.ApiCredentials, "", options.DemoTradingService, false, ArrayParametersSerialization.Array))
        {
            DemoTradingService = options.DemoTradingService;
            SetDataInterpreter(DecompressData, null);
            SendPeriodic(TimeSpan.FromSeconds(5), con => "ping");
        }
        #endregion

        #region Common Methods
        /// <summary>
        /// Set the default options to be used when creating new socket clients
        /// </summary>
        /// <param name="options">The options to use for new clients</param>
        public static void SetDefaultOptions(OkexSocketClientOptions options)
        {
            defaultOptions = options;
        }
        #endregion

        /// <summary>
        /// Set the API key and secret
        /// </summary>
        /// <param name="apiKey">The api key</param>
        /// <param name="apiSecret">The api secret</param>
        /// <param name="passPhrase">The api pass phrase</param>
        public virtual void SetApiCredentials(string apiKey, string apiSecret, string passPhrase)
        {
            Key = apiKey.ToSecureString();
            Secret = apiSecret.ToSecureString();
            PassPhrase = passPhrase.ToSecureString();
        }

        public virtual CallResult<OkexSocketPingPong> Ping() => PingAsync().Result;
        public virtual async Task<CallResult<OkexSocketPingPong>> PingAsync()
        {
            var pit = DateTime.UtcNow;
            var sw = Stopwatch.StartNew();
            var response = await QueryAsync<string>("ping", false).ConfigureAwait(true);
            var pot = DateTime.UtcNow;
            sw.Stop();

            var result = new OkexSocketPingPong { PingTime = pit, PongTime = pot, Latency = sw.Elapsed, PongMessage = response.Data };
            return new CallResult<OkexSocketPingPong>(result, response.Error);
        }

        protected static string DecompressData(byte[] byteData)
        {
            using (var decompressedStream = new MemoryStream())
            using (var compressedStream = new MemoryStream(byteData))
            using (var deflateStream = new DeflateStream(compressedStream, CompressionMode.Decompress))
            {
                deflateStream.CopyTo(decompressedStream);
                decompressedStream.Position = 0;

                using (var streamReader = new StreamReader(decompressedStream))
                {
                    /** /
                    var response = streamReader.ReadToEnd();
                    return response;
                    /**/

                    return streamReader.ReadToEnd();
                }
            }
        }

        protected override SocketConnection GetSocketConnection(string address, bool authenticated)
        {
            return OkexGetSocketConnection(address, authenticated);
        }
        protected virtual SocketConnection OkexGetSocketConnection(string address, bool authenticated)
        {
            address = authenticated
                ? "wss://ws.okx.com:8443/ws/v5/private"
                : "wss://ws.okx.com:8443/ws/v5/public";

            if (DemoTradingService)
            {
                address = authenticated
                    ? "wss://wspap.okx.com:8443/ws/v5/private?brokerId=9999"
                    : "wss://wspap.okx.com:8443/ws/v5/public?brokerId=9999";
            }

            var socketResult = sockets
                .Where(s =>
                    s.Value.Socket.Url.TrimEnd('/') == address.TrimEnd('/') &&
                    (s.Value.Authenticated == authenticated || !authenticated) &&
                    s.Value.Connected)
                .OrderBy(s => s.Value.SubscriptionCount)
                .FirstOrDefault();
            var result = socketResult.Equals(default(KeyValuePair<int, SocketConnection>)) ? null : socketResult.Value;
            if (result != null)
            {
                if (result.SubscriptionCount < SocketCombineTarget || (sockets.Count >= MaxSocketConnections && sockets.All(s => s.Value.SubscriptionCount >= SocketCombineTarget)))
                {
                    // Use existing socket if it has less than target connections OR it has the least connections and we can't make new
                    return result;
                }
            }

            // Create new socket
            var socket = CreateSocket(address);
            var socketConnection = new SocketConnection(this, socket);
            socketConnection.UnhandledMessage += HandleUnhandledMessage;
            foreach (var kvp in genericHandlers)
            {
                var handler = SocketSubscription.CreateForIdentifier(NextId(), kvp.Key, false, kvp.Value);
                socketConnection.AddSubscription(handler);
            }

            return socketConnection;
        }

        protected override bool HandleQueryResponse<T>(SocketConnection s, object request, JToken data, out CallResult<T> callResult)
        {
            return OkexHandleQueryResponse<T>(s, request, data, out callResult);
        }
        protected virtual bool OkexHandleQueryResponse<T>(SocketConnection s, object request, JToken data, out CallResult<T> callResult)
        {
            callResult = new CallResult<T>(default, null);

            // Ping Request
            if (request.ToString() == "ping" && data.ToString() == "pong")
            {
                return true;
            }

            // Check for Error
            if (data is JObject && data["event"] != null && (string)data["event"]! == "error" && data["code"] != null && data["msg"] != null)
            {
                log.Write(LogLevel.Warning, "Query failed: " + (string)data["msg"]!);
                callResult = new CallResult<T>(default, new ServerError($"{(string)data["code"]!}, {(string)data["msg"]!}"));
                return true;
            }

            // Login Request
            if (data is JObject && data["event"] != null && (string)data["event"]! == "login")
            {
                var desResult = Deserialize<T>(data, false);
                if (!desResult)
                {
                    log.Write(LogLevel.Warning, $"Failed to deserialize data: {desResult.Error}. Data: {data}");
                    return false;
                }

                callResult = new CallResult<T>(desResult.Data, null);
                return true;
            }

            return false;
        }

        protected override bool HandleSubscriptionResponse(SocketConnection s, SocketSubscription subscription, object request, JToken message, out CallResult<object> callResult)
        {
            return OkexHandleSubscriptionResponse(s, subscription, request, message, out callResult);
        }
        protected virtual bool OkexHandleSubscriptionResponse(SocketConnection s, SocketSubscription subscription, object request, JToken message, out CallResult<object> callResult)
        {
            callResult = null;

            // Check for Error
            // 30040: {0} Channel : {1} doesn't exist
            if (message["event"] != null && (string)message["event"]! == "error" && message["errorCode"] != null && (string)message["errorCode"]! == "30040")
            {
                log.Write(LogLevel.Warning, "Subscription failed: " + (string)message["message"]!);
                callResult = new CallResult<object>(null, new ServerError($"{(string)message["errorCode"]!}, {(string)message["message"]!}"));
                return true;
            }

            // Check for Success
            if (message["event"] != null && (string)message["event"]! == "subscribe" && message["arg"]["channel"] != null)
            {
                if (request is OkexSocketRequest socRequest)
                {
                    if (socRequest.Arguments.FirstOrDefault().Channel == (string)message["arg"]["channel"]!)
                    {
                        log.Write(LogLevel.Debug, "Subscription completed");
                        callResult = new CallResult<object>(true, null);
                        return true;
                    }
                }
            }

            return false;
        }

        protected override bool MessageMatchesHandler(JToken message, object request)
        {
            return OkexMessageMatchesHandler(message, request);
        }
        protected virtual bool OkexMessageMatchesHandler(JToken message, object request)
        {
            // Ping Request
            if (request.ToString() == "ping" && message.ToString() == "pong")
                return true;

            // Check Point
            if (message.Type != JTokenType.Object)
                return false;

            // Socket Request
            if (request is OkexSocketRequest hRequest)
            {
                // Check for Error
                if (message is JObject && message["event"] != null && (string)message["event"]! == "error" && message["code"] != null && message["msg"] != null)
                    return false;

                // Check for Channel
                if (hRequest.Operation != OkexSocketOperation.Subscribe || message["arg"]["channel"] == null)
                    return false;

                // Compare Request and Response Arguments
                var reqArg = hRequest.Arguments.FirstOrDefault();
                var resArg = JsonConvert.DeserializeObject<OkexSocketRequestArgument>(message["arg"].ToString());

                // Check Data
                var data = message["data"];
                if (data?.HasValues ?? false)
                {
                    if (reqArg.Channel == resArg.Channel &&
                        reqArg.Underlying == resArg.Underlying &&
                        reqArg.InstrumentId == resArg.InstrumentId &&
                        reqArg.InstrumentType == resArg.InstrumentType)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        protected override bool MessageMatchesHandler(JToken message, string identifier)
        {
            return OkexMessageMatchesHandler(message, identifier);
        }
        protected virtual bool OkexMessageMatchesHandler(JToken message, string identifier)
        {
            return true;
        }

        protected override async Task<CallResult<bool>> AuthenticateSocketAsync(SocketConnection s)
        {
            return await OkexAuthenticateSocket(s);
        }
        protected virtual async Task<CallResult<bool>> OkexAuthenticateSocket(SocketConnection s)
        {
            // Check Point
            //if (s.Authenticated)
            //    return new CallResult<bool>(true, null);

            // Check Point
            if (Key == null || Secret == null || PassPhrase == null)
                return new CallResult<bool>(false, new NoApiCredentialsError());

            // Get Credentials
            var key = Key.GetString();
            var secret = Secret.GetString();
            var passphrase = PassPhrase.GetString();

            // Check Point
            if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(secret) || string.IsNullOrEmpty(passphrase))
                return new CallResult<bool>(false, new NoApiCredentialsError());

            // Timestamp
            var timestamp = (DateTime.UtcNow.ToUnixTimeMilliSeconds() / 1000.0m).ToString(CultureInfo.InvariantCulture);

            // Signature
            var signtext = timestamp + "GET" + "/users/self/verify";
            var hmacEncryptor = new HMACSHA256(Encoding.ASCII.GetBytes(secret));
            var signature = OkexAuthenticationProvider.Base64Encode(hmacEncryptor.ComputeHash(Encoding.UTF8.GetBytes(signtext)));
            var request = new OkexSocketAuthRequest(OkexSocketOperation.Login, new OkexSocketAuthRequestArgument
            {
                ApiKey = key,
                Passphrase = passphrase,
                Timestamp = timestamp,
                Signature = signature,
            });

            // Try to Login
            var result = new CallResult<bool>(false, new ServerError("No response from server"));
            await s.SendAndWaitAsync(request, ResponseTimeout, data =>
            {
                if ((string)data["event"] != "login")
                    return false;

                var authResponse = Deserialize<OkexSocketResponse>(data, false);
                if (!authResponse)
                {
                    log.Write(LogLevel.Warning, "Authorization failed: " + authResponse.Error);
                    result = new CallResult<bool>(false, authResponse.Error);
                    return true;
                }
                if (!authResponse.Data.Success)
                {
                    log.Write(LogLevel.Warning, "Authorization failed: " + authResponse.Error.Message);
                    result = new CallResult<bool>(false, new ServerError(authResponse.Error.Code.Value, authResponse.Error.Message));
                    return true;
                }

                log.Write(LogLevel.Debug, "Authorization completed");
                result = new CallResult<bool>(true, null);
                Authendicated = true;
                return true;
            });

            return result;
        }

        protected override async Task<bool> UnsubscribeAsync(SocketConnection connection, SocketSubscription s)
        {
            return await OkexUnsubscribe(connection, s);
        }
        protected virtual async Task<bool> OkexUnsubscribe(SocketConnection connection, SocketSubscription s)
        {
            if (s == null || s.Request == null)
                return false;

            var request = new OkexSocketRequest(OkexSocketOperation.Unsubscribe, ((OkexSocketRequest)s.Request).Arguments);
            await connection.SendAndWaitAsync(request, ResponseTimeout, data =>
            {
                if (data.Type != JTokenType.Object)
                    return false;

                if ((string)data["event"] == "unsubscribe")
                {
                    return (string)data["arg"]["channel"] == request.Arguments.FirstOrDefault().Channel;
                }

                return false;
            });
            return false;
        }
    }
}