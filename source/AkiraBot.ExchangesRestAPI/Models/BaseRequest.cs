using System.ComponentModel.DataAnnotations;
using AkiraBot.ExchangesRestAPI.Options;
using RestSharp;

namespace AkiraBot.ExchangesRestAPI.Models;

public abstract class BaseRequest
{   
    [Required] public RestRequest Request { get; set; }
    [Required] public RestClient Client { get; set; }
    [Required] public string EndpointValue { get; set; }
    [Required] public string FullPath { get; set; }
    public string? Payload { get; set; }
    [Required] public IApiOptions Options { get; set; }

    // сделать делегату с доп реализацией логики, если она необходима
    public abstract BaseRequest Authorize(bool isRequestId);

    public abstract BaseRequest WithPayload(string payload);

    public virtual string? Execute()
    {
        return Client.Execute(Request, Request.Method).Content;
    }
}