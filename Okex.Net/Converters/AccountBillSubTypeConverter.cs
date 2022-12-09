namespace Okex.Net.Converters;

internal class AccountBillSubTypeConverter : BaseConverter<OkexAccountBillSubType>
{
    public AccountBillSubTypeConverter() : this(true) { }
    public AccountBillSubTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkexAccountBillSubType, string>> Mapping => new()
    {
            new KeyValuePair<OkexAccountBillSubType, string>(OkexAccountBillSubType.Buy, "1"),
            new KeyValuePair<OkexAccountBillSubType, string>(OkexAccountBillSubType.Sell, "2"),
            new KeyValuePair<OkexAccountBillSubType, string>(OkexAccountBillSubType.OpenLong, "3"),
            new KeyValuePair<OkexAccountBillSubType, string>(OkexAccountBillSubType.OpenShort, "4"),
            new KeyValuePair<OkexAccountBillSubType, string>(OkexAccountBillSubType.CloseLong, "5"),
            new KeyValuePair<OkexAccountBillSubType, string>(OkexAccountBillSubType.CloseShort, "6"),
            new KeyValuePair<OkexAccountBillSubType, string>(OkexAccountBillSubType.InterestDeduction, "9"),
            new KeyValuePair<OkexAccountBillSubType, string>(OkexAccountBillSubType.TransferIn, "11"),
            new KeyValuePair<OkexAccountBillSubType, string>(OkexAccountBillSubType.TransferOut, "12"),
            new KeyValuePair<OkexAccountBillSubType, string>(OkexAccountBillSubType.ManualMarginIncrease, "160"),
            new KeyValuePair<OkexAccountBillSubType, string>(OkexAccountBillSubType.ManualMarginDecrease, "161"),
            new KeyValuePair<OkexAccountBillSubType, string>(OkexAccountBillSubType.AutoMarginIncrease, "162"),
            new KeyValuePair<OkexAccountBillSubType, string>(OkexAccountBillSubType.AutoBuy, "110"),
            new KeyValuePair<OkexAccountBillSubType, string>(OkexAccountBillSubType.AutoSell, "111"),
            new KeyValuePair<OkexAccountBillSubType, string>(OkexAccountBillSubType.SystemTokenConversionTransferIn, "118"),
            new KeyValuePair<OkexAccountBillSubType, string>(OkexAccountBillSubType.SystemTokenConversionTransferOut, "119"),
            new KeyValuePair<OkexAccountBillSubType, string>(OkexAccountBillSubType.PartialLiquidationCloseLong, "100"),
            new KeyValuePair<OkexAccountBillSubType, string>(OkexAccountBillSubType.PartialLiquidationCloseShort, "101"),
            new KeyValuePair<OkexAccountBillSubType, string>(OkexAccountBillSubType.PartialLiquidationBuy, "102"),
            new KeyValuePair<OkexAccountBillSubType, string>(OkexAccountBillSubType.PartialLiquidationSell, "103"),
            new KeyValuePair<OkexAccountBillSubType, string>(OkexAccountBillSubType.LiquidationLong, "104"),
            new KeyValuePair<OkexAccountBillSubType, string>(OkexAccountBillSubType.LiquidationShort, "105"),
            new KeyValuePair<OkexAccountBillSubType, string>(OkexAccountBillSubType.LiquidationBuy, "106"),
            new KeyValuePair<OkexAccountBillSubType, string>(OkexAccountBillSubType.LiquidationSell, "107"),
            new KeyValuePair<OkexAccountBillSubType, string>(OkexAccountBillSubType.LiquidationTransferIn, "110"),
            new KeyValuePair<OkexAccountBillSubType, string>(OkexAccountBillSubType.LiquidationTransferOut, "111"),
            new KeyValuePair<OkexAccountBillSubType, string>(OkexAccountBillSubType.ADLCloseLong, "125"),
            new KeyValuePair<OkexAccountBillSubType, string>(OkexAccountBillSubType.ADLCloseShort, "126"),
            new KeyValuePair<OkexAccountBillSubType, string>(OkexAccountBillSubType.ADLBuy, "127"),
            new KeyValuePair<OkexAccountBillSubType, string>(OkexAccountBillSubType.ADLSell, "128"),
            new KeyValuePair<OkexAccountBillSubType, string>(OkexAccountBillSubType.Exercised, "170"),
            new KeyValuePair<OkexAccountBillSubType, string>(OkexAccountBillSubType.CounterpartyExercised, "171"),
            new KeyValuePair<OkexAccountBillSubType, string>(OkexAccountBillSubType.ExpiredOTM, "172"),
            new KeyValuePair<OkexAccountBillSubType, string>(OkexAccountBillSubType.DeliveryLong, "112"),
            new KeyValuePair<OkexAccountBillSubType, string>(OkexAccountBillSubType.DeliveryShort, "113"),
            new KeyValuePair<OkexAccountBillSubType, string>(OkexAccountBillSubType.DeliveryExerciseClawback, "117"),
            new KeyValuePair<OkexAccountBillSubType, string>(OkexAccountBillSubType.FundingFeeExpense, "173"),
            new KeyValuePair<OkexAccountBillSubType, string>(OkexAccountBillSubType.FundingFeeIncome, "174"),
            new KeyValuePair<OkexAccountBillSubType, string>(OkexAccountBillSubType.SystemTransferIn, "200"),
            new KeyValuePair<OkexAccountBillSubType, string>(OkexAccountBillSubType.ManuallyTransferIn, "201"),
            new KeyValuePair<OkexAccountBillSubType, string>(OkexAccountBillSubType.SystemTransferOut, "202"),
            new KeyValuePair<OkexAccountBillSubType, string>(OkexAccountBillSubType.ManuallyTransferOut, "203"),
        };
}