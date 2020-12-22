using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Okex.Net.Helpers;
using Okex.Net.Interfaces;
using Okex.Net.RestObjects;
using Okex.Net.SocketObjects.Structure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Okex.Net
{
    public partial class OkexSocketClient: IOkexSocketClientMargin
    {
        #region Margin Trading WS-API

        #region Private Signed Feeds

        /// <summary>
        /// Retrieve the user's margin account information (login authentication required).
        /// </summary>
        /// <param name="symbol">Instrument Id</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public CallResult<UpdateSubscription> Margin_SubscribeToBalance(string symbol, Action<OkexMarginBalance> onData) => Margin_SubscribeToBalance_Async(symbol, onData).Result;
        /// <summary>
        /// Retrieve the user's margin account information (login authentication required).
        /// </summary>
        /// <param name="symbol">Instrument Id</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public async Task<CallResult<UpdateSubscription>> Margin_SubscribeToBalance_Async(string symbol, Action<OkexMarginBalance> onData)
        {
            symbol = symbol.ValidateSymbol();

            var internalHandler = new Action<OkexSocketUpdateResponse<IEnumerable<OkexMarginBalance>>>(data =>
            {
                foreach (var d in data.Data)
                    onData(d);
            });

            var request = new OkexSocketRequest(OkexSocketOperation.Subscribe, $"spot/margin_account:{symbol}");
            return await Subscribe(request, null, true, internalHandler).ConfigureAwait(false);
        }

        #endregion

        #endregion
    }
}
