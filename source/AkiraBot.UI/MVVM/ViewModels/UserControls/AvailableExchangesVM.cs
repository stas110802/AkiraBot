using System.Collections.ObjectModel;
using AkiraBot.Bot;
using AkiraBot.ExchangeClients;
using AkiraBot.ExchangeClients.Clients;
using AkiraBot.ExchangesRestAPI.Options;
using AkiraBot.UI.Core;
using AkiraBot.UI.MVVM.Models;

namespace AkiraBot.UI.MVVM.ViewModels.UserControls;

public class AvailableExchangesVM : ObservableObject
{
    private ObservableCollection<AvailableExchange> _availableExchanges;
    private AvailableExchange _selectedAvailableExchange;
    
    public AvailableExchangesVM()
    {
        _availableExchanges = new ObservableCollection<AvailableExchange>
        {
            new()
            {
                Name = "Binance",
                Description = "Binance, is a global company that operates the largest cryptocurrency exchange." +
                              "\nBinance was founded in 2017 by Changpeng Zhao, a developer who had previously created high frequency trading software. ",
                ImagePath = "Z:\\ProgrammingWorld\\Projects\\C#\\BotAlphaUI\\Images\\binance.png"
            },
            new()
            {
                Name = "NiceHash",
                Description = "Best crypto trading exchange...",
                ImagePath = "C:\\Projects\\AkiraBot\\source\\AkiraBot.UI\\Images\\niceHash.jpg"
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
        
        if (SelectedAvailableExchange.Name == "Binance")
        {
            
        }
        else if (SelectedAvailableExchange.Name == "NiceHash")
        {
            SelectedExchange = new NiceHashClient(new NiceHashOptions
            {
                PublicKey = botKeys.Key,
                SecretKey = botKeys.SecretKey,
                OrganizationId = botKeys.OrgID
            });
        }
    }
}