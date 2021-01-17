using CryptoExchange.Net.Converters;
using Okex.Net.Enums;
using System.Collections.Generic;

namespace Okex.Net.Converters
{
    public class SpotPeriodConverter : BaseConverter<OkexSpotPeriod>
    {
        public SpotPeriodConverter() : this(true) { }
        public SpotPeriodConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OkexSpotPeriod, string>> Mapping => new List<KeyValuePair<OkexSpotPeriod, string>>
        {
            new KeyValuePair<OkexSpotPeriod, string>(OkexSpotPeriod.OneMinute, "60"),
            new KeyValuePair<OkexSpotPeriod, string>(OkexSpotPeriod.ThreeMinutes, "180"),
            new KeyValuePair<OkexSpotPeriod, string>(OkexSpotPeriod.FiveMinutes, "300"),
            new KeyValuePair<OkexSpotPeriod, string>(OkexSpotPeriod.FifteenMinutes, "900"),
            new KeyValuePair<OkexSpotPeriod, string>(OkexSpotPeriod.ThirtyMinutes, "1800"),
            new KeyValuePair<OkexSpotPeriod, string>(OkexSpotPeriod.OneHour, "3600"),
            new KeyValuePair<OkexSpotPeriod, string>(OkexSpotPeriod.TwoHours, "7200"),
            new KeyValuePair<OkexSpotPeriod, string>(OkexSpotPeriod.FourHours, "14400"),
            new KeyValuePair<OkexSpotPeriod, string>(OkexSpotPeriod.SixHours, "21600"),
            new KeyValuePair<OkexSpotPeriod, string>(OkexSpotPeriod.TwelveHours, "43200"),
            new KeyValuePair<OkexSpotPeriod, string>(OkexSpotPeriod.OneDay, "86400"),
            new KeyValuePair<OkexSpotPeriod, string>(OkexSpotPeriod.OneWeek, "604800"),
        };
    }
}