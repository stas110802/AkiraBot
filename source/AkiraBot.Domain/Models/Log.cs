using System.ComponentModel.DataAnnotations.Schema;

namespace AkiraBot.Domain.Models;

[Table("Log")]
public class Log : EntityBase
{
    public string Message { get; set; }
    public DateTime SendDate { get; set; }
    
    [Column("FK_LogType")]
    public int FKLogType { get; set; }
    
    [Column("FK_User")]
    public int FKUser { get; set; }
    
    [ForeignKey(nameof(FKLogType))]
    public virtual LogType LogType { get; set; }
    
    [ForeignKey(nameof(FKUser))]
    public virtual User User { get; set; }
}