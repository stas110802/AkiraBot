using AkiraBot.ExchangesRestAPI.Models;
using RestSharp;

namespace AkiraBot.ExchangesRestAPI.Api;

public interface IExchangeRestApi
{
    public BaseRequest CreateRequest(Method method, Enum niceHashEndpoint, string? query = null, string? payload = null);
}