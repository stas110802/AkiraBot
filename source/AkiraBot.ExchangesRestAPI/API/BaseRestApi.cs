using RestSharp;

namespace AkiraBot.ExchangesRestAPI.API;

public sealed class BaseRestApi
{
    public string GetResponseContent(Method method, Enum endpoint, bool auth = false, string? query = null)
    {
        return string.Empty;
    }
}