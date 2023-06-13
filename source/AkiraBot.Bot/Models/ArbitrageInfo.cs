using AkiraBot.Bot.Enums;
using AkiraBot.ExchangeClients;
using AkiraBot.ExchangeClients.Clients;

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

    public string FirstExchangeName => FirstClient is BinanceClient ? "Binance" : "NiceHash";
    public string SecondExchangeName => SecondClient is BinanceClient ? "Binance" : "NiceHash";
}