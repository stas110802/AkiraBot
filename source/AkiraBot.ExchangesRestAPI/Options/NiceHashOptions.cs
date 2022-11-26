using System.Text;

namespace AkiraBot.ExchangesRestAPI.Options;

public sealed class NiceHashOptions : IApiOptions
{
    public NiceHashOptions()
    {
        BaseUri = "https://api2.nicehash.com";
        Encoding = Encoding.Default;
    }
    
    public string BaseUri { get; set; }
    public string OrganizationId { get; set; }
    public string PublicKey { get; set; }
    public string SecretKey { get; set; }
    public Encoding Encoding { get; }
}