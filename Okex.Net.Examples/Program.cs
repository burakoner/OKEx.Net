using CryptoExchange.Net.Sockets;
using Newtonsoft.Json;
using Okex.Net.Enums;
using Okex.Net.RestObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Okex.Net.Examples
{
    class Program
    {
        static void Main(string[] args)
        {
            // Okex Rest Api Client
            OkexClient api = new OkexClient(new OkexClientOptions { LogVerbosity = CryptoExchange.Net.Logging.LogVerbosity.Debug });
            api.SetApiCredentials("XXXXXXXX-API-KEY-XXXXXXXX", "XXXXXXXX-API-SECRET-XXXXXXXX", "XXXXXXXX-API-PASSPHRASE-XXXXXXXX");
            
            /* System: Public Endpoints */
            var system_public_01 = api.SystemTime();
            var system_public_02 = api.SystemStatus();

            /* Funding: Private Endpoints */
            var funding_public_01 = api.Funding_GetAllBalances();
            var funding_public_02 = api.Funding_GetSubAccount("subaccountname");
            var funding_public_03 = api.Funding_GetAssetValuation(OkexFundingAccountType.TotalAccountAssets, "USD");
            var funding_public_04 = api.Funding_GetCurrencyBalance("BTC");
            var funding_public_05 = api.Funding_Transfer("ETH", 0.1m, OkexFundingTransferAccountType.FundingAccount, OkexFundingTransferAccountType.Spot);
            var funding_public_06 = api.Funding_Withdrawal("ETH", 1.1m, OkexFundingWithdrawalDestination.Others, "0x65b02db9b67b73f5f1e983ae10796f91ded57b64", "--fundpassword--", 0.01m);
            var funding_public_07 = api.Funding_GetWithdrawalHistory();
            var funding_public_08 = api.Funding_GetDepositHistoryByCurrency("BTC");
            var funding_public_09 = api.Funding_GetBills();
            var funding_public_10 = api.Funding_GetDepositAddress("BTC");
            var funding_public_11 = api.Funding_GetDepositHistory();
            var funding_public_12 = api.Funding_GetWithdrawalHistoryByCurrency("BTC");
            var funding_public_13 = api.Funding_GetAllCurrencies();
            var funding_public_14 = api.Funding_GetUserID();
            var funding_public_15 = api.Funding_GetWithdrawalFees();
            var funding_public_16 = api.Funding_GetWithdrawalFees("ETH");
            var funding_public_17 = api.Funding_PiggyBank("ETH", 0.1m, OkexFundingPiggyBankActionSide.Purchase);
            var funding_public_18 = api.Funding_PiggyBank("ETH", 0.1m, OkexFundingPiggyBankActionSide.Redempt);

            /* Spot: Public Endpoints */
            var spot_public_01 = api.Spot_GetTradingPairs();
            var spot_public_02 = api.Spot_GetOrderBook("BTC-USDT");
            var spot_public_03 = api.Spot_GetAllTickers();
            var spot_public_04 = api.Spot_GetSymbolTicker("BTC-USDT");
            var spot_public_05 = api.Spot_GetTrades("BTC-USDT");
            var spot_public_06 = api.Spot_GetCandles("BTC-USDT", OkexSpotPeriod.OneHour);
            var spot_public_07 = api.Spot_GetHistoricalCandles("BTC-USDT", OkexSpotPeriod.OneHour);

            /* Spot: Private Endpoints */
            var spot_place_order_01 = new OkexSpotPlaceOrder
            {
                Symbol = "ETH-BTC",
                ClientOrderId = "ClientOrderId",
                Type = OkexSpotOrderType.Limit,
                Side = OkexSpotOrderSide.Sell,
                TimeInForce = OkexSpotTimeInForce.NormalOrder,
                Price = 0.1m,
                Size = 0.1m,
            };
            var spot_place_order_02 = new OkexSpotPlaceOrder
            {
                Symbol = "ETH-BTC",
                ClientOrderId = "ClientOrderIx",
                Type = OkexSpotOrderType.Limit,
                Side = OkexSpotOrderSide.Sell,
                TimeInForce = OkexSpotTimeInForce.NormalOrder,
                Price = 0.2m,
                Size = 0.2m,
            };
            var spot_place_orders = new List<OkexSpotPlaceOrder>();
            spot_place_orders.Add(spot_place_order_01);
            spot_place_orders.Add(spot_place_order_02);

            var spot_cancel_order_01 = new OkexSpotCancelOrder
            {
                Symbol = "ETH-BTC",
                OrderIds = new List<long> { 1001, 1002, 1003, 1004, 1005 },
                ClientOrderIds = new List<string>()
            };
            var spot_cancel_order_02 = new OkexSpotCancelOrder
            {
                Symbol = "ETH-BTC",
                OrderIds = new List<long> { },
                ClientOrderIds = new List<string> { "coid001", "coid002", "coid003", "coid004", "coid005" }
            };
            var spot_cancel_orders = new List<OkexSpotCancelOrder>();
            spot_cancel_orders.Add(spot_cancel_order_01);
            spot_cancel_orders.Add(spot_cancel_order_02);

            var spot_private_01 = api.Spot_GetAllBalances();
            var spot_private_02 = api.Spot_GetSymbolBalance("BTC");
            var spot_private_03 = api.Spot_GetSymbolBalance("ETH");
            var spot_private_04 = api.Spot_GetSymbolBalance("eth");
            var spot_private_05 = api.Spot_GetSymbolBills("ETH");
            var spot_private_06 = api.Spot_PlaceOrder(spot_place_order_01);
            var spot_private_07 = api.Spot_PlaceOrder(spot_place_order_02);
            var spot_private_08 = api.Spot_PlaceOrder("ETH-BTC", OkexSpotOrderSide.Sell, OkexSpotOrderType.Limit, OkexSpotTimeInForce.NormalOrder, price: 0.1m, size: 0.11m);
            var spot_private_09 = api.Spot_PlaceOrder("ETH-BTC", OkexSpotOrderSide.Sell, OkexSpotOrderType.Limit, OkexSpotTimeInForce.NormalOrder, price: 0.1m, size: 0.11m, clientOrderId: "ClientOrderId");
            var spot_private_10 = api.Spot_BatchPlaceOrders(spot_place_orders);
            var spot_private_11 = api.Spot_CancelOrder("ETH-BTC", 4275473321519104);
            var spot_private_12 = api.Spot_CancelOrder("ETH-BTC", clientOrderId: "clientorderid"); // It works: Case Insensitive
            var spot_private_13 = api.Spot_CancelOrder("ETH-BTC", clientOrderId: "CLIENTORDERID"); // It works: Case Insensitive 
            var spot_private_14 = api.Spot_BatchCancelOrders(spot_cancel_orders);
            var spot_private_15 = api.Spot_ModifyOrder("ETH-BTC", orderId: 1001, newSize: 0.1m);
            var spot_private_16 = api.Spot_BatchModifyOrders(new List<OkexSpotModifyOrder> { });
            var spot_private_17 = api.Spot_GetAllOrders("ETH-BTC", OkexSpotOrderState.Canceled);
            var spot_private_18 = api.Spot_GetAllOrders("ETH-BTC", OkexSpotOrderState.Complete, 2, after: 1);
            var spot_private_19 = api.Spot_GetOpenOrders("ETH-BTC");
            var spot_private_20 = api.Spot_GetOrderDetails("ETH-BTC", clientOrderId: "clientorderid");
            var spot_private_21 = api.Spot_TradeFeeRates();
            var spot_private_22 = api.Spot_GetTransactionDetails("ETH-BTC");
            var spot_private_23 = api.Spot_AlgoPlaceOrder("ETH-BTC", OkexAlgoOrderType.TriggerOrder, OkexMarket.Spot, OkexSpotOrderSide.Buy, size: 0.1m, trigger_price: 0.0101m, trigger_algo_price: 0.0100m, trigger_algo_type: OkexAlgoPriceType.Limit);
            var spot_private_24 = api.Spot_AlgoCancelOrder("ETH-BTC", OkexAlgoOrderType.TriggerOrder, new List<long> { 1001 });
            var spot_private_25 = api.Spot_AlgoGetOrders("ETH-BTC", OkexAlgoOrderType.TriggerOrder);

            /* Margin: Public Endpoints */
            var margin_public_01 = api.Margin_GetMarkPrice("BTC-USDT");

            /* Margin: Private Endpoints */
            var margin_private_01 = api.Margin_GetAllBalances();
            var margin_private_02 = api.Margin_GetSymbolBalance("BTC-USDT");
            var margin_private_03 = api.Margin_GetSymbolBills("BTC-USDT");
            var margin_private_04 = api.Margin_GetAccountSettings();
            var margin_private_05 = api.Margin_GetAccountSettings("BTC-USDT");
            var margin_private_06 = api.Margin_GetLoanHistory(OkexMarginLoanStatus.Outstanding);
            var margin_private_07 = api.Margin_GetLoanHistory("BTC-USDT", OkexMarginLoanState.Complete);
            var margin_private_08 = api.Margin_Loan("BTC-USDT", "BTC", 0.1m);
            var margin_private_09 = api.Margin_Repayment("BTC-USDT", "BTC", 0.1m);
            var margin_private_10 = api.Margin_PlaceOrder("BTC-USDT", OkexSpotOrderSide.Buy, OkexSpotOrderType.Limit);
            var margin_private_11 = api.Margin_BatchPlaceOrders(new List<OkexSpotPlaceOrder> { });
            var margin_private_12 = api.Margin_CancelOrder("BTC-USDT", 1001);
            var margin_private_13 = api.Margin_BatchCancelOrders(new List<OkexSpotCancelOrder> { });
            var margin_private_14 = api.Margin_GetAllOrders("BTC-USDT", OkexSpotOrderState.Complete);
            var margin_private_15 = api.Margin_GetLeverage("BTC-USDT");
            var margin_private_16 = api.Margin_SetLeverage("BTC-USDT", 10);
            var margin_private_17 = api.Margin_GetOrderDetails("BTC-USDT", 1001);
            var margin_private_18 = api.Margin_GetOpenOrders("BTC-USDT");
            var margin_private_19 = api.Margin_GetTransactionDetails("BTC-USDT", 1001);
            var margin_private_20 = api.Margin_AlgoPlaceOrder("BTC-USDT", OkexAlgoOrderType.TriggerOrder, OkexMarket.Margin, OkexSpotOrderSide.Buy, size: 0.1m, trigger_price: 0.0101m, trigger_algo_price: 0.0100m, trigger_algo_type: OkexAlgoPriceType.Limit);
            var margin_private_21 = api.Margin_AlgoCancelOrder("BTC-USDT", OkexAlgoOrderType.TriggerOrder, new List<long> { 1001 });
            var margin_private_22 = api.Margin_AlgoGetOrders("BTC-USDT", OkexAlgoOrderType.TriggerOrder);

            /* Futures: Public Endpoints */
            var futures_public_01 = api.Futures_GetTradingContracts();
            var futures_public_02 = api.Futures_GetOrderBook("BTC-USDT-201225", 20);
            var futures_public_03 = api.Futures_GetAllTickers();
            var futures_public_04 = api.Futures_GetSymbolTicker("BTC-USDT-201225");
            var futures_public_05 = api.Futures_GetTrades("BTC-USDT-201225");
            var futures_public_06 = api.Futures_GetCandles("BTC-USDT-201225", OkexSpotPeriod.OneHour);
            var futures_public_07 = api.Futures_GetIndices("BTC-USDT-201225");
            var futures_public_08 = api.Futures_GetFiatExchangeRates();
            var futures_public_09 = api.Futures_GetEstimatedPrice("BTC-USDT-201225");
            var futures_public_10 = api.Futures_GetOpenInterests("BTC-USDT-201225");
            var futures_public_11 = api.Futures_GetPriceLimit("BTC-USDT-201225");
            var futures_public_12 = api.Futures_GetMarkPrice("BTC-USDT-201225");
            var futures_public_13 = api.Futures_GetLiquidatedOrders("BTC-USDT-201225", OkexFuturesLiquidationStatus.FilledOrdersInTheRecent7Days);
            var futures_public_14 = api.Futures_GetSettlementHistory("BTC-USDT-201225");
            var futures_public_15 = api.Futures_GetHistoricalMarketData("BTC-USDT-201225", OkexSpotPeriod.OneHour);

            /* Futures: Private Endpoints */
            var futures_private_01 = api.Futures_GetPositions();
            var futures_private_02 = api.Futures_GetPositions("BTC-USDT-201225");
            var futures_private_03 = api.Futures_GetBalances();
            var futures_private_04 = api.Futures_GetBalances("BTC-USDT");
            var futures_private_05 = api.Futures_GetLeverage("BTC-USDT");
            var futures_private_06 = api.Futures_SetLeverage(OkexFuturesMarginMode.Crossed, "BTC-USDT", 10);
            var futures_private_07 = api.Futures_GetSymbolBills("ETH-USDT");
            var futures_private_08 = api.Futures_PlaceOrder("ETH-USDT", OkexFuturesOrderType.OpenLong, 0.1m, OkexFuturesTimeInForce.NormalOrder);
            var futures_private_09 = api.Futures_BatchPlaceOrders("ETH-USDT", new List<OkexFuturesPlaceOrder> { });
            var futures_private_10 = api.Futures_ModifyOrder("ETH-USDT", orderId: 1001, newSize: 0.1m);
            var futures_private_11 = api.Futures_BatchModifyOrders("ETH-USDT", new List<OkexFuturesModifyOrder> { });
            var futures_private_12 = api.Futures_CancelOrder("ETH-USDT", 1001);
            var futures_private_13 = api.Futures_BatchCancelOrders("ETH-USDT", new List<long> { }, new List<string> { });
            var futures_private_14 = api.Futures_GetAllOrders("ETH-USDT", OkexFuturesOrderState.Complete, 2, after: 1);
            var futures_private_15 = api.Futures_GetOrderDetails("ETH-USDT", clientOrderId: "clientorderid");
            var futures_private_16 = api.Futures_GetTransactionDetails("ETH-USDT", orderId: 1001);
            var futures_private_17 = api.Futures_SetAccountMode("ETH-USDT", OkexFuturesMarginMode.Crossed);
            var futures_private_18 = api.Futures_GetTradeFeeRates("ETH-USDT");
            var futures_private_19 = api.Futures_MarketCloseAll("ETH-USDT", OkexFuturesDirection.Long);
            var futures_private_20 = api.Futures_CancelAll("ETH-USDT", OkexFuturesDirection.Long);
            var futures_private_21 = api.Futures_GetHoldAmount("ETH-USDT");
            var futures_private_22 = api.Futures_AlgoPlaceOrder("BTC-USDT", OkexFuturesOrderType.OpenLong, OkexAlgoOrderType.TriggerOrder, size: 0.1m, trigger_price: 0.0101m, trigger_algo_price: 0.0100m, trigger_algo_type: OkexAlgoPriceType.Limit);
            var futures_private_23 = api.Margin_AlgoCancelOrder("BTC-USDT", OkexAlgoOrderType.TriggerOrder, new List<long> { 1001 });
            var futures_private_24 = api.Margin_AlgoGetOrders("BTC-USDT", OkexAlgoOrderType.TriggerOrder);

            /* Perpetual Swap: Public Endpoints */
            var swap_public_01 = api.Swap_GetTradingContracts();
            var swap_public_02 = api.Swap_GetOrderBook("BTC-USDT-SWAP");
            var swap_public_03 = api.Swap_GetAllTickers();
            var swap_public_04 = api.Swap_GetSymbolTicker("BTC-USDT-SWAP");
            var swap_public_05 = api.Swap_GetTrades("BTC-USDT-SWAP");
            var swap_public_06 = api.Swap_GetCandles("BTC-USDT-SWAP", OkexSpotPeriod.OneHour);
            var swap_public_07 = api.Swap_GetIndices("BTC-USDT-SWAP");
            var swap_public_08 = api.Swap_GetFiatExchangeRates();
            var swap_public_09 = api.Swap_GetOpenInterests("BTC-USDT-SWAP");
            var swap_public_10 = api.Swap_GetPriceLimit("BTC-USDT-SWAP");
            var swap_public_11 = api.Swap_GetLiquidatedOrders("BTC-USDT-SWAP", OkexSwapLiquidationStatus.FilledOrdersInTheRecent7Days);
            var swap_public_12 = api.Swap_GetNextSettlementTime("BTC-USDT-SWAP");
            var swap_public_13 = api.Swap_GetMarkPrice("BTC-USDT-SWAP");
            var swap_public_14 = api.Swap_GetFundingRateHistory("BTC-USDT-SWAP");
            var swap_public_15 = api.Swap_GetHistoricalMarketData("BTC-USDT-SWAP", OkexSpotPeriod.OneHour);

            /* Perpetual Swap: Private Endpoints */
            var swap_private_01 = api.Swap_GetPositions();
            var swap_private_02 = api.Swap_GetPositions("BTC-USDT-SWAP");
            var swap_private_03 = api.Swap_GetBalances();
            var swap_private_04 = api.Swap_GetBalances("BTC-USDT-SWAP");
            var swap_private_05 = api.Swap_GetLeverage("BTC-USDT-SWAP");
            var swap_private_06 = api.Swap_SetLeverage("BTC-USDT-SWAP", OkexSwapLeverageSide.CrossedMargin, 17);
            var swap_private_07 = api.Swap_GetBills("BTC-USDT-SWAP");
            var swap_private_08 = api.Swap_PlaceOrder("BTC-USDT-SWAP", OkexSwapOrderType.OpenLong, 0.1m);
            var swap_private_09 = api.Swap_BatchPlaceOrders("BTC-USDT-SWAP", new List<OkexSwapPlaceOrder> { });
            var swap_private_10 = api.Swap_CancelOrder("BTC-USDT-SWAP", orderId:1001);
            var swap_private_11 = api.Swap_BatchCancelOrders("BTC-USDT-SWAP", new List<long> { }, new List<string> { });
            var swap_private_12 = api.Swap_ModifyOrder("BTC-USDT-SWAP", orderId: 1001, newSize: 0.1m);
            var swap_private_13 = api.Swap_BatchModifyOrders("BTC-USDT-SWAP", new List<OkexSwapModifyOrder> { });
            var swap_private_14 = api.Swap_GetAllOrders("BTC-USDT-SWAP", OkexSwapOrderState.Complete);
            var swap_private_15 = api.Swap_GetOrderDetails("BTC-USDT-SWAP", clientOrderId: "clientorderid");
            var swap_private_16 = api.Swap_GetTransactionDetails("BTC-USDT-SWAP", orderId: 1001);
            var swap_private_17 = api.Swap_GetHoldAmount("BTC-USDT-SWAP");
            var swap_private_18 = api.Swap_GetTradeFeeRates("BTC-USDT-SWAP");
            var swap_private_19 = api.Swap_MarketCloseAll("BTC-USDT-SWAP", OkexSwapDirection.Long);
            var swap_private_20 = api.Swap_CancelAll("BTC-USDT-SWAP", OkexSwapDirection.Long);
            var swap_private_21 = api.Swap_AlgoPlaceOrder("BTC-USDT", OkexSwapOrderType.OpenLong, OkexAlgoOrderType.TriggerOrder, size: 0.1m, trigger_price: 0.0101m, trigger_algo_price: 0.0100m, trigger_algo_type: OkexAlgoPriceType.Limit);
            var swap_private_22 = api.Swap_AlgoCancelOrder("BTC-USDT", OkexAlgoOrderType.TriggerOrder, new List<long> { 1001 });
            var swap_private_23 = api.Swap_AlgoGetOrders("BTC-USDT", OkexAlgoOrderType.TriggerOrder);

            Console.ReadLine();
            // return;

            var pairs = new List<string>();
            pairs.Add("BTC-USDT");
            pairs.Add("LTC-USDT");
            pairs.Add("ETH-USDT");
            pairs.Add("XRP-USDT");
            pairs.Add("BCH-USDT");
            pairs.Add("EOS-USDT");
            pairs.Add("OKB-USDT");
            pairs.Add("ETC-USDT");
            pairs.Add("TRX-USDT");
            pairs.Add("BSV-USDT");
            pairs.Add("DASH-USDT");
            pairs.Add("NEO-USDT");
            pairs.Add("QTUM-USDT");
            pairs.Add("XLM-USDT");
            pairs.Add("ADA-USDT");
            pairs.Add("AE-USDT");
            pairs.Add("BLOC-USDT");
            pairs.Add("EGT-USDT");
            pairs.Add("IOTA-USDT");
            pairs.Add("SC-USDT");
            pairs.Add("WXT-USDT");
            pairs.Add("ZEC-USDT");

            /* Okex Socket Client Object */
            var ws = new OkexSocketClient(new OkexSocketClientOptions { LogVerbosity = CryptoExchange.Net.Logging.LogVerbosity.Debug });

            /* Pinging */
            // There is no open websocket connection now. So a connection will be established between server and client now, then client sends ping command. This causes latency around 500ms.
            // If you send ping command though an open socket connection, pong time is much more low.
            var ping01 = ws.Ping();
            var ping02 = ws.Ping();

            /* Public Socket Endpoints: */
            var subs = new List<UpdateSubscription>();
            foreach (var pair in pairs)
            {
                var candleSubscription = ws.Spot_SubscribeToCandlesticks(pair, OkexSpotPeriod.FiveMinutes, (data) =>
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
                var tickerSubscription = ws.Spot_SubscribeToTicker(pair, (data) =>
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
                var tradeSubscription = ws.Spot_SubscribeToTrades(pair, (data) =>
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
                var depthSubscription = ws.Spot_SubscribeToOrderBook(pair, OkexSpotOrderBookDepth.Depth400, (data) =>
                {
                    if (data != null && data.Asks != null && data.Asks.Count() > 0 && data.Bids != null && data.Bids.Count() > 0)
                    {
                        Console.WriteLine($"Depth >> {data.Symbol} >> Ask P:{data.Asks.First().Price} Q:{data.Asks.First().Quantity} C:{data.Asks.First().OrdersCount} Bid P:{data.Bids.First().Price} Q:{data.Bids.First().Quantity} C:{data.Bids.First().OrdersCount} ");
                    }
                });
                subs.Add(depthSubscription.Data);
            }


            /* Private Socket Endpoints: */
            var sockLogin = ws.User_Login("XXXXXXXX-API-KEY-XXXXXXXX", "XXXXXXXX-API-SECRET-XXXXXXXX", "XXXXXXXX-API-PASSPHRASE-XXXXXXXX");
            ws.User_Spot_SubscribeToBalance("ETH", (data) =>
            {
                if (data != null)
                {
                    Console.WriteLine($"Balance Update >> {data.Currency} >> Balance:{data.Balance} Available:{data.Available} Frozen:{data.Frozen}");
                }
            });
            ws.User_Spot_SubscribeToOrders("ETH-USDT", (data) =>
            {
                if (data != null)
                {
                    Console.WriteLine($"Order Update >> {data.Symbol} >> Id:{data.OrderId} State:{data.State}");
                }
            });


            /* UnSubscribe */
            // Only Trade Subscriptions remained
            foreach (var sub in subs)
            {
                _ = ws.Unsubscribe(sub);
            }

            Console.ReadLine();
            return;
        }
    }
}
