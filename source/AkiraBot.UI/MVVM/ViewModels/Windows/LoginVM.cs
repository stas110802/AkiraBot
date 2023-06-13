using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows.Forms;
using AkiraBot.Domain.Repositories;
using AkiraBot.UI.Core;
using AkiraBot.UI.MVVM.Views.Windows;

namespace AkiraBot.UI.MVVM.ViewModels.Windows;

public class LoginVM : ObservableObject, ICommandInitializer
{
    private readonly UserRepository _repository;
    private string _login;
    
    public LoginVM()
    {
        _repository = new UserRepository();
        InitializeCommands();
    }
    
    public string Login
    {
        get => _login;
        set => Set(ref _login, value, nameof(Login));
    }

    public SecureString SecurePassword { private get; set; }
    public BaseCommand AuthorizeUserCommand { get; set; }
    public BaseCommand OpenRegisterFormCommand { get; set; }
    public event EventHandler OnFrameStopped;
    
    public void InitializeCommands()
    {
        AuthorizeUserCommand = new BaseCommand(AuthorizeUser);
        OpenRegisterFormCommand = new BaseCommand(OpenRegisterForm);
    }
    
    private void AuthorizeUser(object? args = null)
    {
        var users = _repository.GetAll();
        foreach (var user in users)
        {
            if(user.Login != Login) 
                continue;
            
            var unmanagedPtr = Marshal.SecureStringToGlobalAllocUnicode(SecurePassword);
            var strPsw = Marshal.PtrToStringUni(unmanagedPtr);//  bruh(
            if (user.Password != strPsw) 
                break;
            
            ApplicationWindowVM.TotalUser = user;
            new ApplicationWindow().Show();
            OnFrameStopped.Invoke(null, EventArgs.Empty);

            return;
        }

        MessageBox.Show("Неверный логин или пароль");
    }

    private void OpenRegisterForm(object? args = null)
    {
        new RegisterWindow().Show();
        OnFrameStopped.Invoke(null, EventArgs.Empty);
    }
}