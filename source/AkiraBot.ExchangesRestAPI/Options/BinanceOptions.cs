namespace AkiraBot.ExchangesRestAPI.Options;

public class BinanceOptions : IApiOptions
{
    public string BaseUri { get; set; }
    public string PublicKey { get; set; }
    public string SecretKey { get; set; }
}