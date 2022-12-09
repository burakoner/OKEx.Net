namespace Okex.Net.Objects.Core;

public class OkexSocketClientOptions : ClientOptions
{
    public bool DemoTradingService { get; set; } = false;

    public static OkexSocketClientOptions Default { get; set; } = new OkexSocketClientOptions()
    {
        //SocketSubscriptionsCombineTarget = 100,
        //MaxSocketConnections = 50
    };

    public new OkexApiCredentials ApiCredentials
    {
        get => (OkexApiCredentials)base.ApiCredentials;
        set => base.ApiCredentials = value;
    }

    private OkexSocketApiClientOptions _unifiedStreamsOptions = new OkexSocketApiClientOptions();
    public OkexSocketApiClientOptions UnifiedStreamsOptions
    {
        get => _unifiedStreamsOptions;
        set => _unifiedStreamsOptions = new OkexSocketApiClientOptions(_unifiedStreamsOptions, value);
    }

    public OkexSocketClientOptions() : this(Default)
    {
    }

    internal OkexSocketClientOptions(OkexSocketClientOptions baseOn) : base(baseOn)
    {
        if (baseOn == null)
            return;

        ApiCredentials = (OkexApiCredentials)baseOn.ApiCredentials?.Copy();
        _unifiedStreamsOptions = new OkexSocketApiClientOptions(baseOn.UnifiedStreamsOptions, null);
    }
}

public class OkexSocketApiClientOptions : SocketApiClientOptions
{
    public new OkexApiCredentials ApiCredentials
    {
        get => (OkexApiCredentials)base.ApiCredentials;
        set => base.ApiCredentials = value;
    }

    public OkexSocketApiClientOptions()
    {
    }

    internal OkexSocketApiClientOptions(OkexSocketApiClientOptions baseOn, OkexSocketApiClientOptions newValues) : base(baseOn, newValues)
    {
        ApiCredentials = (OkexApiCredentials)newValues?.ApiCredentials?.Copy() ?? (OkexApiCredentials)baseOn.ApiCredentials?.Copy();
    }
}
