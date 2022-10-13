using CryptoExchange.Net.Sockets;
using Microsoft.Extensions.Logging;
using Okex.Net.Enums;
using Okex.Net.Objects.Core;
using System;
using System.Collections.Generic;

namespace Okex.Net.Examples
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            #region Rest Api Client
            // OKEx Rest Api Client
            OkexClient api = new OkexClient(new OkexClientOptions { LogLevel = LogLevel.Debug, OutputOriginalData=true });
            api.SetApiCredentials("XXXXXXXX-API-KEY-XXXXXXXX", "XXXXXXXX-API-SECRET-XXXXXXXX", "XXXXXXXX-API-PASSPHRASE-XXXXXXXX");
            var market_05 = api.GetCandlesticks("BTC-USDT", OkexPeriod.OneHour);
            var a = 0;

#if FALSE
            /* Public Endpoints (Unsigned) */
            var public_01 = api.GetInstruments(OkexInstrumentType.Spot);
            var public_02 = api.GetInstruments(OkexInstrumentType.Margin);
            var public_03 = api.GetInstruments(OkexInstrumentType.Swap);
            var public_04 = api.GetInstruments(OkexInstrumentType.Futures);
            var public_05 = api.GetInstruments(OkexInstrumentType.Option, "USD");
            var public_06 = api.GetDeliveryExerciseHistory(OkexInstrumentType.Futures, "BTC-USD");
            var public_07 = api.GetDeliveryExerciseHistory(OkexInstrumentType.Option, "BTC-USD");
            var public_08 = api.GetOpenInterests(OkexInstrumentType.Futures);
            var public_09 = api.GetOpenInterests(OkexInstrumentType.Option, "BTC-USD");
            var public_10 = api.GetOpenInterests(OkexInstrumentType.Swap, "BTC-USD");
            var public_11 = api.GetFundingRates("BTC-USD-SWAP");
            var public_12 = api.GetFundingRateHistory("BTC-USD-SWAP");
            var public_13 = api.GetLimitPrice("BTC-USD-SWAP");
            var public_14 = api.GetOptionMarketData("BTC-USD");
            var public_15 = api.GetEstimatedPrice("BTC-USD-211004-41000-C");
            var public_16 = api.GetDiscountInfo();
            var public_17 = api.GetSystemTime();
            var public_18 = api.GetLiquidationOrders(OkexInstrumentType.Futures, underlying: "BTC-USD", alias: OkexInstrumentAlias.Quarter, state: OkexLiquidationState.Unfilled);
            var public_19 = api.GetMarkPrices(OkexInstrumentType.Futures);
            var public_20 = api.GetPositionTiers(OkexInstrumentType.Futures, OkexMarginMode.Isolated, "BTC-USD");
            var public_21 = api.GetInterestRates();
            var public_22 = api.GetVIPInterestRates();
            var public_23 = api.GetUnderlying(OkexInstrumentType.Futures);
            var public_24 = api.GetUnderlying(OkexInstrumentType.Option);
            var public_25 = api.GetUnderlying(OkexInstrumentType.Swap);
            var public_26 = api.GetInsuranceFund(OkexInstrumentType.Margin, currency:"BTC");
            var public_27 = api.UnitConvert( OkexConvertType.CurrencyToContract, instrumentId:"BTC-USD-SWAP", price:35000, size:0.888m);
#endif

#if FALSE
            /* Market Endpoints (Unsigned) */
            var market_01 = api.GetTickers(OkexInstrumentType.Spot);
            var market_02 = api.GetTicker("BTC-USDT");
            var market_03 = api.GetIndexTickers(instrumentId: "BTC-USDT");
            var market_04 = api.GetOrderBook("BTC-USDT", 40);
            var market_05 = api.GetCandlesticks("BTC-USDT", OkexPeriod.OneHour);
            var market_06 = api.GetCandlesticksHistory("BTC-USDT", OkexPeriod.OneHour);
            var market_07 = api.GetIndexCandlesticks("BTC-USDT", OkexPeriod.OneHour);
            var market_08 = api.GetMarkPriceCandlesticks("BTC-USDT", OkexPeriod.OneHour);
            var market_09 = api.GetTrades("BTC-USDT");
            var market_10 = api.GetTradesHistory("BTC-USDT");
            var market_11 = api.Get24HourVolume();
            var market_12 = api.GetOracle();
            var market_13 = api.GetIndexComponents("BTC-USDT");
            var market_14 = api.GetBlockTickers(OkexInstrumentType.Spot);
            var market_15 = api.GetBlockTickers(OkexInstrumentType.Futures);
            var market_16 = api.GetBlockTickers(OkexInstrumentType.Option);
            var market_17 = api.GetBlockTickers(OkexInstrumentType.Swap);
            var market_18 = api.GetBlockTicker("BTC-USDT");
            var market_19 = api.GetBlockTrades("BTC-USDT");
