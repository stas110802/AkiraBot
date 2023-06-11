using System.ComponentModel.DataAnnotations.Schema;

namespace AkiraBot.Domain.Models;

[Table("OrderType")]
public class OrderType : IdentifierRealize
{
    public string Type { get; set; }
}