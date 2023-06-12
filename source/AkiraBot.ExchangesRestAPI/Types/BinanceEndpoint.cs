using System.ComponentModel;

namespace AkiraBot.ExchangesRestAPI.Types;

public enum BinanceEndpoint
{
    [Description("/api/v3/account")] AccountInfo, 
    [Description("/api/v3/ticker/price")] CurrentPrice
}