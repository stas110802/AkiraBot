using System.Globalization;
using AkiraBot.ExchangeClients.Models;
using AkiraBot.ExchangesRestAPI.Api;
using AkiraBot.ExchangesRestAPI.Options;
using AkiraBot.ExchangesRestAPI.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace AkiraBot.ExchangeClients.Clients;

public sealed class NiceHashClient : IExchangeClient
{
    private readonly CustomRestApi<NiceHashRequest> _server;

    public NiceHashClient(NiceHashOptions options)
    {
        _server = new CustomRestApi<NiceHashRequest>(options);
    }
    
    public CurrencyPair GetCurrencyInfo(string currency)
    {
        var response = _server.CreateRequest(Method.Get, NiceHashEndpoint.CurrentPrices)
            .Execute();
        var deserialize = JsonConvert.DeserializeObject<JToken>(response);

        foreach (var item in deserialize)//todo create method in base class BaseExchangeClient
        {
            if (currency != item.Path) continue;

            var currencyPair = new CurrencyPair
            {
                Currency = currency
            };

            var isSuccessfully = decimal.TryParse(item.First.ToString(), out var price);

            if (isSuccessfully == false)
                throw new Exception("[Parse ERROR] : Cannot parse the selling price");

            currencyPair.SellingPrice = price;

            return currencyPair;
        }

        throw new ArgumentException("[Argument ERROR] : Cannot find the specified currency");
    }

    public decimal GetCurrencyPrice(string currency)
    {
        var result = GetCurrencyInfo(currency);

        return result.SellingPrice;
    }

    public IEnumerable<CurrencyBalance> GetAccountBalance()
    {
        var response = _server.CreateRequest(Method.Get, NiceHashEndpoint.Balances)
            .Authorize(false)
            .Execute();
        var parse = JObject.Parse(response);
        var currencies = parse.SelectToken("currencies");
        
        if (currencies == null)
        {
            throw new JsonException("[SelectToken ERROR] : Unable to deserialize response and get currencies");
        }
        
        var result = JsonConvert.DeserializeObject<List<CurrencyBalance>>(currencies.ToString())
            ?.FindAll(x => x.AvailableBalance > 0);
       
        result ??= new List<CurrencyBalance>();
        
        return result;
    }

    // todo replace to api method
    public CurrencyBalance GetCurrencyBalance(string currency)
    {
        // get all the balance currency on the exchange
        var allCurrencies = GetAccountBalance();
        
        // find a necessary currency
        foreach (var item in allCurrencies)
        {
            if (item.Currency == currency)
            {
                return item;
            }
        }
        
        // if not found, returns an exception
        throw new ArgumentException("[Argument ERROR] : Invalid currency name");
    }

    // todo fix this method
    public bool CreateSellOrder(string currency, decimal amount, decimal price)
    {
        var strPrice = price.ToString(CultureInfo.InvariantCulture);
        var strQuantity = amount.ToString(CultureInfo.InvariantCulture);
        var query = $"?market={currency}&side=SELL&type=LIMIT&quantity={strQuantity}&price={strPrice}";
        
        var response = _server.CreateRequest(Method.Post, NiceHashEndpoint.Order, query)
            .Authorize(true)
            .Execute();
        var deserialize = JsonConvert.DeserializeObject<JToken>(response);
        
        return deserialize?["orderId"]?.ToString() != "";
    }

    public bool CreateSellOrder(string currency, decimal amount)
    {
        var strQuantity = amount.ToString(CultureInfo.InvariantCulture);
        // create post url
        var query = $"?market={currency}&side=SELL&type=MARKET&quantity={strQuantity}";
        // create an order and deserialize the received response
        var response = _server.CreateRequest(Method.Post, NiceHashEndpoint.Order, query)
            .Authorize(true)
            .Execute();
        var deserialize = JsonConvert.DeserializeObject<JToken>(response);
        
        return deserialize?["orderId"]?.ToString() != "";
    }

    public bool WithdrawalCurrency(string currency, decimal amount, string address)
    {
        var strQuantity = amount.ToString(CultureInfo.InvariantCulture);
        var query = $"?currency={currency}&amount={strQuantity}&withdrawalAddressId={address}";
        
        var response = _server.CreateRequest(Method.Post, NiceHashEndpoint.Withdrawal, query)
            .Authorize(true)
            .Execute();
        var deserialize = JsonConvert.DeserializeObject<JToken>(response);
        
        return deserialize?["id"]?.ToString() != "";
    }

    public void GetMyOrders()
    {
        throw new Exception("Method not init");
        var query = $"?market=BTCUSDT";
        var response = _server.CreateRequest(Method.Get, NiceHashEndpoint.MyOrders, query)
            .Authorize(true)
            .Execute();
        var deserialize = JsonConvert.DeserializeObject<JToken>(response);
    }

    public bool CancelAllOrders()
    {
        try
        {
            var response = _server.CreateRequest(Method.Delete, NiceHashEndpoint.CancelAllOrders)
                .Authorize(true)
                .Execute();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
    
    
}