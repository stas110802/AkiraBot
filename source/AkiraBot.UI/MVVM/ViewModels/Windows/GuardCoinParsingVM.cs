using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using AkiraBot.Bot;
using AkiraBot.Bot.Models.Configs;
using AkiraBot.Bot.Models.Logs;
using AkiraBot.ExchangeClients;
using AkiraBot.ExchangeClients.Clients;
using AkiraBot.ExchangesRestAPI.Options;
using AkiraBot.UI.Core;
using AkiraBot.UI.MVVM.Models;
using AkiraBot.UI.MVVM.ViewModels.UserControls;

namespace AkiraBot.UI.MVVM.ViewModels.Windows;

public class GuardCoinParsingVM : ObservableObject
{
    private decimal _currentPrice;
    private double _progressBarValue;
    private decimal _currentBalance;
    private readonly IExchangeClient _client;
    private readonly TakeProfitStopLossBot _bot;
    
    public double ProgressBarValue
    {
        get => _progressBarValue;
        set => Set(ref _progressBarValue, value, nameof(ProgressBarValue));
    }

    public GuardCoinParsingVM(GuardCoinParameters info)
    {
        Information = info;
        _client = AvailableExchangesVM.SelectedExchange;
        _bot = new TakeProfitStopLossBot(_client, new CurrencyInfo//todo исправить под 1 модель, брух
        {
            FirstCoin = Information.FirstCoin,
            SecondCoin = Information.SecondCoin,
            UpperPrice = Information.UpperPrice,
            BottomPrice = Information.BottomPrice,
            BalanceLimit = 0
        });
        var task = Task.Factory.StartNew(ParsingData);
    }
    
    public GuardCoinParameters Information { get; set; }
    public decimal CurrentPrice
    {
        get => _currentPrice;
        set => Set(ref _currentPrice, value, nameof(CurrentPrice));
    }
    public decimal CurrentBalance
    {
        get => _currentBalance;
        set => Set(ref _currentBalance, value, nameof(CurrentBalance));
    }

    private void ParsingData()
    {
        var currency = Information.FirstCoin + Information.SecondCoin;
        var task = Task.Factory.StartNew(() =>
        {
            var result = _bot.StartBot();
            if (result is ErrorLog errorLog)
            {
                MessageBox.Show($"{errorLog.Message}\n{errorLog.ErrorDate}");
            }
            else if (result is OrderLog orderLog)
            {
                MessageBox.Show($"Успешная сделка!");
            }
        });
       
        while (true)
        {
            if(task.IsCompleted)
                return;
            CurrentPrice = _client.GetCurrencyPrice(currency);
            var balance = _client.GetAccountBalance()
                .FirstOrDefault(x => x.Currency == Information.FirstCoin);
            if(balance != null)
                CurrentBalance = balance.AvailableBalance;
            StartProgressBar();
        }
    }

    private void StartProgressBar()
    {
        for (var i = 0; i < 100; i += 1)
        {
            if (ProgressBarValue >= 100)
                ProgressBarValue = 0;
            
            ProgressBarValue += 1;
            //Thread.Sleep(100);
            Task.WaitAll(new Task[] { Task.Delay(100) });
        }
    }
}