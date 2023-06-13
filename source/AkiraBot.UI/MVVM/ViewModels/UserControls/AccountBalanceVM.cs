using System.Collections.ObjectModel;
using AkiraBot.UI.Core;
using AkiraBot.UI.MVVM.Models;

namespace AkiraBot.UI.MVVM.ViewModels.UserControls;

public class AccountBalanceVM : ObservableObject
{
    private ObservableCollection<AccountBalance> _accountBalance;
    public AccountBalanceVM()
    {
        var balance = AvailableExchangesVM.SelectedExchange.GetAccountBalance();
        _accountBalance = new ObservableCollection<AccountBalance>
        {
            new AccountBalance
            {
                Balance = 6.83m,
                Currency = "USD",
                ImagePath = @"C:\Users\SosiskaKiller\Documents\usdt.png"
            },
            new AccountBalance
            {
                Balance = 0.00125m,
                Currency = "BTC",
                ImagePath = @"C:\Users\SosiskaKiller\Documents\BTC.jpg"
            },
            new AccountBalance
            {
                Balance = 26,
                Currency = "USDT",
                ImagePath = @"C:\Users\SosiskaKiller\Documents\usdt.png"
            }
        };
    }

    public ObservableCollection<AccountBalance> AccountBalance
    {
        get => _accountBalance;
        set => Set(ref _accountBalance, value, nameof(AccountBalance));
    }
}