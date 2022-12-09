namespace Okex.Net.Converters;

internal class PeriodConverter : BaseConverter<OkexPeriod>
{
    public PeriodConverter() : this(true) { }
    public PeriodConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkexPeriod, string>> Mapping => new List<KeyValuePair<OkexPeriod, string>>
    {
        new KeyValuePair<OkexPeriod, string>(OkexPeriod.OneMinute, "1m"),
        new KeyValuePair<OkexPeriod, string>(OkexPeriod.ThreeMinutes, "3m"),
        new KeyValuePair<OkexPeriod, string>(OkexPeriod.FiveMinutes, "5m"),
        new KeyValuePair<OkexPeriod, string>(OkexPeriod.FifteenMinutes, "15m"),
        new KeyValuePair<OkexPeriod, string>(OkexPeriod.ThirtyMinutes, "30m"),
        new KeyValuePair<OkexPeriod, string>(OkexPeriod.OneHour, "1H"),
        new KeyValuePair<OkexPeriod, string>(OkexPeriod.TwoHours, "2H"),
        new KeyValuePair<OkexPeriod, string>(OkexPeriod.FourHours, "4H"),
        new KeyValuePair<OkexPeriod, string>(OkexPeriod.SixHours, "6H"),
        new KeyValuePair<OkexPeriod, string>(OkexPeriod.TwelveHours, "12H"),
        new KeyValuePair<OkexPeriod, string>(OkexPeriod.OneDay, "1D"),
        new KeyValuePair<OkexPeriod, string>(OkexPeriod.OneWeek, "1W"),
        new KeyValuePair<OkexPeriod, string>(OkexPeriod.OneMonth, "1M"),
        new KeyValuePair<OkexPeriod, string>(OkexPeriod.ThreeMonths, "3M"),
        new KeyValuePair<OkexPeriod, string>(OkexPeriod.SixMonths, "6M"),
        new KeyValuePair<OkexPeriod, string>(OkexPeriod.OneYear, "1Y"),
    };
}