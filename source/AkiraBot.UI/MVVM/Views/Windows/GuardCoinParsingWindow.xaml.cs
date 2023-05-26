using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using AkiraBot.UI.MVVM.Models;
using AkiraBot.UI.MVVM.ViewModels.Windows;

namespace AkiraBot.UI.MVVM.Views.Windows;

public partial class GuardCoinParsingWindow : Window
{
    public GuardCoinParsingWindow(GuardCoinParameters info)
    {
        InitializeComponent();
        DataContext = new GuardCoinParsingVM(info);
    }
    
    [DllImport("user32.dll")]
    private static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);
    
    private void ControlBarPanelOnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        var helper = new WindowInteropHelper(this);
        SendMessage(helper.Handle, 161, 2, 0);
    }
}