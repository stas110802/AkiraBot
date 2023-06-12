using System.ComponentModel.DataAnnotations.Schema;

namespace AkiraBot.Domain.Models;

[Table("ArbitrageOrder")]
public class ArbitrageOrder : EntityBase
{
    [Column("SellCoin")]
    public string FirstCoin { get; set; }
    
    [Column("BuyCoin")]
    public string SecondCoin { get; set; }
    
    [Column("Amount")]
    public decimal Amount { get; set; }

    [Column("BuyPrice")]
    public decimal BuyPrice { get; set; }
    
    [Column("SellPrice")]
    public decimal SellPrice { get; set; }
    
    [Column("ProfitPercentage")]
    public decimal ProfitPercentage { get; set; }
    
    [Column("FirstExchange")]
    public string FirstExchange { get; set; }
    
    [Column("SecondExchange")]
    public string SecondExchange { get; set; }
    
    [Column("OrderDate")]
    public DateTime OrderDate { get; set; }
    
    [Column("FK_User")]
    public int FKUser { get; set; }

    #region Virtual fields
    
    [ForeignKey(nameof(FKUser))]
    public virtual User User { get; set; }
    
    #endregion
}