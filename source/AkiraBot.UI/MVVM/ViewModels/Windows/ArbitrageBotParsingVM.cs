using System.Threading.Tasks;
using System.Windows.Forms;
using AkiraBot.Bot;
using AkiraBot.Bot.Models;
using AkiraBot.Bot.Models.Logs;
using AkiraBot.UI.Core;

namespace AkiraBot.UI.MVVM.ViewModels.Windows;

public class ArbitrageBotParsingVM : ObservableObject
{
    private double _progressBarValue;
    private ArbitrageInfo _arbitrageInfo;
    private decimal _firstPrice;
    private decimal _secondPrice;
    private decimal _difference;

    public ArbitrageBotParsingVM(ArbitrageInfo info)
    {
        ArbitrageInfo = info;
        var task = Task.Factory.StartNew(ParsingData);
    }
    
    public decimal Difference
    {
        get => _difference;
        set => Set(ref _difference, value);
    }
    
    public decimal FirstPrice
    {
        get => _firstPrice;
        set => Set(ref _firstPrice, value);
    }

    public decimal SecondPrice
    {
        get => _secondPrice;
        set => Set(ref _secondPrice, value);
    }
    
    public ArbitrageInfo ArbitrageInfo 
    { 
        get => _arbitrageInfo;
        set => Set(ref _arbitrageInfo, value);
    }
    
    public double ProgressBarValue
    {
        get => _progressBarValue;
        set => Set(ref _progressBarValue, value, nameof(ProgressBarValue));
    }
    
    private void ParsingData()
    {
        var currency = ArbitrageInfo.FirstCoin + ArbitrageInfo.SecondCoin;
        var task = Task.Factory.StartNew(() =>
        {
            var bot = new ArbitrageBot(ArbitrageInfo);
            var resultLog = bot.StartBot();
            if (resultLog is ErrorLog errorLog)
            {
                MessageBox.Show($"{errorLog.Message}\n{errorLog.ErrorDate}");
            }
            else if (resultLog is ArbitrageLog arbitrageLog)
            {
                MessageBox.Show($"Успешная сделка!");
            }
        });
        
        while (true)
        {
            if(task.IsCompleted)
                return;

            FirstPrice = ArbitrageInfo.FirstClient.GetCurrencyPrice(currency);
            SecondPrice = ArbitrageInfo.SecondClient.GetCurrencyPrice(currency);
            if (FirstPrice > SecondPrice)
            {
                Difference = (FirstPrice / SecondPrice - 1) * 100; 
            }
            
            StartProgressBar();
        }
    }

    private void StartProgressBar()
    {
        for (var i = 0; i < 100; i += 1)
        {
            if (ProgressBarValue >= 100)
                ProgressBarValue = 0;
            
            ProgressBarValue += 1;
            //Thread.Sleep(100);
            Task.WaitAll(new Task[] { Task.Delay(100) });
        }
    }
}