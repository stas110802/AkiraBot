using System.Windows.Forms;
using AkiraBot.Bot;
using AkiraBot.ExchangeClients;
using AkiraBot.ExchangeClients.Clients;
using AkiraBot.ExchangesRestAPI.Options;
using AkiraBot.UI.Core;

namespace AkiraBot.UI.MVVM.ViewModels.UserControls;

public class CryptoTradingVM : ObservableObject, ICommandInitializer
{
    private string _firstCoin;
    private string _secondCoin;
    private decimal _amount;
    private IExchangeClient _client;
    
    public CryptoTradingVM()
    {
        _client = AvailableExchangesVM.SelectedExchange;
        InitializeCommands();
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
    
    public decimal Amount
    {
        get => _amount;
        set => Set(ref _amount, value, nameof(Amount));
    }
    
    public BaseCommand CreateOrderCommand { get; set; }
    
    public void InitializeCommands()
    {
        CreateOrderCommand = new BaseCommand(CreateOrder);
    }
    
    private void CreateOrder(object args = null)
    {
        var currencyPair = $"{FirstCoin}{SecondCoin}";
        var result = _client.CreateSellOrder(currencyPair, Amount);
        // добавить результат в БД, отправить сообщ на почту
        if (result)
        {
            MessageBox.Show("Успешно");
        }
        else
        {
            MessageBox.Show("Ошибка!");
        }
    }
}