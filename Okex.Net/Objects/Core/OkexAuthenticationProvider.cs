namespace Okex.Net.Objects.Core;

public class OkexAuthenticationProvider : AuthenticationProvider
{
    private readonly HMACSHA256 encryptor;

    public OkexAuthenticationProvider(OkexApiCredentials credentials) : base(credentials)
    {
        if (credentials == null || credentials.Secret == null)
            throw new ArgumentException("No valid API credentials provided. Key/Secret needed.");

        encryptor = new HMACSHA256(Encoding.ASCII.GetBytes(credentials.Secret.GetString()));
    }

    public override void AuthenticateRequest(RestApiClient apiClient, Uri uri, HttpMethod method, Dictionary<string, object> providedParameters, bool auth, ArrayParametersSerialization arraySerialization, HttpMethodParameterPosition parameterPosition, out SortedDictionary<string, object> uriParameters, out SortedDictionary<string, object> bodyParameters, out Dictionary<string, string> headers)
    {
        uriParameters = parameterPosition == HttpMethodParameterPosition.InUri ? new SortedDictionary<string, object>(providedParameters) : new SortedDictionary<string, object>();
        bodyParameters = parameterPosition == HttpMethodParameterPosition.InBody ? new SortedDictionary<string, object>(providedParameters) : new SortedDictionary<string, object>();
        headers = new Dictionary<string, string>();

        // Get Clients
        var baseClient = ((OkexClientUnifiedApi)apiClient)._baseClient;

        // Check Point
        if (!(auth || baseClient.Options.SignPublicRequests))
            return;

        // Check Point
        if (Credentials == null || Credentials.Key == null || Credentials.Secret == null || ((OkexApiCredentials)Credentials).PassPhrase == null)
            throw new ArgumentException("No valid API credentials provided. Key/Secret/PassPhrase needed.");

        // Set Parameters
        uri = uri.SetParameters(uriParameters, arraySerialization);

        // Signature Body
        // var time = (DateTime.UtcNow.ToUnixTimeMilliSeconds() / 1000.0m).ToString(CultureInfo.InvariantCulture);
        var time = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.sssZ");
        var signtext = time + method.Method.ToUpper() + uri.PathAndQuery.Trim('?');

        if (method == HttpMethod.Post)
        {
            if (bodyParameters.Count == 1 && bodyParameters.Keys.First() == OkexClient.BodyParameterKey)
                signtext += JsonConvert.SerializeObject(bodyParameters[OkexClient.BodyParameterKey]);
            else
                // signtext += JsonConvert.SerializeObject(bodyParameters.OrderBy(p => p.Key).ToDictionary(p => p.Key, p => p.Value));
                signtext += JsonConvert.SerializeObject(bodyParameters);
        }

        // Compute Signature
        var signature = Base64Encode(encryptor.ComputeHash(Encoding.UTF8.GetBytes(signtext)));

        // Headers
        headers.Add("OK-ACCESS-KEY", Credentials.Key.GetString());
        headers.Add("OK-ACCESS-SIGN", signature);
        headers.Add("OK-ACCESS-TIMESTAMP", time);
        headers.Add("OK-ACCESS-PASSPHRASE", ((OkexApiCredentials)Credentials).PassPhrase.GetString());

        // Demo Trading Flag
        if (baseClient.Options.DemoTradingService)
            headers.Add("x-simulated-trading", "1");
    }

    public static string Base64Encode(byte[] plainBytes)
    {
        return Convert.ToBase64String(plainBytes);
    }

    public static string Base64Encode(string plainText)
    {
        var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
        return Convert.ToBase64String(plainTextBytes);
    }

    public static string Base64Decode(string base64EncodedData)
    {
        var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
        return Encoding.UTF8.GetString(base64EncodedBytes);
    }
}
