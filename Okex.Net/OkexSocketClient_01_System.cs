using CryptoExchange.Net.Objects;
using Okex.Net.Interfaces;
using Okex.Net.SocketObjects.Containers;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Okex.Net
{
    public partial class OkexSocketClient : IOkexSocketClientSystem
    {
        public virtual CallResult<OkexGeneralPingPongContainer> Ping() => PingAsync().Result;
        public virtual async Task<CallResult<OkexGeneralPingPongContainer>> PingAsync()
        {
            var pit = DateTime.UtcNow;
            var sw = Stopwatch.StartNew();
            var response = await QueryAsync<string>("ping", false).ConfigureAwait(true);
            var pot = DateTime.UtcNow;
            sw.Stop();

            var result = new OkexGeneralPingPongContainer { PingTime = pit, PongTime = pot, Latency = sw.Elapsed, PongMessage = response.Data };
            return new CallResult<OkexGeneralPingPongContainer>(result, response.Error);
        }
    }
}
