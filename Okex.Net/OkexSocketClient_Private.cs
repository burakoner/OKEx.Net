using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Okex.Net.CoreObjects;
using Okex.Net.Enums;
using Okex.Net.RestObjects.Account;
using Okex.Net.RestObjects.Trade;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Okex.Net
{
    public partial class OkexSocketClient
    {
        #region Private Signed Feeds
        /// <summary>
        /// Retrieve account information. Data will be pushed when triggered by events such as placing/canceling order, and will also be pushed in regular interval according to subscription granularity.
        /// </summary>
        /// <param name="onData">On Data Handler</param>
        /// <returns></returns>
        public virtual CallResult<UpdateSubscription> SubscribeToAccountUpdates(Action<OkexAccountBalance> onData) => SubscribeToAccountUpdates_Async(onData).Result;
        /// <summary>
        /// Retrieve account information. Data will be pushed when triggered by events such as placing/canceling order, and will also be pushed in regular interval according to subscription granularity.
        /// </summary>
        /// <param name="onData">On Data Handler</param>
        /// <returns></returns>
        public virtual async Task<CallResult<UpdateSubscription>> SubscribeToAccountUpdates_Async(Action<OkexAccountBalance> onData)
        {
            var internalHandler = new Action<DataEvent<OkexSocketUpdateResponse<IEnumerable<OkexAccountBalance>>>>(data =>
            {
                foreach (var d in data.Data.Data)
                    onData(d);
            });

            var request = new OkexSocketRequest(OkexSocketOperation.Subscribe, "account");
            return await SubscribeAsync(request, null, true, internalHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve position information. Initial snapshot will be pushed according to subscription granularity. Data will be pushed when triggered by events such as placing/canceling order, and will also be pushed in regular interval according to subscription granularity.
        /// </summary>
        /// <param name="instrumentType">Instrument Type</param>
        /// <param name="instrumentId">Instrument ID</param>
        /// <param name="underlying">Underlying</param>
        /// <param name="onData">On Data Handler</param>
        /// <returns></returns>
        public virtual CallResult<UpdateSubscription> SubscribeToPositionUpdates(
            OkexInstrumentType instrumentType,
            string instrumentId,
            string underlying,
            Action<OkexPosition> onData) => SubscribeToPositionUpdates_Async(instrumentType, instrumentId, underlying, onData).Result;
        /// <summary>
        /// Retrieve position information. Initial snapshot will be pushed according to subscription granularity. Data will be pushed when triggered by events such as placing/canceling order, and will also be pushed in regular interval according to subscription granularity.
        /// </summary>
        /// <param name="instrumentType">Instrument Type</param>
        /// <param name="instrumentId">Instrument ID</param>
        /// <param name="underlying">Underlying</param>
        /// <param name="onData">On Data Handler</param>
        /// <returns></returns>
        public virtual async Task<CallResult<UpdateSubscription>> SubscribeToPositionUpdates_Async(
            OkexInstrumentType instrumentType,
            string instrumentId,
            string underlying,
            Action<OkexPosition> onData)
        {
            var internalHandler = new Action<DataEvent<OkexSocketUpdateResponse<IEnumerable<OkexPosition>>>>(data =>
            {
                foreach (var d in data.Data.Data)
                    onData(d);
            });

            var request = new OkexSocketRequest(OkexSocketOperation.Subscribe, new OkexSocketRequestArgument
            {
                Channel = "positions",
                InstrumentId = instrumentId,
                InstrumentType = instrumentType,
                Underlying = underlying,
            });
            return await SubscribeAsync(request, null, true, internalHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve account balance and position information. Data will be pushed when triggered by events such as filled order, funding transfer.
        /// </summary>
        /// <param name="onData">On Data Handler</param>
        /// <returns></returns>
        public virtual CallResult<UpdateSubscription> SubscribeToBalanceAndPositionUpdates(Action<OkexPositionRisk> onData) => SubscribeToBalanceAndPositionUpdates_Async(onData).Result;
        /// <summary>
        /// Retrieve account balance and position information. Data will be pushed when triggered by events such as filled order, funding transfer.
        /// </summary>
        /// <param name="onData">On Data Handler</param>
        /// <returns></returns>
        public virtual async Task<CallResult<UpdateSubscription>> SubscribeToBalanceAndPositionUpdates_Async(Action<OkexPositionRisk> onData)
        {
            var internalHandler = new Action<DataEvent<OkexSocketUpdateResponse<IEnumerable<OkexPositionRisk>>>>(data =>
            {
                foreach (var d in data.Data.Data)
                    onData(d);
            });

            var request = new OkexSocketRequest(OkexSocketOperation.Subscribe, "balance_and_position");
            return await SubscribeAsync(request, null, true, internalHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve order information. Data will not be pushed when first subscribed. Data will only be pushed when triggered by events such as placing/canceling order.
        /// </summary>
        /// <param name="instrumentType">Instrument Type</param>
        /// <param name="instrumentId">Instrument ID</param>
        /// <param name="underlying">Underlying</param>
        /// <param name="onData">On Data Handler</param>
        /// <returns></returns>
        public virtual CallResult<UpdateSubscription> SubscribeToOrderUpdates(
            OkexInstrumentType instrumentType,
            string instrumentId,
            string underlying,
            Action<OkexOrder> onData) => SubscribeToOrderUpdates_Async(instrumentType, instrumentId, underlying, onData).Result;
        /// <summary>
        /// Retrieve order information. Data will not be pushed when first subscribed. Data will only be pushed when triggered by events such as placing/canceling order.
        /// </summary>
        /// <param name="instrumentType">Instrument Type</param>
        /// <param name="instrumentId">Instrument ID</param>
        /// <param name="underlying">Underlying</param>
        /// <param name="onData">On Data Handler</param>
        /// <returns></returns>
        public virtual async Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdates_Async(
            OkexInstrumentType instrumentType,
            string instrumentId,
            string underlying,
            Action<OkexOrder> onData)
        {
            var internalHandler = new Action<DataEvent<OkexSocketUpdateResponse<IEnumerable<OkexOrder>>>>(data =>
            {
                foreach (var d in data.Data.Data)
                    onData(d);
            });


            var request = new OkexSocketRequest(OkexSocketOperation.Subscribe, new OkexSocketRequestArgument
            {
                Channel = "orders",
                InstrumentId = instrumentId,
                InstrumentType = instrumentType,
                Underlying = underlying,
            });
            return await SubscribeAsync(request, null, true, internalHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve algo orders (includes trigger order, oco order, conditional order). Data will not be pushed when first subscribed. Data will only be pushed when triggered by events such as placing/canceling order.
        /// </summary>
        /// <param name="instrumentType">Instrument Type</param>
        /// <param name="instrumentId">Instrument ID</param>
        /// <param name="underlying">Underlying</param>
        /// <param name="onData">On Data Handler</param>
        /// <returns></returns>
        public virtual CallResult<UpdateSubscription> SubscribeToAlgoOrderUpdates(
            OkexInstrumentType instrumentType,
            string instrumentId,
            string underlying,
            Action<OkexAlgoOrder> onData) => SubscribeToAlgoOrderUpdates_Async(instrumentType, instrumentId, underlying, onData).Result;
        /// <summary>
        /// Retrieve algo orders (includes trigger order, oco order, conditional order). Data will not be pushed when first subscribed. Data will only be pushed when triggered by events such as placing/canceling order.
        /// </summary>
        /// <param name="instrumentType">Instrument Type</param>
        /// <param name="instrumentId">Instrument ID</param>
        /// <param name="underlying">Underlying</param>
        /// <param name="onData">On Data Handler</param>
        /// <returns></returns>
        public virtual async Task<CallResult<UpdateSubscription>> SubscribeToAlgoOrderUpdates_Async(
            OkexInstrumentType instrumentType,
            string instrumentId,
            string underlying,
            Action<OkexAlgoOrder> onData)
        {
            var internalHandler = new Action<DataEvent<OkexSocketUpdateResponse<IEnumerable<OkexAlgoOrder>>>>(data =>
            {
                foreach (var d in data.Data.Data)
                    onData(d);
            });


            var request = new OkexSocketRequest(OkexSocketOperation.Subscribe, new OkexSocketRequestArgument
            {
                Channel = "orders-algo",
                InstrumentId = instrumentId,
                InstrumentType = instrumentType,
                Underlying = underlying,
            });
            return await SubscribeAsync(request, null, true, internalHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve advance algo orders (includes iceberg order and twap order). Data will be pushed when first subscribed. Data will be pushed when triggered by events such as placing/canceling order.
        /// </summary>
        /// <param name="instrumentType">Instrument Type</param>
        /// <param name="instrumentId">Instrument ID</param>
        /// <param name="underlying">Underlying</param>
        /// <param name="onData">On Data Handler</param>
        /// <returns></returns>
        public virtual CallResult<UpdateSubscription> SubscribeToAdvanceAlgoOrderUpdates(
            OkexInstrumentType instrumentType,
            string instrumentId,
            string underlying,
            Action<OkexAlgoOrder> onData) => SubscribeToAdvanceAlgoOrderUpdates_Async(instrumentType, instrumentId, underlying, onData).Result;
        /// <summary>
        /// Retrieve advance algo orders (includes iceberg order and twap order). Data will be pushed when first subscribed. Data will be pushed when triggered by events such as placing/canceling order.
        /// </summary>
        /// <param name="instrumentType">Instrument Type</param>
        /// <param name="instrumentId">Instrument ID</param>
        /// <param name="underlying">Underlying</param>
        /// <param name="onData">On Data Handler</param>
        /// <returns></returns>
        public virtual async Task<CallResult<UpdateSubscription>> SubscribeToAdvanceAlgoOrderUpdates_Async(
            OkexInstrumentType instrumentType,
            string instrumentId,
            string underlying,
            Action<OkexAlgoOrder> onData)
        {
            var internalHandler = new Action<DataEvent<OkexSocketUpdateResponse<IEnumerable<OkexAlgoOrder>>>>(data =>
            {
                foreach (var d in data.Data.Data)
                    onData(d);
            });


            var request = new OkexSocketRequest(OkexSocketOperation.Subscribe, new OkexSocketRequestArgument
            {
                Channel = "algo-advance",
                InstrumentId = instrumentId,
                InstrumentType = instrumentType,
                Underlying = underlying,
            });
            return await SubscribeAsync(request, null, true, internalHandler).ConfigureAwait(false);
        }

        #endregion
    }
}