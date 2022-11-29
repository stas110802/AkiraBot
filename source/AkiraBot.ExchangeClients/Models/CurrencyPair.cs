namespace AkiraBot.ExchangeClients.Models;

public class CurrencyPair
{
    public string Currency { get; set; }
    public decimal SellingPrice { get; set; }
    public decimal TradingVolume { get; set; }
}