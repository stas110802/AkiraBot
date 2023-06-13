using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using AkiraBot.Domain;
using AkiraBot.UI.MVVM.ViewModels.Windows;
using AkiraBot.Utilities.CommonTools.Models;

namespace AkiraBot.UI.MVVM.Views.Windows;

public partial class ApplicationWindow : Window
{
    public ApplicationWindow()
    {
        InitializeComponent();
        DataContext = new ApplicationWindowVM();
    }
    
    [DllImport("user32.dll")]
    private static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);
    
    private void ControlBarPanelOnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        var helper = new WindowInteropHelper(this);
        SendMessage(helper.Handle, 161, 2, 0);
    }

    private void ControlBarPanelOnMouseEnter(object sender, MouseEventArgs e)
    {
        MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
    }

    private void ButtonCloseOnClick(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void MinimizeButtonOnClick(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Minimized;
    }

    private void MaximizeButtonOnClick(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
    }
}