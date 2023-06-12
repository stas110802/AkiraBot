using System.ComponentModel.DataAnnotations.Schema;

namespace AkiraBot.Domain;

public abstract class EntityBase
{
    [Column("ID")]
    public int Id { get; set; }
}