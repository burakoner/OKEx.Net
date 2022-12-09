namespace Okex.Net.Converters;

internal class LightningDepositAccountConverter : BaseConverter<OkexLightningDepositAccount>
{
    public LightningDepositAccountConverter() : this(true) { }
    public LightningDepositAccountConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkexLightningDepositAccount, string>> Mapping => new List<KeyValuePair<OkexLightningDepositAccount, string>>
    {
        new KeyValuePair<OkexLightningDepositAccount, string>(OkexLightningDepositAccount.Spot, "1"),
        new KeyValuePair<OkexLightningDepositAccount, string>(OkexLightningDepositAccount.Funding, "6"),
    };
}