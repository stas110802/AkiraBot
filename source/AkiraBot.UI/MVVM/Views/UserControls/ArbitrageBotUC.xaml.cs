using System.Windows.Controls;
using AkiraBot.Bot;
using AkiraBot.Bot.Models;
using AkiraBot.ExchangeClients.Clients;
using AkiraBot.ExchangesRestAPI.Options;
using AkiraBot.UI.MVVM.ViewModels.UserControls;

namespace AkiraBot.UI.MVVM.Views.UserControls;

public partial class ArbitrageBotUC : UserControl
{
    public ArbitrageBotUC()
    {
        InitializeComponent();
        DataContext = new ArbitrageBotVM();
        
        var botKeys = ConfigInitializer.GetClientConfig();
        var bt = new ArbitrageBot(new ArbitrageInfo
        {
            FirstClient = new BinanceClient(new BinanceOptions()
            {
                PublicKey = botKeys.BinanceInfo.PublicKey,
                SecretKey = botKeys.BinanceInfo.SecretKey
            }),
            SecondClient = new NiceHashClient(new NiceHashOptions
            {
                PublicKey = botKeys.NiceHashInfo.PublicKey,
                SecretKey = botKeys.NiceHashInfo.SecretKey,
                OrganizationId = botKeys.NiceHashInfo.OrganizationId
            }),
            Amount = 1,
            FirstCoin = "BTC",
            SecondCoin = "USDT"
        });
        bt.StartBot();
    }
}