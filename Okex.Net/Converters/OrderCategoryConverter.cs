namespace Okex.Net.Converters;

internal class OrderCategoryConverter : BaseConverter<OkexOrderCategory>
{
    public OrderCategoryConverter() : this(true) { }
    public OrderCategoryConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkexOrderCategory, string>> Mapping => new List<KeyValuePair<OkexOrderCategory, string>>
    {
        new KeyValuePair<OkexOrderCategory, string>(OkexOrderCategory.TWAP, "twap"),
        new KeyValuePair<OkexOrderCategory, string>(OkexOrderCategory.ADL, "adl"),
        new KeyValuePair<OkexOrderCategory, string>(OkexOrderCategory.FullLiquidation, "full_liquidation"),
        new KeyValuePair<OkexOrderCategory, string>(OkexOrderCategory.PartialLiquidation, "partial_liquidation"),
        new KeyValuePair<OkexOrderCategory, string>(OkexOrderCategory.Delivery, "delivery"),
    };
}