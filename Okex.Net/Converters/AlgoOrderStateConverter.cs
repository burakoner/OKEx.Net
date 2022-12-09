namespace Okex.Net.Converters;

internal class AlgoOrderStateConverter : BaseConverter<OkexAlgoOrderState>
{
    public AlgoOrderStateConverter() : this(true) { }
    public AlgoOrderStateConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkexAlgoOrderState, string>> Mapping => new List<KeyValuePair<OkexAlgoOrderState, string>>
    {
        new KeyValuePair<OkexAlgoOrderState, string>(OkexAlgoOrderState.Live, "live"),
        new KeyValuePair<OkexAlgoOrderState, string>(OkexAlgoOrderState.Pause, "pause"),
        new KeyValuePair<OkexAlgoOrderState, string>(OkexAlgoOrderState.Effective, "effective"),
        new KeyValuePair<OkexAlgoOrderState, string>(OkexAlgoOrderState.PartiallyEffective, "partially_effective"),
        new KeyValuePair<OkexAlgoOrderState, string>(OkexAlgoOrderState.Canceled, "canceled"),
        new KeyValuePair<OkexAlgoOrderState, string>(OkexAlgoOrderState.Failed, "order_failed"),
    };
}