using AkiraBot.ExchangeClients.Models;

namespace AkiraBot.ExchangeClients;

public static class ExchangeModelsFactory
{
    public static Dictionary<string, string> GetMappingProperties<T>()
        where T : class
    {
        var result = new Dictionary<string, string>();
        
        var type = typeof(T);
        if (type == typeof(CurrencyBalance))
            result = GetCurrencyBalanceMappingProperties();
            
        return result;
    }
    
    private static Dictionary<string, string> GetCurrencyBalanceMappingProperties()
    {// todo fix this code
        var result = new Dictionary<string, string>
        {
            {"asset", "currency"},
            {"free", "available"}
        };

        return result;
    }
}