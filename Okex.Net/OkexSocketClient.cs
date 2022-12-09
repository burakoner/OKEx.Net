using CryptoExchange.Net.Logging;
using System.Net;

namespace Okex.Net;

public partial class OkexSocketClient : BaseSocketClient
{
    #region Internal Fields
    internal OkexSocketClientOptions Options { get; }
    internal OkexSocketClientUnifiedSocket UnifiedSocket { get; }
    #endregion

    #region Constructor/Destructor
    public OkexSocketClient() : this(OkexSocketClientOptions.Default)
    {
    }

    public OkexSocketClient(OkexSocketClientOptions options) : base("OKX Websocket Api", options)
    {
        UnifiedSocket = AddApiClient(new OkexSocketClientUnifiedSocket(log, this, options));
    }
    #endregion

    #region Common Methods
    public static void SetDefaultOptions(OkexSocketClientOptions options)
    {
        OkexSocketClientOptions.Default = options;
    }

    public virtual void SetApiCredentials(OkexApiCredentials credentials)
    {
        UnifiedSocket.SetApiCredentials(credentials);
    }

    public virtual void SetApiCredentials(string apiKey, string apiSecret, string passPhrase)
    {
        var credentials = new OkexApiCredentials(apiKey, apiSecret, passPhrase);
        UnifiedSocket.SetApiCredentials(credentials);
    }
    #endregion

    internal virtual Task<CallResult<T>> UnifiedQueryAsync<T>(object request, bool authenticated)
    {
        return UnifiedSocket.QueryAsync<T>(request, authenticated);
    }
    internal virtual Task<CallResult<UpdateSubscription>> UnifiedSubscribeAsync<T>(object request, string identifier, bool authenticated, Action<DataEvent<T>> dataHandler, CancellationToken ct)
    {
        return UnifiedSocket.SubscribeAsync(request, identifier, authenticated, dataHandler, ct);
    }

}

public class OkexSocketClientUnifiedSocket : SocketApiClient
{
    #region Internal Fields
    internal readonly OkexSocketClient _baseClient;
    internal bool IsAuthendicated { get; private set; }
    internal OkexApiCredentials _credentials { get; private set; }
    internal OkexSocketClientOptions _options { get; private set; }
    #endregion

    internal OkexSocketClientUnifiedSocket(Log log, OkexSocketClient baseClient, OkexSocketClientOptions options) : base(log, options, options.UnifiedStreamsOptions)
    {
        _log = log;
        _baseClient = baseClient;
        _options = options;
        _credentials = options.ApiCredentials;

        SetDataInterpreter(DecompressData, null);
        SendPeriodic("Ping", TimeSpan.FromSeconds(5), con => "ping");
    }

    protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
        => new OkexAuthenticationProvider((OkexApiCredentials)credentials);

    public void SetApiCredentials(OkexApiCredentials credentials)
    {
        _credentials = credentials;
        Options.ApiCredentials = credentials;
        this.SetApiCredentials((ApiCredentials)credentials);
    }

    public void SetApiCredentials(string apiKey, string apiSecret, string passPhrase)
    {
        var credentials = new OkexApiCredentials(apiKey, apiSecret, passPhrase);

        _credentials = credentials;
        Options.ApiCredentials = credentials;
        this.SetApiCredentials((ApiCredentials)credentials);
    }

    protected static string DecompressData(byte[] byteData)
    {
        using var decompressedStream = new MemoryStream();
        using var compressedStream = new MemoryStream(byteData);
        using var deflateStream = new DeflateStream(compressedStream, CompressionMode.Decompress);
        deflateStream.CopyTo(decompressedStream);
        decompressedStream.Position = 0;

        using var streamReader = new StreamReader(decompressedStream);
        /** /
        var response = streamReader.ReadToEnd();
        return response;
        /**/

        return streamReader.ReadToEnd();
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
        return response.Error != null ? new CallResult<OkexSocketPingPong>(response.Error) : new CallResult<OkexSocketPingPong>(result);
    }



