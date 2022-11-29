using AkiraBot.ExchangeClients.Models;
using AkiraBot.ExchangesRestAPI.Api;
using AkiraBot.ExchangesRestAPI.Options;
using AkiraBot.ExchangesRestAPI.Types;
using AkiraBot.ExchangesRestAPI.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace AkiraBot.ExchangeClients.Clients;

public class BinanceClient : BaseClient, IExchangeClient
{
    private readonly CustomRestApi<BinanceRequest> _server;
    
    public BinanceClient(BinanceOptions options)
    {
        _server = new CustomRestApi<BinanceRequest>(options);
    }
    
    public CurrencyPair GetCurrencyInfo(string currency)
    {
        throw new NotImplementedException();
    }

    public decimal GetCurrencyPrice(string currency)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<CurrencyBalance> GetAccountBalance()
    {
        var timestamp = TimestampHelper.GetUtcTimestamp();
        var query = $"?timestamp={timestamp}";
        var response = _server.CreateRequest(Method.Get, BinanceEndpoint.AccountInfo, query)
            .Authorize()
            .Execute();
        
        var balancesToken = JObject.Parse(response).SelectToken("balances");
        if (balancesToken == null)
        {
            throw new JsonException("[SelectToken ERROR] : Unable to deserialize response and get balances");
        }

        return GetListOfAccountAssets(balancesToken.ToString());
    }

    public CurrencyBalance GetCurrencyBalance(string currency)
    {
        throw new NotImplementedException();
    }

    public bool CreateSellOrder(string currency, decimal amount, decimal price)
    {
        throw new NotImplementedException();
    }

    public bool CreateSellOrder(string currency, decimal amount)
    {
        throw new NotImplementedException();
    }

    public bool CancelAllOrders()
    {
        throw new NotImplementedException();
    }

    public void GetMyOrders()
    {
        throw new NotImplementedException();
    }
}