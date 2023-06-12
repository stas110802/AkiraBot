using System.ComponentModel.DataAnnotations.Schema;

namespace AkiraBot.Domain.Models;

[Table("TakeProfitStopLossOrder")]
public class TakeProfitStopLossOrder : EntityBase
{
    [Column("SellCoin")]
    public string FirstCoin { get; set; }
    
    [Column("BuyCoin")]
    public string SecondCoin { get; set; }
    
    [Column("Amount")]
    public decimal Amount { get; set; }

    [Column("Price")]
    public decimal Price { get; set; }
    
    [Column("OrderDate")]
    public DateTime OrderDate { get; set; }
    
    [Column("FK_User")]
    public int FKUser { get; set; }

    #region Virtual fields
    
    [ForeignKey(nameof(FKUser))]
    public virtual User User { get; set; }
    
    #endregion
}