using System.ComponentModel.DataAnnotations;
using AkiraBot.ExchangesRestAPI.Options;
using RestSharp;
using static System.String;

namespace AkiraBot.ExchangesRestAPI.Models;

public abstract class BaseRequest
{   
    [Required] public RestRequest Request { get; set; }
    [Required] public RestClient Client { get; set; }
    [Required] public string EndpointValue { get; set; }
    [Required] public string FullPath { get; set; }
    public string? Payload { get; set; }
    [Required] public IApiOptions Options { get; set; }
    
    public abstract BaseRequest Authorize(bool isAdditionalLogic = false);

    public abstract BaseRequest WithPayload(string payload);

    public virtual string Execute()
    {
        var result = Client.Execute(Request, Request.Method).Content;
        if (IsNullOrEmpty(result)) throw new Exception("[REST-API] Request fetch error.");
        
        return result;
    }
}