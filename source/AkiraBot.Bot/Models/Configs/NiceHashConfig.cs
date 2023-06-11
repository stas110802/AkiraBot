using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace AkiraBot.Bot.Models.Configs;

public sealed class NiceHashConfig
{
    [JsonProperty("key")]
    public string PublicKey { get; set; }
    
    [JsonProperty("secretKey")]
    public string SecretKey { get; set; }
    
    [JsonProperty("orgId")]
    public string OrganizationId { get; set; }
}