using CryptoExchange.Net.Sockets;
using Microsoft.Extensions.Logging;
using Okex.Net.Enums;
using Okex.Net.RestObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Okex.Net.Examples
{
    internal class Program
    {
        private  static void Main(string[] args)
        {
            // Okex Rest Api Client
            OkexClient api = new OkexClient(new CoreObjects.OkexRestClientOptions { LogLevel = LogLevel.Debug});
            // api.SetApiCredentials("XXXXXXXX-API-KEY-XXXXXXXX", "XXXXXXXX-API-SECRET-XXXXXXXX", "XXXXXXXX-API-PASSPHRASE-XXXXXXXX");
            api.SetApiCredentials("33c3c18a-1ecd-4802-919e-1926b69b00aa", "AFEC3CF61D9785BEA0AA97C8720A8D66", "12345678");

            /* System Endpoints */
            //var system_01 = api.GetSystemStatus();

            /* Public Endpoints */
            //var public_01 = api.GetInstruments(OkexPublicInstrumentType.Spot);
            //var public_02 = api.GetInstruments(OkexPublicInstrumentType.Margin);
            //var public_03 = api.GetInstruments(OkexPublicInstrumentType.Swap);
            //var public_04 = api.GetInstruments(OkexPublicInstrumentType.Futures);
            //var public_05 = api.GetInstruments(OkexPublicInstrumentType.Option, "USD");

            //var public_06 = api.GetDeliveryExerciseHistory(OkexPublicInstrumentType.Futures, "BTC-USD");
            //var public_07 = api.GetDeliveryExerciseHistory(OkexPublicInstrumentType.Option, "BTC-USD");

            //var public_08 = api.GetOpenInterests(OkexPublicInstrumentType.Futures);
            //var public_09 = api.GetOpenInterests(OkexPublicInstrumentType.Option, "BTC-USD");
            //var public_10 = api.GetOpenInterests(OkexPublicInstrumentType.Swap);

            //var public_11 = api.GetFundingRates("BTC-USD-SWAP");

            // var public_12 = api.GetFundingRateHistory("BTC-USD-SWAP");
            //var public_13 = api.GetLimitPrice("BTC-USD-SWAP");
            //var public_14 = api.GetOptionMarketData("BTC-USD");
            //var public_15 = api.GetEstimatedPrice("BTC-USD-211004-41000-C");
            //var public_16 = api.GetDiscountInfo();
            //var public_17 = api.GetSystemTime();
            //var public_18 = api.GetLiquidationOrders(OkexPublicInstrumentType.Futures, underlying: "BTC-USD", alias: OkexPublicInstrumentAlias.Quarter, state: OkexPublicLiquidationState.Unfilled);
            //var public_19 = api.GetMarkPrices(OkexPublicInstrumentType.Futures);
            //var public_20 = api.GetPositionTiers(OkexPublicInstrumentType.Futures, OkexPublicMarginMode.Isolated, "BTC-USD");
            //var public_21 = api.GetInterestRates();
            //var public_22 = api.GetUnderlying( OkexPublicInstrumentType.Futures);
            //var public_23 = api.GetUnderlying( OkexPublicInstrumentType.Option);
            //var public_24 = api.GetUnderlying( OkexPublicInstrumentType.Swap);
            //var public_31 = api.GetAccountBalances();
            //var public_32 = api.GetAccountPositions();
            //var public_33 = api.GetAccountPositionRisk();
            //var public_34 = api.GetAccountConfiguration();
            //var public_34 = api.SetAccountPositionMode( OkexPositionMode.LongShortMode);
            //var public_34 = api.GetAccountLeverage("BTC-USD-211008", OkexPublicMarginMode.Isolated);
            //var public_35 = api.SetAccountLeverage(30, null, "BTC-USD-211008", OkexPublicMarginMode.Isolated, OkexPublicPositionSide.Long);
            //var public_35 = api.GetMaximumAmount("BTC-USDT", OkexTradeMode.Isolated);
            //var public_35 = api.GetMaximumLoanAmount("BTC-USDT", OkexPublicMarginMode.Cross);
            //var public_35 = api.GetFeeRates( OkexPublicInstrumentType.Spot, category: OkexFeeRateCategory.ClassA);
            //var public_36 = api.GetFeeRates( OkexInstrumentType.Futures, category: OkexFeeRateCategory.ClassA);
            //var public_36 = api.GetInterestAccrued();
            //var public_36 = api.GetInterestRate();
            //var public_36 = api.GetMaximumWithdrawals();
            //var public_36 = api.GetCurrencies();
            //var public_36 = api.GetFundingBalance();
            //var public_36 = api.GetDepositAddress("BTC");
            //var public_37 = api.GetDepositAddress("USDT");
            //var public_37 = api.GetTickers( OkexInstrumentType.Spot);
            //var public_37 = api.GetCandlesticks("BTC-USDT", OkexPeriod.OneHour);
            //var public_37 = api.Get24HourVolume();
            //var public_37 = api.GetOracle();
            var public_37 = api.GetIndexComponents("BTC-USDT");


            Console.ReadLine();















            // Console.ReadLine();
            // return;

            /* Sample Pairs * /
            var spot_pairs = new List<string> { "BTC-USDT", "LTC-USDT", "ETH-USDT", "XRP-USDT", "BCH-USDT", "EOS-USDT", "OKB-USDT", "ETC-USDT", "TRX-USDT", "BSV-USDT", "DASH-USDT", "NEO-USDT", "QTUM-USDT", "XLM-USDT", "ADA-USDT", "AE-USDT", "BLOC-USDT", "EGT-USDT", "IOTA-USDT", "SC-USDT", "WXT-USDT", "ZEC-USDT", };
            var futures_pairs = new List<string> { "BTC-USD-210625", "BTC-USD-210326", "BTC-USD-210101", "BTC-USD-201225", "LTC-USD-210625", "LTC-USD-210326", "LTC-USD-210101", "LTC-USD-201225", "ETH-USD-210326", "ETH-USD-210101", "ETH-USD-210625", "ETH-USD-201225", };
            var swap_pairs = new List<string> { "BTC-USD-SWAP", "LTC-USD-SWAP", "ETH-USD-SWAP", };

            /* Okex Socket Client Object * /
            var ws = new OkexSocketClient();

            /* WS Subscriptions * /
            var subs = new List<UpdateSubscription>();
             
            /* 00. Core - Public * /
            ws.SetApiCredentials("XXXXXXXX-API-KEY-XXXXXXXX", "XXXXXXXX-API-SECRET-XXXXXXXX", "XXXXXXXX-API-PASSPHRASE-XXXXXXXX"); // OR
            var ws_core_public_01 = ws.Auth_Login("XXXXXXXX-API-KEY-XXXXXXXX", "XXXXXXXX-API-SECRET-XXXXXXXX", "XXXXXXXX-API-PASSPHRASE-XXXXXXXX");

            /* 01. System - Public * /
            var ws_system_public_01 = ws.Ping();

            /* 03. Spot - Public * /

            // Ticker
            foreach (var pair in spot_pairs)
            {
                var subscription = ws.Spot_SubscribeToTicker(pair, (data) =>
                {
                    if (data != null)
                    {
                        Console.WriteLine($"Ticker >> {data.Symbol} >> LP:{data.LastPrice} LQ:{data.LastQuantity} Bid:{data.BestBidPrice} BS:{data.BestBidSize} Ask:{data.BestAskPrice} AS:{data.BestAskSize} 24O:{data.Open24H} 24H:{data.High24H} 24L:{data.Low24H} 24BV:{data.BaseVolume24H} 24QV:{data.QuoteVolume24H} ");
                    }
                });
                subs.Add(subscription.Data);
            }

            // Candlesticks
            foreach (var pair in spot_pairs)
            {
                var subscription = ws.Spot_SubscribeToCandlesticks(pair, OkexSpotPeriod.FiveMinutes, (data) =>
                {
                    if (data != null)
                    {
                        Console.WriteLine($"Candle >> {data.Symbol} >> ST:{data.StartTime} O:{data.Open} H:{data.High} L:{data.Low} C:{data.Close} V:{data.Volume}");
                    }
                });
                subs.Add(subscription.Data);
            }

            // Trades
            foreach (var pair in spot_pairs)
            {
                var subscription = ws.Spot_SubscribeToTrades(pair, (data) =>
                {
                    if (data != null)
                    {
                        Console.WriteLine($"Trades >> {data.Symbol} >> ID:{data.TradeId} P:{data.Price} A:{data.Size} S:{data.Side} T:{data.Timestamp}");
                    }
                });
                subs.Add(subscription.Data);
            }

            // Order Book
            foreach (var pair in spot_pairs)
            {
                var subscription = ws.Spot_SubscribeToOrderBook(pair, OkexOrderBookDepth.Depth400, (data) =>
                {
                    if (data != null && data.Asks != null && data.Asks.Count() > 0 && data.Bids != null && data.Bids.Count() > 0)
                    {
                        Console.WriteLine($"Depth >> {data.Symbol} >> Ask P:{data.Asks.First().Price} Q:{data.Asks.First().Quantity} C:{data.Asks.First().OrdersCount} Bid P:{data.Bids.First().Price} Q:{data.Bids.First().Quantity} C:{data.Bids.First().OrdersCount} ");
                    }
                });
                subs.Add(subscription.Data);
            }

            // Unsubscribe
            foreach (var sub in subs)
            {
                _ = ws.UnsubscribeAsync(sub);
            }

            /* 03. Spot - Private * /

            // Balance
            ws.Spot_SubscribeToBalance("ETH", (data) =>
            {
                if (data != null)
                {
                    Console.WriteLine($"Balance Update >> {data.Currency} >> Balance:{data.Balance} Available:{data.Available} Frozen:{data.Frozen}");
                }
            });

            // Orders
            ws.Spot_SubscribeToOrders("ETH-USDT", (data) =>
            {
                if (data != null)
                {
                    Console.WriteLine($"Order Update >> {data.Symbol} >> Id:{data.OrderId} State:{data.State}");
                }
            });

            // Algo Orders
            ws.Spot_SubscribeToAlgoOrders("ETH-USDT", (data) =>
            {
                if (data != null)
                {
                    Console.WriteLine($"Order Update >> {data.Symbol} >> Id:{data.OrderId} State:{data.Status}");
                }
            });

            /* 04. Margin - Private * /

            // Balance
            ws.Margin_SubscribeToBalance("ETH-USDT", (data) =>
            {
                if (data != null)
                {
                    Console.WriteLine($"Balance Update >> {data.ProductId} >> MarginRatio:{data.MarginRatio} Liq:{data.LiquidationPrice}");
                }
            });

            /* 05. Futures - Public * /

            // Contracts
            ws.Futures_SubscribeToContracts((data) =>
            {
                if (data != null)
                {
                    Console.WriteLine($"Contract >> {data.Symbol} >> BC:{data.BaseCurrency} QC:{data.QuoteCurrency}");
                }
            });

            // Ticker
            foreach (var pair in futures_pairs)
            {
                var subscription = ws.Futures_SubscribeToTicker(pair, (data) =>
                {
                    if (data != null)
                    {
                        Console.WriteLine($"Ticker >> {data.Symbol} >> LP:{data.LastPrice} LQ:{data.LastQuantity} Bid:{data.BestBidPrice} BS:{data.BestBidSize} Ask:{data.BestAskPrice} AS:{data.BestAskSize} 24H:{data.High24H} 24L:{data.Low24H} 24BV:{data.BaseVolume24H} 24QV:{data.QuoteVolume24H} ");
                    }
                });
                subs.Add(subscription.Data);
            }

            // Candlesticks
            foreach (var pair in futures_pairs)
            {
                var subscription = ws.Futures_SubscribeToCandlesticks(pair, OkexSpotPeriod.FiveMinutes, (data) =>
                {
                    if (data != null)
                    {
                        Console.WriteLine($"Candle >> {data.Symbol} >> ST:{data.StartTime} O:{data.Open} H:{data.High} L:{data.Low} C:{data.Close} V:{data.BaseVolume}");
                    }
                });
                subs.Add(subscription.Data);
            }

            // Trades
            foreach (var pair in futures_pairs)
            {
                var subscription = ws.Futures_SubscribeToTrades(pair, (data) =>
                {
                    if (data != null)
                    {
                        Console.WriteLine($"Trades >> {pair} >> ID:{data.TradeId} P:{data.Price} Q:{data.Quantity} S:{data.Side} T:{data.Timestamp}");
                    }
                });
                subs.Add(subscription.Data);
            }

            // Price Range
            foreach (var pair in futures_pairs)
            {
                var subscription = ws.Futures_SubscribeToPriceRange(pair, (data) =>
                {
                    if (data != null)
                    {
                        Console.WriteLine($"Price Range >> {pair} >> H:{data.Highest} L:{data.Lowest} T:{data.Timestamp}");
                    }
                });
                subs.Add(subscription.Data);
            }

            // Estimated Price
            foreach (var pair in futures_pairs)
            {
                var subscription = ws.Futures_SubscribeToEstimatedPrice(pair, (data) =>
                {
                    if (data != null)
                    {
                        Console.WriteLine($"Estimated Price >> {pair} >> R:{data.Rate} T:{data.Timestamp}");
                    }
                });
                subs.Add(subscription.Data);
            }

            // Order Book
            foreach (var pair in futures_pairs)
            {
                var subscription = ws.Futures_SubscribeToOrderBook(pair, OkexOrderBookDepth.Depth400, (data) =>
                {
                    if (data != null && data.Asks != null && data.Asks.Count() > 0 && data.Bids != null && data.Bids.Count() > 0)
                    {
                        Console.WriteLine($"Depth >> {data.Symbol} >> Ask P:{data.Asks.First().Price} Q:{data.Asks.First().Quantity} C:{data.Asks.First().OrdersCount} Bid P:{data.Bids.First().Price} Q:{data.Bids.First().Quantity} C:{data.Bids.First().OrdersCount} ");
                    }
                });
                subs.Add(subscription.Data);
            }

            // Mark Price
            foreach (var pair in futures_pairs)
            {
                var subscription = ws.Futures_SubscribeToMarkPrice(pair, (data) =>
                {
                    if (data != null)
                    {
                        Console.WriteLine($"Trades >> {pair} >> P:{data.MarkPrice} T:{data.Timestamp}");
                    }
                });
                subs.Add(subscription.Data);
            }

            /* 05. Futures - Private * /

            // Positions
            ws.Futures_SubscribeToPositions("ETH-USDT", (data) =>
            {
                if (data != null)
                {
                    Console.WriteLine($"Positions Update >> {data.Symbol} >> RPNL:{data.RealisedPnl} LUPNL:{data.LongUnrealisedPnl} SUPNL:{data.ShortUnrealisedPnl}");
                }
            });

            // Balance
            ws.Futures_SubscribeToBalance("ETH-USDT", (data) =>
            {
                if (data != null)
                {
                    Console.WriteLine($"Balance Update >> {data.Currency} >> Balance:{data.TotalAvailableBalance} Available:{data.MarginMode} Frozen:{data.MarginFrozen}");
                }
            });

            // Orders
            ws.Futures_SubscribeToOrders("ETH-USDT", (data) =>
            {
                if (data != null)
                {
                    Console.WriteLine($"Order Update >> {data.Symbol} >> Id:{data.OrderId} State:{data.State}");
                }
            });

            // Algo Orders
            ws.Futures_SubscribeToAlgoOrders("ETH-USDT", (data) =>
            {
                if (data != null)
                {
                    Console.WriteLine($"Order Update >> {data.Symbol} >> Id:{data.OrderId} State:{data.Status}");
                }
            });

            /* 06. Swap - Public * /

            // Ticker
            foreach (var pair in swap_pairs)
            {
                var subscription = ws.Swap_SubscribeToTicker(pair, (data) =>
                {
                    if (data != null)
                    {
                        Console.WriteLine($"Ticker >> {data.Symbol} >> LP:{data.LastPrice} LQ:{data.LastQuantity} Bid:{data.BestBidPrice} BS:{data.BestBidSize} Ask:{data.BestAskPrice} AS:{data.BestAskSize} 24H:{data.High24H} 24L:{data.Low24H} 24BV:{data.BaseVolume24H} 24QV:{data.QuoteVolume24H} ");
                    }
                });
                subs.Add(subscription.Data);
            }

            // Candlesticks
            foreach (var pair in swap_pairs)
            {
                var subscription = ws.Swap_SubscribeToCandlesticks(pair, OkexSpotPeriod.FiveMinutes, (data) =>
                {
                    if (data != null)
                    {
                        Console.WriteLine($"Candle >> {data.Symbol} >> ST:{data.StartTime} O:{data.Open} H:{data.High} L:{data.Low} C:{data.Close} V:{data.BaseVolume}");
                    }
                });
                subs.Add(subscription.Data);
            }

            // Trades
            foreach (var pair in swap_pairs)
            {
                var subscription = ws.Swap_SubscribeToTrades(pair, (data) =>
                {
                    if (data != null)
                    {
                        Console.WriteLine($"Trades >> {pair} >> ID:{data.TradeId} P:{data.Price} Q:{data.Quantity} S:{data.Side} T:{data.Timestamp}");
                    }
                });
                subs.Add(subscription.Data);
            }

            // Funding Rate
            foreach (var pair in swap_pairs)
            {
                var subscription = ws.Swap_SubscribeToFundingRate(pair, (data) =>
                {
                    if (data != null)
                    {
                        Console.WriteLine($"Price Range >> {pair} >> RR:{data.RealizedRate} FR:{data.FundingRate} IR:{data.InterestRate}");
                    }
                });
                subs.Add(subscription.Data);
            }

            // Price Range
            foreach (var pair in swap_pairs)
            {
                var subscription = ws.Swap_SubscribeToPriceRange(pair, (data) =>
                {
                    if (data != null)
                    {
                        Console.WriteLine($"Price Range >> {pair} >> H:{data.Highest} L:{data.Lowest} T:{data.Timestamp}");
                    }
                });
                subs.Add(subscription.Data);
            }

            // Order Book
            foreach (var pair in swap_pairs)
            {
                var subscription = ws.Swap_SubscribeToOrderBook(pair, OkexOrderBookDepth.Depth400, (data) =>
                {
                    if (data != null && data.Asks != null && data.Asks.Count() > 0 && data.Bids != null && data.Bids.Count() > 0)
                    {
                        Console.WriteLine($"Depth >> {data.Symbol} >> Ask P:{data.Asks.First().Price} Q:{data.Asks.First().Quantity} C:{data.Asks.First().OrdersCount} Bid P:{data.Bids.First().Price} Q:{data.Bids.First().Quantity} C:{data.Bids.First().OrdersCount} ");
                    }
                });
                subs.Add(subscription.Data);
            }

            // Mark Price
            foreach (var pair in swap_pairs)
            {
                var subscription = ws.Swap_SubscribeToMarkPrice(pair, (data) =>
                {
                    if (data != null)
                    {
                        Console.WriteLine($"Trades >> {pair} >> P:{data.MarkPrice} T:{data.Timestamp}");
                    }
                });
                subs.Add(subscription.Data);
            }

            /* 06. Swap - Private * /

            // Positions
            ws.Swap_SubscribeToPositions("BTC-USD-SWAP", (data) =>
            {
                if (data != null)
                {
                    Console.WriteLine($"Positions Update >> BTC-USD-SWAP >> MM:{data.MarginMode} T:{data.Timestamp}");
                }
            });

            // Balance
            ws.Swap_SubscribeToBalance("BTC-USD-SWAP", (data) =>
            {
                if (data != null)
                {
                    Console.WriteLine($"Balance Update >> BTC-USD-SWAP >> Balance:{data.TotalAvailableBalance} Available:{data.MarginMode} Frozen:{data.MarginFrozen}");
                }
            });

            // Orders
            ws.Swap_SubscribeToOrders("BTC-USD-SWAP", (data) =>
            {
                if (data != null)
                {
                    Console.WriteLine($"Order Update >> {data.Symbol} >> Id:{data.OrderId} State:{data.State}");
                }
            });

            // Algo Orders
            ws.Swap_SubscribeToAlgoOrders("BTC-USD-SWAP", (data) =>
            {
                if (data != null)
                {
                    Console.WriteLine($"Order Update >> {data.Symbol} >> Id:{data.OrderId} State:{data.Status}");
                }
            });

            /* 07. Options - Public * /

            // Contracts
            ws.Options_SubscribeToContracts("BTC-USD", (data) =>
            {
                if (data != null)
                {
                    Console.WriteLine($"Contract >> {data.Instrument} >> C:{data.Category} U:{data.Underlying}");
                }
            });

            // MarketData
            ws.Options_SubscribeToMarketData("BTC-USD", (data) =>
            {
                if (data != null)
                {
                    Console.WriteLine($"Ticker >> {data.Instrument} >> LP:{data.Last} Bid:{data.BestBid} BS:{data.BidVolume} Ask:{data.BestAsk} AS:{data.AskVolume} 24H:{data.High24H} 24L:{data.Low24H}");
                }
            });

            // Candlesticks
            ws.Options_SubscribeToCandlesticks("BTC-USD-201218-16250-C", OkexSpotPeriod.FiveMinutes, (data) =>
            {
                if (data != null)
                {
                    Console.WriteLine($"Candle >> {data.Symbol} >> ST:{data.StartTime} O:{data.Open} H:{data.High} L:{data.Low} C:{data.Close} V:{data.BaseVolume}");
                }
            });

            // Trades
            ws.Options_SubscribeToTrades("BTC-USD-201218-16250-C", (data) =>
            {
                if (data != null)
                {
                    Console.WriteLine($"Trades >> ID:{data.TradeId} P:{data.Price} Q:{data.Quantity} S:{data.Side} T:{data.Timestamp}");
                }
            });

            // Ticker
            ws.Options_SubscribeToTicker("BTC-USD-201218-16250-C", (data) =>
            {
                if (data != null)
                {
                    Console.WriteLine($"Ticker >> {data.Symbol} >> LP:{data.LastPrice} LQ:{data.LastQuantity} Bid:{data.BestBidPrice} BS:{data.BestBidSize} Ask:{data.BestAskPrice} AS:{data.BestAskSize} 24H:{data.High24H} 24L:{data.Low24H}");
                }
            });

            // Order Book
            ws.Options_SubscribeToOrderBook("BTC-USD-201218-16250-C", OkexOrderBookDepth.Depth400, (data) =>
            {
                if (data != null && data.Asks != null && data.Asks.Count() > 0 && data.Bids != null && data.Bids.Count() > 0)
                {
                    Console.WriteLine($"Depth >> {data.Symbol} >> Ask P:{data.Asks.First().Price} Q:{data.Asks.First().Quantity} C:{data.Asks.First().OrdersCount} Bid P:{data.Bids.First().Price} Q:{data.Bids.First().Quantity} C:{data.Bids.First().OrdersCount} ");
                }
            });

            /* 07. Options - Private * /

            // Positions
            ws.Options_SubscribeToPositions("BTC-USD", (data) =>
            {
                if (data != null)
                {
                    Console.WriteLine($"Positions Update >> {data.Instrument} >> RPNL:{data.RealizedPnl} UPNL:{data.UnrealizedPnl}");
                }
            });

            // Balance
            ws.Options_SubscribeToBalance("BTC-USD", (data) =>
            {
                if (data != null)
                {
                    Console.WriteLine($"Balance Update >> {data.Currency} >> Balance:{data.TotalAvailableBalance} Status:{data.AccountStatus}");
                }
            });

            // Orders
            ws.Options_SubscribeToOrders("BTC-USD", (data) =>
            {
                if (data != null)
                {
                    Console.WriteLine($"Order Update >> {data.Instrument} >> Id:{data.OrderId} State:{data.State}");
                }
            });

            /* 09. Index - Public * /

            // Ticker
            ws.Index_SubscribeToTicker("BTC-USD", (data) =>
            {
                if (data != null)
                {
                    Console.WriteLine($"Ticker >> {data.Symbol} >> 24O:{data.Open24H} 24H:{data.High24H} 24L:{data.Low24H}");
                }
            });

            // Candlesticks
            ws.Index_SubscribeToCandlesticks("BTC-USD", OkexSpotPeriod.FiveMinutes, (data) =>
            {
                if (data != null)
                {
                    Console.WriteLine($"Candle >> {data.Symbol} >> ST:{data.StartTime} O:{data.Open} H:{data.High} L:{data.Low} C:{data.Close} V:{data.BaseVolume}");
                }
            });
            */

            Console.ReadLine();
            return;
        }
    }
}
