using System.Security.Cryptography;
using System.Text;
using AkiraBot.ExchangesRestAPI.Options;
using AkiraBot.ExchangesRestAPI.Types;
using AkiraBot.Utilities.EnumUtility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace AkiraBot.ExchangesRestAPI.API;

public sealed class NiceHashAPI
{
    private readonly NiceHashOptions _options;
    
    public NiceHashAPI(NiceHashOptions options)
    {
        _options = options;
    }

    public string GetResponseContent(Method method, NHEndpoint endpoint, bool auth = false, string? query = null,
        bool requestId = false, string? extraEndpoint = null, string? payload = null)
    {
        var strEndpoint = endpoint.ToDescription();
        var full = strEndpoint + query;

        if (extraEndpoint != null)
            strEndpoint += extraEndpoint;
        
        var client = new RestClient(_options.BaseUri);
        var request = new RestRequest(full);

        // user authentication
        if (auth)
        {
            var orgId = _options.OrganizationId;
            var time = GetServerTimestamp();
            var nonce = Guid.NewGuid().ToString();
            var strMethod = method.ToString().ToUpper();
            
            var digest = HashBySegments(time, nonce, orgId, strMethod, strEndpoint, GetQuery(full), payload);

            if (requestId)
            {
                request.AddHeader("X-Request-Id", nonce);
            }

            request.AddHeader("X-Time", time);
            request.AddHeader("X-Nonce", nonce);
            request.AddHeader("X-Auth", _options.PublicKey + ":" + digest);
            request.AddHeader("X-Organization-Id", orgId);
        }

        // if need 'application/json' data
        if (payload != null)
        {
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-type", "application/json");
            request.AddJsonBody(payload);
        }

        var response = client.Execute(request, method);
        var content = response.Content;

        if (string.IsNullOrEmpty(content))
        {
            throw new HttpRequestException("[API ERROR] : Server not responded");
        }

        return content;
    }

    private string GetServerTimestamp()
    {
        var timeResponse = GetResponseContent(Method.Get, NHEndpoint.ServerTime);

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
            _options.PublicKey,
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

        return CalcHMACSHA256Hash(JoinSegments(segments), _options.SecretKey);
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

    private static string JoinSegments(List<string?> segments)
    {
        var sb = new StringBuilder();
        var first = true;

        foreach (var segment in segments)
        {
            if (first == false)
            {
                sb.Append('\x00');
            }
            else
            {
                first = false;
            }

            if (segment != null)
            {
                sb.Append(segment);
            }
        }

        return sb.ToString();
    }
    
    // todo Check this realization !
    private static string JoinSegments2(List<string?> segments)
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

    private string CalcHMACSHA256Hash(string plaintext, string salt)
    {
        var encoding = _options.Encoding;

        var baText2BeHashed = encoding.GetBytes(plaintext);
        var baSalt = encoding.GetBytes(salt);
        var hasher = new HMACSHA256(baSalt);
        var baHashedText = hasher.ComputeHash(baText2BeHashed);

        var result = string.Join("", baHashedText.ToList()
            .Select(b => b.ToString("x2")).ToArray());

        return result;
    }
}