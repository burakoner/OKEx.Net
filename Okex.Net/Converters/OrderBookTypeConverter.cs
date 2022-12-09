namespace Okex.Net.Converters;

internal class OrderBookTypeConverter : BaseConverter<OkexOrderBookType>
{
    public OrderBookTypeConverter() : this(true) { }
    public OrderBookTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkexOrderBookType, string>> Mapping => new List<KeyValuePair<OkexOrderBookType, string>>
    {
        new KeyValuePair<OkexOrderBookType, string>(OkexOrderBookType.OrderBook, "books"),
        new KeyValuePair<OkexOrderBookType, string>(OkexOrderBookType.OrderBook_5, "books5"),
        new KeyValuePair<OkexOrderBookType, string>(OkexOrderBookType.OrderBook_50_l2_TBT, "books50-l2-tbt"),
        new KeyValuePair<OkexOrderBookType, string>(OkexOrderBookType.OrderBook_l2_TBT, "books-l2-tbt"),
        new KeyValuePair<OkexOrderBookType, string>(OkexOrderBookType.BBO_TBT, "bbo-tbt"),
    };
}