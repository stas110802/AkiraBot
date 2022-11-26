namespace AkiraBot.ExchangeClients.Models;

public class ICurrencyBalance
{
    public bool IsActive { get; set; }
    public string Currency { get; set; }
    public decimal TotalBalance { get; set; }
    public decimal AvailableBalance { get; set; }
    public decimal Debt { get; set; }
    public decimal Pending { get; set; }
}