# ![Icon](https://github.com/burakoner/OKEx.Net/blob/master/Okex.Net/Icon/icon.png?raw=true) OKEx.Net 

![Build status](https://travis-ci.org/JKorf/Binance.Net.svg?branch=master)

A .Net wrapper for the OKEx API as described on [OKEx](https://www.okex.com/docs/en/), including all features the API provides using clear and readable objects.

**If you think something is broken, something is missing or have any questions, please open an [Issue](https://github.com/burakoner/OKEx.Net/issues)**

## CryptoExchange.Net
Implementation is build upon the CryptoExchange.Net library, make sure to also check out the documentation on that: [docs](https://github.com/JKorf/CryptoExchange.Net)

Other CryptoExchange.Net implementations:
<table>
<tr>
<td><a href="https://github.com/JKorf/Binance.Net"><img src="https://raw.githubusercontent.com/JKorf/Binance.Net/master/Binance.Net/Icon/icon.png?raw=true"></a>
<br />
<a href="https://github.com/JKorf/Binance.Net">Binance</a>
</td>
<td><a href="https://github.com/JKorf/Bittrex.Net"><img src="https://github.com/JKorf/Bittrex.Net/blob/master/Bittrex.Net/Icon/icon.png?raw=true"></a>
<br />
<a href="https://github.com/JKorf/Bittrex.Net">Bittrex</a>
</td>
<td><a href="https://github.com/JKorf/Bitfinex.Net"><img src="https://github.com/JKorf/Bitfinex.Net/blob/master/Bitfinex.Net/Icon/icon.png?raw=true"></a>
<br />
<a href="https://github.com/JKorf/Bitfinex.Net">Bitfinex</a>
</td>
<td><a href="https://github.com/JKorf/CoinEx.Net"><img src="https://github.com/JKorf/CoinEx.Net/blob/master/CoinEx.Net/Icon/icon.png?raw=true"></a>
<br />
<a href="https://github.com/JKorf/CoinEx.Net">CoinEx</a>
</td>
<td><a href="https://github.com/JKorf/Huobi.Net"><img src="https://github.com/JKorf/Huobi.Net/blob/master/Huobi.Net/Icon/icon.png?raw=true"></a>
<br />
<a href="https://github.com/JKorf/Huobi.Net">Huobi</a>
</td>
<td><a href="https://github.com/JKorf/Kucoin.Net"><img src="https://github.com/JKorf/Kucoin.Net/blob/master/Kucoin.Net/Icon/icon.png?raw=true"></a>
<br />
<a href="https://github.com/JKorf/Kucoin.Net">Kucoin</a>
</td>
<td><a href="https://github.com/JKorf/Kraken.Net"><img src="https://github.com/JKorf/Kraken.Net/blob/master/Kraken.Net/Icon/icon.png?raw=true"></a>
<br />
<a href="https://github.com/JKorf/Kraken.Net">Kraken</a>
</td>
</tr>
</table>
Implementations from third parties:
<table>
<tr>
<td><a href="https://github.com/Zaliro/Switcheo.Net"><img src="https://github.com/Zaliro/Switcheo.Net/blob/master/Resources/switcheo-coin.png?raw=true"></a>
<br />
<a href="https://github.com/Zaliro/Switcheo.Net">Switcheo</a>
</td>
<td><a href="https://github.com/ridicoulous/LiquidQuoine.Net"><img src="https://github.com/ridicoulous/LiquidQuoine.Net/blob/master/Resources/icon.png?raw=true"></a>
<br />
<a href="https://github.com/ridicoulous/LiquidQuoine.Net">Liquid</a>
</td>
</tr>
</table>

## Donations
Donations are greatly appreciated and a motivation to keep improving.

**BTC**:  33WbRKqt7wXARVdAJSu1G1x3QnbyPtZ2bH  
**ETH**:  0x65b02db9b67b73f5f1e983ae10796f91ded57b64  


## Installation
![Nuget version](https://img.shields.io/nuget/v/binance.net.svg)  ![Nuget downloads](https://img.shields.io/nuget/dt/Okex.Net.svg)
Available on [Nuget](https://www.nuget.org/packages/Okex.Net/).
```
pm> Install-Package Okex.Net
```
To get started with Binance.Net first you will need to get the library itself. The easiest way to do this is to install the package into your project using  [NuGet](https://www.nuget.org/packages/Binance.Net/). Using Visual Studio this can be done in two ways.

### Using the package manager
In Visual Studio right click on your solution and select 'Manage NuGet Packages for solution...'. A screen will appear which initially shows the currently installed packages. In the top bit select 'Browse'. This will let you download net package from the NuGet server. In the search box type 'Binance.Net' and hit enter. The Binance.Net package should come up in the results. After selecting the package you can then on the right hand side select in which projects in your solution the package should install. After you've selected all project you wish to install and use Binance.Net in hit 'Install' and the package will be downloaded and added to you projects.

### Using the package manager console
In Visual Studio in the top menu select 'Tools' -> 'NuGet Package Manager' -> 'Package Manager Console'. This should open up a command line interface. On top of the interface there is a dropdown menu where you can select the Default Project. This is the project that Binance.Net will be installed in. After selecting the correct project type  `Install-Package Binance.Net`  in the command line interface. This should install the latest version of the package in your project.

After doing either of above steps you should now be ready to actually start using Binance.Net.
## Getting started
After installing it's time to actually use it. To get started we have to add the Binance.Net namespace:  `using Binance.Net;`.

Binance.Net provides two clients to interact with the Binance API. The  `BinanceClient`  provides all rest API calls. The  `BinanceSocketClient`  provides functions to interact with the websocket provided by the Binance API. Both clients are disposable and as such can be used in a  `using`statement.

## Examples
Examples can be found in the Examples folder.

## Timestamping
Requests made to Binance are checked for a correct timestamp. When requests are send a timestamp is added to the message. When Binance processes the message the timestamp is checked to be > the current time and < the current time + 5000ms (default). If the timestamp is outside these limits the following errors will be returned:
`timestamps 1000ms ahead of server time` or `Timestamp for this request is outside of the recvWindow`
The recvWindow is default 5000ms and can be changed using the `ReceiveWindow` configuration option. All times are communicated in UTC so there won't be any timezone issues. However, because of clock drifting it can be that the client UTC time is not the same as the server UTC time. It is therefor recommended clients use the `SP TimeSync` program to resync the client UTC time more often than windows does by default (every 10 minutes or less is recommended).

## Websockets
The Binance.Net socket client provides several socket endpoint to which can be subscribed.

**Public socket endpoints:**
```C#
using(var client = new BinanceSocketClient())
{
	var successDepth = client.SubscribeToDepthStream("bnbbtc", (data) =>
	{
		// handle data
	});
	var successTrades = client.SubscribeToTradesStream("bnbbtc", (data) =>
	{
		// handle data
	});
	var successKline = client.SubscribeToKlineStream("bnbbtc", KlineInterval.OneMinute, (data) =>
	{
		// handle data
	});
	var successSymbol = client.SubscribeToSymbolTicker("bnbbtc", (data) =>
	{
		// handle data
	});
	var successSymbols = client.SubscribeToAllSymbolTicker((data) =>
	{
		// handle data
	});
	var successOrderBook = client.SubscribeToPartialBookDepthStream("bnbbtc", 10, (data) =>
	{
		// handle data
	});
}
```

**Private socket endpoints:**

For the private endpoint a user stream has to be started on the Binance server. This can be done using the `StartUserStream()` method in the `BinanceClient`. This command will return a listen key which can then be provided to the private socket subscription:
```C#
using(var client = new BinanceSocketClient())
{
	var successOrderBook = client.SubscribeToUserStream(listenKey, 
	(accountInfoUpdate) =>
	{
		// handle account info update
	},
	(orderInfoUpdate) =>
	{
		// handle order info update
	});
}
```

When no longer listening to private endpoints the `client.StopUserStream` method in `BinanceClient` should be used to signal the Binance server the stream can be closed.


## Release notes
* Version 1.0.0 - 26 Jan 2020
    * First Release