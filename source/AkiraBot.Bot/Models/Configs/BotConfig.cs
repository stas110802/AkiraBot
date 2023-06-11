using Newtonsoft.Json;

namespace AkiraBot.Bot.Models.Configs;

public sealed class BotConfig
{
    [JsonProperty("clients")]
    public ConfigClients? Clients { get; set; }
    
    [JsonProperty("smtp")]
    public SmtpHost? Smtp { get; set; }
    
    [JsonProperty("recipientsMail")]
    public List<string>? Recipients { get; set; }
}