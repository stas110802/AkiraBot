using System;
using AkiraBot.UI.Core;
using AkiraBot.UI.MVVM.Models;
using AkiraBot.UI.MVVM.Views.Windows;

namespace AkiraBot.UI.MVVM.ViewModels.UserControls;

public sealed class GuardCoinVM : ObservableObject
{
    private string _firstCoin;
    private string _secondCoin;
    private int _upperPrice;
    private int _bottomPrice;
    
    public GuardCoinVM()
    {
        InitializeCommands();
        FirstCoin = "BTC";
        SecondCoin = "USDT";
        UpperPrice = 35000;
        BottomPrice = 10000;
    }
    
    public string FirstCoin
    {
        get => _firstCoin;
        set => Set(ref _firstCoin, value, nameof(FirstCoin));
    }

    public string SecondCoin
    {
        get => _secondCoin;
        set => Set(ref _secondCoin, value, nameof(SecondCoin));
    }
    
    public int UpperPrice
    {
        get => _upperPrice;
        set => Set(ref _upperPrice, value, nameof(UpperPrice));
    }

    public int BottomPrice
    {
        get => _bottomPrice;
        set => Set(ref _bottomPrice, value, nameof(BottomPrice));
    }
    
    public BaseCommand StartBotCommand { get; set; }
    
    private void StartBot(object? obj = null)
    {
        new GuardCoinParsingWindow( new GuardCoinParameters()
        {
            FirstCoin = _firstCoin.ToUpper(),
            SecondCoin = _secondCoin.ToUpper(),
            UpperPrice = _upperPrice,
            BottomPrice = _bottomPrice
        }).Show();
    }

    private void InitializeCommands()
    {
        StartBotCommand = new BaseCommand(StartBot);
    }
}