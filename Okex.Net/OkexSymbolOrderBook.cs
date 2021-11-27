using CryptoExchange.Net.Objects;
using CryptoExchange.Net.OrderBook;
using CryptoExchange.Net.Sockets;
using Microsoft.Extensions.Logging;
using Okex.Net.CoreObjects;
using Okex.Net.Enums;
using Okex.Net.RestObjects.Market;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Okex.Net
{
	/// <summary>
	/// Symbol order book implementation
	/// </summary>
	public class OkexSymbolOrderBook : SymbolOrderBook
	{
		private readonly OkexSocketClient socketClient;
		private readonly bool _socketOwner;
        private readonly OkexOrderBookType _orderBookType;
        /// <summary>
        /// Create a new order book instance
        /// </summary>
        /// <param name="symbol">The symbol of the order book</param>
        /// <param name="options">The options for the order book</param>
        public OkexSymbolOrderBook(string intrumentId, OkexOrderBookOptions? options = null) : base(intrumentId, options ?? new OkexOrderBookOptions())
        {
            socketClient = options?.SocketClient ?? new OkexSocketClient();
            _socketOwner = options?.SocketClient == null;
            Levels = options?.Limit;
            _orderBookType = options?.OrderBookType ?? OkexOrderBookType.OrderBook;
        }

        /// <inheritdoc />
        protected override async Task<CallResult<UpdateSubscription>> DoStartAsync()
        {
            var result = await socketClient.SubscribeToOrderBook_Async(Symbol, _orderBookType, HandleUpdate).ConfigureAwait(false);
            if (!result)
                return result;

            Status = OrderBookStatus.Syncing;

            var setResult = await WaitForSetOrderBookAsync(10000).ConfigureAwait(false);
            return setResult ? result : new CallResult<UpdateSubscription>(null, setResult.Error);
        }

        /// <inheritdoc />
        protected override async Task<CallResult<bool>> DoResyncAsync()
        {
            return await WaitForSetOrderBookAsync(10000).ConfigureAwait(false);
        }

        /// <inheritdoc />
        protected override void DoReset()
        {
        }

        private void HandleUpdate(OkexOrderBook data)
        {
            switch(data.Action)
			{
                case "update":
					{
                        UpdateOrderBook(data.Time.Ticks, data.Bids, data.Asks);
                        if (data.Checksum != null)
                        {
                            AddChecksum((int)data.Checksum.Value);
                        }
                        break;
					}
                case "partial":
                case null:
                case "snapshot":
					{
                        SetInitialOrderBook(data.Time.Ticks, data.Bids, data.Asks);
                        if (data.Checksum != null)
                        {
                            AddChecksum((int)data.Checksum.Value);
                        }
                        break;
					}
                default:
					{
                        log.Write(LogLevel.Warning, $"{Id} order book {Symbol} unknown action:'{data.Action}'), ignoring");
                        break;
					}
            }
        }
		protected override bool DoChecksum(int checksum)
		{
            var checkStringBuilder = new StringBuilder();
            using (var bidsEnumerator = bids.GetEnumerator())
            {
                using (var asksEnumerotor = asks.GetEnumerator())
                {
                    for (int i = 0; i < 25; i++)
                    {
                        if (bidsEnumerator.MoveNext())
                            checkStringBuilder.Append(bidsEnumerator.Current.Value.Price.ToString(System.Globalization.CultureInfo.InvariantCulture) + ":" + bidsEnumerator.Current.Value.Quantity.ToString(System.Globalization.CultureInfo.InvariantCulture) + ":");
                        if (asksEnumerotor.MoveNext())
                            checkStringBuilder.Append(asksEnumerotor.Current.Value.Price.ToString(System.Globalization.CultureInfo.InvariantCulture) + ":" + asksEnumerotor.Current.Value.Quantity.ToString(System.Globalization.CultureInfo.InvariantCulture) + ":");
                    }
                }
            }
            var checkString = checkStringBuilder.ToString().TrimEnd(':');
            var checkBytes = Encoding.ASCII.GetBytes(checkString);
            var checkHexCrc32 = Force.Crc32.Crc32Algorithm.Compute(checkBytes);
            var result = checkHexCrc32 == (uint)checksum;
            if (!result)
			{
                log.Write(LogLevel.Debug, $"{Id} order book {Symbol} failed checksum. Expected {checkHexCrc32}, received {checksum}");
            }
            return result;
		}

		/// <inheritdoc />
		public override void Dispose()
        {
            processBuffer.Clear();
            asks.Clear();
            bids.Clear();

            if (_socketOwner)
                socketClient?.Dispose();
        }
    }
}
