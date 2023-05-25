using System.Windows.Controls;
using AkiraBot.UI.MVVM.ViewModels.UserControls;

namespace AkiraBot.UI.MVVM.Views.UserControls;

public partial class HomeViewUC : UserControl
{
    public HomeViewUC()
    {
        InitializeComponent();
        DataContext = new HomeViewVM();
    }
}