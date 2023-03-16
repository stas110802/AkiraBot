using System.Runtime.Serialization;
using AkiraBot.Bot.Enums;
using AkiraBot.Bot.Interfaces;
using AkiraBot.Bot.Models.Configs;
using AkiraBot.Utilities.CommonTools;

namespace AkiraBot.Bot.Models.Logs;

public sealed class CurrencyLog : ILog
{
    public CurrencyLog() { }

    public CurrencyLog(CurrencyInfo options, decimal totalPrice, decimal availableBalance)
    {
        ParsingDate = DateTime.UtcNow;
        Info = options;
        TotalPrice = totalPrice;
        AvailableBalance = availableBalance;
        Theme = SubjectTheme.StartSales;
    }
    
    [DataMember]
    public CurrencyInfo Info { get; set; }
    
    [DataMember]
    public decimal AvailableBalance { get; set; }
    
    [DataMember]
    public decimal TotalPrice { get; set; }

    [DataMember]
    public DateTime ParsingDate { get; set; }

    public string FilePath => $"{PathHelper.PathList.LaunchesPath}{DateTime.Now:dd.MM.yyyy}.json";
    
    public SubjectTheme? Theme { get; init; }

    public override string ToString()
    {
        return $"Продаем: {Info.FirstCoin} за {Info.SecondCoin}\n" +
               $"Дата: {ParsingDate}\n" +
               $"Рекомендуемая цена: {Info.UpperPrice} {Info.SecondCoin}\n" +
               $"Критическая цена: {Info.BottomPrice} {Info.SecondCoin}\n" +
               $"Текущий курс: {TotalPrice} {Info.SecondCoin}\n" +
               $"Лимит баланса: {Info.BalanceLimit} {Info.FirstCoin}\n" +
               $"Баланс: {AvailableBalance} {Info.FirstCoin}";
    }
}