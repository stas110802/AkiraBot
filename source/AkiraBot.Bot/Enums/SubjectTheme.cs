using System.ComponentModel;

namespace AkiraBot.Bot.Enums;

public enum SubjectTheme
{
    [Description("Ошибка")] Error,
    [Description("Продажа")] Sell,
    [Description("Активация продажи")] StartSales
}