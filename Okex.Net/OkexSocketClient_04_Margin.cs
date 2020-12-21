using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Okex.Net.Converters;
using Okex.Net.RestObjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Okex.Net.SocketObjects.Structure;
using Okex.Net.SocketObjects.Containers;
using Okex.Net.Helpers;
using Okex.Net.Enums;

namespace Okex.Net
{
    public partial class OkexSocketClient
    {
        #region Margin Trading WS-API

        #region Private Signed Feeds

        public CallResult<UpdateSubscription> Margin_SubscribeToBalance(string symbol, Action<OkexMarginBalance> onData) => Margin_SubscribeToBalance_Async(symbol, onData).Result;
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
