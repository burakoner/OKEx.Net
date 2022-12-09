namespace Okex.Net.Converters;

internal class WithdrawalDestinationConverter : BaseConverter<OkexWithdrawalDestination>
{
    public WithdrawalDestinationConverter() : this(true) { }
    public WithdrawalDestinationConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkexWithdrawalDestination, string>> Mapping => new List<KeyValuePair<OkexWithdrawalDestination, string>>
    {
        new KeyValuePair<OkexWithdrawalDestination, string>(OkexWithdrawalDestination.OKEx, "3"),
        new KeyValuePair<OkexWithdrawalDestination, string>(OkexWithdrawalDestination.DigitalCurrencyAddress, "4"),

    };
}