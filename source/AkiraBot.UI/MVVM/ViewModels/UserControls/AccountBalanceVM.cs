using System.Collections.ObjectModel;
using AkiraBot.UI.Core;
using AkiraBot.UI.MVVM.Models;
using AkiraBot.Utilities.CommonTools.Models;

namespace AkiraBot.UI.MVVM.ViewModels.UserControls;

public class AccountBalanceVM : ObservableObject
{
    private ObservableCollection<AccountBalance> _accountBalance;
    public AccountBalanceVM()
    {
        var balance = AvailableExchangesVM.SelectedExchange.GetAccountBalance();
        var pl = new PathList();
        _accountBalance = new ObservableCollection<AccountBalance>
        {
            new AccountBalance
            {
                Balance = 6.83m,
                Currency = "USD",
                ImagePath = $"{pl.ImagesPath}usdt.png"
            },
            new AccountBalance
            {
                Balance = 0.00125m,
                Currency = "BTC",
                ImagePath = $"{pl.ImagesPath}BTC.jpg"
            },
            new AccountBalance
            {
                Balance = 26,
                Currency = "USDT",
                ImagePath = $"{pl.ImagesPath}usdt.png"
            }
        };
    }

    public ObservableCollection<AccountBalance> AccountBalance
    {
        get => _accountBalance;
        set => Set(ref _accountBalance, value, nameof(AccountBalance));
    }
}