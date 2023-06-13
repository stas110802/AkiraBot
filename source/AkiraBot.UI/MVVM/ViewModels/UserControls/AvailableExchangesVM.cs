using System.Collections.ObjectModel;
using System.Windows.Forms;
using AkiraBot.Bot;
using AkiraBot.ExchangeClients;
using AkiraBot.ExchangeClients.Clients;
using AkiraBot.ExchangesRestAPI.Options;
using AkiraBot.UI.Core;
using AkiraBot.UI.MVVM.Models;
using AkiraBot.Utilities.CommonTools.Models;

namespace AkiraBot.UI.MVVM.ViewModels.UserControls;

public class AvailableExchangesVM : ObservableObject
{
    private ObservableCollection<AvailableExchange> _availableExchanges;
    private AvailableExchange _selectedAvailableExchange;
    
    public AvailableExchangesVM()
    {
        var pl = new PathList();
        _availableExchanges = new ObservableCollection<AvailableExchange>
        {
            new()
            {
                Name = "Binance",
                Description = "Binance, is a global company that operates the largest cryptocurrency exchange." +
                              "\nBinance was founded in 2017 by Changpeng Zhao, a developer who had previously created high frequency trading software. ",
                ImagePath = $"{pl.ImagesPath}binance.png"
            },
            new()
            {
                Name = "NiceHash",
                Description = "Best crypto trading exchange...",
                ImagePath = $"{pl.ImagesPath}niceHash.jpg"
            }
        };
    }

    public static IExchangeClient SelectedExchange;
    
    public ObservableCollection<AvailableExchange> AvailableExchanges
    {
        get => _availableExchanges;
        set => Set(ref _availableExchanges, value, nameof(AvailableExchanges));
    }
    
    public AvailableExchange SelectedAvailableExchange
    {
        get => _selectedAvailableExchange;
        set
        {
            Set(ref _selectedAvailableExchange, value, nameof(SelectedAvailableExchange));
            InitializeClient();
        }
    }

    private void InitializeClient()
    {
        var botKeys = ConfigInitializer.GetClientConfig();
        var name = SelectedAvailableExchange.Name;
        if (name == "Binance")
        {
            SelectedExchange = new BinanceClient(new BinanceOptions
            {
                PublicKey = botKeys.BinanceInfo.PublicKey,
                SecretKey = botKeys.BinanceInfo.SecretKey
            });
        }
        else if (name == "NiceHash")
        {
            SelectedExchange = new NiceHashClient(new NiceHashOptions
            {
                PublicKey = botKeys.NiceHashInfo.PublicKey,
                SecretKey = botKeys.NiceHashInfo.SecretKey,
                OrganizationId = botKeys.NiceHashInfo.OrganizationId
            });
        }
        MessageBox.Show($"{name} выбран!");
    }
}