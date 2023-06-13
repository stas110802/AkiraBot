using AkiraBot.Bot.Interfaces;
using AkiraBot.Bot.Logs;
using AkiraBot.Bot.Models;
using AkiraBot.Bot.Models.Logs;

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

    public ILog StartBot()
    {
        ILog log;
        var currencyPair = $"{_arbitrageInfo.FirstCoin}{_arbitrageInfo.SecondCoin}";
        try
        {
            while (true)
            {
                Thread.Sleep(3000);
                var candle1 = _arbitrageInfo.FirstClient.GetCurrencyPrice(currencyPair);
                var candle2 = _arbitrageInfo.SecondClient.GetCurrencyPrice(currencyPair);
                if (candle1 < candle2)
                    continue;
            
                var difference = (candle1 / candle2 - 1) * 100; 
                if(difference < 3) 
                    continue;

                var firstOrder = _arbitrageInfo.SecondClient.CreateSellOrder(
                    _arbitrageInfo.SecondCoin + _arbitrageInfo.FirstCoin, _arbitrageInfo.Amount);
                if (firstOrder is false)
                {
                    log = WriteErrorLog("[Арбитражный бот] первая сделка на покупки не удалась");
                    break;
                }
            
                var withdrawal = _arbitrageInfo.SecondClient.WithdrawalCurrency(
                    _arbitrageInfo.FirstCoin, _arbitrageInfo.Amount, "");
                if (withdrawal is false)
                {
                    log = WriteErrorLog("[Арбитражный бот] перевод валюты на другую биржу не удался");
                    break;
                }
            
                Thread.Sleep(3000);
                var newAmount = _arbitrageInfo.FirstClient.GetCurrencyBalance(
                    _arbitrageInfo.FirstCoin).AvailableBalance;
                var secondOrder = _arbitrageInfo.FirstClient.CreateSellOrder(currencyPair, newAmount);
                if (secondOrder is false)
                {
                    log = WriteErrorLog("[Арбитражный бот] вторая сделка на продажу не удалась");
                    break;
                }
                var arbitrageLog = new ArbitrageLog(_arbitrageInfo);
                log = arbitrageLog;
            }
        }
        catch (Exception e)
        {
            log = WriteErrorLog(e.Message);
        }

        return log;
    }
    
    private ErrorLog WriteErrorLog(string message)
    {
        var errorLog = new ErrorLog(message);
        _botLogger.AddLog(errorLog);

        return errorLog;
    }
}