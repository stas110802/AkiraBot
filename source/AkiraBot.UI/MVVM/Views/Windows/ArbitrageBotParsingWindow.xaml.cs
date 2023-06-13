using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using AkiraBot.Bot.Models;
using AkiraBot.UI.MVVM.ViewModels.Windows;

namespace AkiraBot.UI.MVVM.Views.Windows;

public partial class ArbitrageBotParsingWindow : Window
{
    public ArbitrageBotParsingWindow(ArbitrageInfo info)
    {
        InitializeComponent();
        DataContext = new ArbitrageBotParsingVM(info);
    }
    
    private void ButtonCloseOnClick(object sender, RoutedEventArgs e)
    {
        Close();
    }
    
    [DllImport("user32.dll")]
    private static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);
    
    private void ControlBarPanelOnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        var helper = new WindowInteropHelper(this);
        SendMessage(helper.Handle, 161, 2, 0);
    }
}