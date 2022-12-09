namespace Okex.Net.Converters;

internal class SavingActionSideConverter : BaseConverter<OkexSavingActionSide>
{
    public SavingActionSideConverter() : this(true) { }
    public SavingActionSideConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkexSavingActionSide, string>> Mapping => new List<KeyValuePair<OkexSavingActionSide, string>>
    {
        new KeyValuePair<OkexSavingActionSide, string>(OkexSavingActionSide.Purchase, "purchase"),
        new KeyValuePair<OkexSavingActionSide, string>(OkexSavingActionSide.Redempt, "redempt"),
    };
}