#endif

#if FALSE
            /* Trading Endpoints (Unsigned) */
            var rubik_01 = api.GetRubikSupportCoin();
            var rubik_02 = api.GetRubikTakerVolume("BTC", OkexInstrumentType.Spot);
            var rubik_03 = api.GetRubikMarginLendingRatio("BTC", OkexPeriod.OneDay);
            var rubik_04 = api.GetRubikLongShortRatio("BTC", OkexPeriod.OneDay);
            var rubik_05 = api.GetRubikContractSummary("BTC", OkexPeriod.OneDay);
            var rubik_06 = api.GetRubikOptionsSummary("BTC", OkexPeriod.OneDay);
            var rubik_07 = api.GetRubikPutCallRatio("BTC", OkexPeriod.OneDay);
            var rubik_08 = api.GetRubikInterestVolumeExpiry("BTC", OkexPeriod.OneDay);
            var rubik_09 = api.GetRubikInterestVolumeStrike("BTC", "20210623", OkexPeriod.OneDay);
            var rubik_10 = api.GetRubikTakerFlow("BTC", OkexPeriod.OneDay);
#endif

#if FALSE
            /* Account Endpoints (Signed) */
            var account_01 = api.GetAccountBalance();
            var account_02 = api.GetAccountPositions();
            var account_03 = api.GetAccountPositionRisk();
            var account_04 = api.GetBillHistory();
            var account_05 = api.GetBillArchive();
            var account_06 = api.GetAccountConfiguration();
            var account_07 = api.SetAccountPositionMode(OkexPositionMode.LongShortMode);
            var account_08 = api.GetAccountLeverage("BTC-USD-211008", OkexMarginMode.Isolated);
            var account_09 = api.SetAccountLeverage(30, null, "BTC-USD-211008", OkexMarginMode.Isolated, OkexPositionSide.Long);
            var account_10 = api.GetMaximumAmount("BTC-USDT", OkexTradeMode.Isolated);
            var account_11 = api.GetMaximumAvailableAmount("BTC-USDT", OkexTradeMode.Isolated);
            var account_12 = api.SetMarginAmount("BTC-USDT", OkexPositionSide.Long, OkexMarginAddReduce.Add, 100.0m);
            var account_13 = api.GetMaximumLoanAmount("BTC-USDT", OkexMarginMode.Cross);
            var account_14 = api.GetFeeRates(OkexInstrumentType.Spot, category: OkexFeeRateCategory.ClassA);
            var account_15 = api.GetFeeRates(OkexInstrumentType.Futures, category: OkexFeeRateCategory.ClassA);
            var account_16 = api.GetInterestAccrued();
            var account_17 = api.GetInterestRate();
            var account_18 = api.SetGreeks(OkexGreeksType.GreeksInCoins);
            var account_19 = api.GetMaximumWithdrawals();
#endif

#if FALSE
            /* SubAccount Endpoints (Signed) */
            var subaccount_01 = api.GetSubAccounts();
            var subaccount_02 = api.ResetSubAccountApiKey("subAccountName", "apiKey", "apiLabel", true, true, "");
            var subaccount_03 = api.GetSubAccountTradingBalances("subAccountName");
            var subaccount_04 = api.GetSubAccountFundingBalances("subAccountName");
            var subaccount_05 = api.GetSubAccountBills();
            var subaccount_06 = api.TransferBetweenSubAccounts("BTC", 0.5m, OkexAccount.Funding, OkexAccount.Unified, "fromSubAccountName", "toSubAccountName");
#endif

#if FALSE
            /* Funding Endpoints (Signed) */
            var funding_01 = api.GetCurrencies();
            var funding_02 = api.GetFundingBalance();
            var funding_03 = api.FundTransfer("BTC", 0.5m, OkexTransferType.TransferWithinAccount, OkexAccount.Margin, OkexAccount.Spot);
            var funding_04 = api.GetFundingBillDetails("BTC");
            var funding_05 = api.GetLightningDeposits("BTC", 0.001m);
            var funding_06 = api.GetDepositAddress("BTC");
            var funding_07 = api.GetDepositAddress("USDT");
            var funding_08 = api.GetDepositHistory("USDT");
            var funding_09 = api.Withdraw("USDT", 100.0m, OkexWithdrawalDestination.DigitalCurrencyAddress, "toAddress", "password", 1.0m, "USDT-TRC20");
            var funding_10 = api.GetLightningWithdrawals("BTC", "invoice", "password");
            var funding_11 = api.GetWithdrawalHistory("USDT");
            var funding_12 = api.GetSavingBalances();
            var funding_13 = api.SavingPurchaseRedemption("USDT", 10.0m, OkexSavingActionSide.Purchase);
