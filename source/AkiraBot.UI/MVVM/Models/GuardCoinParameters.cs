namespace AkiraBot.UI.MVVM.Models;

public sealed class GuardCoinParameters
{
    public string FirstCoin { get; set; }
    public string SecondCoin { get; set; }
    public int UpperPrice { get; set; }
    public int BottomPrice { get; set; }
}