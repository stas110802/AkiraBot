using AkiraBot.Bot.Enums;
using AkiraBot.ExchangeClients;

namespace AkiraBot.Bot.Models;

public class ArbitrageInfo
{
    public ArbitrageInfo()
    {
        Type = CandleType.FifteenMin;
    }
    
    public string FirstCoin { get; set; }
    public string SecondCoin { get; set; }
    public decimal Amount { get; set; }
    public CandleType Type { get; set; }
    public IExchangeClient FirstClient { get; set; }
    public IExchangeClient SecondClient { get; set; }
}