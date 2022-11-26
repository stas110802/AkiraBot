using AkiraBot.ExchangesRestAPI.Models;
using AkiraBot.ExchangesRestAPI.Options;
using AkiraBot.Utilities.EnumUtility;
using RestSharp;

namespace AkiraBot.ExchangesRestAPI.Api;

public class CustomRestApi<TRequest> : IExchangeRestApi
    where TRequest : BaseRequest, new()
{
    private readonly IApiOptions _options;

    public CustomRestApi(IApiOptions options)
    {
        _options = options;
    }

    public BaseRequest CreateRequest(Method method, Enum niceHashEndpoint, string? query = null, string? payload = null)
    {
        var strEndpoint = niceHashEndpoint.ToDescription();
        var full = strEndpoint + query;
        
        var result = new TRequest
        {
            Request = new RestRequest(full),
            Client = new RestClient(_options.BaseUri),
            FullPath = full,
            EndpointValue = niceHashEndpoint.ToDescription(),
            Payload = payload,
            Options = _options
        };

        return result;
    }
}