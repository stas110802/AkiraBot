using System.Windows.Controls;
using AkiraBot.UI.MVVM.ViewModels.UserControls;

namespace AkiraBot.UI.MVVM.Views.UserControls;

public partial class AvailableExchangesUC : UserControl
{
    public AvailableExchangesUC()
    {
        InitializeComponent();
        DataContext = new AvailableExchangesVM();
    }
}