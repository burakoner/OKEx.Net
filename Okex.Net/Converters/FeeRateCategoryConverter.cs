namespace Okex.Net.Converters;

internal class FeeRateCategoryConverter : BaseConverter<OkexFeeRateCategory>
{
    public FeeRateCategoryConverter() : this(true) { }
    public FeeRateCategoryConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkexFeeRateCategory, string>> Mapping => new List<KeyValuePair<OkexFeeRateCategory, string>>
    {
        new KeyValuePair<OkexFeeRateCategory, string>(OkexFeeRateCategory.ClassA, "1"),
        new KeyValuePair<OkexFeeRateCategory, string>(OkexFeeRateCategory.ClassB, "2"),
        new KeyValuePair<OkexFeeRateCategory, string>(OkexFeeRateCategory.ClassC, "3"),
        new KeyValuePair<OkexFeeRateCategory, string>(OkexFeeRateCategory.ClassD, "4"),
    };
}