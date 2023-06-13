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
    }
}