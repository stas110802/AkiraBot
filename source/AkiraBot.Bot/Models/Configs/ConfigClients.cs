using Newtonsoft.Json;

namespace AkiraBot.Bot.Models.Configs;

public sealed class ConfigClients 
{
    [JsonProperty("nicehash")]
    public NiceHashConfig NiceHashInfo { get; set; }
    
    [JsonProperty("binance")]
    public BinanceConfig BinanceInfo { get; set; }
}