namespace Okex.Net.Converters;

internal class AccountBillTypeConverter : BaseConverter<OkexAccountBillType>
{
    public AccountBillTypeConverter() : this(true) { }
    public AccountBillTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkexAccountBillType, string>> Mapping => new()
    {
        new KeyValuePair<OkexAccountBillType, string>(OkexAccountBillType.Transfer, "1"),
        new KeyValuePair<OkexAccountBillType, string>(OkexAccountBillType.Trade, "2"),
        new KeyValuePair<OkexAccountBillType, string>(OkexAccountBillType.Delivery, "3"),
        new KeyValuePair<OkexAccountBillType, string>(OkexAccountBillType.AutoTokenConversion, "4"),
        new KeyValuePair<OkexAccountBillType, string>(OkexAccountBillType.Liquidation, "5"),
        new KeyValuePair<OkexAccountBillType, string>(OkexAccountBillType.MarginTransfer, "6"),
        new KeyValuePair<OkexAccountBillType, string>(OkexAccountBillType.InterestDeduction, "7"),
        new KeyValuePair<OkexAccountBillType, string>(OkexAccountBillType.FundingFee, "8"),
        new KeyValuePair<OkexAccountBillType, string>(OkexAccountBillType.ADL, "9"),
        new KeyValuePair<OkexAccountBillType, string>(OkexAccountBillType.Clawback, "10"),
        new KeyValuePair<OkexAccountBillType, string>(OkexAccountBillType.SystemTokenConversion, "11"),
        new KeyValuePair<OkexAccountBillType, string>(OkexAccountBillType.StrategyTransfer, "12"),
    };
}