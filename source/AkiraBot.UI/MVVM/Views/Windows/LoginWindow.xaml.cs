using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AkiraBot.UI.MVVM.ViewModels.Windows;

namespace AkiraBot.UI.MVVM.Views.Windows;

public partial class LoginWindow : Window
{
    public LoginWindow()
    {
        InitializeComponent();
        var vm = new LoginVM();
        vm.OnFrameStopped += (_, _) => { Close(); };
        DataContext = vm;
    }
    
    private void Window_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed)
            DragMove();
    }

    private void btnMinimize_Click(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Minimized;
    }

    private void btnClose_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
    
    private void PasswordBoxPasswordChanged(object sender, RoutedEventArgs e)
    {
        if (DataContext != null)
        {
            ((dynamic)DataContext).SecurePassword = ((PasswordBox)sender).SecurePassword;
        }
    }
}