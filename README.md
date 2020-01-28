# ![Icon](https://github.com/burakoner/OKEx.Net/blob/master/Okex.Net/Icon/icon.png?raw=true) OKEx.Net 

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
![Nuget version](https://img.shields.io/nuget/v/OKEx.Net.svg)  ![Nuget downloads](https://img.shields.io/nuget/dt/OKEx.Net.svg)
Available on [Nuget](https://www.nuget.org/packages/OKEx.Net).
```
pm> Install-Package OKEx.Net
```
To get started with OKEx.Net first you will need to get the library itself. The easiest way to do this is to install the package into your project using  [NuGet](https://www.nuget.org/packages/OKEx.Net). Using Visual Studio this can be done in two ways.

### Using the package manager
In Visual Studio right click on your solution and select 'Manage NuGet Packages for solution...'. A screen will appear which initially shows the currently installed packages. In the top bit select 'Browse'. This will let you download net package from the NuGet server. In the search box type 'OKEx.Net' and hit enter. The OKEx.Net package should come up in the results. After selecting the package you can then on the right hand side select in which projects in your solution the package should install. After you've selected all project you wish to install and use OKEx.Net in hit 'Install' and the package will be downloaded and added to you projects.

### Using the package manager console
In Visual Studio in the top menu select 'Tools' -> 'NuGet Package Manager' -> 'Package Manager Console'. This should open up a command line interface. On top of the interface there is a dropdown menu where you can select the Default Project. This is the project that OKEx.Net will be installed in. After selecting the correct project type  `Install-Package OKEx.Net`  in the command line interface. This should install the latest version of the package in your project.

After doing either of above steps you should now be ready to actually start using OKEx.Net.
## Getting started
After installing it's time to actually use it. To get started we have to add the OKEx.Net namespace:  `using Okex.Net;`.

OKEx.Net provides two clients to interact with the OKEx API. The  `OkexClient`  provides all rest API calls. The  `OkexSocketClient`  provides functions to interact with the websocket provided by the OKEx API. Both clients are disposable and as such can be used in a  `using`statement.

## Examples
**Public Api Endpoints:**
```C#
OkexClient apiClient = new OkexClient();
var ok00 = apiClient.General_ServerTime();
var ok01 = apiClient.Spot_GetTradingPairs();
var ok02 = apiClient.Spot_GetOrderBook("BTC-USDT");
var ok03 = apiClient.Spot_GetAllTickers();
var ok04 = apiClient.Spot_GetSymbolTicker("BTC-USDT");
var ok05 = apiClient.Spot_GetTrades("BTC-USDT");
var ok06 = apiClient.Spot_GetCandles("BTC-USDT", SpotPeriod.OneHour);
```

**Private Spot Api Endpoints:**
```C#
var apiClient = new OkexClient();
apiClient.SetApiCredentials("XXXXXXXX-API-KEY-XXXXXXXX", "XXXXXXXX-API-SECRET-XXXXXXXX", "XXXXXXXX-API-PASSPHRASE-XXXXXXXX");
var ok11 = apiClient.Spot_GetAllBalances();
var ok12 = apiClient.Spot_GetSymbolBalance("BTC");
var ok13 = apiClient.Spot_GetSymbolBalance("ETH");
var ok14 = apiClient.Spot_GetSymbolBalance("eth");
var ok15 = apiClient.Spot_GetSymbolBills("ETH");
var ok16 = apiClient.Spot_PlaceOrder("ETH-BTC", SpotOrderSide.Sell, SpotOrderType.Limit, SpotTimeInForce.NormalOrder, price: 0.1m, size: 0.11m);
var ok17 = apiClient.Spot_PlaceOrder("ETH-BTC", SpotOrderSide.Sell, SpotOrderType.Limit, SpotTimeInForce.NormalOrder, price: 0.1m, size: 0.11m, clientOrderId: "ClientOrderId");
var ok18 = apiClient.Spot_CancelOrder("ETH-BTC", 4275473321519104);
var ok19 = apiClient.Spot_CancelOrder("ETH-BTC", clientOrderId: "clientorderid"); // It works: Case Insensitive
var ok20 = apiClient.Spot_CancelOrder("ETH-BTC", clientOrderId: "CLIENTORDERID"); // It works: Case Insensitive 
var ok21 = apiClient.Spot_GetAllOrders("ETH-BTC", SpotOrderState.Canceled);
var ok22 = apiClient.Spot_GetAllOrders("ETH-BTC", SpotOrderState.Complete, 2, after: 1);
var ok23 = apiClient.Spot_GetOpenOrders("ETH-BTC");
var ok24 = apiClient.Spot_GetOrderDetails("ETH-BTC", clientOrderId: "clientorderid");
var ok25 = apiClient.Spot_TradeFeeRates();
```

**Private Funding Api Endpoints:**
```C#
var apiClient = new OkexClient();
apiClient.SetApiCredentials("XXXXXXXX-API-KEY-XXXXXXXX", "XXXXXXXX-API-SECRET-XXXXXXXX", "XXXXXXXX-API-PASSPHRASE-XXXXXXXX");
var ok31 = apiClient.Funding_GetAllBalances();
var ok32 = apiClient.Funding_GetCurrencyBalance("BTC");
var ok33 = apiClient.Funding_GetAssetValuation(FundingAccountType.TotalAccountAssets, "USD");
var ok34 = apiClient.Funding_GetSubAccount("subaccountname");
var ok35 = apiClient.Funding_Transfer("ETH", 0.1m, FundingTransferAccountType.FundingAccount, FundingTransferAccountType.Spot);
var ok36 = apiClient.Funding_Transfer("ETH", 0.3m, FundingTransferAccountType.Spot, FundingTransferAccountType.FundingAccount);
var ok37 = apiClient.Funding_GetAllCurrencies();
var ok38 = apiClient.Funding_GetBills();
var ok41 = apiClient.Funding_GetDepositAddress("ETH");
var ok42 = apiClient.Funding_GetDepositHistory();
var ok43 = apiClient.Funding_GetDepositHistoryByCurrency("BTC");
var ok44 = apiClient.Funding_GetDepositHistoryByCurrency("ETH");
var ok45 = apiClient.Funding_GetWithdrawalFees();
var ok46 = apiClient.Funding_GetWithdrawalFees("ETH");
var ok47 = apiClient.Funding_Withdrawal("ETH", 1.1m, FundinWithdrawalDestination.Others, "0x65b02db9b67b73f5f1e983ae10796f91ded57b64", "--fundpassword--", 0.01m);
var ok48 = apiClient.Funding_GetWithdrawalHistory();
var ok49 = apiClient.Funding_GetWithdrawalHistoryByCurrency("BTC");
var ok50 = apiClient.Funding_GetWithdrawalHistoryByCurrency("ETH");
```

## Websockets
The OKEx.Net socket client provides several socket endpoint to which can be subscribed.

**Public Socket Endpoints:**
```C#
var pairs = new List<string>();
pairs.Add("BTC-USDT");
pairs.Add("LTC-USDT");
pairs.Add("ETH-USDT");
pairs.Add("XRP-USDT");
pairs.Add("BCH-USDT");
pairs.Add("EOS-USDT");
pairs.Add("OKB-USDT");

/* OkexSocketClient Object */
var wsClient = new OkexSocketClient(new OkexSocketClientOptions { LogVerbosity = CryptoExchange.Net.Logging.LogVerbosity.Debug);

/* Public Socket Endpoints: */
var subs = new List<UpdateSubscription>();
foreach (var pair in pairs)
{
    var candleSubscription = wsClient.Spot_SubscribeToCandlesticks(pair, Okex.Net.SpotPeriod.FiveMinutes, (data) =>
    {
        if (data != null)
        {
            Console.WriteLine($"Candle >> {data.Symbol} >> ST:{data.StartTime} O:{data.Open} H:{data.High} L:{data.Low} C:{data.Close} V:{data.Volume}");
        }
    });
    subs.Add(candleSubscription.Data);
}
foreach (var pair in pairs)
{
    var tickerSubscription = wsClient.Spot_SubscribeToTicker(pair, (data) =>
    {
        if (data != null)
        {
            Console.WriteLine($"Ticker >> {data.Symbol} >> LP:{data.LastPrice} LQ:{data.LastQuantity} Bid:{data.BestBidPrice} BS:{data.BestBidSize} Ask:{data.BestAskPrice} AS:{data.BestAskSize} 24O:{data.Open24H} 24H:{data.High24H} 24L:{data.Low24H} 24BV:{data.BaseVolume24H} 24QV:{data.QuoteVolume24H} ");
        }
    });
    subs.Add(tickerSubscription.Data);
}
foreach (var pair in pairs)
{
    var tradeSubscription = wsClient.Spot_SubscribeToTrades(pair, (data) =>
    {
        if (data != null)
        {
            Console.WriteLine($"Trades >> {data.Symbol} >> ID:{data.TradeId} P:{data.Price} A:{data.Size} S:{data.Side} T:{data.Timestamp}");
        }
    });
    // subs.Add(tradeSubscription.Data);
}
foreach (var pair in pairs)
{
    var depthSubscription = wsClient.Spot_SubscribeToOrderBook(pair, SpotOrderBookDepth.All, (data) =>
    {
        if (data != null && data.Asks != null && data.Asks.Count() > 0 && data.Bids != null && data.Bids.Count() > 0)
        {
            Console.WriteLine($"Depth >> {data.Symbol} >> Ask P:{data.Asks.First().Price} Q:{data.Asks.First().Quantity} C:{data.Asks.First().OrdersCount} Bid P:{data.Bids.First().Price} Q:{data.Bids.First().Quantity} C:{data.Bids.First().OrdersCount} ");
        }
    });
    subs.Add(depthSubscription.Data);
}
```

**Private Socket Endpoints:**
```C#
/* OkexSocketClient Object */
var wsClient = new OkexSocketClient(new OkexSocketClientOptions { LogVerbosity = CryptoExchange.Net.Logging.LogVerbosity.Debug);

var sockLogin = wsClient.User_Login("XXXXXXXX-API-KEY-XXXXXXXX", "XXXXXXXX-API-SECRET-XXXXXXXX", "XXXXXXXX-API-PASSPHRASE-XXXXXXXX");
wsClient.User_Spot_SubscribeToBalance("ETH", (data) =>
{
    if (data != null)
    {
        Console.WriteLine($"Balance Update >> {data.Currency} >> Balance:{data.Balance} Available:{data.Available} Frozen:{data.Frozen}");
    }
});
wsClient.User_Spot_SubscribeToOrders("ETH-USDT", (data) =>
{
    if (data != null)
    {
        Console.WriteLine($"Order Update >> {data.Symbol} >> Id:{data.OrderId} State:{data.State}");
    }
});
```

## Release Notes
* Version 1.0.2 - 28 Jan 2020
    * Upgraded to CryptoExchange.Net v3.0.3
    * Added Funding API Deposit Endpoints
    * Added Funding API Withdrawal Endpoints
    * Added Websockets Login Method
    * Added Websockets User Stream Subscriptions
    * Adapted some Spot Api endpoint models with websocket models
    * Added Examples Project

* Version 1.0.0 - 26 Jan 2020
    * First Release