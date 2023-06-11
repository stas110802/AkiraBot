using System.ComponentModel.DataAnnotations.Schema;

namespace AkiraBot.Domain;

public abstract class IdentifierRealize
{
    [Column("ID")]
    public int Id { get; set; }
}