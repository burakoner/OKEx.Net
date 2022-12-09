namespace Okex.Net.Converters;

internal class DeliveryExerciseHistoryTypeConverter : BaseConverter<OkexDeliveryExerciseHistoryType>
{
    public DeliveryExerciseHistoryTypeConverter() : this(true) { }
    public DeliveryExerciseHistoryTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkexDeliveryExerciseHistoryType, string>> Mapping => new List<KeyValuePair<OkexDeliveryExerciseHistoryType, string>>
    {
        new KeyValuePair<OkexDeliveryExerciseHistoryType, string>(OkexDeliveryExerciseHistoryType.Delivery, "delivery"),
        new KeyValuePair<OkexDeliveryExerciseHistoryType, string>(OkexDeliveryExerciseHistoryType.Exercised, "exercised"),
        new KeyValuePair<OkexDeliveryExerciseHistoryType, string>(OkexDeliveryExerciseHistoryType.ExpiredOtm, "expired_otm"),
    };
}