using System.Windows.Controls;
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