using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using Okex.Net.Converters;
using Okex.Net.RestObjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Okex.Net.Helpers;
using System.Linq;
using Okex.Net.Enums;

namespace Okex.Net
{
	public partial class OkexClient
	{
		#region Algo Tradimg API

		#region Private Signed Endpoints

		/// <summary>
		/// Places an Algo Order
		/// Rate limit：40 requests per 2 seconds
		/// </summary>
		/// <param name="symbol">Trading pair symbol</param>
		/// <param name="type">1. trigger order； 2. trail order; 3. iceberg order; 4. time-weighted average price 5. stop order;</param>
		/// <param name="mode">1：spot 2：margin</param>
		/// <param name="side">buy or sell</param>
		/// <param name="size">Total number of orders must between 0 and 1,000,000, incl. both numbers</param>
		/// <param name="trigger_price">Trigger price must be between 0 and 1,000,000</param>
		/// <param name="trigger_algo_price">Order price must be between 0 and 1,000,000</param>
		/// <param name="trigger_algo_type">1: Limit 2: Market ; trigger price type, default is limit price; when it is the market price, the commission price need not be filled;</param>
		/// <param name="trail_callback_rate">Callback rate must be between 0.001 (0.1%) and 0.05 (5%）</param>
		/// <param name="trail_trigger_price">Trigger price must be between 0 and 1,000,000</param>
		/// <param name="iceberg_algo_variance">Order depth must be between 0.0001 (0.01%) and 0.01 (1%)</param>
		/// <param name="iceberg_avg_amount">Single order average amount,Single average value, fill in the value 1/1000 of the total amount \ <= X \ <= total amount</param>
		/// <param name="iceberg_limit_price">Price limit must be between 0 and 1,000,000</param>
		/// <param name="twap_sweep_range">Auto-cancelling order range must be between 0.005 (0.5%) and 0.01 (1%), incl. both numbers</param>
		/// <param name="twap_sweep_ratio">Auto-cancelling order rate must be between 0.01 and 1, incl. both numbers</param>
		/// <param name="twap_single_limit">Single order limit,fill in the value 1/1000 of the total amount \ <= X \ <= total amount</param>
		/// <param name="twap_limit_price">Price limit must be between 0 and 1,000,000, incl, 1,000,000</param>
		/// <param name="twap_time_interval">Time interval must be between 5 and 120, incl. both numbers</param>
		/// <param name="tp_trigger_type">1:limit 2:market；TP trigger type，The default is limit price；</param>
		/// <param name="tp_trigger_price">TP trigger price must be between 0 and 1,000,000</param>
		/// <param name="tp_price">TP order price must be between 0 and 1,000,000</param>
		/// <param name="sl_trigger_type">1:limit 2:market；TP trigger type，The default is limit price；When it is the market price, the tp_price does not need to be filled;</param>
		/// <param name="sl_trigger_price">SL trigger price must be between 0 and 1,000,000</param>
		/// <param name="sl_price">SL order price must be between 0 and 1,000,000</param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public WebCallResult<OkexAlgoPlacedOrder> Algo_PlaceOrder(
			/* General Parameters */
			string symbol,
			OkexAlgoOrderType type,
			OkexMarket mode,
			OkexSpotOrderSide side,
			decimal size,

			/* Trigger Order Parameters */
			decimal? trigger_price = null,
			decimal? trigger_algo_price = null,
			OkexAlgoPriceType? trigger_algo_type = null,

			/* Trail Order Parameters */
			decimal? trail_callback_rate = null,
			decimal? trail_trigger_price = null,

			/* Iceberg Order Parameters (Maximum 6 orders) */
			decimal? iceberg_algo_variance = null,
			decimal? iceberg_avg_amount = null,
			decimal? iceberg_limit_price = null,

			/* TWAP Parameters (Maximum 6 orders) */
			decimal? twap_sweep_range = null,
			decimal? twap_sweep_ratio = null,
			int? twap_single_limit = null,
			decimal? twap_limit_price = null,
			int? twap_time_interval = null,

			/* Stop Order Parameters */
			OkexAlgoPriceType? tp_trigger_type = null,
			decimal? tp_trigger_price = null,
			decimal? tp_price = null,
			OkexAlgoPriceType? sl_trigger_type = null,
			decimal? sl_trigger_price = null,
			decimal? sl_price = null,

