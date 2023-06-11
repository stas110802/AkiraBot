using System.Windows;
using System.Windows.Input;

namespace AkiraBot.UI.MVVM.Views.Windows;

public partial class RegisterWindow : Window
{
    public RegisterWindow()
    {
        InitializeComponent();
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
        //Application.Current.Shutdown();
        Close();
    }

    private void btnLogin_Click(object sender, RoutedEventArgs e)
    {
        new ApplicationWindow().Show();
        Close();
    }
}