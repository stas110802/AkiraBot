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
using AkiraBot.ExchangeClients;
using AkiraBot.ExchangeClients.Clients;
using AkiraBot.ExchangesRestAPI.Options;
using AkiraBot.UI.Core;
using AkiraBot.UI.MVVM.Models;

namespace AkiraBot.UI.MVVM.ViewModels.Windows;

public class GuardCoinParsingVM : ObservableObject
{
    private decimal _currentPrice;
    private double _progressBarValue;
    private decimal _currentBalance;
    private IExchangeClient _client;
    
    public double ProgressBarValue
    {
        get => _progressBarValue;
        set => Set(ref _progressBarValue, value, nameof(ProgressBarValue));
    }

    public GuardCoinParsingVM(GuardCoinParameters info)
    {
        Information = info;
        
        var botKeys = ConfigInitializer.GetClientConfig();
        _client = new NiceHashClient(new NiceHashOptions
        {
            PublicKey = botKeys.Key,
            SecretKey = botKeys.SecretKey,
            OrganizationId = botKeys.OrgID
        });
        var task = Task.Factory.StartNew(() =>
        {
            ParsingData();
        });
        
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
        
        while (true)
        {
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
        for (var i = 0; i < 100; i += 10)
        {
            if (ProgressBarValue >= 100)
                ProgressBarValue = 0;
            
            ProgressBarValue += 1;
            Thread.Sleep(100);
            // Task.WaitAll(new Task[] { Task.Delay(2000) });
        }
    }
}