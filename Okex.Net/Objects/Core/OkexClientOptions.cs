namespace Okex.Net.Objects.Core;

public class OkexClientOptions : ClientOptions
{
    public bool DemoTradingService { get; set; } = false;
    public bool SignPublicRequests { get; set; } = false;

    public static OkexClientOptions Default { get; set; } = new OkexClientOptions();


    private OkexRestApiClientOptions _unifiedApiOptions = new OkexRestApiClientOptions(OkexApiAddresses.Default.UnifiedAddress)
    {
        RateLimiters = new List<IRateLimiter>
        {
            new RateLimiter()
            .AddPartialEndpointLimit("/api/v5/trade/order", 60, TimeSpan.FromSeconds(2), null, true, true)
        }
    };

    public new OkexApiCredentials ApiCredentials
    {
        get => (OkexApiCredentials)base.ApiCredentials;
        set => base.ApiCredentials = value;
    }

    public OkexRestApiClientOptions UnifiedApiOptions
    {
        get => _unifiedApiOptions;
        set => _unifiedApiOptions = new OkexRestApiClientOptions(_unifiedApiOptions, value);
    }

    public OkexClientOptions() : this(Default)
    {
    }

    internal OkexClientOptions(OkexClientOptions baseOn) : base(baseOn)
    {
        if (baseOn == null)
            return;

        DemoTradingService = baseOn.DemoTradingService;
        SignPublicRequests = baseOn.SignPublicRequests;

        ApiCredentials = (OkexApiCredentials)baseOn.ApiCredentials?.Copy();
        _unifiedApiOptions = new OkexRestApiClientOptions(baseOn.UnifiedApiOptions, null);
    }
}

public class OkexRestApiClientOptions : RestApiClientOptions
{
    public new OkexApiCredentials ApiCredentials
    {
        get => (OkexApiCredentials)base.ApiCredentials;
        set => base.ApiCredentials = value;
    }

    public OkexRestApiClientOptions()
    {
    }

    internal OkexRestApiClientOptions(string baseAddress) : base(baseAddress)
    {
    }

    internal OkexRestApiClientOptions(OkexRestApiClientOptions baseOn, OkexRestApiClientOptions newValues) : base(baseOn, newValues)
    {
        ApiCredentials = (OkexApiCredentials)newValues?.ApiCredentials?.Copy() ?? (OkexApiCredentials)baseOn.ApiCredentials?.Copy();
    }
}
