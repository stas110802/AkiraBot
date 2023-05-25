using System.Collections.ObjectModel;
using AkiraBot.UI.Core;
using AkiraBot.UI.MVVM.Models;

namespace AkiraBot.UI.MVVM.ViewModels.UserControls;

public class AvailableExchangesVM : ObservableObject
{
    private ObservableCollection<AvailableExchange> _availableExchanges;

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
                Name = "KuCoin",
                Description = "Best crypto trading exchange...",
                ImagePath = "Z:\\ProgrammingWorld\\Projects\\C#\\BotAlphaUI\\Images\\binance.png"
            },
            new()
            {
                Name = "Akira production",
                Description = "Best crypto trading exchange...",
                ImagePath = "Z:\\ProgrammingWorld\\Projects\\C#\\BotAlphaUI\\Images\\binance.png"
            }
        };
    }

    public ObservableCollection<AvailableExchange> AvailableExchanges
    {
        get => _availableExchanges;
        set => Set(ref _availableExchanges, value, nameof(AvailableExchanges));
    }
}