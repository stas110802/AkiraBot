using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using AkiraBot.UI.MVVM.ViewModels.UserControls;
using AkiraBot.Utilities.CommonTools.Models;

namespace AkiraBot.UI.MVVM.Views.UserControls;

public partial class HomeViewUC : UserControl
{
    public HomeViewUC()
    {
        InitializeComponent();
        DataContext = new HomeViewVM();
        var pl = new PathList();
        BackImg.ImageSource = new BitmapImage(new Uri($"{pl.ImagesPath}visa.jpg", UriKind.Relative));
    }
}