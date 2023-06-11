using AkiraBot.Bot.Models.Configs;
using AkiraBot.Utilities.CommonTools;

namespace AkiraBot.Bot;

public static class ConfigInitializer
{
    static ConfigInitializer()
    {
        PathHelper.CheckForFileExists(ConfigFilePath);
        InitConfig();
    }

    public static BotConfig? Config { get; set; }

    public static ConfigClients? GetClientConfig()
    {
        return Config?.Clients;
     }

    public static SmtpHost? GetSmtpEmailConfig()
    {
        return Config?.Smtp;
    }

    public static string[]? GetRecipientMails()
    {
        return Config?.Recipients?.ToArray();
    }

    public static void InitConfig()
    {
        Config = GetConfig<BotConfig>(ConfigFilePath);
    }

    public static void WriteNewConfig(BotConfig cfg)
    {
        JsonHelper.WriteDataAt(ConfigFilePath ,cfg);
        Config = cfg;
    }
    
    public static void UpdateClientInfo(ConfigClients client)
    {
        if (Config == null) return;
        Config.Clients = client;
        WriteNewConfig(Config);
    }

    public static void UpdateSmtpInfo(SmtpHost smtpHost)
    {
        if (Config == null) return;
        Config.Smtp = smtpHost;
        WriteNewConfig(Config);
    }

    public static void UpdateRecipientInfo(List<string> recipients)
    {
        if (Config == null) return;
        Config.Recipients = recipients;
        WriteNewConfig(Config);
    }

    private static string ConfigFilePath => $"{PathHelper.PathList.ConfigsPath}cfg.json";
    
    private static T? GetConfig<T>(string path)
        where T : class
    {
        return JsonHelper.GetDeserializeObject<T>(path);
    }
}