namespace AkiraBot.ExchangeClients.Models;

public interface ICurrencyPair
{
    public string Currency { get; set; }
    public decimal SellingPrice { get; set; }
    public decimal TradingVolume { get; set; }
}