    protected override async Task<CallResult<bool>> AuthenticateSocketAsync(SocketConnection s)
    {
        // Check Point
        //if (s.Authenticated)
        //    return new CallResult<bool>(true, null);

        // Check Point
        if (_credentials == null || _credentials.Key == null || _credentials.Secret == null || _credentials.PassPhrase == null)
            return new CallResult<bool>(new NoApiCredentialsError());

        // Get Credentials
        var key = _credentials.Key.GetString();
        var secret = _credentials.Secret.GetString();
        var passphrase = _credentials.PassPhrase.GetString();

        // Check Point
        if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(secret) || string.IsNullOrEmpty(passphrase))
            return new CallResult<bool>(new NoApiCredentialsError());

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
        var result = new CallResult<bool>(new ServerError("No response from server"));
        await s.SendAndWaitAsync(request, TimeSpan.FromSeconds(10), data =>
        {
            if ((string)data["event"] != "login")
                return false;

            var authResponse = Deserialize<OkexSocketResponse>(data);
            if (!authResponse)
            {
                _log.Write(LogLevel.Warning, "Authorization failed: " + authResponse.Error);
                result = new CallResult<bool>(authResponse.Error);
                return true;
            }
            if (!authResponse.Data.Success)
            {
                _log.Write(LogLevel.Warning, "Authorization failed: " + authResponse.Error.Message);
                result = new CallResult<bool>(new ServerError(authResponse.Error.Code.Value, authResponse.Error.Message));
                return true;
            }

            _log.Write(LogLevel.Debug, "Authorization completed");
            result = new CallResult<bool>(true);
            IsAuthendicated = true;
            return true;
        });

        return result;
    }

    protected override bool HandleQueryResponse<T>(SocketConnection s, object request, JToken data, out CallResult<T> callResult)
    {
        callResult = null;

        // Ping Request
        if (request.ToString() == "ping" && data.ToString() == "pong")
        {
            return true;
        }

        // Check for Error
        if (data is JObject && data["event"] != null && (string)data["event"]! == "error" && data["code"] != null && data["msg"] != null)
        {
            _log.Write(LogLevel.Warning, "Query failed: " + (string)data["msg"]!);
            callResult = new CallResult<T>(new ServerError($"{(string)data["code"]!}, {(string)data["msg"]!}"));
            return true;
        }

        // Login Request
        if (data is JObject && data["event"] != null && (string)data["event"]! == "login")
        {
            var desResult = Deserialize<T>(data);
            if (!desResult)
            {
                _log.Write(LogLevel.Warning, $"Failed to deserialize data: {desResult.Error}. Data: {data}");
                return false;
            }

            callResult = new CallResult<T>(desResult.Data);
            return true;
        }

        return false;
    }

    protected override bool HandleSubscriptionResponse(SocketConnection s, SocketSubscription subscription, object request, JToken message, out CallResult<object> callResult)
    {
        callResult = null;

        // Ping-Pong
        var json = message.ToString();
        if (json == "pong")
            return false;

        // Check for Error
        // 30040: {0} Channel : {1} doesn't exist
        if (message.HasValues && message["event"] != null && (string)message["event"]! == "error" && message["errorCode"] != null && (string)message["errorCode"]! == "30040")
        {
            _log.Write(LogLevel.Warning, "Subscription failed: " + (string)message["message"]!);
            callResult = new CallResult<object>(new ServerError($"{(string)message["errorCode"]!}, {(string)message["message"]!}"));
            return true;
        }

        // Check for Success
        if (message.HasValues && message["event"] != null && (string)message["event"]! == "subscribe" && message["arg"]["channel"] != null)
        {
            if (request is OkexSocketRequest socRequest)
            {
                if (socRequest.Arguments.FirstOrDefault().Channel == (string)message["arg"]["channel"]!)
                {
                    _log.Write(LogLevel.Debug, "Subscription completed");
                    callResult = new CallResult<object>(true);
                    return true;
                }
            }
        }

        return false;
    }

    protected override bool MessageMatchesHandler(SocketConnection s, JToken message, string identifier)
    {
        return true;
    }

    protected override bool MessageMatchesHandler(SocketConnection s, JToken message, object request)
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
    
    public new Task<CallResult<T>> QueryAsync<T>(object request, bool authenticated)
    {
        return base.QueryAsync<T>(request, authenticated);
    }

    public new Task<CallResult<UpdateSubscription>> SubscribeAsync<T>(object request, string identifier, bool authenticated, Action<DataEvent<T>> dataHandler, CancellationToken ct)
    {
        return base.SubscribeAsync(request, identifier, authenticated, dataHandler, ct);
    }

    protected override async Task<bool> UnsubscribeAsync(SocketConnection connection, SocketSubscription s)
    {
        if (s == null || s.Request == null)
            return false;

        var request = new OkexSocketRequest(OkexSocketOperation.Unsubscribe, ((OkexSocketRequest)s.Request).Arguments);
        await connection.SendAndWaitAsync(request, TimeSpan.FromSeconds(10), data =>
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

    protected override async Task<CallResult<SocketConnection>> GetSocketConnection(string address, bool authenticated)
    {
        address = authenticated
            ? "wss://ws.okx.com:8443/ws/v5/private"
            : "wss://ws.okx.com:8443/ws/v5/public";

        if (_options.DemoTradingService)
        {
            address = authenticated
                ? "wss://wspap.okx.com:8443/ws/v5/private?brokerId=9999"
                : "wss://wspap.okx.com:8443/ws/v5/public?brokerId=9999";
        }
        return await base.GetSocketConnection(address, authenticated);
    }
}