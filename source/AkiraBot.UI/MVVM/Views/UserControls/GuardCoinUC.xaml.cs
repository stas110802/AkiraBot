using System.Windows.Controls;
using AkiraBot.UI.MVVM.ViewModels.UserControls;

namespace AkiraBot.UI.MVVM.Views.UserControls;

public partial class GuardCoinUC : UserControl
{
    public GuardCoinUC()
    {
        InitializeComponent();
        DataContext = new GuardCoinVM();
    }
}