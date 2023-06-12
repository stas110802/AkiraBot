using AkiraBot.Bot.Enums;
using AkiraBot.Bot.Interfaces;
using AkiraBot.Utilities.CommonTools;

namespace AkiraBot.Bot.Models.Logs;

public class ArbitrageLog : ILog
{
    public ArbitrageLog(ArbitrageInfo info)
    {
        ArbitrageInfo = info;
        Theme = SubjectTheme.StartSales;
    }
    
    public ArbitrageInfo ArbitrageInfo { get; set; }
    public string FilePath => $"{PathHelper.PathList.OrderPath}{DateTime.Now:dd.MM.yyyy}.json";
    public SubjectTheme? Theme { get; init; }
    
    public override string ToString()
    {
        return "Арбитражная сделка!";
    }
}