using CryptoExchange.Net;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Okex.Net.Converters;
using Okex.Net.RestObjects;
using Okex.Net.SocketObjects;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Okex.Net.Interfaces;
using Okex.Net.SocketObjects.Spot;
using System.Diagnostics;
using System.Text;
using System.Globalization;
using CryptoExchange.Net.Authentication;
using System.Security;
using System.Security.Cryptography;

namespace Okex.Net
{
    /// <summary>
    /// Client for the Okex socket API
    /// </summary>
    public partial class OkexSocketClient
    {
        #region Spot & Margin
        /// <summary>
        /// Retrieve the latest price, best bid & offer and 24-hours trading volume of a single contract.
        /// </summary>
        /// <param name="symbol">Trading pair symbol</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public CallResult<UpdateSubscription> Spot_SubscribeToTicker(string symbol, Action<OkexSpotTicker> onData) => Spot_SubscribeToTicker_Async(symbol, onData).Result;
        /// <summary>
        /// Retrieve the latest price, best bid & offer and 24-hours trading volume of a single contract.
        /// </summary>
		/// <param name="symbol">Trading pair symbol</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public async Task<CallResult<UpdateSubscription>> Spot_SubscribeToTicker_Async(string symbol, Action<OkexSpotTicker> onData)
        {
            symbol = symbol.ValidateSymbol();

            var internalHandler = new Action<OkexSocketUpdateResponse<IEnumerable<OkexSpotTicker>>>(data =>
            {
                foreach (var d in data.Data)
                {
                    onData(d);
                }
            });

            var request = new OkexSocketRequest(OkexSocketOperation.Subscribe, $"spot/ticker:{symbol}");
            return await Subscribe(request, null, false, internalHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Subscribes tickers for multiple symbols
        /// </summary>
        /// <param name="symbols">Trading pair symbols Maximum Length: 100 symbols</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public CallResult<UpdateSubscription> Spot_SubscribeToTickers(IEnumerable<string> symbols, Action<OkexSpotTicker> onData) => Spot_SubscribeToTickers_Async(symbols, onData).Result;
        /// <summary>
        /// Subscribes tickers for multiple symbols
        /// </summary>
        /// <param name="symbols">Trading pair symbols. Maximum Length: 100 symbols</param>
        /// <param name="onData">The handler for updates</param>
        public async Task<CallResult<UpdateSubscription>> Spot_SubscribeToTickers_Async(IEnumerable<string> symbols, Action<OkexSpotTicker> onData)
        {
            // To List
            var symbolList = symbols.ToList();

            // Check Point
            if (symbolList.Count > 100)
                throw new ArgumentException("Symbols can contain maximum 100 elements");

            for (int i=0; i< symbolList.Count;i++)
                symbolList[i] = symbolList[i].ValidateSymbol();

            var internalHandler = new Action<OkexSocketUpdateResponse<IEnumerable<OkexSpotTicker>>>(data =>
            {
                foreach (var d in data.Data)
                {
                    onData(d);
                }
            });

            var tickerList = new List<string>();
            for (int i = 0; i < symbolList.Count; i++)
                tickerList.Add($"spot/ticker:{symbolList[i]}");

            var request = new OkexSocketRequest(OkexSocketOperation.Subscribe, tickerList);
            return await Subscribe(request, null, false, internalHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve the candlestick data
        /// </summary>
		/// <param name="symbol">Trading pair symbol</param>
        /// <param name="period">The period of a single candlestick</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public CallResult<UpdateSubscription> Spot_SubscribeToCandlesticks(string symbol, SpotPeriod period, Action<OkexSpotCandle> onData) => Spot_SubscribeToCandlesticks_Async(symbol, period, onData).Result;
        /// <summary>
        /// Retrieve the candlestick data
        /// </summary>
		/// <param name="symbol">Trading pair symbol</param>
        /// <param name="period">The period of a single candlestick</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public async Task<CallResult<UpdateSubscription>> Spot_SubscribeToCandlesticks_Async(string symbol, SpotPeriod period, Action<OkexSpotCandle> onData)
        {
            symbol = symbol.ValidateSymbol();

            var internalHandler = new Action<OkexSocketUpdateResponse<IEnumerable<OkexSpotCandleContainer>>>(data =>
            {
                foreach (var d in data.Data)
                {
                    d.Timestamp = DateTime.UtcNow;
                    d.Candle.Symbol = symbol.ToUpper(OkexHelpers.OkexCultureInfo);
                    onData(d.Candle);
                }
            });

            var request = new OkexSocketRequest(OkexSocketOperation.Subscribe, $"spot/candle{JsonConvert.SerializeObject(period, new SpotPeriodConverter(false))}s:{symbol}");
            return await Subscribe(request, null, false, internalHandler).ConfigureAwait(false);
        }


        /// <summary>
        /// Get the filled orders data
        /// </summary>
		/// <param name="symbol">Trading pair symbol</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public CallResult<UpdateSubscription> Spot_SubscribeToTrades(string symbol, Action<OkexSpotTrade> onData) => Spot_SubscribeToTrades_Async(symbol, onData).Result;
        /// <summary>
        /// Get the filled orders data
        /// </summary>
		/// <param name="symbol">Trading pair symbol</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public async Task<CallResult<UpdateSubscription>> Spot_SubscribeToTrades_Async(string symbol, Action<OkexSpotTrade> onData)
        {
            symbol = symbol.ValidateSymbol();

            var internalHandler = new Action<OkexSocketUpdateResponse<IEnumerable<OkexSpotTrade>>>(data =>
            {
                foreach (var d in data.Data)
                {
                    onData(d);
                }
            });

            var request = new OkexSocketRequest(OkexSocketOperation.Subscribe, $"spot/trade:{symbol}");
            return await Subscribe(request, null, false, internalHandler).ConfigureAwait(false);
        }


        /// <summary>
        /// Depth-Five: Back to the previous five entries of depth data,This data is snapshot data per 100 milliseconds.For every 100 milliseconds, we will snapshot and push 5 entries of market depth data of the current order book.
        /// Depth-All: After subscription, 400 entries of market depth data of the order book will first be pushed. Subsequently every 100 milliseconds we will snapshot and push entries that have changed during this time.
        /// </summary>
		/// <param name="symbol">Trading pair symbol</param>
        /// <param name="depth">Order Book Depth</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public CallResult<UpdateSubscription> Spot_SubscribeToOrderBook(string symbol, SpotOrderBookDepth depth, Action<OkexSpotOrderBook> onData) => Spot_SubscribeToTrades_Async(symbol, depth, onData).Result;
        /// <summary>
        /// Depth-Five: Back to the previous five entries of depth data,This data is snapshot data per 100 milliseconds.For every 100 milliseconds, we will snapshot and push 5 entries of market depth data of the current order book.
        /// Depth-All: After subscription, 400 entries of market depth data of the order book will first be pushed. Subsequently every 100 milliseconds we will snapshot and push entries that have changed during this time.
        /// </summary>
		/// <param name="symbol">Trading pair symbol</param>
        /// <param name="depth">Order Book Depth</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public async Task<CallResult<UpdateSubscription>> Spot_SubscribeToTrades_Async(string symbol, SpotOrderBookDepth depth, Action<OkexSpotOrderBook> onData)
        {
            symbol = symbol.ValidateSymbol();

            var internalHandler = new Action<OkexSocketOrderBookUpdate>(data =>
            {
                foreach (var d in data.Data)
                {
                    d.Symbol = symbol.ToUpper(OkexHelpers.OkexCultureInfo);
                    d.DataType = depth == SpotOrderBookDepth.Depth5 ? SpotOrderBookDataType.DepthTop5 : data.DataType;
                    onData(d);
                }
            });

            var channel = "depth";
            if(depth == SpotOrderBookDepth.Depth5) channel = "depth5";
            else if(depth == SpotOrderBookDepth.Depth400) channel = "depth";
            else if (depth == SpotOrderBookDepth.TickByTick) channel = "depth_l2_tbt";
            var request = new OkexSocketRequest(OkexSocketOperation.Subscribe, $"spot/{channel}:{symbol}");
            return await Subscribe(request, null, false, internalHandler).ConfigureAwait(false);
        }
        #endregion
    }
}
