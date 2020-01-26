using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Okex.Net.RestObjects;
using Okex.Net.SocketObjects;

namespace Okex.Net.Interfaces
{
    /// <summary>
    /// Interface for the Okex socket client
    /// </summary>
    public interface IOkexSocketClient: ISocketClient
    {
    }
}