using AkiraBot.Bot;
using AkiraBot.Bot.Enums;
using AkiraBot.Bot.Models;
using AkiraBot.ExchangeClients;
using AkiraBot.ExchangeClients.Clients;
using AkiraBot.ExchangesRestAPI.Options;
using AkiraBot.UI.Core;
using AkiraBot.UI.MVVM.Views.Windows;

namespace AkiraBot.UI.MVVM.ViewModels.UserControls;

public class ArbitrageBotVM : ObservableObject, ICommandInitializer
{
    private string _firstCoin;
    private string _secondCoin;
    private int _amount;
    private string _firstExchange;
    private string _secondExchange;
    private string _candleType;
    
    public ArbitrageBotVM()
    {
        InitializeCommands();
    }
    
    public string CandleType
    {
        get => _candleType;
        set => Set(ref _candleType, value.Replace("System.Windows.Controls.ComboBoxItem: ", ""));
    }
    
    public string SecondExchange
    {
        get => _secondExchange;
        set => Set(ref _secondExchange, value.Replace("System.Windows.Controls.ComboBoxItem: ", ""));
    }
    
    public string FirstExchange
    {
        get => _firstExchange;
        set => Set(ref _firstExchange, value.Replace("System.Windows.Controls.ComboBoxItem: ", ""));
    }
    
    public string FirstCoin
    {
        get => _firstCoin;
        set => Set(ref _firstCoin, value);
    }

    public string SecondCoin
    {
        get => _secondCoin;
        set => Set(ref _secondCoin, value);
    }

    public int Amount
    {
        get => _amount;
        set => Set(ref _amount, value);
    }
    
    public BaseCommand StartBotCommand { get; set; }
    
    public void InitializeCommands()
    {
        StartBotCommand = new BaseCommand(StartBot);
    }
    
    private void StartBot(object? obj = null)
    {
        IExchangeClient first;
        IExchangeClient second;
        InitClients(out first, out second);
        
        var info = new ArbitrageInfo
        {
            FirstCoin = FirstCoin,
            SecondCoin = SecondCoin,
            Amount = Amount,
            FirstClient = first,
            SecondClient = second,
            Type = GetCandleType()
        };

        new ArbitrageBotParsingWindow(info).Show();
    }

    private void InitClients(out IExchangeClient first, out IExchangeClient second)
    {
        var cfg = ConfigInitializer.GetClientConfig();

        if (FirstExchange == "Binance")
        {
            first = new BinanceClient(new BinanceOptions
            {
                PublicKey = cfg.BinanceInfo.PublicKey,
                SecretKey = cfg.BinanceInfo.SecretKey
            });
        }
        else
        {
            first = new NiceHashClient(new NiceHashOptions()
            {
                PublicKey = cfg.NiceHashInfo.PublicKey,
                SecretKey = cfg.NiceHashInfo.SecretKey,
                OrganizationId = cfg.NiceHashInfo.OrganizationId
            });
        }
        
        if (SecondExchange == "Binance")
        {
            second = new BinanceClient(new BinanceOptions
            {
                PublicKey = cfg.BinanceInfo.PublicKey,
                SecretKey = cfg.BinanceInfo.SecretKey
            });
        }
        else
        {
            second = new NiceHashClient(new NiceHashOptions()
            {
                PublicKey = cfg.NiceHashInfo.PublicKey,
                SecretKey = cfg.NiceHashInfo.SecretKey,
                OrganizationId = cfg.NiceHashInfo.OrganizationId
            });
        }
    }

    private CandleType GetCandleType()
    {
        return CandleType == "15 min" ? Bot.Enums.CandleType.FifteenMin : Bot.Enums.CandleType.ThirtyMin;
    }
}