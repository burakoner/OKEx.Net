namespace Okex.Net.Objects.Core;

public class OkexSocketRequest
{
    [JsonProperty("op"), JsonConverter(typeof(OkexSocketOperationConverter))]
    public OkexSocketOperation Operation { get; set; }

    [JsonProperty("args")]
    public List<OkexSocketRequestArgument> Arguments { get; set; }

    public OkexSocketRequest(OkexSocketOperation op, params OkexSocketRequestArgument[] args)
    {
        Operation = op;
        Arguments = args.ToList();
    }

    public OkexSocketRequest(OkexSocketOperation op, IEnumerable<OkexSocketRequestArgument> args)
    {
        Operation = op;
        Arguments = args.ToList();
    }

    public OkexSocketRequest(OkexSocketOperation op, string channel)
    {
        Operation = op;
        Arguments = new List<OkexSocketRequestArgument>();
        Arguments.Add(new OkexSocketRequestArgument(channel));
    }

    public OkexSocketRequest(OkexSocketOperation op, string channel, string instrumentId)
    {
        Operation = op;
        Arguments = new List<OkexSocketRequestArgument>();
        Arguments.Add(new OkexSocketRequestArgument(channel, instrumentId));
    }

    public OkexSocketRequest(OkexSocketOperation op, string channel, string underlying, string instrumentId)
    {
        Operation = op;
        Arguments = new List<OkexSocketRequestArgument>();
        Arguments.Add(new OkexSocketRequestArgument(channel, underlying, instrumentId));
    }

    public OkexSocketRequest(OkexSocketOperation op, string channel, OkexInstrumentType instrumentType)
    {
        Operation = op;
        Arguments = new List<OkexSocketRequestArgument>();
        Arguments.Add(new OkexSocketRequestArgument(channel, instrumentType));
    }

    public OkexSocketRequest(OkexSocketOperation op, string channel, OkexInstrumentType instrumentType, string underlying)
    {
        Operation = op;
        Arguments = new List<OkexSocketRequestArgument>();
        Arguments.Add(new OkexSocketRequestArgument(channel, instrumentType, underlying));
    }

}

public class OkexSocketRequestArgument
{
    [JsonProperty("channel")]
    public string Channel { get; set; }

    [JsonProperty("uly", NullValueHandling = NullValueHandling.Ignore)]
    public string Underlying { get; set; }

    [JsonProperty("instId", NullValueHandling = NullValueHandling.Ignore)]
    public string InstrumentId { get; set; }

    [JsonProperty("instType", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(InstrumentTypeConverter))]
    public OkexInstrumentType? InstrumentType { get; set; }

    public OkexSocketRequestArgument()
    {
    }

    public OkexSocketRequestArgument(string channel)
    {
        if (!string.IsNullOrEmpty(channel)) Channel = channel;
    }

    public OkexSocketRequestArgument(string channel, string instrumentId)
    {
        if (!string.IsNullOrEmpty(channel)) Channel = channel;
        if (!string.IsNullOrEmpty(instrumentId)) InstrumentId = instrumentId;
    }

    public OkexSocketRequestArgument(string channel, string underlying, string instrumentId)
    {
        if (!string.IsNullOrEmpty(channel)) Channel = channel;
        if (!string.IsNullOrEmpty(underlying)) Underlying = underlying;
        if (!string.IsNullOrEmpty(instrumentId)) InstrumentId = instrumentId;
    }

    public OkexSocketRequestArgument(string channel, OkexInstrumentType? instrumentType)
    {
        if (!string.IsNullOrEmpty(channel)) Channel = channel;
        if (instrumentType != null) InstrumentType = instrumentType;
    }

    public OkexSocketRequestArgument(string channel, OkexInstrumentType? instrumentType, string underlying)
    {
        if (!string.IsNullOrEmpty(channel)) Channel = channel;
        if (!string.IsNullOrEmpty(underlying)) Underlying = underlying;
        if (instrumentType != null) InstrumentType = instrumentType;
    }
}

public class OkexSocketAuthRequest
{
    [JsonProperty("op"), JsonConverter(typeof(OkexSocketOperationConverter))]
    public OkexSocketOperation Operation { get; set; }

    [JsonProperty("args")]
    public List<OkexSocketAuthRequestArgument> Arguments { get; set; }

    public OkexSocketAuthRequest()
    {
    }

    public OkexSocketAuthRequest(OkexSocketOperation op, params OkexSocketAuthRequestArgument[] args)
    {
        Operation = op;
        Arguments = args.ToList();
    }

    public OkexSocketAuthRequest(OkexSocketOperation op, IEnumerable<OkexSocketAuthRequestArgument> args)
    {
        Operation = op;
        Arguments = args.ToList();
    }
}

public class OkexSocketAuthRequestArgument
{
    [JsonProperty("apiKey", NullValueHandling = NullValueHandling.Ignore)]
    public string ApiKey { get; set; }

    [JsonProperty("passphrase", NullValueHandling = NullValueHandling.Ignore)]
    public string Passphrase { get; set; }

    [JsonProperty("timestamp", NullValueHandling = NullValueHandling.Ignore)]
    public string Timestamp { get; set; }

    [JsonProperty("sign", NullValueHandling = NullValueHandling.Ignore)]
    public string Signature { get; set; }

    public OkexSocketAuthRequestArgument()
    {
    }
}

public enum OkexSocketOperation
{
    Subscribe,
    Unsubscribe,
    Login,
}

public class OkexSocketOperationConverter : BaseConverter<OkexSocketOperation>
{
    public OkexSocketOperationConverter() : this(true) { }
    public OkexSocketOperationConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkexSocketOperation, string>> Mapping => new List<KeyValuePair<OkexSocketOperation, string>>
    {
        new KeyValuePair<OkexSocketOperation, string>(OkexSocketOperation.Subscribe, "subscribe"),
        new KeyValuePair<OkexSocketOperation, string>(OkexSocketOperation.Unsubscribe, "unsubscribe"),
        new KeyValuePair<OkexSocketOperation, string>(OkexSocketOperation.Login, "login"),
    };
}