			/* Cancellation Token */
			CancellationToken ct = default)
			=> Algo_PlaceOrder_Async(
			symbol,
			type,
			mode,
			side,
			size,

			/* Trigger Order Parameters */
			trigger_price,
			trigger_algo_price,
			trigger_algo_type,

			/* Trail Order Parameters */
			trail_callback_rate,
			trail_trigger_price,

			/* Iceberg Order Parameters (Maximum 6 orders) */
			iceberg_algo_variance,
			iceberg_avg_amount,
			iceberg_limit_price,

			/* TWAP Parameters (Maximum 6 orders) */
			twap_sweep_range,
			twap_sweep_ratio,
			twap_single_limit,
			twap_limit_price,
			twap_time_interval,

			/* Stop Order Parameters */
			tp_trigger_type,
			tp_trigger_price,
			tp_price,
			sl_trigger_type,
			sl_trigger_price,
			sl_price,

			/* Cancellation Token */
			ct).Result;
		/// <summary>
		/// Places an Algo Order
		/// Rate limit：40 requests per 2 seconds
		/// </summary>
		/// <param name="symbol">Trading pair symbol</param>
		/// <param name="type">1. trigger order； 2. trail order; 3. iceberg order; 4. time-weighted average price 5. stop order;</param>
		/// <param name="mode">1：spot 2：margin</param>
		/// <param name="side">buy or sell</param>
		/// <param name="size">Total number of orders must between 0 and 1,000,000, incl. both numbers</param>
		/// <param name="trigger_price">Trigger price must be between 0 and 1,000,000</param>
		/// <param name="trigger_algo_price">Order price must be between 0 and 1,000,000</param>
		/// <param name="trigger_algo_type">1: Limit 2: Market ; trigger price type, default is limit price; when it is the market price, the commission price need not be filled;</param>
		/// <param name="trail_callback_rate">Callback rate must be between 0.001 (0.1%) and 0.05 (5%）</param>
		/// <param name="trail_trigger_price">Trigger price must be between 0 and 1,000,000</param>
		/// <param name="iceberg_algo_variance">Order depth must be between 0.0001 (0.01%) and 0.01 (1%)</param>
		/// <param name="iceberg_avg_amount">Single order average amount,Single average value, fill in the value 1/1000 of the total amount \ <= X \ <= total amount</param>
		/// <param name="iceberg_limit_price">Price limit must be between 0 and 1,000,000</param>
		/// <param name="twap_sweep_range">Auto-cancelling order range must be between 0.005 (0.5%) and 0.01 (1%), incl. both numbers</param>
		/// <param name="twap_sweep_ratio">Auto-cancelling order rate must be between 0.01 and 1, incl. both numbers</param>
		/// <param name="twap_single_limit">Single order limit,fill in the value 1/1000 of the total amount \ <= X \ <= total amount</param>
		/// <param name="twap_limit_price">Price limit must be between 0 and 1,000,000, incl, 1,000,000</param>
		/// <param name="twap_time_interval">Time interval must be between 5 and 120, incl. both numbers</param>
		/// <param name="tp_trigger_type">1:limit 2:market；TP trigger type，The default is limit price；</param>
		/// <param name="tp_trigger_price">TP trigger price must be between 0 and 1,000,000</param>
		/// <param name="tp_price">TP order price must be between 0 and 1,000,000</param>
		/// <param name="sl_trigger_type">1:limit 2:market；TP trigger type，The default is limit price；When it is the market price, the tp_price does not need to be filled;</param>
		/// <param name="sl_trigger_price">SL trigger price must be between 0 and 1,000,000</param>
		/// <param name="sl_price">SL order price must be between 0 and 1,000,000</param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns></returns>
		public async Task<WebCallResult<OkexAlgoPlacedOrder>> Algo_PlaceOrder_Async(
			/* General Parameters */
			string symbol,
			OkexAlgoOrderType type,
			OkexMarket mode,
			OkexSpotOrderSide side,
			decimal size,

			/* Trigger Order Parameters */
			decimal? trigger_price = null,
			decimal? trigger_algo_price = null,
			OkexAlgoPriceType? trigger_algo_type = null,

			/* Trail Order Parameters */
			decimal? trail_callback_rate = null,
			decimal? trail_trigger_price = null,

			/* Iceberg Order Parameters (Maximum 6 orders) */
			decimal? iceberg_algo_variance = null,
			decimal? iceberg_avg_amount = null,
			decimal? iceberg_limit_price = null,

			/* TWAP Parameters (Maximum 6 orders) */
			decimal? twap_sweep_range = null,
			decimal? twap_sweep_ratio = null,
			int? twap_single_limit = null,
			decimal? twap_limit_price = null,
			int? twap_time_interval = null,

			/* Stop Order Parameters */
			OkexAlgoPriceType? tp_trigger_type = null,
			decimal? tp_trigger_price = null,
			decimal? tp_price = null,
			OkexAlgoPriceType? sl_trigger_type = null,
			decimal? sl_trigger_price = null,
			decimal? sl_price = null,

			/* Cancellation Token */
			CancellationToken ct = default)
		{
			symbol = symbol.ValidateSymbol();

			var parameters = new Dictionary<string, object>
			{
				{ "instrument_id", symbol },
				{ "order_type", JsonConvert.SerializeObject(type, new AlgoOrderTypeConverter(false)) },
				{ "size", size },
				{ "mode", JsonConvert.SerializeObject(mode, new MarketConverter(false)) },
				{ "side", JsonConvert.SerializeObject(side, new SpotOrderSideConverter(false)) },
			};

			if (type == OkexAlgoOrderType.TriggerOrder)
			{
				if (trigger_price == null) throw new ArgumentException("trigger_price is mandatory for Trigger Order");
				if (trigger_algo_price == null) throw new ArgumentException("trigger_algo_price is mandatory for Trigger Order");
				// if(trigger_algo_type == null) throw new ArgumentException("trigger_algo_type is mandatory for Trigger Order");

				parameters.AddParameter("trigger_price", trigger_price);
				parameters.AddParameter("algo_price", trigger_algo_price);
				parameters.AddOptionalParameter("algo_type", JsonConvert.SerializeObject(side, new AlgoPriceTypeConverter(false)));
			}

			else if (type == OkexAlgoOrderType.TrailOrder)
			{
				if (trail_callback_rate == null) throw new ArgumentException("trail_callback_rate is mandatory for Trail Order");
				if (trail_trigger_price == null) throw new ArgumentException("trail_trigger_price is mandatory for Trail Order");

				parameters.AddParameter("callback_rate", trail_callback_rate);
				parameters.AddParameter("trigger_price", trail_trigger_price);
			}

			else if (type == OkexAlgoOrderType.IcebergOrder)
			{
				if (iceberg_algo_variance == null) throw new ArgumentException("iceberg_algo_variance is mandatory for Iceberg Order");
				if (iceberg_avg_amount == null) throw new ArgumentException("iceberg_avg_amount is mandatory for Iceberg Order");
				if (iceberg_limit_price == null) throw new ArgumentException("iceberg_limit_price is mandatory for Iceberg Order");

				parameters.AddParameter("algo_variance", iceberg_algo_variance);
				parameters.AddParameter("avg_amount", iceberg_avg_amount);
				parameters.AddParameter("limit_price", iceberg_limit_price);
			}

			else if (type == OkexAlgoOrderType.TWAP)
			{
				if (twap_sweep_range == null) throw new ArgumentException("twap_sweep_range is mandatory for TWAP Order");
				if (twap_sweep_ratio == null) throw new ArgumentException("twap_sweep_ratio is mandatory for TWAP Order");
				if (twap_single_limit == null) throw new ArgumentException("twap_single_limit is mandatory for TWAP Order");
				if (twap_limit_price == null) throw new ArgumentException("twap_limit_price is mandatory for TWAP Order");
				if (twap_time_interval == null) throw new ArgumentException("twap_time_interval is mandatory for TWAP Order");

				parameters.AddParameter("sweep_range", twap_sweep_range);
				parameters.AddParameter("sweep_ratio", twap_sweep_ratio);
				parameters.AddParameter("single_limit", twap_single_limit);
				parameters.AddParameter("limit_price", twap_limit_price);
				parameters.AddParameter("time_interval", twap_time_interval);
			}

			else if (type == OkexAlgoOrderType.StopOrder)
			{
				//if(tp_trigger_type == null) throw new ArgumentException("tp_trigger_type is mandatory for Stop Order");
				//if(tp_trigger_price == null) throw new ArgumentException("tp_trigger_price is mandatory for Stop Order");
				//if(tp_price == null) throw new ArgumentException("tp_price is mandatory for Stop Order");
				//if(sl_trigger_type == null) throw new ArgumentException("sl_trigger_type is mandatory for Stop Order");
				//if(sl_trigger_price == null) throw new ArgumentException("sl_trigger_price is mandatory for Stop Order");
				//if(sl_price == null) throw new ArgumentException("sl_price is mandatory for Stop Order");

				parameters.AddOptionalParameter("tp_trigger_type", tp_trigger_type);
				parameters.AddOptionalParameter("tp_trigger_price", tp_trigger_price);
				parameters.AddOptionalParameter("tp_price", tp_price);
				parameters.AddOptionalParameter("sl_trigger_type", sl_trigger_type);
				parameters.AddOptionalParameter("sl_trigger_price", sl_trigger_price);
				parameters.AddOptionalParameter("sl_price", sl_price);
			}

			return await SendRequest<OkexAlgoPlacedOrder>(GetUrl(Endpoints_Algo_PlaceOrder), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
		}

		/// <summary>
		/// If user use "algo_id" to cancel unfulfilled orders, they can cancel a maximum of 6 iceberg/TWAP or 10 trigger/trail orders at the same time.
		/// Rate limit：20 requests per 2 seconds
		/// Examples:
		/// - Single Order Cancellation: POST /api/spot/v3/cancel_batch_algos{"instrument_id": "BTC-USDT","order_type":"1","algo_ids": ["1600593327162368"]}
		/// - Batch Order Cancellation: POST /api/spot/v3/cancel_batch_algos{"instrument_id": "BTC-USDT","order_type":"1","algo_ids":["1600593327162368","1600593327162369"]}
		/// </summary>
		/// <param name="symbol">Trading pair symbol</param>
		/// <param name="type">1. trigger order; 2. trail order; 3. iceberg order; 4. time-weighted average price ; 5. stop order</param>
		/// <param name="algo_ids">Cancel specific order ID</param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns>
		/// Return Parameter: Return parameter is the order ID of canceled orders. This does not mean that the orders are successfully canceled. Orders that are pending cannot be canceled, only unfulfilled orders can be canceled.
		/// Description: This does not guarantee orders are canceled successfully. Users are advised to request the order list to confirm after using the cancelation endpoint.
		/// </returns>
		public WebCallResult<OkexAlgoCancelledOrder> Algo_CancelOrder(string symbol, OkexAlgoOrderType type, IEnumerable<long> algo_ids, CancellationToken ct = default) => Algo_CancelOrder_Async(symbol, type, algo_ids, ct).Result;
		/// <summary>
		/// If user use "algo_id" to cancel unfulfilled orders, they can cancel a maximum of 6 iceberg/TWAP or 10 trigger/trail orders at the same time.
		/// Rate limit：20 requests per 2 seconds
		/// Examples:
		/// - Single Order Cancellation: POST /api/spot/v3/cancel_batch_algos{"instrument_id": "BTC-USDT","order_type":"1","algo_ids": ["1600593327162368"]}
		/// - Batch Order Cancellation: POST /api/spot/v3/cancel_batch_algos{"instrument_id": "BTC-USDT","order_type":"1","algo_ids":["1600593327162368","1600593327162369"]}
		/// </summary>
		/// <param name="symbol">Trading pair symbol</param>
		/// <param name="type">1. trigger order; 2. trail order; 3. iceberg order; 4. time-weighted average price ; 5. stop order</param>
		/// <param name="algo_ids">Cancel specific order ID</param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns>
		/// Return Parameter: Return parameter is the order ID of canceled orders. This does not mean that the orders are successfully canceled. Orders that are pending cannot be canceled, only unfulfilled orders can be canceled.
		/// Description: This does not guarantee orders are canceled successfully. Users are advised to request the order list to confirm after using the cancelation endpoint.
		/// </returns>
		public async Task<WebCallResult<OkexAlgoCancelledOrder>> Algo_CancelOrder_Async(string symbol, OkexAlgoOrderType type, IEnumerable<long> algo_ids, CancellationToken ct = default)
		{
			symbol = symbol.ValidateSymbol();

			if (algo_ids == null && algo_ids.Count() == 0)
				throw new ArgumentException("algo_ids is mandatory.");

			var parameters = new Dictionary<string, object>
			{
				{ "instrument_id", symbol },
				{ "algo_ids", algo_ids! },
				{ "order_type", JsonConvert.SerializeObject(type, new AlgoOrderTypeConverter(false)) },
			};

			return await SendRequest<OkexAlgoCancelledOrder>(GetUrl(Endpoints_Algo_CancelOrder), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
		}

		/// <summary>
		/// Obtaining Order List
		/// Rate limit：20 requests per 2 seconds
		/// </summary>
		/// <param name="symbol">Trading pair symbol</param>
		/// <param name="type">1. trigger order; 2. trail order; 3. iceberg order; 4. time-weighted average price ; 5. stop order</param>
		/// <param name="status">[Status and algo_ids are mandatory, select either one] Order status: (1. Pending; 2. 2. Effective; 3. Cancelled; 4. Partially effective; 5. Paused; 6. Order failed [Status 4 and 5 only applies to iceberg and TWAP orders]</param>
		/// <param name="algo_ids">[status and algo_ids are mandatory, select either one] Enquiry specific order ID</param>
		/// <param name="limit">[Optional] Request page content after this ID (updated records)</param>
		/// <param name="before">[Optional] Request page content before this ID (past records)</param>
		/// <param name="after">[Optional] The number of results returned by the page. Default and maximum are both 100 (see the description on page for more details)</param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns>Symbol grouped algo orders list. Dictionary&lt;string: symbol, IEnumerable&lt;OkexSpotAlgoOrder&gt;: algo orders&gt;</returns>
		/// <returns></returns>
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8604 // Possible null reference argument.
		public WebCallResult<Dictionary<string, IEnumerable<OkexAlgoOrder>>> Algo_GetOrders(string symbol, OkexAlgoOrderType type, OkexAlgoStatus? status = null, IEnumerable<long> algo_ids = null, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default) => Algo_GetOrders_Async(symbol, type, status, algo_ids, limit, before, after, ct).Result;
#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
		/// <summary>
		/// Obtaining Order List
		/// Rate limit：20 requests per 2 seconds
		/// </summary>
		/// <param name="symbol">Trading pair symbol</param>
		/// <param name="type">1. trigger order; 2. trail order; 3. iceberg order; 4. time-weighted average price ; 5. stop order</param>
		/// <param name="status">[Status and algo_ids are mandatory, select either one] Order status: (1. Pending; 2. 2. Effective; 3. Cancelled; 4. Partially effective; 5. Paused; 6. Order failed [Status 4 and 5 only applies to iceberg and TWAP orders]</param>
		/// <param name="algo_ids">[status and algo_ids are mandatory, select either one] Enquiry specific order ID</param>
		/// <param name="limit">[Optional] Request page content after this ID (updated records)</param>
		/// <param name="before">[Optional] Request page content before this ID (past records)</param>
		/// <param name="after">[Optional] The number of results returned by the page. Default and maximum are both 100 (see the description on page for more details)</param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns>Symbol grouped algo orders list. Dictionary&lt;string: symbol, IEnumerable&lt;OkexSpotAlgoOrder&gt;: algo orders&gt;</returns>
		/// <returns></returns>
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
		public async Task<WebCallResult<Dictionary<string, IEnumerable<OkexAlgoOrder>>>> Algo_GetOrders_Async(string symbol, OkexAlgoOrderType type, OkexAlgoStatus? status = null, IEnumerable<long> algo_ids = null, int limit = 100, long? before = null, long? after = null, CancellationToken ct = default)
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
		{
			symbol = symbol.ValidateSymbol();
			limit.ValidateIntBetween(nameof(limit), 1, 100);

			if (status == null && (algo_ids == null || algo_ids.Count() == 0))
				throw new ArgumentException("status and algo_ids are mandatory, select either one");

			var parameters = new Dictionary<string, object>
			{
				{ "instrument_id", symbol },
				{ "order_type", JsonConvert.SerializeObject(type, new AlgoOrderTypeConverter(false)) },
				{ "limit", limit },
			};
			parameters.AddOptionalParameter("status", JsonConvert.SerializeObject(status, new AlgoStatusConverter(false)));
			parameters.AddOptionalParameter("algo_ids", algo_ids);
			parameters.AddOptionalParameter("before", before);
			parameters.AddOptionalParameter("after", after);

			return await SendRequest<Dictionary<string, IEnumerable<OkexAlgoOrder>>>(GetUrl(Endpoints_Algo_OrderList), HttpMethod.Get, ct, parameters, signed: true).ConfigureAwait(false);
		}

		#endregion

		#endregion
	}
}
