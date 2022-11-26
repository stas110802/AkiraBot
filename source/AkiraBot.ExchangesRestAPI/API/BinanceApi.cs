using AkiraBot.ExchangesRestAPI.Options;
using AkiraBot.Utilities.EnumUtility;
using RestSharp;

namespace AkiraBot.ExchangesRestAPI.API;

public class BinanceApi
{
    private readonly BinanceOptions _options;
    public BinanceApi(BinanceOptions options)
    {// https://api.binance.com
        _options = options;
    }
    
    public string GetResponseContent(Method method, Enum endpoint, bool auth = false, string? query = null)
    {
        var strEndpoint = endpoint.ToDescription();
        var full = strEndpoint + query;
        var client = new RestClient();
        var request = new RestRequest(full);

        if (auth)
        {
            
        }

        var result = client.Execute(request, method).Content;
        if (string.IsNullOrEmpty(result))
        {
            throw new NullReferenceException("[REST_API] null content ref exception");
        }
        
        
        return result;
    }
}