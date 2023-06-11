using Newtonsoft.Json;

namespace AkiraBot.Bot.Models.Configs;

public class BinanceConfig
{
    [JsonProperty("key")]
    public string PublicKey { get; set; }
    
    [JsonProperty("secretKey")]
    public string SecretKey { get; set; }
}