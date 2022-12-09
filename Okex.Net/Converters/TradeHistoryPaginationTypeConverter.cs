namespace Okex.Net.Converters;

internal class TradeHistoryPaginationTypeConverter : BaseConverter<OkexTradeHistoryPaginationType>
{
    public TradeHistoryPaginationTypeConverter() : this(true) { }
    public TradeHistoryPaginationTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkexTradeHistoryPaginationType, string>> Mapping => new List<KeyValuePair<OkexTradeHistoryPaginationType, string>>
    {
        new KeyValuePair<OkexTradeHistoryPaginationType, string>(OkexTradeHistoryPaginationType.TradeId, "1"),
        new KeyValuePair<OkexTradeHistoryPaginationType, string>(OkexTradeHistoryPaginationType.Timestamp, "2"),
    };
}