namespace Okex.Net.Converters;

internal class LiquidationStateConverter : BaseConverter<OkexLiquidationState>
{
    public LiquidationStateConverter() : this(true) { }
    public LiquidationStateConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkexLiquidationState, string>> Mapping => new List<KeyValuePair<OkexLiquidationState, string>>
    {
        new KeyValuePair<OkexLiquidationState, string>(OkexLiquidationState.Unfilled, "unfilled"),
        new KeyValuePair<OkexLiquidationState, string>(OkexLiquidationState.Filled, "filled"),
    };
}