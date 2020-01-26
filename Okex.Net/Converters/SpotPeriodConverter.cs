using CryptoExchange.Net.Converters;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    internal class SpotPeriodConverter : BaseConverter<SpotPeriod>
    {
        public SpotPeriodConverter() : this(true) { }
        public SpotPeriodConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<SpotPeriod, string>> Mapping => new List<KeyValuePair<SpotPeriod, string>>
        {
            new KeyValuePair<SpotPeriod, string>(SpotPeriod.OneMinute, "60"),
            new KeyValuePair<SpotPeriod, string>(SpotPeriod.ThreeMinutes, "180"),
            new KeyValuePair<SpotPeriod, string>(SpotPeriod.FiveMinutes, "300"),
            new KeyValuePair<SpotPeriod, string>(SpotPeriod.FifteenMinutes, "900"),
            new KeyValuePair<SpotPeriod, string>(SpotPeriod.ThirtyMinutes, "1800"),
            new KeyValuePair<SpotPeriod, string>(SpotPeriod.OneHour, "3600"),
            new KeyValuePair<SpotPeriod, string>(SpotPeriod.TwoHours, "7200"),
            new KeyValuePair<SpotPeriod, string>(SpotPeriod.FourHours, "14400"),
            new KeyValuePair<SpotPeriod, string>(SpotPeriod.SixHours, "21600"),
            new KeyValuePair<SpotPeriod, string>(SpotPeriod.TwelveHours, "43200"),
            new KeyValuePair<SpotPeriod, string>(SpotPeriod.OneDay, "86400"),
            new KeyValuePair<SpotPeriod, string>(SpotPeriod.OneWeek, "604800"),
        };
    }
}