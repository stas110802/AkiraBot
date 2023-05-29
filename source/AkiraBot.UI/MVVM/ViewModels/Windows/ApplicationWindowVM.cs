using AkiraBot.UI.Core;
using AkiraBot.UI.MVVM.ViewModels.UserControls;

namespace AkiraBot.UI.MVVM.ViewModels.Windows;

public class ApplicationWindowVM : ObservableObject
{
    private string _captionName;
    private ObservableObject _mainContent;
    
    public ApplicationWindowVM()
    {
        InitializeCommands();
        OpenHomeView();
    }
    
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
    
    private void ChangeScreenFrame(ObservableObject view, string captionName)
    {
        MainContent = view;
        CaptionName = captionName;
    }

    private void InitializeCommands()
    {
        AvailableExchangesCommand = new BaseCommand(OpenAvailableExchangesView);
        HomeViewCommand = new BaseCommand(OpenHomeView);
        GuardCoinCommand = new BaseCommand(OpenGuardCoinView);
        ArbitrageBotCommand = new BaseCommand(OpenArbitrageBot);
        BalanceCommand = new BaseCommand(OpenBalance);
    }
}