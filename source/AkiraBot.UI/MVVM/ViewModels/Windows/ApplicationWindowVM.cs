using System.Diagnostics;
using AkiraBot.Domain.Models;
using AkiraBot.UI.Core;
using AkiraBot.UI.MVVM.ViewModels.UserControls;
using AkiraBot.Utilities.CommonTools.Models;

namespace AkiraBot.UI.MVVM.ViewModels.Windows;

public class ApplicationWindowVM : ObservableObject, ICommandInitializer
{
    private string _captionName;
    private ObservableObject _mainContent;
    
    public ApplicationWindowVM()
    {
        InitializeCommands();
        OpenHomeView();
    }

    public static User TotalUser;
    public string CaptionName
    {
        get => _captionName;
        set => Set(ref _captionName, value, nameof(CaptionName));
    }

    public ObservableObject MainContent
    {
        get => _mainContent;
        set => Set(ref _mainContent, value, nameof(MainContent));
    }

    public BaseCommand AvailableExchangesCommand { get;  set; }
    public BaseCommand HomeViewCommand { get; set; }
    public BaseCommand GuardCoinCommand { get; set; }
    public BaseCommand ArbitrageBotCommand { get; set; }
    public BaseCommand BalanceCommand { get; set; }
    public BaseCommand TradingCommand { get; set; }
    public BaseCommand ConfigCommand { get; set; }
    
    public void InitializeCommands()
    {
        AvailableExchangesCommand = new BaseCommand(OpenAvailableExchangesView);
        HomeViewCommand = new BaseCommand(OpenHomeView);
        GuardCoinCommand = new BaseCommand(OpenGuardCoinView);
        ArbitrageBotCommand = new BaseCommand(OpenArbitrageBot);
        BalanceCommand = new BaseCommand(OpenBalance);
        TradingCommand = new BaseCommand(OpenTradingCrypto);
        ConfigCommand = new BaseCommand(OpenConfig);
    }
    
    private void OpenAvailableExchangesView(object? args = null)
    {
        ChangeScreenFrame(new AvailableExchangesVM(), "Available Exchanges");
    }
    
    private void OpenHomeView(object? args = null)
    {
        ChangeScreenFrame(new HomeViewVM(), "Home");
    }
    
    private void OpenGuardCoinView(object? args = null)
    {
        ChangeScreenFrame(new GuardCoinVM(), "Coin Guard");
    }
    
    private void OpenArbitrageBot(object? args = null)
    {
        ChangeScreenFrame(new ArbitrageBotVM(), "Arbitrage BOT");
    }

    private void OpenBalance(object? args = null)
    {
        ChangeScreenFrame(new AccountBalanceVM(), "Balance");
    }
    
    private void OpenTradingCrypto(object? args = null)
    {
        ChangeScreenFrame(new CryptoTradingVM(), "Market");
    }
    
    private void OpenConfig(object? args = null)
    {
        var pathList = new PathList();
        Process.Start("explorer.exe", pathList.ConfigsPath);
    }
    
    private void ChangeScreenFrame(ObservableObject view, string captionName)
    {
        MainContent = view;
        CaptionName = captionName;
    }
}