namespace AkiraBot.Bot.Models.Configs;

public sealed class BotKeys // todo У крипто-бирж разные данные (тут сделано под NiceHash)
{
    public string Key { get; set; }
    public string SecretKey { get; set; }
    public string OrgID { get; set; }
}