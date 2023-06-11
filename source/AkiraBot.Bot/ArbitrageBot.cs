using AkiraBot.Bot.Logs;
using AkiraBot.Bot.Models;
using AkiraBot.Bot.Models.Configs;
using AkiraBot.ExchangeClients;

namespace AkiraBot.Bot;

public class ArbitrageBot
{
    private readonly BotLogger _botLogger;
    private readonly ArbitrageInfo _arbitrageInfo;
    
    public ArbitrageBot(ArbitrageInfo info)
    {
        _arbitrageInfo = info;
        
        _botLogger = new BotLogger
        {
            RecipientsEmails = ConfigInitializer.GetRecipientMails(),
            SmtpSender = new SmtpSender(ConfigInitializer.GetSmtpEmailConfig())
        };
    }

    public void StartBot()
    {
        var currencyPair = $"{_arbitrageInfo.FirstCoin}{_arbitrageInfo.SecondCoin}";
        
        var candle1 = _arbitrageInfo.FirstClient.GetCurrencyPrice(currencyPair);
        var candle2 = _arbitrageInfo.SecondClient.GetCurrencyPrice(currencyPair);
        
        
    }
}