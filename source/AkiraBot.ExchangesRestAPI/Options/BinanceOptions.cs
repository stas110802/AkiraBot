namespace AkiraBot.ExchangesRestAPI.Options;

public class BinanceOptions : IApiOptions
{
    public BinanceOptions()
    {
        BaseUri = "https://api.binance.com";
    }
    public string BaseUri { get; set; }
    public string PublicKey { get; set; }
    public string SecretKey { get; set; }
}