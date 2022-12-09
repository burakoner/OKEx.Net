namespace Okex.Net.Objects.Core;

public class OkexApiAddresses
{
    public string UnifiedAddress { get; set; }

    public static OkexApiAddresses Default = new OkexApiAddresses
    {
        UnifiedAddress = "https://www.okx.com",
    };
}
