using System.Text;
using AkiraBot.ExchangesRestAPI.Models;
using AkiraBot.ExchangesRestAPI.Options;
using AkiraBot.ExchangesRestAPI.Types;
using AkiraBot.ExchangesRestAPI.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;


namespace AkiraBot.ExchangesRestAPI.Api;

public class NiceHashRequest : BaseRequest
{
    public override NiceHashRequest Authorize(bool isRequestId)
    {
        var orgId = ChildOptions.OrganizationId;
        var time = GetServerTimestamp();
        var nonce = Guid.NewGuid().ToString();
        var strMethod = Request.Method.ToString().ToUpper();
            
        var digest = HashBySegments(time, nonce, orgId, strMethod, EndpointValue, GetQuery(FullPath), Payload);

        if (isRequestId)
        {
            Request.AddHeader("X-Request-Id", nonce);
        }

        Request.AddHeader("X-Time", time);
        Request.AddHeader("X-Nonce", nonce);
        Request.AddHeader("X-Auth", ChildOptions.PublicKey + ":" + digest);
        Request.AddHeader("X-Organization-Id", orgId);

        return this;
    }

    public override NiceHashRequest WithPayload(string payload)
    {
        Request.AddHeader("Accept", "application/json");
        Request.AddHeader("Content-type", "application/json");
        Request.AddJsonBody(payload);

        return this;
    }

    private NiceHashOptions ChildOptions 
        => Options as NiceHashOptions ?? throw new Exception("Wrong options");

    private string GetServerTimestamp()
    {
        var publicApi = new CustomRestApi<NiceHashRequest>(new NiceHashOptions());// todo can create static factory for public api's
        var timeResponse = publicApi.CreateRequest(Method.Get, NiceHashEndpoint.ServerTime).Execute();

        if (string.IsNullOrEmpty(timeResponse))
        {
            throw new Exception("[API ERROR] : The server is not responding");
        }

        var serverTimeObject = JsonConvert.DeserializeObject<JToken>(timeResponse);
        var time = serverTimeObject?["serverTime"]?.ToString();

        if (time == null)
            throw new NullReferenceException("[ERROR] Server timestamp is null");
        
        return time;
    }

    private string HashBySegments(string time, string nonce, string orgId, string method, string encodedPath, string? query, string? bodyStr)
    {
        var segments = new List<string?>
        {
            ChildOptions.PublicKey,
            time,
            nonce,
            null,
            orgId,
            null,
            method,
            encodedPath,
            query
        };

        if (string.IsNullOrEmpty(bodyStr) == false)
        {
            segments.Add(bodyStr);
        }

        return HashCalculator.CalculateHMACSHA256Hash(
            JoinSegments(segments), ChildOptions.SecretKey);
    }
    
    private static string GetBasePath(string url)
    {
        var arrSplit = url.Split('?');

        return arrSplit[0];
    }
    
    private static string? GetQuery(string url)
    {
        var arrSplit = url.Split('?');

        return arrSplit.Length == 1 ? null : arrSplit[1];
    }

    private static string JoinSegments(IReadOnlyList<string?> segments)
    {
        var sb = new StringBuilder();
        sb.Append(segments[0]);

        foreach (var segment in segments.Skip(1))
        {
            sb.Append('\x00');
            if (segment == null) continue;
            sb.Append(segment);
        }

        return sb.ToString();
    }
}