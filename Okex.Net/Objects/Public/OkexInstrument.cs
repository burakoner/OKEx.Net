namespace Okex.Net.Objects.Public;

public class OkexInstrument
{
    /// <summary>
    /// Instrument type
    /// </summary>
    [JsonProperty("instType"), JsonConverter(typeof(InstrumentTypeConverter))]
    public OkexInstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Instrument ID, e.g. BTC-USD-SWAP
    /// </summary>
    [JsonProperty("instId")]
    public string Instrument { get; set; }

    /// <summary>
    /// Underlying, e.g. BTC-USD. Only applicable to FUTURES/SWAP/OPTION
    /// </summary>
    [JsonProperty("uly")]
    public string Underlying { get; set; }

    [JsonProperty("category")]
    public string Category { get; set; }

    [JsonProperty("baseCcy")]
    public string BaseCurrency { get; set; }

    [JsonProperty("quoteCcy")]
    public string QuoteCurrency { get; set; }

    [JsonProperty("settleCcy")]
    public string SettlementCurrency { get; set; }

    [JsonProperty("ctVal")]
    public decimal? ContractValue { get; set; }

    [JsonProperty("ctMult")]
    public decimal? ContractMultiplier { get; set; }

    [JsonProperty("ctValCcy")]
    public string ContractValueCurrency { get; set; }

    [JsonProperty("optType"), JsonConverter(typeof(OptionTypeConverter))]
    public OkexOptionType? OptionType { get; set; }

    [JsonProperty("stk")]
    public decimal? StrikePrice { get; set; }

    [JsonProperty("listTime"), JsonConverter(typeof(OkexTimestampConverter))]
    public DateTime? ListingTime { get; set; }

    [JsonProperty("expTime"), JsonConverter(typeof(OkexTimestampConverter))]
    public DateTime? ExpiryTime { get; set; }

    [JsonProperty("lever")]
    public int? MaximumLeverage { get; set; }

    [JsonProperty("tickSz")]
    public decimal TickSize { get; set; }

    [JsonProperty("lotSz")]
    public decimal LotSize { get; set; }

    [JsonProperty("minSz")]
    public decimal MinimumOrderSize { get; set; }

    [JsonProperty("ctType"), JsonConverter(typeof(ContractTypeConverter))]
    public OkexContractType? ContractType { get; set; }

    [JsonProperty("alias"), JsonConverter(typeof(InstrumentAliasConverter))]
    public OkexInstrumentAlias? Alias { get; set; }

    [JsonProperty("state"), JsonConverter(typeof(InstrumentStateConverter))]
    public OkexInstrumentState state { get; set; }
}
