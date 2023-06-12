using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows.Forms;
using AkiraBot.Domain.Models;
using AkiraBot.Domain.Repositories;
using AkiraBot.UI.Core;
using AkiraBot.UI.MVVM.Views.Windows;

namespace AkiraBot.UI.MVVM.ViewModels.Windows;

public class RegisterVM : ObservableObject, ICommandInitializer
{
    private string? _userName;
    private string? _email;
    private readonly UserRepository _repository;
    
    public RegisterVM()
    {
        InitializeCommands();
        _repository = new UserRepository();
    }
    
    public SecureString SecurePassword { private get; set; }
    public event EventHandler OnFrameStopped;
    public BaseCommand RegisterUserCommand { get; set; }
    public string? UserName
    {
        get => _userName;
        set => Set(ref _userName, value, nameof(UserName));
    }

    public string? Email
    {
        get => _email;
        set => Set(ref _email, value, nameof(Email));
    }
    
    public void InitializeCommands()
    {
        RegisterUserCommand = new BaseCommand(RegisterUser);
    }

    private void RegisterUser(object? args = null)
    {
        if (CheckForNullableFields())
        {
            MessageBox.Show("Нулевые параметры");
            return;
        }
        
        if (UserName.Length < 5)
        {
            MessageBox.Show("Длина имени должна быть не меньше 5 символов");
            return;
        }

        var isCreateByLogin =  _repository.IsUserCreateByLogin(UserName);
        if (isCreateByLogin)
        {
            MessageBox.Show("Данный логин уже зарегистрирован");
            return;
        }
        
        var isCreateByEmail =  _repository.IsUserCreateByEmail(Email);
        if (isCreateByEmail)
        {
            MessageBox.Show("Данная почта уже зарегистрирована");
            return;
        }

        var addressAttribute = new EmailAddressAttribute();
        var isValidEmail = addressAttribute.IsValid(Email);
        if (isValidEmail is false)
        {
            MessageBox.Show("Почта неверного формата");
            return;
        }

        var unmanagedPtr = Marshal.SecureStringToGlobalAllocUnicode(SecurePassword);
        var strPsw = Marshal.PtrToStringUni(unmanagedPtr);//  bruh(
        if (strPsw is { Length: < 7 })
        {
            MessageBox.Show("Пароль должен содержать минимум 7 символов");
            return;
        }
        
        var newUser = new User
        {
            Login = UserName,
            Email = Email,
            Password = strPsw,
            RegisterDate = DateTime.Now
        };
        _repository.Create(newUser);
        new LoginWindow().Show();
        OnFrameStopped.Invoke(null, EventArgs.Empty);
    }

    private bool CheckForNullableFields()
    {
        return UserName == null || Email == null;
    }
}