using System.ComponentModel.DataAnnotations.Schema;

namespace AkiraBot.Domain.Models;

[Table("SpotOrder")]
public class SpotOrder : EntityBase
{
    [Column("FirstCoin")]
    public string FirstCoin { get; set; }
    
    [Column("SecondCoin")]
    public string SecondCoin { get; set; }
    
    [Column("Amount")]
    public decimal Amount { get; set; }

    [Column("Price")]
    public decimal Price { get; set; }
    
    [Column("OrderDate")]
    public DateTime OrderDate { get; set; }
    
    [Column("FK_OrderType")]
    public int FKOrderType { get; set; }
    
    [Column("FK_User")]
    public int FKUser { get; set; }

    #region Virtual fields
    
    [ForeignKey(nameof(FKOrderType))]
    public virtual OrderType OrderType { get; set; }
    
    [ForeignKey(nameof(FKUser))]
    public virtual User User { get; set; }
    
    #endregion
}