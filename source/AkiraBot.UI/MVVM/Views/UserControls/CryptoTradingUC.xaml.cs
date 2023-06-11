using System.Windows.Controls;
using AkiraBot.UI.MVVM.ViewModels.UserControls;

namespace AkiraBot.UI.MVVM.Views.UserControls;

public partial class CryptoTradingUC : UserControl
{
    public CryptoTradingUC()
    {
        InitializeComponent();
        DataContext = new CryptoTradingVM();
    }
}