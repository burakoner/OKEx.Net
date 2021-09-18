using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Newtonsoft.Json;
using Okex.Net.Converters;
using Okex.Net.Enums;
using Okex.Net.Helpers;
using Okex.Net.Interfaces;
using Okex.Net.RestObjects;
using Okex.Net.SocketObjects.Containers;
using Okex.Net.SocketObjects.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Okex.Net
{
    public partial class OkexSocketClient : IOkexSocketClientIndex
    {
        #region Index WS-API

        #region Public Unsigned Feeds

        /// <summary>
        /// This channel is a public index channel that includes the k-lines and tickers for indices. It can be taken reference to for futures and spot trading.
        /// The indices currently available are all USD-denominated. The asset list includes: BTC, LTC, ETH, ETC, XRP, EOS, BTG.
        /// Get the public index ticker data
        /// </summary>
        /// <param name="symbol">The trading pair</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public virtual CallResult<UpdateSubscription> Index_SubscribeToTicker(string symbol, Action<OkexIndexTicker> onData) => Index_SubscribeToTicker_Async(symbol, onData).Result;
        /// <summary>
        /// This channel is a public index channel that includes the k-lines and tickers for indices. It can be taken reference to for futures and spot trading.
        /// The indices currently available are all USD-denominated. The asset list includes: BTC, LTC, ETH, ETC, XRP, EOS, BTG.
        /// Get the public index ticker data
        /// </summary>
        /// <param name="symbol">The trading pair</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public virtual async Task<CallResult<UpdateSubscription>> Index_SubscribeToTicker_Async(string symbol, Action<OkexIndexTicker> onData)
        {
            var internalHandler = new Action<DataEvent<OkexSocketUpdateResponse<IEnumerable<OkexIndexTicker>>>>(data =>
            {
                foreach (var d in data.Data.Data)
                    onData(d);
            });

            var request = new OkexSocketRequest(OkexSocketOperation.Subscribe, $"index/ticker:{symbol}");
            return await SubscribeAsync(request, null, false, internalHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// This channel is a public index channel that includes the k-lines and tickers for indices. It can be taken reference to for futures and spot trading.
        /// The indices currently available are all USD-denominated. The asset list includes: BTC, LTC, ETH, ETC, XRP, EOS, BTG.
        /// Get the public index ticker data
        /// </summary>
        /// <param name="symbol">The underlying symbols Maximum Length: 100 symbols</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public virtual CallResult<UpdateSubscription> Index_SubscribeToTicker(IEnumerable<string> symbol, Action<OkexOptionsTicker> onData) => Index_SubscribeToTicker_Async(symbol, onData).Result;
        /// <summary>
        /// This channel is a public index channel that includes the k-lines and tickers for indices. It can be taken reference to for futures and spot trading.
        /// The indices currently available are all USD-denominated. The asset list includes: BTC, LTC, ETH, ETC, XRP, EOS, BTG.
        /// Get the public index ticker data
        /// </summary>
        /// <param name="symbol">The underlying symbols Maximum Length: 100 symbols</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public virtual async Task<CallResult<UpdateSubscription>> Index_SubscribeToTicker_Async(IEnumerable<string> symbol, Action<OkexOptionsTicker> onData)
        {
            // To List
            var symbolList = symbol.ToList();

            // Check Point
            if (symbolList.Count == 0)
                throw new ArgumentException("Symbols must contain 1 element at least");

            if (symbolList.Count > 100)
                throw new ArgumentException("Symbols can contain maximum 100 elements");

            for (int i = 0; i < symbolList.Count; i++)
                symbolList[i] = symbolList[i].ValidateSymbol();

            var internalHandler = new Action<DataEvent<OkexSocketUpdateResponse<IEnumerable<OkexOptionsTicker>>>>(data =>
            {
                foreach (var d in data.Data.Data)
                    onData(d);
            });

            var tickerList = new List<string>();
            for (int i = 0; i < symbolList.Count; i++)
                tickerList.Add($"index/ticker:{symbolList[i]}");

            var request = new OkexSocketRequest(OkexSocketOperation.Subscribe, tickerList);
            return await SubscribeAsync(request, null, false, internalHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Get the kline information
        /// </summary>
        /// <param name="symbol">The trading pair</param>
        /// <param name="period">The period of a single candlestick</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public virtual CallResult<UpdateSubscription> Index_SubscribeToCandlesticks(string symbol, OkexSpotPeriod period, Action<OkexIndexCandle> onData) => Index_SubscribeToCandlesticks_Async(symbol, period, onData).Result;
        /// <summary>
        /// Get the kline information
        /// </summary>
        /// <param name="symbol">The trading pair</param>
        /// <param name="period">The period of a single candlestick</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public virtual async Task<CallResult<UpdateSubscription>> Index_SubscribeToCandlesticks_Async(string symbol, OkexSpotPeriod period, Action<OkexIndexCandle> onData)
        {
            var internalHandler = new Action<DataEvent<OkexSocketUpdateResponse<IEnumerable<OkexIndexCandleContainer>>>>(data =>
            {
                foreach (var d in data.Data.Data)
                {
                    d.Timestamp = DateTime.UtcNow;
                    d.Candle.Symbol = symbol.ToUpper(OkexGlobals.OkexCultureInfo);
                    onData(d.Candle);
                }
            });

            var period_s = JsonConvert.SerializeObject(period, new SpotPeriodConverter(false));
            var request = new OkexSocketRequest(OkexSocketOperation.Subscribe, $"index/candle{period_s}s:{symbol}");
            return await SubscribeAsync(request, null, false, internalHandler).ConfigureAwait(false);
        }

        #endregion

        #endregion
    }
}
