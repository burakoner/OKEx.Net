namespace Okex.Net.Converters;

internal class WithdrawalStateConverter : BaseConverter<OkexWithdrawalState>
{
    public WithdrawalStateConverter() : this(true) { }
    public WithdrawalStateConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkexWithdrawalState, string>> Mapping => new List<KeyValuePair<OkexWithdrawalState, string>>
    {
        new KeyValuePair<OkexWithdrawalState, string>(OkexWithdrawalState.PendingCancel, "-3"),
        new KeyValuePair<OkexWithdrawalState, string>(OkexWithdrawalState.Canceled, "-2"),
        new KeyValuePair<OkexWithdrawalState, string>(OkexWithdrawalState.Failed, "-1"),
        new KeyValuePair<OkexWithdrawalState, string>(OkexWithdrawalState.Pending, "0"),
        new KeyValuePair<OkexWithdrawalState, string>(OkexWithdrawalState.Sending, "1"),
        new KeyValuePair<OkexWithdrawalState, string>(OkexWithdrawalState.Sent, "2"),
        new KeyValuePair<OkexWithdrawalState, string>(OkexWithdrawalState.AwaitingEmailVerification, "3"),
        new KeyValuePair<OkexWithdrawalState, string>(OkexWithdrawalState.AwaitingManualVerification, "4"),
        new KeyValuePair<OkexWithdrawalState, string>(OkexWithdrawalState.AwaitingIdentityVerification, "5"),
    };
}