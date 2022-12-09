namespace Okex.Net.Objects.Core;

public class OkexRestApiError : Error
{
    public OkexRestApiError(int? code, string message, object data) : base(code, message, data)
    {
    }
}