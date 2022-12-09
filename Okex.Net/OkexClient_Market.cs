namespace Okex.Net;

public partial class OkexClient
{
    #region Market API Endpoints
    /// <summary>
    /// Retrieve the latest price snapshot, best bid/ask price, and trading volume in the last 24 hours.
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="underlying">Underlying</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual WebCallResult<IEnumerable<OkexTicker>> GetTickers(OkexInstrumentType instrumentType, string underlying = null, CancellationToken ct = default)
        => GetTickersAsync(instrumentType, underlying, ct).Result;
    /// <summary>
    /// Retrieve the latest price snapshot, best bid/ask price, and trading volume in the last 24 hours.
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="underlying">Underlying</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<WebCallResult<IEnumerable<OkexTicker>>> GetTickersAsync(OkexInstrumentType instrumentType, string underlying = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)) },
        };
        parameters.AddOptionalParameter("uly", underlying);

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexTicker>>>(UnifiedApi.GetUri(Endpoints_V5_Market_Tickers), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OkexTicker>>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OkexTicker>>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data);
    }

    /// <summary>
    /// Retrieve the latest price snapshot, best bid/ask price, and trading volume in the last 24 hours.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual WebCallResult<OkexTicker> GetTicker(string instrumentId, CancellationToken ct = default)
        => GetTickerAsync(instrumentId, ct).Result;
    /// <summary>
    /// Retrieve the latest price snapshot, best bid/ask price, and trading volume in the last 24 hours.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<WebCallResult<OkexTicker>> GetTickerAsync(string instrumentId, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "instId", instrumentId },
        };

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexTicker>>>(UnifiedApi.GetUri(Endpoints_V5_Market_Ticker), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<OkexTicker>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<OkexTicker>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data.FirstOrDefault());
    }

    /// <summary>
    /// Retrieve index tickers.
    /// </summary>
    /// <param name="quoteCurrency">Quote currency. Currently there is only an index with USD/USDT/BTC as the quote currency.</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual WebCallResult<IEnumerable<OkexIndexTicker>> GetIndexTickers(string quoteCurrency = null, string instrumentId = null, CancellationToken ct = default)
        => GetIndexTickersAsync(quoteCurrency, instrumentId, ct).Result;
    /// <summary>
    /// Retrieve index tickers.
    /// </summary>
    /// <param name="quoteCurrency">Quote currency. Currently there is only an index with USD/USDT/BTC as the quote currency.</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<WebCallResult<IEnumerable<OkexIndexTicker>>> GetIndexTickersAsync(string quoteCurrency = null, string instrumentId = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("quoteCcy", quoteCurrency);
        parameters.AddOptionalParameter("instId", instrumentId);

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexIndexTicker>>>(UnifiedApi.GetUri(Endpoints_V5_Market_IndexTickers), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OkexIndexTicker>>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OkexIndexTicker>>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data);
    }

    /// <summary>
    /// Retrieve a instrument is order book.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="depth">Order book depth per side. Maximum 400, e.g. 400 bids + 400 asks. Default returns to 1 depth data</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual WebCallResult<OkexOrderBook> GetOrderBook(string instrumentId, int depth = 1, CancellationToken ct = default)
        => GetOrderBookAsync(instrumentId, depth, ct).Result;
    /// <summary>
    /// Retrieve a instrument is order book.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="depth">Order book depth per side. Maximum 400, e.g. 400 bids + 400 asks. Default returns to 1 depth data</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<WebCallResult<OkexOrderBook>> GetOrderBookAsync(string instrumentId, int depth = 1, CancellationToken ct = default)
    {
        if (depth < 1 || depth > 400)
            throw new ArgumentException("Depth can be between 1-400.");

        var parameters = new Dictionary<string, object>
        {
            {"instId", instrumentId},
            {"sz", depth},
        };

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexOrderBook>>>(UnifiedApi.GetUri(Endpoints_V5_Market_Books), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        if (!result.Success || result.Data.Data.Count() == 0) return result.AsError<OkexOrderBook>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<OkexOrderBook>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        var orderbook = result.Data.Data.FirstOrDefault();
        orderbook.Instrument = instrumentId;
        return result.As(orderbook);
    }

    /// <summary>
    /// Retrieve the candlestick charts. This endpoint can retrieve the latest 1,440 data entries. Charts are returned in groups based on the requested bar.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="period">Bar size, the default is 1m</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 300; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual WebCallResult<IEnumerable<OkexCandlestick>> GetCandlesticks(string instrumentId, OkexPeriod period, long? after = null, long? before = null, int limit = 100, CancellationToken ct = default)
        => GetCandlesticksAsync(instrumentId, period, after, before, limit, ct).Result;
    /// <summary>
    /// Retrieve the candlestick charts. This endpoint can retrieve the latest 1,440 data entries. Charts are returned in groups based on the requested bar.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="period">Bar size, the default is 1m</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 300; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<WebCallResult<IEnumerable<OkexCandlestick>>> GetCandlesticksAsync(string instrumentId, OkexPeriod period, long? after = null, long? before = null, int limit = 100, CancellationToken ct = default)
    {
        if (limit < 1 || limit > 300)
            throw new ArgumentException("Limit can be between 1-300.");

        var parameters = new Dictionary<string, object>
        {
            { "instId", instrumentId },
            { "bar", JsonConvert.SerializeObject(period, new PeriodConverter(false)) },
        };
        parameters.AddOptionalParameter("after", after?.ToString());
        parameters.AddOptionalParameter("before", before?.ToString());
        parameters.AddOptionalParameter("limit", limit.ToString());

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexCandlestick>>>(UnifiedApi.GetUri(Endpoints_V5_Market_Candles), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OkexCandlestick>>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OkexCandlestick>>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        foreach (var candle in result.Data.Data) candle.Instrument = instrumentId;
        return result.As(result.Data.Data);
    }

    /// <summary>
    /// Retrieve history candlestick charts from recent years.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="period">Bar size, the default is 1m</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual WebCallResult<IEnumerable<OkexCandlestick>> GetCandlesticksHistory(string instrumentId, OkexPeriod period, long? after = null, long? before = null, int limit = 100, CancellationToken ct = default)
        => GetCandlesticksHistoryAsync(instrumentId, period, after, before, limit, ct).Result;
    /// <summary>
    /// Retrieve history candlestick charts from recent years.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="period">Bar size, the default is 1m</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<WebCallResult<IEnumerable<OkexCandlestick>>> GetCandlesticksHistoryAsync(string instrumentId, OkexPeriod period, long? after = null, long? before = null, int limit = 100, CancellationToken ct = default)
    {
        if (limit < 1 || limit > 100)
            throw new ArgumentException("Limit can be between 1-100.");

        var parameters = new Dictionary<string, object>
        {
            { "instId", instrumentId },
            { "bar", JsonConvert.SerializeObject(period, new PeriodConverter(false)) },
        };
        parameters.AddOptionalParameter("after", after?.ToString());
        parameters.AddOptionalParameter("before", before?.ToString());
        parameters.AddOptionalParameter("limit", limit.ToString());

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexCandlestick>>>(UnifiedApi.GetUri(Endpoints_V5_Market_HistoryCandles), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OkexCandlestick>>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OkexCandlestick>>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        foreach (var candle in result.Data.Data) candle.Instrument = instrumentId;
        return result.As(result.Data.Data);
    }

    /// <summary>
    /// Retrieve the candlestick charts of the index. This endpoint can retrieve the latest 1,440 data entries. Charts are returned in groups based on the requested bar.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="period">Bar size, the default is 1m</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual WebCallResult<IEnumerable<OkexCandlestick>> GetIndexCandlesticks(string instrumentId, OkexPeriod period, long? after = null, long? before = null, int limit = 100, CancellationToken ct = default)
        => GetIndexCandlesticksAsync(instrumentId, period, after, before, limit, ct).Result;
    /// <summary>
    /// Retrieve the candlestick charts of the index. This endpoint can retrieve the latest 1,440 data entries. Charts are returned in groups based on the requested bar.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="period">Bar size, the default is 1m</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<WebCallResult<IEnumerable<OkexCandlestick>>> GetIndexCandlesticksAsync(string instrumentId, OkexPeriod period, long? after = null, long? before = null, int limit = 100, CancellationToken ct = default)
    {
        if (limit < 1 || limit > 100)
            throw new ArgumentException("Limit can be between 1-100.");

        var parameters = new Dictionary<string, object>
        {
            { "instId", instrumentId },
            { "bar", JsonConvert.SerializeObject(period, new PeriodConverter(false)) },
        };
        parameters.AddOptionalParameter("after", after?.ToString());
        parameters.AddOptionalParameter("before", before?.ToString());
        parameters.AddOptionalParameter("limit", limit.ToString());

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexCandlestick>>>(UnifiedApi.GetUri(Endpoints_V5_Market_IndexCandles), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OkexCandlestick>>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OkexCandlestick>>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        foreach (var candle in result.Data.Data) candle.Instrument = instrumentId;
        return result.As(result.Data.Data);
    }

    /// <summary>
    /// Retrieve the candlestick charts of mark price. This endpoint can retrieve the latest 1,440 data entries. Charts are returned in groups based on the requested bar.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="period">Bar size, the default is 1m</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual WebCallResult<IEnumerable<OkexCandlestick>> GetMarkPriceCandlesticks(string instrumentId, OkexPeriod period, long? after = null, long? before = null, int limit = 100, CancellationToken ct = default)
        => GetMarkPriceCandlesticksAsync(instrumentId, period, after, before, limit, ct).Result;
    /// <summary>
    /// Retrieve the candlestick charts of mark price. This endpoint can retrieve the latest 1,440 data entries. Charts are returned in groups based on the requested bar.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="period">Bar size, the default is 1m</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<WebCallResult<IEnumerable<OkexCandlestick>>> GetMarkPriceCandlesticksAsync(string instrumentId, OkexPeriod period, long? after = null, long? before = null, int limit = 100, CancellationToken ct = default)
    {
        if (limit < 1 || limit > 100)
            throw new ArgumentException("Limit can be between 1-100.");

        var parameters = new Dictionary<string, object>
        {
            { "instId", instrumentId },
            { "bar", JsonConvert.SerializeObject(period, new PeriodConverter(false)) },
        };
        parameters.AddOptionalParameter("after", after?.ToString());
        parameters.AddOptionalParameter("before", before?.ToString());
        parameters.AddOptionalParameter("limit", limit.ToString());

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexCandlestick>>>(UnifiedApi.GetUri(Endpoints_V5_Market_MarkPriceCandles), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OkexCandlestick>>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OkexCandlestick>>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        foreach (var candle in result.Data.Data) candle.Instrument = instrumentId;
        return result.As(result.Data.Data);
    }

    /// <summary>
    /// Retrieve the recent transactions of an instrument.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual WebCallResult<IEnumerable<OkexTrade>> GetTrades(string instrumentId, int limit = 100, CancellationToken ct = default)
        => GetTradesAsync(instrumentId, limit, ct).Result;
    /// <summary>
    /// Retrieve the recent transactions of an instrument.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<WebCallResult<IEnumerable<OkexTrade>>> GetTradesAsync(string instrumentId, int limit = 100, CancellationToken ct = default)
    {
        if (limit < 1 || limit > 500)
            throw new ArgumentException("Limit can be between 1-500.");

        var parameters = new Dictionary<string, object>
        {
            { "instId", instrumentId },
        };
        parameters.AddOptionalParameter("limit", limit.ToString());

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexTrade>>>(UnifiedApi.GetUri(Endpoints_V5_Market_Trades), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OkexTrade>>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OkexTrade>>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data);
    }

    /// <summary>
    /// Get trades history
    /// Retrieve the recent transactions of an instrument from the last 3 months with pagination.
    /// Rate Limit: 10 requests per 2 seconds
    /// </summary>
    /// <param name="instrumentId">Instrument ID, e.g. BTC-USDT</param>
    /// <param name="type">Pagination Type</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual WebCallResult<IEnumerable<OkexTrade>> GetTradesHistory(string instrumentId, OkexTradeHistoryPaginationType type = OkexTradeHistoryPaginationType.TradeId, long? after = null, long? before = null, int limit = 100, CancellationToken ct = default)
        => GetTradesHistoryAsync(instrumentId, type, after, before, limit, ct).Result;
    /// <summary>
    /// Get trades history
    /// Retrieve the recent transactions of an instrument from the last 3 months with pagination.
    /// Rate Limit: 10 requests per 2 seconds
    /// </summary>
    /// <param name="instrumentId">Instrument ID, e.g. BTC-USDT</param>
    /// <param name="type">Pagination Type</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<WebCallResult<IEnumerable<OkexTrade>>> GetTradesHistoryAsync(string instrumentId, OkexTradeHistoryPaginationType type = OkexTradeHistoryPaginationType.TradeId, long? after = null, long? before = null, int limit = 100, CancellationToken ct = default)
    {
        if (limit < 1 || limit > 100)
            throw new ArgumentException("Limit can be between 1-100.");

        var parameters = new Dictionary<string, object>
        {
            { "instId", instrumentId },
        };

        parameters.AddOptionalParameter("type", JsonConvert.SerializeObject(type, new TradeHistoryPaginationTypeConverter(false)));
        parameters.AddOptionalParameter("after", after?.ToString());
        parameters.AddOptionalParameter("before", before?.ToString());
        parameters.AddOptionalParameter("limit", limit.ToString());

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexTrade>>>(UnifiedApi.GetUri(Endpoints_V5_Market_TradesHistory), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OkexTrade>>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OkexTrade>>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data);
    }

    /// <summary>
    /// The 24-hour trading volume is calculated on a rolling basis, using USD as the pricing unit.
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual WebCallResult<Okex24HourVolume> Get24HourVolume(CancellationToken ct = default)
        => Get24HourVolumeAsync(ct).Result;
    /// <summary>
    /// The 24-hour trading volume is calculated on a rolling basis, using USD as the pricing unit.
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<WebCallResult<Okex24HourVolume>> Get24HourVolumeAsync(CancellationToken ct = default)
    {
        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<Okex24HourVolume>>>(UnifiedApi.GetUri(Endpoints_V5_Market_Platform24Volume), HttpMethod.Get, ct).ConfigureAwait(false);
        if (!result.Success) return result.AsError<Okex24HourVolume>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<Okex24HourVolume>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data.FirstOrDefault());
    }

    /// <summary>
    /// Get the crypto price of signing using Open Oracle smart contract.
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual WebCallResult<OkexOracle> GetOracle(CancellationToken ct = default)
        => GetOracleAsync(ct).Result;
    /// <summary>
    /// Get the crypto price of signing using Open Oracle smart contract.
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<WebCallResult<OkexOracle>> GetOracleAsync(CancellationToken ct = default)
    {
        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexOracle>>>(UnifiedApi.GetUri(Endpoints_V5_Market_OpenOracle), HttpMethod.Get, ct).ConfigureAwait(false);
        if (!result.Success) return result.AsError<OkexOracle>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<OkexOracle>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data.FirstOrDefault());
    }

    /// <summary>
    /// Get the index component information data on the market
    /// </summary>
    /// <param name="index"></param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual WebCallResult<OkexIndexComponents> GetIndexComponents(string index, CancellationToken ct = default)
        => GetIndexComponentsAsync(index, ct).Result;
    /// <summary>
    /// Get the index component information data on the market
    /// </summary>
    /// <param name="index"></param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<WebCallResult<OkexIndexComponents>> GetIndexComponentsAsync(string index, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "index", index },
        };

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<OkexIndexComponents>>(UnifiedApi.GetUri(Endpoints_V5_Market_IndexComponents), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<OkexIndexComponents>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<OkexIndexComponents>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data);
    }

    /// <summary>
    /// Get block tickers
    /// Retrieve the latest block trading volume in the last 24 hours.
    /// Rate Limit: 20 requests per 2 seconds
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="underlying">Underlying</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual WebCallResult<IEnumerable<OkexBlockTicker>> GetBlockTickers(OkexInstrumentType instrumentType, string underlying = null, CancellationToken ct = default)
        => GetBlockTickersAsync(instrumentType, underlying, ct).Result;
    /// <summary>
    /// Get block tickers
    /// Retrieve the latest block trading volume in the last 24 hours.
    /// Rate Limit: 20 requests per 2 seconds
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="underlying">Underlying</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<WebCallResult<IEnumerable<OkexBlockTicker>>> GetBlockTickersAsync(OkexInstrumentType instrumentType, string underlying = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)) },
        };
        parameters.AddOptionalParameter("uly", underlying);

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexBlockTicker>>>(UnifiedApi.GetUri(Endpoints_V5_Market_BlockTickers), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OkexBlockTicker>>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OkexBlockTicker>>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data);
    }

    /// <summary>
    /// Get block ticker
    /// Retrieve the latest block trading volume in the last 24 hours.
    /// Rate Limit: 20 requests per 2 seconds
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual WebCallResult<OkexBlockTicker> GetBlockTicker(string instrumentId, CancellationToken ct = default)
        => GetBlockTickerAsync(instrumentId, ct).Result;
    /// <summary>
    /// Get block ticker
    /// Retrieve the latest block trading volume in the last 24 hours.
    /// Rate Limit: 20 requests per 2 seconds
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<WebCallResult<OkexBlockTicker>> GetBlockTickerAsync(string instrumentId, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "instId", instrumentId },
        };

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexBlockTicker>>>(UnifiedApi.GetUri(Endpoints_V5_Market_BlockTicker), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<OkexBlockTicker>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<OkexBlockTicker>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data.FirstOrDefault());
    }

    /// <summary>
    /// Get block trades
    /// Retrieve the recent block trading transactions of an instrument. Descending order by tradeId.
    /// Rate Limit: 20 requests per 2 seconds
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual WebCallResult<IEnumerable<OkexTrade>> GetBlockTrades(string instrumentId, CancellationToken ct = default)
        => GetBlockTradesAsync(instrumentId, ct).Result;
    /// <summary>
    /// Get block trades
    /// Retrieve the recent block trading transactions of an instrument. Descending order by tradeId.
    /// Rate Limit: 20 requests per 2 seconds
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<WebCallResult<IEnumerable<OkexTrade>>> GetBlockTradesAsync(string instrumentId, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "instId", instrumentId },
        };

        var result = await UnifiedApi.ExecuteAsync<OkexRestApiResponse<IEnumerable<OkexTrade>>>(UnifiedApi.GetUri(Endpoints_V5_Market_BlockTrades), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<OkexTrade>>(new OkexRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data.ErrorCode > 0) return result.AsError<IEnumerable<OkexTrade>>(new OkexRestApiError(result.Data.ErrorCode, result.Data.ErrorMessage, null));

        return result.As(result.Data.Data);
    }
    #endregion
}