using AkiraBot.Bot;
using AkiraBot.ExchangeClients.Clients;
using AkiraBot.ExchangesRestAPI.Options;
using AkiraBot.UI.Core;

namespace AkiraBot.UI.MVVM.ViewModels.UserControls;

public class HomeViewVM : ObservableObject
{
    private string _eterniumPrice;
    private string _bitcoinPrice;

    public HomeViewVM()
    {
        var cfg = ConfigInitializer.GetClientConfig()?.NiceHashInfo;
        if (cfg != null)
        {
           AvailableExchangesVM.SelectedExchange = new NiceHashClient(new NiceHashOptions
           {
               PublicKey = cfg.PublicKey,
               SecretKey = cfg.SecretKey,
               OrganizationId = cfg.OrganizationId
           }); 
        }

        BitcoinPrice = $"{AvailableExchangesVM.SelectedExchange.GetCurrencyPrice("BTCUSDT")}$";
        EterniumPrice = $"{AvailableExchangesVM.SelectedExchange.GetCurrencyPrice("ETHUSDT")}$";
    }
    
    public string EterniumPrice
    {
        get => _eterniumPrice;
        set => Set(ref _eterniumPrice, value);
    }

    public string BitcoinPrice
    {
        get => _bitcoinPrice;
        set => Set(ref _bitcoinPrice, value);
    }
}