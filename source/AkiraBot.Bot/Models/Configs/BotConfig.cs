namespace AkiraBot.Bot.Models.Configs;

public sealed class BotConfig
{
    public BotKeys? Client { get; set; }
    public SmtpHost? Smtp { get; set; }
    public List<string>? Recipients { get; set; }
}