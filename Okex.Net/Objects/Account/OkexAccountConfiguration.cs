namespace Okex.Net.Objects.Account;

public class OkexAccountConfiguration
{
    [JsonProperty("uid")]
    public long UserId { get; set; }

    [JsonProperty("acctLv"), JsonConverter(typeof(AccountLevelConverter))]
    public OkexAccountLevel AccountLevel { get; set; }

    [JsonProperty("posMode"), JsonConverter(typeof(PositionModeConverter))]
    public OkexPositionMode PositionMode { get; set; }

    [JsonProperty("autoLoan"), JsonConverter(typeof(OkexBooleanConverter))]
    public bool AutoLoan { get; set; }

    [JsonProperty("greeksType"), JsonConverter(typeof(GreeksTypeConverter))]
    public OkexGreeksType GreeksType { get; set; }

    [JsonProperty("level")]
    public string Level { get; set; }

    [JsonProperty("levelTmp")]
    public string LevelTemporary { get; set; }

    [JsonProperty("ctIsoMode"), JsonConverter(typeof(MarginTransferModeConverter))]
    public OkexMarginTransferMode ContractIsolatedMarginTradingMode { get; set; }

    [JsonProperty("mgnIsoMode"), JsonConverter(typeof(MarginTransferModeConverter))]
    public OkexMarginTransferMode MarginIsolatedMarginTradingMode { get; set; }

    [JsonProperty("liquidationGear")]
    public string liquidationGear { get; set; }

    [JsonProperty("spotOffsetType")]
    public string spotOffsetType { get; set; }
}
