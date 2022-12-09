namespace Okex.Net.Converters;

internal class InsuranceTypeConverter : BaseConverter<OkexInsuranceType>
{
    public InsuranceTypeConverter() : this(true) { }
    public InsuranceTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkexInsuranceType, string>> Mapping => new List<KeyValuePair<OkexInsuranceType, string>>
    {
        new KeyValuePair<OkexInsuranceType, string>(OkexInsuranceType.All, "all"),
        new KeyValuePair<OkexInsuranceType, string>(OkexInsuranceType.LiquidationBalanceDeposit, "liquidation_balance_deposit"),
        new KeyValuePair<OkexInsuranceType, string>(OkexInsuranceType.BankruptcyLoss, "bankruptcy_loss"),
        new KeyValuePair<OkexInsuranceType, string>(OkexInsuranceType.PlatformRevenue, "platform_revenue"),
    };
}