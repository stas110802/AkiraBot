using System.Runtime.Serialization;
using AkiraBot.Bot.Enums;
using AkiraBot.Bot.Interfaces;
using AkiraBot.Bot.Models.Configs;
using AkiraBot.Utilities.CommonTools;

namespace AkiraBot.Bot.Models.Logs;

public sealed class OrderLog : ILog
{
    public OrderLog() { }

    public OrderLog(CurrencyInfo options, decimal sellPrice, decimal amount)
    {
        OrderDate = DateTime.UtcNow;
        Info = options;
        SellPrice = sellPrice;
        Amount = amount;
        Theme = SubjectTheme.Sell;
    }
    
    [DataMember]
    public decimal SellPrice { get; set; }
    
    [DataMember]
    public decimal Amount { get; set; }
    
    [DataMember]
    public CurrencyInfo Info { get; set; }
    
    [DataMember]
    public DateTime OrderDate { get; set; }

    public override string ToString()
    {
        return $"Продаем: {Info.FirstCoin} за {Info.SecondCoin}\n" +
               $"Дата: {OrderDate}\n" +
               $"Рекомендуемая цена: {Info.UpperPrice} {Info.SecondCoin}\n" +
               $"Критическая цена: {Info.BottomPrice} {Info.SecondCoin}\n" +
               $"Цена продажи: {SellPrice} {Info.SecondCoin}\n" +
               $"Количество: {Amount} {Info.FirstCoin}";
    }
    
    public string FilePath => $"{PathHelper.PathList.OrderPath}{DateTime.Now:dd.MM.yyyy}.json";
    public SubjectTheme? Theme { get; init; }
}