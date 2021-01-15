using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Okex.Net.CoreObjects;
using Okex.Net.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Okex.Net
{
    /// <summary>
    /// Client for the Okex REST API
    /// </summary>
    public partial class OkexClient : RestClient, IRestClient, IOkexClient
    {
        #region Public Fields
        /// <summary>
        /// Whether public requests should be signed if ApiCredentials are provided. Needed for accurate rate limiting.
        /// </summary>
        public bool SignPublicRequests { get; }

        /// <summary>
        /// Flag for sending data without root key
        /// </summary>
        public static readonly string BodyParameterKey = "<BODY>";
        #endregion

        #region Private Fields
        private static OkexClientOptions defaultOptions = new OkexClientOptions();
        private static OkexClientOptions DefaultOptions => defaultOptions.Copy();
        private readonly CultureInfo ci = CultureInfo.InvariantCulture;
        #endregion

        #region Constructor/Destructor
        /// <summary>
        /// Create a new instance of OkexClient using the default options
        /// </summary>
        public OkexClient() : this(DefaultOptions)
        {
        }

        /// <summary>
        /// Create a new instance of the OkexClient with the provided options
        /// </summary>
        public OkexClient(OkexClientOptions options) : base("Okex", options, options.ApiCredentials == null ? null : new OkexAuthenticationProvider(options.ApiCredentials, "", options.SignPublicRequests, ArrayParametersSerialization.Array))
        {
            SignPublicRequests = options.SignPublicRequests;
        }
        #endregion

        #region Common Methods
        /// <summary>
        /// Sets the default options to use for new clients
        /// </summary>
        /// <param name="options">The options to use for new clients</param>
        public static void SetDefaultOptions(OkexClientOptions options)
        {
            defaultOptions = options;
        }

        /// <summary>
        /// Set the API key and secret
        /// </summary>
        /// <param name="apiKey">The api key</param>
        /// <param name="apiSecret">The api secret</param>
        /// <param name="passPhrase">The passphrase you specified when creating the API key</param>
        public void SetApiCredentials(string apiKey, string apiSecret, string passPhrase)
        {
            SetAuthenticationProvider(new OkexAuthenticationProvider(new ApiCredentials(apiKey, apiSecret), passPhrase, SignPublicRequests, ArrayParametersSerialization.Array));
        }
        #endregion

        #region Core Methods
        protected override IRequest ConstructRequest(Uri uri, HttpMethod method, Dictionary<string, object>? parameters, bool signed, PostParameters postPosition, ArrayParametersSerialization arraySerialization, int requestId)
        {
            if (parameters == null)
                parameters = new Dictionary<string, object>();

            var uriString = uri.ToString();
            if (authProvider != null)
                parameters = authProvider.AddAuthenticationToParameters(uriString, method, parameters, signed, postPosition, arraySerialization);

            if ((method == HttpMethod.Get || method == HttpMethod.Delete || postParametersPosition == PostParameters.InUri) && parameters?.Any() == true)
                uriString += "?" + parameters.CreateParamString(true, arraySerialization);

            if (method == HttpMethod.Post && signed)
            {
                var uriParamNames = new[] { "AccessKeyId", "SignatureMethod", "SignatureVersion", "Timestamp", "Signature" };
                var uriParams = parameters.Where(p => uriParamNames.Contains(p.Key)).ToDictionary(k => k.Key, k => k.Value);
                uriString += "?" + uriParams.CreateParamString(true, ArrayParametersSerialization.MultipleValues);
                parameters = parameters.Where(p => !uriParamNames.Contains(p.Key)).ToDictionary(k => k.Key, k => k.Value);
            }

            var contentType = requestBodyFormat == RequestBodyFormat.Json ? Constants.JsonContentHeader : Constants.FormContentHeader;
            var request = RequestFactory.Create(method, uriString, requestId);
            request.Accept = Constants.JsonContentHeader;

            var headers = new Dictionary<string, string>();
            if (authProvider != null)
                headers = authProvider.AddAuthenticationToHeaders(uriString, method, parameters!, signed, postPosition, arraySerialization);

            foreach (var header in headers)
                request.AddHeader(header.Key, header.Value);

            if ((method == HttpMethod.Post || method == HttpMethod.Put) && postParametersPosition != PostParameters.InUri)
            {
                if (parameters?.Any() == true)
                    WriteParamBody(request, parameters, contentType);
                else
                    request.SetContent(requestBodyEmptyContent, contentType);
            }

            return request;
        }

        protected override void WriteParamBody(IRequest request, Dictionary<string, object> parameters, string contentType)
        {
            if (requestBodyFormat == RequestBodyFormat.Json)
            {
                if (parameters.Count == 1 && parameters.Keys.First() == BodyParameterKey)
                {
                    var stringData = JsonConvert.SerializeObject(parameters[BodyParameterKey]);
                    request.SetContent(stringData, contentType);
                }
                else
                {
                    var stringData = JsonConvert.SerializeObject(parameters.OrderBy(p => p.Key).ToDictionary(p => p.Key, p => p.Value));
                    request.SetContent(stringData, contentType);
                }
            }
            else if (requestBodyFormat == RequestBodyFormat.FormData)
            {
                var formData = HttpUtility.ParseQueryString(string.Empty);
                foreach (var kvp in parameters.OrderBy(p => p.Key))
                {
                    if (kvp.Value.GetType().IsArray)
                    {
                        var array = (Array)kvp.Value;
                        foreach (var value in array)
                            formData.Add(kvp.Key, value.ToString());
                    }
                    else
                        formData.Add(kvp.Key, kvp.Value.ToString());
                }
                var stringData = formData.ToString();
                request.SetContent(stringData, contentType);
            }
        }

        protected override Error ParseErrorResponse(JToken error)
        {
            if (error["code"] == null || error["message"] == null)
                return new ServerError(error.ToString());

            return new ServerError((int)error["code"]!, (string)error["message"]!);
        }

        protected Uri GetUrl(string endpoint, string param = "")
        {
            var x = endpoint.IndexOf('<');
            var y = endpoint.IndexOf('>');
            if (x > -1 && y > -1) endpoint = endpoint.Replace(endpoint.Substring(x, y - x + 1), param);

            return new Uri($"{BaseAddress.TrimEnd('/')}/{endpoint}");
        }

        private static long ToUnixTimestamp(DateTime time)
        {
            return (long)(time - new DateTime(1970, 1, 1)).TotalMilliseconds;
        }

        #endregion

    }
}
