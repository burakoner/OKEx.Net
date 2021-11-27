using CryptoExchange.Net.Objects;
using Okex.Net.Enums;

namespace Okex.Net.CoreObjects
{
	/// <summary>
	/// Order book options
	/// </summary>
	public class OkexOrderBookOptions : OrderBookOptions
    {
        /// <summary>
        /// The client to use for the socket connection. When using the same client for multiple order books the connection can be shared.
        /// </summary>
        public OkexSocketClient? SocketClient { get; }

        /// <summary>
        /// The top amount of results to keep in sync. If for example limit=10 is used, the order book will contain the 10 best bids and 10 best asks. Leaving this null will sync the full order book
        /// </summary>
        public int? Limit { get; set; }

        /// <summary>
        /// Order Book Type
        /// Use books for 400 depth levels, book5 for 5 depth levels, books50-l2-tbt tick-by-tick 50 depth levels, and books-l2-tbt for tick-by-tick 400 depth levels.
        /// Default to OrderBook.
        /// </summary>
        public OkexOrderBookType OrderBookType { get; set; }
        /// <summary>
        /// Create new options
        /// </summary>
        /// <param name="client">The client to use for the socket connection. When using the same client for multiple order books the connection can be shared.</param>
        public OkexOrderBookOptions(int? limit = null, OkexOrderBookType orderBookType = OkexOrderBookType.OrderBook, OkexSocketClient? client = null) : base("Okex", false, false)
        {
            Limit = limit;
            OrderBookType = orderBookType;
            SocketClient = client;
        }
    }
}