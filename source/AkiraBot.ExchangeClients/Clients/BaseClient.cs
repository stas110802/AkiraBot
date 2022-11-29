using AkiraBot.ExchangeClients.Models;
using Newtonsoft.Json;

namespace AkiraBot.ExchangeClients.Clients;

public abstract class BaseClient
{
    /// <summary>
    /// Deserialize response to list of CurrencyBalance
    /// </summary>
    /// <param name="response"></param>
    /// <returns></returns>
    protected static IEnumerable<CurrencyBalance> GetListOfAccountAssets(string response)
    {
        var result = JsonConvert.DeserializeObject<List<CurrencyBalance>>(response)
            ?.FindAll(x => x.AvailableBalance > 0);
       
        result ??= new List<CurrencyBalance>();
        
        return result;
    }
}