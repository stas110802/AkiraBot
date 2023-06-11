using AkiraBot.Bot.Interfaces;
using AkiraBot.Bot.Logs;
using AkiraBot.Bot.Models.Configs;
using AkiraBot.Bot.Models.Logs;
using AkiraBot.ExchangeClients;
using AkiraBot.ExchangeClients.Models;
using AkiraBot.Utilities.CommonTools;

namespace AkiraBot.Bot;

public sealed class TakeProfitStopLossBot
{
    private readonly IExchangeClient _client;
    private readonly BotLogger _botLogger;
    private readonly CurrencyInfo _currencyInfo;
    
    public TakeProfitStopLossBot(IExchangeClient client, CurrencyInfo currencyInfo)
    {
        _client = client;
        _currencyInfo = currencyInfo;
        _botLogger = new BotLogger
        {
            RecipientsEmails = ConfigInitializer.GetRecipientMails(),
            SmtpSender = new SmtpSender(ConfigInitializer.GetSmtpEmailConfig())
        };
    }

    /// <summary>
    /// Starts parsing prices from the exchange
    /// and sells coins if the price has reached the required limit
    /// </summary>
    public ILog StartBot()
    {
        ILog log;
        var currency = _currencyInfo.FirstCoin + _currencyInfo.SecondCoin;

        try
        {
            var launchLog = GetTotalCurrencyInfo(currency);
            _botLogger.AddLog(launchLog);

            while (true)
            {
                var balance = _client.GetAccountBalance()
                    .FirstOrDefault(x => x.Currency == _currencyInfo.FirstCoin);
                if (balance is null)
                   throw new Exception("Нулевой баланс!");
                    
                var parseLog = GetTotalCurrencyInfo(currency, balance);

                var currentPrice = parseLog.TotalPrice;
                if (currentPrice <= _currencyInfo.UpperPrice &&
                    currentPrice >= _currencyInfo.BottomPrice)
                {
                    Thread.Sleep(10000);
                    continue;
                }
                
                // if the balance is not active, then do not create an order
                if (balance.IsActive is false)
                {
                    log = WriteErrorLog("Заброкированнный баланс!");
                    break;
                }

                var amount = balance.AvailableBalance;
                if (amount < _currencyInfo.BalanceLimit)
                {
                    ConsoleHelper.LoadingBar(10);
                    continue;
                }

                // create sell order
                var orderResult = _client.CreateSellOrder(currency, amount);// MARKET ORDER
                if (orderResult)
                {
                    var orderLog = new OrderLog(
                    options: _currencyInfo, sellPrice: currentPrice, amount: amount);
                    _botLogger.AddLog(orderLog);
                    log = orderLog;
                }
                else
                {
                    log = WriteErrorLog($"Неудачная попытка разместить ордер на продажу {_currencyInfo.FirstCoin}-{_currencyInfo.SecondCoin}");
                }
            }
        }
        catch (Exception error)
        {
            log = WriteErrorLog(error.Message);
        }

        return log;
    }
    
    private CurrencyLog GetTotalCurrencyInfo(string currency, CurrencyBalance? balance = null)
    {
        
        var price = _client.GetCurrencyPrice(currency); 
        balance ??= _client
            .GetAccountBalance()
            .FirstOrDefault(x => x.Currency == _currencyInfo.FirstCoin);
        var accBalance = balance?.AvailableBalance ?? 0 ;
        var log = new CurrencyLog(_currencyInfo, price, accBalance);

        return log;
    }
    
    private ErrorLog WriteErrorLog(string message)
    {
        var errorLog = new ErrorLog(message);
        _botLogger.AddLog(errorLog);

        return errorLog;
    }
    
    private void RestartBot(string? error = null)
    {
        if(string.IsNullOrEmpty(error) is false)
            WriteErrorLog($"Сбой работы бота. {error}.\nБот будет перезапущен!");
        
        ConsoleHelper.LoadingBar(30, "restart application", 1, 3);
        StartBot();
    }
}