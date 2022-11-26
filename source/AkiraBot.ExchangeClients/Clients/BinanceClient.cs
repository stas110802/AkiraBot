using AkiraBot.ExchangeClients.Models;
using AkiraBot.ExchangeClients.Models.NiceHash;

namespace AkiraBot.ExchangeClients.Clients;

public class BinanceClient : IExchangeClient
{
    public BinanceClient()
    {
        
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
        throw new NotImplementedException();
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