#endif

#if FALSE
            /* Trade Endpoints (Signed) */
            var trade_01 = api.PlaceOrder("BTC-USDT", OkexTradeMode.Cash, OkexOrderSide.Buy, OkexPositionSide.Long, OkexOrderType.MarketOrder, 0.1m);
            var trade_02 = api.PlaceMultipleOrders(new List<OkexOrderPlaceRequest>());
            var trade_03 = api.CancelOrder("BTC-USDT");
            var trade_04 = api.CancelMultipleOrders(new List<OkexOrderCancelRequest>());
            var trade_05 = api.AmendOrder("BTC-USDT");
            var trade_06 = api.AmendMultipleOrders(new List<OkexOrderAmendRequest>());
            var trade_07 = api.ClosePosition("BTC-USDT", OkexMarginMode.Isolated);
            var trade_08 = api.GetOrderDetails("BTC-USDT");
            var trade_09 = api.GetOrderList();
            var trade_10 = api.GetOrderHistory(OkexInstrumentType.Swap);
            var trade_11 = api.GetOrderArchive(OkexInstrumentType.Futures);
            var trade_12 = api.GetTransactionHistory();
            var trade_13 = api.GetTransactionArchive(OkexInstrumentType.Futures);
            var trade_14 = api.PlaceAlgoOrder("BTC-USDT", OkexTradeMode.Isolated, OkexOrderSide.Sell, OkexAlgoOrderType.Conditional, 0.1m);
            var trade_15 = api.CancelAlgoOrder(new List<OkexAlgoOrderRequest>());
            var trade_16 = api.CancelAdvanceAlgoOrder(new List<OkexAlgoOrderRequest>());
            var trade_17 = api.GetAlgoOrderList(OkexAlgoOrderType.OCO);
            var trade_18 = api.GetAlgoOrderHistory(OkexAlgoOrderType.Conditional);
#endif
            #endregion

            #region Socket Api Client
            /* OKEx Socket Client */
            var ws = new OkexSocketClient();
            ws.SetApiCredentials("XXXXXXXX-API-KEY-XXXXXXXX", "XXXXXXXX-API-SECRET-XXXXXXXX", "XXXXXXXX-API-PASSPHRASE-XXXXXXXX");

            /* Sample Pairs */
            var sample_pairs = new List<string> { "BTC-USDT", "LTC-USDT", "ETH-USDT", "XRP-USDT", "BCH-USDT", "EOS-USDT", "OKB-USDT", "ETC-USDT", "TRX-USDT", "BSV-USDT", "DASH-USDT", "NEO-USDT", "QTUM-USDT", "XLM-USDT", "ADA-USDT", "AE-USDT", "BLOC-USDT", "EGT-USDT", "IOTA-USDT", "SC-USDT", "WXT-USDT", "ZEC-USDT", };

            /* WS Subscriptions */
            var subs = new List<UpdateSubscription>();

#if FALSE
            /* Instruments (Public) */
            ws.SubscribeToInstruments(OkexInstrumentType.Spot, (data) =>
            {
                if (data != null)
                {
                    // ... Your logic here
                    Console.WriteLine($"Instrument {data.Instrument} BaseCurrency:{data.BaseCurrency} Category:{data.Category}");
                }
            });
#endif

#if FALSE
            /* Tickers (Public) */
            foreach (var pair in sample_pairs)
            {
                var subscription = ws.SubscribeToTickers(pair, (data) =>
                {
                    if (data != null)
                    {
                        // ... Your logic here
                        Console.WriteLine($"Ticker {data.Instrument} Ask:{data.AskPrice} Bid:{data.BidPrice}");
                    }
                });
                subs.Add(subscription.Data);
            }
#endif

#if FALSE
            /* Unsubscribe */
            foreach (var sub in subs)
            {
                _ = ws.UnsubscribeAsync(sub);
            }
#endif

#if FALSE
            /* Interests (Public) */
            foreach (var pair in sample_pairs)
            {
                ws.SubscribeToInterests(pair, (data) =>
                {
                    if (data != null)
                    {
                        // ... Your logic here
                    }
                });
            }
#endif

#if FALSE
            /* Candlesticks (Public) */
            foreach (var pair in sample_pairs)
            {
                ws.SubscribeToCandlesticks(pair, OkexPeriod.FiveMinutes, (data) =>
                {
                    if (data != null)
                    {
                        // ... Your logic here
                    }
                });
            }
#endif

