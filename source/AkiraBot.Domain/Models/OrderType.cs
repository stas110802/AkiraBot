using System.ComponentModel.DataAnnotations.Schema;

namespace AkiraBot.Domain.Models;

[Table("OrderType")]
public class OrderType : EntityBase
{
    public string Type { get; set; }
}