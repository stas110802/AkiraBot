using AkiraBot.Bot.Enums;

namespace AkiraBot.Bot.Interfaces;

public interface ILog
{
    public string FilePath { get; }
    public SubjectTheme? Theme { get; init; }
}