#if FALSE
            /* Trades (Public) */
            foreach (var pair in sample_pairs)
            {
                ws.SubscribeToTrades(pair, (data) =>
                {
                    if (data != null)
                    {
                        // ... Your logic here
                    }
                });
            }
#endif

#if FALSE
            /* Estimated Price (Public) */
            foreach (var pair in sample_pairs)
            {
                ws.SubscribeToTrades(pair, (data) =>
                {
                    if (data != null)
                    {
                        // ... Your logic here
                    }
                });
            }
#endif

#if FALSE
            /* Mark Price (Public) */
            foreach (var pair in sample_pairs)
            {
                ws.SubscribeToMarkPrice(pair, (data) =>
                {
                    if (data != null)
                    {
                        // ... Your logic here
                    }
                });
            }
#endif

#if FALSE
            /* Mark Price Candlesticks (Public) */
            foreach (var pair in sample_pairs)
            {
                ws.SubscribeToMarkPriceCandlesticks(pair, OkexPeriod.FiveMinutes, (data) =>
                {
                    if (data != null)
                    {
                        // ... Your logic here
                    }
                });
            }
#endif

#if FALSE
            /* Limit Price (Public) */
            foreach (var pair in sample_pairs)
            {
                ws.SubscribeToPriceLimit(pair, (data) =>
                {
                    if (data != null)
                    {
                        // ... Your logic here
                    }
                });
            }
#endif

#if FALSE
            /* Order Book (Public) */
            foreach (var pair in sample_pairs)
            {
                ws.SubscribeToOrderBook(pair, OkexOrderBookType.OrderBook, (data) =>
                {
                    if (data != null && data.Asks != null && data.Asks.Count() > 0 && data.Bids != null && data.Bids.Count() > 0)
                    {
                        // ... Your logic here
                    }
                });
            }
#endif

#if FALSE
            /* Option Summary (Public) */
            ws.SubscribeToOptionSummary("USD", (data) =>
            {
                if (data != null)
                {
                    // ... Your logic here
                }
            });
#endif

#if FALSE
            /* Funding Rates (Public) */
            foreach (var pair in sample_pairs)
            {
                ws.SubscribeToFundingRates(pair, (data) =>
                {
                    if (data != null)
                    {
                        // ... Your logic here
                    }
                });
            }
#endif

#if FALSE
            /* Index Candlesticks (Public) */
            foreach (var pair in sample_pairs)
            {
                ws.SubscribeToIndexCandlesticks(pair, OkexPeriod.FiveMinutes, (data) =>
                {
                    if (data != null)
                    {
                        // ... Your logic here
                    }
                });
            }
#endif

#if FALSE
            /* Index Tickers (Public) */
            foreach (var pair in sample_pairs)
            {
                ws.SubscribeToIndexTickers(pair, (data) =>
                {
                    if (data != null)
                    {
                        // ... Your logic here
                    }
                });
            }
#endif

#if FALSE
            /* System Status (Public) */
            ws.SubscribeToSystemStatus((data) =>
            {
                if (data != null)
                {
                    // ... Your logic here
                }
            });
#endif

#if FALSE
            /* Account Updates (Private) */
            ws.SubscribeToAccountUpdates((data) =>
            {
                if (data != null)
                {
                    // ... Your logic here
                }
            });
#endif

#if FALSE
            /* Position Updates (Private) */
            ws.SubscribeToPositionUpdates(OkexInstrumentType.Futures, "INSTRUMENT", "UNDERLYING", (data) =>
            {
                if (data != null)
                {
                    // ... Your logic here
                }
            });
#endif

#if FALSE
            /* Balance And Position Updates (Private) */
            ws.SubscribeToBalanceAndPositionUpdates((data) =>
            {
                if (data != null)
                {
                    // ... Your logic here
                }
            });
#endif

#if FALSE
            /* Order Updates (Private) */
            ws.SubscribeToOrderUpdates(OkexInstrumentType.Futures, "INSTRUMENT", "UNDERLYING", (data) =>
            {
                if (data != null)
                {
                    // ... Your logic here
                }
            });
#endif

#if FALSE
            /* Algo Order Updates (Private) */
            ws.SubscribeToAlgoOrderUpdates(OkexInstrumentType.Futures, "INSTRUMENT", "UNDERLYING", (data) =>
            {
                if (data != null)
                {
                    // ... Your logic here
                }
            });
#endif

#if FALSE
            /* Advance Algo Order Updates (Private) */
            ws.SubscribeToAdvanceAlgoOrderUpdates(OkexInstrumentType.Futures, "INSTRUMENT", "UNDERLYING", (data) =>
            {
                if (data != null)
                {
                    // ... Your logic here
                }
            });
#endif
            #endregion

            // Stop Here
            Console.ReadLine();
            return;
        }
    }
}