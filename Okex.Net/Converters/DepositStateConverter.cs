namespace Okex.Net.Converters;

internal class DepositStateConverter : BaseConverter<OkexDepositState>
{
    public DepositStateConverter() : this(true) { }
    public DepositStateConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkexDepositState, string>> Mapping => new List<KeyValuePair<OkexDepositState, string>>
    {
        new KeyValuePair<OkexDepositState, string>(OkexDepositState.WaitingForConfirmation, "1"),
        new KeyValuePair<OkexDepositState, string>(OkexDepositState.Credited, "2"),
        new KeyValuePair<OkexDepositState, string>(OkexDepositState.Successful, "3"),
        new KeyValuePair<OkexDepositState, string>(OkexDepositState.Pending, "4"),
    };
}